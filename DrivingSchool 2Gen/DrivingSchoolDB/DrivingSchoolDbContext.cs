using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolDB
{
    public class DrivingSchoolDbContext : IdentityDbContext<ApplicationUser>
    {
        public DrivingSchoolDbContext(DbContextOptions<DrivingSchoolDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Serie> Series { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
       // public virtual DbSet<ApplicationUser> Users { get; set; }

    }
}
