using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetitionApp.Data.Configuration;

namespace PetitionApp.Data
{
    public class PetitionAppDbContext: IdentityDbContext<User, Role, Guid>
    {

        public DbSet<Petition> Petitions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PetitionTags> PetitionTags { get; set; }
        public DbSet<UserPetitions> UserPetitions { get; set; }
        public DbSet<Image> Images { get; set; }
        public PetitionAppDbContext(DbContextOptions<PetitionAppDbContext> options)
           : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new ImageConfiguration());

            builder
                .ApplyConfiguration(new PetitionConfiguration());

            builder
                .ApplyConfiguration(new PetitionTagsConfiguration());

            builder
                .ApplyConfiguration(new TagConfiguration());

            builder
                .ApplyConfiguration(new UserPetitionConfiguration());

        }
    }
}
