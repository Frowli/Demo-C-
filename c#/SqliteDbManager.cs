using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class SqliteDbManager
    {


        public ConcreteStatService statPop = new ConcreteStatService();
        public SQLiteConnection connection;
        public SqliteDbManager()
        {
            connection = new SQLiteConnection("Data Source=citystatecountry.db;Version=3;FailIfMissing=True"); //create database path
        }
 
        public void GetCountryPopulation()
        {
            List<Tuple<string, int>> populationData = new List<Tuple<string, int>>();
            //query to get Country names and population
            string query = "SELECT Country.CountryName, SUM(City.Population) AS Population FROM Country JOIN State ON Country.CountryId = State.CountryId JOIN City ON State.StateId = City.StateId GROUP BY Country.CountryId";
            SQLiteCommand myCommand = new SQLiteCommand(query, connection);
            SQLiteDataReader result = myCommand.ExecuteReader();
            var statData = statPop.GetCountryPopulations();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    populationData.Add(new Tuple<string, int>(result[0].ToString(), (int)result.GetDouble(1))); //casting to double, as UK has a decimal point for some reason.
                }
            }
            populationData.AddRange(statData); //combine both lists
            populationData = statData.Distinct().ToList();
            //List<Tuple<string, int>> uniquePopulationData = populationData.Distinct().ToList();
            Console.WriteLine(String.Join("\n", populationData)); //remove duplicates

        }

        public void OpenConnection() //Open connection to database
        {
            if(connection.State != System.Data.ConnectionState.Open) //if connection to db is not open, open it
            {
                connection.Open();
                Console.WriteLine("Connected!");
            }
        }

        public void CloseConnection() //Close connection to database
        {
            if (connection.State != System.Data.ConnectionState.Closed) //if connection is open, close it.
            {
                connection.Close();
                Console.WriteLine("Disconnected!");
            }
        }
    }
}
