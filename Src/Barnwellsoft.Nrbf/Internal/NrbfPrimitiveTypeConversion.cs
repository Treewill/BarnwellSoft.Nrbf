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
using System.Collections.ObjectModel;
using System.Linq;

namespace Barnwellsoft.Nrbf.Internal
{
    internal static class NrbfPrimitiveTypeConversion
    {
        public static NrbfMemberType ToMemberType(this NrbfPrimitiveType type)
        {
            if (!PrimitiveMemberTypes.TryGetValue(type, out NrbfMemberType memberType))
                throw new ArgumentOutOfRangeException(nameof(type), type, null);

            return memberType;
        }

        public static NrbfPrimitiveType ToPrimitiveType(this NrbfMemberType type)
        {
            if (!MemberPrimitiveTypes.TryGetValue(type, out NrbfPrimitiveType primitiveType))
                throw new ArgumentOutOfRangeException(nameof(type), type, null);

            return primitiveType;
        }

        public static ReadOnlyDictionary<NrbfPrimitiveType, NrbfMemberType> PrimitiveMemberTypes { get; } =
            new ReadOnlyDictionary<NrbfPrimitiveType, NrbfMemberType>(
                new Dictionary<NrbfPrimitiveType, NrbfMemberType>()
                {
                    [NrbfPrimitiveType.Boolean] = NrbfMemberType.Boolean,
                    [NrbfPrimitiveType.Byte] = NrbfMemberType.Byte,
                    [NrbfPrimitiveType.Char] = NrbfMemberType.Char,
                    [NrbfPrimitiveType.Decimal] = NrbfMemberType.Decimal,
                    [NrbfPrimitiveType.Double] = NrbfMemberType.Double,
                    [NrbfPrimitiveType.Int16] = NrbfMemberType.Int16,
                    [NrbfPrimitiveType.Int32] = NrbfMemberType.Int32,
                    [NrbfPrimitiveType.Int64] = NrbfMemberType.Int64,
                    [NrbfPrimitiveType.SByte] = NrbfMemberType.SByte,
                    [NrbfPrimitiveType.Single] = NrbfMemberType.Single,
                    [NrbfPrimitiveType.TimeSpan] = NrbfMemberType.TimeSpan,
                    [NrbfPrimitiveType.DateTime] = NrbfMemberType.DateTime,
                    [NrbfPrimitiveType.UInt16] = NrbfMemberType.UInt16,
                    [NrbfPrimitiveType.UInt32] = NrbfMemberType.UInt32,
                    [NrbfPrimitiveType.UInt64] = NrbfMemberType.UInt64,
                    [NrbfPrimitiveType.Null] = null,
                    [NrbfPrimitiveType.String] = NrbfMemberType.String,
                });

        public static ReadOnlyDictionary<NrbfMemberType, NrbfPrimitiveType> MemberPrimitiveTypes { get; } =
            new ReadOnlyDictionary<NrbfMemberType, NrbfPrimitiveType>(
                PrimitiveMemberTypes
                    .Where(it => !(it.Value is null))
                    .ToDictionary(
                        it => it.Value,
                        it => it.Key));
    }
}