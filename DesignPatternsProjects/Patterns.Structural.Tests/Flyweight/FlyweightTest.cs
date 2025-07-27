using Xunit;

namespace Patterns.Structural.Flyweight.Tests
{
    public class FlyweightTest
    {
        [Fact]
        public void TestFlyweightPattern()
        {
            // Créer la fabrique de formats
            var formatFactory = new CharacterFormatFactory();
            var editor = new TextEditor(formatFactory);

            // Ajouter des caractères avec différents formats
            editor.AddCharacter('H', 0, 0, "Arial", 12, "normal", "black");
            editor.AddCharacter('e', 10, 0, "Arial", 12, "normal", "black");
            editor.AddCharacter('l', 20, 0, "Arial", 12, "normal", "black");
            editor.AddCharacter('l', 30, 0, "Arial", 12, "normal", "black");
            editor.AddCharacter('o', 40, 0, "Arial", 12, "normal", "black");

            // Ajouter du texte en gras
            editor.AddCharacter('W', 60, 0, "Arial", 12, "bold", "black");
            editor.AddCharacter('o', 70, 0, "Arial", 12, "bold", "black");
            editor.AddCharacter('r', 80, 0, "Arial", 12, "bold", "black");
            editor.AddCharacter('l', 90, 0, "Arial", 12, "bold", "black");
            editor.AddCharacter('d', 100, 0, "Arial", 12, "bold", "black");

            // Ajouter du texte coloré
            editor.AddCharacter('!', 110, 0, "Arial", 12, "normal", "red");

            // Vérifier le nombre de caractères
            Assert.Equal(11, editor.GetCharacterCount());

            // Vérifier le nombre de formats uniques
            // On devrait avoir seulement 3 formats :
            // 1. Arial 12pt normal black
            // 2. Arial 12pt bold black
            // 3. Arial 12pt normal red
            Assert.Equal(3, formatFactory.GetFormatCount());

            // Ajouter plus de caractères avec les mêmes formats
            editor.AddCharacter('?', 120, 0, "Arial", 12, "normal", "red");
            editor.AddCharacter('!', 130, 0, "Arial", 12, "bold", "black");

            // Vérifier que le nombre de formats n'a pas augmenté
            Assert.Equal(3, formatFactory.GetFormatCount());

            // Vérifier le nouveau nombre total de caractères
            Assert.Equal(13, editor.GetCharacterCount());

            // Rendre le texte (vérifie qu'il n'y a pas d'exception)
            editor.RenderText();
        }
    }
}
