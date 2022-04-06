using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetitionApp.Core.Repositories;

namespace PetitionApp.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPetitionRepository petition { get; }
        ITagRepository tag { get; }
        IVoiceRepository voice { get; }
        Task<int> CommitAsync();
    }
}
