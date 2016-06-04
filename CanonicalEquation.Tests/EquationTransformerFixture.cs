using NUnit.Framework;

namespace CanonicalEquation.Tests
{
    [TestFixture(Description = "Class for testing EquationTransformer")]
    internal class EquationTransformerFixture
    {
        [Test(Description = "Test is equation is canonical")]
        [TestCase("- 3.0x^3y^2 = 2.5x -1.0", ExpectedResult = "- 3.0x^3y^2 - 2.5x + 1.0 = 0")]
        [TestCase("x^2 + xy = - xy + y", ExpectedResult = "x^2 + 2.0xy - y = 0")]
        [TestCase("x^2 + 3.5xy + y = y^2 - xy + y", ExpectedResult = "x^2 - y^2 + 4.5xy = 0")]
        [TestCase("x^2 + 3.5xyz + yzx = 2.7 - ab + 4.6", ExpectedResult = "4.5xyz + x^2 + ab - 7.3 = 0")]
        [TestCase("1.0 = 1.0", ExpectedResult = "0 = 0")]
        [TestCase("3.3x^4y = 3.3yx^4", ExpectedResult = "0 = 0")]
        [TestCase("(x + y) - y = x - y", ExpectedResult = "y = 0")]
        [TestCase("x + (x + y) = x + y", ExpectedResult = "x = 0")]
        [TestCase("(x + (x + y)) = (x + y)", ExpectedResult = "x = 0")]
        [TestCase("-(x + y) - y - z = 5.0", ExpectedResult = "- x - 2.0y - z - 5.0 = 0")]
        [TestCase("-(x + y - y - z) = (5.0 - z)", ExpectedResult = "- x + 2.0z - 5.0 = 0")]
        [TestCase("-(x - (x + y)) = 3x^2 + y", ExpectedResult = "- 3.0x^2 = 0")]
        [TestCase("(x + (x + y)) = (x + y)", ExpectedResult = "x = 0")]
        [TestCase("(x) + 3.9y = -(-y)", ExpectedResult = "x + 2.9y = 0")]
        [TestCase("xyz = 0.9zyx", ExpectedResult = "0.1xyz = 0")]
        public string TestEquationBeCanonical(string equation)
        {
            // Arrange
            var sut = new EquationTransformer();

            // Act
            var canonicalEquation = sut.MakeCanonical(equation);

            // Assert
            return canonicalEquation;
        }
    }
}
