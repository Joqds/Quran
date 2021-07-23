using FluentValidation;

namespace Quran.Server.Application.SampleEntities.Queries.GetSampleEntitiesWithPagination
{
    public class GetSampleEntitiesWithPaginationQueryValidator : AbstractValidator<GetSampleEntitiesWithPaginationQuery>
    {
        public GetSampleEntitiesWithPaginationQueryValidator()
        {
            RuleFor(x => x.SamplePropertySearch)
                .NotEmpty().MinimumLength(3).WithMessage("Search term must be at least three character");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
