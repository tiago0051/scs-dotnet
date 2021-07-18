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
    /// Interaction logic for LoginFuncionarioWindow.xaml
    /// </summary>
    public partial class LoginFuncionarioWindow : Window
    {
        public LoginFuncionarioWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Classes.LoginFuncionario.IsFirstLogin())
            {
                this.Hide();
                new RegisterFuncionarioWindow().ShowDialog();
                this.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Util.CheckFuncionarioIsLoged())
                Environment.Exit(0);
        }
    }
}
