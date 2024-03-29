﻿using icounselvault.Models.Auth;
using icounselvault.Models.Counseling;
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
        // Auth
        public DbSet<User> USER { get; set; }
        // Profiles
        public DbSet<Counselor> COUNSELOR { get; set; }
        public DbSet<CounselorExperience> COUNSELOR_EXPERIENCE { get; set; }
        public DbSet<Client> CLIENT { get; set; }
        public DbSet<ClientExperience> CLIENT_EXPERIENCE { get; set; }
        // Counseling
        public DbSet<ClientGuidanceHistory> CLIENT_GUIDANCE_HISTORY { get; set; }
        public DbSet<CounselDataInsertRequest> COUNSEL_DATA_INSERT_REQUEST { get; set; }
        public DbSet<CounselRequest> COUNSEL_REQUEST { get; set; }
    }
}
