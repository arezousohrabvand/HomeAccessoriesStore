using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class PaymentType
    {
       
        public int PaymentTypeId { get; set; }//pk
        [Required]
        public int CutomerId { get; set; }//fk
        public int OrderDetailId { get; set; }//fk
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "ExpiryDate")]
        public string ExpiryDate { get; set; }
        public string CvvCode { get; set; }

        public Customers Customers { get; set; }
        public OrderDetail OrderDetail { get; set; }




    }
}
