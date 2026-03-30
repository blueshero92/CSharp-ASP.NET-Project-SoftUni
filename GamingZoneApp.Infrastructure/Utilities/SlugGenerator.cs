using GamingZoneApp.Infrastructure.Utilities.Interfaces;

namespace GamingZoneApp.Infrastructure.Utilities
{
    public class SlugGenerator : ISlugGenerator
    {
        public string GenerateSlug(string input)
        {
            string[] inputSplit = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                       .Select(i => i.ToLowerInvariant())
                                       .ToArray();

            string slug = string.Join("-", inputSplit);

            return slug;
        }
    }
}
