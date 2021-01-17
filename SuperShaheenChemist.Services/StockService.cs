using SuperShaheenChemist.Database;
using SuperShaheenChemist.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Services
{
    public class StockService
    {
        #region Singleton
        public static StockService Instance
        {
            get
            {
                if (instance == null) instance = new StockService();
                return instance;
            }
        }
        private static StockService instance { get; set; }

        private StockService()
        {

        }
        #endregion
        public void AddStock(StockInventry stock)
        {
            using (var context = new CBContext())
            {
                if (context.StockInventries.Any(x => x.ProductId == stock.ProductId))
                {
                    var data = context.StockInventries.Where(x => x.ProductId == stock.ProductId).FirstOrDefault();
                    data.Stock = data.Stock + stock.Stock;
                    data.Received = data.Received + stock.Received;
                    data.TotalAmount = data.TotalAmount + stock.TotalAmount;
                    context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.StockInventries.Add(stock);
                    context.SaveChanges();
                }
                
            }
        }
        //public int stockValue(int pId)
        //{
        //    using (var context=new CBContext())
        //    {
        //        context.StockInventries.
        //    }
        //}
    }
}
