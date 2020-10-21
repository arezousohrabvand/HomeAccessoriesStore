using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Brand
    {
        [Display(Name = "Brand")]

        public int BrandId { get; set; }//pk
        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }//fk
        public string BrandName { get; set; }
        public Products Products { get; set; }


    }
}
