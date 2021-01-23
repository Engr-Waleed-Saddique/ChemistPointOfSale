using Newtonsoft.Json;
using SuperShaheenChemist.Entities;
using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.Models;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SuperShaheenChemist.Web.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SavePurchase(string purchaseItems)
        {

            
            var result = JsonConvert.DeserializeObject<List<PurchaseViewModel>>(purchaseItems);
            PurchaseProducts temp = new PurchaseProducts();
            StockInventry stock = new StockInventry();

            foreach (var pItems in result)
            {
                //Adding Product data in Purchase with DateWise
                
                
                    Product p=ProductsService.Instance.GetProductInfo(pItems.ProductID);
                temp.ProductId = p.Id;
                temp.Qty = pItems.Quantity;
                temp.Date = DateTime.Now;
                temp.TotalAmount = pItems.TotalAmount;
                ProductsService.Instance.PurchaseProduct(temp);

                //Adding Products data in stock

                stock.ProductId = pItems.ProductID;
                stock.Price = ProductsService.Instance.ProductPrice(pItems.ProductID);
                stock.Received = stock.Received + pItems.Quantity;
                stock.Stock = pItems.Quantity;
                stock.TotalAmount = stock.TotalAmount + pItems.TotalAmount;

                StockService.Instance.AddStock(stock);

            }
            return Json("Success");
        }
        public JsonResult GetProductList(string productName)
        {
            IEnumerable<Product> products = new List<Product>();
            products = ProductsService.Instance.GetProducts();
            var p= products.Where(x => x.ProductName.ToLower().Contains(productName.ToLower())
            || x.BarCode.ToLower().Contains(productName.ToLower())).Select(x => new {productID =x.Id, ProductName=x.ProductName });
            return Json(p, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getProduct(int productId)
        {
            Product product = new Product();
            product = ProductsService.Instance.GetProductById(productId);
            return Json(product,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Purchases()
        {
            return View();
        }
        public JsonResult GetData(JqueryDatatableParam param,string fromDate, string toDate)
        {

            var products = ProductsService.Instance.PurchaseSearchByDate(fromDate, toDate);

            //Pagination
            var displayResult = products.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();


            List<PurchaseTableViewModel> p = new List<PurchaseTableViewModel>();
            PurchaseTableViewModel model = new PurchaseTableViewModel();
            foreach(var c in displayResult)
            {
                model.ProductName = c.Product.ProductName;
                model.GenericName = c.Product.GenericName;
                model.Qty = c.Qty;
                model.TotalAmount = c.TotalAmount;
                model.Date = c.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                p.Add(model);
                model = new PurchaseTableViewModel();
            }
            var totalRecords = products.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = p
            }, JsonRequestBehavior.AllowGet);
        }





        //public ActionResult GetData(JqueryDatatableParam param)
        //{
        //    var products = ProductsService.Instance.PurchaseSearchByDate(fromDate, toDate);

        //    //Searching
        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        products = products.Where(x => x.ProductName.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.GenericName.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.BatchNo.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.BarCode.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.Location.ToLower().Contains(param.sSearch.ToLower())
        //                                      || x.ExpiryDate.ToString("dd'/'MM'/'yyyy").ToLower().Contains(param.sSearch.ToLower())).ToList();
        //    }

        //    //Sorting
        //    var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
        //    var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
        //    if (sortColumnIndex == 3)
        //    {
        //        products = sortDirection == "asc" ? products.OrderBy(c => c.ProductName).ToList() : products.OrderByDescending(c => c.ProductName).ToList();
        //    }
        //    else if (sortColumnIndex == 4)
        //    {
        //        products = sortDirection == "asc" ? products.OrderBy(c => c.GenericName).ToList() : products.OrderByDescending(c => c.GenericName).ToList();
        //    }
        //    else if (sortColumnIndex == 5)
        //    {
        //        products = sortDirection == "asc" ? products.OrderBy(c => c.PackRetailCost).ToList() : products.OrderByDescending(c => c.PackRetailCost).ToList();
        //    }
        //    else
        //    {
        //        Func<Product, string> orderingFunction = e => sortColumnIndex == 0 ? e.ProductName : sortColumnIndex == 1 ? e.Location : e.Location;

        //        products = sortDirection == "asc" ? products.OrderBy(orderingFunction).ToList() : products.OrderByDescending(orderingFunction).ToList();
        //    }

        //    //Pagination
        //    var displayResult = products.Skip(param.iDisplayStart)
        //       .Take(param.iDisplayLength).ToList();
        //    var totalRecords = products.Count();


        //    //Sending data 
        //    return Json(new
        //    {
        //        param.sEcho,
        //        iTotalRecords = totalRecords,
        //        iTotalDisplayRecords = totalRecords,
        //        aaData = displayResult
        //    }, JsonRequestBehavior.AllowGet);

        //}


    }
}