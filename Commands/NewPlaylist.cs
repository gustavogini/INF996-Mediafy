using MediaFy.ViewModel;
using MediaFy.View;
using System;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para criar uma nova playlist.
    /// </summary>
    class NewPlaylist : ICommand
    {
        private MainWindowViewModel viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel da janela principal (MainWindowViewModel).
        /// </summary>
        /// <param name="viewModel">O ViewModel da janela principal (MainWindowViewModel) a ser associado com o comando.</param>
        public NewPlaylist(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// Evento que é disparado quando o resultado de CanExecuteChanged é alterado.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Método que determina se o comando pode ser executado no estado atual.
        /// Sempre retorna verdadeiro, pois este comando pode ser executado em qualquer situação.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        /// <returns>Verdadeiro.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Método que é executado quando o comando é acionado.
        /// Cria uma nova janela PlaylistView, obtém o ViewModel da janela e a abre através do MediaManager.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            PlaylistView view = new PlaylistView();
            viewModel.OpenPlaylistView(view.GetViewModel());
            view.Show();
        }
    }
}
