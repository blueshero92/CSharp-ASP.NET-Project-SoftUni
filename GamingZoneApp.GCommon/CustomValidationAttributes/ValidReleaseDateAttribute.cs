using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static GamingZoneApp.GCommon.Constants.AppConstants;

namespace GamingZoneApp.GCommon.CustomValidationAttributes
{
    /// <summary>
    /// Custom validation attribute to validate release date in the Add/Edit form.
    /// </summary>
    public class ValidReleaseDateAttribute : ValidationAttribute
    {        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime releaseDate;

            //Checks if the release date is provided as DateTime.
            if (value is DateTime dateTime)
            {
                releaseDate = dateTime;
            }
            //Checks if the release date is provided as string and tries to parse it.
            else if (value is string dateString && !string.IsNullOrWhiteSpace(dateString))
            {
                //TryParseExact ensures the date is in the specified format and validates the month/day ranges.
                bool parsedDate = DateTime.TryParseExact(
                    dateString, 
                    DateFormat, 
                    CultureInfo.InvariantCulture, 
                    DateTimeStyles.None, 
                    out releaseDate);

                //Returns error if parsing fails.
                if (!parsedDate)
                {
                    return new ValidationResult($"Please enter the date in {DateFormat} format.");
                }
            }
            else
            {
                return ValidationResult.Success;
            }

            //Check if the date is in the future.
            if (releaseDate > DateTime.Today)
            {
                return new ValidationResult("Release date can't be in the future.");
            }

            //Check if the year is before the minimum allowed year.
            if (releaseDate.Year < ReleaseDateMinYear)
            {
                return new ValidationResult($"Release date can't be earlier than {ReleaseDateMinYear}.");
            }

            return ValidationResult.Success;
        }
    }
}