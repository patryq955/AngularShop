using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MinLength(6, ErrorMessage = "Login jest za krótki")]
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public bool IsAdmin { get; set; }

        public string City { get; set; }

        public string Url { get; set; }
    }
}