using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sistema_Caixa_Seguro.Classes
{
    class DB
    {
        private static string dbpath = "";
        public async static void Open_db()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("database.db", CreationCollisionOption.OpenIfExists);
            dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.db");
        }

        public static SqliteCommand ExecuteCommand(String command)
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                SqliteCommand createTable = new SqliteCommand(command, db);

                return createTable;
            }
        }
    }
}
