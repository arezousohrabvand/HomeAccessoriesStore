using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailsId { get; set; }//pk
        [Required]
        [Display(Name = "Product ")]
        public int ProductId { get; set; }//fk
        [Display(Name = " Order ")]
        public int OrderId { get; set; }//fk
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0.01, 99999)]
        public double Price { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0.01, 99999)]
        public double Discount { get; set; }
        public string Payment { get; set; }
        public Orders Orders { get; set; }
        public Products Products { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }

    }
}
