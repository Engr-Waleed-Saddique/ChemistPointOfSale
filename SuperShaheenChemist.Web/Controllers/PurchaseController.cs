using SuperShaheenChemist.Entities;
using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperShaheenChemist.Web.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            return View();
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
