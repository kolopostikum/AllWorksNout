using System.Collections.Generic;
using System;

namespace Seminar
{
    public class VersionStack<T>
    {
        private LinkedList<T> stack;
        private Dictionary<int, LinkedListNode<T>> stateMap;
        private int nextVersion;

        public VersionStack()
        {
            stack = new LinkedList<T>();
            stateMap = new Dictionary<int, LinkedListNode<T>>();
            nextVersion = 0;
        }

        public void Push(T item)
        {
            stack.AddLast(item);
            stateMap[nextVersion++] = stack.Last;
        }

        public T Pop()
        {
            if (stack.Count == 0)
                throw new InvalidOperationException("Stack is empty.");

            var lastNode = stack.Last;
            stack.RemoveLast();
            stateMap.Remove(--nextVersion);
            return lastNode.Value;
        }

        public void Rollback(int version)
        {
            if (!stateMap.ContainsKey(version))
                throw new ArgumentException("Invalid version number.");

            var node = stateMap[version];
            while (stack.Last != node)
            {
                stateMap.Remove(--nextVersion);
                stack.RemoveLast();
            }
        }

        public void Forget()
        {
            stack.Clear();
            stateMap.Clear();
            nextVersion = 0;
        }
    }
}
