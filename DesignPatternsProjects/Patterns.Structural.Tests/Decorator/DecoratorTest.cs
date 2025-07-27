using Xunit;

namespace Patterns.Structural.Decorator.Tests
{
    public class DecoratorTest
    {
        [Fact]
        public void TestDecoratorPattern()
        {
            // Test simple text
            IText simpleText = new SimpleText("Hello");
            Assert.Equal("Hello", simpleText.GetContent());

            // Test single decorators
            IText boldText = new BoldDecorator(simpleText);
            Assert.Equal("<b>Hello</b>", boldText.GetContent());

            IText italicText = new ItalicDecorator(simpleText);
            Assert.Equal("<i>Hello</i>", italicText.GetContent());

            IText underlineText = new UnderlineDecorator(simpleText);
            Assert.Equal("<u>Hello</u>", underlineText.GetContent());

            IText colorText = new ColorDecorator(simpleText, "red");
            Assert.Equal("<span style=\"color: red\">Hello</span>", colorText.GetContent());

            // Test nested decorators
            IText boldItalicText = new BoldDecorator(new ItalicDecorator(simpleText));
            Assert.Equal("<b><i>Hello</i></b>", boldItalicText.GetContent());

            IText colorBoldUnderlineText = new ColorDecorator(
                new BoldDecorator(
                    new UnderlineDecorator(simpleText)
                ), 
                "blue"
            );
            Assert.Equal(
                "<span style=\"color: blue\"><b><u>Hello</u></b></span>", 
                colorBoldUnderlineText.GetContent()
            );

            // Test all decorators combined
            IText allDecorators = new ColorDecorator(
                new BoldDecorator(
                    new ItalicDecorator(
                        new UnderlineDecorator(simpleText)
                    )
                ),
                "green"
            );
            Assert.Equal(
                "<span style=\"color: green\"><b><i><u>Hello</u></i></b></span>",
                allDecorators.GetContent()
            );
        }
    }
}
