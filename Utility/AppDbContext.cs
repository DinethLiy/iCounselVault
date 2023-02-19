using icounselvault.Models.Auth;
using icounselvault.Models.Profiles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace icounselvault.Utility
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        //Auth
        public DbSet<User> USER { get; set; }
        //Profiles
        public DbSet<Counselor> COUNSELOR { get; set; }
        public DbSet<CounselorExperience> COUNSELOR_EXPERIENCE { get; set; }
        public DbSet<Client> CLIENT { get; set; }
        public DbSet<ClientExperience> CLIENT_EXPERIENCE { get; set; }
    }
}
