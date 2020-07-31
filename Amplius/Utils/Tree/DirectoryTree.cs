using System.Collections.Generic;
using System.IO;

namespace Amplius.Utils.Tree
{
    public static class DirectoryTree
    {
        public static string NameFactory(ITreeNode node)
        {
            if (node is DirectoryNode dn)
                return dn.Info.Name;
            else if (node is FileNode fn)
                return fn.Info.Name;
            else
                return "Unknown";
        }
        public static ITreeNode NodeFactory(DirectoryInfo root)
        {
            var directories = root.GetDirectories();
            var files = root.GetFiles();

            var mergedNodes = new List<ITreeNode>();
            var directoryNodes = new List<ITreeNode>();
            var fileNodes = new List<ITreeNode>();

            foreach (var directory in directories)
                directoryNodes.Add(NodeFactory(directory));
            foreach (var file in files)
                directoryNodes.Add(new FileNode(file));

            mergedNodes.AddRange(directoryNodes);
            mergedNodes.AddRange(fileNodes);

            return new DirectoryNode(root, mergedNodes);
        }
    }
}
