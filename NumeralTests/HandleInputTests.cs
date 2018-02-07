using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace NumeralTests
{
    [TestClass]
    public class HandleInputTests
    {
        [TestMethod]
        public void SingleValidInput()
        {
            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("140");

            // assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("140 = \"CXL\"", result.First());
        }

        [TestMethod]
        public void MultipleValidInput()
        {
            var expectedResults = new Dictionary<string, string>
            {
                { "140", "CXL" },
                { "3999", "MMMCMXCIX" }
            };

            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("140,3999");

            // assert
            Assert.AreEqual(2, result.Count());

            var index = 0;
            foreach (var expected in expectedResults)
            {
                Assert.AreEqual($"{expected.Key} = \"{expected.Value}\"", result[index]);
                index++;
            }
        }

        [TestMethod]
        public void SingleInvalidInput()
        {
            var expectedResult = "140number";

            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("140number");

            // assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual($"{expectedResult} is not a number", result.First());
        }

        [TestMethod]
        public void MultipleInvalidInputs()
        {
            var expectedResults = new List<string> { "140number", "notanumber" };

            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("140number,notanumber");

            // assert
            Assert.AreEqual(2, result.Count());

            var index = 0;
            foreach (var expected in expectedResults)
            {
                Assert.AreEqual($"{expected} is not a number", result[index]);
                index++;
            }
        }

        [TestMethod]
        public void SingleTooLargeInput()
        {
            var expectedResult = "14000";

            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("14000");

            // assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual($"The supplied number, {expectedResult}, is not within the allowed range, please enter a number between 1 and 3999", result.First());
        }

        [TestMethod]
        public void MultipleTooLargeInputs()
        {
            var expectedResults = new List<string> { "64354", "4000" };

            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("64354,4000");

            // assert
            Assert.AreEqual(2, result.Count());

            var index = 0;
            foreach (var expected in expectedResults)
            {
                Assert.AreEqual($"The supplied number, {expected}, is not within the allowed range, please enter a number between 1 and 3999", result[index]);
                index++;
            }
        }

        [TestMethod]
        public void SingleTooSmallInput()
        {
            var expectedResult = "-300";

            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("-300");

            // assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual($"The supplied number, {expectedResult}, is not within the allowed range, please enter a number between 1 and 3999", result.First());
        }

        [TestMethod]
        public void MultipleTooSmallInputs()
        {
            var expectedResults = new List<string> { "-64354", "0" };

            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.HandleInput("-64354,0");

            // assert
            Assert.AreEqual(2, result.Count());

            var index = 0;
            foreach (var expected in expectedResults)
            {
                Assert.AreEqual($"The supplied number, {expected}, is not within the allowed range, please enter a number between 1 and 3999", result[index]);
                index++;
            }
        }
    }
}
