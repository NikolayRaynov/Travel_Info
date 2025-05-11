namespace Travel_Info.Web.ViewModels.Destination
{
    public class DestinationPageViewModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public ICollection<DestinationIndexViewModel> Destinations { get; set; }

        public DestinationPageViewModel()
        {
            Destinations = new List<DestinationIndexViewModel>();
        }
    }
}
