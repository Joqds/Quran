using System.Linq;
using FluentValidation;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Application.Quran.Queries.GetSurahChunk
{
    public class GetAyatBySurahQueryValidator : AbstractValidator<GetAyatBySurahQuery>
    {
        private readonly IApplicationDbContext _context;
        private Surah Surah { get; set; }

        public GetAyatBySurahQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.SurahId)
                .Must(Predicate)
                .WithMessage(ValidationMessages.Quran_SurahNotFount).ChildRules(rules =>
                {
                    When(x => x.StartPage.HasValue,
                        () =>
                        {
                            RuleFor(x => x.StartPage)
                                .LessThan(x => Surah.Page)
                                .WithMessage(ValidationMessages.Quran_SurahStartPageGreaterThanStartPage);
                            RuleFor(x => x.StartPage)
                                .GreaterThan(x => Surah.Ayat.Max(y => y.PageId))
                                .WithMessage(ValidationMessages.Quran_SurahFinishPageLessThanStartPage);
                            RuleFor(x => x.EndPage)
                                .LessThan(x => Surah.Page)
                                .WithMessage(ValidationMessages.Quran_SurahStartPageGreaterThanEndPage);
                            RuleFor(x => x.EndPage)
                                .GreaterThan(x => Surah.Ayat.Max(y => y.PageId))
                                .WithMessage(ValidationMessages.Quran_SurahFinishPageLessThanEndPage);

                        });
                });
        }

        private bool Predicate(int surahId)
        {
            Surah = _context.Sovar.Find(surahId);
            return Surah != null;
        }
    }
}