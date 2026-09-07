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
using System.Collections.Generic;
using System.Globalization;

using Barnwellsoft.Nrbf.Internal;

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An NRBF node.
    /// </summary>
    public abstract class NrbfNode
    {
        /// <summary>
        /// The node type.
        /// </summary>
        public NrbfNodeType Type { get; }

        /// <summary>
        /// Gets a value at the given index.
        /// </summary>
        /// <param name="indices">The indices of the value to get.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="indices"/> does not have the right number of indices.
        /// -or-
        /// <paramref name="indices"/> values are not the right type.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// <paramref name="indices"/> does not match a member.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="indices"/> is outside the collection.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// this value does not support indexing.
        /// </exception>
        public abstract NrbfNode this[params object[] indices]
        {
            get;
            set;
        }

        /// <summary>
        /// Creates an NRBF node of the given type.
        /// </summary>
        /// <param name="type">The node type.</param>
        internal NrbfNode(NrbfNodeType type)
        {
            Type = type;
        }

        /// <summary>
        /// Unwraps the value in the node if it was wrapped.
        /// </summary>
        /// <returns>
        /// The value in the node, if wrapped;
        /// otherwise <see langword="this"/>.
        /// </returns>
        public virtual NrbfNode Unwrap() => this;

        /// <summary>
        /// Wraps a value if the value can be wrapped.
        /// </summary>
        /// <returns>
        /// The wrapped value, if the value can be wrapped;
        /// otherwise <see langword="this"/>.
        /// </returns>
        public virtual NrbfNode Wrap() => this;

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Boolean"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Boolean"/>.</exception>
        public static explicit operator bool(NrbfNode value)
        {
            if (value.Type != NrbfNodeType.Boolean)
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Boolean.",
                        value.Type));
            return (bool)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Byte"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Byte"/>.</exception>
        public static explicit operator byte(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Byte.",
                        value.Type));

            return (byte)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Char"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Char"/>.</exception>
        public static explicit operator char(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Char.",
                        value.Type));

            return (char)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Decimal"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Decimal"/>.</exception>
        public static explicit operator decimal(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Decimal.",
                        value.Type));

            return (decimal)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Double"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Double"/>.</exception>
        public static explicit operator double(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Double.",
                        value.Type));

            return (double)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Int16"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Int16"/>.</exception>
        public static explicit operator short(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Int16.",
                        value.Type));

            return (short)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Int32"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Int32"/>.</exception>
        public static explicit operator int(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Int32.",
                        value.Type));

            return (int)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Int64"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Int64"/>.</exception>
        public static explicit operator long(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Int64.",
                        value.Type));

            return (long)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="SByte"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="SByte"/>.</exception>
        public static explicit operator sbyte(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to SByte.",
                        value.Type));

            return (sbyte)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="Single"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="Single"/>.</exception>
        public static explicit operator float(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to Single.",
                        value.Type));

            return (float)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="TimeSpan"/>.</exception>
        public static explicit operator TimeSpan(NrbfNode value)
        {
            if (value.Type != NrbfNodeType.TimeSpan)
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to TimeSpan.",
                        value.Type));

            return (TimeSpan)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="DateTime"/>.</exception>
        public static explicit operator DateTime(NrbfNode value)
        {
            if (value.Type != NrbfNodeType.DateTime)
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to DateTime.",
                        value.Type));

            return (DateTime)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="UInt16"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="UInt16"/>.</exception>
        public static explicit operator ushort(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to UInt16.",
                        value.Type));

            return (ushort)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="UInt32"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="UInt32"/>.</exception>
        public static explicit operator uint(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to UInt32.",
                        value.Type));

            return (uint)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="UInt64"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="UInt64"/>.</exception>
        public static explicit operator ulong(NrbfNode value)
        {
            if (!value.Type.IsNumeric())
                    throw new InvalidCastException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Cannot convert {0} to UInt64.",
                            value.Type));

            return (ulong)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="NrbfNode"/> to <see cref="String"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="InvalidCastException">When the value cannot be converted to <see cref="String"/>.</exception>
        public static explicit operator string(NrbfNode value)
        {
            if (value is null)
                return null;

            if (value.Type != NrbfNodeType.String)
                throw new InvalidCastException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot convert {0} to String.",
                        value.Type));

            return (string)((NrbfValue)value).Value;
        }

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Boolean"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(bool value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Byte"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(byte value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Char"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(char value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Decimal"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(decimal value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Double"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(double value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Int16"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(short value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Int32"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(int value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Int64"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(long value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="SByte"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(sbyte value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="Single"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(float value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="TimeSpan"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(TimeSpan value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="DateTime"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(DateTime value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="UInt16"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(ushort value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="UInt32"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(uint value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="UInt64"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(ulong value) =>
            new NrbfValue(value);

        /// <summary>
        /// Preforms an implicit conversion from <see cref="String"/> to <see cref="NrbfNode"/>.
        /// </summary>
        /// <param name="value">The value to create an <see cref="NrbfValue"/> from.</param>
        /// <returns>The <see cref="NrbfValue"/> initialized with the specified value.</returns>
        public static implicit operator NrbfNode(string value)
        {
            if (value is null)
                return null;
            return new NrbfValue(value);
        }
    }
}
