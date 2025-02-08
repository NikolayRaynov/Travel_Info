using System.ComponentModel.DataAnnotations;

namespace Travel_Info.Web.ViewModels.FavoritePlace
{
    using static Travel_Info.Common.EntityValidationConstants.Destination;
    using static Travel_Info.Common.EntityValidationConstants.Image;
    public class AddToFavoriteViewModel
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
        public string DestinationImageUrl { get; set; } = null!;
    }
}
