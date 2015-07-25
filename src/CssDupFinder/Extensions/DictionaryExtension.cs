using System.Collections.Generic;

namespace CssDupFinder.Extensions
{
    public static class DictionaryExtension
    {
        public static TResult GetOrNew<TKey, TResult>(this IDictionary<TKey, TResult> dic, TKey key) where TResult : new()
        {
            dic.ThrowIfNull("dic");

            TResult value;

            if (dic.ContainsKey(key))
            {
                value = dic[key];
            }
            else
            {
                value = new TResult();
                dic.Add(key, value);
            }

            return value;
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            dic.ThrowIfNull("dic");

            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
    }
}