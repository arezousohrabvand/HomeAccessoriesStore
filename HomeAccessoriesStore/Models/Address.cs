using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Address
    {
       
        public int AddressId { get; set; }//pk
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }//fk
        

        public string StreetNumber { get; set; }
        public string StreetName { get; set; }

        public string City { get; set; }
        public string PostalCode { get; set; }

        public string Province { get; set; }
        public Customers Customers { get; set; }





    }
}
