using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Info.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int DestinationId { get; set; }

        public string UserId { get; set; } = null!;

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Destination Destination { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
