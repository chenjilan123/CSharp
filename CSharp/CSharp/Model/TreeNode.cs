using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }

        public static TreeNode GetTreeNode(int?[] data)
        {
            if (data == null || data.Length == 0 || !data[0].HasValue) return null;
            return GetTreeNode(data, 1);
        }

        private static TreeNode GetTreeNode(int?[] data, int index)
        {
            if (index > data.Length || !data[index - 1].HasValue) return null;
            var node = new TreeNode(data[index - 1].Value)
            {
                left = GetTreeNode(data, index * 2),
                right = GetTreeNode(data, index * 2 + 1),
            };
            return node;
        }

        public static TreeNode GetTreeNodeShort(int?[] data)
        {
            return null;
        }
    }
}
