namespace GamingZoneApp.Data.Common.Constants
{
    public static class EntityValidationConstants
    {
        public class GameConstants
        {
            
            /// <summary>
            /// Title should be able to store text with length up to 184 characters.
            /// </summary>
            public const int TitleMaxLength = 184;

            /// <summary>
            /// Game description should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int GameDescriptionMaxLength = 2048;

            /// <summary>
            /// Column type of the rating should have 2 digits before and 1 digit after the decimal point.
            /// </summary>
            public const string GameRatingType = "DECIMAL(3,1)";
        }

        public class DeveloperConstants
        {
            /// <summary>
            /// Developer name should be able to store text with length up to 128 characters.
            /// </summary>
            public const int DevNameMaxLength = 128;

            /// <summary>
            /// Developer description should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int DevDescriptionMaxLength = 2048;
        }

        public class PublisherConstants
        {
            /// <summary>
            /// Publisher name should be able to store text with length up to 128 characters.
            /// </summary>
            public const int PublisherNameMaxLength = 128;

            /// <summary>
            /// Publisher description should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int PublisherDescriptionMaxLength = 2048;
        }
    }
}
