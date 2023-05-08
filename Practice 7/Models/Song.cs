namespace Practice_7.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        //Ссылка на автора
        public int AuthorId { get; set; } 
        public Author Author { get; set; }
    }
}
