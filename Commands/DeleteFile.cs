using MediaFy.Model;
using MediaFy.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para remover arquivos da lista.
    /// </summary>
    class DeleteFile : ICommand
    {
        private MainWindowParent viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel do MainWindowParent.
        /// </summary>
        /// <param name="viewModel">O ViewModel do MainWindowParent a ser associado com o comando.</param>
        public DeleteFile(MainWindowParent viewModel)
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
        /// <returns>Verdadeiro se houver mais de 0 arquivos selecionados, caso contrário, falso.</returns>
        public bool CanExecute(object parameter)
        {
            return viewModel.SelectedFiles.Count > 0;
        }

        /// <summary>
        /// Método que é executado quando o comando é acionado.
        /// Remove os arquivos selecionados da lista de arquivos no ViewModel do MainWindowParent.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            List<FileInformation> filesToRemove = new List<FileInformation>(viewModel.SelectedFiles);

            string messageString = "Do you want to remove these files from the list?\n";
            string filesString = "";
            foreach (FileInformation fi in filesToRemove)
            {
                filesString += fi.FileName + "\n";
            }

            if (MessageBox.Show(messageString + filesString, "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (FileInformation fi in filesToRemove)
                {
                    viewModel.DeleteFile(fi);
                }
            }
        }
    }
}
