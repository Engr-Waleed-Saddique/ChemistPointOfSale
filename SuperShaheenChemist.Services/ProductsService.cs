using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class ProductsService
    {
        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();
                return instance;
            }
        }
        private static ProductsService instance { get; set; }

        private ProductsService()
        {

        }
        #endregion


        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public List<Product> GetProducts()
        {
            
            using(var context=new CBContext())
            {
                return context.Products.ToList();
            }
        }
        public Product GetProductById(int id)
        {

            using (var context = new CBContext())
            {
                return context.Products.Where(product => product.Id == id).FirstOrDefault();
            }
        }
        public List<Product> GetProductsNameBarcode(string productName)
        {
            using (var context = new CBContext())
            {

                return context.Products.ToList();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        public decimal ProductPrice(int ID)
        {
            using (var context = new CBContext())
            {
                Product product=context.Products.Find(ID);
                return product.PackCost;
            }
        }

        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(product => product.Id == ID).FirstOrDefault();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void PurchaseProduct(PurchaseProducts product)
        {
            using (var context = new CBContext())
            {
                //if (context.PurchaseProducts.Any(x => x.ProductId == product.ProductId))
                //{
                //    var purchaseRecord= context.PurchaseProducts.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                //    purchaseRecord.Qty = purchaseRecord.Qty+product.Qty;
                //    context.PurchaseProducts.Attach(purchaseRecord);
                //    context.Entry(purchaseRecord).Property(x => x.Qty).IsModified = true;
                //    context.SaveChanges();
                //}
                context.PurchaseProducts.Add(product);
                context.SaveChanges();
                
            }
        }
    }
}
