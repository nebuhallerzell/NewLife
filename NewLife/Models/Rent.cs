using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewLife.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Rent time is required")]
        public DateTime Rent_Time { get; set; }

        [Required(ErrorMessage = "Back time is required")]
        public DateTime Back_Time { get; set; }

        [Required(ErrorMessage = "Car is required")]
        public int CarId { get; set; }
        public Car? Car { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }
        public User? User { get; set; }

        public double Rent_Price { get; set; }

        public void CalculateRentPrice(double carPrice)
        {
            int rentDurationInDays = (int)(Back_Time.Date - Rent_Time.Date).TotalDays;
            Rent_Price = rentDurationInDays * carPrice;
        }
    }
}
