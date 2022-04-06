using Microsoft.EntityFrameworkCore;
using PetitionApp.Core.Models;
using PetitionApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Data.Repositories
{
    public class VoiceRepository : Repository<UserPetitions>, IVoiceRepository
    {

        public VoiceRepository(DbContext context) : base(context) { }

        public PetitionAppDbContext PetitionContext
        {
            get
            {
                return Context as PetitionAppDbContext;
            }
        }

        public IEnumerable<UserPetitions> GetVoicedPetitions(Guid userId)
        {
            return PetitionContext.UserPetitions
                .Where(up => up.UserId == userId)
                .Include(up => up.Petition);
        }

        public bool IsVoiced(UserPetitions userPetitions)
        {
            var voice = PetitionContext.UserPetitions
                .FirstOrDefault(up => up.UserId == userPetitions.UserId && up.PetitionId == userPetitions.PetitionId);
            if(voice != null)
            {
                Context.Entry<UserPetitions>(voice).State = EntityState.Detached;
            }
            return  voice!= null; 
        }
    }
}
