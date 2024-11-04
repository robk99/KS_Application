using FluentValidation;
namespace KS.Application.Offers.Create
{
    public class OfferCreateDTOValidator : AbstractValidator<OfferCreateDTO>
    {
        public OfferCreateDTOValidator()
        {
            RuleFor(x => x.ArticleIds)
                .NotEmpty()
                .Must(IdsAreValid).WithMessage("All ArticleIds must be greater than 0.");
        }

        private bool IdsAreValid(IList<long> articleIds)
        {
            return articleIds.All(id => id > 0);
        }
    }
}
