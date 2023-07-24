using MediaFy.Model;
using MediaFy.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para ordenar a lista na ListView.
    /// </summary>
    class SortView : ICommand
    {
        private MainWindowParent viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel do MainWindowParent.
        /// </summary>
        /// <param name="viewModel">O ViewModel do MainWindowParent a ser associado com o comando.</param>
        public SortView(MainWindowParent viewModel)
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
        /// Obtém o parâmetro do cabeçalho da ListView que foi clicado e ordena a lista no ViewModel de acordo com o IComparer apropriado usando um switch.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (cabeçalho da ListView que foi clicado).</param>
        public void Execute(object parameter)
        {
            string sortType = parameter.ToString();

            IComparer<FileInformation> comparer = null;

            switch (sortType)
            {
                case "Media":
                    comparer = new SortByMediaType();
                    break;
                case "Name":
                    comparer = new SortByName();
                    break;
                case "Duration":
                    comparer = new SortBySpan();
                    break;
                case "FileType":
                    comparer = new SortByType();
                    break;
                default:
                    comparer = new SortByName();
                    break;
            }

            viewModel.SortFileInfo(comparer);
        }
    }
}
