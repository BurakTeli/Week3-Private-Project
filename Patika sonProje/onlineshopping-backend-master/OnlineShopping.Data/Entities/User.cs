﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Data.Entities
{
    public enum UserRole { Customer, Admin }

    public class User
    {
        [Key]
        public int Id { get; set; }
      //  [Required] 
        public string FirstName { get; set; }
       // [Required] 
        public string LastName { get; set; }

      //  [Required, EmailAddress]
        public string Email { get; set; }

       // [Required] 
        public string PhoneNumber { get; set; }

       // [Required] 
        public string Password { get; set; }

       // [Required] 
        public UserRole Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
