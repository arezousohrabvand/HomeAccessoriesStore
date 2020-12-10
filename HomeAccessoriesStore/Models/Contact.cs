using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{ 
    [Table("contacts")]
    public class Contact
    {
        public int Id { get; set;}
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Message { get; set; }
        [StringLength(100)]
        public string PhoneNumber { get; set; }
    }
}
