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
    internal static class NrbfValueDefaulting
    {
        public static NrbfNode GetDefaultValue(NrbfMemberType type)
        {
            if (ValueDefaults.TryGetValue(type, out var value))
                return value;
            return null;
        }

        private static ReadOnlyDictionary<NrbfMemberType, NrbfValue> ValueDefaults { get; } =
            new ReadOnlyDictionary<NrbfMemberType, NrbfValue>(
                new Dictionary<NrbfMemberType, NrbfValue>()
                {
                    { NrbfMemberType.Boolean, new NrbfValue(false) },
                    { NrbfMemberType.Byte, new NrbfValue((byte)0) },
                    { NrbfMemberType.Char, new NrbfValue('\0') },
                    { NrbfMemberType.Decimal, new NrbfValue(0m) },
                    { NrbfMemberType.Double, new NrbfValue(0.0) },
                    { NrbfMemberType.Int16, new NrbfValue((short)0) },
                    { NrbfMemberType.Int32, new NrbfValue(0) },
                    { NrbfMemberType.Int64, new NrbfValue(0L) },
                    { NrbfMemberType.SByte, new NrbfValue((sbyte)0) },
                    { NrbfMemberType.Single, new NrbfValue(0.0f) },
                    { NrbfMemberType.TimeSpan, new NrbfValue(TimeSpan.Zero) },
                    { NrbfMemberType.DateTime, new NrbfValue(default(DateTime)) },
                    { NrbfMemberType.UInt16, new NrbfValue((ushort)0) },
                    { NrbfMemberType.UInt32, new NrbfValue(0U) },
                    { NrbfMemberType.UInt64, new NrbfValue(0UL) },
                });




    }
}
