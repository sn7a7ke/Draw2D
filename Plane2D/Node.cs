using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane2D
{
    //public class Node<T> : IEnumerable<T>// : ICloneable
    //                                  //where T : ICloneable
    //{
    //    public T Value { get; private set; }
    //    public Node(T value) => Value = value;
    //    public Node<T> Next { get; set; }
    //    public Node<T> Previous { get; set; }

    //    public IEnumerator GetEnumerator()
    //    {
    //        Node<T> currentNode = this;
    //        do
    //        {
    //            yield return currentNode;
    //            currentNode = currentNode.Next;
    //        } while (currentNode!=null && currentNode != this);//} while (currentNode != this); //если кольцевой список
    //    }

    //    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    //    {
    //        Node<T> currentNode = this;
    //        do
    //        {
    //            yield return currentNode.Value;
    //            currentNode = currentNode.Next;
    //        } while (currentNode != null && currentNode != this);//} while (currentNode != this); //если кольцевой список
    //    }

    //    //public object Clone() => new Node<T>((T)Value.Clone());
    //}
}
