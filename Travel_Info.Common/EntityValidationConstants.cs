using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Info.Common
{
    public static class EntityValidationConstants
    {
        public static class Image
        {
            public const int ImageUrlMaxLength = 2083;
            public const int ImageDescriptionMinLength = 10;
            public const int ImageDescriptionMaxLength = 500;
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 10;
            public const int CategoryNameMaxLength = 80;
        }

        public static class Destination
        {
            public const int DestinationNameMinLength = 4;
            public const int DestinationNameMaxLength = 85;
            public const int DestinationDescriptionMinLength = 10;
            public const int DestinationDescriptionMaxLength = 500;
        }
    }
}
