using MediaFy.Commands;
using MediaFy.Events;
using MediaFy.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MediaFy.ViewModel
{
    /// <summary>
    /// ViewModel para o reprodutor de áudio e vídeo.
    /// </summary>
    public class AudioFyViewModel : MainWindowParent
    {
        private Uri audioSource;
        private Uri videoSource;
        private bool isPlaying = true;
        private int playIndex = 0;
        private bool isShuffle = false;
        private string totalDurationString = "";

        /// <summary>
        /// Evento acionado ao alternar entre reproduzir e pausar.
        /// </summary>
        public EventHandler<PlayPause> TogglePlayEvent;

        /// <summary>
        /// Evento acionado ao trocar entre áudio e vídeo.
        /// </summary>
        public EventHandler<VideoFyEvent> VideoPlayerEvent;

        /// <summary>
        /// Inicializa uma nova instância da classe AudioFyViewModel.
        /// </summary>
        public AudioFyViewModel() : base()
        {
            TogglePlayCommand = new ChangePlay(this);
            NewSongCommand = new NewMusic(this);
        }

        // Toggle play on and off
        /// <summary>
        /// Alterna entre reprodução e pausa.
        /// </summary>
        public void TogglePlay()
        {
            isPlaying = !isPlaying;

            TogglePlayEvent.Invoke(this, new PlayPause(isPlaying));

            OnPropertyChanged("PlayText");
            UpdateTotalDuration();
        }

        /// <summary>
        /// Reproduz a próxima música na lista de reprodução.
        /// </summary>
        public void PlayNext()
        {
            if (playIndex == FileInfo.Count - 1 && !isShuffle)
                return;

            FileInfo[playIndex].IsPlaying = false;

            if (isShuffle)
            {
                Random rnd = new Random();
                int previousIndex = playIndex;

                while (previousIndex == playIndex)
                {
                    playIndex = rnd.Next(0, FileInfo.Count - 1);
                }
            }
            else
                playIndex++;

            SetSource();

            FileInfo[playIndex].IsPlaying = true;

            OnPropertyChanged("PlayIndex");
            //NewSongEvent.Invoke(this, new Events.NewSongEvent(new Uri(FileInfo[playIndex].FilePath)));
        }

        /// <summary>
        /// Reproduz a música anterior na lista de reprodução.
        /// </summary>
        public void PlayPrevious()
        {
            if (playIndex <= 0)
                return;

            FileInfo[playIndex].IsPlaying = false;

            playIndex--;
            SetSource();

            FileInfo[playIndex].IsPlaying = true;

            OnPropertyChanged("PlayIndex");
            //NewSongEvent.Invoke(this, new Events.NewSongEvent(new Uri(FileInfo[playIndex].FilePath)));
        }

        /// <summary>
        /// Define um novo índice de reprodução (por meio de entrada do usuário).
        /// </summary>
        /// <param name="index">O novo índice de reprodução.</param>
        public void SetPlayIndex(int index)
        {
            if (index < 0 || index > FileInfo.Count - 1)            // Guard clause para o índice
                return;

            FileInfo[playIndex].IsPlaying = false;

            playIndex = index;

            SetSource();

            FileInfo[playIndex].IsPlaying = true;

            OnPropertyChanged("PlayIndex");
        }

        /// <summary>
        /// Define a lista de reprodução do reprodutor de mídia.
        /// </summary>
        /// <param name="fileInfo">A coleção de informações de arquivos para a lista de reprodução.</param>
        public void SetPlayList(ObservableCollection<FileInformation> fileInfo)
        {
            FileInfo.Clear();

            foreach (FileInformation fih in fileInfo)
            {
                fih.IsPlaying = false;
                this.FileInfo.Add(fih);
            }
            playIndex = 0;

            SetSource();

            OnPropertyChanged("PlayIndex");
            FileInfo[playIndex].IsPlaying = true;

            UpdateTotalDuration();
        }

        /// <summary>
        /// Atualiza a duração total para a lista de arquivos.
        /// </summary>
        private void UpdateTotalDuration()
        {
            TimeSpan totalDuration = new TimeSpan();
            foreach (FileInformation fih in FileInfo)
            {
                totalDuration += TimeSpan.FromSeconds(fih.DurationSpan.TotalSeconds);
            }

            totalDurationString = totalDuration.ToString(@"hh\:mm\.ss");

            OnPropertyChanged("TotalDuration");
        }

        /// <summary>
        /// Define a origem da mídia (áudio ou vídeo) com base no arquivo atualmente reproduzido.
        /// </summary>
        private void SetSource()
        {
            if (FileInfo[playIndex].MediaType == FileType.Audio)
            {
                VideoSource = null;
                AudioSource = new Uri(FileInfo[playIndex].FilePath);
                VideoPlayerEvent.Invoke(this, new Events.VideoFyEvent(false));
            }
            else if (FileInfo[playIndex].MediaType == FileType.Video)
            {
                AudioSource = null;
                VideoSource = new Uri(FileInfo[playIndex].FilePath);
                VideoPlayerEvent.Invoke(this, new Events.VideoFyEvent(true));
            }
            OnPropertyChanged("TotalDuration");
        }

        /// <summary>
        /// Limpa a lista de reprodução.
        /// </summary>
        public void ClearPlaylist()
        {
            // Implementação futura
        }

        // Get total duration for FileInfo
        /// <summary>
        /// Obtém a duração total da lista de reprodução.
        /// </summary>
        public string TotalDuration
        {
            get
            {
                return "Tempo Total: " + totalDurationString;
            }
        }

        /// <summary>
        /// Obtém um valor que indica se a mídia está sendo reproduzida.
        /// </summary>
        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        /// <summary>
        /// Obtém o índice atual de reprodução na lista de reprodução.
        /// </summary>
        public int PlayIndex
        {
            get { return playIndex; }
        }

        /// <summary>
        /// Obtém ou define a origem do áudio.
        /// </summary>
        public Uri AudioSource
        {
            get { return audioSource; }
            set
            {
                audioSource = value;
                OnPropertyChanged("Fonte do Áudio");
            }
        }

        /// <summary>
        /// Obtém ou define a origem do vídeo.
        /// </summary>
        public Uri VideoSource
        {
            get { return videoSource; }
            set
            {
                videoSource = value;
                OnPropertyChanged("Fonte do Vídeo");
            }
        }

        /// <summary>
        /// Obtém ou define um valor que indica se a reprodução está em modo aleatório (shuffle).
        /// </summary>
        public bool IsShuffle
        {
            get { return isShuffle; }
            set
            {
                isShuffle = value;
                OnPropertyChanged("IsShuffle");
            }
        }

               /// <summary>
        /// Obtém o texto exibido no botão de reprodução (Play/Pause).
        /// </summary>
        public string PlayText
        {
            get
            {
                return (isPlaying ? "Pause" : "Play");
            }
        }

        /// <summary>
        /// Comando para alternar entre reproduzir e pausar.
        /// </summary>
        public ICommand TogglePlayCommand { get; private set; }

        /// <summary>
        /// Comando para adicionar uma nova música à lista de reprodução.
        /// </summary>
        public ICommand NewSongCommand { get; private set; }
    }
}

