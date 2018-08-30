using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]

        public string Password { get; set; }

        [Required]
        [CompareAttribute("Password", ErrorMessage = "Emails mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}