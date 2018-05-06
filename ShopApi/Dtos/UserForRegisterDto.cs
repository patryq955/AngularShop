using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(40,MinimumLength=4,ErrorMessage="Password is too short")  ]
        public string Password { get; set; }
    }
}