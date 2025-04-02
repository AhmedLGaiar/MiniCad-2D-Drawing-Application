using System.ComponentModel.DataAnnotations;

namespace WPF_Final_Project.DataBase
{
    public enum Gender
    {
        Male,
        Female,
    }
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string UserName { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        [EmailAddress]
        public string? email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public Gender gender { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }
    }
}
