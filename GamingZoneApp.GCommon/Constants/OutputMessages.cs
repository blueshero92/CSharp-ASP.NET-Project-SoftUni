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

        public class UserManagementControllerErrors
        {
            public const string ErrorRoleNotSelected = "Please select a role to assign.";
            public const string ErrorAssigningRole = "Role already assigned to user.";
            public const string ErrorRetrievingUsers = "An error occurred while retrieving users. Please try again.";
            public const string ErrorRetrievingRoles = "An error occurred while retrieving roles. Please try again.";

            public const string ErrorRoleNotSelectedForRemoval = "Please select a role to remove.";
            public const string ErrorRemovingRole = "An error occurred while removing the role. Please ensure the user and role exist and try again.";

            public const string ErrorDeletingUser = "An error occurred while deleting the user. Please ensure the user exists and try again.";

            public const string UserNotFoundError = "User not found. Please ensure the user exists and try again.";

        }

        public class UserManagementControllerSuccessMessages
        {
            public const string RoleAssignedSuccessfullyMessage = "Role assigned successfully!";
            public const string RoleRemovedSuccessfullyMessage = "Role removed successfully!";
            public const string UserDeletedSuccessfullyMessage = "User deleted successfully!";
        }

        public class DeveloperManagementControllerErrors
        {
            public const string ErrorAddingDeveloper = "An error occurred while adding the developer. Please try again.";
            public const string ErrorEditingDeveloperForm = "Please correct the errors in the form.";
            public const string ErrorEditingDeveloper = "An error occurred while editing the developer. Please try again.";
            public const string ErrorDeletingDeveloper = "An error occurred while deleting the developer. Please try again.";
        }

        public class DeveloperManagemntControllerSuccessMessages
        {
            public const string DeveloperAddedSuccessfullyMessage = "Developer added successfully!";
            public const string DeveloperEditedSuccessfullyMessage = "Developer edited successfully!";
            public const string DeveloperDeletedSuccessfullyMessage = "Developer deleted successfully!";
        }

        public class PublisherManagementControllerErrors
        {
            public const string ErrorAddingPublisher = "An error occurred while adding the publisher. Please try again.";
            public const string ErrorEditingPublisherForm = "Please correct the errors in the form.";
            public const string ErrorEditingPublisher = "An error occurred while editing the publisher. Please try again.";
            public const string ErrorDeletingPublisher = "An error occurred while deleting the publisher. Please try again.";
        }

        public class PublisherManagementControllerSuccessMessages
        {
            public const string PublisherAddedSuccessfullyMessage = "Publisher added successfully!";
            public const string PublisherEditedSuccessfullyMessage = "Publisher edited successfully!";
            public const string PublisherDeletedSuccessfullyMessage = "Publisher deleted successfully!";
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
