using System;
using System.Threading;

namespace Patterns.Structural.Proxy
{
    /// <summary>
    /// Interface Subject - Définit l'interface commune pour RealSubject et Proxy
    /// afin que le Proxy puisse être utilisé partout où le RealSubject est attendu
    /// </summary>
    public interface IImage
    {
        void Display();
        string GetFilename();
        int GetSize();
    }

    /// <summary>
    /// RealSubject - Définit l'objet réel que le Proxy représente
    /// </summary>
    public class RealImage : IImage
    {
        private readonly string _filename;
        private readonly int _size;
        
        public RealImage(string filename, int size)
        {
            _filename = filename;
            _size = size;
            LoadImageFromDisk();
        }

        /// <summary>
        /// Méthode coûteuse simulant le chargement d'une image depuis le disque
        /// </summary>
        private void LoadImageFromDisk()
        {
            Console.WriteLine($"Chargement de l'image {_filename} depuis le disque...");
            // Simulation d'un chargement coûteux
            Thread.Sleep(1000); 
            Console.WriteLine($"Image {_filename} chargée avec succès!");
        }

        public void Display()
        {
            Console.WriteLine($"Affichage de l'image {_filename} ({_size} KB)");
        }

        public string GetFilename()
        {
            return _filename;
        }

        public int GetSize()
        {
            return _size;
        }
    }

    /// <summary>
    /// Proxy - Maintient une référence qui permet au proxy d'accéder à l'objet réel
    /// Le Proxy peut être responsable de la création et de la suppression du RealSubject
    /// </summary>
    public class ImageProxy : IImage
    {
        private readonly string _filename;
        private readonly int _size;
        
        // Le RealSubject n'est créé que lorsqu'il est vraiment nécessaire (chargement paresseux)
        private RealImage _realImage;

        public ImageProxy(string filename, int size)
        {
            _filename = filename;
            _size = size;
            Console.WriteLine($"Proxy créé pour l'image {_filename} sans charger l'image réelle");
        }

        /// <summary>
        /// Crée le RealSubject seulement lorsqu'il est nécessaire
        /// </summary>
        public void Display()
        {
            // Charge l'image réelle seulement au moment de l'afficher
            if (_realImage == null)
            {
                _realImage = new RealImage(_filename, _size);
            }
            
            // Délégation au RealSubject
            _realImage.Display();
        }

        public string GetFilename()
        {
            return _filename;
        }

        public int GetSize()
        {
            return _size;
        }
    }

    /// <summary>
    /// Proxy - Implémente un chargement paresseux (lazy loading)
    /// </summary>
    public class LazyImageProxy : IImage
    {
        private readonly string _filename;
        private readonly int _size;
        private RealImage? _realImage;

        public LazyImageProxy(string filename, int size)
        {
            _filename = filename;
            _size = size;
        }

        public void Display()
        {
            // Crée le RealSubject seulement lors de la première utilisation
            if (_realImage == null)
            {
                _realImage = new RealImage(_filename, _size);
            }
            _realImage.Display();
        }

        public string GetFilename()
        {
            return _filename;
        }

        public int GetSize()
        {
            return _size;
        }
    }

    /// <summary>
    /// Un autre type de Proxy - ProtectionProxy qui ajoute un contrôle d'accès
    /// </summary>
    public class ProtectedImageProxy : IImage
    {
        private readonly string _filename;
        private readonly int _size;
        private readonly string _userRole;
        private RealImage? _realImage;

        public ProtectedImageProxy(string filename, int size, string userRole)
        {
            _filename = filename;
            _size = size;
            _userRole = userRole;
        }

        public void Display()
        {
            // Vérifie les autorisations avant d'accéder à l'objet réel
            if (_userRole != "admin" && _filename.Contains("confidential"))
            {
                Console.WriteLine($"Accès refusé à l'image {_filename}. Rôle requis: admin");
                return;
            }

            // Crée et utilise le RealSubject seulement si l'accès est autorisé
            if (_realImage == null)
            {
                _realImage = new RealImage(_filename, _size);
            }
            
            _realImage.Display();
        }

        public string GetFilename()
        {
            return _filename;
        }

        public int GetSize()
        {
            return _size;
        }
    }
}


