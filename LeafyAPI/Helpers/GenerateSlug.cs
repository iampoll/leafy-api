//create function to generate slug from name
//slug is used for the url of the user
//slug should not have special characters, only letters, numbers, and hyphens
//slug should not have consecutive hyphens
//slug should not start or end with a hyphen

using System.Text.RegularExpressions;

namespace LeafyAPI.Helpers
{
    public static class GenerateSlug
    {
        public static string CreateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            string slug = name.ToLowerInvariant();

            slug = RemoveSpecialCharacters(slug);

            slug = RemoveConsecutiveHyphens(slug);

            slug = RemoveLeadingAndTrailingHyphens(slug);

            return slug;
        }

        private static string RemoveSpecialCharacters(string input)
        {
            return Regex.Replace(input, @"[^a-z0-9\s-]", "");
        }

        private static string RemoveConsecutiveHyphens(string input)
        {
            return Regex.Replace(input, @"[\s-]+", "-");
        }

        private static string RemoveLeadingAndTrailingHyphens(string input)
        {
            return input.Trim('-');
        }
    }
}
