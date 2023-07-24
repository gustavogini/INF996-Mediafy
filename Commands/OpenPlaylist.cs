using MediaFy.ViewModel;
using MediaFy.View;
using System;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using MediaFy.Model;
using System.Windows;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para abrir uma playlist.
    /// </summary>
    class OpenPlaylist : ICommand
    {
        private MainWindowViewModel viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel da janela principal (MainWindowViewModel).
        /// </summary>
        /// <param name="viewModel">O ViewModel da janela principal (MainWindowViewModel) a ser associado com o comando.</param>
        public OpenPlaylist(MainWindowViewModel viewModel)
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
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        /// <returns>Verdadeiro.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Método que é executado quando o comando é acionado.
        /// Abre uma janela de diálogo para selecionar uma playlist (.playlist) e carrega a playlist utilizando a classe LoadPersistent.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            try
            {
                VistaOpenFileDialog openDialog = new VistaOpenFileDialog();
                openDialog.Filter = "playlist files (*.playlist)|*.playlist";
                openDialog.Multiselect = false;

                if (openDialog.ShowDialog() != true)
                {
                    return;
                }

                Playlist playlist = Load.LoadBinary<Playlist>(openDialog.FileName);

                PlaylistView view = new PlaylistView(new PlaylistViewModel(playlist, openDialog.FileName));
                viewModel.OpenPlaylistView(view.GetViewModel());
                view.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
    }
}
