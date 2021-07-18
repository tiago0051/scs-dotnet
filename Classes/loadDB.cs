using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    
        public static void Fornecedor()
        {
            String command = "CREATE TABLE IF NOT EXISTS `Fornecedor` (" +
              "`idFornecedor` INTEGER NOT NULL UNIQUE," +
              "`NomeFornecedor` TEXT NOT NULL," +
              "`NomeContatoFornecedor` TEXT NOT NULL," +
              "`EmailFornedor` TEXT NOT NULL," +
              "`LocalidadeFornecedor` TEXT NOT NULL," +
              "PRIMARY KEY('idFornecedor' AUTOINCREMENT));";

            SqliteCommand cmd = DB.ExecuteCommand(command);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public static void Produtos()
        {
            string command = "CREATE TABLE IF NOT EXISTS `Produto` (" +
                "`idProduto` INTEGER NOT NULL UNIQUE," +
                "`idCodBarProduto` INTEGER NOT NULL," +
                "`DescricaoProduto` TEXT NOT NULL," +
                "`idFornecedor` INTEGER NOT NULL," +
                "`EstoqueProduto` INTEGER NOT NULL," +
                "`EstoqueMinimoProduto` INTEGER NOT NULL," +
                "`ValorCustoProduto` NUMERIC(8,2) NOT NULL," +
                "`ValorVendaVarejoProduto` NUMERIC(8,2) NOT NULL," +
                "`ValorVendaAtacadoProduto` NUMERIC(8,2) NOT NULL," +
                "'QuantidadeMinimaAtacado' INTEGER NOT NULL," +
                "`idCategoria` INTEGER NOT NULL," +
                "`AtivoProduto` TEXT NOT NULL," +
                "`FabricanteProduto` TEXT NOT NULL," +
                "`ModeloProduto` TEXT NOT NULL," +
                "`ObservacaoProduto` TEXT NOT NULL," +
                "`DataCadastroProduto` TEXT NOT NULL," +
                "PRIMARY KEY('idProduto' AUTOINCREMENT));";

            SqliteCommand cmd = DB.ExecuteCommand(command);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }
    }
}
