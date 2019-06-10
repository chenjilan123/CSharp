using CSharp.Algorithm.Struct;
using System;
using Xunit;

namespace CSharp.Algorithm
{
    public class StructTest
    {
        [Fact]
        public void CircularQueueTest()
        {
            MyCircularQueue circularQueue = new MyCircularQueue(3); // ���ó���Ϊ 3

            Assert.True(circularQueue.Enqueue(1));  // ���� true

            Assert.True(circularQueue.Enqueue(2));  // ���� true

            Assert.True(circularQueue.Enqueue(3));  // ���� true

            Assert.False(circularQueue.Enqueue(4));  // ���� false����������

            Assert.Equal(3, circularQueue.Rear());  // ���� 3

            Assert.True(circularQueue.IsFull());  // ���� true

            Assert.True(circularQueue.Dequeue());  // ���� true

            Assert.True(circularQueue.Enqueue(4));  // ���� true

            Assert.Equal(4, circularQueue.Rear());  // ���� 4
        }
    }
}
