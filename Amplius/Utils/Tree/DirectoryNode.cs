using System.Collections.Generic;
using System.IO;

namespace Amplius.Utils.Tree
{
    public sealed class DirectoryNode : ITreeNode
    {
        public DirectoryInfo Info { get; }

        private readonly IEnumerable<ITreeNode> collection;

        public DirectoryNode(DirectoryInfo info, IEnumerable<ITreeNode> collection)
        {
            Info = info;
            this.collection = collection;
        }

        public IEnumerable<ITreeNode> GetChildren() => collection;
    }
}
