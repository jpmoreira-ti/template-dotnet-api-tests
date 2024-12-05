namespace TestsAPI.FakeStoreResponse
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required string Image { get; set; }
        public Rating? Rating { get; set; }
    }

    public class Rating
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}