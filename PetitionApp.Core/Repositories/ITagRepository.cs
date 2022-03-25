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

        /// <summary>
        /// Проверяет существоавание тега в БД,
        /// в случае отсутсвтия создает его.
        /// </summary>
        /// <param name="tags">Список тегов статьи без идентификаторов</param>
        /// <returns>Список тегов с идентификаторами</returns>
        Task<IEnumerable<Tag>> FindOrCreateTags(IEnumerable<Tag> tags);

        Task CreatePetitionTags(int petitionId, IEnumerable<Tag> tags);


    }
}
