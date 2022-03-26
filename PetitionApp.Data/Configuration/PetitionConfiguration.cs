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
    internal class PetitionConfiguration : IEntityTypeConfiguration<Petition>
    {
        public void Configure(EntityTypeBuilder<Petition> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasIndex(u => u.Title)
                .IsUnique();

            builder
                .Property(x => x.Id)
                .UseIdentityColumn();

            builder
                .HasOne(x => x.Author);

            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(x => x.Text)
                .IsRequired()
                .HasColumnType("text");

            builder
                .Property(x => x.CountVoices)
                .HasDefaultValue(0);

            builder
                .Property(x => x.CreationDate)
                .HasDefaultValue(DateOnly.FromDateTime(DateTime.Now));

        }
    }
}
