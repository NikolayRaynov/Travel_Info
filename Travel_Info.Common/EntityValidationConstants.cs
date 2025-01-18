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
            public const int UrlMinLength = 8;
            public const int UrlMaxLength = 2083;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;
        }
    }
}
