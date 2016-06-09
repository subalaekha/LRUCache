using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    class TestLruCache
    {
        public void ValidateLruCache()
        {
            LRUclass<int, string> lru = new LRUclass<int, string>(100);

            Random rand = new Random();

            for (int i = 0; i < 10000; i++)
            {
                int key = rand.Next(1000);
                string value = key.ToString();

                string valueFromLru;
                if (lru.TryGetValue(key, out valueFromLru) == true)
                {
                    if (valueFromLru != value)
                    {
                        throw new InvalidProgramException("value not found in the cache for the key");
                    }
                }
                else
                {
                    lru.Add(key, value);
                    if (lru.GetCurrentSize() > 100)
                    {
                        throw new InvalidProgramException("LRU capacity exceeds 100");
                    }
                }
            }
        }
    }
}
