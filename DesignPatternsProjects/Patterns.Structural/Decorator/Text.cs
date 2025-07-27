using System;

namespace Patterns.Structural.Decorator
{
    /// <summary>
    /// Component - IText
    /// </summary>
    public interface IText
    {
        string GetContent();
    }

    /// <summary>
    /// Concrete Component - SimpleText
    /// </summary>
    public class SimpleText : IText
    {
        private readonly string _content;

        public SimpleText(string content)
        {
            _content = content;
        }

        public string GetContent()
        {
            return _content;
        }
    }

    /// <summary>
    /// Decorator - TextDecorator
    /// </summary>
    public abstract class TextDecorator : IText
    {
        protected readonly IText _text;

        protected TextDecorator(IText text)
        {
            _text = text;
        }

        public virtual string GetContent()
        {
            return _text.GetContent();
        }
    }

    /// <summary>
    /// Concrete Decorator - BoldDecorator
    /// </summary>
    public class BoldDecorator : TextDecorator
    {
        public BoldDecorator(IText text) : base(text)
        {
        }

        public override string GetContent()
        {
            return $"<b>{base.GetContent()}</b>";
        }
    }

    /// <summary>
    /// Concrete Decorator - ItalicDecorator
    /// </summary>
    public class ItalicDecorator : TextDecorator
    {
        public ItalicDecorator(IText text) : base(text)
        {
        }

        public override string GetContent()
        {
            return $"<i>{base.GetContent()}</i>";
        }
    }

    /// <summary>
    /// Concrete Decorator - UnderlineDecorator
    /// </summary>
    public class UnderlineDecorator : TextDecorator
    {
        public UnderlineDecorator(IText text) : base(text)
        {
        }

        public override string GetContent()
        {
            return $"<u>{base.GetContent()}</u>";
        }
    }

    /// <summary>
    /// Concrete Decorator - ColorDecorator
    /// </summary>
    public class ColorDecorator : TextDecorator
    {
        private readonly string _color;

        public ColorDecorator(IText text, string color) : base(text)
        {
            _color = color;
        }

        public override string GetContent()
        {
            return $"<span style=\"color: {_color}\">{base.GetContent()}</span>";
        }
    }
}
