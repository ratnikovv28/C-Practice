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
    }
}
