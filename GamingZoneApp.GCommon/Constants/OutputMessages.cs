using System.Reflection.Metadata;

namespace GamingZoneApp.GCommon.Constants
{
    public static class OutputMessages
    {
        public class GameInputModelErrors
        {
            public const string TitleMinLengthError = "Title must be at least {1} characters long.";
            public const string TitleMaxLengthError = "Title cannot exceed {1} characters.";

            public const string DescriptionMinLengthError = "Description must be at least {1} characters long.";
            public const string DescriptionMaxLengthError = "Description cannot exceed {1} characters.";

            public const string ImageUrlMinLengthError = "Image URL must be at least {1} characters long.";
            public const string ImageUrlMaxLengthError = "Image URL cannot exceed {1} characters.";

            public const string DeveloperRequiredError = "Please select a developer.";
            public const string PublisherRequiredError = "Please select a publisher.";
        }

        public class BaseControllerErrors
        {
            public const string UserNotAuthenticatedError = "Authenticated user Id claim not found or invalid.";

        }

        public class GameControllerErrors
        {
            public const string GameAlreadyInFavoritesError = "This game is already in your favorites.";
            public const string OwnGameCannotBeAddedToFavoritesError = "You cannot add your own game to favorites.";
            public const string ErrorAddingGameToFavorites = "An error occurred while adding the game to favorites. Please try again.";
            public const string ErrorAddingGame = "An error occurred while adding the game. Please try again.";

            public const string OwnGameCannotbeRemovedFromFavoritesError 
                = "You cannot remove your own game from favorites because you cannot add it in the first place.";

            public const string GameNotInFavoritesError = "This game is not in your favorites.";
            public const string ErrorRemovingGameFromFavorites = "An error occurred while removing the game from favorites. Please try again.";

            public const string DeveloperDoesNotExistError = "Selected developer does not exist.";
            public const string PublisherDoesNotExistError = "Selected publisher does not exist.";

            public const string NotAuthorizedToEditGameError = "You are not authorized to edit this game.";
            public const string NotAuthorizedToDeleteGameError = "You are not authorized to delete this game.";

            public const string ErrorEditingGame = "An error occurred while editing the game. Please try again.";
            public const string ErrorDeletingGame = "An error occurred while deleting the game. Please try again.";


        }

        public class RegisterErrors
        {
            public const string UsernameLengthError = "The {0} must be at least {2} and at max {1} characters long.";
            public const string PasswordLengthError = "The {0} must be at least {2} and at max {1} characters long.";

            public const string EmailNotSupportedError = "The default UI requires a user store with email support.";

            public const string PasswordsDoNotMatchError = "The password and confirmation password do not match.";
            
        }

        public class TempDataSuccessMessages
        {
            public const string GameAddedToFavoritesSuccessfullyMessage = "Successfully added to favorites!";
            public const string GameRemovedFromFavoritesSuccessfullyMessage = "Successfully removed from favorites!";

            public const string GameAddedSuccessfullyMessage = "Game added successfully!";
            public const string GameEditedSuccessfullyMessage = "Game edited successfully!";
            public const string GameDeletedSuccessfullyMessage = "Game deleted successfully!";

            public const string UserLoggedInSuccessfullyMessage = "You have logged in successfully!";
            public const string UserRegisteredSuccessfullyMessage = "You have registered successfully!";
        }
    }
}
