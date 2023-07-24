using MediaFy.ViewModel;
using System;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para alternar a reprodução de mídia.
    /// </summary>
    class ChangePlay : ICommand
    {
        private AudioFyViewModel viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel do MediaPlayerViewModel.
        /// </summary>
        /// <param name="viewModel">O ViewModel do MediaPlayerViewModel a ser associado com o comando.</param>
        public ChangePlay(AudioFyViewModel viewModel)
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
        /// Chama o método TogglePlay no ViewModel do MediaPlayerViewModel.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (não utilizado neste caso).</param>
        public void Execute(object parameter)
        {
            viewModel.TogglePlay();
        }
    }
}
