using System;
using System.Collections.Generic;

namespace Net_Stack_
{
    public class Stack<T>
    {
        private readonly List<T> _items;

        public Stack()
        {
            _items = new List<T>();
        }

        public T CurrentItem => _items.Count > 0 ? _items[^1] : throw new InvalidOperationException("Стек пуст.");
        public int Count => _items.Count;
        public bool IsEmpty => _items.Count == 0;

        public void Push(T item)
        {
            _items.Add(item);
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Стек пуст.");
            }

            var item = _items[^1];
            _items.RemoveAt(_items.Count - 1);
            return item;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}