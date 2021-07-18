using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Sistema_Caixa_Seguro.Classes
{
    class Login
    {

        public static void CheckLogin()
        {
            if(!IsClientLoged())
                new Windows.LoginWindow().ShowDialog();
            if (!IsClientLoged())
                Environment.Exit(0);

            if (!NaoVencido())
            {
                MessageBox.Show("Entre em contato para ativar a sua conta. E-mail do suporte: contato@sistemacaixaseguro.com", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0);
            }

            int days = AlarmeVencimentoProximo();
            if (days < 5 && days > 1)
            {
                MessageBox.Show("Faltam " + days + " dias para a sua licensa vencer, por favor entrar em contato para a renovação", "Licensa de uso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (days <= 1)
            {
                MessageBox.Show("Falta " + days + " dia para a sua licensa vencer, por favor entrar em contato para a renovação", "Licensa de uso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static Boolean IsClientLoged()
        {
            String identificacao = Properties.Settings.Default.idetificacaoCliente;

            if (identificacao != "")
            {
                Object[] o = Util.ExecuteMySqlDataReader("SELECT * FROM cliente  inner join servico ON cliente.idServico = servico.idServico WHERE identificacaoCliente='" + identificacao + "'");
                if (o != null && ((MySqlConnection)o[0]).State != System.Data.ConnectionState.Closed && 
                    ((MySqlConnection)o[0]).State != System.Data.ConnectionState.Broken)
                {
                    var reader = (MySqlDataReader)o[1];
                    reader.Read();

                    if (reader.HasRows && reader.GetString("senhaCliente") == Properties.Settings.Default.senhaCliente)
                    {
                        Properties.Settings.Default.idCliente = reader.GetString(0);
                        Properties.Settings.Default.idetificacaoCliente = identificacao;
                        Properties.Settings.Default.nomeCliente = reader.GetString(2);
                        Properties.Settings.Default.telCliente = reader.GetString(3);
                        Properties.Settings.Default.emailCliente = reader.GetString(4);
                        Properties.Settings.Default.nomeEmpresa = reader.GetString(5);
                        Properties.Settings.Default.dataVencimentoCliente = reader.GetDateTime("vencimentoServico");
                        Properties.Settings.Default.Save();

                        return true;
                    }

                    reader.Close();
                    ((MySqlConnection)o[0]).Close();
                }
                else
                {
                    if (NaoVencido())
                        return true;
                }
            }
            return false;
        }

        public static Boolean NaoVencido()
        {
            return Properties.Settings.Default.dataVencimentoCliente >= DateTime.Now;
        }

        public static int AlarmeVencimentoProximo()
        {
            int days = Properties.Settings.Default.dataVencimentoCliente.Subtract(DateTime.Now.Date).Days;
            return days;
        }

        public static Boolean LogarCliente(string usuario, string senha)
        {
            string senhaCrypted = Encrypt(senha);

            Object[] o = Util.ExecuteMySqlDataReader("SELECT * FROM cliente  inner join servico ON cliente.idServico = servico.idServico WHERE identificacaoCliente='" + usuario + "'");
            var reader = (MySqlDataReader)o[1];

            reader.Read();

            if (reader.HasRows && senhaCrypted == reader.GetString("senhaCliente"))
            {
                Properties.Settings.Default.idetificacaoCliente = usuario;
                Properties.Settings.Default.senhaCliente = senhaCrypted;
                Properties.Settings.Default.dataVencimentoCliente = reader.GetDateTime("vencimentoServico");
                Properties.Settings.Default.idCliente = reader.GetString(0);
                Properties.Settings.Default.nomeCliente = reader.GetString(2);
                Properties.Settings.Default.telCliente = reader.GetString(3);
                Properties.Settings.Default.emailCliente = reader.GetString(4);
                Properties.Settings.Default.nomeEmpresa = reader.GetString(5);
                Properties.Settings.Default.Save();

                return true;
            }

            reader.Close();
            ((MySqlConnection)o[0]).Close();

            return false;
        }

        public static string Encrypt(String textToEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(textToEncrypt);
            // Get the key from config file

            string key = "@#$¨&^`^}{asd4435";
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (true)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                //set the secret key for the tripleDES algorithm
                Key = keyArray,
                //mode of operation. there are other 4 modes.
                //We choose ECB(Electronic code Book)
                Mode = CipherMode.ECB,
                //padding mode(if any extra byte added)

                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}
