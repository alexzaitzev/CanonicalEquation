using System;
using System.Collections.Generic;
using System.Globalization;
using CanonicalEquation.Summand;
using NUnit.Framework;

namespace CanonicalEquation.Tests.Summand
{
    [TestFixture(Description = "Tests summand class")]
    internal class TermFixture
    {
        [Test(Description = "Test getting negative number")]
        public void TestGetNegativeNumber()
        {
            // Arrange
            var sut = new Term { Number = 2.5f, Sign = Signs.Minus };

            // Act
            var number = sut.GetNumber();

            // Assert
            Assert.AreEqual(-2.5f, number);
        }

        [Test(Description = "Test getting positive number")]
        public void TestGetPositiveNumber()
        {
            // Arrange
            var sut = new Term { Number = 2.5f, Sign = Signs.Plus };

            // Act
            var number = sut.GetNumber();

            // Assert
            Assert.AreEqual(2.5f, number);
        }

        [Test(Description = "Test getting positive number without fraction")]
        public void TestGetPositiveNumberWithoutFraction()
        {
            // Arrange
            var sut = new Term { Number = 3f, Sign = Signs.Plus };

            // Act
            var number = sut.GetNumber();

            // Assert
            Assert.AreEqual(3.0f, number);
        }

        [Test(Description = "Test inversing number sign from negative to positive")]
        public void TestInverseNumberSignFromNegativeToPositive()
        {
            // Arrange
            var sut = new Term { Number = 2.5f, Sign = Signs.Minus };

            // Act
            sut.InverseSign();
            var number = sut.GetNumber();

            // Assert
            Assert.AreEqual(2.5f, number);
        }

        [Test(Description = "Test inversing number sign from positive to negative")]
        public void TestInverseNumberSignFromPositiveToNegative()
        {
            // Arrange
            var sut = new Term { Number = 2.5f, Sign = Signs.Plus };

            // Act
            sut.InverseSign();
            var number = sut.GetNumber();

            // Assert
            Assert.AreEqual(-2.5f, number);
        }

        [Test(Description = "Test getting term rank")]
        public void TestGetRank()
        {
            // Arrange
            var sut = new Term
            {
                Number = 2.5f,
                Sign = Signs.Plus,
                Vars = new List<Variable>
                {
                    new Variable('x', 3),
                    new Variable('y', 1),
                    new Variable('z', 2)
                }
            };

            // Act
            var rank = sut.Rank;

            // Assert
            Assert.AreEqual(6, rank);
        }

        [Test(Description = "Test getting term variables in string")]
        public void TestGetStringVars()
        {
            // Arrange
            var sut = new Term
            {
                Number = 2.5f,
                Sign = Signs.Plus,
                Vars = new List<Variable>
                {
                    new Variable('y', 1),
                    new Variable('X', 3),
                    new Variable('Z', 2)
                }
            };

            // Act
            var stringVars = sut.VarsToString();

            // Assert
            Assert.AreEqual("x^3z^2y", stringVars);
        }

        [Test(Description = "Test ToString method")]
        public void TestToStringMethod()
        {
            // Arrange
            var sut = new Term
            {
                Number = 33f,
                Sign = Signs.Plus,
                Vars = new List<Variable>
                {
                    new Variable('Z', -4)
                }
            };

            // Act
            var termToString = sut.ToString();

            // Assert
            Assert.AreEqual("33.0z^-4", termToString);
        }

        [Test(Description = "Test ToString method with number less, that one")]
        public void TestToStringMethodWithNumbersLessThanOne()
        {
            // Arrange
            var sut = new Term
            {
                Number = 0.1f,
                Sign = Signs.Plus,
                Vars = new List<Variable>
                {
                    new Variable('x', 1)
                }
            };

            // Act
            var termToString = sut.ToString();

            // Assert
            Assert.AreEqual("0.1x", termToString);
        }

        [Test(Description = "Test ToString method for constant")]
        public void TestToStringMethodForConstant()
        {
            // Arrange
            var sut = new Term
            {
                Number = 4.4f,
                Sign = Signs.Minus
            };

            // Act
            var termToString = sut.ToString();

            // Assert
            Assert.AreEqual("- 4.4", termToString);
        }

        [Test(Description = "Test ToString method for constant 1")]
        public void TestToStringMethodForConstantOne()
        {
            // Arrange
            var sut = new Term
            {
                Number = 1.0f,
                Sign = Signs.Plus
            };

            // Act
            var termToString = sut.ToString();

            // Assert
            Assert.AreEqual("1.0", termToString);
        }

        [Test(Description = "Test adding number when term positive sign do not changed")]
        public void TestAddToNumberMethodWhenPositiveSignDoNotChanged()
        {
            // Arrange
            var sut = new Term
            {
                Number = 3.1f,
                Sign = Signs.Plus
            };

            // Act
            var sum = sut.AddToNumber(2.2f);

            // Assert
            Assert.AreEqual(5.3, sum, 0.001);
        }

        [Test(Description = "Test adding number when term negative sign do not changed")]
        public void TestAddToNumberMethodWhenNeativeSignDoNotChanged()
        {
            // Arrange
            var sut = new Term
            {
                Number = 6.6f,
                Sign = Signs.Minus
            };

            // Act
            var sum = sut.AddToNumber(-1.2f);

            // Assert
            Assert.AreEqual(-7.8, sum, 0.001);
        }

        [Test(Description = "Test adding number when term sign changed to negative")]
        public void TestAddToNumberMethodWhenSignChangedToNegative()
        {
            // Arrange
            var sut = new Term
            {
                Number = 1.1f,
                Sign = Signs.Plus
            };

            // Act
            var sum = sut.AddToNumber(-2.9f);

            // Assert
            Assert.AreEqual(-1.8, sum, 0.001);
        }

        [Test(Description = "Test adding number when term sign changed to positive")]
        public void TestAddToNumberMethodWhenSignChangedToPositive()
        {
            // Arrange
            var sut = new Term
            {
                Number = 0.5f,
                Sign = Signs.Minus
            };

            // Act
            var sum = sut.AddToNumber(1.5f);

            // Assert
            Assert.AreEqual(1f, sum, 0.001);
        }

        [Test(Description = "Test if term with Numer equals 0 is zero")]
        public void TestIfTermWithZeroNumberIsZero()
        {
            // Arrange
            var sut = new Term
            {
                Number = 0f,
                Sign = Signs.Plus,
                Vars = new List<Variable>
                {
                    new Variable('x', 1)
                }
            };

            // Act
            var isZero = sut.IsZero();

            // Assert
            Assert.True(isZero, "Should be zero");
        }

        [Test(Description = "Test if term with Numer not equals 0 is not zero")]
        public void TestIfTermWithNonZeroNumberIsNotZero()
        {
            // Arrange
            var sut = new Term
            {
                Number = 11.1f,
                Sign = Signs.Minus,
                Vars = new List<Variable>
                {
                    new Variable('x', 1)
                }
            };

            // Act
            var isZero = sut.IsZero();

            // Assert
            Assert.False(isZero, "Should not be zero");
        }

        [Test(Description = "Test terms comparison, when they have the same number of variables")]
        public void TestTermsComparisonWithSameCountVariables()
        {
            // Arrange
            var sut = new Term
            {
                Number = 1f,
                Sign = Signs.Minus,
                Vars = new List<Variable>
                {
                    new Variable('x', 1)
                }
            };

            var sut1 = new Term
            {
                Number = 1f,
                Sign = Signs.Minus,
                Vars = new List<Variable>
                {
                    new Variable('y', 1)
                }
            };

            // Act
            var comparisonResult = sut.CompareTo(sut1);

            // Assert
            Assert.True(comparisonResult != 0, "Terms with the same rank and variables count, but different letter should be different");
        }

        [Test(Description = "Test terms comparison, when they have the same rank")]
        public void TestTermsComparisonWithSameRank()
        {
            // Arrange
            var sut = new Term
            {
                Number = 1f,
                Sign = Signs.Minus,
                Vars = new List<Variable>
                {
                    new Variable('x', 1),
                    new Variable('y', 1),
                }
            };

            var sut1 = new Term
            {
                Number = 1f,
                Sign = Signs.Minus,
                Vars = new List<Variable>
                {
                    new Variable('y', 2)
                }
            };

            // Act
            var comparisonResult = sut.CompareTo(sut1);

            // Assert
            Assert.True(comparisonResult < 0, "Term with higher variable rank should go first");
        }
    }
}
