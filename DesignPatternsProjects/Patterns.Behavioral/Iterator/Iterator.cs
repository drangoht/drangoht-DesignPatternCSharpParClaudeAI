using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.Iterator
{
    /// <summary>
    /// Iterator - Interface définissant les méthodes pour parcourir une collection
    /// </summary>
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
    }

    /// <summary>
    /// Aggregate - Interface définissant la méthode pour créer un itérateur
    /// </summary>
    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }

    /// <summary>
    /// ConcreteAggregate - Implémentation concrète de la collection avec itérateur personnalisé
    /// </summary>
    public class BookCollection : IAggregate<Book>
    {
        private readonly List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public int Count => _books.Count;

        public Book GetBook(int index)
        {
            return _books[index];
        }

        public IIterator<Book> CreateIterator()
        {
            return new BookIterator(this);
        }
        
        // Itérateur spécialisé qui parcourt seulement les livres d'un genre spécifique
        public IIterator<Book> CreateGenreIterator(string genre)
        {
            return new GenreIterator(this, genre);
        }
        
        // Itérateur qui parcourt les livres en ordre inverse
        public IIterator<Book> CreateReverseIterator()
        {
            return new ReverseIterator(this);
        }
    }

    /// <summary>
    /// ConcreteIterator - Implémentation concrète de l'itérateur
    /// </summary>
    public class BookIterator : IIterator<Book>
    {
        private readonly BookCollection _collection;
        private int _currentIndex = 0;

        public BookIterator(BookCollection collection)
        {
            _collection = collection;
        }

        public bool HasNext()
        {
            return _currentIndex < _collection.Count;
        }

        public Book Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("Fin de la collection atteinte");
            }
            
            return _collection.GetBook(_currentIndex++);
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }
    
    /// <summary>
    /// Itérateur spécialisé qui filtre les livres par genre
    /// </summary>
    public class GenreIterator : IIterator<Book>
    {
        private readonly BookCollection _collection;
        private readonly string _genre;
        private int _currentIndex = 0;

        public GenreIterator(BookCollection collection, string genre)
        {
            _collection = collection;
            _genre = genre;
            // Positionnement sur le premier livre du genre
            MoveToNextValidBook();
        }

        // Avance jusqu'au prochain livre du genre recherché
        private void MoveToNextValidBook()
        {
            while (_currentIndex < _collection.Count && 
                   _collection.GetBook(_currentIndex).Genre != _genre)
            {
                _currentIndex++;
            }
        }

        public bool HasNext()
        {
            return _currentIndex < _collection.Count;
        }

        public Book Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException($"Plus de livres du genre {_genre}");
            }
            
            Book book = _collection.GetBook(_currentIndex++);
            MoveToNextValidBook(); // Se positionne sur le prochain livre du genre
            return book;
        }

        public void Reset()
        {
            _currentIndex = 0;
            MoveToNextValidBook();
        }
    }
    
    /// <summary>
    /// Itérateur qui parcourt la collection en ordre inverse
    /// </summary>
    public class ReverseIterator : IIterator<Book>
    {
        private readonly BookCollection _collection;
        private int _currentIndex;

        public ReverseIterator(BookCollection collection)
        {
            _collection = collection;
            _currentIndex = collection.Count - 1;
        }

        public bool HasNext()
        {
            return _currentIndex >= 0;
        }

        public Book Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("Début de la collection atteint");
            }
            
            return _collection.GetBook(_currentIndex--);
        }

        public void Reset()
        {
            _currentIndex = _collection.Count - 1;
        }
    }

    /// <summary>
    /// Classe représentant un élément de la collection
    /// </summary>
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string Genre { get; }
        public int Year { get; }

        public Book(string title, string author, string genre, int year)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
        }

        public override string ToString()
        {
            return $"\"{Title}\" par {Author} ({Year}) - Genre: {Genre}";
        }
    }
}


