using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public interface ILinkedList<T>
    {
        /// <summary>
        /// Returns the number of elements in this.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the first element of this, or null if there
        /// is no such element.
        /// </summary>
        T First { get; }

        /// <summary>
        /// Returns the last element of this, or null if there
        /// is not such element.
        /// </summary>
        T Last { get; }

        /// <summary>
        /// Adds the given element at the head of the list and returns
        /// the corresponding LinkedListNode.
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        void AddFirst(T Element);

        /// <summary>
        /// Removes the first element from this.
        /// </summary>
        void RemoveFirst();

        /// <summary>
        /// Removes the last element from this.
        /// </summary>
        void RemoveLast();

        /// <summary>
        /// Adds the given element at the tail of the list and 
        /// returns the corresponding LinkedListNode.
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        void AddLast(T Element);

        /// <summary>
        /// Returns the element at the given Index.
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Element"></param>
        /// <returns></returns>
        T GetElementAt(int Index);

        /// <summary>
        /// Removes the element at the given index.
        /// </summary>
        /// <param name="Index"></param>
        void RemoveElementAt(int Index);

        /// <summary>
        /// Returns the index of the given element.
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        int IndexOf(T Element);

        /// <summary>
        /// Returns true if and only if the given element
        /// is in the list.
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        bool Contains(T Element);

        /// <summary>
        /// Removes all elements from this.
        /// </summary>
        void Clear();
    }
}
