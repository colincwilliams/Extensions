namespace ColinCWilliams.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class DictionaryExtensions
    {
        private const uint PrettyPrintMinSeparation = 4;
        private const char PrettyPrintDefaultSeparator = ' ';

        public static string PrettyPrint<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            char separator = PrettyPrintDefaultSeparator,
            uint minSeparation = PrettyPrintMinSeparation)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            return dictionary.AsEnumerable().PrettyPrint(separator, minSeparation);
        }

        public static string PrettyPrint<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            char separator = PrettyPrintDefaultSeparator,
            uint minSeparation = PrettyPrintMinSeparation)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            return dictionary.AsEnumerable().PrettyPrint(separator, minSeparation);
        }

        public static string PrettyPrint<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            char separator = PrettyPrintDefaultSeparator,
            uint minSeparation = PrettyPrintMinSeparation)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            return dictionary.AsEnumerable().PrettyPrint(separator, minSeparation);
        }

        public static string PrettyPrint<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> kvPairs,
            char separator = PrettyPrintDefaultSeparator,
            uint minSeparation = PrettyPrintMinSeparation)
        {
            kvPairs.ThrowIfNull(nameof(kvPairs));

            string printed = string.Empty;

            if (kvPairs.Any())
            {
                StringBuilder sb = new StringBuilder();

                uint maxKeyLength = kvPairs.Max(kvPair => GetStringLength(kvPair.Key));

                uint totalKeyLength = maxKeyLength + minSeparation;

                foreach (var pair in kvPairs)
                {
                    sb.Append(pair.Key);

                    uint keyLength = GetStringLength(pair.Key);
                    sb.RepeatAppend(separator, totalKeyLength - keyLength);

                    sb.Append(pair.Value);
                    sb.AppendLine();
                }

                printed = sb.ToString();
            }

            return printed;
        }

        private static uint GetStringLength(object o)
        {
            return o == null ? 0 : (uint)o.ToString().Length;
        }
    }
}