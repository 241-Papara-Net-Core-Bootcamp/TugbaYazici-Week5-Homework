using Microsoft.EntityFrameworkCore;
using PaparaThirdWeek.Data.Configurations;
using PaparaThirdWeek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Data.Context
{
    public class PaparaAppDbContext : DbContext
    {
       
        public PaparaAppDbContext(DbContextOptions<PaparaAppDbContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<FakeUser> FakeUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            //reflection her classın ne iş yaptığını bilir.
            //DbContext teki tüm configurationları bulup register eder.
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaparaAppDbContext).Assembly);
        }
        

    } 
}
