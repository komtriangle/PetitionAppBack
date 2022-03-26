using FluentValidation;
using PetitionApp.API.DTO;

namespace PetitionApp.API.Validators
{
    public class CreatePetitionValidator: AbstractValidator<CreatePetitionDTO>
    {

        public CreatePetitionValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Текст петиции не может быть пустым");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Заголовок петиции не может быть пустым");

            RuleFor(x => x.Goal)
                .GreaterThan(0)
                .WithMessage("Цель петициии не может быть отрицательным числом или нулем");
        }
    }
}
