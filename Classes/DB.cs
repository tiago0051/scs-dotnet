using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Caixa_Seguro.Classes
{
    class DB
    {
        private static string dbpath = "";
        public static void Open_db()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string file = path + "database_sistema_caixa_seguro.db";
            if (!File.Exists(file)) File.CreateText(file);
            dbpath = Path.Combine(path, "database_sistema_caixa_seguro.db");
        }

        public static SqliteCommand ExecuteCommand(string command)
        {
            using SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}");
            SqliteCommand createTable = new SqliteCommand(command, db);

            return createTable;
        }
    }
}
