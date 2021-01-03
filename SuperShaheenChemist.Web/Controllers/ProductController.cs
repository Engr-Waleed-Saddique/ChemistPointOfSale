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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add_new_product()
        {
            DropDownViewModels model = new DropDownViewModels();
            model.categories= CategoriesService.Instance.GetAllCategories();
            model.distributors = DistributorService.Instance.GetAllDistributors();
            model.companies = CompanyService.Instance.GetAllCompanies();
            model.medicinesTypes = MedicineTypeService.Instance.GetAllMedicineTypes();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Product model)
        {
            var newProduct = new Product();
            newProduct.ProductName = model.ProductName;
            //newProduct.CompanyID= CategoriesService.Instance.GetCategory(model.CategoryID);
            newProduct.GenericName = model.GenericName;
            newProduct.BatchNo = model.BatchNo;
            newProduct.BarCode = model.BarCode;
            newProduct.Description = model.Description;
            //newProduct.CategoryID = model.CategoryID;
            //if we are doing large projects then we have to use above line that is commented.For this we have to add also one more attrbute in Product class
            // with name CategoryID and entity framework replace The existing cloumn name with this and make this as Foriegn key.We can use this to reduce the
            // number of database calls.




            //ProductService.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");

        }
    }
}
