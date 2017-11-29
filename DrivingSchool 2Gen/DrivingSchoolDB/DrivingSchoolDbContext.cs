using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolDB
{
    public class DrivingSchoolDbContext : IdentityDbContext<ApplicationUser>
    {
        public DrivingSchoolDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
