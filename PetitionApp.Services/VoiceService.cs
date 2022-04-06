using Microsoft.AspNetCore.Identity;
using PetitionApp.Core.Models;
using PetitionApp.Core.Repositories;
using PetitionApp.Core.Services;
using PetitionApp.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Services
{
    public class VoiceService : IVoiceService
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly UserPetitionValidator _userPetitionValidator;

        public VoiceService(IUnitOfWork unitOfWork, UserManager<User> userManager, UserPetitionValidator userPetitionValidator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userPetitionValidator = userPetitionValidator;
        }

        public async Task AddVoice(UserPetitions userPetitions)
        {
            var validateResult = _userPetitionValidator.Validate(userPetitions);
            if (!validateResult.IsValid)
            {
                throw new Exception($"Ошибки валидации: {string.Join(", ", validateResult.Errors)}");
            }

            var petition = _unitOfWork.petition.GetById(userPetitions.PetitionId);

            if (_unitOfWork.voice.IsVoiced(userPetitions))
            {
                return;
            }
          
            try
            {
                await _unitOfWork.voice.CreateAsync(userPetitions);
                await _unitOfWork.CommitAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Ошибка во время добавления голоса");
            }
        }

        public IEnumerable<UserPetitions> GetVoicedPetitions(Guid userId)
        {
            return _unitOfWork.voice.GetVoicedPetitions(userId);
        }

        public async Task<bool> IsVoiced(UserPetitions userPetitions)
        {
            var validateResult = _userPetitionValidator.Validate(userPetitions);
            if (!validateResult.IsValid)
            {
                throw new Exception($"Ошибки валидации: {string.Join(", ", validateResult.Errors)}");
            }
            try
            {
                var petition = _unitOfWork.petition.GetById(userPetitions.PetitionId);
                var IsVoiced = _unitOfWork.voice.IsVoiced(userPetitions);
                return IsVoiced;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка во время проверки наличия голоса");
            }
        }

        public async Task RemoveVoice(UserPetitions userPetitions)
        {
            var validateResult = _userPetitionValidator.Validate(userPetitions);
            if (!validateResult.IsValid)
            {
                throw new Exception($"Ошибки валидации: {string.Join(", ", validateResult.Errors)}");
            }

            var petition = _unitOfWork.petition.GetById(userPetitions.PetitionId);
            if (!_unitOfWork.voice.IsVoiced(userPetitions))
            {
                return;
            }
            try
            {
       
                 _unitOfWork.voice.Delete(userPetitions);
                await _unitOfWork.CommitAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Ошибка по время удаления голоса");
            }
        }
    }
}
