namespace Travel_Info.Web.ViewModels.FavoritePlace
{
    using System.ComponentModel.DataAnnotations;
    using static Travel_Info.Common.EntityValidationConstants.Destination;
    using static Travel_Info.Common.EntityValidationConstants.Image;
    public class DeleteFavoriteViewModel
    {
        public int DestinationId { get; set; }

        [Required]
        [MinLength(DestinationNameMinLength)]
        [MaxLength(DestinationNameMaxLength)]
        public string DestinationName { get; set; } = null!;

        [Required]
        [MinLength(DestinationDescriptionMinLength)]
        [MaxLength(DestinationDescriptionMaxLength)]
        public string DestinationDescription { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength)]
        public List<string> DestinationImageUrls { get; set; } = new List<string>();
    }
}
