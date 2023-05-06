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
    public class AuthorRepository : RepositoryBase
    {
        public ObservableCollection<Author> GetAuthorsList()
        {
            var authors = new ObservableCollection<Author>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT *\r\nFROM Authors";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        authors.Add(new Author((int)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(2)));
                    }
                }

                reader.Close();
            }

            return authors;
        }

        public void DeleteAuthorFromList(int authorId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Authors WHERE author_id = @authorId";
                command.Parameters.Add("@authorId", System.Data.SqlDbType.Int).Value = authorId;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }
            }
        }

        public void AddAuthorToList(string firstName, string lastName)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Authors VALUES(NEXT VALUE FOR table_name_id_seq, @firstName, @lastName)";
                command.Parameters.Add("@firstName", System.Data.SqlDbType.NVarChar).Value = firstName;
                command.Parameters.Add("@lastName", System.Data.SqlDbType.NVarChar).Value = lastName;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }
            }
        }

        public ObservableCollection<Author> FindAuthorsList(string firstName, string lastName)
        {
            var authors = new ObservableCollection<Author>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT *\r\nFROM Authors;";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (((firstName != null || firstName != "") && firstName == (string)reader.GetValue(1)) |
                            ((lastName != null || lastName != "") && lastName == (string)reader.GetValue(2)))
                        {
                            authors.Add(new Author((int)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(2)));
                        }
                        else if ((firstName == null || firstName == "") && (lastName == null || lastName == ""))
                            authors.Add(new Author((int)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(2)));
                    }
                }

                reader.Close();
            }

            return authors;
        }
    }
}
