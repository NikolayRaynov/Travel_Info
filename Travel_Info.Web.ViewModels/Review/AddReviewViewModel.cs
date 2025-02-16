namespace Travel_Info.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using static Travel_Info.Common.EntityValidationConstants.Destination;
    using static Travel_Info.Common.EntityValidationConstants.Review;
    public class AddReviewViewModel
    {
        public int DestinationId { get; set; }

        [Required]
        [MinLength(DestinationNameMinLength)]
        [MaxLength(DestinationNameMaxLength)]
        public string DestinationName { get; set; } = null!;
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = string.Empty;
    }
}
