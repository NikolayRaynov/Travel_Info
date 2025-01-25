using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static Travel_Info.Common.EntityValidationConstants.Category;

namespace Travel_Info.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Name of the category in English.")]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Name of the category in Cyrillic.")]
        public string NameBg { get; set; } = string.Empty;

        public virtual ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
    }
}
