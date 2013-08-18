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
                return _elements.First;
            }
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public T Pop()
        {
            throw new NotImplementedException();
        }

        public void Push(T element)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
