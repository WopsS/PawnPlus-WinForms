using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public enum TreeNodeType
    {
        Directory,
        File,
        Root
    }

    public class TreeNodeSorter : IComparer
    {
        public int Compare(object treeNodeOne, object treeNodeTwo)
        {
            TreeNode firstTreeNode = (TreeNode)treeNodeOne;
            TreeNode secondTreeNode = (TreeNode)treeNodeTwo;

            TreeNodeType firstTag = firstTreeNode.Tag == null ? TreeNodeType.File : (TreeNodeType)firstTreeNode.Tag;
            TreeNodeType secondTag = secondTreeNode.Tag == null ? TreeNodeType.File : (TreeNodeType)secondTreeNode.Tag;

            // Compare the length of the strings, returning the difference.
            if (firstTag < secondTag)
            {
                return -1;
            }
            else if (firstTag > secondTag)
            {
                return 1;
            }

            return string.Compare(firstTreeNode.Text, secondTreeNode.Text);
        }
    }

    public class TreeNodeHelper
    {
        /// <summary>
        /// Get node by path.
        /// </summary>
        /// <param name="nodes">Collection of nodes.</param>
        /// <param name="path">TreeView path for node.</param>
        /// <returns>Returns null if the node won't be find, otherwise <see cref="TreeNode"/>.</returns>
        public static TreeNode GetNodeByPath(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode currentNode in nodes)
            {
                if (System.IO.Path.GetExtension(currentNode.FullPath) != string.Empty)
                {
                    continue;
                }

                if (currentNode.FullPath == path)
                {
                    return currentNode;
                }

                if (currentNode.Nodes.Count > 0)
                {
                    TreeNode treeNode = GetNodeByPath(currentNode.Nodes, path);

                    // Check if we found a node.
                    if (treeNode != null)
                    {
                        return treeNode;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Change key of a node.
        /// </summary>
        /// <param name="node">Node to change the key.</param>
        /// <param name="newKey">New key of the node.</param>
        public static void ChangeKey(TreeNode node, string newKey)
        {
            node.Name = newKey;
        }

        /// <summary>
        /// Replace a key or a part from a key with a new one.
        /// </summary>
        /// <param name="nodes">Collection of nodes.</param>
        /// <param name="oldKey">Old key or a part from the old key.</param>
        /// <param name="newKey">New key or a part from the new key.</param>
        public static void ChangeKeys(TreeNodeCollection nodes, string oldKey, string newKey)
        {
            foreach (TreeNode currentNode in nodes)
            {
                if (string.IsNullOrEmpty(currentNode.Name) == false && string.IsNullOrWhiteSpace(currentNode.Name) == false && currentNode.Name.Length > oldKey.Length && currentNode.Name.Substring(0, oldKey.Length) == oldKey)
                {
                    ChangeKey(currentNode, Path.Combine(newKey, currentNode.Name.Remove(0, oldKey.Length + 1)));
                }

                if (currentNode.Nodes.Count > 0)
                {
                    ChangeKeys(currentNode.Nodes, oldKey, newKey);
                }
            }
        }
    }
}
