using DataStructures.LinkedList;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Stack
{
    public class Stack<T> : IStack<T> where T : IEquatable<T>
    {
        private LinkedList<T> _elements;

        public Stack()
        {
            _elements = new LinkedList<T>();
        }

        public T Top
        {
            get
            {
                return _elements.Last;
            }
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public T Pop()
        {
            T last = _elements.Last;
            _elements.RemoveLast();
            return last;
        }

        public void Push(T element)
        {
            _elements.AddLast(element);
        }

        public void Clear()
        {
            _elements.Clear();
        }
    }
}
