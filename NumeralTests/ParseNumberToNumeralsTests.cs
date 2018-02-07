using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumeralTests
{
    [TestClass]
    public class ParseNumberToNumeralsTests
    {
        [TestMethod]
        public void I_equals_1()
        {
            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.ParseNumberToNumerals(1);

            // assert
            Assert.AreEqual(result, "I");
        }

        [TestMethod]
        public void V_equals_5()
        {
            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.ParseNumberToNumerals(5);

            // assert
            Assert.AreEqual(result, "V");
        }

        [TestMethod]
        public void X_equals_10()
        {
            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.ParseNumberToNumerals(10);

            // assert
            Assert.AreEqual(result, "X");
        }

        [TestMethod]
        public void XX_equals_20()
        {
            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.ParseNumberToNumerals(20);

            // assert
            Assert.AreEqual(result, "XX");
        }

        [TestMethod]
        public void MMMCMXCIX_equals_3999()
        {
            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.ParseNumberToNumerals(3999);

            // assert
            Assert.AreEqual(result, "MMMCMXCIX");
        }

        [TestMethod]
        public void CXL_equals_140()
        {
            // arrange
            var service = new NumeralLogic.NumeralService();

            // act
            var result = service.ParseNumberToNumerals(140);

            // assert
            Assert.AreEqual(result, "CXL");
        }
    }
}
