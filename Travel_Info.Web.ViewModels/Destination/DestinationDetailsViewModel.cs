﻿using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Web.ViewModels.Destination
{
    public class DestinationDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    }
}
