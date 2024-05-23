using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewLife.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        public DateTime Rent_Time { get; set; }
        public DateTime Back_Time { get; set; }


        [ForeignKey("Car")]
        [ValidateNever]
        public int C_Id { get; set; }
        public Car Car { get; set; }


        [ForeignKey("User")]
        [ValidateNever]
        public int U_Id { get; set; }
        public User User { get; set; }

    }
}
