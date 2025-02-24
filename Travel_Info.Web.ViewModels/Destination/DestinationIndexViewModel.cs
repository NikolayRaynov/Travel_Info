using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Web.ViewModels.Destination
{
    public class DestinationIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new List<string>();
        public string UserId { get; set; } = string.Empty;
        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();

    }
}
