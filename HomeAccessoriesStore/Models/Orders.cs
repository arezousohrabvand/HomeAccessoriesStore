using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Orders
    {
        public int OrdersId { get; set; }//pk
        [Display(Name = "Customer")]
        public string CustomerId { get; set; }//fk

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "OderDate")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name ="Postal Code")]
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        [Display(Name = "Email Address ")]
        public string EmailAddress { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
