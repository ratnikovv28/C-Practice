using Practice_6.Helpers;
using Practice_6.Repositories;
using Practice_6.ViewModels;

namespace Practice_6.Models
{

    public class Author
    {
        private readonly AuthorRepository authorRepository;
        private readonly AuthorVM authorVM;
        public int AuthorId { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Author(int authorId, string firstName, string lastName)
        {
            AuthorId = authorId;
            FirstName = firstName;
            LastName = lastName;
            DeleteAuthor = new RelayCommand(DeleteAuthorFunc);
        }

        #region Command's
        public RelayCommand DeleteAuthor { get; }

        #endregion

        #region Function's
        public void DeleteAuthorFunc(object obj)
        {
            authorVM.Authors.Remove(this);
            authorRepository.DeleteAuthorFromList(AuthorId);
        }
        #endregion
    }
}
