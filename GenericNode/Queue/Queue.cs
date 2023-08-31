using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GenericNode
{
    internal class Queue<T>
    {
        private Node<T> first;
        private Node<T> last;

        public Queue()
        {
            this.first = null;
            this.last = null;
        }
        public Queue(Queue<T> other)
        {
            Queue<T> temp = new Queue<T>();
            while (!other.IsEmpty())
            {
                T t = other.Remove();
                this.Insert(t);
                temp.Insert(t);
            }
            while (!temp.IsEmpty())
                other.Insert(temp.Remove());
        }
        public void Insert(T temp)
        {
            Node<T> node = new Node<T>(temp);
            if (this.IsEmpty())
                first = node;
            else
                last.SetNext(node);
            last = node;
        }

        public T Remove()
        {
            T x = first.GetValue();
            first = first.GetNext();
            if (IsEmpty())
                last = null;
            return x;
        }

        public T Head() => first.GetValue();
        public bool IsEmpty() => first is null;

        public override string ToString()
        {
            if (this.IsEmpty()) return "[Empty]";
            Queue<T> tQueue = new Queue<T>(this);
            string s = "[";
            T temp = tQueue.Remove();
            while (!tQueue.IsEmpty())
            {
                s += temp + ",";
                temp = tQueue.Remove();
            }
            return s + temp + "]";
        }
        public int Length()
        {
            Queue<T> q = new Queue<T>(this);
            int count = 0;
            while (!q.IsEmpty())
            {
                count++;
                q.Remove();
            }
            return count;
        }
        internal void RemoveDuplicates()
        {
            List<T> noDupes = new List<T>();
            while (!IsEmpty())
            {
                T val = Remove();
                if (!noDupes.Contains(val))
                    noDupes.Add(val);
            }
            foreach (T item in noDupes)
                Insert(item);
        }
        internal void RemoveDuplicates(bool idk)
        {
            Node<T> noDupes = null;
            while (!IsEmpty())
            {
                T val = Remove();
                if (noDupes == null)
                {
                    noDupes = new Node<T>(val);
                    continue;
                }
                Node<T> node = noDupes;
                while (node.HasNext())
                {
                    if (node.GetValue().Equals(val)) {
                        break;
                    }
                    node = node.GetNext();
                }
                if (!node.GetValue().Equals(val)) {
                    node.SetNext(new Node<T>(val));
                }
            }
            while (noDupes.HasNext())
            {
                Insert(noDupes.GetValue());
                noDupes = noDupes.GetNext();
            }
            Insert(noDupes.GetValue());
            noDupes = null;
        }
    }
}
