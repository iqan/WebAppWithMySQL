using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WebAppWithMySQL.Models;

namespace WebAppWithMySQL.Data
{
    public class DataService
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DataService()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            uid = "iqan";
            password = "iqan";
            var connectionString = "SERVER=localhost;" + "DATABASE=sakila;" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public List<City> GetCityList(out string error)
        {
            error = string.Empty;
            var cities = new List<City>() {};
            var query = "select * from city";
            try
            {
                connection.Open();
                var cmd = new MySqlCommand(query, connection);
                var dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    cities.Add(
                        new City()
                        {
                            city_id = int.Parse(dataReader["city_id"].ToString()),
                            city = dataReader["city"].ToString(),
                            country_id = int.Parse(dataReader["country_id"].ToString()),
                            last_update = DateTime.Parse(dataReader["last_update"].ToString())
                        }
                    );
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        error = "Cannot connect to server.  Contact administrator";
                        break;
                    case 1045:
                        error = "Invalid username/password, please try again";
                        break;
                }
            }
            finally
            {
                connection.Close();
            }
            return cities;
        }
    }
}