using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Models
{
    public class House
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public decimal Value { get; set; }

        public int UserId { get; set; }

        public int numberRooms { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}