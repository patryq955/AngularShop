using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set;}

        public string Url {get;set;}

        public DateTime DateAdd {get;set;}



    }
}