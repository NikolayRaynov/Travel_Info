namespace Travel_Info.Web.ViewModels.Destination
{
    using System.ComponentModel.DataAnnotations;
    using static Travel_Info.Common.EntityValidationConstants.Destination;
    using static Travel_Info.Common.EntityValidationConstants.Image;
    public class CreateDestinationViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DestinationNameMinLength)]
        [MaxLength(DestinationNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(DestinationDescriptionMinLength)]
        [MaxLength(DestinationDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
    }
}
