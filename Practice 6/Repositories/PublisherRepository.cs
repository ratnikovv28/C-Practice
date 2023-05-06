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

        public void DeletePublisherFromList(int publisherId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Publishers WHERE publisher_id = @publisherId";
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

        public void AddPublisherToList(string publisherName)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Publishers VALUES(NEXT VALUE FOR table_name_id_seq, @publisherName)";
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

        public ObservableCollection<Publisher> FindPublishersList(string publisherName)
        {
            var publishers = new ObservableCollection<Publisher>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT *\r\nFROM Publishers;";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (((publisherName != null || publisherName != "") && publisherName == (string)reader.GetValue(1)))
                        {
                            publishers.Add(new Publisher((int)reader.GetValue(0), (string)reader.GetValue(1)));
                        }
                        else if ((publisherName == null || publisherName == ""))
                            publishers.Add(new Publisher((int)reader.GetValue(0), (string)reader.GetValue(1)));
                    }
                }

                reader.Close();
            }

            return publishers;
        }
    }
}
