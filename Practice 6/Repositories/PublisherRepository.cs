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
    public class PublisherRepository : RepositoryBase
    {
        public ObservableCollection<Publisher> GetPublishersList()
        {
            var publishers = new ObservableCollection<Publisher>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT *\r\nFROM Publishers";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        publishers.Add(new Publisher((int)reader.GetValue(0), (string)reader.GetValue(1)));
                    }
                }

                reader.Close();
            }

            return publishers;
        }
    }
}
