namespace GamingZoneApp.GCommon.Constants
{
    public static class AppConstants
    {
        //Standard date format used across the application.
        public const string AppDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// This is the minimum value for the game rating.
        /// </summary>
        public const decimal GameRatingMinValue = 0.0m;

        /// <summary>
        /// This is the maximum value for the game rating.
        /// </summary>
        public const decimal GameRatingMaxValue = 10.0m;

        //The first ever video game was created in 1958.
        /// <summary>
        /// The minimum valid year for a game's release date.
        /// </summary>
        public const int ReleaseDateMinYear = 1958;

        //Messages for TempData keys used to display notification messages to the user when adding to favorites, creating, editing or deleting a game.
        public const string ErrorTempDataKey = "ErrorMessage";
        public const string SuccessTempDataKey = "SuccessMessage";
        public const string WarningTempDataKey = "WarningMessage";
        public const string InfoTempDataKey = "InfoMessage";
        public const string FavoritesErrorTempDataKey = "FavoritesError";

    }
}
