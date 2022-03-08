using FluentValidation;

namespace MegaFortnite.Business.GetProfile
{
    public class GetProfileCommandValidator : AbstractValidator<GetProfileCommand>
    {
        public GetProfileCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage($"Invalid user {nameof(GetProfileCommand.Id)}");
        }
    }
}
