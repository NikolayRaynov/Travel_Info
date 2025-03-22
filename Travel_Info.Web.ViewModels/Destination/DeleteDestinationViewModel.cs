using System.ComponentModel.DataAnnotations;

namespace Travel_Info.Web.ViewModels.Destination
{
    using static Travel_Info.Common.EntityValidationConstants.Destination;
    public class DeleteDestinationViewModel
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
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
