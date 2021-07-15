using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sistema_Caixa_Seguro.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Logar()
        {
            if (!Classes.Login.LogarCliente(TexBoxUsuario.Text, TextBoxSenha.Password))
            {
                MessageBox.Show("Usuario ou senha incorreto", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else Close();
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Logar();
        }

        private void TextBoxSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Logar();
        }

        private void TexBoxUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Logar();
        }
    }
}
