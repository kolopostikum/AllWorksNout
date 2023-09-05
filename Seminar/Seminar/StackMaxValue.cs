using System;
using System.Collections.Generic;

namespace Seminar
{
    public class StackMaxValue<T>
        where T : IComparable
    {
        private LinkedList<T> StackRealisation;
        private LinkedList<T> MaxValue;

        public StackMaxValue()
        {
            StackRealisation = new LinkedList<T>();
            MaxValue = new LinkedList<T>();
        }

        public void Push(T value)
        {
            if (MaxValue.Count == 0)
                MaxValue.AddLast(value);
            else if (value.CompareTo(MaxValue.Last.Value) >= 0)
                MaxValue.AddLast(value);
            StackRealisation.AddLast(value);
        }

        public bool TryPop (out T result)
        {
            if (StackRealisation.Count != 0)
            {
                result = StackRealisation.Last.Value;
                if (result.CompareTo(MaxValue.Last.Value) == 0)
                    MaxValue.RemoveLast();
                StackRealisation.RemoveLast();
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public bool TryPeek(out T result)
        {
            if (StackRealisation.Count != 0)
            {
                result = StackRealisation.Last.Value;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public bool GetMax(out T result)
        {
            if (StackRealisation.Count != 0)
            {
                result = MaxValue.Last.Value;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

    }
}
