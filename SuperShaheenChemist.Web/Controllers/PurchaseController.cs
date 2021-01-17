using Newtonsoft.Json;
using SuperShaheenChemist.Entities;
using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections.Generic;
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
                temp.ProductId= pItems.ProductID;
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
    }
}

