using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Info.Data.Models
{
    public class FavoritePlace
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
    }
}
