using MediaFy.Commands;
using MediaFy.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using MediaFy.Events;

namespace MediaFy.ViewModel
{
    /// <summary>
    /// Classe ViewModel para a janela principal do aplicativo MediaFy.
    /// </summary>
    public class MainWindowViewModel : MainWindowParent
    {
        private ObservableCollection<PlaylistViewModel> openPlaylists;
        private bool isDraggingFiles = false;

        /// <summary>
        /// Inicializa uma nova instância da classe MainWindowViewModel.
        /// </summary>
        public MainWindowViewModel() : base()
        {
            openPlaylists = new ObservableCollection<PlaylistViewModel>();

            OpenFilesCommand = new OpenFile(this);
            OpenFolderCommand = new OpenFolder(this);
            NewPlaylistCommand = new NewPlaylist(this);
            OpenPlaylistCommand = new OpenPlaylist(this);
            ExitProgramCommand = new Exit(this);
        }

        //Adiciona arquivos às listas/colecções, sobrescreve a implementação base para verificar os MediaTypes dos arquivos e perguntar ao usuário se eles querem adicioná-los
        public override void AddToFileList(FileInfo[] infoArray)
        {
            foreach (FileInfo fi in infoArray)
            {
                if (base.CheckIfFileInCollection(fi.Name))
                    return;

                if (FileVerification.VerifyFileType(fi.Extension) != FileType.Unknown ||
                    MessageBox.Show("Unknown File Type Found.\nDo you still want to add it to the list?\nThese files cannot be added to playlists.", "Unknown File Type", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    FileInfo.Add(new FileInformation(fi));
                }
            }

            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        //Abre uma view de playlist com base em um ViewModel já criado
        public void OpenPlaylistView(PlaylistViewModel viewModel)
        {
            openPlaylists.Add(viewModel);
            openPlaylists[openPlaylists.Count - 1].Hover += HoverPlaylist;

            foreach (FileInformation fih in viewModel.FileInfo)
            {
                bool alreadyInMediaManager = false;
                foreach (FileInformation fih2 in FileInfo)
                {
                    if (fih2.FilePath == fih.FilePath)
                    {
                        alreadyInMediaManager = true;
                        break;
                    }
                }
                if (!alreadyInMediaManager)
                {
                    FileInfo.Add(fih);
                    OnPropertyChanged("FileInfo");
                }
            }

            //Se for uma nova playlist (ou seja, sem arquivos em FileInfo), adiciona os arquivos marcados no media manager ao abri-la
            if (viewModel.FileInfo.Count == 0)
            {
                foreach (FileInformation fih in SelectedFiles)
                {
                    viewModel.AddToFileList(fih);
                }
            }

            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        //Assinante do evento HoverPlaylist.
        private void HoverPlaylist(object sender, Hover e)
        {
            if (!e.IsHovering)
            {
                ((PlaylistViewModel)sender).DropFiles -= DropFiles;

            }
            else
            {
                ((PlaylistViewModel)sender).DropFiles += DropFiles;

            }
        }

        //Chamado quando ocorre o evento DropFiles do PlaylistViewModel.
        private void DropFiles(object sender, Drop e)
        {
            if (isDraggingFiles)
            {
                string unknownFileTypeNames = "";

                foreach (FileInformation fih in SelectedFiles)
                {
                    if (fih.MediaType == FileType.Unknown)
                        unknownFileTypeNames += fih.FileName + "\n";
                    else
                        ((PlaylistViewModel)sender).AddToFileList(fih);
                }
                isDraggingFiles = false;
                Clear.Execute(null);

                //Guard clause se não houver arquivos que não puderam ser adicionados (devido ao tipo de arquivo não ser nem áudio nem vídeo)
                if (unknownFileTypeNames != "")
                {
                    MessageBox.Show("These files of unknown filetype were not added:\n" + unknownFileTypeNames, "Files Not Added");
                }
            }
        }

        //Define a propriedade DraggingFiles (realizada pela View)
        public bool DraggingFiles
        {
            set
            {
                isDraggingFiles = value;
                if (isDraggingFiles)
                    Mouse.OverrideCursor = Cursors.Hand;
                else
                    Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Propriedades
        /// </summary>
        // Propriedades de comandos
        public ICommand OpenFilesCommand
        {
            get;
            private set;
        }
        public ICommand OpenFolderCommand
        {
            get;
            private set;
        }
        public ICommand NewPlaylistCommand
        {
            get;
            private set;
        }
        public ICommand OpenPlaylistCommand
        {
            get;
            private set;
        }
        public ICommand ExitProgramCommand
        {
            get;
            private set;
        }
        public ICommand OpenHelpWindowCommand
        {
            get;
            private set;
        }
    }
}
