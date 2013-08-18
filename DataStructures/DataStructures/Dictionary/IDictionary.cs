using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Dictionary
{
    public interface IDictionary<K, V> : IEnumerable<K>
    {
        int Count { get; }
        V this[K Key] { get; set; }
        
        bool TryGetValue(K Key, out V Value);
        bool InsertOrUpdate(K Key, V Value, out V OldValue);
        bool ContainsKey(K Key);
        void RemoveKey(K Key);
        void Clear();
    }
}
