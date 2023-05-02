using System.Collections.ObjectModel;
using System.Windows;
using Practice_6.Helpers;
using Practice_6.Models;
using Practice_6.Repositories;

namespace Practice_6.ViewModels
{
    public class DataVM : NotifyPropertyChanged
    {
        public BookRepository bookRepository;
        private AuthorRepository authorRepository;
        private PublisherRepository publisherRepository;
        public DataVM()
        {
            authorRepository = new AuthorRepository();
            publisherRepository = new PublisherRepository();
            bookRepository = new BookRepository();
            Books = bookRepository.GetBooksList();
            Publishers = publisherRepository.GetPublishersList();
            Authors = authorRepository.GetAuthorsList();
            UpdateData = new RelayCommand(UpdateDataFunc);
            AddBook = new RelayCommand(AddBookFunc);
            FindData = new RelayCommand(FindDataFunc);
        }

        #region Property's
        public ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged("Books");
            }
        }

        public ObservableCollection<Author> _authors;
        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropertyChanged("Authors");
            }
        }

        public ObservableCollection<Publisher> _publishers;
        public ObservableCollection<Publisher> Publishers
        {
            get => _publishers;
            set
            {
                _publishers = value;
                OnPropertyChanged("Publishers");
            }
        }

        private string _bookNameText;

        public string BookNameText
        {
            get { return _bookNameText; }
            set 
            { 
                _bookNameText = value;
                OnPropertyChanged("BookNameText");
            }
        }

        private int _selectedAuthor;
        public int SelectedAuthor
        {
            get { return _selectedAuthor; }
            set 
            { 
                _selectedAuthor = value;
                OnPropertyChanged("SelectedAuthor");
            }
        }

        private int _selectedPublisher;
        public int SelectedPublisher
        {
            get { return _selectedPublisher; }
            set
            {
                _selectedPublisher = value;
                OnPropertyChanged("SelectedPublisher");
            }
        }
        #endregion

        #region Command's
        public RelayCommand UpdateData { get; }

        public RelayCommand AddBook { get; }

        public RelayCommand FindData { get; }
        #endregion

        #region Function's
        public void UpdateDataFunc(object obj)
        {
            Books = bookRepository.GetBooksList();
            Authors = authorRepository.GetAuthorsList();
            Publishers = publisherRepository.GetPublishersList();
        }

        public void AddBookFunc(object obj)
        {
            if (SelectedPublisher == null || SelectedAuthor == null || BookNameText == null) return;
            bookRepository.AddBookToList(BookNameText, SelectedAuthor + 1, SelectedPublisher + 1);
            Books = bookRepository.GetBooksList();
        }

        public void FindDataFunc(object obj)
        {
            Books = bookRepository.FindDataList(BookNameText, SelectedAuthor, SelectedPublisher);
        }
        #endregion
    }
}
