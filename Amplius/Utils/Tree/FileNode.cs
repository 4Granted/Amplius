using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Amplius.Utils.Tree
{
    public sealed class FileNode : ITreeNode
    {
        public FileInfo Info { get; }

        public FileNode(FileInfo info) => Info = info;

        public IEnumerable<ITreeNode> GetChildren() => Enumerable.Empty<ITreeNode>();
    }
}
