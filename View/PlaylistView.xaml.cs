using MediaFy.Model;
using MediaFy.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaFy.View
{
    /// <summary>
    /// Lógica de interação para PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : Window
    {
        /// <summary>
        /// Inicializa uma nova instância da classe PlaylistView.
        /// </summary>
        public PlaylistView()
        {
            InitializeComponent();

            DataContext = new PlaylistViewModel();
        }

        /// <summary>
        /// Inicializa uma nova instância da classe PlaylistView com base em um ViewModel existente (para abrir uma lista de reprodução existente).
        /// </summary>
        /// <param name="viewModel">O ViewModel da lista de reprodução existente.</param>
        public PlaylistView(PlaylistViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        /// <summary>
        /// Obtém uma referência para o ViewModel associado a esta janela.
        /// </summary>
        /// <returns>O ViewModel atual.</returns>
        public PlaylistViewModel GetViewModel()
        {
            return (PlaylistViewModel)DataContext;
        }

        /// <summary>
        ///     Eventos acionados na janela. Chama os métodos/eventos do ViewModel.
        /// </summary>

        // Seleção alterada => adiciona a seleção à coleção observável no ViewModel
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaylistViewModel viewModel = (PlaylistViewModel)DataContext;

            viewModel.SelectedFiles = new ObservableCollection<FileInformation>(filesListView.SelectedItems.Cast<FileInformation>().ToList());
        }

        // Eventos de hover e soltar arquivos
        private void MouseEnterForm(object sender, MouseEventArgs e)
        {
            GetViewModel().TriggerHover(true);
        }

        private void MouseLeaveForm(object sender, MouseEventArgs e)
        {
            GetViewModel().TriggerHover(false);
        }

        private void MouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            GetViewModel().TriggerDropFiles();
        }

        // Fechando a janela => Chama ClosePlaylist, que solicita a confirmação de salvamento se a lista de reprodução não estiver vazia.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GetViewModel().ClosePlaylistCommand.Execute(null);
        }
    }
}
