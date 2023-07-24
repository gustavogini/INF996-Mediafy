using MediaFy.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado.
    /// </summary>
    class Clear : ICommand
    {
        private MainWindowParent viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel (MainWindowParent).
        /// </summary>
        /// <param name="viewModel">O ViewModel (MainWindowParent) a ser associado com o comando.</param>
        public Clear(MainWindowParent viewModel)
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
        /// <returns>Verdadeiro se houver mais de um arquivo selecionado no ViewModel, caso contrário, falso.</returns>
        public bool CanExecute(object parameter)
        {
            return viewModel.SelectedFiles.Count > 0;
        }

        /// <summary>
        /// Método que é executado quando o comando é acionado.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            viewModel.SelectedFiles.Clear();
            // Obtém a instância atual da janela principal (MainWindow).
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            // Limpa a seleção de itens na ListView (filesListView) da MainWindow.
            window.filesListView.SelectedItems.Clear();
        }
    }
}
