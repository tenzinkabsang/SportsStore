namespace SportsStore.Web.Infrastructure
{
    public static class StringExtensions
    {
        public static string FirstName(this string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return fullName;

            return fullName.Split(new char[] { ' ' })[0];
        }

        public static string LastName(this string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return fullName;

            string[] result = fullName.Split(new char[] { ' ' });

            return result.Length == 2 ? result[1] : result[0];
        }
    }
}
