using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolDB
{
    public class DrivingSchoolDbContext : IdentityDbContext<ApplicationUser>
    {
        public DrivingSchoolDbContext(DbContextOptions<DrivingSchoolDbContext> options) : base(options)
        {

        }

        public DbSet<Serie> Series { get; set; }
        public DbSet<Question> Questions { get; set; }  

    }
}
