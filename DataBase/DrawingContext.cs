using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Final_Project.DataBase;

namespace WPF_Final_Project.DataBase
{
    class DrawingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=AHMEDELGAIAR;Database=Drawing Construction;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}
