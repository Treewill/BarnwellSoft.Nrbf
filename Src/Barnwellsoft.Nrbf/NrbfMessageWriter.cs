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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Barnwellsoft.Nrbf.Internal;

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An NRBF message writer.
    /// </summary>
    public class NrbfMessageWriter
    {
        private int _sharedIdCounter = 1;

        private Stream _stream;

        private readonly Dictionary<NrbfClassInfo, NrbfClass> _classObjects =
            new Dictionary<NrbfClassInfo, NrbfClass>();

        private readonly Dictionary<NrbfNode, NrbfObjectId> _objectIds =
            new Dictionary<NrbfNode, NrbfObjectId>(NrbfReferenceEqualityComparer.Instance);

        private readonly Dictionary<NrbfLibrary, NrbfLibraryId> _libraryIds =
            new Dictionary<NrbfLibrary, NrbfLibraryId>(NrbfReferenceEqualityComparer.Instance);

        private readonly Queue<NrbfNode> _pendingNodes =
            new Queue<NrbfNode>();

        /// <summary>
        /// Creates an NRBF message writer.
        /// </summary>
        public NrbfMessageWriter()
        {

        }

        /// <summary>
        /// Writes an NRBF message to the stream.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="node">The node.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="node"/> is <see langword="null"/>.
        /// </exception>
        public void WriteMessage(Stream stream, NrbfNode node)
        {
            if (node is null)
                throw new ArgumentNullException(nameof(node));

            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            _stream = stream;

            try
            {
                var root = node.Wrap();
                GetObjectId(root);
                WriteSerializationHeader(root);

                while (_pendingNodes.Count > 0)
                {
                    var item = _pendingNodes.Dequeue();
                    switch (item.Type)
                    {
                        case NrbfNodeType.String:
                            WriteString((NrbfValue)item);
                            break;

                        case NrbfNodeType.Class:
                            WriteClass((NrbfClass)item);
                            break;

                        case NrbfNodeType.Array:
                            WriteArray((NrbfArray)item);
                            break;

                        case NrbfNodeType.Invalid:
                        case NrbfNodeType.Boolean:
                        case NrbfNodeType.Byte:
                        case NrbfNodeType.Char:
                        case NrbfNodeType.Decimal:
                        case NrbfNodeType.Double:
                        case NrbfNodeType.Int16:
                        case NrbfNodeType.Int32:
                        case NrbfNodeType.Int64:
                        case NrbfNodeType.SByte:
                        case NrbfNodeType.Single:
                        case NrbfNodeType.TimeSpan:
                        case NrbfNodeType.DateTime:
                        case NrbfNodeType.UInt16:
                        case NrbfNodeType.UInt32:
                        case NrbfNodeType.UInt64:
                        default:
                            throw new InvalidOperationException("Invalid serialization node.");
                    }
                }

                WriteRecordType(NrbfRecordType.MessageEnd);
            }
            finally
            {
                ClearState();
            }
        }

        private void ClearState()
        {
            _stream = null;
            _sharedIdCounter = 1;
            _classObjects.Clear();
            _objectIds.Clear();
            _libraryIds.Clear();
            _pendingNodes.Clear();
        }

        private void WriteMember(NrbfClassMember member, NrbfNode node)
        {
            NrbfBinaryType binaryType = member.GetBinaryType();
            switch (binaryType)
            {
                case NrbfBinaryType.Primitive:
                    WritePrimitiveUnTyped(member.Type.ToPrimitiveType(), (NrbfValue)node);
                    return;

                case NrbfBinaryType.String:
                case NrbfBinaryType.Object:
                case NrbfBinaryType.SystemClass:
                case NrbfBinaryType.Class:
                case NrbfBinaryType.ObjectArray:
                case NrbfBinaryType.StringArray:
                case NrbfBinaryType.PrimitiveArray:
                {
                    if (node is null)
                    {
                        WriteNulls(1);
                    }
                    else if (node is NrbfValue nrbfValue
                             && nrbfValue.Type != NrbfNodeType.String)
                    {
                        WriteMemberPrimitiveTyped(nrbfValue);
                    }
                    else
                    {
                        WriteMemberReference(GetObjectId(node));
                    }
                    return;
                }
                default:
                    throw new InvalidOperationException();
            }
        }

        private void WriteClassHeader(NrbfClass nrbfClass)
        {
            var info = nrbfClass.ClassInfo;
            var metaObject = _classObjects[info];

            if (!ReferenceEquals(metaObject, nrbfClass))
            {
                WriteRecordType(NrbfRecordType.ClassWithId);
                WriteObjectId(_objectIds[nrbfClass]);
                WriteObjectId(_objectIds[metaObject]);
                return;
            }

            EnsureLibraryWritten(info.MemberType);

            var members = info.Members;
            foreach (var member in members)
            {
                var memberType = member.Type;
                EnsureLibraryWritten(memberType);
            }

            var recordType = NrbfRecordType.ClassWithMembersAndTypes;
            if (info.Library.IsSystemLibrary) 
                recordType = NrbfRecordType.SystemClassWithMembersAndTypes;

            WriteRecordType(recordType);
            WriteObjectId(_objectIds[metaObject]);
            WriteLengthPrefixedString(info.ClassName);
            WriteInt32(members.Count);

            foreach (var member in members) 
                WriteLengthPrefixedString(member.Name);

            foreach (var member in members)
                WriteBinaryType(member.GetBinaryType());

            foreach (var member in members) 
                WriteAdditionalInfo(member);

            if (recordType == NrbfRecordType.ClassWithMembersAndTypes)
                WriteLibraryId(_libraryIds[info.Library]);
        }

        private void WriteAdditionalInfo(NrbfClassMember member)
        {
            switch (member.GetBinaryType())
            {
                case NrbfBinaryType.Primitive:
                case NrbfBinaryType.PrimitiveArray:
                    WritePrimitiveType(member.Type.GetUltimateArrayElementType().ToPrimitiveType());
                    return;

                case NrbfBinaryType.SystemClass:
                    WriteLengthPrefixedString(member.Type.ClassName);
                    return;

                case NrbfBinaryType.Class:
                    WriteLengthPrefixedString(member.Type.ClassName);
                    WriteLibraryId(_libraryIds[member.Type.Library]);
                    return;

                case NrbfBinaryType.String:
                case NrbfBinaryType.Object:
                case NrbfBinaryType.ObjectArray:
                case NrbfBinaryType.StringArray:
                    return;

                default:
                    throw new ArgumentException("Invalid class member binary type.");
            }
        }

        private void EnsureLibraryWritten(NrbfMemberType memberType)
        {
            var library = memberType.Library;
            if (library.IsSystemLibrary)
                return;

            if (_libraryIds.ContainsKey(library))
                return;

            var libraryId = GetNextLibraryId();
            _libraryIds.Add(library, libraryId);
            WriteLibrary(library, libraryId);
        }

        private void WriteArray(NrbfArray array)
        {
            if (array.Rank > 1)
            {
                WriteBinaryArray(array);
                return;
            }

            switch (array.ElementType.GetBinaryType())
            {
                case NrbfBinaryType.Primitive:
                    WritePrimitiveArray(array);
                    return;

                case NrbfBinaryType.Object:
                    WriteObjectArray(array);
                    return;

                case NrbfBinaryType.String:
                    WriteStringArray(array);
                    return;

                default:
                    WriteBinaryArray(array);
                    return;
            }
        }

        private void WritePrimitiveArray(NrbfArray array)
        {
            var type = array.ElementType.ToPrimitiveType();
            WriteRecordType(NrbfRecordType.ArraySinglePrimitive);
            WriteObjectId(_objectIds[array]);
            WriteInt32(array.Count);
            WritePrimitiveType(type);

            foreach (var item in array) 
                WritePrimitiveUnTyped(type, (NrbfValue)item);
        }

        private void WriteStringArray(NrbfArray array)
        {
            WriteRecordType(NrbfRecordType.ArraySingleString);
            WriteNullableArray(array);
        }

        private void WriteObjectArray(NrbfArray array)
        {
            WriteRecordType(NrbfRecordType.ArraySingleObject);
            WriteNullableArray(array);
        }

        private void WriteNullableArray(NrbfArray array)
        {
            WriteObjectId(_objectIds[array]);
            WriteInt32(array.Count);
            WriteNullableArrayContents(array);
        }

        private void WriteNullableArrayContents(NrbfArray array)
        {
            var type = new NrbfClassMember("Item", array.ElementType, true);

            foreach (var block in array.EnumerateBlocks())
            {
                foreach (var item in block.Values)
                    WriteMember(type, item);

                if (block.NullCount > 0)
                    WriteNulls(block.NullCount);
            }
        }

        private void WriteBinaryArray(NrbfArray array)
        {
            var elementType = array.ElementType;
            var dimensions = array.Dimensions;
            var arrayType = GetBinaryArrayType(dimensions, elementType);

            EnsureLibraryWritten(elementType);
            WriteRecordType(NrbfRecordType.BinaryArray);
            WriteObjectId(_objectIds[array]);

            WriteBinaryArrayType(arrayType);

            WriteInt32(dimensions.Count);
            foreach (var dimension in dimensions) 
                WriteInt32(dimension.Length);

            if (arrayType == NrbfBinaryArrayType.SingleOffset
                || arrayType == NrbfBinaryArrayType.RectangularOffset
                || arrayType == NrbfBinaryArrayType.JaggedOffset)
            {
                foreach (var dimension in dimensions) 
                    WriteInt32(dimension.Offset);
            }

            WriteBinaryType(elementType.GetBinaryType());
            WriteAdditionalInfo(new NrbfClassMember("Item", elementType));

            if (elementType.GetBinaryType() == NrbfBinaryType.Primitive)
            {
                var type = elementType.ToPrimitiveType();
                foreach (var item in array) 
                    WritePrimitiveUnTyped(type, (NrbfValue)item);
            }
            else
            {
                WriteNullableArrayContents(array);
            }
        }

        private NrbfBinaryArrayType GetBinaryArrayType(
            IReadOnlyList<NrbfArrayDimension> dimensions,
            NrbfMemberType elementType)
        {
            if (dimensions.Count == 1)
            {
                if (elementType.IsArray)
                {
                    if (dimensions[0].Offset == 0)
                        return NrbfBinaryArrayType.Jagged;
                    return NrbfBinaryArrayType.JaggedOffset;
                }

                if (dimensions[0].Offset == 0)
                    return NrbfBinaryArrayType.Single;
                return NrbfBinaryArrayType.SingleOffset;
            }

            if (dimensions.All(dimension => dimension.Offset == 0))
                return NrbfBinaryArrayType.Rectangular;
            return NrbfBinaryArrayType.RectangularOffset;
        }

        private void WriteClass(NrbfClass nrbfClass)
        {
            WriteClassHeader(nrbfClass);
            foreach (var member in nrbfClass.ClassInfo.Members)
            {
                var value = nrbfClass[member.Name];
                WriteMember(member, value);
            }
        }

        private void WritePrimitiveUnTyped(
            NrbfPrimitiveType primitiveType,
            NrbfValue value)
        {
            if (value.Type.ToPrimitiveMemberType().ToPrimitiveType() != primitiveType)
                throw new ArgumentException();

            switch (primitiveType)
            {
                case NrbfPrimitiveType.Boolean:
                    WriteBoolean((bool)value.Value);
                    return;

                case NrbfPrimitiveType.Byte:
                WriteByte((byte)value.Value);
                return;

            case NrbfPrimitiveType.Char:
                WriteChar((char)value.Value);
                return;

            case NrbfPrimitiveType.Decimal:
                WriteDecimal((decimal)value.Value);
                return;

            case NrbfPrimitiveType.Double:
                WriteDouble((double)value.Value);
                return;

            case NrbfPrimitiveType.Int16:
                WriteInt16((short)value.Value);
                return;

            case NrbfPrimitiveType.Int32:
                WriteInt32((int)value.Value);
                return;

            case NrbfPrimitiveType.Int64:
                WriteInt64((long)value.Value);
                return;

            case NrbfPrimitiveType.SByte:
                WriteSByte((sbyte)value.Value);
                return;

            case NrbfPrimitiveType.Single:
                WriteSingle((float)value.Value);
                return;

            case NrbfPrimitiveType.TimeSpan:
                WriteTimeSpan((TimeSpan)value.Value);
                return;

            case NrbfPrimitiveType.DateTime:
                WriteDateTime((DateTime)value.Value);
                return;

            case NrbfPrimitiveType.UInt16:
                WriteUInt16((ushort)value.Value);
                return;

            case NrbfPrimitiveType.UInt32:
                WriteUInt32((uint)value.Value);
                return;

            case NrbfPrimitiveType.UInt64:
                WriteUInt64((ulong)value.Value);
                return;

            case NrbfPrimitiveType.String:
                WriteLengthPrefixedString((string)value.Value);
                return;

            case NrbfPrimitiveType.Null:
            default:
                throw new ArgumentOutOfRangeException(nameof(primitiveType), primitiveType, null);
            }
        }

        private void WriteNulls(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return;

            if (count == 1)
            {
                WriteRecordType(NrbfRecordType.ObjectNull);
            }
            else if (count <= 255)
            {
                WriteRecordType(NrbfRecordType.ObjectNullMultiple256);
                WriteByte((byte)count);
            }
            else
            {
                WriteRecordType(NrbfRecordType.ObjectNullMultiple);
                WriteInt32(count);
            }
        }

        private void WriteString(NrbfValue value)
        {
            var id = GetObjectId(value);
            WriteRecordType(NrbfRecordType.BinaryObjectString);
            WriteObjectId(id);
            WriteLengthPrefixedString((string)value.Value);
        }

        private void WriteMemberPrimitiveTyped(NrbfValue value)
        {
            var type = value.Type.ToPrimitiveMemberType().ToPrimitiveType();
            if (type == NrbfPrimitiveType.Null
                || type == NrbfPrimitiveType.String)
            {
                throw new ArgumentException(nameof(value));
            }

            WriteRecordType(NrbfRecordType.MemberPrimitiveTyped);
            WritePrimitiveType(type);
            WritePrimitiveUnTyped(type, value);
        }

        private void WriteMemberReference(NrbfObjectId id)
        {
            WriteRecordType(NrbfRecordType.MemberReference);
            WriteObjectId(id);
        }

        private void WriteSerializationHeader(NrbfNode wrappedRoot)
        {
            WriteRecordType(NrbfRecordType.SerializedStreamHeader);
            WriteObjectId(_objectIds[wrappedRoot]);
            WriteInt32(-1);
            WriteInt32(1);
            WriteInt32(0);
        }

        private void WriteLibrary(
            NrbfLibrary library,
            NrbfLibraryId libraryId)
        {
            WriteRecordType(NrbfRecordType.BinaryLibrary);
            WriteLibraryId(libraryId);
            WriteLengthPrefixedString(library.Name);
        }

        private void WriteRecordType(NrbfRecordType type) =>
            WriteByte((byte)type);

        private void WriteBinaryType(NrbfBinaryType binaryType) => 
            WriteByte((byte)binaryType);

        private void WritePrimitiveType(NrbfPrimitiveType type) => 
            WriteByte((byte)type);

        private void WriteBinaryArrayType(NrbfBinaryArrayType type) => 
            WriteByte((byte)type);

        private void WriteObjectId(NrbfObjectId id) => 
            WriteInt32(id.Value);

        private void WriteLibraryId(NrbfLibraryId id) => 
            WriteInt32(id.Value);

        private void WriteBoolean(bool value)
        {
            byte b = 0;
            if (value)
                b = 1;

            _stream.WriteByte(b);
        }

        private void WriteByte(byte value) => 
            _stream.WriteByte(value);

        private void WriteChar(char value)
        {
            var chars = new[] {value};
            var bytes = new byte[4];
            var count = Encoding.UTF8.GetBytes(chars, 0, 1, bytes, 0);
            _stream.Write(bytes, 0, count);
        }

        private void WriteDecimal(decimal value)
        {
            var text = value.ToString(CultureInfo.InvariantCulture);
            WriteLengthPrefixedString(text);
        }

        private void WriteDouble(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            _stream.Write(bytes, 0, 8);
        }

        private void WriteInt16(short value)
        {
            var bytes = new byte[2];
            bytes[0] = (byte)((value >> 0x00) & 0xFF);
            bytes[1] = (byte)((value >> 0x08) & 0xFF);
            _stream.Write(bytes, 0, 2);
        }

        private void WriteInt32(int value)
        {
            var bytes = new byte[4];
            bytes[0] = (byte)((value >> 0x00) & 0xFF);
            bytes[1] = (byte)((value >> 0x08) & 0xFF);
            bytes[2] = (byte)((value >> 0x10) & 0xFF);
            bytes[3] = (byte)((value >> 0x18) & 0xFF);
            _stream.Write(bytes, 0, 4);
        }

        private void WriteInt64(long value)
        {
            var bytes = new byte[8];
            bytes[0] = (byte)((value >> 0x00) & 0xFF);
            bytes[1] = (byte)((value >> 0x08) & 0xFF);
            bytes[2] = (byte)((value >> 0x10) & 0xFF);
            bytes[3] = (byte)((value >> 0x18) & 0xFF);
            bytes[4] = (byte)((value >> 0x20) & 0xFF);
            bytes[5] = (byte)((value >> 0x28) & 0xFF);
            bytes[6] = (byte)((value >> 0x30) & 0xFF);
            bytes[7] = (byte)((value >> 0x38) & 0xFF);
            _stream.Write(bytes, 0, 8);
        }

        private void WriteSByte(sbyte value) =>
            _stream.WriteByte((byte)value);

        private void WriteSingle(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            _stream.Write(bytes, 0, 4);
        }

        private void WriteTimeSpan(TimeSpan value) =>
            WriteInt64(value.Ticks);

        private void WriteDateTime(DateTime value)
        {
            var ticks = (ulong)value.Ticks & 0x3FFFFFFFFFFFFFFFUL;
            var kind = (ulong)value.Kind << 62;
            WriteUInt64(ticks | kind);
        }

        private void WriteUInt16(ushort value)
        {
            var bytes = new byte[2];
            bytes[0] = (byte)((value >> 0x00) & 0xFF);
            bytes[1] = (byte)((value >> 0x08) & 0xFF);
            _stream.Write(bytes, 0, 2);
        }

        private void WriteUInt32(uint value)
        {
            var bytes = new byte[4];
            bytes[0] = (byte)((value >> 0x00) & 0xFF);
            bytes[1] = (byte)((value >> 0x08) & 0xFF);
            bytes[2] = (byte)((value >> 0x10) & 0xFF);
            bytes[3] = (byte)((value >> 0x18) & 0xFF);
            _stream.Write(bytes, 0, 4);
        }

        private void WriteUInt64(ulong value)
        {
            var bytes = new byte[8];
            bytes[0] = (byte)((value >> 0x00) & 0xFF);
            bytes[1] = (byte)((value >> 0x08) & 0xFF);
            bytes[2] = (byte)((value >> 0x10) & 0xFF);
            bytes[3] = (byte)((value >> 0x18) & 0xFF);
            bytes[4] = (byte)((value >> 0x20) & 0xFF);
            bytes[5] = (byte)((value >> 0x28) & 0xFF);
            bytes[6] = (byte)((value >> 0x30) & 0xFF);
            bytes[7] = (byte)((value >> 0x38) & 0xFF);
            _stream.Write(bytes, 0, 8);
        }

        private void WriteLengthPrefixedString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            WriteLengthPrefixedStringLengthPrefix(bytes.Length);
            _stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteLengthPrefixedStringLengthPrefix(int length)
        {
            var bytes = new byte[5];
            var count = 0;
            var more = true;
            var rest = length;

            while (more)
            {
                var next = rest >> 7;
                var b = rest & 0x7F;
                more = next > 0;
                if (more) 
                    b |= 0x80;

                bytes[count] = (byte)b;
                ++count;
                rest = next;
            }

            _stream.Write(bytes, 0, count);
        }

        private NrbfObjectId GetObjectId(NrbfNode value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (_objectIds.TryGetValue(value, out var id))
                return id;

            if (value is NrbfClass nrbfClass)
            {
                var info = nrbfClass.ClassInfo;
                if (!_classObjects.ContainsKey(info)) 
                    _classObjects.Add(info, nrbfClass);
            }

            id = GetNextObjectId();
            _objectIds.Add(value, id);
            _pendingNodes.Enqueue(value);
            return id;
        }

        private NrbfObjectId GetNextObjectId() => 
            new NrbfObjectId(NextId());

        private NrbfLibraryId GetNextLibraryId() =>
            new NrbfLibraryId(NextId());

        private int NextId()
        {
            if (_sharedIdCounter == int.MaxValue)
                throw new InvalidOperationException("Too many items.");

            var id = _sharedIdCounter;
            _sharedIdCounter++;
            return id;
        }
    }
}