using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Travel_Info.Web.ViewModels.Category
{
    using static Travel_Info.Common.EntityValidationConstants.Category;

    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на категорията на английски е задължително.")]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength,
            ErrorMessage = "Името на английски трябва да е между {2} и {1} символа.")]
        [Display(Name = "Име (Английски)")]
        [Comment("Name of the category in English. Used for folder names.")]
        public string NameBg { get; set; } = string.Empty;

        [Required(ErrorMessage = "Името на категорията на български е задължително.")]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength,
            ErrorMessage = "Името на български трябва да е между {2} и {1} символа.")]
        [Display(Name = "Име (Български)")]
        [Comment("Name of the category in Cyrillic. Used for display.")]
        public string NameEn { get; set; } = string.Empty;
    }
}
