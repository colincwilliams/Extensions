namespace Extensions.Tests
{
    using ColinCWilliams.Extensions;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using System;
    using System.Text;

    public enum TestEnum
    {
        Blue = -1,
        Default = 0,
        Red = 1,
        Green = 2
    }

    [TestClass]
    public class ExtensionsTests
    {
        #region Parse Enum Tests
        [TestMethod]
        public void ParseEnumNullString()
        {
            Assert.AreEqual(TestEnum.Default, RunParseEnum(null));
        }

        [TestMethod]
        public void ParseEnumInvalidString()
        {
            Assert.AreEqual(TestEnum.Default, RunParseEnum("asdf"));
        }

        [TestMethod]
        public void ParseEnumInvalidNumericalString()
        {
            Assert.AreEqual(TestEnum.Default, RunParseEnum("123"));
        }

        [TestMethod]
        public void ParseEnumInvalidNegativeNumericalString()
        {
            Assert.AreEqual(TestEnum.Default, RunParseEnum("-123"));
        }

        [TestMethod]
        public void ParseEnumValidString()
        {
            Assert.AreEqual(TestEnum.Red, RunParseEnum("Red"));
        }

        [TestMethod]
        public void ParseEnumValidNumericalString()
        {
            Assert.AreEqual(TestEnum.Green, RunParseEnum("2"));
        }

        [TestMethod]
        public void ParseEnumValidNegativeNumericalString()
        {
            Assert.AreEqual(TestEnum.Blue, RunParseEnum("-1"));
        }

        private TestEnum RunParseEnum(string str)
        {
            return str.ParseEnum(TestEnum.Default);
        }
        #endregion // Parse Enum Tests

        #region Repeat Append Tests
        [TestMethod]
        public void RepeatAppendZeroCount()
        {
            ValidateRepeatAppend("!", 0, "Hello, World", "Hello, World");
        }

        [TestMethod]
        public void RepeatAppendNonZeroCount()
        {
            ValidateRepeatAppend("!", 3, "Hello, World", "Hello, World!!!");
        }

        public void RepeatAppendNonString()
        {
            ValidateRepeatAppend(3, 3, "Hello, World", "Hello, World333");
        }

        private void ValidateRepeatAppend(object appendObject, uint appendCount, string initialString, string finalString)
        {
            StringBuilder sb = new StringBuilder(initialString);
            sb.RepeatAppend(appendObject, appendCount);
            Assert.AreEqual(finalString, sb.ToString());
        }
        #endregion // Repeat Append Tests

        #region Throw If Null Tests
        [TestMethod]
        public void ThrowIfNullThrowsWhenNull()
        {
            Assert.IsTrue(ThrowIfNullThrewException(null), "Expected exception to be thrown.");            
        }

        [TestMethod]
        public void ThrowIfNullDoesNotThrowWhenNotNull()
        {
            Assert.IsFalse(ThrowIfNullThrewException(new object()), "Unexpected ArgumentNullException thrown.");
        }

        private bool ThrowIfNullThrewException(object o)
        {
            const string argumentName = "ArgumentName";
            bool exceptionThrown = false;

            try
            {
                o.ThrowIfNull(argumentName);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(argumentName, ex.ParamName);
                exceptionThrown = true;
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected exception type thrown.");
                exceptionThrown = true;
            }

            return exceptionThrown;
        }
        #endregion // Throw If Null Tests
    }
}
