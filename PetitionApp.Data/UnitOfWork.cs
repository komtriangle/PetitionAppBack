using PetitionApp.Core.Repositories;
using PetitionApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private PetitionAppDbContext _context;
        private ITagRepository _tagRepository;
        private IPetitionRepository _petitionRepository;
        private IVoiceRepository _voiceRepository;

        public UnitOfWork(PetitionAppDbContext context)
        {
            _context = context;
        }

        public IPetitionRepository petition => _petitionRepository = _petitionRepository ?? new PetitionRepository(_context);
        public ITagRepository tag  => _tagRepository = _tagRepository ?? new TagRepository(_context);
        public IVoiceRepository voice => _voiceRepository = _voiceRepository ?? new VoiceRepository(_context);


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            
        }
    }
}
