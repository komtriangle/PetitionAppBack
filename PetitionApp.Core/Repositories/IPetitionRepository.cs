using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Repositories
{
    public interface IPetitionRepository: IRepository<Petition> 
    {
        Petition GetById(int Id);

        IEnumerable<Petition> GetTopPetitions(int count);

        IEnumerable<Petition> GetVoicesForUser(User user);
        IEnumerable<Petition> GetPetitionsByAuthor(User user); 

        Task<Image> AddImageToPetitionAsync(Petition petition, Image image);
        Task<IEnumerable<Tag>> AddTagsToPetitionAsync(Petition petition, IEnumerable<Tag> tags);

        Task AddVoiceAsync(Petition petition, User user);

        IEnumerable<Petition> GetPetitionsByTags(IEnumerable<Tag> tags);
    }
}
