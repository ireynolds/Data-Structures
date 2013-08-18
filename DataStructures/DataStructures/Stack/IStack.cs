using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Stack
{
    public interface IStack<T>
    {
        T Top { get; }
        int Count { get; }
        T Pop();
        void Push(T element);
        void Clear();
    }
}
