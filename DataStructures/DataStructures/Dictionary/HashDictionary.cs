using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Dictionary
{
    public class HashDictionary<K, V> : IDictionary<K, V>, IEnumerable<K>
    {
        private int _count;
        private LinkedListDictionary<K, V>[] _buckets;

        private const int LOAD_FACTOR = 3;
        private const int INIT_CAP = 4;

        public int Count
        {
            get { return _count; }
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

        private LinkedListDictionary<K, V> BucketOf(K Key)
        {
            return BucketOf(Key, _buckets);
        }

        private LinkedListDictionary<K, V> BucketOf(K Key, LinkedListDictionary<K, V>[] Buckets)
        {
            int index = IndexOf(Key);
            if (Buckets[index] == null) {
                Buckets[index] = new LinkedListDictionary<K,V>();
            }
            return Buckets[index];
        }

        private int IndexOf(K Key)
        {
            return IndexOf(Key, _buckets);
        }

        private int IndexOf(K Key, LinkedListDictionary<K, V>[] Buckets)
        {
            return Key.GetHashCode() % Buckets.Length;
        }

        private void Resize()
        {
            if (Count < LOAD_FACTOR * _buckets.Length)
            {
                return;
            }

            var newBuckets = new LinkedListDictionary<K, V>[LOAD_FACTOR * _buckets.Length];
            foreach (K key in this)
            {
                V oldValue;
                InsertOrUpdate(key, this[key], out oldValue, newBuckets);
            }
            _buckets = newBuckets;
        }

        public HashDictionary() 
        {
            _count = 0;
            _buckets = new LinkedListDictionary<K,V>[INIT_CAP];
        }

        public bool TryGetValue(K Key, out V Value)
        {
            return BucketOf(Key).TryGetValue(Key, out Value);
        }

        public bool InsertOrUpdate(K Key, V Value, out V OldValue)
        {
            return InsertOrUpdate(Key, Value, out OldValue, _buckets);
        }

        private bool InsertOrUpdate(K Key, V Value, out V OldValue, LinkedListDictionary<K, V>[] Buckets) 
        {
            bool oldValExists = BucketOf(Key, Buckets).InsertOrUpdate(Key, Value, out OldValue);
            if (!oldValExists)
            {
                _count++;
            }
            return oldValExists;
        }

        public bool ContainsKey(K Key)
        {
            return BucketOf(Key).ContainsKey(Key);
        }

        public void RemoveKey(K Key)
        {
            int index = IndexOf(Key);
            if (_buckets[index] == null)
            {
                return;
            }
            
            if (_buckets[index].ContainsKey(Key))
            {
                _buckets[index].RemoveKey(Key);
                _count--;
            }

            if (_buckets[index].Count == 0)
            {
                _buckets[index] = null;
            }
        }

        public void Clear()
        {
            _buckets = new LinkedListDictionary<K, V>[INIT_CAP];
            _count = 0;
        }

        public IEnumerator<K> GetEnumerator()
        {
            return new HashDictionaryIterator(_buckets.AsEnumerable<LinkedListDictionary<K, V>>().GetEnumerator());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class HashDictionaryIterator : IEnumerator<K>
        {
            private IEnumerator<LinkedListDictionary<K, V>> _bucketIter;
            private IEnumerator<K> _keyIter;

            public K Current
            {
                get { return _keyIter.Current; }
            }

            public HashDictionaryIterator(IEnumerator<LinkedListDictionary<K, V>> Iterable)
            {
                _bucketIter = Iterable;
                Reset();
            }

            public void Dispose()
            {
                // Do nothing
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            private bool AdvanceBucketIterator()
            {
                while (_bucketIter.MoveNext())
                {
                    if (_bucketIter.Current != null) 
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool MoveNext()
            {
                // HashDictionary is empty.
                if (_keyIter == null)
                {
                    return false;
                }

                // There is a key in the current bucket
                if (_keyIter.MoveNext())
                {
                    return true;
                }

                if (!AdvanceBucketIterator()) {
                    return false;
                }

                // There is another bucket.
                _keyIter = _bucketIter.Current.GetEnumerator();
                    
                // This is guaranteed to be true because a representation
                // invariant is that there are no empty LinkedListDictionaries
                // in the HashDictionary.
                return _keyIter.MoveNext();
            }

            public void Reset()
            {
                _bucketIter.Reset();
                if (AdvanceBucketIterator())
                {
                    _keyIter = _bucketIter.Current.GetEnumerator();
                }
            }
        }
    }
}
