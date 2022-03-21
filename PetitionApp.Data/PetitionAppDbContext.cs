using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Data
{
    public class PetitionAppDbContext: IdentityDbContext<User, Role, Guid>
    {
        public PetitionAppDbContext(DbContextOptions<PetitionAppDbContext> options)
           : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
