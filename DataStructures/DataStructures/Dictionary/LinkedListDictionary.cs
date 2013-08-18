using DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.Dictionary;


namespace DataStructures.Dictionary
{
    public class LinkedListDictionary<K, V> : IDictionary<K, V>, IEnumerable<K>
    {
        private DataStructures.LinkedList.LinkedList<KeyValuePair> _pairs;

        public int Count 
        {
            get
            {
                return _pairs.Count;
            }
        }

        public V this[K Key]
        {
            get
            {
                V val;
                if (TryGetValue(Key, out val))
                {
                    return val;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            set
            {
                V oldVal;
                InsertOrUpdate(Key, value, out oldVal);
            }
        }

        public LinkedListDictionary()
        {
            _pairs = new DataStructures.LinkedList.LinkedList<KeyValuePair>();
        }

        private bool TryGetKeyValuePair(K Key, out KeyValuePair Pair)
        {
            int idx = _pairs.IndexOf(new KeyValuePair(Key, default(V)));
            if (idx == -1)
            {
                Pair = null;
                return false;
            }
            else
            {
                Pair = _pairs.GetElementAt(idx);
                return true;
            }
        }

        public bool TryGetValue(K Key, out V Value)
        {
            KeyValuePair pair;
            if (TryGetKeyValuePair(Key, out pair))
            {
                Value = pair.Value;
                return true;
            }
            else
            {
                Value = default(V);
                return false;
            }
        }

        public bool InsertOrUpdate(K Key, V Value, out V OldValue)
        {
            KeyValuePair pair;
            if (TryGetKeyValuePair(Key, out pair))
            {
                OldValue = pair.Value;
                pair.Value = Value;
                return true;
            }
            else
            {
                OldValue = default(V);
                _pairs.AddFirst(new KeyValuePair(Key, Value));
                return false;
            }
        }

        public bool ContainsKey(K Key)
        {
            KeyValuePair oldEl;
            return TryGetKeyValuePair(Key, out oldEl);
        }

        public void RemoveKey(K Key)
        {
            KeyValuePair el = new KeyValuePair(Key, default(V));
            int idx = _pairs.IndexOf(el);
            if (idx != -1)
            {
                _pairs.RemoveElementAt(idx);
            }
        }

        public void Clear()
        {
            _pairs.Clear();
        }

        public IEnumerator<K> GetEnumerator()
        {
            return new LinkedListDictionaryEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class LinkedListDictionaryEnumerator : IEnumerator<K>
        {
            private LinkedListDictionary<K, V> _iterable;
            private IEnumerator<KeyValuePair> _iterator;

            public LinkedListDictionaryEnumerator(LinkedListDictionary<K, V> Iterable)
            {
                _iterable = Iterable;
                _iterator = Iterable._pairs.GetEnumerator();
            }

            public K Current
            {
                get { return _iterator.Current.Key; }
            }

            public void Dispose()
            {
                // Nothing to do.
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return _iterator.MoveNext();
            }

            public void Reset()
            {
                _iterator.Reset();
            }
        }

        private class KeyValuePair : IEquatable<KeyValuePair>
        {
            public K Key;
            public V Value;

            public KeyValuePair(K Key, V Value)
            {
                this.Key = Key;
                this.Value = Value;
            }

            public bool Equals(KeyValuePair other)
            {
                return Key.Equals(other.Key);
            }
        }
    }

}
