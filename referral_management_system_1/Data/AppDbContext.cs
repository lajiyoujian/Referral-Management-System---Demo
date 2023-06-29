using Microsoft.EntityFrameworkCore;
using referral_management_system_1.Models;

    namespace referral_management_system_1.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            //// Configure any additional model mappings or relationships without primary keys.
            //modelBuilder.Entity<Patient>().ToTable("Patient");
            //modelBuilder.Entity<Referrals>().ToTable("Referrals");
            //modelBuilder.Entity<Specialties>().ToTable("Specialties");
            //modelBuilder.Entity<Provider>().ToTable("Provider");

            //*** code for primary key added ***
            modelBuilder.Entity<Patient>().HasKey(p => p.PatientId);
            modelBuilder.Entity<Referrals>().HasKey(r => r.ReferralId);
            modelBuilder.Entity<Specialties>().HasKey(s => s.SpecialityId);
            modelBuilder.Entity<Provider>().HasKey(p => p.ProviderId);

            base.OnModelCreating(modelBuilder);
        }

            public DbSet<Patient> Patients { get; set; }
            public DbSet<Referrals> Referrals { get; set; }
            public DbSet<Specialties> Specialties { get; set; }
            public DbSet<Provider> Providers { get; set; }
           // write db set to representation of the table in db to manipulte data
          

    }

    }

