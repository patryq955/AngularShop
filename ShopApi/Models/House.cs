using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models
{
    public class House
    {
        [Key]
        public int Id{get;set;}

        public string City {get;set;}

        public decimal Value {get;set;}

        public int UserId {get;set;}

        public virtual User User {get;set;}
    }
}