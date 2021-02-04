using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class SaleService
    {
        #region Singleton
        public static SaleService Instance
        {
            get
            {
                if (instance == null) instance = new SaleService();
                return instance;
            }
        }
        private static SaleService instance { get; set; }

        private SaleService()
        {

        }
        #endregion

        public int OrderItemMaxID()
        {
            using (var context = new CBContext())
            {
                return context.OrderItems.Max(x => (int?)x.OrderID) ?? 0;
            }
        }
        public int OrderMaxID()
        {
            using (var context = new CBContext())
            {
                return context.Orders.Max(x => (int?)x.Id) ?? 0;
            }
        }
        public void SaveOrder(Order item)
        {
            using (var context = new CBContext())
            {
                context.Orders.Add(item);
                context.SaveChanges();
            }
        }
        public void SaveSale(OrderItem items)
        {
            using (var context=new CBContext())
            {
                context.OrderItems.Add(items);
                context.SaveChanges();
            }
        }
        
    }
}
