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

namespace Barnwellsoft.Nrbf.Internal
{
    internal static class NrbfBinaryTypeConversion
    {
        public static NrbfBinaryType GetBinaryType(this NrbfClassMember member)
        {
            var binaryType = member.Type.GetBinaryType();

            if (!member.SerializeTyped)
                return binaryType;

            switch (binaryType)
            {
                case NrbfBinaryType.Primitive:
                    return NrbfBinaryType.SystemClass;

                case NrbfBinaryType.PrimitiveArray:
                    return NrbfBinaryType.ObjectArray;

                case NrbfBinaryType.String:
                case NrbfBinaryType.Object:
                case NrbfBinaryType.SystemClass:
                case NrbfBinaryType.Class:
                case NrbfBinaryType.ObjectArray:
                case NrbfBinaryType.StringArray:
                default:
                    return binaryType;
            }
        }

        public static NrbfBinaryType GetBinaryType(this NrbfMemberType type)
        {
            if (type.IsArray)
                return GetArrayBinaryType(type);

            if (!type.Library.IsSystemLibrary)
                return NrbfBinaryType.Class;

            if (type.Equals(NrbfMemberType.String))
                return NrbfBinaryType.String;

            if (type.IsPrimitiveBinaryType())
                return NrbfBinaryType.Primitive;

            if (type.Equals(NrbfMemberType.Object))
                return NrbfBinaryType.Object;

            return NrbfBinaryType.SystemClass;
        }

        private static NrbfBinaryType GetArrayBinaryType(NrbfMemberType type)
        {
            switch (type.GetUltimateArrayElementType().GetBinaryType())
            {
                case NrbfBinaryType.Primitive:
                    return NrbfBinaryType.PrimitiveArray;

                case NrbfBinaryType.String:
                    return NrbfBinaryType.StringArray;

                case NrbfBinaryType.Object:
                    return NrbfBinaryType.ObjectArray;

                case NrbfBinaryType.SystemClass:
                    return NrbfBinaryType.SystemClass;

                case NrbfBinaryType.Class:
                    return NrbfBinaryType.Class;

                default:
                    throw new InvalidOperationException("Unexpected ultimate array element type.");
            }
        }
    }
}