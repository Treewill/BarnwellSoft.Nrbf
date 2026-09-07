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
    internal static class NrbfValueConversion
    {

        delegate bool TryValueConversionDelegate(ref NrbfNode node);

        public static bool TryConvertNode(NrbfMemberType type, ref NrbfNode node)
        {
            if (TryConversions.TryGetValue(type, out var conversion))
                return conversion.Invoke(ref node);

            return true;
        }

        private static bool TryBooleanConversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            return value.Type == NrbfNodeType.Boolean;
        }


        private static bool TryByteConversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Byte)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToByte(value.Value));
            return true;
        }

        private static bool TryCharConversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Char)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToChar(value.Value));
            return true;
        }

        private static bool TryDecimalConversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Decimal)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToDecimal(value.Value));
            return true;
        }

        private static bool TryDoubleConversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Double)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToDouble(value.Value));
            return true;
        }

        private static bool TryInt16Conversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Int16)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToInt16(value.Value));
            return true;
        }

        private static bool TryInt32Conversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Int32)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToInt32(value.Value));
            return true;
        }

        private static bool TryInt64Conversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Int64)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToInt64(value.Value));
            return true;
        }

        private static bool TrySByteConversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.SByte)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToSByte(value.Value));
            return true;
        }

        private static bool TrySingleConversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.Single)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToSingle(value.Value));
            return true;
        }

        private static bool TryTimeSpanConversion(ref NrbfNode node) =>
            node is NrbfValue value && value.Type == NrbfNodeType.TimeSpan;

        private static bool TryDateTimeConversion(ref NrbfNode node) =>
            node is NrbfValue value && value.Type == NrbfNodeType.DateTime;

        private static bool TryUInt16Conversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.UInt16)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToUInt16(value.Value));
            return true;
        }

        private static bool TryUInt32Conversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.UInt32)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToUInt32(value.Value));
            return true;
        }

        private static bool TryUInt64Conversion(ref NrbfNode node)
        {
            if (!(node is NrbfValue value))
                return false;

            if (value.Type == NrbfNodeType.UInt64)
                return true;

            if (!value.Type.IsNumeric())
                return false;

            node = new NrbfValue(Convert.ToUInt64(value.Value));
            return true;
        }
        
        private static ReadOnlyDictionary<NrbfMemberType, TryValueConversionDelegate> TryConversions { get; } =
            new ReadOnlyDictionary<NrbfMemberType, TryValueConversionDelegate>(
                new Dictionary<NrbfMemberType, TryValueConversionDelegate>()
                {
                    { NrbfMemberType.Boolean, TryBooleanConversion },
                    { NrbfMemberType.Byte, TryByteConversion },
                    { NrbfMemberType.Char, TryCharConversion },
                    { NrbfMemberType.Decimal, TryDecimalConversion },
                    { NrbfMemberType.Double, TryDoubleConversion },
                    { NrbfMemberType.Int16, TryInt16Conversion },
                    { NrbfMemberType.Int32, TryInt32Conversion },
                    { NrbfMemberType.Int64, TryInt64Conversion },
                    { NrbfMemberType.SByte, TrySByteConversion },
                    { NrbfMemberType.Single, TrySingleConversion },
                    { NrbfMemberType.TimeSpan, TryTimeSpanConversion },
                    { NrbfMemberType.DateTime, TryDateTimeConversion },
                    { NrbfMemberType.UInt16, TryUInt16Conversion },
                    { NrbfMemberType.UInt32, TryUInt32Conversion },
                    { NrbfMemberType.UInt64, TryUInt64Conversion },
                });
    }
}
