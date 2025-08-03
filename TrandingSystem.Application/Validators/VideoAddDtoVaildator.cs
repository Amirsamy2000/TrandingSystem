using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Resources;

namespace TrandingSystem.Application.Validators
{
    public class VideoAddDtoVaildator : AbstractValidator<VideoAddedDto>
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

        private readonly string[] allowedExtensionsVideo = new[] { ".mp4", ".avi", ".mov", ".mkv", ".wmv", ".webm" };
    private readonly string[] allowedContentTypesVideo = new[] {
        "video/mp4",
        "video/x-msvideo",
        "video/quicktime",
        "video/x-matroska",
        "video/x-ms-wmv",
        "video/webm"
    };

        public VideoAddDtoVaildator(IStringLocalizer<ValidationMessages> localizer)
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
                .NotEmpty().WithMessage(_localizer["Required"]);
            RuleFor(x => x.VideoUrl)
               .NotEmpty().WithMessage(_localizer["Required"]);


            RuleFor(x => x.ImageVideoUrl)
    .Must(file => file == null || IsValidImageType(file))
    .WithMessage(localizer["OnlyImageTypesAllowed"])

    .Must(file => file == null || IsValidImageExtension(file))
    .WithMessage(localizer["InvalidImageExtension"]);

            // fir video
            RuleFor(x => x.VideoUrl)
    .Must(file => IsValidTypeVideo(file))
    .WithMessage(localizer["OnlyVideoTypesAllowed"])

    .Must(file => IsValidExtensionVideo(file))
    .WithMessage(localizer["InvalidVideoExtension"]);

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

        private bool IsValidTypeVideo(IFormFile file)
        {
            if (file == null) return true;
            return allowedContentTypesVideo.Contains(file.ContentType.ToLower());
        }
        private bool IsValidExtensionVideo(IFormFile file)
        {
            if (file == null) return false;
            var ext = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensionsVideo.Contains(ext);
        }



    }
}
