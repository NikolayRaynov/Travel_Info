using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Info.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public int DestinationId { get; set; }

        [Required]
        public string Url { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public virtual Destination Destination { get; set; } = null!;
    }
}
