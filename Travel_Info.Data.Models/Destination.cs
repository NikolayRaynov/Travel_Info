using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Travel_Info.Common.EntityValidationConstants.Destination;

namespace Travel_Info.Data.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DestinationNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(DestinationDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        [Required]
        public int FavoritePlaceId { get; set; }
        [ForeignKey(nameof(FavoritePlaceId))]
        public FavoritePlace FavoritePlace { get; set; } = null!;

        [Required]
        public int PlaceToVisistId { get; set; }
        [ForeignKey(nameof(PlaceToVisistId))]
        public PlaceToVisit PlaceToVisit { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
