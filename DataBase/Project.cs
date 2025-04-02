using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF_Final_Project.DataBase
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ProjectData { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}

