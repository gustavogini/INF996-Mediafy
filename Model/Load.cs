using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace MediaFy.Model
{
    /// <summary>
    /// Classe de utilidade para carregar dados persistentes de arquivos.
    /// </summary>
    public class Load
    {
        /// <summary>
        /// Carrega um arquivo de texto e retorna seu conteúdo como um array de strings.
        /// </summary>
        /// <param name="filePath">O caminho do arquivo de texto a ser carregado.</param>
        /// <returns>Um array de strings contendo as linhas do arquivo de texto.</returns>
        public static string[] LoadTextFile(string filePath)
        {
            string[] outStringArray;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    List<string> stringList = new List<string>();
                    string line;
                    while ((line = sr.ReadLine()) != null) // Lê as linhas do arquivo de texto e adiciona à lista até o final do arquivo.
                    {
                        stringList.Add(line);
                    }
                    outStringArray = stringList.ToArray();
                }
            }
            return outStringArray;
        }

        /// <summary>
        /// Carrega um arquivo binário e retorna os dados desserializados no tipo especificado.
        /// </summary>
        /// <typeparam name="T">O tipo de objeto a ser desserializado.</typeparam>
        /// <param name="filePath">O caminho do arquivo binário a ser carregado.</param>
        /// <returns>Um objeto do tipo especificado, contendo os dados carregados do arquivo binário.</returns>
        public static T LoadBinary<T>(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fileStream);
            }
        }

        /// <summary>
        /// Carrega um arquivo XML e retorna os dados desserializados no tipo especificado.
        /// </summary>
        /// <typeparam name="T">O tipo de objeto a ser desserializado.</typeparam>
        /// <param name="filePath">O caminho do arquivo XML a ser carregado.</param>
        /// <returns>Um objeto do tipo especificado, contendo os dados carregados do arquivo XML.</returns>
        public static T LoadXML<T>(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(fileStream);
            }
        }
    }
}
