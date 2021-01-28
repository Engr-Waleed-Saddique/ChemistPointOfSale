﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShaheenChemist.Entities
{
    public class StockInventry
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Received { get; set; }
        public int Sale { get; set; }
        public int Stock { get; set; }
        public int Return { get; set; }
        public decimal Price { get; set; }
        public int TotalAmount { get; set; }
    }
}
