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
using System.Globalization;

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An NRBF value node.
    /// </summary>
    public sealed class NrbfValue :
        NrbfNode
        , IEquatable<NrbfValue>
#if HAVE_ICONVERTIBLE
        , IConvertible
#endif
    {
        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/>.
        /// The operation is not supported.
        /// </summary>
        /// <param name="indices">Unused.</param>
        /// <exception cref="InvalidOperationException">Always.</exception>
        public override NrbfNode this[params object[] indices]
        {
            get =>
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot access child value on {0}.",
                        Type));
            set =>
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot set child value on {0}.",
                        Type));
        }

        /// <summary>
        /// The underlying value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Creates an NRBF value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="type">The type of the value.</param>
        private NrbfValue(object value, NrbfNodeType type)
            : base(type)
        {
            Value = value;
        }

        /// <summary>
        /// Creates an NRBF <see cref="Boolean"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(bool value)
            : this(value, NrbfNodeType.Boolean)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Byte"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(byte value)
            : this(value, NrbfNodeType.Byte)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Char"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(char value)
            : this(value, NrbfNodeType.Char)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Decimal"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(decimal value)
            : this(value, NrbfNodeType.Decimal)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Double"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(double value)
            : this(value, NrbfNodeType.Double)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Int16"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(short value)
            : this(value, NrbfNodeType.Int16)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Int32"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(int value)
            : this(value, NrbfNodeType.Int32)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Int64"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(long value)
            : this(value, NrbfNodeType.Int64)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="SByte"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(sbyte value)
            : this(value, NrbfNodeType.SByte)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="Single"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(float value)
            : this(value, NrbfNodeType.Single)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="TimeSpan"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(TimeSpan value)
            : this(value, NrbfNodeType.TimeSpan)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="DateTime"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(DateTime value)
            : this(value, NrbfNodeType.DateTime)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="UInt16"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(ushort value)
            : this(value, NrbfNodeType.UInt16)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="UInt32"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(uint value)
            : this(value, NrbfNodeType.UInt32)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="UInt64"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(ulong value)
            : this(value, NrbfNodeType.UInt64)
        {
        }

        /// <summary>
        /// Creates an NRBF <see cref="String"/> value node.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public NrbfValue(string value)
            : this(value, NrbfNodeType.String)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc/>
        public override NrbfNode Wrap()
        {
            switch (Type)
            {
                case NrbfNodeType.Boolean:
                    return WrapValue(this, NrbfClassInfo.Boolean);

                case NrbfNodeType.Byte:
                    return WrapValue(this, NrbfClassInfo.Byte);

                case NrbfNodeType.Char:
                    return WrapValue(this, NrbfClassInfo.Char);

                case NrbfNodeType.Double:
                    return WrapValue(this, NrbfClassInfo.Double);

                case NrbfNodeType.Int16:
                    return WrapValue(this, NrbfClassInfo.Int16);

                case NrbfNodeType.Int32:
                    return WrapValue(this, NrbfClassInfo.Int32);

                case NrbfNodeType.Int64:
                    return WrapValue(this, NrbfClassInfo.Int64);

                case NrbfNodeType.SByte:
                    return WrapValue(this, NrbfClassInfo.SByte);

                case NrbfNodeType.Single:
                    return WrapValue(this, NrbfClassInfo.Single);

                case NrbfNodeType.UInt16:
                    return WrapValue(this, NrbfClassInfo.UInt16);

                case NrbfNodeType.UInt32:
                    return WrapValue(this, NrbfClassInfo.UInt32);

                case NrbfNodeType.UInt64:
                    return WrapValue(this, NrbfClassInfo.UInt64);

                case NrbfNodeType.Decimal:
                    return WrapDecimal(this);

                case NrbfNodeType.TimeSpan:
                    return WrapTimeSpan(this);

                case NrbfNodeType.DateTime:
                    return WrapDateTime(this);

                case NrbfNodeType.String:
                    return this;

                default:
                    throw new InvalidOperationException("NrbfValue is invalid.");
            }
        }

        /// <summary>
        /// Indicates whether the current value is equal to another value.
        /// </summary>
        /// <param name="other">A value to compare with this value.</param>
        /// <returns>
        /// <see langword="true"/>if this value is equal to <paramref name="other"/>;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool Equals(NrbfValue other)
        {
            return
                !(other is null)
                && Type == other.Type
                && Value.Equals(other.Value);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return
                obj is NrbfValue other
                && Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() =>
            Value.ToString();

#if HAVE_ICONVERTIBLE
        /// <summary>
        /// Returns the <see cref="TypeCode"/> for this instance.
        /// </summary>
        /// <returns>
        /// The enumerated constant that is the <see cref="TypeCode"/> of the class or
        /// value type that implements this interface.
        /// </returns>
        public TypeCode GetTypeCode() => 
            Convert.GetTypeCode(Value);
#endif

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent <see cref="Boolean"/> value
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A <see cref="Boolean"/> value equivalent to the value of this instance.
        /// </returns>
        public bool ToBoolean(IFormatProvider provider) => 
            Convert.ToBoolean(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 8-bit unsigned integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 8-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public byte ToByte(IFormatProvider provider) => 
            Convert.ToByte(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent Unicode character
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A Unicode character equivalent to the value of this instance.
        /// </returns>
        public char ToChar(IFormatProvider provider) => 
            Convert.ToChar(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent <see cref="DateTime"/>
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> instance equivalent to the value of this instance.
        /// </returns>
        public DateTime ToDateTime(IFormatProvider provider) => 
            Convert.ToDateTime(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent <see cref="Decimal"/> number
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A <see cref="Decimal"/> number equivalent to the value of this instance.
        /// </returns>
        public decimal ToDecimal(IFormatProvider provider) => 
            Convert.ToDecimal(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent double-precision floating-point number
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A double-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        public double ToDouble(IFormatProvider provider) => 
            Convert.ToDouble(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 16-bit signed integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// An 16-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public short ToInt16(IFormatProvider provider) => 
            Convert.ToInt16(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 32-bit signed integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// An 32-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public int ToInt32(IFormatProvider provider) => 
            Convert.ToInt32(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 64-bit signed integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// An 64-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public long ToInt64(IFormatProvider provider) => 
            Convert.ToInt64(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 8-bit signed integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// An 8-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public sbyte ToSByte(IFormatProvider provider) => 
            Convert.ToSByte(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent single-precision floating-point number
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A single-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        public float ToSingle(IFormatProvider provider) => 
            Convert.ToSingle(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent <see cref="String"/>
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> instance equivalent to the value of this instance.
        /// </returns>
        public string ToString(IFormatProvider provider) => 
            Convert.ToString(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an <see cref="Object"/> of the specified <see cref="Type"/> that has an equivalent value,
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="conversionType">
        /// The <see cref="Type"/> to which the value of this instance is converted.
        /// </param>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.</param>
        /// <returns>
        /// An <see cref="Object"/> instance of type conversionType whose value is equivalent to the value of this instance.
        /// </returns>
        public object ToType(Type conversionType, IFormatProvider provider) => 
            Convert.ChangeType(Value, conversionType, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 16-bit unsigned integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 16-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public ushort ToUInt16(IFormatProvider provider) => 
            Convert.ToUInt16(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 32-bit unsigned integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 32-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public uint ToUInt32(IFormatProvider provider) => 
            Convert.ToUInt32(Value, provider);

        /// <summary>
        /// Converts the value of this instance to
        /// an equivalent 64-bit unsigned integer
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> interface implementation that
        /// supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 64-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public ulong ToUInt64(IFormatProvider provider) => 
            Convert.ToUInt64(Value, provider);

        /// <summary>
        /// Wraps a primitive value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="classInfo">The primitive class info.</param>
        /// <returns>The wrapped value.</returns>
        private static NrbfNode WrapValue(
            NrbfValue value,
            NrbfClassInfo classInfo)
        {
            return new NrbfClass(classInfo)
            {
                ["m_value"] = value
            };
        }

        /// <summary>
        /// Wraps a decimal value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The wrapped value.</returns>
        private static NrbfNode WrapDecimal(NrbfValue value)
        {
            var bits = decimal.GetBits((decimal)value.Value);

            return new NrbfClass(NrbfClassInfo.Decimal)
            {
                ["lo"] = bits[0],
                ["mid"] = bits[1],
                ["hi"] = bits[2],
                ["flags"] = bits[3],
            };
        }

        /// <summary>
        /// Wraps a TimeSpan value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The wrapped value.</returns>
        private static NrbfNode WrapTimeSpan(NrbfValue value)
        {
            var timeSpan = (TimeSpan)value.Value;

            return new NrbfClass(NrbfClassInfo.TimeSpan)
            {
                ["_ticks"] = timeSpan.Ticks
            };
        }

        /// <summary>
        /// Wraps a DateTime value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The wrapped value.</returns>
        private static NrbfNode WrapDateTime(NrbfValue value)
        {
            var dateTime = (DateTime)value.Value;
            var ticks = dateTime.Ticks;
            var kind = dateTime.Kind;

            return new NrbfClass(NrbfClassInfo.DateTime)
            {
                ["ticks"] = ticks,
                ["dateData"] = ((ulong)kind << 62) | (ulong)ticks,
            };
        }
    }
}
