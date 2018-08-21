using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ShopApi.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }

        [Required(ErrorMessage = "Brak zdjęcia")]
        public IFormFile File { get; set; }

        public string Description { get; set; }

        public DateTime DateAdd { get; set; }

        public string PublicId { get; set; }

        public PhotoForCreationDto()
        {
            DateAdd = DateTime.Now;
        }
    }
}