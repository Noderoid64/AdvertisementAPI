using System;

namespace AdvertisingApi.Helpers
{
    public static class Assert
    {
        public static void isNotBlank(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("IsNullOrWhiteSpace");
            }
        }
    }
}