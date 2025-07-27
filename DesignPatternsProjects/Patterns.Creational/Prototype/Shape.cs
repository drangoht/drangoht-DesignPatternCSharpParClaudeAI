using System;

namespace Patterns.Creational.Prototype
{
    /// <summary>
    /// Classe abstraite de base pour tous les prototypes.
    /// </summary>
    public abstract class Shape : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }

        /// <summary>
        /// Constructeur normal
        /// </summary>
        public Shape()
        {
        }

        /// <summary>
        /// Constructeur de prototype qui copie les valeurs depuis une autre instance
        /// </summary>
        public Shape(Shape source)
        {
            if (source != null)
            {
                this.X = source.X;
                this.Y = source.Y;
                this.Color = source.Color;
            }
        }

        /// <summary>
        /// Méthode de clonage qui est implémentée par chaque sous-classe concrète
        /// </summary>
        public abstract object Clone();

        public override string ToString()
        {
            return $"{this.GetType().Name} [X={X}, Y={Y}, Color={Color}]";
        }
    }

    /// <summary>
    /// Classe concrète de prototype Rectangle
    /// </summary>
    public class Rectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle()
        {
        }

        /// <summary>
        /// Constructeur de prototype qui clone depuis une autre instance de Rectangle
        /// </summary>
        public Rectangle(Rectangle source) : base(source)
        {
            if (source != null)
            {
                this.Width = source.Width;
                this.Height = source.Height;
            }
        }

        public override object Clone()
        {
            return new Rectangle(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Width={Width}, Height={Height}";
        }
    }

    /// <summary>
    /// Classe concrète de prototype Circle
    /// </summary>
    public class Circle : Shape
    {
        public int Radius { get; set; }

        public Circle()
        {
        }

        /// <summary>
        /// Constructeur de prototype qui clone depuis une autre instance de Circle
        /// </summary>
        public Circle(Circle source) : base(source)
        {
            if (source != null)
            {
                this.Radius = source.Radius;
            }
        }

        public override object Clone()
        {
            return new Circle(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Radius={Radius}";
        }
    }
}


