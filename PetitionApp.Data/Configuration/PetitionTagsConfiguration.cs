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
    internal class PetitionTagsConfiguration : IEntityTypeConfiguration<PetitionTags>
    {
        public void Configure(EntityTypeBuilder<PetitionTags> builder)
        {

            builder
                .HasKey(x =>new  { x.PetitionId, x.TagId });

            builder
                .HasOne(x => x.Tag)
                .WithMany(x => x.PetitionTags)
                .HasForeignKey(x => x.TagId);

            builder
                .HasOne(x => x.Petition)
                .WithMany(x => x.PetitionTags)
                .HasForeignKey(x => x.PetitionId);



        }
    }
}
