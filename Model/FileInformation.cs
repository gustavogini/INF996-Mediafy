using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.ComponentModel;
using System.IO;

namespace MediaFy.Model
{
    /// <summary>
    /// Classe que representa as informações de um arquivo de mídia.
    /// </summary>
    [Serializable]
    public class FileInformation : INotifyPropertyChanged
    {
        private bool isPlaying = false;

        private FileType mediaType;

        private string fileName;
        private string filePath;
        private string directoryPath;
        private string fileType;

        private long fileSize;
        private string fileSizeString;
        private TimeSpan durationSpan;
        private string durationString;

        /// <summary>
        /// Construtor da classe FileInformation.
        /// </summary>
        /// <param name="info">Objeto FileInfo contendo informações do arquivo de mídia.</param>
        public FileInformation(FileInfo info)
        {
            fileName = info.Name;
            filePath = info.FullName;
            fileSize = info.Length;
            fileType = info.Extension;
            mediaType = FileVerification.VerifyFileType(fileType);

            VerifyFileLength();

            directoryPath = info.DirectoryName.Substring(info.DirectoryName.LastIndexOf('\\'));

            FileSizeToString();
        }

        // Método privado para verificar o comprimento da mídia e salvar em TimeSpan e string formatada.
        private void VerifyFileLength()
        {
            if (mediaType != Model.FileType.Unknown)
            {
                ShellFile sf = ShellFile.FromFilePath(filePath);
                double length = 0; // Comprimento em nanossegundos

                double.TryParse(sf.Properties.System.Media.Duration.Value.ToString(), out length);

                if (length > 0)
                {
                    double durationSeconds = length / 10000000; // Converter para segundos
                    durationSpan = TimeSpan.FromSeconds(durationSeconds);

                    if (durationSpan.Hours == 0)
                        durationString = durationSpan.ToString(@"mm\.ss");
                    else
                        durationString = durationSpan.ToString(@"hh\:mm\.ss");
                }
            }
        }

        // Chama o método estático FileConverter para converter o tamanho do arquivo em uma string formatada.
        private void FileSizeToString()
        {
            fileSizeString = FileConverter.FileSizeToString(fileSize);
        }

        // Propriedades da classe
        public string FileName
        {
            get { return fileName.Substring(0, fileName.Length - fileType.Length); }
        }
        public string FilePath
        {
            get { return filePath; }
        }
        public string DirectoryPath
        {
            get { return directoryPath; }
        }
        public string FileType
        {
            get { return fileType; }
        }
        public long FileSize
        {
            get { return fileSize; }
        }
        public string FileSizeString
        {
            get { return fileSizeString; }
        }
        public FileType MediaType
        {
            get { return mediaType; }
            set { mediaType = value; }
        }
        public string DurationString
        {
            get { return durationString; }
        }
        public TimeSpan DurationSpan
        {
            get { return durationSpan; }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
        }

        #region INotifyPropertyChanged EventHandler and method
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
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
