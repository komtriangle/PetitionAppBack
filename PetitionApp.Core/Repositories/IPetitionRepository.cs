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
        Petition GetByIdAdync(int Id);

        IEnumerable<Petition> GetVoicesForUser(User user);
        IEnumerable<Petition> GetPetitionsByAuthor(User user); 

        Task<Image> AddImageToPetition(Petition petition, Image image);
        Task<IEnumerable<Tag>> AddTagsToPetition(Petition petition, IEnumerable<Tag> tags);
    }
}
