using Practice_6.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_6.Repositories
{
    public class BookRepository : RepositoryBase
    {
        public ObservableCollection<Book> GetBooksList()
        {
            var books = new ObservableCollection<Book>();

            using(var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT b.book_id, b.book_name, a.last_name, p.publisher_name\r\nFROM Books b\r\nINNER JOIN Authors a ON b.author_id = a.author_id\r\nINNER JOIN Publishers p ON p.publisher_id = b.publisher_id;";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        books.Add(new Book((int)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(2), (string)reader.GetValue(3)));
                    }
                }

                reader.Close();
            }

            return books;
        }

        public void DeleteBookFromList(int bookId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Books WHERE book_id = @bookId";
                command.Parameters.Add("@bookId", System.Data.SqlDbType.Int).Value = bookId;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }
            }
        }

        public void AddBookToList(string bookName, int authorId, int publisherId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Books VALUES(NEXT VALUE FOR table_name_id_seq, @bookName, @authorId, @publisherId)";
                command.Parameters.Add("@bookName", System.Data.SqlDbType.NVarChar).Value = bookName;
                command.Parameters.Add("@authorId", System.Data.SqlDbType.Int).Value = authorId;
                command.Parameters.Add("@publisherId", System.Data.SqlDbType.Int).Value = publisherId;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }
            }
        }

        public void DeleteAllFromList()
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Books;";
                command.ExecuteNonQuery();
            }
        }

        public void AddBookToListByNames(string bookName, string authorName, string publisherName)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Books VALUES(NEXT VALUE FOR table_name_id_seq, @bookName, (SELECT author_id FROM Authors WHERE last_name = @authorName), (SELECT publisher_id FROM Publishers WHERE publisher_name = @publisherName))";
                command.Parameters.Add("@bookName", System.Data.SqlDbType.NVarChar).Value = bookName;
                command.Parameters.Add("@authorName", System.Data.SqlDbType.NVarChar).Value = authorName;
                command.Parameters.Add("@publisherName", System.Data.SqlDbType.NVarChar).Value = publisherName;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }
            }
        }

        public ObservableCollection<Book> FindDataList(string bookName, int authorId, int publisherId)
        {
            var books = new ObservableCollection<Book>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT b.book_id, a.author_id, p.publisher_id, b.book_name, a.last_name, p.publisher_name\r\nFROM Books b\r\nINNER JOIN Authors a ON b.author_id = a.author_id\r\nINNER JOIN Publishers p ON p.publisher_id = b.publisher_id;";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (((bookName != null || bookName != "") && bookName == (string)reader.GetValue(3)) |
                            (authorId != 0 && authorId == (int)reader.GetValue(1)) |
                            (publisherId != 0 && publisherId == (int)reader.GetValue(2)))
                        {
                            books.Add(new Book((int)reader.GetValue(0), (string)reader.GetValue(3), (string)reader.GetValue(4), (string)reader.GetValue(5)));
                        }
                        else if((bookName == null || bookName == "") && authorId == 0 && publisherId == 0)
                            books.Add(new Book((int)reader.GetValue(0), (string)reader.GetValue(3), (string)reader.GetValue(4), (string)reader.GetValue(5)));
                    }
                }

                reader.Close();
            }

            return books;
        }
    }
}
