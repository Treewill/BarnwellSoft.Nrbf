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
    /// The NRBF record type enumeration.
    /// </summary>
    internal enum NrbfRecordType
    {
        /// <summary>
        /// Identifies the SerializationHeaderRecord.
        /// </summary>
        SerializedStreamHeader = 0,

        /// <summary>
        /// Identifies a ClassWithId record.
        /// </summary>
        ClassWithId = 1,

        /// <summary>
        /// Identifies a SystemClassWithMembers record.
        /// </summary>
        SystemClassWithMembers = 2,

        /// <summary>
        /// Identifies a ClassWithMembers record.
        /// </summary>
        ClassWithMembers = 3,

        /// <summary>
        /// Identifies a SystemClassWithMembersAndTypes record.
        /// </summary>
        SystemClassWithMembersAndTypes = 4,

        /// <summary>
        /// Identifies a ClassWithMembersAndTypes record.
        /// </summary>
        ClassWithMembersAndTypes = 5,

        /// <summary>
        /// Identifies a BinaryObjectString record.
        /// </summary>
        BinaryObjectString = 6,

        /// <summary>
        /// Identifies a MessageEnd record.
        /// </summary>
        BinaryArray = 7,

        /// <summary>
        /// Identifies a MemberPrimitiveTyped record.
        /// </summary>
        MemberPrimitiveTyped = 8,

        /// <summary>
        /// Identifies a MemberReference record.
        /// </summary>
        MemberReference = 9,

        /// <summary>
        /// Identifies an ObjectNull record.
        /// </summary>
        ObjectNull = 10,

        /// <summary>
        /// Identifies a MessageEnd record.
        /// </summary>
        MessageEnd = 11,

        /// <summary>
        /// Identifies a BinaryLibrary record.
        /// </summary>
        BinaryLibrary = 12,

        /// <summary>
        /// Identifies an ObjectNullMultiple256 record.
        /// </summary>
        ObjectNullMultiple256 = 13,

        /// <summary>
        /// Identifies an ObjectNullMultiple record.
        /// </summary>
        ObjectNullMultiple = 14,

        /// <summary>
        /// Identifies an ArraySinglePrimitive record.
        /// </summary>
        ArraySinglePrimitive = 15,

        /// <summary>
        /// Identifies an ArraySingleObject record.
        /// </summary>
        ArraySingleObject = 16,

        /// <summary>
        /// Identifies an ArraySingleString record.
        /// </summary>
        ArraySingleString = 17,

        /// <summary>
        /// Identifies a MethodCall record.
        /// </summary>
        MethodCall = 21,

        /// <summary>
        /// Identifies a MethodReturn record.
        /// </summary>
        MethodReturn = 22,
    }
}