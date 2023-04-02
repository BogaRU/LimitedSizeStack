using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private static int count = 0;
        static int Limit;
        StackItem<T> Head;
        StackItem<T> Tail;

        public LimitedSizeStack(int limit)
        {
            Limit = limit;
        }

        public void Push(T item)
        {
            if (Limit == 0) return;
            var newItem = new StackItem<T>() { Value = item, Next = null, Prev = null };
            if (Head == null) Head = Tail = newItem;
            else
            {
                newItem.Prev = Tail;
                Tail.Next = newItem;
                Tail = newItem;
            }
            if (count >= Limit)
            {
                Head.Next.Prev = null;
                Head = Head.Next;
            }
            else count++;

        }

        public T Pop()
        {
            if (Head == null) throw new InvalidOperationException("Stack is already empty!");
            var item = Tail.Value;
            if (Head == Tail) Head = Tail = null;
            else Tail = Tail.Prev;
            count--;
            return item;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }
    }

    public class StackItem<T>
    {
        public T Value { get; set; }
        public StackItem<T> Next { get; set; }
        public StackItem<T> Prev { get; set; }

    }
}
