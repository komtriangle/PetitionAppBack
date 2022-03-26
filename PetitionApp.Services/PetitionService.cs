using PetitionApp.Core.Models;
using PetitionApp.Core.Repositories;
using PetitionApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Services
{
    public class PetitionService : IPetitionService
    {
        private IUnitOfWork _unitOfWork;

        public PetitionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddVoice(Petition petition, User user)
        {
            await _unitOfWork.petition.AddVoiceAsync(petition, user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Petition> CreateAsync(Petition petition, IEnumerable<Tag> tags)
        {
            var IsDublicate = _unitOfWork.petition.Find(p => p.Title == petition.Title).FirstOrDefault() != null;
            if (IsDublicate)
            {
                throw new Exception("Петиция с таким заголовком уже есть");
            }
            try
            {
                var tagsInDb = await _unitOfWork.tag.FindOrCreateTags(tags);
                await _unitOfWork.petition.CreateAsync(petition);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.tag.CreatePetitionTags(petition.Id, tags);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("При создании петиции произошла ошибка");
            }
            return petition;
        }

        public async Task DeletePetition(int petitionId)
        {
            var petition = _unitOfWork.petition.GetById(petitionId);
            if(petition == null)
            {
                throw new ArgumentException("Петиции с таким Id нет");
            }
            _unitOfWork.petition.Delete(petition);
            await _unitOfWork.CommitAsync();

        }

        public  IEnumerable<Petition> FindPetitions(Expression<Func<Petition, bool>> predicate)
        {
            return  _unitOfWork.petition.Find(predicate);
        }

        public IEnumerable<Petition> GetPetitionByAuthor(User author)
        {
            return _unitOfWork.petition.GetPetitionsByAuthor(author);
        }

        public Task<Petition> UpdatePetition(Petition petition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Petition> GetTopPetitions(int count)
        {
             return _unitOfWork.petition.GetTopPetitions(count);
        }
    }
}
