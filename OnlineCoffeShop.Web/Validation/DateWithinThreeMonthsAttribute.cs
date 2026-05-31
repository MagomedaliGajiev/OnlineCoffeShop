using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineCoffeShop.Web.Validation;

/// <summary>
/// Дата должна быть в диапазоне [сегодня; сегодня + 3 месяца].
/// </summary>
public class DateWithinThreeMonthsAttribute : ValidationAttribute, IClientModelValidator
{
    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        // value — это значение свойства Date.
        // Если [Required] уже отработал на пустоту, можно пропустить null.
        if (value is null)
            return ValidationResult.Success; // про обязательность отвечает [Required]

        if (value is not DateTime date)
            return new ValidationResult("Некорректная дата");

        var today = DateTime.Today;
        var max = today.AddMonths(3);

        if (date < today || date > max)
        {
            return new ValidationResult(
                FormatErrorMessage(validationContext.MemberName),
                new[] { validationContext.MemberName! });
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        var today = DateTime.Today;
        var max = today.AddMonths(3);

        // Каждый MergeAttribute добавляет в <input> атрибут data-val-*.
        // 1) включаем валидацию для этого поля
        MergeAttribute(context.Attributes, "data-val", "true");
        // 2) текст ошибки
        MergeAttribute(context.Attributes, "data-val-within3m",
            FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        // 3) параметры (границы диапазона) — формат yyyy-MM-dd понимает new Date() в JS
        MergeAttribute(context.Attributes, "data-val-within3m-min",
            today.ToString("yyyy-MM-dd"));
        MergeAttribute(context.Attributes, "data-val-within3m-max",
            max.ToString("yyyy-MM-dd"));
    }

    // Дефолтный текст ошибки, если ErrorMessage не задан в атрибуте.
    public override string FormatErrorMessage(string name)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
            return ErrorMessage;

        var today = DateTime.Today;
        var max = today.AddMonths(3);
        return $"Дата доставки должна быть с {today:dd.MM.yyyy} по {max:dd.MM.yyyy}";
    }

    // Маленький помощник: добавить атрибут, если его ещё нет.
    private static bool MergeAttribute(
        IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key)) return false;
        attributes.Add(key, value);
        return true;
    }
}