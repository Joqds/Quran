using FluentValidation;

namespace Quran.Server.Application.SampleEntities.Commands.UpdateSampleEntity
{
    public class UpdateSampleEntityCommandValidator : AbstractValidator<UpdateSampleEntityCommand>
    {
        public UpdateSampleEntityCommandValidator()
        {
            RuleFor(v => v.SampleProperty)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
