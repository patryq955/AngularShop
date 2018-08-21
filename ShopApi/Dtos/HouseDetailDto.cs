using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Dtos
{
    public class HouseDetailDto
    {
        public int Id { get; set; }

        public string City { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        public string UserName { get; set; }

        public int numberRooms { get; set; }
    }
}