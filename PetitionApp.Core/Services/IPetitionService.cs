using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Services
{
    public  interface IPetitionService
    {
        Task<Petition> CreateAsync(Petition petition, IEnumerable<Tag> tags);
        Task DeletePetition(int petitionId);
        Task<Petition> UpdatePetition(Petition petition);
        Task AddVoice(Petition petition, User user);

        IEnumerable<Petition> GetPetitionByAuthor(User author);

        IEnumerable<Petition> FindPetitions(Expression<Func<Petition, bool>> predicate);

        IEnumerable<Petition> GetTopPetitions(int count);
    }
}
