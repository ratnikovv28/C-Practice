namespace Practice_6.Models
{
    public class Publisher
    {
        public long PublisherId { get; }
        public string PublisherName { get; }

        public Publisher(long publisherId, string publisherName)
        {
            PublisherId = publisherId;
            PublisherName = publisherName;
        }
    }
}
