using FluentValidation;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByRubQueryValidator : AbstractValidator<GetAyatByRubQuery>
    {
        public GetAyatByRubQueryValidator()
        {
            RuleFor(x => x.RubId)
                .InclusiveBetween(1, 240)
                .WithMessage(ValidationMessages.Quran_RubMustBetween);
        }
    }
}