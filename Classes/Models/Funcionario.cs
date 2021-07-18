using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Caixa_Seguro.Classes.Models
{
    public class Funcionario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int IdTipoAcesso { get; set; }

        public void RegisterFuncionario(string Senha)
        {
            SqliteCommand cmd = Classes.DB.ExecuteCommand("INSERT INTO Funcionario(NomeFuncionario, SenhaFuncionario, idTipoAcesso, DataCadastroFuncionario) VALUES" +
                "('" + Nome + "', '" + Login.Encrypt(Senha) + "', '" + IdTipoAcesso + "', '" + DateTime.Now + "');");

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

    }
}
