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
    /// The NRBF binary type enumeration.
    /// </summary>
    internal enum NrbfBinaryType
    {
        /// <summary>
        /// The Remoting Type is defined in PrimitiveTypeEnumeration and the Remoting Type is not a string.
        /// </summary>
        Primitive = 0,

        /// <summary>
        /// The Remoting Type is a LengthPrefixedString.
        /// </summary>
        String = 1,

        /// <summary>
        /// The Remoting Type is System.Object.
        /// </summary>
        Object = 2,

        /// <summary>
        /// The remoting type is one of:
        /// <ul>
        /// <li> A Class in the System Library. </li>
        /// <li> An array whose Ultimate Array Item Type is a Class in the System Library </li>
        /// <li>
        /// An Array whose Ultimate Array Item Type is System.Object, String, or a Primitive Type
        /// but does not meet the definition of ObjectArray, StringArray, or PrimitiveArray.
        /// </li>
        /// </ul>
        /// </summary>
        SystemClass = 3,

        /// <summary>
        /// The Remoting Type is a Class or an Array whose Ultimate Array Item Type
        /// is a Class that is not in the System Library.
        /// </summary>
        Class = 4,

        /// <summary>
        /// The Remoting Type is a single-dimensional Array of
        /// System.Object with a lower bound of 0.
        /// </summary>
        ObjectArray = 5,

        /// <summary>
        /// The Remoting Type is a single-dimensional Array of
        /// String with a lower bound of 0.
        /// </summary>
        StringArray = 6,

        /// <summary>
        /// The Remoting Type is a single-dimensional Array of
        /// a Primitive Type with a lower bound of 0.
        /// </summary>
        PrimitiveArray = 7,
    }
}