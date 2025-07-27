using System.Collections;
using System.Collections.Generic;

namespace Patterns.Behavioral.Iterator
{
    /// <summary>
    /// Stratégies de parcours disponibles pour l'arbre binaire
    /// </summary>
    public enum TraversalStrategy
    {
        PreOrder,    // Racine-Gauche-Droite
        InOrder,     // Gauche-Racine-Droite
        PostOrder,   // Gauche-Droite-Racine
        LevelOrder   // Niveau par niveau
    }

    /// <summary>
    /// Interface Iterator générique pour l'arbre binaire
    /// </summary>
    public interface ITreeIterator<T> : IEnumerator<T>
    {
    }

    /// <summary>
    /// Nœud de l'arbre binaire
    /// </summary>
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Arbre binaire qui implémente IEnumerable pour permettre l'utilisation de foreach
    /// </summary>
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> _root;

        public void Add(T value)
        {
            if (_root == null)
            {
                _root = new Node<T>(value);
                return;
            }

            var current = _root;
            while (true)
            {
                if (value.CompareTo(current.Value) < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node<T>(value);
                        break;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node<T>(value);
                        break;
                    }
                    current = current.Right;
                }
            }
        }

        public ITreeIterator<T> GetIterator(TraversalStrategy strategy)
        {
            return strategy switch
            {
                TraversalStrategy.PreOrder => new PreOrderIterator<T>(_root),
                TraversalStrategy.InOrder => new InOrderIterator<T>(_root),
                TraversalStrategy.PostOrder => new PostOrderIterator<T>(_root),
                TraversalStrategy.LevelOrder => new LevelOrderIterator<T>(_root),
                _ => new InOrderIterator<T>(_root)
            };
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetIterator(TraversalStrategy.InOrder);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Itérateur pour le parcours préfixe (PreOrder)
    /// </summary>
    public class PreOrderIterator<T> : ITreeIterator<T>
    {
        private readonly Node<T> _root;
        private readonly Stack<Node<T>> _stack;
        private Node<T> _current;

        public PreOrderIterator(Node<T> root)
        {
            _root = root;
            _stack = new Stack<Node<T>>();
            Reset();
        }

        public bool MoveNext()
        {
            if (_stack.Count == 0) return false;

            _current = _stack.Pop();
            
            if (_current.Right != null)
                _stack.Push(_current.Right);
            if (_current.Left != null)
                _stack.Push(_current.Left);

            return true;
        }

        public void Reset()
        {
            _stack.Clear();
            if (_root != null)
                _stack.Push(_root);
            _current = null;
        }

        public T Current => _current.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }

    /// <summary>
    /// Itérateur pour le parcours infixe (InOrder)
    /// </summary>
    public class InOrderIterator<T> : ITreeIterator<T>
    {
        private readonly Node<T> _root;
        private readonly Stack<Node<T>> _stack;
        private Node<T> _current;

        public InOrderIterator(Node<T> root)
        {
            _root = root;
            _stack = new Stack<Node<T>>();
            Reset();
        }

        public bool MoveNext()
        {
            while (_stack.Count > 0 || _current != null)
            {
                if (_current != null)
                {
                    _stack.Push(_current);
                    _current = _current.Left;
                }
                else
                {
                    _current = _stack.Pop();
                    var result = _current;
                    _current = _current.Right;
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            _stack.Clear();
            _current = _root;
        }

        public T Current => _current.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }

    /// <summary>
    /// Itérateur pour le parcours postfixe (PostOrder)
    /// </summary>
    public class PostOrderIterator<T> : ITreeIterator<T>
    {
        private readonly Node<T> _root;
        private readonly Stack<Node<T>> _stack;
        private Node<T> _lastNodeVisited;
        private Node<T> _current;

        public PostOrderIterator(Node<T> root)
        {
            _root = root;
            _stack = new Stack<Node<T>>();
            Reset();
        }

        public bool MoveNext()
        {
            while (_stack.Count > 0)
            {
                var node = _stack.Peek();
                if (node.Left != null && node.Left != _lastNodeVisited && node.Right != _lastNodeVisited)
                {
                    _stack.Push(node.Left);
                }
                else if (node.Right != null && node.Right != _lastNodeVisited)
                {
                    _stack.Push(node.Right);
                }
                else
                {
                    _current = _stack.Pop();
                    _lastNodeVisited = _current;
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            _stack.Clear();
            if (_root != null)
                _stack.Push(_root);
            _lastNodeVisited = null;
            _current = null;
        }

        public T Current => _current.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }

    /// <summary>
    /// Itérateur pour le parcours par niveau (LevelOrder)
    /// </summary>
    public class LevelOrderIterator<T> : ITreeIterator<T>
    {
        private readonly Node<T> _root;
        private readonly Queue<Node<T>> _queue;
        private Node<T> _current;

        public LevelOrderIterator(Node<T> root)
        {
            _root = root;
            _queue = new Queue<Node<T>>();
            Reset();
        }

        public bool MoveNext()
        {
            if (_queue.Count == 0) return false;

            _current = _queue.Dequeue();
            
            if (_current.Left != null)
                _queue.Enqueue(_current.Left);
            if (_current.Right != null)
                _queue.Enqueue(_current.Right);

            return true;
        }

        public void Reset()
        {
            _queue.Clear();
            if (_root != null)
                _queue.Enqueue(_root);
            _current = null;
        }

        public T Current => _current.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}
