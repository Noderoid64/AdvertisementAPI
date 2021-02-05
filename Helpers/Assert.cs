using System;

namespace AdvertisingApi.Helpers
{
    public static class Assert
    {
        public static void IsNotBlank(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("IsNullOrWhiteSpace");
            }
        }
    }
}