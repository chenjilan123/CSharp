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
            MyCircularQueue circularQueue = new MyCircularQueue(3); // 设置长度为 3

            Assert.True(circularQueue.Enqueue(1));  // 返回 true

            Assert.True(circularQueue.Enqueue(2));  // 返回 true

            Assert.True(circularQueue.Enqueue(3));  // 返回 true

            Assert.False(circularQueue.Enqueue(4));  // 返回 false，队列已满

            Assert.Equal(3, circularQueue.Rear());  // 返回 3

            Assert.True(circularQueue.IsFull());  // 返回 true

            Assert.True(circularQueue.Dequeue());  // 返回 true

            Assert.True(circularQueue.Enqueue(4));  // 返回 true

            Assert.Equal(4, circularQueue.Rear());  // 返回 4
        }
    }
}
