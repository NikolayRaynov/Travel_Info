using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Travel_Info.Common.EntityValidationConstants.Review;

namespace Travel_Info.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string? Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public int DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual Destination Destination { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
