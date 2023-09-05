using System;
using System.Collections.Generic;
using System.Linq;

namespace SeminarListDictionary
{
    public class LRU<T>
    {
        private Dictionary<int, T> keyValues;
        private LinkedList<int> keysStack;

        public LRU()
        {
            keyValues = new Dictionary<int, T>();
            keysStack = new LinkedList<int>();
        }

        void Add(int key, T value) 
        {
            keyValues[key] = value;
            keysStack.AddLast(key);
        }

        T Get(int key) 
        {
            if (!keyValues.ContainsKey(key))
                throw new InvalidOperationException();

            keysStack.Remove(key);
            keysStack.AddLast(key);
            return keyValues[key];
        }
        void RemoveLeastRecentlyUsed() 
        {
            if (keysStack.Count == 0)
                throw new InvalidOperationException();

            keyValues.Remove(keysStack.First.Value);
            keysStack.RemoveFirst();
        } //удаляет дольше всего не использовавшийся ключ
    }
}
