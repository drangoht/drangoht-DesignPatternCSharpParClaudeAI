using System;

namespace Patterns.Behavioral.Interpreter
{
    /// <summary>
    /// Test du pattern Interpreter
    /// </summary>
    public class InterpreterTest : Patterns.Behavioral.IPatternTest
    {
        public void Run()
        {
            Console.WriteLine("Démonstration du pattern Interpreter");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Le pattern Interpreter définit une représentation grammaticale pour un langage et un interpréteur qui utilise cette représentation.");
            Console.WriteLine();
            
            // Code de démonstration du pattern
            Console.WriteLine("Exemple du pattern en action:");
            try 
            {
                // Exécuter le code du pattern
                RunPatternDemo();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("La démonstration complète n'est pas encore implémentée.");
                Console.WriteLine("Consultez le code source pour plus de détails sur ce pattern.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'exécution: {ex.Message}");
            }
        }

        private void RunPatternDemo()
        {
            // Création du contexte
            Context context = new Context();
            
            // Initialisation des variables
            Console.WriteLine("Initialisation des variables dans le contexte:");
            context.SetVariable("x", 10);
            context.SetVariable("y", 5);
            context.SetVariable("z", 42);
            context.DisplayVariables();
            Console.WriteLine();
            
            // Création et interprétation d'expressions simples
            Console.WriteLine("1. Expressions simples:");
            
            // Variable x
            IExpression expressionX = new VariableExpression("x");
            Console.WriteLine($"x = {expressionX.Interpret(context)}");
            
            // Nombre 15
            IExpression expressionNum = new NumberExpression(15);
            Console.WriteLine($"15 = {expressionNum.Interpret(context)}");
            Console.WriteLine();
            
            // Création et interprétation d'expressions plus complexes
            Console.WriteLine("2. Expressions arithmétiques:");
            
            // x + y
            IExpression expressionAdd = new AddExpression(
                new VariableExpression("x"),
                new VariableExpression("y")
            );
            Console.WriteLine($"x + y = {expressionAdd.Interpret(context)}");
            
            // z - y
            IExpression expressionSubtract = new SubtractExpression(
                new VariableExpression("z"),
                new VariableExpression("y")
            );
            Console.WriteLine($"z - y = {expressionSubtract.Interpret(context)}");
            
            // (x + y) * (z - y)
            IExpression expressionMultiply = new MultiplyExpression(
                expressionAdd,
                expressionSubtract
            );
            Console.WriteLine($"(x + y) * (z - y) = {expressionMultiply.Interpret(context)}");
            Console.WriteLine();
            
            // Test d'expressions conditionnelles
            Console.WriteLine("3. Expressions conditionnelles:");
            
            // x > y
            IExpression expressionGreater = new GreaterExpression(
                new VariableExpression("x"),
                new VariableExpression("y")
            );
            Console.WriteLine($"x > y ? {expressionGreater.Interpret(context)}");
            
            // Exemple d'utilisation d'un parser
            Console.WriteLine("\n4. Utilisation d'un parser d'expressions:");
            ExpressionParser parser = new ExpressionParser();
            
            // Parsing et interprétation d'expressions
            string[] expressions = {
                "x + 2",
                "x - y",
                "z * (x + y)",
                "x > y ? x : y"
            };
            
            foreach (var expr in expressions)
            {
                try
                {
                    IExpression parsedExpr = parser.Parse(expr);
                    int result = parsedExpr.Interpret(context);
                    Console.WriteLine($"{expr} = {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'interprétation de '{expr}': {ex.Message}");
                }
            }
            
            Console.WriteLine("\nLe pattern Interpreter permet de:");
            Console.WriteLine("- Définir une grammaire pour un langage");
            Console.WriteLine("- Interpréter des expressions dans ce langage");
            Console.WriteLine("- Représenter les règles grammaticales comme une hiérarchie d'objets");
        }

        public string GetName()
        {
            return "Interpreter";
        }

        public string GetDescription()
        {
            return "Le pattern Interpreter définit une représentation grammaticale pour un langage et un interpréteur qui utilise cette représentation.";
        }
    }
}

