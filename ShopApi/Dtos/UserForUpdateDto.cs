using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos
{
    public class UserForUpdateDto
    {
        [Required(ErrorMessage = "Miasto jest wymagane")]
        public string City { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        public string KnownAs { get; set; }
    }
}