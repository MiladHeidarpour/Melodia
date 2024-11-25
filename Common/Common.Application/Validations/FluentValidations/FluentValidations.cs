using Common.Application.FileUtil;
using Common.Application.SecurityUtil;
using Common.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Common.Application.Validations.FluentValidations
{
    public static class FluentValidations
    {
        public static IRuleBuilderOptionsConditions<T, TProperty> JustImageFile<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "شما فقط قادر به وارد کردن عکس میباشید") where TProperty : IFormFile?
        {
            return ruleBuilder.Custom((file, context) =>
            {
                if (file == null)
                    return;

                if (!file.IsImage())
                {
                    context.AddFailure(errorMessage);
                }
            });
        }

        public static IRuleBuilderOptionsConditions<T, TProperty> JustMp3File<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "شما فقط قادر به وارد کردن Mp3 میباشید") where TProperty : IFormFile?
        {
            return ruleBuilder.Custom((file, context) =>
            {
                if (file == null)
                    return;

                if (!file.IsMp3())
                {
                    context.AddFailure(errorMessage);
                }
            });
        }

        public static IRuleBuilderOptionsConditions<T, string> ValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = ValidationMessages.InvalidPhoneNumber)
        {
            return ruleBuilder.Custom((phoneNumber, context) =>
            {
                if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length is < 11 or > 11)
                    context.AddFailure(errorMessage);

            });
        }

        public static IRuleBuilderOptionsConditions<T, TProperty> JustValidFile<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "فایل نامعتبر است") where TProperty : IFormFile
        {
            return ruleBuilder.Custom((file, context) =>
            {
                if (file == null)
                    return;

                if (!file.IsValidFile())
                {
                    context.AddFailure(errorMessage);
                }
            });
        }
    }
}