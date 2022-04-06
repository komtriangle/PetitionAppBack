using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PetitionApp.Core.Models;
using PetitionApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Services.Validators
{
    public class UserPetitionValidator: AbstractValidator<UserPetitions>
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserPetitionValidator(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

            RuleFor(up => up.UserId)
                .MustAsync(async (userId, token)
                => (await ValidateUserIdAsync(userId)))
                .WithMessage("Пользователя с таким Id нет");

            RuleFor(up => up.PetitionId)
                .Must(ValidatePetitionId)
                .WithMessage("Петиции с таким Id нет");
        }

        private async Task<bool> ValidateUserIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString()) != null;
        }
        
        private bool ValidatePetitionId(int petitionId)
        {
            return  _unitOfWork.petition.GetById(petitionId) != null;
        }
    }
}
