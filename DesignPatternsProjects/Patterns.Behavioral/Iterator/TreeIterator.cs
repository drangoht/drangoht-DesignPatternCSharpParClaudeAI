using System;
using System.Collections;
using System.Collections.Generic;

namespace Patterns.Behavioral.Iterator
{
    /// <summary>
    /// Interface Iterator générique
    /// </summary>
    public interface ITreeIterator<out T> : IEnumerator<T>
    {
        TraversalStrategy Strategy { get; }
    }

    /// <summary>
    /// Stratégies de parcours disponibles
    /// </summary>
    public enum TraversalStrategy
    {
        PreOrder,   // Racine, Gauche, Droite
        InOrder,    // Gauche, Racine, Droite
        PostOrder,  // Gauche, Droite, Racine
        LevelOrder  // Niveau par niveau
    }

    /// <summary>
    /// Nœud de l'arbre binaire
    /// </summary>
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public TreeNode<T>? Left { get; set; }
        public TreeNode<T>? Right { get; set; }

        public TreeNode(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? string.Empty;
        }
    }

    /// <summary>
    /// Arbre binaire avec différentes stratégies de parcours
    /// </summary>
    public class BinaryTree<T> : IEnumerable<T>
    {
        private TreeNode<T>? _root;
        private readonly TraversalStrategy _defaultStrategy;

        public BinaryTree(TraversalStrategy defaultStrategy = TraversalStrategy.InOrder)
        {
            _defaultStrategy = defaultStrategy;
        }

        public void Add(T value)
        {
            if (_root == null)
            {
                _root = new TreeNode<T>(value);
                return;
            }

            // Ajoute de manière équilibrée niveau par niveau
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.Left == null)
                {
                    current.Left = new TreeNode<T>(value);
                    return;
                }
                
                if (current.Right == null)
                {
                    current.Right = new TreeNode<T>(value);
                    return;
                }

                queue.Enqueue(current.Left);
                queue.Enqueue(current.Right);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetIterator(_defaultStrategy);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ITreeIterator<T> GetIterator(TraversalStrategy strategy)
        {
            return strategy switch
            {
                TraversalStrategy.PreOrder => new PreOrderIterator<T>(_root),
                TraversalStrategy.InOrder => new InOrderIterator<T>(_root),
                TraversalStrategy.PostOrder => new PostOrderIterator<T>(_root),
                TraversalStrategy.LevelOrder => new LevelOrderIterator<T>(_root),
                _ => throw new ArgumentException("Stratégie de parcours non supportée")
            };
        }
    }

    /// <summary>
    /// Itérateur pour le parcours préfixe (PreOrder)
    /// </summary>
    public class PreOrderIterator<T> : ITreeIterator<T>
    {
        private readonly TreeNode<T>? _root;
        private readonly Stack<TreeNode<T>> _stack;
        private TreeNode<T>? _current;
        private bool _disposed;

        public PreOrderIterator(TreeNode<T>? root)
        {
            _root = root;
            _stack = new Stack<TreeNode<T>>();
            Reset();
        }

        public TraversalStrategy Strategy => TraversalStrategy.PreOrder;

        public bool MoveNext()
        {
            ThrowIfDisposed();
            if (_stack.Count == 0) return false;

            _current = _stack.Pop();

            // Empile d'abord droite puis gauche (pour traiter gauche en premier)
            if (_current.Right != null)
                _stack.Push(_current.Right);
            if (_current.Left != null)
                _stack.Push(_current.Left);

            return true;
        }

        public void Reset()
        {
            ThrowIfDisposed();
            _stack.Clear();
            if (_root != null)
                _stack.Push(_root);
            _current = null;
        }

        public T Current
        {
            get
            {
                ThrowIfDisposed();
                if (_current == null) throw new InvalidOperationException();
                return _current.Value;
            }
        }

        object IEnumerator.Current => Current!;

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _stack.Clear();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Itérateur pour le parcours infixe (InOrder)
    /// </summary>
    public class InOrderIterator<T> : ITreeIterator<T>
    {
        private readonly TreeNode<T>? _root;
        private readonly Stack<TreeNode<T>> _stack;
        private TreeNode<T>? _current;
        private bool _disposed;

        public InOrderIterator(TreeNode<T>? root)
        {
            _root = root;
            _stack = new Stack<TreeNode<T>>();
            Reset();
        }

        public TraversalStrategy Strategy => TraversalStrategy.InOrder;

        public bool MoveNext()
        {
            ThrowIfDisposed();
            if (_stack.Count == 0 && _current == null) return false;

            // Parcours tous les nœuds gauches
            while (_current != null)
            {
                _stack.Push(_current);
                _current = _current.Left;
            }

            if (_stack.Count > 0)
            {
                _current = _stack.Pop();
                _current = _current.Right;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            ThrowIfDisposed();
            _stack.Clear();
            _current = _root;
        }

        public T Current
        {
            get
            {
                ThrowIfDisposed();
                if (_current == null) throw new InvalidOperationException();
                return _current.Value;
            }
        }

        object IEnumerator.Current => Current!;

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _stack.Clear();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Itérateur pour le parcours postfixe (PostOrder)
    /// </summary>
    public class PostOrderIterator<T> : ITreeIterator<T>
    {
        private readonly TreeNode<T>? _root;
        private readonly Stack<TreeNode<T>> _stack;
        private TreeNode<T>? _current;
        private TreeNode<T>? _lastVisited;
        private bool _disposed;

        public PostOrderIterator(TreeNode<T>? root)
        {
            _root = root;
            _stack = new Stack<TreeNode<T>>();
            Reset();
        }

        public TraversalStrategy Strategy => TraversalStrategy.PostOrder;

        public bool MoveNext()
        {
            ThrowIfDisposed();
            while (_stack.Count > 0)
            {
                _current = _stack.Peek();

                // Si on descend ou qu'on n'a pas de fils
                if (_lastVisited == null || 
                    _lastVisited == _current.Left ||
                    _lastVisited == _current.Right)
                {
                    if (_current.Left != null)
                        _stack.Push(_current.Left);
                    else if (_current.Right != null)
                        _stack.Push(_current.Right);
                    else
                    {
                        _stack.Pop();
                        _lastVisited = _current;
                        return true;
                    }
                }
                // Si on remonte du fils gauche
                else if (_current.Left == _lastVisited)
                {
                    if (_current.Right != null)
                        _stack.Push(_current.Right);
                    else
                    {
                        _stack.Pop();
                        _lastVisited = _current;
                        return true;
                    }
                }
                // Si on remonte du fils droit
                else
                {
                    _stack.Pop();
                    _lastVisited = _current;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            ThrowIfDisposed();
            _stack.Clear();
            if (_root != null)
                _stack.Push(_root);
            _lastVisited = null;
        }

        public T Current
        {
            get
            {
                ThrowIfDisposed();
                if (_current == null) throw new InvalidOperationException();
                return _current.Value;
            }
        }

        object IEnumerator.Current => Current!;

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _stack.Clear();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Itérateur pour le parcours par niveau (LevelOrder)
    /// </summary>
    public class LevelOrderIterator<T> : ITreeIterator<T>
    {
        private readonly TreeNode<T>? _root;
        private readonly Queue<TreeNode<T>> _queue;
        private TreeNode<T>? _current;
        private bool _disposed;

        public LevelOrderIterator(TreeNode<T>? root)
        {
            _root = root;
            _queue = new Queue<TreeNode<T>>();
            Reset();
        }

        public TraversalStrategy Strategy => TraversalStrategy.LevelOrder;

        public bool MoveNext()
        {
            ThrowIfDisposed();
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
            ThrowIfDisposed();
            _queue.Clear();
            if (_root != null)
                _queue.Enqueue(_root);
            _current = null;
        }

        public T Current
        {
            get
            {
                ThrowIfDisposed();
                if (_current == null) throw new InvalidOperationException();
                return _current.Value;
            }
        }

        object IEnumerator.Current => Current!;

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _queue.Clear();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
