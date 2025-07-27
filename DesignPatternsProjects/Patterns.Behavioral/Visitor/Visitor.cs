using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.Visitor
{
    /// <summary>
    /// Visitor - Interface définissant les méthodes de visite pour chaque type d'élément
    /// </summary>
    public interface IDocumentVisitor
    {
        void Visit(TextElement element);
        void Visit(ImageElement element);
        void Visit(TableElement element);
        void Visit(HyperlinkElement element);
        void Visit(Document document);
    }

    /// <summary>
    /// Element - Interface définissant une méthode pour accepter un visiteur
    /// </summary>
    public interface IDocumentElement
    {
        void Accept(IDocumentVisitor visitor);
        string GetContent();
    }

    /// <summary>
    /// ConcreteElement - Élément de texte dans un document
    /// </summary>
    public class TextElement : IDocumentElement
    {
        public string Text { get; private set; }
        public bool IsBold { get; private set; }
        public bool IsItalic { get; private set; }

        public TextElement(string text, bool isBold = false, bool isItalic = false)
        {
            Text = text;
            IsBold = isBold;
            IsItalic = isItalic;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetContent()
        {
            string formattedText = Text;
            if (IsBold) formattedText = $"**{formattedText}**";
            if (IsItalic) formattedText = $"*{formattedText}*";
            return formattedText;
        }
    }

    /// <summary>
    /// ConcreteElement - Élément image dans un document
    /// </summary>
    public class ImageElement : IDocumentElement
    {
        public string Source { get; private set; }
        public string AltText { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ImageElement(string source, string altText, int width = 0, int height = 0)
        {
            Source = source;
            AltText = altText;
            Width = width;
            Height = height;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetContent()
        {
            return $"[Image: {AltText} ({Source})]";
        }
    }

    /// <summary>
    /// ConcreteElement - Élément tableau dans un document
    /// </summary>
    public class TableElement : IDocumentElement
    {
        public List<string[]> Rows { get; private set; }
        public string[] Headers { get; private set; }

        public TableElement(string[] headers)
        {
            Headers = headers;
            Rows = new List<string[]>();
        }

        public void AddRow(string[] cells)
        {
            if (cells.Length != Headers.Length)
            {
                throw new ArgumentException("Le nombre de cellules doit correspondre au nombre d'en-têtes");
            }
            Rows.Add(cells);
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetContent()
        {
            return $"[Tableau avec {Rows.Count} lignes et {Headers.Length} colonnes]";
        }
    }

    /// <summary>
    /// ConcreteElement - Élément lien hypertexte dans un document
    /// </summary>
    public class HyperlinkElement : IDocumentElement
    {
        public string Url { get; private set; }
        public string Text { get; private set; }

        public HyperlinkElement(string url, string text)
        {
            Url = url;
            Text = text;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetContent()
        {
            return $"[{Text}]({Url})";
        }
    }

    /// <summary>
    /// ObjectStructure - Structure composite qui contient les éléments à visiter
    /// </summary>
    public class Document : IDocumentElement
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        private readonly List<IDocumentElement> _elements = new List<IDocumentElement>();

        public Document(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public void AddElement(IDocumentElement element)
        {
            _elements.Add(element);
        }

        public void Accept(IDocumentVisitor visitor)
        {
            // Visite d'abord le document lui-même
            visitor.Visit(this);
            
            // Puis visite chaque élément du document
            foreach (var element in _elements)
            {
                element.Accept(visitor);
            }
        }

        public string GetContent()
        {
            return $"Document: {Title} par {Author}";
        }

        public IReadOnlyList<IDocumentElement> GetElements()
        {
            return _elements.AsReadOnly();
        }
    }

    /// <summary>
    /// ConcreteVisitor - Visiteur qui exporte le document au format HTML
    /// </summary>
    public class HtmlExportVisitor : IDocumentVisitor
    {
        private string _result = "";

        public void Visit(TextElement element)
        {
            string content = element.Text;
            if (element.IsBold) content = $"<strong>{content}</strong>";
            if (element.IsItalic) content = $"<em>{content}</em>";
            _result += $"<p>{content}</p>\n";
        }

        public void Visit(ImageElement element)
        {
            string sizeAttributes = "";
            if (element.Width > 0 && element.Height > 0)
            {
                sizeAttributes = $" width=\"{element.Width}\" height=\"{element.Height}\"";
            }
            _result += $"<img src=\"{element.Source}\" alt=\"{element.AltText}\"{sizeAttributes} />\n";
        }

        public void Visit(TableElement element)
        {
            _result += "<table border=\"1\">\n";
            
            // En-têtes de tableau
            _result += "  <tr>\n";
            foreach (var header in element.Headers)
            {
                _result += $"    <th>{header}</th>\n";
            }
            _result += "  </tr>\n";
            
            // Lignes de données
            foreach (var row in element.Rows)
            {
                _result += "  <tr>\n";
                foreach (var cell in row)
                {
                    _result += $"    <td>{cell}</td>\n";
                }
                _result += "  </tr>\n";
            }
            
            _result += "</table>\n";
        }

        public void Visit(HyperlinkElement element)
        {
            _result += $"<a href=\"{element.Url}\">{element.Text}</a>\n";
        }

        public void Visit(Document document)
        {
            _result = $"<!DOCTYPE html>\n<html>\n<head>\n  <title>{document.Title}</title>\n";
            _result += "  <meta charset=\"UTF-8\">\n";
            _result += $"  <meta name=\"author\" content=\"{document.Author}\">\n";
            _result += "</head>\n<body>\n";
            _result += $"<h1>{document.Title}</h1>\n";
            _result += $"<p><em>Par {document.Author}</em></p>\n";
        }

        public string GetResult()
        {
            return _result + "</body>\n</html>";
        }
    }

    /// <summary>
    /// ConcreteVisitor - Visiteur qui exporte le document au format Markdown
    /// </summary>
    public class MarkdownExportVisitor : IDocumentVisitor
    {
        private string _result = "";

        public void Visit(TextElement element)
        {
            string content = element.Text;
            if (element.IsBold) content = $"**{content}**";
            if (element.IsItalic) content = $"*{content}*";
            _result += content + "\n\n";
        }

        public void Visit(ImageElement element)
        {
            _result += $"![{element.AltText}]({element.Source})\n\n";
        }

        public void Visit(TableElement element)
        {
            // En-têtes de tableau
            _result += "| ";
            _result += string.Join(" | ", element.Headers);
            _result += " |\n";
            
            // Ligne de séparation
            _result += "| ";
            for (int i = 0; i < element.Headers.Length; i++)
            {
                _result += "--- | ";
            }
            _result = _result.TrimEnd(' ', '|') + " |\n";
            
            // Lignes de données
            foreach (var row in element.Rows)
            {
                _result += "| ";
                _result += string.Join(" | ", row);
                _result += " |\n";
            }
            
            _result += "\n";
        }

        public void Visit(HyperlinkElement element)
        {
            _result += $"[{element.Text}]({element.Url})\n\n";
        }

        public void Visit(Document document)
        {
            _result = $"# {document.Title}\n\n";
            _result += $"*Par {document.Author}*\n\n";
        }

        public string GetResult()
        {
            return _result;
        }
    }

    /// <summary>
    /// ConcreteVisitor - Visiteur qui compte les statistiques du document
    /// </summary>
    public class StatisticsVisitor : IDocumentVisitor
    {
        public int TextElementCount { get; private set; }
        public int ImageElementCount { get; private set; }
        public int TableElementCount { get; private set; }
        public int HyperlinkElementCount { get; private set; }
        public int TotalWordCount { get; private set; }
        public int TotalCharacterCount { get; private set; }

        public void Visit(TextElement element)
        {
            TextElementCount++;
            
            // Compter les mots et caractères
            TotalWordCount += CountWords(element.Text);
            TotalCharacterCount += element.Text.Length;
        }

        public void Visit(ImageElement element)
        {
            ImageElementCount++;
            
            // Compter les mots dans le texte alternatif
            TotalWordCount += CountWords(element.AltText);
        }

        public void Visit(TableElement element)
        {
            TableElementCount++;
            
            // Compter les mots dans les en-têtes
            foreach (var header in element.Headers)
            {
                TotalWordCount += CountWords(header);
                TotalCharacterCount += header.Length;
            }
            
            // Compter les mots dans les cellules
            foreach (var row in element.Rows)
            {
                foreach (var cell in row)
                {
                    TotalWordCount += CountWords(cell);
                    TotalCharacterCount += cell.Length;
                }
            }
        }

        public void Visit(HyperlinkElement element)
        {
            HyperlinkElementCount++;
            
            // Compter les mots dans le texte du lien
            TotalWordCount += CountWords(element.Text);
            TotalCharacterCount += element.Text.Length;
        }

        public void Visit(Document document)
        {
            // Compter les mots dans le titre et l'auteur
            TotalWordCount += CountWords(document.Title) + CountWords(document.Author);
            TotalCharacterCount += document.Title.Length + document.Author.Length;
        }

        private static int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;
                
            // Diviser le texte par espaces et compter les parties non vides
            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public string GetSummary()
        {
            return $"Statistiques du document :\n" +
                   $"- Éléments de texte : {TextElementCount}\n" +
                   $"- Images : {ImageElementCount}\n" +
                   $"- Tableaux : {TableElementCount}\n" +
                   $"- Liens hypertexte : {HyperlinkElementCount}\n" +
                   $"- Nombre total d'éléments : {TextElementCount + ImageElementCount + TableElementCount + HyperlinkElementCount}\n" +
                   $"- Nombre total de mots : {TotalWordCount}\n" +
                   $"- Nombre total de caractères : {TotalCharacterCount}";
        }
    }
}


