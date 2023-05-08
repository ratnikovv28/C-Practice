using System.Collections;
using System.Collections.Generic;

namespace Practice_7.Models
{
    public class Author
    {
        public Author() => Songs = new HashSet<Song>();

        public int Id { get; set; }
        public string Pseudonym { get; set; }
        public string Country { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
