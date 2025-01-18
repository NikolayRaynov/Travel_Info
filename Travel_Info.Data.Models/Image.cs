using System.ComponentModel.DataAnnotations;
using static Travel_Info.Common.EntityValidationConstants.Image;

namespace Travel_Info.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public int DestinationId { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        public string Url { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public virtual Destination Destination { get; set; } = null!;
    }
}
