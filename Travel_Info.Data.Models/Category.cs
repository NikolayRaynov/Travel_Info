using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Travel_Info.Common.EntityValidationConstants.Category;

namespace Travel_Info.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
    }
}
