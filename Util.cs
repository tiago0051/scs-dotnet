using MySql.Data.MySqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Caixa_Seguro
{
    class Util
    {
        public static Classes.Models.Funcionario FuncionarioLogado = null;

        private static readonly String keySendGrid = "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";

        public static bool CheckFuncionarioIsLoged()
        {
            return FuncionarioLogado != null;
        }

        private static MySqlConnection OpenConnectionMySql()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server=54.232.127.87;Database=sql_SCS;Uid=sql_SCS;Pwd=iMERtwsasM3XaxLM;");
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                ReportError(ex.Message);
                return null;
            }
        }

        public static Object[] ExecuteMySqlDataReader(string command)
        {
            try
            {
                MySqlConnection connection = OpenConnectionMySql();

                if(connection != null)
                {
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = command;

                    var reader = cmd.ExecuteReader();

                    return new Object[] { connection, reader };
                }
                else
                {

                }return null;
            }
            catch (Exception ex)
            {
                ReportError(ex.Message);
                return null;
            }
        }

        public static void ExecuteNonQuery(string command)
        {
            try
            {
                MySqlConnection connection = OpenConnectionMySql();

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = command;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ReportError(ex.Message);
            }
        }

        public static void ReportError(String error)
        {
            var client = new SendGridClient(keySendGrid);
            var from = new EmailAddress("errors@sistemacaixaseguro.com", "Usuario de erros");
            var subject = "Erro na aplicação desktop maxcred";
            var to = new EmailAddress("tiago.salgado@devsoftbr.com", "Tiago Salgado");
            var htmlView = Properties.Settings.Default.idCliente + "<br/>" + error;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "a", htmlView);
            client.SendEmailAsync(msg);
        }
    }
}
