using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewLife.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Car_Name { get; set; }
        [Required]
        [Range(1000,100000)]
        public double Car_Km { get; set;}
        [Required]
        [Range(1000,20000)]
        public double Car_Price { get; set;}

        public string? Car_About { get; set; }
        public bool Car_Rent { get; set; }

        [ValidateNever]
        public string? CarImgUrl { get; set; }

    }
}
