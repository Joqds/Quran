using FluentValidation;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByPageQueryValidator : AbstractValidator<GetAyatByPageQuery>
    {
        public GetAyatByPageQueryValidator()
        {
            RuleFor(x => x.StartPageId)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.Quran_PageMustBeGreaterThanZero);
            When(x => x.FinishPageId.HasValue, () =>
                RuleFor(x => x.FinishPageId)
                    .LessThanOrEqualTo(x => x.StartPageId)
                    .WithMessage(ValidationMessages.Quran_LastPageIfSetMustBeGreaterThanStartPage)
            );
        }
    }
}