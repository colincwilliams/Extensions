namespace Extensions.Tests
{
    using System;
    using System.Collections.Generic;
    using ColinCWilliams.Extensions;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using System.Text;
    [TestClass]
    public class DictionaryExtensionsTests
    {
        private readonly Dictionary<string, string> nullDictionary = null;
        private readonly Dictionary<string, string> emptyDictionary = new Dictionary<string, string>();
        private readonly Dictionary<string, string> fullDictionary = new Dictionary<string, string>()
        {
            { "Key1", "Value1" },
            { "LongKey2", "LongValue2" },
            { "Key3", "Value3" }
        };

        #region Pretty Print Tests
        private readonly char alternativeSeparator = '.';
        private readonly uint alternativeMinSeparation = 2;

        [TestMethod]
        public void PrettyPrintNullDictionary()
        {
            bool exceptionThrown = false;

            try
            {
                nullDictionary.PrettyPrint();
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected exception type thrown.");
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void PrettyPrintEmptyDictionary()
        {
            string result;

            result = emptyDictionary.PrettyPrint();
            Assert.AreEqual(string.Empty, result);

            result = emptyDictionary.PrettyPrint('.', alternativeMinSeparation);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void PrettyPrintFullDictionaryDefaults()
        {
            string result = fullDictionary.PrettyPrint();
            string[] expectedLines = new string[]
            {
                "Key1        Value1",
                "LongKey2    LongValue2",
                "Key3        Value3"
            };

            ValidatePrettyPrint(result, expectedLines);
        }

        [TestMethod]
        public void PrettyPrintFullDictionaryAlternatives()
        {
            string result = fullDictionary.PrettyPrint(alternativeSeparator, alternativeMinSeparation);
            string[] expectedLines = new string[]
            {
                "Key1......Value1",
                "LongKey2..LongValue2",
                "Key3......Value3"
            };

            ValidatePrettyPrint(result, expectedLines);
        }

        [TestMethod]
        public void PrettyPrintFullDictionaryZeroSeparation()
        {
            string result = fullDictionary.PrettyPrint(alternativeSeparator, 0);
            string[] expectedLines = new string[]
            {
                "Key1....Value1",
                "LongKey2LongValue2",
                "Key3....Value3"
            };

            ValidatePrettyPrint(result, expectedLines);
        }

        private void ValidatePrettyPrint(string result, string[] expectedLines)
        {
            StringBuilder expectedResult = new StringBuilder();
            
            foreach (string line in expectedLines)
            {
                expectedResult.AppendLine(line);
            }

            Assert.AreEqual(expectedResult.ToString(), result);
        }
        #endregion // Pretty Print Tests
    }
}
