using MediaFy.Events;
using MediaFy.ViewModel;
using System;
using System.Windows;

namespace MediaFy.View
{
    /// <summary>
    /// Lógica de interação para VideoPlayer.xaml. Usa o mesmo ViewModel que MediaPlayer.xaml.
    /// </summary>
    public partial class VideoPlayer : Window
    {
        private bool isVideoPlaying = true;

        /// <summary>
        /// Inicializa uma nova instância da classe VideoPlayer.
        /// </summary>
        /// <param name="viewModel">O ViewModel associado ao reprodutor de áudio.</param>
        public VideoPlayer(AudioFyViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.TogglePlayEvent += TogglePlay;

            mediaElement.Play();
        }

        /// <summary>
        /// Método de alternância de reprodução, assinante do evento TogglePlay do reprodutor de mídia.
        /// </summary>
        /// <param name="sender">O objeto que acionou o evento.</param>
        /// <param name="e">Os dados do evento que contêm informações sobre o comando de reprodução.</param>
        private void TogglePlay(object sender, PlayPause e)
        {
            if (!isVideoPlaying)
                return;

            if (e.IsPlaying == true)
                mediaElement.Play();
            else
                mediaElement.Pause();
        }

        /// <summary>
        ///     Eventos acionados na janela.
        /// </summary>

        // VolumeChanged segue o slider na janela.
        private void Volume_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
                mediaElement.Volume = e.NewValue;
        }

        // Quando a mídia termina, reproduz a próxima.
        private void Media_Ended(object sender, RoutedEventArgs e)
        {
            ((AudioFyViewModel)DataContext).PlayNext();
        }

        // Se a janela for fechada, pausa a mídia e remove o evento TogglePlay do ViewModel.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isVideoPlaying = false;
            mediaElement.Pause();
            ((AudioFyViewModel)DataContext).TogglePlayEvent -= TogglePlay;
        }

        // Se a janela for carregada, inicia a reprodução da mídia imediatamente.
        private void Window_Loaded(object sender, EventArgs e)
        {
            isVideoPlaying = true;
            mediaElement.Play();
        }
    }
}
