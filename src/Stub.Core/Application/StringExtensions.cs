using System.Text;

namespace Stub.Core.Application
{
    public static class StringExtensions
    {
        public static string FromBase64String(this string base64String) =>
                Encoding.UTF8.GetString(Convert.FromBase64String(base64String ?? string.Empty));

        public static string ToBase64String(this string plainText) =>
          Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText ?? string.Empty));

        public static string Minify(this string text) =>
          text.Replace("\n", "").Replace("\r", "").Replace(" ", "").ToLower();

        public static bool IsEqualTo(this string a, string b) =>
            a.Equals(b, StringComparison.InvariantCultureIgnoreCase);
    }
}
