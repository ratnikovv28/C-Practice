namespace Practice_6.Models
{
    public class Author
    {
        public long AuthorId { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Author(long authorId, string firstName, string lastName)
        {
            AuthorId = authorId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
