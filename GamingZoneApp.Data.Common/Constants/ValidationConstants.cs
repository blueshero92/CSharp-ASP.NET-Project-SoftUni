using System.Reflection.Metadata;

namespace GamingZoneApp.Data.Common.Constants
{
    public static class ValidationConstants
    {
    

        public class GameConstants
        {
            /// <summary>
            /// Title should be able to store text with length of at least 2 characters.
            /// </summary>
            public const int TitleMinLength = 2;

            /// <summary>
            /// Title should be able to store text with length up to 184 characters.
            /// </summary>
            public const int TitleMaxLength = 184;

            /// <summary>
            /// Game description should be able to store text with length of at least to 30 characters.
            /// </summary>
            public const int GameDescriptionMinLength = 30;

            /// <summary>
            /// Game description should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int GameDescriptionMaxLength = 2048;

            /// <summary>
            /// Column type of the rating should have 2 digits before and 1 digit after the decimal point.
            /// </summary>
            public const string GameRatingType = "DECIMAL(3,1)";

            /// <summary>
            /// Release date column type should be datetime2.
            /// </summary>
            public const string ReleaseDateType = "datetime2";

            /// <summary>
            /// This is the maximum value for the game rating.
            /// </summary>
            public const decimal GameRatingMaxValue = 10.0m;

            /// <summary>
            /// Image URL should be able to store text with length of at least 7 characters.
            /// </summary>
            public const int ImageUrlMinLength = 7;

            /// <summary>
            /// Image URL should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int ImageUrlMaxLength = 2048;
        }

        public class DeveloperConstants
        {
            /// <summary>
            /// Developer Name should be able to store text with length of at least 2 characters.
            /// </summary>
            public const int DevNameMinLength = 2;

            /// <summary>
            /// Developer name should be able to store text with length up to 128 characters.
            /// </summary>
            public const int DevNameMaxLength = 128;

            /// <summary>
            /// Developer description should be able to store text with length of at least to 256 characters.
            /// </summary>
            public const int DevDescriptionMinLength = 256;

            /// <summary>
            /// Developer description should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int DevDescriptionMaxLength = 2048;

            /// <summary>
            /// Image URL should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int ImageUrlMaxLength = 2048;
        }

        public class PublisherConstants
        {
            /// <summary>
            /// Publisher Name should be able to store text with length of at least 2 characters.
            /// </summary>
            public const int PublisherNameMinLength = 2;

            /// <summary>
            /// Publisher name should be able to store text with length up to 128 characters.
            /// </summary>
            public const int PublisherNameMaxLength = 128;

            /// <summary>
            /// Publisher description should be able to store text with length of at least to 256 characters.
            /// </summary>
            public const int PublisherDescriptionMinLength = 256;

            /// <summary>
            /// Publisher description should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int PublisherDescriptionMaxLength = 2048;

            /// <summary>
            /// Image URL should be able to store text with length up to 2048 characters.
            /// </summary>
            public const int ImageUrlMaxLength = 2048;
        }
    }
}
