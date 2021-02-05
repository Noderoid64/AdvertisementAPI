namespace AdvertisingApi.Model
{
    public class Category
    {
        public long CategoryId { get; set; }
        public string Title { get; set; }

        public Category() { }

        public Category(string title)
        {
            Title = title;
        }
    }
}