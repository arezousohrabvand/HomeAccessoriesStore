﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Address
    {
       
        public int AddressId { get; set; }//pk
        //It shows that it is required
        [Required]
        //with Name = "Customer") we can have Customer in product page instead of Customer Id
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
