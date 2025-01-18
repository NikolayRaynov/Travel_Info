using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Info.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<PlaceToVisit> PlacesToVisit { get; set; } = new List<PlaceToVisit>();
        public virtual ICollection<FavoritePlace> FavoritesPlaces { get; set; } = new List<FavoritePlace>();
    }
}
