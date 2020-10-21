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
        public int CustomerId { get; set; }//fk
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "OderDate")]
        public DateTime OderDate { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
