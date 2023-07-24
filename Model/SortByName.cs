using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MediaFy.Model
{
    class SortByName : IComparer<FileInformation>
    {
        public int Compare([AllowNull] FileInformation x, [AllowNull] FileInformation y)
        {
            return x.FileName.CompareTo(y.FileName);
        }
    }
}
