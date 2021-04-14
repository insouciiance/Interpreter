using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Collections
{
    public class Hashtable<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private const double MaxLoadFactor = .8;

        private LinkedList<KeyValuePair<TKey, TValue>>[] _table;
        private int _count;

        public Hashtable(int size = 5)
        {
            _table = new LinkedList<KeyValuePair<TKey, TValue>>[size];
        }

        public void Add(TKey key, TValue val)
        {
            if ((double) _count / _table.Length > MaxLoadFactor)
            {
                Resize();
            }

            int index = HashToIndex(key, _table.Length);
            if (_table[index] != null)
            {
                _table[index].Add(new KeyValuePair<TKey, TValue>(key, val));
            }
            else
            {
                _table[index] = new LinkedList<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, val));
            }

            _count++;
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = HashToIndex(key, _table.Length);

                if (key == null)
                {
                    throw new ArgumentNullException();
                }

                if (index >= _table.Length || _table[index] == null)
                {
                    throw new KeyNotFoundException();
                }

                if (_table[index].Head == _table[index].Last && key.Equals(_table[index].Head.Data.Key))
                {
                    return _table[index].Head.Data.Value;
                }

                foreach (KeyValuePair<TKey, TValue> node in _table[index])
                {
                    if (node.Key.Equals(key))
                    {
                        return node.Value;
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        private void Resize(int multiplier = 2)
        {
            var oldTable = _table;
            _table = new LinkedList<KeyValuePair<TKey, TValue>>[_table.Length * multiplier];

            foreach (var chain in oldTable)
            {
                if (chain?.Head != null)
                {
                    foreach (KeyValuePair<TKey, TValue> node in chain)
                    {
                        Add(node.Key, node.Value);
                    }
                }
            }
        }

        private int HashToIndex(object o, int arraySize)
        {
            string hash = o.ToString().ToLowerInvariant();
            long sum = 0;
            long mul = 1;
            for (int i = 0; i < hash.Length; i++)
            {
                mul = (i % 4 == 0) ? 1 : mul * 256;
                sum += hash[i] * mul;
            }
            return (int)(Math.Abs(sum) % arraySize);
        }
    }
}
