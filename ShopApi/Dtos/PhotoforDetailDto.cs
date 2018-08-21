using System;

namespace ShopApi.Dtos
{
    public class PhotoforDetailDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdd { get; set; }

        public string PublicId { get; set; }

        public bool IsMain { get; set; }
        
    }
}