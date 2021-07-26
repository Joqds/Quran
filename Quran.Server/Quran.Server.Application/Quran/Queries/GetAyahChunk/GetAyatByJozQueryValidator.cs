using FluentValidation;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByJozQueryValidator : AbstractValidator<GetAyatByJozQuery>
    {
        public GetAyatByJozQueryValidator()
        {
            RuleFor(x => x.JozId)
                .InclusiveBetween(1, 30)
                .WithMessage(ValidationMessages.Quran_JozMustBetween);
        }
    }
}