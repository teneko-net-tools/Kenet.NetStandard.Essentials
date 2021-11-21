// Copyright (c) Teroneko.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Teronis.Utils
{
    /// <summary>
    /// Contains utilities for <see cref="object"/>.
    /// </summary>
    public static class ObjectUtils
    {
        /// <summary>
        /// Gets the value of a Property for a given object instance.
        /// </summary>
        /// <typeparam name="TValue">The <see cref="Type"/> you want the value to be converted to when returned.</typeparam>
        /// <param name="instance">The Type instance to extract the Property's data from.</param>
        /// <param name="propertyName">The name of the Property to extract the data from.</param>
        /// <param name="flags">The binding flags</param>
        [return: MaybeNull]
        internal static TValue GetPropertyValue<TValue>(object instance, string propertyName, BindingFlags flags = 0)
        {
            var pi = instance.GetType().GetProperty(propertyName, flags);
            return (TValue?)pi?.GetValue(instance, null);
        }

        /// <summary>
        /// Gets the value of a Property for a given object instance.
        /// </summary>
        /// <typeparam name="TValue">The <see cref="Type"/> you want the value to be converted to when returned.</typeparam>
        /// <param name="instance">The Type instance to extract the Property's data from.</param>
        /// <param name="fieldName">The name of the Property to extract the data from.</param>
        /// <param name="flags">The binding flags</param>
        [return: MaybeNull]
        internal static TValue GetFieldValue<TValue>(object instance, string fieldName, BindingFlags flags = 0)
        {
            var pi = instance.GetType().GetField(fieldName, flags);
            return (TValue?)pi?.GetValue(instance);
        }

        /// <summary>
        /// Checks nullability of <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// True if <paramref name="obj"/> is
        /// <br/>1. null,
        /// <br/>2. is reference type,
        /// <br/>3. is of type <see cref="Nullable{T}"/>,
        /// otherwise false.
        /// </returns>
        public static bool IsNullable(object obj)
        {
            if (obj == null) {
                return true; // obvious
            }
            
            var type = obj.GetType();

            if (!type.IsValueType) {
                return true; // ref-type
            }

            if (Nullable.GetUnderlyingType(type) != null) {
                return true; // Nullable<T>
            }

            return false; // value-type
        }
    }
}
