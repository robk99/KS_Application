using FluentValidation;

namespace KS.Application.Offers.Update
{
    public class OfferUpdateDTOValidator : AbstractValidator<OfferUpdateDTO>
    {
        public OfferUpdateDTOValidator()
        {
            RuleFor(x => x.ArticleIds)
                .NotEmpty()
                .Must(articleIds => articleIds.All(id => id > 0)).WithMessage("All ArticleIds must be greater than zero.");

            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
