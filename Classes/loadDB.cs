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
    class LoadDB
    {
        public static void Acessos()
        {
            String command = "CREATE TABLE IF NOT EXISTS `TipoAcesso` (" +
                "`idTipoAcesso` INTEGER NOT NULL UNIQUE," +
                "`NomeAcesso` TEXT NOT NULL, " +
                "PRIMARY KEY('idTipoAcesso' AUTOINCREMENT));";

            SqliteCommand cmd = DB.ExecuteCommand(command);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            command = "INSERT INTO 'TipoAcesso'(NomeAcesso)" +
                "SELECT 'Vendedor' " +
                "WHERE NOT EXISTS(SELECT * FROM 'TipoAcesso' WHERE NomeAcesso = 'Vendedor');";

            cmd.CommandText = command;
            cmd.ExecuteNonQuery();

            command = "INSERT INTO 'TipoAcesso'(NomeAcesso)" +
                "SELECT 'Gerente' " +
                "WHERE NOT EXISTS(SELECT * FROM 'TipoAcesso' WHERE NomeAcesso = 'Gerente');";

            cmd.CommandText = command;
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public static void Funcionarios()
        {
            String command = "CREATE TABLE IF NOT EXISTS 'Funcionario'(" +
                "'idFuncionario' INTEGER NOT NULL UNIQUE," +
                "'NomeFuncionario'   TEXT NOT NULL," +
                "'SenhaFuncionario' TEXT NOT NULL," +
                "'idTipoAcesso'  INTEGER NOT NULL," +
                "'DataCadastroFuncionario' TEXT NOT NULL, " +
                "PRIMARY KEY('idFuncionario' AUTOINCREMENT) " +
                "FOREIGN KEY(idTipoAcesso) REFERENCES TipoAcesso(idTipoAcesso));";

            SqliteCommand cmd = DB.ExecuteCommand(command);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public static void CodBarras()
        {
            string command = "CREATE TABLE IF NOT EXISTS `CodBarProduto` (" +
                "`idCodBarProduto` INTEGER NOT NULL UNIQUE," +
                "`idProduto` INTEGER NOT NULL, " +
                "`CodBar` TEXT NOT NULL, " +
                "PRIMARY KEY('idCodBarProduto' AUTOINCREMENT) );";

            SqliteCommand cmd = DB.ExecuteCommand(command);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public static void CategoriaProdutos()
        {
            string command = "CREATE TABLE IF NOT EXISTS `CategoriaProdutos` (" +
                "`idCategoria` INTEGER NOT NULL UNIQUE," +
                "`NomeCategoria` TEXT NOT NULL, " +
                "`DescricaoCategoria` TEXT NOT NULL, " +
                "PRIMARY KEY('idCategoria' AUTOINCREMENT) );";

            SqliteCommand cmd = DB.ExecuteCommand(command);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }
    }
}
