using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingZoneApp.Data.Common.Constants
{
    public static class AppConstants
    {
        //Standard date format used across the application.
        public const string DateFormat = "yyyy-MM-dd";

        //Regex pattern to validate date in the format "yyyy-MM-dd".
        public const string ReleaseDateValidationRegex = "[0-9]{4}-[0-9]{2}-[0-9]{2}";
    }
}
