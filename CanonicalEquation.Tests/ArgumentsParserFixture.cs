using System;
using CanonicalEquation.Infra;
using NUnit.Framework;

namespace CanonicalEquation.Tests
{
    [TestFixture(Description = "Class for testing ArgumentsParser")]
    internal class ArgumentsParserFixture
    {
        [Test(Description = "Test execution without input argument")]
        public void TestNoArgumentExecution()
        {
            // Arrange
            string[] input = {};
            var sut = new ArgumentsParser();

            // Act
            var workMode = sut.Parse(input);

            // Assert
            Assert.AreEqual(ApplicationWorkMode.Console, workMode);
        }

        [Test(Description = "Test execution with one correct argument: console")]
        public void TestConsoleArgumentExecution()
        {
            // Arrange
            string[] input = {"/m:console"};
            var sut = new ArgumentsParser();

            // Act
            var workMode = sut.Parse(input);

            // Assert
            Assert.AreEqual(ApplicationWorkMode.Console, workMode);
        }

        [Test(Description = "Test execution with too many arguments")]
        public void TestWithTooManyArgumentsExecution()
        {
            // Arrange
            string[] input = { "/m:console", "in", "out" };
            var sut = new ArgumentsParser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.Parse(input));
        }

        [Test(Description = "Test execution with incorrect argument")]
        public void TestOneInvalidArgumentExecution()
        {
            // Arrange
            string[] input = { "/f:console" };
            var sut = new ArgumentsParser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.Parse(input));
        }

        [Test(Description = "Test execution with incorrect argument value")]
        public void TestOneInvalidValueArgumentExecution()
        {
            // Arrange
            string[] input = { "/m:stream" };
            var sut = new ArgumentsParser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.Parse(input));
        }

        [Test(Description = "Test execution with correct argument: file")]
        public void TestFileArgumentExecution()
        {
            // Arrange
            string[] input = { "/m:file", "input.txt" };
            var sut = new ArgumentsParser();

            // Act 
            var workMode = sut.Parse(input);

            // Assert
            Assert.AreEqual(ApplicationWorkMode.File, workMode);
        }

        [Test(Description = "Test execution with incorrect argument: file")]
        public void TestFileIncorrectArgumentExecution()
        {
            // Arrange
            string[] input = { "/m:file" };
            var sut = new ArgumentsParser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.Parse(input));
        }

        [Test(Description = "Test argument case insensitivity")]
        public void TestInsensitivityArgument()
        {
            // Arrange
            string[] input = { "/m:cOnSoLE" };
            var sut = new ArgumentsParser();

            // Act
            var workMode = sut.Parse(input);

            // Assert
            Assert.AreEqual(ApplicationWorkMode.Console, workMode);
        }
    }
}
