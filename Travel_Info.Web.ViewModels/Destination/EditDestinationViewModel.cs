namespace Travel_Info.Web.ViewModels.Destination
{
    using System.ComponentModel.DataAnnotations;
    using static Travel_Info.Common.EntityValidationConstants.Destination;
    using static Travel_Info.Common.EntityValidationConstants.Image;
    public class EditDestinationViewModel
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
        public int CategoryId { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        public List<string> ImageUrls { get; set; } = new List<string>();
        public string User { get; set; } = string.Empty;
    }
}
