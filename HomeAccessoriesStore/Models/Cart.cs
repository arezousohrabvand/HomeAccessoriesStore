using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }//pk
        [Required]
        public int ProductId { get; set; }//fk
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public Decimal TotalPrice { get; set; }
        public DateTime  Date{ get; set; }
    public Products Products { get; set; }


    }
}
