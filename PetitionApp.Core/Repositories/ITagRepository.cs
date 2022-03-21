using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Repositories
{
    public interface ITagRepository: IRepository<Tag> 
    {
        IEnumerable<Tag> GetTagsByPetitionId(int id);


    }
}
