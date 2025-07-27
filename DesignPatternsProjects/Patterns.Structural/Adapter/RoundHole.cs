using System;

namespace Patterns.Structural.Adapter
{
    /// <summary>
    /// Exemple concret: Adapter des chevilles carrées pour des trous ronds
    /// </summary>
    
    /// <summary>
    /// Classe qui représente un trou rond - notre système cible
    /// </summary>
    public class RoundHole
    {
        public double Radius { get; private set; }

        public RoundHole(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Vérifie si un piquet rond peut être inséré dans ce trou
        /// </summary>
        public bool Fits(RoundPeg peg)
        {
            return this.Radius >= peg.GetRadius();
        }
    }

    /// <summary>
    /// Classe qui représente un piquet rond - compatible avec le trou rond
    /// </summary>
    public class RoundPeg
    {
        public double Radius { get; private set; }

        public RoundPeg(double radius)
        {
            Radius = radius;
        }

        public virtual double GetRadius()
        {
            return Radius;
        }
    }

    /// <summary>
    /// Classe qui représente un piquet carré - incompatible avec le trou rond
    /// </summary>
    public class SquarePeg
    {
        public double Width { get; private set; }

        public SquarePeg(double width)
        {
            Width = width;
        }

        public double GetWidth()
        {
            return Width;
        }
    }

    /// <summary>
    /// Adaptateur pour utiliser des piquets carrés dans des trous ronds
    /// </summary>
    public class SquarePegAdapter : RoundPeg
    {
        private SquarePeg _peg;

        public SquarePegAdapter(SquarePeg peg) : base(0)
        {
            _peg = peg;
        }

        /// <summary>
        /// L'adaptateur calcule le rayon minimum du cercle qui peut contenir le piquet carré
        /// </summary>
        public override double GetRadius()
        {
            // Le rayon du cercle circonscrit est égal à la moitié de la diagonale du carré
            return _peg.GetWidth() * Math.Sqrt(2) / 2;
        }
    }
}


