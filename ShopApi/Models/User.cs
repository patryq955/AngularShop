using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ShopApi.Models
{
    public class User : IdentityUser<int>
    {
        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public bool IsAdmin { get; set; }

        public string City { get; set; }

        public string Url { get; set; }

        public ICollection<House> Houses { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}