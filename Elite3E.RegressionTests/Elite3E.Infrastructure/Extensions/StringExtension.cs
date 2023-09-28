namespace Elite3E.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static bool ToBoolean(this string value)
        {
            if (value.ToLower().Contains("yes") || value.ToLower().Contains("true"))
                return true;
         
            return false;
        }
    }
}
