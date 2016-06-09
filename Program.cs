using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    public class LRUclass<T ,V>
    {
        private int capacity;

        // We need to store both the key and the value in the linked list. This is needed because when we reach
        // capacity and need to remove the last item from the linked list, we will also have to remove the item
        // from the dictionary. To remove the item from the dictionary, we need the key corresponding to the last
        // item in the linked list.
        private LinkedList<Tuple<T, V>> LL = new LinkedList<Tuple<T, V>>();
        private Dictionary<T, LinkedListNode<Tuple<T, V>>> DD = new Dictionary<T, LinkedListNode<Tuple<T, V>>>();

        public LRUclass(int capacity)
        {
            this.capacity = capacity;
        }

        public int GetCurrentSize()
        {
            return DD.Count;
        }

        public bool TryGetValue(T key, out V value)
        {
            LinkedListNode<Tuple<T, V>> nodeValue;
            if (DD.TryGetValue(key, out nodeValue) == false)
            {
                // assign some default value and return false to indicate that the key is not present.
                value = default(V);
                return false;
            }

            // Retrieve the value from the node.
            value = nodeValue.Value.Item2;

            // Move the node to the head because it was recently accessed
            LL.Remove(nodeValue);
            LL.AddFirst(nodeValue);

            return true;
        }
        public void Clear()
        {
            // Clear linked list and the dictionary.
            DD.Clear();
            LL.Clear();
        }
        public void Add(T key, V value)
        {
            if (DD.ContainsKey(key) == true)
            {
                throw new ArgumentException("Key is already present in LRUCache");
            }

            // If the number is elements is at capacity, we will remove the last element from the linked list.
            if (DD.Count == capacity)
            {
                LinkedListNode<Tuple<T, V>> nodeValue = LL.Last;
                LL.RemoveLast();
                DD.Remove(nodeValue.Value.Item1);
            }

            LinkedListNode<Tuple<T, V>> node = new LinkedListNode<Tuple<T, V>>(new Tuple<T, V>(key, value));
            LL.AddFirst(node);
            DD.Add(key, node);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            TestLruCache test = new TestLruCache();

            test.ValidateLruCache();
            Console.ReadKey();
        }
    }
}
