using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Calischool.Models;

namespace Calischool.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> StudentRegisters { get; set; }
        public DbSet<Materials> StudentMaterials { get; set; }
        public DbSet<Mentorship> CourseMentorship { get; set; }
    }
}
