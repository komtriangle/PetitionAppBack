using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Services
{
    public interface IVoiceService
    {
        Task AddVoice(UserPetitions userPetitions);
        Task RemoveVoice(UserPetitions userPetitions);

        IEnumerable<UserPetitions> GetVoicedPetitions(Guid userId);
        Task<bool> IsVoiced(UserPetitions userPetitions);
    }
}
