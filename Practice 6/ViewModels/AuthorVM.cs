using Practice_6.Helpers;
using Practice_6.Models;
using Practice_6.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Practice_6.ViewModels
{
    public class AuthorVM : NotifyPropertyChanged
    {
        private AuthorRepository authorRepository;
        public AuthorVM()
        {
            authorRepository = new AuthorRepository();
            Authors = authorRepository.GetAuthorsList();
            AddAuthor = new RelayCommand(AddAuthorFunc);
            FindAuthor = new RelayCommand(FindAuthorFunc);
            GetAuthors = new RelayCommand(GetAuthorsFunc);
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

        private string _firstNameText;

        public string FirstNameText
        {
            get { return _firstNameText; }
            set
            {
                _firstNameText = value;
                OnPropertyChanged("FirstNameText");
            }
        }

        private string _lastNameText;

        public string LastNameText
        {
            get { return _lastNameText; }
            set
            {
                _lastNameText = value;
                OnPropertyChanged("LastNameText");
            }
        }

        public RelayCommand AddAuthor { get; }

        public RelayCommand FindAuthor { get; }

        public RelayCommand GetAuthors { get; }

        public void AddAuthorFunc(object obj)
        {
            if (LastNameText == null || FirstNameText == null) return;
            authorRepository.AddAuthorToList(FirstNameText, LastNameText);
            Authors = authorRepository.GetAuthorsList();
        }

        public void FindAuthorFunc(object obj)
        {
            Authors = authorRepository.FindAuthorsList(FirstNameText, LastNameText);
        }

        public void GetAuthorsFunc(object obj)
        {
            Authors = authorRepository.GetAuthorsList();
        }
    }
}
