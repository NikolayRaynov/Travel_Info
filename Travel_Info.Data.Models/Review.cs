using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Info.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }


        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;


        [Required]
        public int DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual Destination Destination { get; set; } = null!;
    }
}
