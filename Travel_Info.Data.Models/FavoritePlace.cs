using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travel_Info.Data.Models
{
    public class FavoritePlace
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Comment("Identifier of the user who marked the place as favorite.")]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
    }
}
