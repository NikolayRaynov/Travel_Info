namespace Travel_Info.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using static Travel_Info.Common.EntityValidationConstants.Review;
    public class AddReviewViewModel
    {
        public int DestinationId { get; set; }
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = string.Empty;
    }
}
