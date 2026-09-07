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

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// The type of <see cref="NrbfNode"/>.
    /// </summary>
    public enum NrbfNodeType
    {
        /// <summary>
        /// An invalid node type.
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// A <see cref="System.Boolean"/> value.
        /// </summary>
        Boolean = 1,

        /// <summary>
        /// A <see cref="System.Byte"/> value.
        /// </summary>
        Byte = 2,

        /// <summary>
        /// A <see cref="System.Char"/> value.
        /// </summary>
        Char = 3,

        /// <summary>
        /// A <see cref="System.Decimal"/> value.
        /// </summary>
        Decimal = 4,

        /// <summary>
        /// A <see cref="System.Double"/> value.
        /// </summary>
        Double = 5,

        /// <summary>
        /// An <see cref="System.Int16"/> value.
        /// </summary>
        Int16 = 6,

        /// <summary>
        /// An <see cref="System.Int32"/> value.
        /// </summary>
        Int32 = 7,

        /// <summary>
        /// An <see cref="System.Int64"/> value.
        /// </summary>
        Int64 = 8,

        /// <summary>
        /// An <see cref="System.SByte"/> value.
        /// </summary>
        SByte = 9,

        /// <summary>
        /// A <see cref="System.Single"/> value.
        /// </summary>
        Single = 10,

        /// <summary>
        /// A <see cref="System.TimeSpan"/> value.
        /// </summary>
        TimeSpan = 11,

        /// <summary>
        /// A <see cref="System.DateTime"/> value.
        /// </summary>
        DateTime = 12,

        /// <summary>
        /// A <see cref="System.UInt16"/> value.
        /// </summary>
        UInt16 = 13,

        /// <summary>
        /// A <see cref="System.UInt32"/> value.
        /// </summary>
        UInt32 = 14,

        /// <summary>
        /// A <see cref="System.UInt64"/> value.
        /// </summary>
        UInt64 = 15,

        /// <summary>
        /// A <see cref="System.String"/> value.
        /// </summary>
        String = 16,

        /// <summary>
        /// A <see cref="NrbfClass"/> value.
        /// </summary>
        Class = 17,

        /// <summary>
        /// An <see cref="NrbfArray"/> value.
        /// </summary>
        Array = 18,
    }
}
