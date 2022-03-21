using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Data.Configuration
{
    public class UserPetitionConfiguration : IEntityTypeConfiguration<UserPetitions>
    {
        public void Configure(EntityTypeBuilder<UserPetitions> builder)
        {

            builder
                .HasKey(x => new { x.UserId, x.PetitionId});
            builder
                 .HasOne(x => x.User)
                 .WithMany(x => x.UserPetitions)
                 .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Petition)
                .WithMany(x => x.UserPetitions)
                .HasForeignKey(x => x.PetitionId);
        }
    }
}
