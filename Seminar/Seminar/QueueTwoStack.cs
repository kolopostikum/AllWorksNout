using System.Collections.Generic;

namespace Seminar
{
    public class QueueTwoStack<T>
    {
        private LinkedList<T> stack1;
        private LinkedList<T> stack2;

        public QueueTwoStack()
        {
            stack1 = new LinkedList<T>();
            stack2 = new LinkedList<T>();
        }

        public void Push(T value)
        {
            stack1.AddLast(value);
        }

        public bool TryPop(out T result)
        {
            if (stack2.Count != 0)
            {
                result = stack2.Last.Value;
                stack2.RemoveLast();
                return true;
            }
            
            while(!this.Empty())
            {
                stack2.AddLast(stack1.Last.Value);
                stack1.RemoveLast();
            }

            if (stack2.Count != 0)
            {
                result = stack2.Last.Value;
                stack2.RemoveLast();
                return true;
            }

            result = default;
            return false;
        }

        public bool TryPeek(out T result)
        {
            if (stack2.Count != 0)
            {
                result = stack2.Last.Value;
                return true;
            }

            while (!this.Empty())
            {
                stack2.AddLast(stack1.Last.Value);
                stack1.RemoveLast();
            }

            if (stack2.Count != 0)
            {
                result = stack2.Last.Value;
                return true;
            }

            result = default;
            return false; ;
        }

        public bool Empty()
        {
            return (stack1.Count == 0)&&(stack2.Count == 0);
        }
    }
}
