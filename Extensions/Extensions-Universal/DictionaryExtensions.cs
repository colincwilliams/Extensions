namespace ColinCWilliams.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static string PrettyPrint<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            char separator = EnumerableExtensions.PrettyPrintDefaultSeparator,
            uint minSeparation = EnumerableExtensions.PrettyPrintMinSeparation)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            return dictionary.AsEnumerable().PrettyPrint(separator, minSeparation);
        }

        public static string PrettyPrint<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            char separator = EnumerableExtensions.PrettyPrintDefaultSeparator,
            uint minSeparation = EnumerableExtensions.PrettyPrintMinSeparation)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            return dictionary.AsEnumerable().PrettyPrint(separator, minSeparation);
        }

        public static string PrettyPrint<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            char separator = EnumerableExtensions.PrettyPrintDefaultSeparator,
            uint minSeparation = EnumerableExtensions.PrettyPrintMinSeparation)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            return dictionary.AsEnumerable().PrettyPrint(separator, minSeparation);
        }

        public static string PrettyPrint<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> kvPairs,
            char separator = EnumerableExtensions.PrettyPrintDefaultSeparator,
            uint minSeparation = EnumerableExtensions.PrettyPrintMinSeparation)
        {
            kvPairs.ThrowIfNull(nameof(kvPairs));
            return kvPairs.Select((kv) => new List<object>() { kv.Key, kv.Value }).PrettyPrint();
        }
    }
}