using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            Console.WriteLine("Getting DB Connection...");
            SqliteDbManager databaseObject = new SqliteDbManager();

            databaseObject.OpenConnection();
            databaseObject.GetCountryPopulation();
            databaseObject.CloseConnection();


            //IDbManager db = new SqliteDbManager();
            //DbConnection conn = db.getConnection();
             
            
            

            

            Console.ReadLine();
            //if(conn == null)
            //{
            //    Console.WriteLine("Failed to get connection");
            //}
        }
    }
}
