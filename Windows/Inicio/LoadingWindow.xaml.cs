using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private readonly BackgroundWorker worker = new BackgroundWorker();

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Classes.Login.CheckLogin();
            worker.ReportProgress(10, "Logado com sucesso");

            Classes.DB.Open_db();
            worker.ReportProgress(20, "Abrindo banco de dados");

            Classes.LoadDB.Acessos();
            worker.ReportProgress(30, "Lendo tabela acessos");

            Classes.LoadDB.CategoriaProdutos();
            worker.ReportProgress(40, "Lendo tabela categoria_produtos");

            Classes.LoadDB.CodBarras();
            worker.ReportProgress(50, "Lendo tabela cod_barras");

            Classes.LoadDB.Funcionarios();
            worker.ReportProgress(60, "Lendo tabela funcionarios");


            Classes.LoadDB.Fornecedor();
            worker.ReportProgress(70, "Lendo tabela fornecedores");

            Classes.LoadDB.Produtos();
            worker.ReportProgress(80, "Lendo tabela produtos");
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
            LabelProgress.Content = e.UserState;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;

            worker.RunWorkerAsync();
        }
    }
}
