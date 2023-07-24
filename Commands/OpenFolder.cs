using System;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using System.Windows;
using MediaFy.ViewModel;
using System.IO;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para abrir uma pasta.
    /// </summary>
    class OpenFolder : ICommand
    {
        private MainWindowViewModel mediaManagerViewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel da janela principal (MainWindowViewModel).
        /// </summary>
        /// <param name="viewModel">O ViewModel da janela principal (MainWindowViewModel) a ser associado com o comando.</param>
        public OpenFolder(MainWindowViewModel viewModel)
        {
            mediaManagerViewModel = viewModel;
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
        /// Abre uma janela de diálogo para selecionar uma pasta e adiciona todos os arquivos dessa pasta à lista de arquivos no ViewModel da janela principal.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            try
            {
                VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog() == true)
                {
                    if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(dialog.SelectedPath);
                        FileInfo[] infoArray = di.GetFiles("*.*");
                        mediaManagerViewModel.AddToFileList(infoArray);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }
    }
}
