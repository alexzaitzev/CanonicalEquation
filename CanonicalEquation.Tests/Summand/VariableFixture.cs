using CanonicalEquation.Summand;
using NUnit.Framework;

namespace CanonicalEquation.Tests.Summand
{
    [TestFixture(Description = "Test variable class")]
    internal class VariableFixture
    {
        [Test(Description = "Test variable ToString method")]
        public void TestVariableToString()
        {
            // Arrange
            var sut = new Variable { Letter = 'y', Power = 5 };

            // Act
            var sutString = sut.ToString();

            // Assert
            Assert.AreEqual("y^5", sutString);
        }

        [Test(Description = "Test variable ToString method with uppercase")]
        public void TestVariableToStringUpperCase()
        {
            // Arrange
            var sut = new Variable {Letter = 'X', Power = 2};

            // Act
            var sutString = sut.ToString();

            // Assert
            Assert.AreEqual("x^2", sutString);
        }

        [Test(Description = "Test variable ToString method with negative power")]
        public void TestVariableToStringWithNegativePower()
        {
            // Arrange
            var sut = new Variable { Letter = 'x', Power = -1 };

            // Act
            var sutString = sut.ToString();

            // Assert
            Assert.AreEqual("x^-1", sutString);
        }

        [Test(Description = "Test variable ToString method with power = 1")]
        public void TestVariableToStringWithPowerOfOne()
        {
            // Arrange
            var sut = new Variable { Letter = 'x', Power = 1 };

            // Act
            var sutString = sut.ToString();

            // Assert
            Assert.AreEqual("x", sutString);
        }

        [Test(Description = "Test variable ToString method with power = 0")]
        public void TestVariableToStringWithZeroPower()
        {
            // Arrange
            var sut = new Variable { Letter = 'x', Power = 0 };

            // Act
            var sutString = sut.ToString();

            // Assert
            Assert.AreEqual(string.Empty, sutString);
        }
    }
}
