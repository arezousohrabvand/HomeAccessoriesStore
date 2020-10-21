using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class ProductCategories
    {
       [Key]
        public int ProductCategoriesId { get; set; }//pk
        public int ProductId { get; set; }//fk
        public string ProductName { get; set; }
        public Products Products { get; set; }
       
    }
}
