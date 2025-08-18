using FluentValidation;

namespace BusinessLogicalLayer.Validators.Manga
{
    internal class MangaValidator : AbstractValidator<Entities.MangaS.Manga>
    {
        public void ValidateImages()
        {
            //RuleFor(c => c.CoverImageLink).NotNull().WithMessage("CoverImageLink deve ser informado.");
            //RuleFor(c => c.PosterImageLink).NotNull().WithMessage("PosterImageLink deve ser informado.");
        }
        //Nome, desc...
    }
}
