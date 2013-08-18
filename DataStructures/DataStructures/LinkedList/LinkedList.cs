using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public class LinkedList<T> : ILinkedList<T>, IEnumerable<T> 
        where T : IEquatable<T>
    {
        private LinkedListNode _sentinel;

        public int Count { get; private set; }

        public T First 
        {
            get
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException();
                }
                return _sentinel.Next.Value;
            }
        }

        public T Last
        {
            get
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException();
                }
                return _sentinel.Prev.Value;
            }
        }

        /// <summary>
        /// Creates an empty LinkedList.
        /// </summary>
        public LinkedList()
        {
            _sentinel = new LinkedListNode(default(T));
            _sentinel.Next = _sentinel;
            _sentinel.Prev = _sentinel;
        }

        /// <summary>
        /// Creates a LinkedList from the elements of Source.
        /// </summary>
        /// <param name="Source"></param>
        public LinkedList(IEnumerable<T> Source)
        {
            _sentinel = new LinkedListNode(default(T));
            foreach (T el in Source)
            {
                this.AddLast(el);
            }
        }

        public void AddFirst(T Element)
        {
            var newEl = new LinkedListNode(Element);
            newEl.Next = _sentinel.Next;
            newEl.Prev = _sentinel;
            _sentinel.Next.Prev = newEl;
            _sentinel.Next = newEl;
            Count++;
        }

        public void RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            Count--;
            _sentinel.Next = _sentinel.Next.Next;
        }

        public void RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            Count--;
            _sentinel.Prev = _sentinel.Prev.Prev;
        }

        public void AddLast(T Element)
        {
            var newEl = new LinkedListNode(Element);
            newEl.Next = _sentinel;
            newEl.Prev = _sentinel.Prev;
            _sentinel.Prev.Next = newEl;
            _sentinel.Prev = newEl;
            Count++;
        }

        public T GetElementAt(int Index)
        {
            if (Index >= Count || Index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            LinkedListNode curr = _sentinel.Next;
            while (Index != 0)
            {
                curr = curr.Next;
                Index--;
            }

            return curr.Value;
        }

        public void RemoveElementAt(int Index)
        {
            if (Index >= Count || Index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            LinkedListNode curr = _sentinel;
            while (Index != 0)
            {
                curr = curr.Next;
                Index--;
            }

            curr.Next.Next.Prev = curr;
            curr.Next = curr.Next.Next;
            Count--;
        }

        public int IndexOf(T element)
        {
            int i = 0;
            LinkedListNode curr = _sentinel.Next;
            while (curr != _sentinel && !curr.Value.Equals(element))
            {
                curr = curr.Next;
                i++;
            }

            if (curr == _sentinel)
            {
                return -1;
            }
            else
            {
                return i;
            }
        }

        public bool Contains(T Element)
        {
            if (Count == 0)
            {
                return false;
            }

            LinkedListNode curr = _sentinel;
            while (curr.Next != _sentinel)
            {
                if (curr.Next.Value.Equals(Element))
                {
                    return true;
                }
                curr = curr.Next;
            }

            return false;
        }

        public void Clear()
        {
            _sentinel.Next = _sentinel;
            _sentinel.Prev = _sentinel;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator(this);
        }

        private class LinkedListNode : IEquatable<LinkedListNode>
        {
            public LinkedListNode Next { get; internal set; }

            public LinkedListNode Prev { get; internal set; }

            public T Value { get; set; }

            public LinkedListNode(T Value)
            {
                this.Value = Value;
            }

            public bool Equals(LinkedListNode other)
            {
                return Value.Equals(other.Value);
            }
        }

        private class LinkedListEnumerator : IEnumerator<T>
        {
            private LinkedList<T> _iterable;
            private LinkedListNode _curr;

            public LinkedListEnumerator(LinkedList<T> l)
            {
                _iterable = l;
                _curr = l._sentinel;
            }

            public T Current
            {
                get { return _curr.Value; }
            }

            public void Dispose()
            {
                // nothing to do
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_curr.Next == _iterable._sentinel)
                {
                    return false;
                }
                _curr = _curr.Next;
                return true;
            }

            public void Reset()
            {
                _curr = _iterable._sentinel;
            }
        }
    }
}
