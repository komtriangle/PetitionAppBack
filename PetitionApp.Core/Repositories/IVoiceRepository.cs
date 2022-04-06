using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Repositories
{
    public interface IVoiceRepository : IRepository<UserPetitions>
    {
        /// <summary>
        /// Возвращает список петиций, за которые проголосовал
        /// пользователь
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        IEnumerable<UserPetitions> GetVoicedPetitions(Guid userId);

        /// <summary>
        /// Возвращает значение - проголосовал ли
        /// пользователь за петицию
        /// </summary>
        /// <param name="petition">Петиция</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsVoiced(UserPetitions userPetitions);
    }
}
