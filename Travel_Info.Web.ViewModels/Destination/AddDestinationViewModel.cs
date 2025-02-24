namespace Travel_Info.Web.ViewModels.Destination
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using Travel_Info.Web.ViewModels.Category;
    using static Travel_Info.Common.EntityValidationConstants.Destination;
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

        [Required]
        public int CategoryId { get; set; }
        public string User { get; set; } = string.Empty;
        public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
