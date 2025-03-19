using System.Text;

namespace Asset_Manager.Models  
{
    public static class StringExtensionMethods
    {
        public static string Slug(this string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if (!char.IsPunctuation(c) || c == '-')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Replace(' ', '-').ToLower();
        }

        public static bool EqualsNoCase(this string str, string tocompare) =>
            str?.ToLower() == tocompare?.ToLower();

        public static int ToInt(this string str)
        {
            int.TryParse(str, out int id);
            return id;
        }

        public static string Capitalize(this string str) =>
            str?.Substring(0, 1)?.ToUpper() + str?.Substring(1);
    }
}
