using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;database=BeniTaniyorMusunDB;integrated security=true;");

            optionsBuilder.UseSqlServer("Server=104.247.162.242\\MSSQLSERVER2019;Database=sinesozl_benitaniyormusun;User Id=sinesozl_admin;Password=843261Sb*!;TrustServerCertificate=true");


            optionsBuilder.EnableSensitiveDataLogging(); //hata için eklendi
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                        .HasMany(m => m.Tests)
                        .WithMany(a => a.Questions)
                        .UsingEntity<TestQuestion>(
                j => j.HasOne(ma => ma.Test).WithMany().HasForeignKey(ma => ma.TestId),
                j => j.HasOne(ma => ma.Question).WithMany().HasForeignKey(ma => ma.QuestionId),
                j =>
                {
                    j.HasKey(ma => new { ma.TestId, ma.QuestionId });
                    j.ToTable("TestQuestion");
                }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Guest> Guests { get; set; }    
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<ScoreList> ScoreLists { get; set; }

    }
}
