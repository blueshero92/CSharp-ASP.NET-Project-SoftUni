namespace GamingZoneApp.GCommon.Constants
{
    public static class AppConstants
    {
        //Standard date format used across the application.
        public const string DateFormat = "yyyy-MM-dd";

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
    }
}
