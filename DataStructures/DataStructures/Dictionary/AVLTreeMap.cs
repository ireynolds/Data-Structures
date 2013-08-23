using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Dictionary
{
    class AVLTreeMap<K, V>
        where K : IComparable<K>
        where V : class
    {
        private int _count;
        private Node _overallRoot;

        /// <summary>
        /// Returns the number of elements in this.
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        public AVLTreeMap()
        {
            _count = 0;
            _overallRoot = null;
        }

        public void Clear()
        {
            _overallRoot = null;
        }

        /// <summary>
        /// Returns the value that the given key maps to, or null if there is no such value.
        /// </summary>
        /// <param name="Key">The key whose value to return.</param>
        /// <returns>The value mapped to by the given key</returns>
        /// <exception cref="ArgumentNullException">If Key is null.</exception>
        public V Get(K Key)
        {
            try
            {
                CheckForNull(Key, "Key");
            }
            catch
            {
                throw;
            }

            Node Current = _overallRoot;
            while (Current != null)
            {
                if (Key.CompareTo(Current.Key) < 0)
                    Current = Current.Left;
                else if (Key.CompareTo(Current.Key) > 0)
                    Current = Current.Right;
                else // if (Key.CompareTo(Current.Key) == 0)
                    return Current.Value;
            }
            return null;
        }

        /// <summary>
        /// Ensures that the given Key maps to the given Value. Overwrites an
        /// existing value if necessary.
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <param name="Value">The value.</param>
        /// <returns>The value that was overwritten, or null if the given key was not in this.</returns>
        /// <exception cref="ArgumentNullException">If either argument is null.</exception>
        public V Insert(K Key, V Value)
        {
            try
            {
                CheckForNull(Key, "Key");
                CheckForNull(Value, "Value");
            }
            catch
            {
                throw;
            }

            _overallRoot = Insert(Key, Value, _overallRoot);
            return null;
        }

        private Node Insert(K Key, V Value, Node Current)
        {
            if (Current == null)
                Current = new Node(Key, Value, null, null);
            else
            {
                if (Key.CompareTo(Current.Key) < 0)
                {
                    Current.Left = Insert(Key, Value, Current.Left);
                }
                else if (Key.CompareTo(Current.Key) > 0)
                {
                    Current.Right = Insert(Key, Value, Current.Right);
                }
                else // if (Key.CompareTo(Current.Key) == 0)
                {
                    Current.Value = Value;
                }
            }

            return Balance(Current);
        }

        /// <summary>
        /// Ensures that the given Key is not in the map. Removes an existing
        /// key-value pair if necessary.
        /// </summary>
        /// <param name="Key">The key whose mapping to remove.</param>
        /// <returns>The value referenced by the key removed, or null if no such value existed.</returns>
        /// <throws cref="ArgumentNullException"></throws>
        public V Remove(K Key)
        {
            try
            {
                CheckForNull(Key, "Key");
            }
            catch
            {
                throw;
            }

            // TODO implement this method.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of the keys in the tree, presented in order.
        /// </summary>
        /// <returns></returns>
        internal List<K> TraverseInOrder()
        {
            List<K> List = new List<K>();
            TraverseInOrder(_overallRoot, List);
            return List;
        }

        private void TraverseInOrder(Node Current, List<K> List)
        {
            if (Current == null)
                return;
            else
            {
                TraverseInOrder(Current.Left, List);
                List.Add(Current.Key);
                TraverseInOrder(Current.Right, List);
            }
        }

        private Node Balance(Node Current)
        {
            int hLeft = Height(Current.Left);
            int hRight = Height(Current.Right);

            if (hLeft - hRight > 1)
            {
                // left is taller by 2. Which side of left is taller?
                if (Height(Current.Left.Left) > Height(Current.Left.Right))
                {
                    // left-left is taller.
                    Current = RotateRight(Current);
                }
                else // if (Height(Current.Left.Left) < Height(Current.Left.Right))
                {
                    // left-right is taller.
                    Current.Left = RotateLeft(Current.Left);
                    Current = RotateRight(Current);
                }
            }
            else if (hRight - hLeft > 1)
            {
                // left is taller by 2. Which side of left is taller?
                if (Height(Current.Right.Right) > Height(Current.Right.Left))
                {
                    // right-right is taller.
                    Current = RotateLeft(Current);
                }
                else // if (Height(Current.Right.Right) < Height(Current.Right.Left))
                {
                    // right-left is taller.
                    Current.Right = RotateRight(Current.Right);
                    Current = RotateLeft(Current);
                }
            }

            return Current;
        }

        private Node RotateRight(Node Root)
        {
            Node l = Root.Left;
            Root.Left = l.Right;
            l.Right = Root;
            return l;
        }

        private Node RotateLeft(Node Root)
        {
            Node r = Root.Right;
            Root.Right = r.Left;
            r.Left = Root;
            return r;
        }

        private int Height(Node Root)
        {
            if (Root == null)
                return -1;
            else
            {
                int hLeft = Height(Root.Left);
                int hRight = Height(Root.Right);
                return Math.Max(hLeft, hRight) + 1;
            }
        }

        private void CheckForNull(object Parameter, string ParameterName)
        {
            if (Parameter == null)
                throw new ArgumentNullException("The " + ParameterName + " argument cannot be null");
        }

        private void CheckRep()
        {
            CheckLeftLessRightMore();
            CheckBalance(_overallRoot);
        }

        private void CheckLeftLessRightMore()
        {
            Stack<Node> s = new Stack<Node>();
            s.Push(_overallRoot);
            while (s.Count > 0)
            {
                Node Current = s.Pop();
                if (Current != null)
                {
                    if ((Current.Left != null && Current.Left.Key.CompareTo(Current.Key) > 0) ||
                        (Current.Right != null && Current.Right.Key.CompareTo(Current.Key) < 0))
                        throw new Exception();
                    s.Push(Current.Left);
                    s.Push(Current.Right);
                }
            }
        }

        private void CheckBalance(Node Current)
        {
            if (Current == null)
                return;

            int hLeft = Height(Current.Left);
            int hRight = Height(Current.Right);
            if (Math.Abs(hLeft - hRight) > 1)
                throw new Exception();

            CheckBalance(Current.Left);
            CheckBalance(Current.Right);
        }

        private class Node
        {
            public Node(K Key, V Value, Node Left, Node Right)
            {
                _key = Key;
                _value = Value;
                _left = Left;
                _right = Right;
            }

            /// <summary>
            /// The root of the left subtree.
            /// </summary>
            private Node _left;
            public Node Left
            {
                get
                {
                    return _left;
                }
                set
                {
                    if (!Object.ReferenceEquals(_right, value))
                        _left = value;
                }
            }

            /// <summary>
            /// The root of the right subtree.
            /// </summary>
            private Node _right;
            public Node Right
            {
                get
                {
                    return _right;
                }
                set
                {
                    if (!Object.ReferenceEquals(_right, value))
                        _right = value;
                }
            }

            /// <summary>
            /// This node's key.
            /// </summary>
            private readonly K _key;
            public K Key
            {
                get
                {
                    return _key;
                }
            }

            /// <summary>
            /// The value that this node's key maps to.
            /// </summary>
            private V _value;
            public V Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    if (!Object.ReferenceEquals(_value, value))
                        _value = value;
                }
            }
        }

        private class Tuple<E>
        {
            public E Data;
            public Node Node;
            public Tuple(Node Node, E Data)
            {
                this.Node = Node;
                this.Data = Data;
            }
        }
    }
}
