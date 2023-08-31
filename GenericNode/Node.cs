using GenericNode.T_Value_Test;
using System.Numerics;

namespace GenericNode
{
    public class Node<T>
    {
        private T Value;
        private Node<T> Next;


        public Node(T Value, Node<T> next = null)
        {
            this.Value = Value;
            this.Next = next;
        }
        public T GetValue() => Value;
        public Node<T> GetNext() => Next;

        public void SetValue(T Value) => this.Value = Value;
        public void SetNext(Node<T> next) => this.Next = next;
        //Func<Node<T>,bool> Predict)
        public bool Remove(Node<T> curr)
        {
            if (curr == this)
            {
                if (!curr.HasNext())
                    return false;
                Node<T> next = curr.GetNext();
                Value = next.Value;
                Next = next.Next;
                return true;
            }
            Node<T> prev = GetPreviousNode(curr);
            if (prev == null)
                return false;
            prev.SetNext(curr.GetNext());
            return true;
        }
        internal int RemoveAll(Predicate<T> match)
        {
            if (match == null) return 0;
            int i = 0;
            Node<T> node = this;
            
            while (!match(node.GetValue()) && node.HasNext())
                node = node.GetNext();

            while (match(node.GetValue()) && node.HasNext())
            {
                if (Remove(node))
                    i += 1;
                else
                    node = node.GetNext();
            }
            //if (match(node.GetValue()))
            //  i += Remove(node) ? 1 : 0;
            if (i == 0 || !HasNext())
                return i;
            return i + RemoveAll(match);
        }

        private Node<T> GetPreviousNode(Node<T> current)
        {
            Node<T> first = this;
            for (Node<T> next = first.Next; first.HasNext(); first = next,
                next = first.Next)
                if (next == current)
                    return first;
            return null;
        }

        public bool HasNext() => Next is not null;
        public override string ToString() => $"{Value} --> {Next}";
    }
}