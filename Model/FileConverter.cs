namespace MediaFy.Model
{
    /// <summary>
    /// Classe utilitária para converter o tamanho do arquivo em uma string formatada.
    /// </summary>
    public class FileConverter
    {
        /// <summary>
        /// Converte o tamanho do arquivo (em bytes) em uma string formatada (Bytes, KB, MB, GB).
        /// </summary>
        /// <param name="fileSize">Tamanho do arquivo em bytes.</param>
        /// <returns>Uma string formatada representando o tamanho do arquivo.</returns>
        public static string FileSizeToString(long fileSize)
        {
            string outString = "";
            float workingSize = fileSize;
            string numSize = fileSize.ToString();

            if (numSize.Length <= 3)
            {
                outString = string.Format("{0} {1}", workingSize, "Bytes");
            }
            else if (numSize.Length <= 6)
            {
                outString = string.Format("{0:0.#} {1}", workingSize / 1000, "KB");
            }
            else if (numSize.Length <= 9)
            {
                outString = string.Format("{0:0.##} {1}", workingSize / 1000000, "MB");
            }
            else
            {
                outString = string.Format("{0:0.##} {1}", workingSize / 1000000000, "GB");
            }

            return outString;
        }
    }
}
