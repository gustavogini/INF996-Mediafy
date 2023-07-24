using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MediaFy.Model
{
    class SortByMediaType : IComparer<FileInformation>
    {
        public int Compare([AllowNull] FileInformation x, [AllowNull] FileInformation y)
        {
            return x.MediaType.CompareTo(y.MediaType);
        }
    }
}
