using MediaFy.Model;
using MediaFy.ViewModel;
using Ookii.Dialogs.Wpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para fechar a playlist.
    /// </summary>
    class ClosePlaylist : ICommand
    {
        private PlaylistViewModel viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel da playlist (PlaylistViewModel).
        /// </summary>
        /// <param name="viewModel">O ViewModel da playlist (PlaylistViewModel) a ser associado com o comando.</param>
        public ClosePlaylist(PlaylistViewModel viewModel)
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
        /// Solicita ao usuário para salvar a playlist e, em seguida, salva o arquivo da playlist.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            // Verifica se há arquivos na playlist antes de prosseguir.
            if (viewModel.Playlist.FilesList.Count <= 0)
                return;

            try
            {
                // Pergunta ao usuário se ele deseja salvar as alterações na playlist antes de fechá-la.
                // Se não houver caminho de arquivo (filePath) definido no ViewModel, isso indica que é uma nova playlist.
                if (viewModel.FilePath == null)
                {
                    if (MessageBox.Show("Do you want to save the playlist \"" + viewModel.PlaylistName + "\"?", "Save Playlist?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                }
                else
                {
                    if (MessageBox.Show("Do you want to save the changes to \"" + viewModel.PlaylistName + "\"?", "Save Playlist?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                }

                // Se o caminho do arquivo (filePath) ainda não foi definido, pede ao usuário que escolha onde salvar a playlist.
                if (viewModel.FilePath == null)
                {
                    VistaSaveFileDialog saveDialog = new VistaSaveFileDialog();
                    saveDialog.Filter = "Playlist files (*.playlist)|*.playlist";
                    saveDialog.DefaultExt = ".playlist";

                    // Mostra a janela de diálogo para salvar o arquivo da playlist.
                    if (saveDialog.ShowDialog() == true)
                        viewModel.FilePath = saveDialog.FileName;
                    else
                        return; // Se o usuário cancelar a ação de salvar, retorna sem fazer nada.
                }

                // Adiciona a extensão ".playlist" se o caminho do arquivo (filePath) não tiver essa extensão.
                if (!viewModel.FilePath.EndsWith(".playlist"))
                {
                    viewModel.FilePath += ".playlist";
                }

                // Salva a playlist no arquivo especificado.
                Save.SaveBinary<Playlist>(viewModel.FilePath, viewModel.Playlist);
            }
            catch (Exception ex)
            {
                // Em caso de erro, mostra uma mensagem de erro.
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
    }
}
