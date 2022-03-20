using FluentValidation;

namespace MegaFortnite.Business.GetProfile
{
    public class GetProfileCommandValidator : AbstractValidator<GetProfileCommand>
    {
        public GetProfileCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage($"Invalid CustomerId");
        }
    }
}
