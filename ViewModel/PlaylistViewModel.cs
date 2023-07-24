using MediaFy.Commands;
using MediaFy.Events;
using MediaFy.Model;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MediaFy.ViewModel
{
    /// <summary>
    /// Classe ViewModel para a janela de visualização de playlist do aplicativo MediaFy.
    /// </summary>
    public class PlaylistViewModel : MainWindowParent
    {
        public event EventHandler<Hover> Hover;
        public event EventHandler<Drop> DropFiles;
        private Playlist playlist;
        private string filePath = null;
        
        /// <summary>
        /// Construtor vazio para novas playlists.
        /// </summary>
        public PlaylistViewModel() : base()
        {
            playlist = new Playlist();
            SetupCommands();
        }

        /// <summary>
        /// Construtor com a playlist aberta (de um arquivo) e o caminho do arquivo de onde foi aberta.
        /// </summary>
        public PlaylistViewModel(Playlist playlist, string filePath) : base()
        {
            this.filePath = filePath;
            this.playlist = playlist;

            string tempStr = "";

            foreach (FileInformation fih in playlist.FilesList)
            {
                if (File.Exists(fih.FilePath))   // Verifica se o arquivo existe antes de adicioná-lo à lista de arquivos.
                    FileInfo.Add(fih);
                else
                    tempStr += fih.FileName + "\n";
            }

            if (tempStr != "")
                MessageBox.Show("O Arquivo não foi encontrado:\n" + tempStr, "Não Pode Ser Encontrado");

            SetupCommands();
        }

        /// <summary>
        /// Configura os comandos e OnPropertyChangeds iniciais. Chamado nos construtores.
        /// </summary>
        private void SetupCommands()
        {
            PlayPlaylistCommand = new StartPlaylist(this);
            ClosePlaylistCommand = new ClosePlaylist(this);

            OnPropertyChanged("PlaylistName");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("TotalDuration");
        }

        /// <summary>
        /// Adiciona um arquivo à lista de FileInfo e à playlist através da adição de um objeto FileInformation.
        /// </summary>
        public void AddToFileList(FileInformation fileToAdd)
        {
            if (CheckIfFileInCollection(fileToAdd.FileName)) // As playlists atualmente não suportam múltiplas ocorrências do mesmo arquivo
            {
                foreach (FileInformation fih in FileInfo)
                {
                    if (fileToAdd.FilePath == fih.FilePath)
                        return;
                }
            }

            FileInfo.Add(fileToAdd);
            playlist.AddFile(fileToAdd);
            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("TotalDuration");
        }

        /// <summary>
        /// Chamado pela view para criar a funcionalidade de arrastar e soltar.
        /// </summary>
        public void TriggerHover(bool isHovering)
        {
            Hover hoverEvent = new Hover(isHovering);
            Hover(this, hoverEvent);
        }

        /// <summary>
        /// Chamado pelo view para executar a funcionalidade de soltar arquivos arrastados.
        /// </summary>
        public void TriggerDropFiles()
        {
            DropFiles(this, new Drop());
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>
        /// Obtém a duração total para FileInfo e formata em TimeSpan.
        /// </summary>
        public string TotalDuration
        {
            get 
            {
                TimeSpan totalDuration = new TimeSpan();
                foreach (FileInformation fih in FileInfo)
                {
                    totalDuration += fih.DurationSpan;
                }
                return "Playlist Duration: " + totalDuration.ToString(@"hh\:mm\:ss");
            }
        }

        /// <summary>
        /// Obtém ou define o nome da playlist vinculado ao TextBox na janela.
        /// </summary>
        public string PlaylistName
        {
            get { return playlist.Name; ; }
            set 
            { 
                playlist.Name = value;
                OnPropertyChanged("PlaylistName");
            }
        }

        /// <summary>
        /// Obtém ou define o caminho do arquivo da playlist.
        /// </summary>
        public string FilePath
        {
            set { filePath = value; }
            get { return filePath; }
        }

        /// <summary>
        /// Obtém a playlist.
        /// </summary>
        public Playlist Playlist
        {
            get { return playlist; }
        }

        /// <summary>
        /// Comandos.
        /// </summary>
        public ICommand PlayPlaylistCommand
        {
            get;
            private set;
        }

        public ICommand ClosePlaylistCommand
        {
            get;
            private set;
        }
    }
}
