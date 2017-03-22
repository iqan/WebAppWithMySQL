using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using WebAppWithMySQL.Models;

namespace WebAppWithMySQL.Data
{
    public class DataService
    {
        private readonly MySqlConnection _connection;

        public DataService()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString + "password=iqan;";
            _connection = new MySqlConnection(connectionString);
        }

        public List<City> GetCityList(out string error)
        {
            error = string.Empty;
            var cities = new List<City>() {};
            var query = "select * from city";
            try
            {
                _connection.Open();
                var cmd = new MySqlCommand(query, _connection);
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
                    default:
                        error = "An error occurred. Error: " + ex.Message;
                        break;
                }
            }
            catch (Exception e)
            {
                error = "An error occurred. Error: " + e.Message;
            }
            finally
            {
                _connection.Close();
            }
            return cities;
        }
    }
}