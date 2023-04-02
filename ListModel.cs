using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        LimitedSizeStack<Tuple<bool, int, TItem>> stack;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            stack = new LimitedSizeStack<Tuple<bool, int, TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            stack.Push(new Tuple<bool, int, TItem>(true, Items.Count - 1, item));
        }

        public void RemoveItem(int index)
        {
            stack.Push(new Tuple<bool, int, TItem>(false, index, Items[index]));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return stack.Count > 0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                var tup = stack.Pop();
                if (tup.Item1 == true)
                {
                    Items.RemoveAt(tup.Item2);           
                }
                else
                {
                    Items.Insert(tup.Item2, tup.Item3);
                }
            }
        }
    }
}
