using Microsoft.EntityFrameworkCore;
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
        [Comment("Name of the destination.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(DestinationDescriptionMaxLength)]
        [Comment("Description of the destination.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Identifier of the category to which the destination belongs.")]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        [Comment("Identifier of the favorite place associated with the destination.")]
        public int? FavoritePlaceId { get; set; }
        [ForeignKey(nameof(FavoritePlaceId))]
        public virtual FavoritePlace? FavoritePlace { get; set; }

        [Comment("Identifier of the place to visit associated with the destination.")]
        public int? PlaceToVisistId { get; set; }
        [ForeignKey(nameof(PlaceToVisistId))]
        public virtual PlaceToVisit? PlaceToVisit { get; set; }

        [Comment("Indicator for logical deletion of the destination.")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
