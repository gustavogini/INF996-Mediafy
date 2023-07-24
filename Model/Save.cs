using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace MediaFy.Model
{
    /// <summary>
    /// Classe que contém métodos para salvar objetos persistentemente em diferentes formatos, como texto, binário e XML.
    /// </summary>
    public class Save
    {
        /// <summary>
        /// Salva um objeto em formato de arquivo de texto.
        /// </summary>
        /// <param name="filePath">O caminho completo do arquivo a ser salvo.</param>
        /// <param name="saveObject">Uma lista de strings contendo os dados a serem salvos no arquivo de texto.</param>
        public static void SaveTextFile(string filePath, List<string> saveObject)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    foreach (string line in saveObject)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// Salva um objeto em formato binário.
        /// </summary>
        /// <typeparam name="T">O tipo do objeto a ser salvo.</typeparam>
        /// <param name="filePath">O caminho completo do arquivo a ser salvo.</param>
        /// <param name="saveObject">O objeto genérico a ser salvo em formato binário.</param>
        public static void SaveBinary<T>(string filePath, T saveObject)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, saveObject);
            }
        }

        /// <summary>
        /// Salva um objeto em formato XML, especialmente útil para listas com tipos definidos.
        /// </summary>
        /// <typeparam name="T">O tipo do objeto a ser salvo.</typeparam>
        /// <param name="filePath">O caminho completo do arquivo a ser salvo.</param>
        /// <param name="saveObject">O objeto genérico a ser salvo em formato XML.</param>
        /// <param name="types">Uma matriz de tipos que especifica os tipos que serão incluídos na serialização XML.</param>
        public static void SaveXML<T>(string filePath, T saveObject, Type[] types)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), types);
                xmlSerializer.Serialize(fileStream, saveObject);
            }
        }

        /// <summary>
        /// Sobrecarga do método SaveXML para salvar um objeto em formato XML sem tipos específicos (para objetos simples).
        /// </summary>
        /// <typeparam name="T">O tipo do objeto a ser salvo.</typeparam>
        /// <param name="filePath">O caminho completo do arquivo a ser salvo.</param>
        /// <param name="saveObject">O objeto genérico a ser salvo em formato XML.</param>
        public static void SaveXML<T>(string filePath, T saveObject)
        {
            Type[] types = null;
            SaveXML(filePath, saveObject, types);
        }
    }
}
