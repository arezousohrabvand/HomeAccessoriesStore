using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Stock
    {
       
        public int StockId { get; set; }//pk
    
        public int ProductId { get; set; }//fk
        public string StockStatus { get; set; }
        public int Quantity { get; set; }

        public Products Products { get; set; }




    }
}
