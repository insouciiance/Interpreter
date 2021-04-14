using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Collections
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public LinkedListNode<T> Head { get; private set; }
        public LinkedListNode<T> Last { get; private set; }

        public LinkedList(T initialData)
        {
            Head = new LinkedListNode<T>(initialData, null);
            Last = Head;
        }

        public LinkedList() { }

        public void Add(T item)
        {
            if (Last == null)
            {
                Head = new LinkedListNode<T>(item, null);
                Last = Head;
                return;
            }
            
            Last.Next = new LinkedListNode<T>(item, null);
            Last = Last.Next;
        }

        public void AddFirst(T item)
        {
            if (Head == null)
            {
                Head = new LinkedListNode<T>(item, null);
                Last = Head;
                return;
            }

            Head = new LinkedListNode<T>(item, Head);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new LinkedListEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        private class LinkedListEnumerator : IEnumerator<T>
        {
            public T Current => _currentNode.Data;
            
            private readonly LinkedListNode<T> _head;
            private LinkedListNode<T> _currentNode;
            object IEnumerator.Current => Current;

            private bool _firstNodeVisited = false;

            public LinkedListEnumerator(LinkedList<T> head)
            {
                _head = head.Head;
                _currentNode = _head;
            }

            public void Dispose() { }
            public bool MoveNext()
            {
                if (!_firstNodeVisited)
                {
                    _firstNodeVisited = true;
                    return true;
                }
                
                if (_currentNode.Next != null)
                {
                    _currentNode = _currentNode.Next;
                    return true;
                }

                return false;
            }
            public void Reset()
            {
                _firstNodeVisited = false;
                _currentNode = _head;
            }
        }
    }
}
