using System;

namespace Patterns.Behavioral.TemplateMethod
{
    /// <summary>
    /// AbstractClass - Définit le squelette d'un algorithme dans une méthode,
    /// reportant certaines étapes aux sous-classes.
    /// </summary>
    public abstract class DocumentGenerator
    {
        /// <summary>
        /// La "méthode template" définit le squelette de l'algorithme.
        /// </summary>
        public void GenerateDocument()
        {
            Console.WriteLine("Début de la génération du document...");
            
            // Étapes de l'algorithme qui sont communes à tous les types de documents
            CreateHeader();
            
            // Hook - peut être surchargé par les sous-classes, mais a une implémentation par défaut
            if (IncludeTableOfContents())
            {
                CreateTableOfContents();
            }
            
            // Opérations abstraites - doivent être implémentées par les sous-classes
            CreateContent();
            
            // Étapes communes à tous les types de documents
            CreateFooter();
            
            // Hook - avec une implémentation par défaut
            if (IncludeAppendix())
            {
                CreateAppendix();
            }
            
            Console.WriteLine("Fin de la génération du document.\n");
        }
        
        // Opérations concrètes avec implémentation par défaut
        protected virtual void CreateHeader()
        {
            Console.WriteLine("Création de l'en-tête standard du document");
        }
        
        protected virtual void CreateTableOfContents()
        {
            Console.WriteLine("Création de la table des matières standard");
        }
        
        protected virtual void CreateFooter()
        {
            Console.WriteLine("Création du pied de page standard avec numéro de page");
        }
        
        protected virtual void CreateAppendix()
        {
            Console.WriteLine("Création d'annexes standard");
        }
        
        // Opérations abstraites - doivent être implémentées par les sous-classes
        protected abstract void CreateContent();
        
        // Méthodes hook - peuvent être surchargées par les sous-classes
        // Elles fournissent un comportement par défaut mais peuvent être redéfinies
        protected virtual bool IncludeTableOfContents()
        {
            return true; // Par défaut, inclut une table des matières
        }
        
        protected virtual bool IncludeAppendix()
        {
            return false; // Par défaut, n'inclut pas d'annexes
        }
    }

    /// <summary>
    /// ConcreteClass - Implémente les opérations primitives pour réaliser
    /// les étapes spécifiques de l'algorithme
    /// </summary>
    public class ReportGenerator : DocumentGenerator
    {
        protected override void CreateHeader()
        {
            Console.WriteLine("Création d'un en-tête de rapport avec logo de l'entreprise");
        }
        
        protected override void CreateContent()
        {
            Console.WriteLine("Création du contenu du rapport avec graphiques et tableaux");
        }
        
        protected override void CreateFooter()
        {
            Console.WriteLine("Création d'un pied de page de rapport avec date et numéro de page");
        }
        
        // Ne redéfinit pas IncludeTableOfContents() - utilise l'implémentation par défaut (true)
        
        protected override bool IncludeAppendix()
        {
            return true; // Les rapports incluent des annexes
        }
        
        protected override void CreateAppendix()
        {
            Console.WriteLine("Création d'annexes avec données supplémentaires pour le rapport");
        }
    }
    
    /// <summary>
    /// ConcreteClass - Une autre implémentation concrète avec ses propres spécificités
    /// </summary>
    public class EmailGenerator : DocumentGenerator
    {
        protected override void CreateHeader()
        {
            Console.WriteLine("Création d'un en-tête d'e-mail avec adresse et objet");
        }
        
        protected override void CreateContent()
        {
            Console.WriteLine("Création du contenu de l'e-mail avec message principal");
        }
        
        protected override bool IncludeTableOfContents()
        {
            return false; // Les e-mails n'incluent pas de table des matières
        }
        
        // Ne redéfinit pas IncludeAppendix() - utilise l'implémentation par défaut (false)
    }
    
    /// <summary>
    /// ConcreteClass - Une troisième implémentation concrète
    /// </summary>
    public class ResearchPaperGenerator : DocumentGenerator
    {
        protected override void CreateHeader()
        {
            Console.WriteLine("Création d'un en-tête d'article de recherche avec titre, auteurs et affiliation");
        }
        
        protected override void CreateTableOfContents()
        {
            Console.WriteLine("Création d'une table des matières détaillée pour l'article de recherche");
        }
        
        protected override void CreateContent()
        {
            Console.WriteLine("Création du contenu de l'article avec résumé, introduction, méthodes, résultats et discussion");
        }
        
        protected override void CreateFooter()
        {
            Console.WriteLine("Création d'un pied de page d'article avec références bibliographiques");
        }
        
        protected override bool IncludeAppendix()
        {
            return true; // Les articles de recherche incluent souvent des annexes
        }
        
        protected override void CreateAppendix()
        {
            Console.WriteLine("Création d'annexes avec données supplémentaires et matériels de recherche");
        }
    }
}


