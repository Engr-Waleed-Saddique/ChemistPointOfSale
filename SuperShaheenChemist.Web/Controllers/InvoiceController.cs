using SuperShaheenChemist.Services;
using SuperShaheenChemist.Web.ViewModels;
using System;
using System.Collections.Generic;
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
            return View();
        }
        public ActionResult ProductsWithBatchNo(string Pname)
        {
            var products = ProductsService.Instance.GetProducts();

            var ob = products.Where(x => x.ProductName.Trim().ToLower() == Pname.Trim().ToLower()).Select(x => new { x.BatchNo, x.Id }).ToList();

            return Json(ob,JsonRequestBehavior.AllowGet);
        }
    }
}
