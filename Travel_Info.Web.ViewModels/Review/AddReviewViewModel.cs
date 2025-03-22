namespace Travel_Info.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using static Travel_Info.Common.EntityValidationConstants.Review;
    public class AddReviewViewModel
    {
        public int DestinationId { get; set; }

        [Range(1, 5, ErrorMessage = "Рейтингът трябва да бъде между 1 и 5.")]
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = string.Empty;
    }
}
