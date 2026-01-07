namespace GamingZoneApp.Data.Common.Constants
{
    public static class EntityValidationConstants
    {
        public class GameConstants
        {
            ///Maximum characters for a game title.
            public const int TitleMaxLength = 184;

            ///Maximum characters for the game description/trivia.
            public const int GameDescriptionMaxLength = 2048;

            //Column type for the game rating.
            public const string GameRatingType = "DECIMAL(3,1)";
        }

        public class DeveloperConstants
        {
            ///Maximum characters for the developer name.
            public const int DevNameMaxLength = 128;

            ///Maximum characters for the developer description.
            public const int DevDescriptionMaxLength = 2048;
        }

        public class PublisherConstants
        {
            //Maximum characters for the publisher name.
            public const int PublisherNameMaxLength = 128;

            //Maximum characters for the publisher description.
            public const int PublisherDescriptionMaxLength = 2048;
        }
    }
}
