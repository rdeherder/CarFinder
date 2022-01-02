namespace CarFinderApi.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            value = value.ToLower();
            string result = value[0].ToString().ToUpper();
            if (value.Length > 1)
            {
                result += value[1..];
            }

            return result;
        }
    }
}
