using MediaFy.Events;
using MediaFy.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaFy.View
{
    /// <summary>
    /// Janela de reprodução de mídia.
    /// </summary>
    public partial class MediaPlayer : Window
    {
        private bool isPlayingVideo = false;
        private VideoPlayer openVideoPlayer = null;

        /// <summary>
        /// Inicializa a janela e configura os inscritos aos eventos.
        /// </summary>
        public MediaPlayer()
        {
            InitializeComponent();

            // Cria e configura o ViewModel para o player de áudio.
            AudioFyViewModel mediaPlayerViewModel = new AudioFyViewModel();
            DataContext = mediaPlayerViewModel;

            // Inscreve-se nos eventos do ViewModel.
            mediaPlayerViewModel.TogglePlayEvent += TogglePlay;
            mediaPlayerViewModel.VideoPlayerEvent += SwitchWindow;

            // Inicia a reprodução de mídia.
            mediaElement.Play();
        }

        /// <summary>
        /// Obtém uma referência para o ViewModel associado a esta janela.
        /// </summary>
        /// <returns>O ViewModel atual.</returns>
        public AudioFyViewModel GetViewModel()
        {
            return (AudioFyViewModel)DataContext;
        }

        /// <summary>
        /// Alterna entre reprodução de vídeo e áudio. A reprodução de vídeo abre uma nova janela (VideoPlayerView).
        /// </summary>
        /// <param name="sender">O objeto que acionou o evento.</param>
        /// <param name="e">Os dados do evento que contêm informações sobre a mídia a ser reproduzida.</param>
        private void SwitchWindow(object sender, VideoFyEvent e)
        {
            if (e.IsVideo)
            {
                if (openVideoPlayer != null)     // Fecha o openVideoPlayer se ele já existe. Não há tempo para implementar uma troca no momento.
                {
                    openVideoPlayer.Close();
                }

                this.Topmost = false;
                mediaElement.Pause();
                isPlayingVideo = true;

                // Abre uma nova janela de reprodução de vídeo.
                openVideoPlayer = new VideoPlayer(GetViewModel());
                openVideoPlayer.Show();
            }
            else
            {
                this.Topmost = true;
                isPlayingVideo = false;

                // Fecha a janela de reprodução de vídeo, se existir.
                openVideoPlayer?.Close();
                mediaElement.Play();
            }
        }

        /// <summary>
        /// Alterna entre reprodução e pausa com base nos dados do evento.
        /// </summary>
        /// <param name="sender">O objeto que acionou o evento.</param>
        /// <param name="e">Os dados do evento que contêm informações sobre o comando de reprodução.</param>
        private void TogglePlay(object sender, PlayPause e)
        {
            if (isPlayingVideo)
                return;

            if (e.IsPlaying == true)
                mediaElement.Play();
            else
                mediaElement.Pause();
        }

        /// <summary>
        /// Eventos acionados na janela.
        /// </summary>

        // Clicar duas vezes em um item da ListView inicia a reprodução.
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = filesListView.Items.IndexOf(((ListViewItem)sender).Content);
            GetViewModel().SetPlayIndex(index);
        }

        // Altera o volume da mídia com base no valor do slider.
        private void Volume_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
                mediaElement.Volume = e.NewValue;
        }

        // Se a mídia terminar, reproduz a próxima mídia na lista.
        private void Media_Ended(object sender, RoutedEventArgs e)
        {
            ((AudioFyViewModel)DataContext).PlayNext();
        }

        // Se a janela for fechada, a mídia será interrompida.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mediaElement.Stop();
        }
    }
}
