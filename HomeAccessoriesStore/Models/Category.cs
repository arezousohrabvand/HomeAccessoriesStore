using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; }
       
        [Required]
        public string Name { get; set; }
        public List<Products>Products { get; set; }
     
    }
}
