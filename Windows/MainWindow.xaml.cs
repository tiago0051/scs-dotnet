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

namespace Sistema_Caixa_Seguro.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProject()
        {
            this.Hide();
            new LoadingWindow().ShowDialog();
            this.Show();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            StartProject();
        }
    }
}
