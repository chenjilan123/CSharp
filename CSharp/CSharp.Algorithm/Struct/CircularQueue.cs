public class MyCircularQueue
{
    public int[] _data;
    private int _head;
    private int _tail;
    /** Initialize your data structure here. Set the size of the queue to be k. */
    public MyCircularQueue(int k)
    {
        _data = new int[k];
        _head = -1;
        _tail = -1;
    }

    /** Insert an element into the circular queue. Return true if the operation is successful. */
    public bool Enqueue(int value)
    {
        if (IsFull()) return false;
        if (IsEmpty()) _head = 0;
        _tail++;
        if (_tail == _data.Length) _tail = 0;
        _data[_tail] = value;
        return true;
    }

    /** Delete an element from the circular queue. Return true if the operation is successful. */
    public bool Dequeue()
    {
        if (IsEmpty()) return false;
        if (_head == _tail)
            _head = _tail = -1;
        else
            _head++;
        if (_head == _data.Length) _head = 0;
        return true;
    }

    /** Get the front item from the queue. */
    public int Front()
    {
        if (IsEmpty()) return -1;
        return _data[_head];
    }

    /** Get the last item from the queue. */
    public int Rear()
    {
        if (IsEmpty()) return -1;
        return _data[_tail];
    }

    /** Checks whether the circular queue is empty or not. */
    public bool IsEmpty()
    {
        return _head == -1;
    }

    /** Checks whether the circular queue is full or not. */
    public bool IsFull()
    {
        //参考
        //return (p_end + 1) % p_size == p_start;

        var dis = _head - _tail;
        if (dis < 0) dis += _data.Length;
        return dis == 1;
    }
}
