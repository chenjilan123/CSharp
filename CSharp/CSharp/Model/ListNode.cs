using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    public class ListNode
    {
        //public ListNode CreateNode(int[] data, int index)
        //{
        //    if (data == null || index >= data.Length) return null;
        //    var node = new ListNode(data[index]);
        //    node.next = CreateNode(data, ++index);
        //    return node;
        //}
        public static ListNode CreateNode(int[] data, int index = 0)
        {
            return new ListNode(data, index);
        }

        public ListNode(int[] data, int index)
        {
            this.val = data[index];
            if (++index >= data.Length) return;
            this.next = new ListNode(data, index);
        }

        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

}
