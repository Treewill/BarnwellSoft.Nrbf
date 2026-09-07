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
using Barnwellsoft.Nrbf.Internal;

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An NRBF class.
    /// </summary>
    public sealed class NrbfClass : NrbfNode
    {
        /// <summary>
        /// The values of the members.
        /// </summary>
        private readonly NrbfNode[] _values;

        /// <summary>
        /// The class type information.
        /// </summary>
        public NrbfClassInfo ClassInfo { get; }

        /// <summary>
        /// Gets a value at the given index.
        /// </summary>
        /// <param name="indices">The indices of the value to get.</param>
        /// <exception cref="KeyNotFoundException">
        /// <paramref name="indices"/> first element is not the name of a member.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="indices"/> does not have exactly one string value.
        /// -or-
        /// <paramref name="value"/> cannot be converted to the target type.
        /// </exception>
        public override NrbfNode this[params object[] indices]
        {
            get
            {
                if (indices.Length != 1)
                    throw new ArgumentException("Indices must have exactly one string value.");

                if (indices[0].GetType().IsArray)
                    return this[indices[0]];

                var element = indices[0];
                if (!(element is string index))
                    throw new ArgumentException("Index must be a string value.");

                return this[index];
            }
            set
            {
                if (indices.Length != 1)
                    throw new ArgumentException("Indices must have exactly one string value.");

                var element = indices[0];
                if (!(element is string index))
                    throw new ArgumentException("Index must be a string value.");

                this[index] = value;
            }
        }

        /// <summary>
        /// The member with the given name.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <exception cref="KeyNotFoundException">
        /// <paramref name="name"/> is not the name of a member.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> cannot be converted to the target type.
        /// </exception>
        public NrbfNode this[string name]
        {
            get
            {
                if (!ClassInfo.MemberIndices.TryGetValue(name, out var i))
                    throw new KeyNotFoundException();

                return _values[i];
            }
            set
            {
                if (!ClassInfo.MemberIndices.TryGetValue(name, out var i))
                    throw new KeyNotFoundException();

                var member = ClassInfo.Members[i];
                if (!member.SerializeTyped && !NrbfValueConversion.TryConvertNode(member.Type, ref value))
                    throw new ArgumentException($"Cannot convert value to {member.Type.ClassName}.");

                _values[i] = value;
            }
        }

        /// <summary>
        /// Creates an NRBF class.
        /// </summary>
        /// <param name="classInfo">The class type information.</param>
        public NrbfClass(NrbfClassInfo classInfo)
            : base(NrbfNodeType.Class)
        {
            ClassInfo = classInfo;
            _values = CreatesValues(classInfo);
        }

        /// <summary>
        /// Creates the values for an NRBF class with the class type information.
        /// </summary>
        /// <param name="classInfo">The class type information.</param>
        /// <returns>The values.</returns>
        private static NrbfNode[] CreatesValues(NrbfClassInfo classInfo)
        {
            var members = classInfo.Members;

            var values = new NrbfNode[members.Count];
            for (var i = 0; i < values.Length; ++i)
                values[i] = NrbfValueDefaulting.GetDefaultValue(members[i].Type);

            return values;
        }

        /// <summary>
        /// Unwraps the value in the node if it was wrapped.
        /// </summary>
        /// <returns>
        /// The value in the node, if wrapped;
        /// otherwise this.
        /// </returns>
        public override NrbfNode Unwrap()
        {
            if (ClassInfo.IsMatch(NrbfClassInfo.Boolean))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Byte))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Char))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Double))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Int16))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Int32))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Int64))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.SByte))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Single))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.UInt16))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.UInt32))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.UInt64))
                return UnwrapValue(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.Decimal))
                return UnwrapDecimal(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.TimeSpan))
                return UnwrapTimeSpan(this);

            if (ClassInfo.IsMatch(NrbfClassInfo.DateTime))
                return UnwrapDateTime(this);

            return this;
        }

        /// <summary>
        /// Unwraps a typical primitive.
        /// </summary>
        /// <param name="nrbfClass">The boxed primitive.</param>
        /// <returns>The primitive.</returns>
        private static NrbfNode UnwrapValue(NrbfClass nrbfClass) => 
            nrbfClass["m_value"];

        /// <summary>
        /// Unwraps a <see cref="Decimal"/>.
        /// </summary>
        /// <param name="nrbfClass">The boxed <see cref="Decimal"/>.</param>
        /// <returns>The <see cref="Decimal"/>.</returns>
        private static NrbfNode UnwrapDecimal(NrbfClass nrbfClass)
        {
            var lo = (int)nrbfClass["lo"];
            var mid = (int)nrbfClass["mid"];
            var hi = (int)nrbfClass["hi"];
            var flags = (int)nrbfClass["flags"];

            var isNegative = (flags & 0x80000000) != 0;
            var scale = (byte)(flags >> 16);

            return new decimal(lo, mid, hi, isNegative, scale);
        }

        /// <summary>
        /// Unwraps a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="nrbfClass">The boxed <see cref="TimeSpan"/>.</param>
        /// <returns>The <see cref="TimeSpan"/>.</returns>
        private static NrbfNode UnwrapTimeSpan(NrbfClass nrbfClass) => 
            new TimeSpan((long)nrbfClass["_ticks"]);

        /// <summary>
        /// Unwraps a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="nrbfClass">The boxed <see cref="DateTime"/>.</param>
        /// <returns>The <see cref="DateTime"/>.</returns>
        private static NrbfNode UnwrapDateTime(NrbfClass nrbfClass)
        {
            var ticks = (long)nrbfClass["ticks"];
            var dateData = (ulong)nrbfClass["dateData"];

            return new DateTime(ticks, (DateTimeKind)(dateData >> 62));
        }
    }
}