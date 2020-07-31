using System.Collections.Generic;

namespace Amplius.Utils.Tree
{
    public interface ITreeNode
    {
        public IEnumerable<ITreeNode> GetChildren();
    }
}
