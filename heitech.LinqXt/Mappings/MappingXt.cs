using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Mappings
{
    public static class MappingXt
    {
        ///<summary>
        /// Try substitute a key with a new value, but let the value be intact.
        ///</summary>
        public static bool TrySubstituteKey<K, V>(this IDictionary<K, V> dictionary, K old, K @new)
        {
            if (dictionary.TryGetValue(old, out V val))
            {
                dictionary.Remove(old);
                dictionary.Add(@new, val);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Uses default Equals method for value
        /// </summary>
        public static bool TrySubstituteKeyValuePair<K, V>(this IDictionary<K, V> dictionary, 
            (K key, V val) kv1, (K key, V val) kv2)
        {
            if (dictionary.TryGetValue(kv1.key, out V val))
            {
                if (val.Equals(kv1.val))
                {
                    dictionary.Remove(kv1.key);
                    dictionary.Add(kv2.key, kv2.val);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Uses default Equals method for value
        /// </summary>
        public static bool TrySubstituteValue<K, V>(this IDictionary<K, V> dictionary, V value, V sndVal)
        {
            var k_v = dictionary.FirstOrDefault(x => x.Value.Equals(value));
            if (!k_v.Equals(default(KeyValuePair<K, V>)))
            {
                dictionary[k_v.Key] = sndVal;
                return true;
            }
            return false;
        }

        ///<summary>
        /// convert Map to Tuple Array
        ///</summary>
        public static (K key, V value)[] ToTupleArray<K, V>(this IDictionary<K, V> dictionary)
        {
            var list = new List<(K k, V v)>();
            foreach (var item in dictionary)
                list.Add((item.Key, item.Value));

            return list.ToArray();
        }
    }
}
