using System.Collections.Generic;

namespace Practice_7.Models
{
    public class ViewModel
    {
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
