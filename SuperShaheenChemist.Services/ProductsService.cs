using System;
using System.Collections.Generic;
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


    }
}
