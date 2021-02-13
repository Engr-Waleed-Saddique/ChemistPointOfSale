using Newtonsoft.Json;
using SuperShaheenChemist.Entities;
using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperShaheenChemist.Web.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateInvoice()
        {
            int billno=PurchaseService.Instance.OrdersMaxID()+1;
            ViewBag.billno = billno;
            return View();
        }
        public ActionResult ProductsWithBatchNo(string Pname)
        {
            var products = ProductsService.Instance.GetProducts();

            var ob = products.Where(x => x.ProductName.Trim().ToLower() == Pname.Trim().ToLower()).Select(x => new { x.BatchNo, x.Id }).ToList();

            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetExpiry(string batchNo)
        {
            var products = ProductsService.Instance.GetProducts();
            var ob = products.Where(x => x.BatchNo.Trim().ToLower() == batchNo.Trim().ToLower()).Select(x => new { x.ExpiryDate, x.PackRetailCost }).FirstOrDefault();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnitPrice(string BatchNo)
        {
            var products = ProductsService.Instance.GetProducts();
            var ob = products.Where(x => x.BatchNo.Trim().ToLower() == BatchNo.Trim().ToLower()).Select(x => x.UnitRetail).FirstOrDefault();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveSaleProducts(string saleItems,string CustomerName)
        {

            var result = JsonConvert.DeserializeObject<List<SaleViewModel>>(saleItems);
            OrderItem temp = new OrderItem();
            Order order = new Order();   
            decimal total = 0;
            foreach(var item in result)
            {
                total= total+Convert.ToDecimal(item.Amount);
            }
            order.TotalAmount = total;
            order.Date = DateTime.Now;
            order.CustomerName = CustomerName;
            SaleService.Instance.SaveOrder(order);
            foreach (var item in result)
            {
                temp.OrderID = SaleService.Instance.OrderMaxID();
                temp.ProductID = item.ProductID;
                temp.BatchNo = item.BatchNo;
                try
                {
                    if (item.Quantity != "")
                    {
                        temp.Quantity = int.Parse(item.Quantity);
                    }
                }
                catch(Exception e)
                {
                    temp.Quantity = 0;
                }
                try
                {
                    if (item.Loose != "")
                    {
                        temp.Loose = int.Parse(item.Loose);
                    }
                }
                catch(Exception e)
                {
                    temp.Loose = 0;
                }

                temp.Price = Convert.ToDecimal(item.Price);
                temp.ExpiryDate = DateTime.Parse(item.ExpiryDate);
                try
                {
                    if (item.Discount != "")
                    {
                        temp.Discount = int.Parse(item.Discount);
                    }
                }
                catch(Exception e)
                {
                    temp.Discount = 0;
                }
                temp.Amount = Convert.ToDecimal(item.Amount);
                SaleService.Instance.SaveSale(temp);
            }

            StockInventry stock = new StockInventry();
            //Minus the product from stock which are saled.
            foreach (var item in result)
            {
                stock.ProductId = item.ProductID;
                stock.BatchNo = item.BatchNo;
                try
                {
                    if (item.Amount != "")
                    {
                        stock.TotalAmount = int.Parse(item.Amount);
                    }
                }
                catch (Exception e)
                {
                    stock.Stock = 0;
                }

                try
                {
                    if (item.Quantity != "")
                    {
                        stock.Stock = int.Parse(item.Quantity);
                    }
                }
                catch (Exception e)
                {
                    stock.Stock = 0;
                }
                StockService.Instance.SaleProductsDeductedFromStock(stock);
            }
            int orderId = PurchaseService.Instance.OrdersMaxID();
            return Json(orderId);

        }
        [HttpGet]
        public ActionResult PreviewReceipt(int id)
        {
            List<OrderItem> data = new List<OrderItem>();
            data=PurchaseService.Instance.GetOrderDetials(id);
            decimal total = 0;
            int numberOfItems = 0;
            foreach(var item in data)
            {
                total =total+ item.Amount;
                numberOfItems++;
            }
            ViewBag.total = total;
            ViewBag.numberofitems = numberOfItems;
            ViewBag.BillNo ="";
            ViewBag.data = data;
            ViewBag.orderNo = id;
            return View();
        }
    }
}
