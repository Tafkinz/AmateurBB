using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameTeam> GameTeams { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPerson> TeamOwners { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
