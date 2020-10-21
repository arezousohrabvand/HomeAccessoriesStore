using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccessoriesStore.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Customers Customers { get; set; }

    }
}
