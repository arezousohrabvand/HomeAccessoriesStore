using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }//pk
        //validating email
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        public string Password { get; set; }
        [StringLength(50)]
        [Display(Name ="First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
     
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Display(Name ="Full Name")]
        public string fullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Payment { get; set; }
        public List<Address> Addresses { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public List<User> Users { get; set; }
    }
}
