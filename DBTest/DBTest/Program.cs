using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.Common;

//Bu program desktop-hfs3cb1.StoreDB.dbo daki tablodan verileri çekip yazdırmaya yarar.

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            String provider = ConfigurationManager.AppSettings["provider"];
            String connectionString = ConfigurationManager.AppSettings["connectionString"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    Console.WriteLine("Connection Error");
                    Console.ReadLine();
                    return;
                }
                connection.ConnectionString = connectionString;
                connection.Open();

                DbCommand command = factory.CreateCommand();

                if (command == null)
                {
                    Console.WriteLine("Command Error");
                    Console.ReadLine();
                    return;
                }
                command.Connection = connection;
                command.CommandText = "Select * From Products";

                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"{dataReader["ProdId"]}" + $"{dataReader["Product"]}");
                    }
                }
                Console.ReadLine();
            }


        }
    }
}
