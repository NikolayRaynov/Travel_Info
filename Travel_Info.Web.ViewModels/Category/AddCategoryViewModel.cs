using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Travel_Info.Common.EntityValidationConstants.Category; // Уверете се, че този using е правилен и сочи към вашите константи

namespace Travel_Info.Web.ViewModels.Category
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Името на категорията на английски е задължително.")]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = "Името на английски трябва да е между {2} и {1} символа.")]
        [Display(Name = "Име (Английски)")]
        [Comment("Name of the category in English. Used for folder names.")]
        public string NameEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Името на категорията на български е задължително.")]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = "Името на български трябва да е между {2} и {1} символа.")]
        [Display(Name = "Име (Български)")]
        [Comment("Name of the category in Cyrillic. Used for display.")]
        public string NameBg { get; set; } = string.Empty;
    }
}