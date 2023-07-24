using MediaFy.ViewModel;
using MediaFy.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para reproduzir uma playlist.
    /// </summary>
    class StartPlaylist : ICommand
    {
        private MainWindowParent viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel do MainWindowParent.
        /// </summary>
        /// <param name="viewModel">O ViewModel do MainWindowParent a ser associado com o comando.</param>
        public StartPlaylist(MainWindowParent viewModel)
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
        /// <returns>Verdadeiro se houver mais de um arquivo na lista de arquivos (FileInfo) do ViewModel, caso contrário, falso.</returns>
        public bool CanExecute(object parameter)
        {
            return viewModel.FileInfo.Count > 0;
        }

        /// <summary>
        /// Método que é executado quando o comando é acionado.
        /// Abre uma instância do MediaPlayer, a menos que já exista uma janela de MediaPlayer aberta, caso em que utiliza a instância existente.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            MediaPlayer mediaPlayer = null;
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType() == typeof(MediaPlayer))
                {
                    mediaPlayer = (MediaPlayer)w;
                    break;
                }
            }

            if (mediaPlayer == null)
            {
                mediaPlayer = new MediaPlayer();
            }

            mediaPlayer.GetViewModel().SetPlayList(viewModel.FileInfo);
            mediaPlayer.Show();
        }
    }
}
