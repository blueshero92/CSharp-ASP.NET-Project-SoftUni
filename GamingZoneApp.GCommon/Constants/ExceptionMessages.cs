namespace GamingZoneApp.GCommon.Constants
{
    public static class ExceptionMessages
    {
        public const string RoleSeedingExceptionMessage = "Failed to seed role {0}.";
        public const string UsernameNotFoundInConfigurationExceptionMessage = "Admin username not found in configuration.";
        public const string EmailNotFoundInConfigurationExceptionMessage = "Admin email not found in configuration.";
        public const string PasswordNotFoundInConfigurationExceptionMessage = "Admin password not found in configuration.";

        public const string ErrorWhileTryingToSeedAdminUserExceptionMessage = "There was an error while trying to seed admin user!";
        public const string ErrorWhileTryingToAddAdminUserToAdminRoleExceptionMessage = "There was an error while trying to add admin user to admin role!";
    }
}
