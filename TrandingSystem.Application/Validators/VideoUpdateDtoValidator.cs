using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Resources;

namespace TrandingSystem.Application.Validators
{
    public class VideoUpdateDtoValidator : AbstractValidator<ViedoUpdateDto>
    {
        private readonly IStringLocalizer<ValidationMessages> _localizer;

        private readonly string[] _allowedContentTypes = new[]
    {
        "image/jpeg", "image/png", "image/gif", "image/webp"
    };
        private readonly string[] _allowedExtensions = new[]
  {
        ".jpg", ".jpeg", ".png", ".gif", ".webp"
    };
        public VideoUpdateDtoValidator(IStringLocalizer<ValidationMessages> localizer)
        {
            _localizer = localizer;
            RuleFor(x => x.TitleAR)
               .NotEmpty().WithMessage(_localizer["Required"]);

            RuleFor(x => x.TitleEN)
                .NotEmpty().WithMessage(_localizer["Required"]);

            RuleFor(x => x.DescriptionAR)
                .NotEmpty().WithMessage(_localizer["Required"]);

            RuleFor(x => x.DescriptionEN)
                .NotEmpty().WithMessage(_localizer["Required"]);



            RuleFor(x => x.ImageVideoUrl)
    .Must(file => file == null || IsValidImageType(file))
    .WithMessage(localizer["OnlyImageTypesAllowed"])

    .Must(file => file == null || IsValidImageExtension(file))
    .WithMessage(localizer["InvalidImageExtension"]);

            RuleFor(x => x.Cost)
                // التكلفة يجب أن تكون غير سالبة دائمًا
                .GreaterThanOrEqualTo(0).Empty()
                .WithMessage(_localizer["CostMustBeGreaterThanOrEqualToZero"])

                // شرط خاص: إذا مش مدفوع، يجب أن تكون التكلفة 0
                .Must((model, cost) => model.IsPaid || cost == 0)
                .WithMessage(_localizer["CostMustBeZeroIfNotPaid"]);

        }



        private bool IsValidImageType(IFormFile file)
        {
            if (file == null) return true;
            return _allowedContentTypes.Contains(file.ContentType.ToLower());
        }
        private bool IsValidImageExtension(IFormFile file)
        {
            if (file == null) return false;
            var ext = Path.GetExtension(file.FileName).ToLower();
            return _allowedExtensions.Contains(ext);
        }

    }
}
