using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdd { get; set; }

        public string PublicId { get; set; }

        public bool IsMain { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}