using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Travel_Info.Common.EntityValidationConstants.Image;

namespace Travel_Info.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        [Comment("URL of the image.")]
        public string Url { get; set; } = string.Empty;

        [Required]
        [MaxLength(ImageDescriptionMaxLength)]
        [Comment("Description of the image.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Identifier of the destination to which the image belongs.")]
        public int DestinationId { get; set; }

        [ForeignKey(nameof(DestinationId))]
        public virtual Destination Destination { get; set; } = null!;
    }
}
