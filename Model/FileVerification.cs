using System;

namespace MediaFy.Model
{
    /// <summary>
    /// Classe utilitária para verificar o tipo de mídia com base na extensão do arquivo.
    /// </summary>
    public class FileVerification
    {
        // Extensões comuns de áudio
        private static string[] audioExtensions =
        {
            ".wav", ".mp3", ".aac", ".ogg", ".aiff", ".m4a", ".flac", ".wma"
        };

        // Extensões comuns de vídeo
        private static string[] videoExtensions =
        {
            ".mp4", ".avi", ".divx", ".wmv", ".mov", ".mkv", ".webm", ".m4v", ".m4p", ".mpeg", ".mpg", ".m2v", ".flv"
        };

        /// <summary>
        /// Verifica se a extensão do arquivo está presente nas extensões de áudio ou vídeo e retorna o tipo de mídia correspondente.
        /// </summary>
        /// <param name="fileExtension">A extensão do arquivo a ser verificado.</param>
        /// <returns>O tipo de mídia correspondente à extensão do arquivo.</returns>
        public static FileType VerifyFileType(string fileExtension)
        {
            if (Array.IndexOf(audioExtensions, fileExtension.ToLower()) != -1)
            {
                return FileType.Audio;
            }
            else if (Array.IndexOf(videoExtensions, fileExtension.ToLower()) != -1)
            {
                return FileType.Video;
            }
            else
            {
                return FileType.Unknown;
            }
        }
    }
}
