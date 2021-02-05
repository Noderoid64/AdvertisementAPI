namespace AdvertisingApi.Presistance
{
    public static class PostgressSQL
    {
        public static string ResetQueueSequenceVal()
        {
            return "SELECT setval('queuesequence', 1, true)";
        }

        public static string GetQueueSequenceVal()
        {
            return "SELECT nextval('queuesequence')";
        }

        public static string SetQueueSequenceVal(long value)
        {
            return $"SELECT setval('queuesequence', {value}, FALSE)";
        }
    }
}