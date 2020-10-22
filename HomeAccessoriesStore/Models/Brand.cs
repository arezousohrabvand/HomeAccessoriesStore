using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Brand
    {
        //with Display(Name = "Brand" we can have Brand in product page instead of BrandId
        [Display(Name = "Brand")]
        public int BrandId { get; set; }//pk
        [Required]
        //with Display(Name = "Product" we can have Product in product page instead of ProductId
        [Display(Name = "Product")]
        public int ProductId { get; set; }//fk
        public string BrandName { get; set; }
        public Products Products { get; set; }


    }
}
