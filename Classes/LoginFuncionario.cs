using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Caixa_Seguro.Classes
{
    class LoginFuncionario : Util
    {

        public static bool IsFirstLogin()
        {
            bool isFirstLoged = false;
            SqliteCommand cmd = Classes.DB.ExecuteCommand("SELECT NomeFuncionario FROM Funcionario");

            cmd.Connection.Open();
            SqliteDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
                isFirstLoged = true;

            cmd.Connection.Close();

            return isFirstLoged;
        }
    }
}
