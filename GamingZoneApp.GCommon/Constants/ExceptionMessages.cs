namespace GamingZoneApp.GCommon.Constants
{
    public static class ExceptionMessages
    {
        public const string RoleSeedingExceptionMessage = "Failed to seed role {0}.";

        public const string AdminUsernameNotFoundInConfigurationExceptionMessage = "Admin username not found in configuration.";
        public const string AdminEmailNotFoundInConfigurationExceptionMessage = "Admin email not found in configuration.";
        public const string AdminPasswordNotFoundInConfigurationExceptionMessage = "Admin password not found in configuration.";

        public const string ErrorWhileTryingToSeedAdminUserExceptionMessage = "There was an error while trying to seed admin user!";
        public const string ErrorWhileTryingToAddAdminUserToAdminRoleExceptionMessage = "There was an error while trying to add admin user to admin role!";

        public const string ModeratorUsernameNotFoundInConfigurationExceptionMessage = "Moderator username not found in configuration.";
        public const string ModeratorEmailNotFoundInConfigurationExceptionMessage = "Moderator email not found in configuration.";
        public const string ModeratorPasswordNotFoundInConfigurationExceptionMessage = "Moderator password not found in configuration.";

        public const string ErrorWhileTryingToSeedModeratorUserExceptionMessage = "There was an error while trying to seed moderator user!";
        public const string ErrorWhileTryingToAddModeratorUserToModeratorRoleExceptionMessage = "There was an error while trying to add moderator user to moderator role!";
    }
}
