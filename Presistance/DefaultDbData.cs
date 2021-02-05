using AdvertisingApi.Model;

namespace AdvertisingApi.Presistance
{
    public static class DefaultDbData
    {
        public static Category[] Categories = new Category[]
        {
            new() {CategoryId = 1L, Title = "Games"},
            new() {CategoryId = 2L, Title = "Computer science"},
            new() {CategoryId = 3L, Title = "Guitar"},
            new() {CategoryId = 4L, Title = "Social"}
        };

        public static Tag[] Tags = new Tag[]
        {
            new() {TagId = 1L, Title = "Nice job"},
            new() {TagId = 2L, Title = "Meet"},
            new() {TagId = 3L, Title = "Life"},
            new() {TagId = 4L, Title = "Films"},
            new() {TagId = 5L, Title = "One more tag"}
        };

        public static Advertisement[] Ads = new Advertisement[]
        {
            new()
            {
                AdvertisementId = 1L,
                AdType = AdType.TextAd,
                CategoryId = 1L,
                Cost = 34.3m,
                Content = "SomeTest",
                Views = 34L
            },
            new()
            {
                AdvertisementId = 2L,
                AdType = AdType.BannerAd,
                CategoryId = 4L,
                Cost = 15.0m,
                Content = "Girls like dogs... Do you want to buy a dog?",
                Views = 45233L
            },
            new()
            {
                AdvertisementId = 4L,
                AdType = AdType.BannerAd,
                CategoryId = 4L,
                Cost = 15.0m,
                Content = "<html><h1>Hello world</h1></html>",
                Views = 2L
            }
        };
    }
}