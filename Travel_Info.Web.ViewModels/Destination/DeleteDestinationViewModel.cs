namespace Travel_Info.Web.ViewModels.Destination
{
    public class DeleteDestinationViewModel
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
