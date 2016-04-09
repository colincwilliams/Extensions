namespace ColinCWilliams.Extensions
{
    using System;
    using System.Text;

    public static class Extensions
    {
        /// <summary>
        /// Parses a string to an enum value from enum type T, or returns the provided default value
        /// if the parse fails. A parse is considered to have failed if the resulting value does
        /// not match a defined value in the enum type T.
        /// </summary>
        /// <typeparam name="T">The enum type to parse from.</typeparam>
        /// <param name="str">The string to parse.</param>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns></returns>
        public static T ParseEnum<T>(this string str, T defaultValue) where T : struct
        {
            T result;
            if (str == null || !Enum.TryParse(str, out result) || !Enum.IsDefined(typeof(T), result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// Appends an object to a StringBuilder the specified number of times.
        /// </summary>
        /// <param name="stringBuilder">The StringBuilder to append to.</param>
        /// <param name="obj">The object to append.</param>
        /// <param name="count">The number of times to append the object.</param>
        /// <returns></returns>
        public static StringBuilder RepeatAppend(this StringBuilder stringBuilder, object obj, uint count)
        {
            stringBuilder.ThrowIfNull(nameof(stringBuilder));

            for (int i = 0; i < count; i++)
            {
                stringBuilder.Append(obj);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Throws an ArgumentNullException with the provided argument name if the object is null.
        /// </summary>
        /// <param name="obj">The object to check for null.</param>
        /// <param name="argName">The argument name to use in the ArgumentNullException.</param>
        public static void ThrowIfNull(this object obj, string argName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}