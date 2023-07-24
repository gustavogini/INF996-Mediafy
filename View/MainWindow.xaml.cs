using MediaFy.Model;
using MediaFy.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaFy
{
    /// <summary>
    /// Lógica de interação para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Inicializa uma nova instância da classe MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        /// <summary>
        /// Obtém uma referência para o ViewModel associado a esta janela.
        /// </summary>
        /// <returns>O ViewModel atual.</returns>
        public MainWindowViewModel GetViewModel()
        {
            return (MainWindowViewModel)DataContext;
        }

        /// <summary>
        ///     Eventos acionados na janela. Chama os métodos/eventos do ViewModel.
        /// </summary>

        // Seleção alterada => adiciona a seleção à coleção observável no ViewModel
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;

            viewModel.SelectedFiles = new ObservableCollection<FileInformation>(filesListView.SelectedItems.Cast<FileInformation>().ToList());
        }

        // Início do arrastar usado para arrastar arquivos para listas de reprodução
        private void MouseDragStart(object sender, MouseButtonEventArgs e)
        {
            GetViewModel().DraggingFiles = true;
        }

        // Parada do arrastar usado para arrastar arquivos para listas de reprodução
        private void MouseDragStop(object sender, MouseButtonEventArgs e)
        {
            GetViewModel().DraggingFiles = false;
        }

        // Executa o comando Exit, que fecha as listas de reprodução e encerra a aplicação de forma segura
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GetViewModel().ExitProgramCommand.Execute(null);
        }
    }
}
