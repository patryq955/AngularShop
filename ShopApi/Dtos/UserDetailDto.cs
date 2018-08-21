using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopApi.Models;

namespace ShopApi.Dtos
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public bool IsAdmin { get; set; }

        public string City { get; set; }

        public string UrlPhoto { get; set; }

        public int Age { get; set; }

        public ICollection<PhotoforDetailDto> Photos { get; set; }

    }
}