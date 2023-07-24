using System;
using System.Collections.Generic;

namespace MediaFy.Model
{
    /// <summary>
    /// Classe utilizada para armazenar playlists.
    /// </summary>
    [Serializable]
    public class Playlist
    {
        private List<FileInformation> fileList;
        private string name = "";

        /// <summary>
        /// Construtor padrão que inicializa a lista de arquivos da playlist.
        /// </summary>
        public Playlist()
        {
            fileList = new List<FileInformation>();
        }

        /// <summary>
        /// Adiciona um objeto FileInformation à playlist.
        /// </summary>
        /// <param name="newFile">O FileInformation a ser adicionado à playlist.</param>
        public void AddFile(FileInformation newFile)
        {
            if (newFile != null)
            {
                fileList.Add(newFile);
            }
        }

        /// <summary>
        /// Remove um objeto FileInformation da playlist.
        /// </summary>
        /// <param name="removeFile">O FileInformation a ser removido da playlist.</param>
        public void RemoveFile(FileInformation removeFile)
        {
            for (int i = 0; i < fileList.Count; i++)
            {
                if (fileList[i].FilePath == removeFile.FilePath)
                {
                    fileList.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Obtém ou define o nome da playlist.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Obtém a lista de FileInformation da playlist.
        /// </summary>
        public List<FileInformation> FilesList
        {
            get { return fileList; }
        }
    }
}
