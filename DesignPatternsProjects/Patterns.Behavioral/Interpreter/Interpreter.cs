using System;
using System.Collections.Generic;

namespace Patterns.Behavioral.Interpreter
{
    /// <summary>
    /// Context - Contient les informations globales utilisées par l'interpréteur
    /// </summary>
    public class Context
    {
        private readonly Dictionary<string, int> _variables = new Dictionary<string, int>();

        public void SetVariable(string name, int value)
        {
            if (_variables.ContainsKey(name))
            {
                _variables[name] = value;
            }
            else
            {
                _variables.Add(name, value);
            }
        }

        public int GetVariable(string name)
        {
            if (!_variables.ContainsKey(name))
            {
                throw new ArgumentException($"Variable '{name}' non définie.");
            }
            return _variables[name];
        }

        public void DisplayVariables()
        {
            Console.WriteLine("Variables dans le contexte:");
            foreach (var variable in _variables)
            {
                Console.WriteLine($"  {variable.Key} = {variable.Value}");
            }
        }
    }

    /// <summary>
    /// AbstractExpression - Interface commune pour toutes les expressions
    /// </summary>
    public interface IExpression
    {
        int Interpret(Context context);
        string ToString();
    }

    /// <summary>
    /// TerminalExpression - Expression représentant un nombre entier
    /// </summary>
    public class NumberExpression : IExpression
    {
        private readonly int _number;

        public NumberExpression(int number)
        {
            _number = number;
        }

        public int Interpret(Context context)
        {
            return _number;
        }

        public override string ToString()
        {
            return _number.ToString();
        }
    }

    /// <summary>
    /// TerminalExpression - Expression représentant une variable
    /// </summary>
    public class VariableExpression : IExpression
    {
        private readonly string _name;

        public VariableExpression(string name)
        {
            _name = name;
        }

        public int Interpret(Context context)
        {
            return context.GetVariable(_name);
        }

        public override string ToString()
        {
            return _name;
        }
    }

    /// <summary>
    /// NonterminalExpression - Expression représentant une addition
    /// </summary>
    public class AddExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public AddExpression(IExpression left, IExpression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public int Interpret(Context context)
        {
            return _leftExpression.Interpret(context) + _rightExpression.Interpret(context);
        }

        public override string ToString()
        {
            return $"({_leftExpression} + {_rightExpression})";
        }
    }

    /// <summary>
    /// NonterminalExpression - Expression représentant une soustraction
    /// </summary>
    public class SubtractExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public SubtractExpression(IExpression left, IExpression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public int Interpret(Context context)
        {
            return _leftExpression.Interpret(context) - _rightExpression.Interpret(context);
        }

        public override string ToString()
        {
            return $"({_leftExpression} - {_rightExpression})";
        }
    }

    /// <summary>
    /// NonterminalExpression - Expression représentant une multiplication
    /// </summary>
    public class MultiplyExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public MultiplyExpression(IExpression left, IExpression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public int Interpret(Context context)
        {
            return _leftExpression.Interpret(context) * _rightExpression.Interpret(context);
        }

        public override string ToString()
        {
            return $"({_leftExpression} * {_rightExpression})";
        }
    }

    /// <summary>
    /// NonterminalExpression - Expression représentant une division
    /// </summary>
    public class DivideExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public DivideExpression(IExpression left, IExpression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public int Interpret(Context context)
        {
            int rightValue = _rightExpression.Interpret(context);
            if (rightValue == 0)
            {
                throw new DivideByZeroException("Division par zéro");
            }
            return _leftExpression.Interpret(context) / rightValue;
        }

        public override string ToString()
        {
            return $"({_leftExpression} / {_rightExpression})";
        }
    }

    /// <summary>
    /// NonterminalExpression - Expression représentant une assignation de variable
    /// </summary>
    public class AssignExpression : IExpression
    {
        private readonly string _variableName;
        private readonly IExpression _expression;

        public AssignExpression(string variableName, IExpression expression)
        {
            _variableName = variableName;
            _expression = expression;
        }

        public int Interpret(Context context)
        {
            int value = _expression.Interpret(context);
            context.SetVariable(_variableName, value);
            return value;
        }

        public override string ToString()
        {
            return $"{_variableName} = {_expression}";
        }
    }

    /// <summary>
    /// Parseur simplifié pour le langage d'expressions
    /// </summary>
    public class ExpressionParser
    {
        // Analyse une expression simple de la forme: var = expr ou expr
        public IExpression Parse(string expression)
        {
            expression = expression.Trim();
            
            // Vérifier si c'est une assignation
            if (expression.Contains("="))
            {
                string[] parts = expression.Split('=');
                if (parts.Length != 2)
                {
                    throw new ArgumentException("Expression d'assignation invalide");
                }

                string variableName = parts[0].Trim();
                IExpression valueExpression = ParseAddSubtract(parts[1].Trim());
                
                return new AssignExpression(variableName, valueExpression);
            }
            else
            {
                return ParseAddSubtract(expression);
            }
        }

        // Analyse une expression avec addition/soustraction
        private IExpression ParseAddSubtract(string expression)
        {
            expression = expression.Trim();
            
            // Chercher l'opérateur + ou - de plus faible priorité (non imbriqué dans des parenthèses)
            int parenthesesLevel = 0;
            
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                char c = expression[i];
                
                if (c == ')')
                    parenthesesLevel++;
                else if (c == '(')
                    parenthesesLevel--;
                
                if (parenthesesLevel == 0 && (c == '+' || c == '-'))
                {
                    string leftStr = expression.Substring(0, i).Trim();
                    string rightStr = expression.Substring(i + 1).Trim();
                    
                    IExpression left = ParseMultiplyDivide(leftStr);
                    IExpression right = ParseMultiplyDivide(rightStr);
                    
                    if (c == '+')
                        return new AddExpression(left, right);
                    else
                        return new SubtractExpression(left, right);
                }
            }
            
            // Pas d'opérateur + ou - trouvé, passer à la priorité suivante
            return ParseMultiplyDivide(expression);
        }

        // Analyse une expression avec multiplication/division
        private IExpression ParseMultiplyDivide(string expression)
        {
            expression = expression.Trim();
            
            // Chercher l'opérateur * ou / de plus faible priorité
            int parenthesesLevel = 0;
            
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                char c = expression[i];
                
                if (c == ')')
                    parenthesesLevel++;
                else if (c == '(')
                    parenthesesLevel--;
                
                if (parenthesesLevel == 0 && (c == '*' || c == '/'))
                {
                    string leftStr = expression.Substring(0, i).Trim();
                    string rightStr = expression.Substring(i + 1).Trim();
                    
                    IExpression left = ParseTerm(leftStr);
                    IExpression right = ParseTerm(rightStr);
                    
                    if (c == '*')
                        return new MultiplyExpression(left, right);
                    else
                        return new DivideExpression(left, right);
                }
            }
            
            // Pas d'opérateur * ou / trouvé, passer à la priorité suivante
            return ParseTerm(expression);
        }

        // Analyse un terme (nombre, variable ou expression parenthésée)
        private IExpression ParseTerm(string expression)
        {
            expression = expression.Trim();
            
            // Expression vide
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentException("Expression vide");
            
            // Expression parenthésée
            if (expression.StartsWith("(") && expression.EndsWith(")"))
                return ParseAddSubtract(expression.Substring(1, expression.Length - 2));
            
            // Nombre
            if (char.IsDigit(expression[0]) || (expression[0] == '-' && expression.Length > 1 && char.IsDigit(expression[1])))
            {
                if (int.TryParse(expression, out int number))
                    return new NumberExpression(number);
            }
            
            // Variable
            if (IsValidVariableName(expression))
                return new VariableExpression(expression);
            
            throw new ArgumentException($"Expression invalide: {expression}");
        }

        // Vérifie si une chaîne est un nom de variable valide
        private bool IsValidVariableName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            
            // Un nom de variable doit commencer par une lettre ou un underscore
            if (!char.IsLetter(name[0]) && name[0] != '_')
                return false;
            
            // Les caractères suivants peuvent être des lettres, des chiffres ou des underscores
            for (int i = 1; i < name.Length; i++)
            {
                if (!char.IsLetterOrDigit(name[i]) && name[i] != '_')
                    return false;
            }
            
            return true;
        }
    }

    /// <summary>
    /// NonterminalExpression - Expression représentant une comparaison "plus grand que"
    /// </summary>
    public class GreaterExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public GreaterExpression(IExpression left, IExpression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public int Interpret(Context context)
        {
            // Retourne 1 si vrai, 0 si faux (valeurs booléennes)
            return _leftExpression.Interpret(context) > _rightExpression.Interpret(context) ? 1 : 0;
        }

        public override string ToString()
        {
            return $"({_leftExpression} > {_rightExpression})";
        }
    }
}


