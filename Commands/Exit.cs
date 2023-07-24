using MediaFy.ViewModel;
using MediaFy.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para sair do programa.
    /// </summary>
    class Exit : ICommand
    {
        private MainWindowParent viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel do MainWindowParent.
        /// </summary>
        /// <param name="viewModel">O ViewModel do MainWindowParent a ser associado com o comando.</param>
        public Exit(MainWindowParent viewModel)
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
        /// O método CanExecute é chamado quando o usuário fecha a janela.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        /// <returns>Verdadeiro.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Método que é executado quando o comando é acionado.
        /// Fecha todas as instâncias de PlaylistView com segurança e, em seguida, encerra o aplicativo.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            // Cria uma lista para armazenar as instâncias da janela PlaylistView que precisam ser fechadas.
            List<PlaylistView> playlistViews = new List<PlaylistView>();

            // Itera sobre todas as janelas abertas na aplicação atual.
            foreach (Window w in Application.Current.Windows)
            {
                // Verifica se a janela é do tipo PlaylistView.
                if (w is PlaylistView playlistView)
                    playlistViews.Add(playlistView); // Adiciona a janela à lista para fechá-la posteriormente.
            }

            // Fecha cada instância da janela PlaylistView na lista.
            foreach (PlaylistView plv in playlistViews)
            {
                plv.Close();
            }

            // Encerra o aplicativo.
            Application.Current.Shutdown();
        }

        // Método auxiliar que verifica se uma janela é do tipo PlaylistView.
        // Esse método não é utilizado no código atual e pode ser removido.
        private bool IsWindowPlaylist(Window w)
        {
            try
            {
                PlaylistView plv = (PlaylistView)w;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
