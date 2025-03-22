using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Travel_Info.Common.EntityValidationConstants.Review;

namespace Travel_Info.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Comment("Rating given to the destination.")]
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        [Comment("Comment from the user about the destination.")]
        public string? Comment { get; set; }

        [Required]
        [Comment("Date when the review was created.")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Comment("Identifier of the user who wrote the review.")]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("Identifier of the destination for which the review is written.")]
        public int DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual Destination Destination { get; set; } = null!;
    }
}
