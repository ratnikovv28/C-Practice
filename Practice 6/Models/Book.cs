using Newtonsoft.Json;
using Practice_6.Helpers;
using Practice_6.Repositories;
using Practice_6.ViewModels;

namespace Practice_6.Models
{
    public class Book
    {
        private readonly BookRepository bookRepository;
        private readonly DataVM dataVM;
        public int BookId { get; }
        public string BookName { get; }
        public string AuthorName { get; }
        public string PublisherName { get; }

        public Book(int bookId, string bookName, string authorName, string publisherName)
        {
            BookId = bookId;
            BookName = bookName;
            AuthorName = authorName;
            PublisherName = publisherName;
            DeleteBook = new RelayCommand(DeleteBookFunc);
            bookRepository = new BookRepository();
            dataVM = AllUC._DataVM;
        }

        #region Command's
        public RelayCommand DeleteBook { get; }

        #endregion

        #region Function's
        public void DeleteBookFunc(object obj)
        {
            dataVM.Books.Remove(this);
            bookRepository.DeleteBookFromList(BookId);
        }
        #endregion
    }
}
