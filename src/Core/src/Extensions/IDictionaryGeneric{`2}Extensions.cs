﻿// Copyright (c) Teroneko.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Teronis.Extensions
{
    public static class IDictionaryGenericExtensions
    {
        public static V AddOrUpdate<K, V>(this IDictionary<K, V> dictionary, K key, V value)
            where K : notnull
        {
            if (!dictionary.ContainsKey(key)) {
                return dictionary.AddAndReturn(key, value);
            } else {
                return dictionary[key] = value;
            }
        }

        public static V AddOrUpdate<K, V>(this IDictionary<K, V> dictionary, K key, V value, Func<V, V> repalceValue)
            where K : notnull
        {
            repalceValue = repalceValue ?? throw new ArgumentNullException(nameof(repalceValue));

            if (dictionary.TryGetValue(key, out var dictionaryValue)) {

                if (repalceValue != null) {
                    return dictionary[key] = repalceValue(dictionaryValue);
                }

                return dictionaryValue;
            }

            return dictionary.AddAndReturn(key, value);
        }

        /// <summary>
        /// Does not throw an exception when key does not exist, instead the default value will be returned.
        /// </summary>
        [return: MaybeNull]
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TKey : notnull =>
            dictionary.TryGetValue(key, out TValue? value) ? value : value;

        /// <summary>
        /// Does not throw an exception when key does not exist, instead the default nullable value will be returned.
        /// </summary>
        public static V? GetNullableStructureValue<K, V>(this IDictionary<K, V> dictionary, K key)
            where K : notnull
            where V : struct =>
            dictionary.TryGetValue(key, out V value) ? (V?)value : default(V?);

        /// <summary>
        /// Does not throw an exception when key does not exist, instead the default nullable value will be returned.
        /// </summary>
        public static V? GetReadOnlyNullableStructureValue<K, V>(this IReadOnlyDictionary<K, V> dictionary, K key)
            where K : notnull
            where V : struct =>
            dictionary.TryGetValue(key, out V value) ? (V?)value : default(V?);

        public static V AddAndReturn<K, V>(this IDictionary<K, V> source, K key, V value)
            where K : notnull
        {
            source.Add(key, value);
            return value;
        }

        public static V RemoveAndReturn<K, V>(this IDictionary<K, V> source, K key)
            where K : notnull
        {
            var value = source[key];
            source.Remove(key);
            return value;
        }

        public static bool TryRemove<K, V>(this IDictionary<K, V> source, K key, [MaybeNullWhen(false)] out V value)
            where K : notnull
        {
            if (source.TryGetValue(key, out value)) {
                source.Remove(key);
                return true;
            } else {
                return false;
            }
        }
    }
}
