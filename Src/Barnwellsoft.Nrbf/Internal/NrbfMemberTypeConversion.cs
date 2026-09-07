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
    internal static class NrbfMemberTypeConversion
    {
        public static bool IsPrimitiveBinaryType(this NrbfMemberType type)
        {
            var types = NrbfPrimitiveTypeConversion.MemberPrimitiveTypes;
            if (!types.TryGetValue(type, out var primitive))
                return false;

            return primitive != NrbfPrimitiveType.String;
        }

        public static NrbfMemberType GetArrayElementType(
            this NrbfMemberType type)
        {
            if (!type.IsArray)
                throw new ArgumentException("Type is not an array type", nameof(type));

            var className = type.ClassName
                .Substring(0, type.ClassName.Length - 2);

            return new NrbfMemberType(className, type.Library, type.IsFrozen);
        }

        public static NrbfMemberType GetUltimateArrayElementType(
            this NrbfMemberType type)
        {
            var name = type.ClassName;
            var end = name.Length;

            var lastEnd = end;
            while (true)
            {
                --end;
                if (name[end] != ']')
                    break;

                --end;
                while (name[end] == ',')
                    --end;

                if (name[end] != '[')
                    break;

                lastEnd = end;
            }

            if (lastEnd == name.Length)
                return type;

            var className = name.Substring(0, lastEnd);
            return new NrbfMemberType(className, type.Library, type.IsFrozen);
        }
    }
}