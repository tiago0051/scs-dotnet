using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sistema_Caixa_Seguro.Windows.Funcionario
{
    /// <summary>
    /// Interaction logic for RegisterFuncionarioWindow.xaml
    /// </summary>
    public partial class RegisterFuncionarioWindow : Window
    {
        public RegisterFuncionarioWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqliteCommand command = Classes.DB.ExecuteCommand("SELECT * FROM TipoAcesso");

            command.Connection.Open();

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ComboBoxAcessos.Items.Add(reader.GetInt16(0) + " - " + reader.GetString(1));
            }

            reader.Close();
            command.Connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(ComboBoxAcessos.Text.Substring(0, ComboBoxAcessos.Text.IndexOf("-")), out int result) && TextBoxUsuario.Text != "" && TextBoxSenha.Password != "")
            {
                new Classes.Models.Funcionario() { Nome = TextBoxUsuario.Text, IdTipoAcesso = result }.RegisterFuncionario(TextBoxSenha.Password);
                MessageBox.Show("Registrado com sucesso", "Registrar funcionario", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Preencha as informações corretamente!", "Registrar funcionario", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
