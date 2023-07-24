using MediaFy.Commands;
using MediaFy.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace MediaFy.ViewModel
{
    /// <summary>
    /// Classe abstrata que atua como ViewModel base para a janela principal do aplicativo.
    /// </summary>
    public abstract class MainWindowParent : INotifyPropertyChanged
    {
        private ObservableCollection<FileInformation> fileInfo;
        private ObservableCollection<FileInformation> selectedFiles;

        private IComparer<FileInformation> previousSort = null;

        /// <summary>
        /// Inicializa uma nova instância da classe MainWindowParent.
        /// </summary>
        public MainWindowParent()
        {
            fileInfo = new ObservableCollection<FileInformation>();
            selectedFiles = new ObservableCollection<FileInformation>();

            Clear = new Clear(this);
            RemoveFilesCommand = new DeleteFile(this);
            SortListViewCommand = new SortView(this);
        }

        // Adiciona arquivos às listas/colecções, implementação base, mas não é utilizada
        public virtual void AddToFileList(FileInfo[] infoArray)
        {
            foreach (FileInfo fi in infoArray)
            {
                if (CheckIfFileInCollection(fi.Name))            // Se o arquivo já estiver na coleção, ignore a adição deste arquivo
                {
                    continue;
                }
                fileInfo.Add(new FileInformation(fi));
            }

            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        // Método para excluir um arquivo
        public void DeleteFile(FileInformation fi)
        {
            for (int i = 0; i < fileInfo.Count; i++)
            {
                if (fileInfo[i].FilePath == fi.FilePath)
                    fileInfo.RemoveAt(i);
            }
            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        // Ordena a FileInfo. Copia para lista, ordena a lista, cria uma nova coleção observável. A coleção Observável não possui funções de classificação
        public void SortFileInfo(IComparer<FileInformation> comparer)
        {
            List<FileInformation> tempList = new List<FileInformation>(FileInfo);

            if (previousSort == null || previousSort.GetType() != comparer.GetType())
            {
                tempList.Sort(comparer);
                previousSort = comparer;
            }
            else
            {
                tempList.Reverse();
            }
            fileInfo = new ObservableCollection<FileInformation>(tempList);
            OnPropertyChanged("FileInfo");
        }

        // Verifica se o arquivo FileInfo já está na coleção FileInfo
        protected bool CheckIfFileInCollection(string fileName)
        {
            foreach (FileInformation fih in FileInfo)
            {
                if (fileName == fih.FileName)
                    return true;
            }
            return false;
        }

        // Propriedades
        public ObservableCollection<FileInformation> FileInfo
        {
            get { return fileInfo; }
        }
        public ObservableCollection<FileInformation> SelectedFiles
        {
            get { return selectedFiles; }
            set
            {
                selectedFiles = value;
                OnPropertyChanged("SelectedFiles");
                OnPropertyChanged("NoOfSelectedFiles");
            }
        }


        #region Propriedades de Contagem de Arquivos na Interface do Usuário
        public int NoOfFiles
        {
            get { return fileInfo.Count; }
        }
        public int NoOfVideoFiles
        {
            get
            {
                int count = 0;

                foreach (FileInformation fi in fileInfo)
                {
                    if (fi.MediaType == FileType.Video)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public int NoOfAudioFiles
        {
            get
            {
                int count = 0;

                foreach (FileInformation fi in fileInfo)
                {
                    if (fi.MediaType == FileType.Audio)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public int NoOfSelectedFiles
        {
            get { return selectedFiles.Count; }
        }
        #endregion

        // Propriedades de comandos
        public ICommand Clear
        {
            get;
            private set;
        }
        public ICommand RemoveFilesCommand
        {
            get;
            private set;
        }
        public ICommand SortListViewCommand
        {
            get;
            private set;
        }

        // INotifyPropertyChanged event handler e método
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
