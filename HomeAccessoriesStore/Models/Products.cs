using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Products
    {
        

        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0.01,99999)]
        public double Price { get; set; }
        public string Brand { get; set; }
        public string Photo { get; set; }
    
        public string Stock { get; set; }
        public Category Category { get; set; }
        //with Display(Name ="Category) we can have Category in product page instead of Category Id
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Cart> Cart { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Brand> Brands { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }
        
    }
}