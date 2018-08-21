using System;

namespace ShopApi.Dtos
{
    public class UserForListDto
    {
 public int Id { get; set; }

        public string UserName { get; set; }
        public string Gender { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string UrlPhoto { get; set; }
    }
}