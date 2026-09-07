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

namespace Barnwellsoft.Nrbf.Internal
{
    /// <summary>
    /// The NRBF primitive type enumeration.
    /// </summary>
    internal enum NrbfPrimitiveType
    {
        /// <summary>
        /// Identifies a BOOLEAN.
        /// </summary>
        Boolean = 1,

        /// <summary>
        /// Identifies a BYTE.
        /// </summary>
        Byte = 2,

        /// <summary>
        /// Identifies a Char.
        /// </summary>
        Char = 3,

        /// <summary>
        /// Identifies a Decimal.
        /// </summary>
        Decimal = 5,

        /// <summary>
        /// Identifies a Double.
        /// </summary>
        Double = 6,

        /// <summary>
        /// Identifies an INT16.
        /// </summary>
        Int16 = 7,

        /// <summary>
        /// Identifies an INT32.
        /// </summary>
        Int32 = 8,

        /// <summary>
        /// Identifies an INT64.
        /// </summary>
        Int64 = 9,

        /// <summary>
        /// Identifies an INT8.
        /// </summary>
        SByte = 10,

        /// <summary>
        /// Identifies a Single.
        /// </summary>
        Single = 11,

        /// <summary>
        /// Identifies a TimeSpan.
        /// </summary>
        TimeSpan = 12,

        /// <summary>
        /// Identifies a DateTime.
        /// </summary>
        DateTime = 13,

        /// <summary>
        /// Identifies an UINT16.
        /// </summary>
        UInt16 = 14,

        /// <summary>
        /// Identifies an UINT32.
        /// </summary>
        UInt32 = 15,

        /// <summary>
        /// Identifies an UINT64.
        /// </summary>
        UInt64 = 16,

        // TODO: Note: Unsupported during serializations yet defined.
        /// <summary>
        /// Identifies a Null Object.
        /// </summary>
        Null = 17,

        // TODO: Note: Unsupported during serializations yet defined.
        /// <summary>
        /// Identifies a LengthPrefixedString. 
        /// </summary>
        String = 18,
    }
}
