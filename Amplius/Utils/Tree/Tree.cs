using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Amplius.Utils.Tree
{
#nullable enable
    public sealed class Tree : ITreeNode
    {
        public delegate string NameFactory(ITreeNode node);
        public delegate ITreeNode NodeFactory<T>(T type);

        private readonly List<ITreeNode> roots;

        private Tree(ITreeNode root)
        {
            roots = new List<ITreeNode>();
            roots.Add(root);
        }

        public Tree Merge(Tree tree)
        {
            roots.AddRange(tree.GetChildren());
            return this;
        }
        public void Walk(Action<ITreeNode>? action = null) => SWalk(action);

        private void SWalk(Action<ITreeNode>? action = null, ITreeNode? target = null)
        {
            if (action == null)
                return;

            var aTarget = target ?? this;

            foreach (var child in aTarget.GetChildren())
            {
                action.Invoke(child);
                SWalk(action, child);
            }
        }

        public void PrettyPrint(TextWriter writer, NameFactory getDisplayName)
        {
            foreach (var root in roots)
                PrettyPrint(writer, root, getDisplayName);
        }
        private void PrettyPrint(TextWriter writer, ITreeNode node, NameFactory getDisplayName, string indent = "", bool isLast = true)
        {
            var isToConsole = writer == Console.Out;

            var tokenMarker = isLast ? "└──" : "├──";

            if (isToConsole)
                Console.ForegroundColor = ConsoleColor.DarkGray;

            writer.Write(indent);
            writer.Write(tokenMarker);

            if (isToConsole)
                Console.ForegroundColor = ConsoleColor.White;

            writer.Write(getDisplayName.Invoke(node));

            if (isToConsole)
                Console.ResetColor();

            writer.WriteLine();

            indent += isLast ? "   " : "│  ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(writer, child, getDisplayName, indent, child == lastChild);
        }

        public static Tree From(ITreeNode root) => new Tree(root);
        public static Tree FromType<T>(T root, NodeFactory<T> factory) => From(factory.Invoke(root));
        public static Tree FromDirectory(DirectoryInfo root) => FromType(root, DirectoryTree.NodeFactory);

        public IEnumerable<ITreeNode> GetChildren()
        {
            foreach (var node in roots)
                yield return node;
        }
    }
}
