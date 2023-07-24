using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MediaFy.Model
{
    /// <summary>
    /// Classe que implementa a interface IComparer para realizar a comparação de objetos FileInformation com base na duração do arquivo.
    /// </summary>
    public class SortBySpan : IComparer<FileInformation>
    {
        /// <summary>
        /// Compara dois objetos FileInformation com base na duração do arquivo em segundos.
        /// </summary>
        /// <param name="x">O primeiro objeto FileInformation a ser comparado.</param>
        /// <param name="y">O segundo objeto FileInformation a ser comparado.</param>
        /// <returns>Um inteiro que indica a relação de ordem entre os objetos (menor que zero se x for menor que y, igual a zero se x for igual a y e maior que zero se x for maior que y).</returns>
        public int Compare([AllowNull] FileInformation x, [AllowNull] FileInformation y)
        {
            return x.DurationSpan.TotalSeconds.CompareTo(y.DurationSpan.TotalSeconds);
        }
    }
}
