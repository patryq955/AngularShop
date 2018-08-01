using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage="Nazwa użytkownika jest wymagana")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Hasło jest wymagane")]
        [StringLength(40,MinimumLength=4,ErrorMessage="Hasło jest za krótkie")  ]
        
        public string Password { get; set; }
    }
}