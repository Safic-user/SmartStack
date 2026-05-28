using System;
using System.Collections;
using System.Collections.Generic;

namespace SmartStackProject
{
    public class SmartStack<T>:IEnumerable<T>
    {
        private T[] _items;
        private int _count;
        public SmartStack()
        {
            _items = new T[4];
            _count = 0;
        }
        public SmartStack(int Count)
        {
            _items = new T[Count];
            _count = 0;
        }
        public SmartStack(IEnumerable<T> Collection)
        {
            int size = 0;
            foreach (T item in Collection) { size++; }

            _items = new T[size];
            _count = 0;

            foreach (T item in Collection) { _items[_count++] = item; }
        }
        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                T[] newArray = new T[_count * 2];
                Array.Copy(_items, newArray, _count);
                _items = newArray;
            }
            _items[_count++] = item;
        }
        public void PushRange(IEnumerable<T> collection)
        {
            foreach (T item in collection) { Push(item); }
        }
        public T Pop()
        {
            if (_count == 0) throw new InvalidOperationException("Стек пуст.");
            T item = _items[--_count];
            _items[_count] = default(T);
            return item;
        }
        public T Peek()
        {
            if (_count == 0) throw new InvalidOperationException("Стек пуст.");
            return _items[_count - 1];
        }
        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < _items.Length; i++)
            {
                if (comparer.Equals(_items[i], item)) return true;
            }
            return false;
        }
        public int Count()
        {
            return _items.Length;
        }
        public int Capacity()
        {
            return _count;
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item; 
            }
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public T this[int depth] 
        {  
            get 
            {
                if (depth < 0 || depth >= _count) throw new ArgumentOutOfRangeException("Выход за границы коллекции.");
                return _items[depth]; 
            } 
        }


    }
}
