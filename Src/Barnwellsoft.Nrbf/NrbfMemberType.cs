#region license
// Copyright (c) 2026 Trevor Barnwell
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the “Software”), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An NRBF member type.
    /// </summary>
    public sealed class NrbfMemberType : IEquatable<NrbfMemberType>
    {
        /// <inheritdoc cref="Library"/>
        private NrbfLibrary _library;

        /// <summary>
        /// The name of the type.
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// True if the type is an array type.
        /// </summary>
        public bool IsArray => ArrayRank > 0;

        /// <summary>
        /// The array rank if the type is an array;
        /// otherwise zero.
        /// </summary>
        public int ArrayRank { get; }

        /// <summary>
        /// A value that indicates whether the <see cref="NrbfMemberType"/> is currently modifiable.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the <see cref="NrbfMemberType"/> is frozen and cannot be modified;
        /// <see langword="false"/> if the <see cref="NrbfMemberType"/> can be modified.
        /// </returns>
        public bool IsFrozen
        {
            get;
            private set;
        }

        /// <summary>
        /// The library for the type.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When <see cref="IsFrozen"/> and a new value is provided.
        /// </exception>
        public NrbfLibrary Library
        {
            get => _library;
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                if (ReferenceEquals(_library, value))
                    return;

                if (IsFrozen)
                    throw new InvalidOperationException("Cannot change frozen member type.");

                _library = value;
            }
        }

        /// <summary>
        /// Gets a member type for an array whose elements are this type.
        /// </summary>
        /// <param name="rank">The rank of the array.</param>
        /// <returns>
        /// A member type for an array whose elements are this type.
        /// </returns>
        public NrbfMemberType GetArray(int rank = 1)
        {
            if (rank < 1)
                throw new ArgumentOutOfRangeException(nameof(rank), "Rank must be greater than or equal to 1.");

            var suffix = new char[rank + 1];
            suffix[0] = '[';
            suffix[rank] = ']';
            for (var i = 1; i < rank; ++i)
                suffix[i] = ',';

            return new NrbfMemberType(ClassName + new string(suffix), _library, IsFrozen);
        }

        /// <summary>
        /// An NRBF member type.
        /// </summary>
        /// <param name="className">The name of the type.</param>
        /// <param name="library">The library for the type.</param>
        /// <param name="isFrozen">
        /// A value that indicates whether the <see cref="NrbfMemberType"/> is currently modifiable.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="className"/> is null.
        /// -or-
        /// <paramref name="library"/> is null.
        /// </exception>
        public NrbfMemberType(
            string className,
            NrbfLibrary library,
            bool isFrozen = true)
        {
            if (className is null)
                throw new ArgumentNullException(nameof(className));

            if (library is null)
                throw new ArgumentNullException(nameof(library));

            _library = library;
            IsFrozen = isFrozen;
            ClassName = className;
            ArrayRank = GetArrayRank(ClassName);
        }

        /// <summary>
        /// Makes the <see cref="NrbfMemberType"/> unmodifiable and
        /// sets <see cref="IsFrozen"/> to <see langword="true"/>.
        /// </summary>
        public void Freeze() =>
            IsFrozen = true;

        /// <summary>
        /// Indicates whether the current member type is equal to another member type.
        /// </summary>
        /// <param name="other">A member type to compare with this member type.</param>
        /// <returns>
        /// <see langword="true"/>if this member type is equal to <paramref name="other"/>;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool Equals(NrbfMemberType other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            var equal =
                ClassName == other.ClassName
                && ReferenceEquals(_library, other._library);

            return equal;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is NrbfMemberType other))
                return false;
            return Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => 
            ClassName.GetHashCode();

        /// <summary>
        /// Indicates whether the values of two specified <see cref="NrbfMemberType"/> objects are equal.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> are equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(NrbfMemberType a, NrbfMemberType b) =>
            a?.Equals(b) ?? b is null;

        /// <summary>
        /// Indicates whether the values of two specified <see cref="NrbfMemberType"/> objects are not equal.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> are not equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(NrbfMemberType a, NrbfMemberType b) =>
            !(a == b);

        /// <summary>
        /// A member type representing a <see cref="System.Boolean"/>.
        /// </summary>
        public static NrbfMemberType Boolean { get; } =
            new NrbfMemberType(
                "System.Boolean",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.Byte"/>.
        /// </summary>
        public static NrbfMemberType Byte { get; } =
            new NrbfMemberType(
                "System.Byte",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.Char"/>.
        /// </summary>
        public static NrbfMemberType Char { get; } =
            new NrbfMemberType(
                "System.Char",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.Decimal"/>.
        /// </summary>
        public static NrbfMemberType Decimal { get; } =
            new NrbfMemberType(
                "System.Decimal",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.Double"/>.
        /// </summary>
        public static NrbfMemberType Double { get; } =
            new NrbfMemberType(
                "System.Double",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.Int16"/>.
        /// </summary>
        public static NrbfMemberType Int16 { get; } =
            new NrbfMemberType(
                "System.Int16",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.Int32"/>.
        /// </summary>
        public static NrbfMemberType Int32 { get; } =
            new NrbfMemberType(
                "System.Int32",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.Int64"/>.
        /// </summary>
        public static NrbfMemberType Int64 { get; } =
            new NrbfMemberType(
                "System.Int64",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.SByte"/>.
        /// </summary>
        public static NrbfMemberType SByte { get; } =
            new NrbfMemberType(
                "System.SByte",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.Single"/>.
        /// </summary>
        public static NrbfMemberType Single { get; } =
            new NrbfMemberType(
                "System.Single",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.TimeSpan"/>.
        /// </summary>
        public static NrbfMemberType TimeSpan { get; } =
            new NrbfMemberType(
                "System.TimeSpan",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.DateTime"/>.
        /// </summary>
        public static NrbfMemberType DateTime { get; } =
            new NrbfMemberType(
                "System.DateTime",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.UInt16"/>.
        /// </summary>
        public static NrbfMemberType UInt16 { get; } =
            new NrbfMemberType(
                "System.UInt16",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.UInt32"/>.
        /// </summary>
        public static NrbfMemberType UInt32 { get; } =
            new NrbfMemberType(
                "System.UInt32",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.UInt64"/>.
        /// </summary>
        public static NrbfMemberType UInt64 { get; } =
            new NrbfMemberType(
                "System.UInt64",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing a <see cref="System.String"/>.
        /// </summary>
        public static NrbfMemberType String { get; } =
            new NrbfMemberType(
                "System.String",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// A member type representing an <see cref="System.Object"/>.
        /// </summary>
        public static NrbfMemberType Object { get; } =
            new NrbfMemberType(
                "System.Object",
                NrbfLibrary.SystemLibrary);

        /// <summary>
        /// Gets the array rank from the type name.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <returns>
        /// The array rank of the type if it is an array;
        /// zero otherwise.
        /// </returns>
        private static int GetArrayRank(string typeName)
        {
            var end = typeName.Length - 1;
            if (typeName[end] != ']')
                return 0;

            --end;
            while (end > 0 && typeName[end] == ',')
                --end;

            if (end == 0)
                return 0;

            if (typeName[end] != '[')
                return 0;

            return typeName.Length - end - 1;
        }
    }
}
