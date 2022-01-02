using NUnit.Framework;

namespace CarFinderApi.Test
{
    public class UnitTests
    {
        [Test]
        public void CapitalizeEmptyString_ReturnsEmptyString()
        {
            // Arrange
            string inputValue = string.Empty;

            // Act
            string result = Extensions.StringExtensions.Capitalize(inputValue);

            // Assert
            Assert.AreEqual(inputValue, result);
        }

        [Test]
        public void CapitalizeNullString_ReturnsNull()
        {
            string inputValue = null;

            string result = Extensions.StringExtensions.Capitalize(inputValue);

            Assert.AreEqual(inputValue, result);
        }

        [Test]
        public void CapitalizeSpaces_ReturnsSpaces()
        {
            string inputValue = "     ";

            string result = Extensions.StringExtensions.Capitalize(inputValue);

            Assert.AreEqual(inputValue, result);
        }

        [Test]
        public void CapitalizeStartingWithNumber_ReturnsSameValue()
        {
            string inputValue = "3littlepigs";

            string result = Extensions.StringExtensions.Capitalize(inputValue);

            Assert.AreEqual(inputValue, result);
        }

        [Test]
        public void CapitalizeAllLowerCase_ReturnsCapitalized()
        {
            string inputValue = "greatscott";
            string expectedResult = "Greatscott";

            string result = Extensions.StringExtensions.Capitalize(inputValue);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CapitalizeAllUpperCase_ReturnsCapitalized()
        {
            string inputValue = "THISISHEAVY";
            string expectedValue = "Thisisheavy";

            string result = Extensions.StringExtensions.Capitalize(inputValue);

            Assert.AreEqual(expectedValue, result);
        }
    }
}
