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

namespace Barnwellsoft.Nrbf.Internal
{
    internal static class NrbfNodeTypeOperations
    {
        /// <summary>
        /// Checks if a <see cref="NrbfNodeType"/> is a numeric type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/> if the <see cref="NrbfNodeType"/> is a numeric type; otherwise <see langword="false"/>.</returns>
        public static bool IsNumeric(this NrbfNodeType type)
        {
            switch (type)
            {
                case NrbfNodeType.Byte:
                case NrbfNodeType.Char:
                case NrbfNodeType.Decimal:
                case NrbfNodeType.Double:
                case NrbfNodeType.Int16:
                case NrbfNodeType.Int32:
                case NrbfNodeType.Int64:
                case NrbfNodeType.SByte:
                case NrbfNodeType.Single:
                case NrbfNodeType.UInt16:
                case NrbfNodeType.UInt32:
                case NrbfNodeType.UInt64:
                    return true;

                case NrbfNodeType.Invalid:
                case NrbfNodeType.Boolean:
                case NrbfNodeType.TimeSpan:
                case NrbfNodeType.DateTime:
                case NrbfNodeType.String:
                case NrbfNodeType.Class:
                case NrbfNodeType.Array:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the <see cref="NrbfMemberType"/> for a primitive <see cref="NrbfNodeType"/>.
        /// </summary>
        /// <param name="type">The primitive <see cref="NrbfNodeType"/>.</param>
        /// <returns>
        /// The <see cref="NrbfMemberType"/> for the primitive <see cref="NrbfNodeType"/>.
        /// </returns>
        public static NrbfMemberType ToPrimitiveMemberType(this NrbfNodeType type)
        {
            if (!PrimitiveMemberTypes.TryGetValue(type, out var memberType))
                throw new ArgumentException("Type is not a primitive type.", nameof(type));

            return memberType;
        }

        public static ReadOnlyDictionary<NrbfNodeType, NrbfMemberType> PrimitiveMemberTypes { get; } =
            new ReadOnlyDictionary<NrbfNodeType, NrbfMemberType>(
                new Dictionary<NrbfNodeType, NrbfMemberType>()
                {
                    [NrbfNodeType.Boolean] = NrbfMemberType.Boolean,
                    [NrbfNodeType.Byte] = NrbfMemberType.Byte,
                    [NrbfNodeType.Char] = NrbfMemberType.Char,
                    [NrbfNodeType.Decimal] = NrbfMemberType.Decimal,
                    [NrbfNodeType.Double] = NrbfMemberType.Double,
                    [NrbfNodeType.Int16] = NrbfMemberType.Int16,
                    [NrbfNodeType.Int32] = NrbfMemberType.Int32,
                    [NrbfNodeType.Int64] = NrbfMemberType.Int64,
                    [NrbfNodeType.SByte] = NrbfMemberType.SByte,
                    [NrbfNodeType.Single] = NrbfMemberType.Single,
                    [NrbfNodeType.TimeSpan] = NrbfMemberType.TimeSpan,
                    [NrbfNodeType.DateTime] = NrbfMemberType.DateTime,
                    [NrbfNodeType.UInt16] = NrbfMemberType.UInt16,
                    [NrbfNodeType.UInt32] = NrbfMemberType.UInt32,
                    [NrbfNodeType.UInt64] = NrbfMemberType.UInt64,
                });
    }
}
