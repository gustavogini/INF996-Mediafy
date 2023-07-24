using MediaFy.ViewModel;
using System;
using System.Windows.Input;

namespace MediaFy.Commands
{
    /// <summary>
    /// Classe que implementa o ICommand para criar um comando personalizado para reproduzir a próxima ou a música anterior.
    /// </summary>
    class NewMusic : ICommand
    {
        private AudioFyViewModel viewModel;

        /// <summary>
        /// Construtor que recebe uma referência ao ViewModel do MediaPlayerViewModel.
        /// </summary>
        /// <param name="viewModel">O ViewModel do MediaPlayerViewModel a ser associado com o comando.</param>
        public NewMusic(AudioFyViewModel viewModel)
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
        /// <param name="parameter">Parâmetro de comando (pode ser "Next" ou "Previous").</param>
        /// <returns>Verdadeiro se o parâmetro for "Next" ou se o modo "Shuffle" estiver desativado (false); caso contrário, falso.</returns>
        public bool CanExecute(object parameter)
        {
            if ("Next" == (string)parameter)
                return true;
            else
                return (viewModel.IsShuffle ? false : true);
        }

        /// <summary>
        /// Método que é executado quando o comando é acionado.
        /// </summary>
        /// <param name="parameter">Parâmetro de comando (pode ser "Next" ou "Previous").</param>
        public void Execute(object parameter)
        {
            if ("Next" == (string)parameter)
            {
                viewModel.PlayNext();
            }
            else
            {
                viewModel.PlayPrevious();
            }
        }
    }
}
