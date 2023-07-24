using MediaFy.ViewModel;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para abrir arquivos.
    /// </summary>
    class OpenFile : ICommand
    {
        private MainWindowViewModel mediaManagerViewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel da janela principal (MainWindowViewModel).
        /// </summary>
        /// <param name="viewModel">O ViewModel da janela principal (MainWindowViewModel) a ser associado com o comando.</param>
        public OpenFile(MainWindowViewModel viewModel)
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
        /// Abre uma janela de diálogo para selecionar múltiplos arquivos e adiciona-os à lista de arquivos no ViewModel da janela principal.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            try
            {
                VistaOpenFileDialog dialog = new VistaOpenFileDialog();
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == true)
                {
                    if (dialog.FileNames.Length > 0)
                    {
                        FileInfo[] infoArray = new FileInfo[dialog.FileNames.Length];
                        for (int i = 0; i < infoArray.Length; i++)
                        {
                            infoArray[i] = new FileInfo(dialog.FileNames[i]);
                        }
                        mediaManagerViewModel.AddToFileList(infoArray);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open files.", "Error");
            }
        }
    }
}
