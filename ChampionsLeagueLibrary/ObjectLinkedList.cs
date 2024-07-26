using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace ChampionsLeagueLibrary
{
    public class ObjectLinkedList : ICollection, IEnumerable, IList
    {
        private Node first;
        private Node last;
        private int count;

        private class Node
        {
            public object Data { get; set; }
            public Node Prev { get; set; }
            public Node Next { get; set; }
        }


        public ObjectLinkedList()
        {
            first = null;
            last = null;
            count = 0;
        }

        public object? this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    return null;
                }
                Node tmp = first;

                for (int i = 0; i < index; i++)
                {
                    tmp = tmp.Next;
                }
                return tmp.Data;
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    return;
                }
                Node tmp = first;

                for (int i = 0; i < index; i++)
                {
                    tmp = tmp.Next;
                }
                tmp.Data = value;
            }
        }

        public int Count => count;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public int Add(object? value)
        {
            Node node = new()
            {
                Data = value
            };
            if (first == null)
            {
                first = node;
                last = node;
            }
            else
            {
                last.Next = node;
                node.Prev = last;
                last = node;
            }
            count++;
            return count;
        }

        public void Clear()
        {
            first = null;
            last = null;
            count = 0;
        }

        public bool Contains(object? value)
        {
            Node node = first;
            for (int i = 0; i < count; i++)
            {
                if(Equals(node.Data, value))
                {
                    return true;
                }
                else
                {
                    node = node.Next;
                }
            }
            return false;
        }

        public void CopyTo(Array array, int index)
        {
            if (index >= 0 && index < Count)
            {
                Node node = first;
                for (int i = 0; i < count; i++)
                {
                    array.SetValue(node.Data, index);
                    node = node.Next;
                    index++;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            Node node = first;
            for (int i = 0; i < count; i++)
            {
                yield return node.Data;
                node = node.Next;
            }
        }

        public int IndexOf(object? value)
        {
            Node node = first;
            for (int i = 0; i < count; i++)
            {
                if (Equals(node.Data, value))
                {
                    return i;
                }
                else
                {
                    node = node.Next;
                }
            }
            return -1;
        }

        public void Insert(int index, object? value)
        {
            if (index >= 0 && index <= count)
            {
                Node node = new()
                {
                    Data = value
                };

                if (index == 0)
                {
                    if (first == null)
                    {
                        Add(value);
                    }
                    else
                    {
                        node.Next = first;
                        first.Prev = node;
                        first = node;
                        count++;
                    }
                }
                else if (index == count)
                {
                    Add(value);
                }
                else
                {
                    Node tmp = first;
                    for (int i = 0; i < index; i++)
                    {
                        tmp = tmp.Next;
                    }
                    node.Prev = tmp.Prev;
                    node.Next = tmp;
                    tmp.Prev.Next = node;
                    tmp.Prev = node;
                    count++;
                }

            }
        }

        public void Remove(object? value)
        {
            Node node = first;
            for (int i = 0; i < count; i++)
            {
                if (Equals(node.Data, value))
                {
                    if (Count == 1)
                    {
                        first = null;
                        last = null;
                    }
                    else if (node == first)
                    {
                        first = first.Next;
                    }
                    else if (node == last)
                    {
                        last = last.Prev;
                    }
                    else
                    {
                        node.Prev.Next = node.Next;
                        node.Next.Prev = node.Prev;
                    }
                    count--;
                }
                node = node.Next;
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                Node node = first;
                for (int i = 0; i < index; i++)
                {
                    node = node.Next;
                }

                if (Count == 1)
                {
                    first = null;
                    last = null;
                }
                else if (node == first)
                {
                    first = first.Next;
                }
                else if (node == last)
                {
                    last = last.Prev;
                }
                else
                {
                    node.Prev.Next = node.Next;
                    node.Next.Prev = node.Prev;
                }
                count--;
            }
        }
    }
}
