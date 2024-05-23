using System.ComponentModel.DataAnnotations;

namespace NewLife.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string User_Surname { get; set; }
        [Required]
        public string User_Email { get; set;}
        [Required]
        public string User_Password { get; set; }
        [Required]
        public string User_Phone { get; set;}

        public string? User_Type { get; set; } = "Standart";
        public string? User_Adress { get; set; }

    }
}
