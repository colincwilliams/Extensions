namespace ColinCWilliams.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class EnumerableExtensions
    {
        public const uint PrettyPrintMinSeparation = 4;
        public const char PrettyPrintDefaultSeparator = ' ';

        /// <summary>
        /// Executes a function for each item in an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of items in the enumerable.</typeparam>
        /// <param name="list">The enumerable to run the function on.</param>
        /// <param name="function">The function to execute, which takes three parameters: the current item, the index of the item in the enumerable and a boolean representing if the item is the last item in the list or not (true if last).</param>
        public static void Map<T>(this IEnumerable<T> list, Action<T, int, bool> function)
        {
            list.ThrowIfNull(nameof(list));

            using (IEnumerator<T> enumerator = list.GetEnumerator())
            {
                int itemIndex = 0;
                bool isLast = !enumerator.MoveNext();
                while (!isLast)
                {
                    T current = enumerator.Current;
                    function(current, itemIndex++, isLast);
                    isLast = !enumerator.MoveNext();
                }
            }
        }

        public static string PrettyPrint<T>(
            this IEnumerable<IEnumerable<T>> list,
            char separator = PrettyPrintDefaultSeparator,
            uint minSeparation = PrettyPrintMinSeparation)
        {
            list.ThrowIfNull(nameof(list));

            string printed = string.Empty;

            if (list.Any())
            {
                StringBuilder sb = new StringBuilder();
                List<uint> maxFieldLengths = new List<uint>();

                foreach (var fields in list)
                {
                    fields.Map((field, fieldIndex, isLast) =>
                    {
                        // Get field string length along with minimum separation distance. Don't add separation for last field.
                        uint fieldStringLength = field.GetStringLength() + (isLast ? 0 : minSeparation);

                        // Get the current max length for this field column or 0 if it hasn't been calculated yet.
                        uint currentFieldMaxLength = fieldIndex < maxFieldLengths.Count ? maxFieldLengths[fieldIndex] : 0;

                        // Insert the new maximum field length.
                        maxFieldLengths.Insert(fieldIndex, Math.Max(fieldStringLength, currentFieldMaxLength));
                    });
                }

                foreach (var fields in list)
                {
                    fields.Map((field, fieldIndex, isLast) =>
                    {
                        sb.Append(field);

                        // Don't append spacing to the last field on a line.
                        if (!isLast)
                        {
                            uint fieldLength = field.GetStringLength();
                            uint appendLength = maxFieldLengths[fieldIndex] - fieldLength;

                            sb.RepeatAppend(separator, appendLength);
                        }
                    });

                    sb.AppendLine();
                }

                printed = sb.ToString();
            }

            return printed;
        }
    }
}