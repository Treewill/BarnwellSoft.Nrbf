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
    /// An NRBF message reader.
    /// </summary>
    public class NrbfMessageReader
    {
        private Stream _stream;

        private readonly List<NrbfHeader> _headers =
            new List<NrbfHeader>();

        private readonly Dictionary<NrbfLibraryId, NrbfLibrary> _libraries =
            new Dictionary<NrbfLibraryId, NrbfLibrary>();

        private readonly Dictionary<NrbfObjectId, NrbfNode> _nodes =
            new Dictionary<NrbfObjectId, NrbfNode>();

        private readonly Dictionary<NrbfLibraryId, List<NrbfMemberType>> _libraryReferences =
            new Dictionary<NrbfLibraryId, List<NrbfMemberType>>();

        private readonly Dictionary<NrbfObjectId, List<NrbfNodeReference>> _nodeReferences =
            new Dictionary<NrbfObjectId, List<NrbfNodeReference>>();

        private int _callDepth;

        private readonly int _maxRecursionDepth;

        private readonly int _maxArrayRank;

        /// <summary>
        /// Creates an NRBF message reader.
        /// </summary>
        /// <param name="options">NRBF message reader options.</param>
        public NrbfMessageReader(
            NrbfMessageReaderOptions options = null)
        {
            options = options ?? new NrbfMessageReaderOptions();
            _maxRecursionDepth = options.MaximumRecursionDepth;
            _maxArrayRank = options.MaximumArrayRank;
        }

        /// <summary>
        /// Deserializes a message from the stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The NRBF message.</returns>
        /// <exception cref="NrbfMessageReaderException">
        /// The stream does not contain a supported NRBF message.
        /// </exception>
        public NrbfMessage ReadMessage(Stream stream)
        {
            _stream = stream;

            try
            {
                ReadMessage();

                ResolveLibraryReferences();
                ResolveNodeReferences();

                if (_headers.Count == 0)
                    throw new NrbfMessageReaderException("Header not found in message.");

                var found = _nodes.TryGetValue(_headers.First().Root, out var node);

                if (!found)
                    throw new NrbfMessageReaderException("Root element not found in message.");

                return new NrbfMessage(node.Unwrap());
            }
            finally
            {
                ClearState();
            }
        }

        private void ClearState()
        {
            _stream = null;
            _headers.Clear();
            _nodeReferences.Clear();
            _nodes.Clear();
            _libraryReferences.Clear();
            _libraries.Clear();
        }

        private void ReadMessage()
        {
            while (true)
            {
                var recordType = ReadRecordType();
                switch (recordType)
                {
                    case NrbfRecordType.SerializedStreamHeader:
                        ReadHeader();
                        break;

                    case NrbfRecordType.ClassWithId:
                        ReadClassWithId();
                        break;

                    case NrbfRecordType.SystemClassWithMembers:
                        ReadSystemClassWithMembers();
                        break;

                    case NrbfRecordType.ClassWithMembers:
                        ReadClassWithMembers();
                        break;

                    case NrbfRecordType.SystemClassWithMembersAndTypes:
                        ReadSystemClassWithMembersAndTypes();
                        break;

                    case NrbfRecordType.ClassWithMembersAndTypes:
                        ReadClassWithMembersAndTypes();
                        break;

                    case NrbfRecordType.BinaryObjectString:
                        ReadBinaryObjectString();
                        break;

                    case NrbfRecordType.BinaryArray:
                        ReadBinaryArray();
                        break;

                    case NrbfRecordType.MemberPrimitiveTyped:
                        throw new NrbfMessageReaderException("Unexpected member record found while reading message.");

                    case NrbfRecordType.MemberReference:
                        throw new NrbfMessageReaderException("Unexpected member record found while reading message.");

                    case NrbfRecordType.ObjectNull:
                        throw new NrbfMessageReaderException("Unexpected null record found while reading message.");

                    case NrbfRecordType.MessageEnd:
                        return;

                    case NrbfRecordType.BinaryLibrary:
                        ReadBinaryLibrary();
                        break;

                    case NrbfRecordType.ObjectNullMultiple256:
                        throw new NrbfMessageReaderException("Unexpected null (multiple) record found while reading message.");

                    case NrbfRecordType.ObjectNullMultiple:
                        throw new NrbfMessageReaderException("Unexpected null (multiple) record found while reading message.");

                    case NrbfRecordType.ArraySinglePrimitive:
                        ReadPrimitiveArray();
                        break;

                    case NrbfRecordType.ArraySingleObject:
                        ReadSingleObjectArray();
                        break;

                    case NrbfRecordType.ArraySingleString:
                        ReadSingleStringArray();
                        break;

                    case NrbfRecordType.MethodCall:
                        throw new NrbfMessageReaderException("Unsupported method call record found while reading message.");

                    case NrbfRecordType.MethodReturn:
                        throw new NrbfMessageReaderException("Unsupported method return record found while reading message.");

                    default:
                        throw new NrbfMessageReaderException("Unknown record type found while reading message.");
                }
            }
        }

        private NrbfRecordType ReadRecordType()
        {
            var byteRead = _stream.ReadByte();
            if (byteRead == -1)
                throw new NrbfMessageReaderException("Unexpected end of stream while reading record type.");

            return (NrbfRecordType)byteRead;
        }

        private NrbfPrimitiveType ReadPrimitiveType()
        {
            var byteRead = _stream.ReadByte();
            if (byteRead == -1)
                throw new NrbfMessageReaderException("Unexpected end of stream while reading primitive type.");

            return (NrbfPrimitiveType)byteRead;
        }

        private NrbfBinaryType ReadBinaryType()
        {
            var byteRead = _stream.ReadByte();
            if (byteRead == -1)
                throw new NrbfMessageReaderException("Unexpected end of stream while reading binary type.");

            return (NrbfBinaryType)byteRead;
        }

        private NrbfBinaryArrayType ReadBinaryArrayType()
        {
            var byteRead = _stream.ReadByte();
            if (byteRead == -1)
                throw new NrbfMessageReaderException("Unexpected end of stream while reading binary array type.");

            return (NrbfBinaryArrayType)byteRead;
        }

        private void ReadHeader()
        {
            var rootId = ReadObjectId();
            var id = ReadInt32();
            var versionMajor = ReadInt32();
            var versionMinor = ReadInt32();
            var header = new NrbfHeader(id, versionMajor, versionMinor, rootId);
            _headers.Add(header);
        }

        private NrbfClass ReadClassWithId()
        {
            var objectId = ReadObjectId();
            var classMetadataId = ReadObjectId();

            var sourceClass = (NrbfClass)_nodes[classMetadataId];
            var nrbfClass = new NrbfClass(sourceClass.ClassInfo);

            _nodes.Add(objectId, nrbfClass);
            ReadClassData(nrbfClass);
            return nrbfClass;
        }

        private NrbfClass ReadSystemClassWithMembers()
        {
            var objectId = ReadObjectId();
            var className = ReadLengthPrefixedString();
            var memberCount = ReadInt32();
            var memberNames = ReadMemberNames(memberCount);
            var members = InferClassMembers(memberNames);

            var type = new NrbfMemberType(className, NrbfLibrary.SystemLibrary);
            var metadata = new NrbfClassInfo(type, members);
            var nrbfClass = new NrbfClass(metadata);
            _nodes.Add(objectId, nrbfClass);

            ReadClassData(nrbfClass);
            return nrbfClass;
        }

        private NrbfClass ReadClassWithMembers()
        {
            var objectId = ReadObjectId();
            var className = ReadLengthPrefixedString();
            var memberCount = ReadInt32();
            var memberNames = ReadMemberNames(memberCount);
            var members = InferClassMembers(memberNames);
            var libraryId = ReadLibraryId();

            var type = new NrbfMemberType(className, UnresolvedLibraryReference, false);
            var metadata = new NrbfClassInfo(type, members);

            AddLibraryReference(libraryId, type);

            var nrbfClass = new NrbfClass(metadata);
            _nodes.Add(objectId, nrbfClass);

            ReadClassData(nrbfClass);
            return nrbfClass;
        }

        private NrbfClass ReadSystemClassWithMembersAndTypes()
        {
            var objectId = ReadObjectId();
            var className = ReadLengthPrefixedString();
            var memberCount = ReadInt32();
            var memberNames = ReadMemberNames(memberCount);
            var members = ReadMemberTypes(memberNames);

            var type = new NrbfMemberType(className, NrbfLibrary.SystemLibrary);
            var metadata = new NrbfClassInfo(type, members);
            var nrbfClass = new NrbfClass(metadata);
            _nodes.Add(objectId, nrbfClass);

            ReadClassData(nrbfClass);
            return nrbfClass;
        }

        private NrbfClass ReadClassWithMembersAndTypes()
        {
            var objectId = ReadObjectId();
            var className = ReadLengthPrefixedString();
            var memberCount = ReadInt32();
            var memberNames = ReadMemberNames(memberCount);
            var members = ReadMemberTypes(memberNames);
            var libraryId = ReadLibraryId();

            var type = new NrbfMemberType(className, UnresolvedLibraryReference, false);
            var metadata = new NrbfClassInfo(type, members);
            AddLibraryReference(libraryId, type);

            var nrbfClass = new NrbfClass(metadata);
            _nodes.Add(objectId, nrbfClass);

            ReadClassData(nrbfClass);
            return nrbfClass;
        }

        private List<string> ReadMemberNames(int count)
        {
            var memberNames = new List<string>();
            for (var i = 0; i < count; ++i) 
                memberNames.Add(ReadLengthPrefixedString());
            return memberNames;
        }

        private List<NrbfClassMember> ReadMemberTypes(List<string> names)
        {
            var memberCount = names.Count;

            var types = new List<NrbfBinaryType>();
            for (var i = 0; i < memberCount; ++i) 
                types.Add(ReadBinaryType());

            var members = new List<NrbfClassMember>();
            for (var i = 0; i < memberCount; ++i)
            {
                var member = CompleteClassMember(names[i], types[i]);
                members.Add(member);
            }

            return members;
        }

        private NrbfClassMember CompleteClassMember(
            string name,
            NrbfBinaryType binaryType)
        {
            var type = CompleteMemberType(binaryType);
            var serializeTyped =
                binaryType == NrbfBinaryType.SystemClass
                && type.IsPrimitiveBinaryType();

            return new NrbfClassMember(name, type, serializeTyped);
        }

        private NrbfMemberType CompleteMemberType(NrbfBinaryType binaryType)
        {
            switch (binaryType)
            {
                case NrbfBinaryType.Primitive:
                    return ReadPrimitiveType().ToMemberType();

                case NrbfBinaryType.SystemClass:
                    return new NrbfMemberType(
                        ReadLengthPrefixedString(),
                        NrbfLibrary.SystemLibrary);

                case NrbfBinaryType.Class:
                {
                    var className = ReadLengthPrefixedString();
                    var libraryId = ReadLibraryId();
                    var memberType = new NrbfMemberType(className, UnresolvedLibraryReference, false);
                    AddLibraryReference(libraryId, memberType);
                    return memberType;
                }

                case NrbfBinaryType.PrimitiveArray:
                    return ReadPrimitiveType().ToMemberType().GetArray();

                case NrbfBinaryType.String:
                    return NrbfMemberType.String;

                case NrbfBinaryType.Object:
                    return NrbfMemberType.Object;

                case NrbfBinaryType.ObjectArray:
                    return NrbfMemberType.Object.GetArray();

                case NrbfBinaryType.StringArray:
                    return NrbfMemberType.String.GetArray();

                default:
                    throw new ArgumentOutOfRangeException(nameof(binaryType));
            }
        }

        private NrbfValue ReadBinaryObjectString()
        {
            var objectId = ReadObjectId();
            var text = ReadLengthPrefixedString();
            var value = new NrbfValue(text);
            _nodes.Add(objectId, value);
            return value;
        }

        private void ReadBinaryLibrary()
        {
            var libraryId = ReadLibraryId();
            var name = ReadLengthPrefixedString();
            var library = new NrbfLibrary(name);
            _libraries.Add(libraryId, library);
        }

        private NrbfArray ReadPrimitiveArray()
        {
            var objectId = ReadObjectId();
            var length = ReadInt32();
            var type = ReadPrimitiveType();
            var array = new NrbfArray(type.ToMemberType(), new[] { new NrbfArrayDimension(length) });
            ReadPrimitiveArrayData(array);
            _nodes.Add(objectId, array);
            return array;
        }

        private void ReadPrimitiveArrayData(NrbfArray array)
        {
            if (!array.ElementType.IsPrimitiveBinaryType())
                throw new ArgumentException("Array is not primitive", nameof(array));

            switch (array.ElementType.ToPrimitiveType())
            {
                case NrbfPrimitiveType.Boolean:
                    ReadPrimitiveArrayData(array, ReadBooleanNrbfValue);
                    return;

                case NrbfPrimitiveType.Byte:
                    ReadPrimitiveArrayData(array, ReadByteNrbfValue);
                    return;

                case NrbfPrimitiveType.Char:
                    ReadPrimitiveArrayData(array, ReadCharNrbfValue);
                    return;

                case NrbfPrimitiveType.Decimal:
                    ReadPrimitiveArrayData(array, ReadDecimalNrbfValue);
                    return;

                case NrbfPrimitiveType.Double:
                    ReadPrimitiveArrayData(array, ReadDoubleNrbfValue);
                    return;

                case NrbfPrimitiveType.Int16:
                    ReadPrimitiveArrayData(array, ReadInt16NrbfValue);
                    return;

                case NrbfPrimitiveType.Int32:
                    ReadPrimitiveArrayData(array, ReadInt32NrbfValue);
                    return;

                case NrbfPrimitiveType.Int64:
                    ReadPrimitiveArrayData(array, ReadInt64NrbfValue);
                    return;

                case NrbfPrimitiveType.SByte:
                    ReadPrimitiveArrayData(array, ReadSByteNrbfValue);
                    return;

                case NrbfPrimitiveType.Single:
                    ReadPrimitiveArrayData(array, ReadSingleNrbfValue);
                    return;

                case NrbfPrimitiveType.TimeSpan:
                    ReadPrimitiveArrayData(array, ReadTimeSpanNrbfValue);
                    return;

                case NrbfPrimitiveType.DateTime:
                    ReadPrimitiveArrayData(array, ReadDateTimeNrbfValue);
                    return;

                case NrbfPrimitiveType.UInt16:
                    ReadPrimitiveArrayData(array, ReadUInt16NrbfValue);
                    return;

                case NrbfPrimitiveType.UInt32:
                    ReadPrimitiveArrayData(array, ReadUInt32NrbfValue);
                    return;

                case NrbfPrimitiveType.UInt64:
                    ReadPrimitiveArrayData(array, ReadUInt64NrbfValue);
                    return;

                case NrbfPrimitiveType.Null:
                    throw new NrbfMessageReaderException("Unexpected null primitive type while reading primitive array type.");

                case NrbfPrimitiveType.String:
                    throw new NrbfMessageReaderException("Unexpected string primitive type while reading primitive array type.");

                default:
                    throw new NrbfMessageReaderException("Unknown primitive type found while reading primitive array type.");
            }
        }

        private void ReadPrimitiveArrayData(
            NrbfArray array,
            Func<NrbfNode> readValue)
        {
            if (array.Dimensions.Any(dimension => dimension.Length == 0))
                return;

            var index = new NrbfArrayIndexIterator(array.Dimensions);
            while (true)
            {
                array[index.Current] = readValue.Invoke();
                if (!index.Skip(1))
                    break;
            }
        }

        private NrbfArray ReadSingleObjectArray()
        {
            var type = NrbfMemberType.Object;
            return ReadSingleObjectArrayValue(type);
        }

        private NrbfArray ReadSingleObjectArrayValue(NrbfMemberType type)
        {
            var objectId = ReadObjectId();
            var length = ReadInt32();
            var array = new NrbfArray(type, new[] { new NrbfArrayDimension(length) });
            _nodes.Add(objectId, array);
            ReadObjectArrayItems(array);
            return array;
        }

        private void ReadObjectArrayItems(NrbfArray array)
        {
            if (array.Dimensions.Any(dimension => dimension.Length == 0))
                return;

            using (TrackCallDepth())
            {
                var skip = 0;
                var indexes = new NrbfArrayIndexIterator(array.Dimensions);
                while (indexes.Skip(skip))
                {
                    skip = 1;
                    var recordType = ReadRecordType();
                    switch (recordType)
                    {
                        case NrbfRecordType.SerializedStreamHeader:
                            throw new NrbfMessageReaderException(
                                "Unexpected stream header record found while reading array items.");

                        case NrbfRecordType.ClassWithId:
                            array[indexes.Current] = ReadClassWithId();
                            break;

                        case NrbfRecordType.SystemClassWithMembers:
                            array[indexes.Current] = ReadSystemClassWithMembers();
                            break;

                        case NrbfRecordType.ClassWithMembers:
                            array[indexes.Current] = ReadClassWithMembers();
                            break;

                        case NrbfRecordType.SystemClassWithMembersAndTypes:
                            array[indexes.Current] = ReadSystemClassWithMembersAndTypes();
                            break;

                        case NrbfRecordType.ClassWithMembersAndTypes:
                            array[indexes.Current] = ReadClassWithMembersAndTypes();
                            break;

                        case NrbfRecordType.BinaryObjectString:
                            array[indexes.Current] = ReadBinaryObjectString();
                            break;

                        case NrbfRecordType.BinaryArray:
                            array[indexes.Current] = ReadBinaryArray();
                            break;

                        case NrbfRecordType.MemberPrimitiveTyped:
                            array[indexes.Current] = ReadPrimitiveTypedValue();
                            break;

                        case NrbfRecordType.MemberReference:
                            array[indexes.Current] = UnresolvedValueReference;
                            AddNodeReference(ReadObjectId(), array, indexes.Current.ToArray());
                            break;

                        case NrbfRecordType.ObjectNull:
                            array[indexes.Current] = null;
                            break;

                        case NrbfRecordType.MessageEnd:
                            throw new NrbfMessageReaderException("Unexpected message end while reading array items.");

                        case NrbfRecordType.BinaryLibrary:
                            ReadBinaryLibrary();
                            break;

                        case NrbfRecordType.ObjectNullMultiple256:
                            skip = ReadByte();
                            break;

                        case NrbfRecordType.ObjectNullMultiple:
                            skip = ReadInt32();
                            break;

                        case NrbfRecordType.ArraySinglePrimitive:
                            array[indexes.Current] = ReadPrimitiveArray();
                            break;

                        case NrbfRecordType.ArraySingleObject:
                            array[indexes.Current] = ReadSingleObjectArray();
                            break;

                        case NrbfRecordType.ArraySingleString:
                            array[indexes.Current] = ReadSingleStringArray();
                            break;

                        case NrbfRecordType.MethodCall:
                            throw new NrbfMessageReaderException(
                                "Unsupported method call record found while reading array items.");

                        case NrbfRecordType.MethodReturn:
                            throw new NrbfMessageReaderException(
                                "Unsupported method return record found while reading array items.");

                        default:
                            throw new NrbfMessageReaderException(
                                "Unknown record type found while reading array items.");
                    }
                }
            }
        }

        private NrbfArray ReadSingleStringArray()
        {
            var type = NrbfMemberType.String;
            return ReadSingleObjectArrayValue(type);
        }

        private NrbfArray ReadBinaryArray()
        {
            var objectId = ReadObjectId();
            var binaryArrayType = ReadBinaryArrayType();
            var rank = ReadInt32();

            if (rank > _maxArrayRank)
                throw new NrbfMessageReaderException("Nrbf file has array with rank too large.");

            var lengths = new int[rank];
            for(var i = 0; i < rank; ++i)
            {
                var length = ReadInt32();
                lengths[i] = length;
            }

            var offsets = new int[rank];
            if (binaryArrayType == NrbfBinaryArrayType.SingleOffset
                || binaryArrayType == NrbfBinaryArrayType.JaggedOffset
                || binaryArrayType == NrbfBinaryArrayType.RectangularOffset)
            {
                for(var i = 0; i < rank; ++i)
                    offsets[i] = ReadInt32();
            }

            var dimensions = new NrbfArrayDimension[rank];
            for (var i = 0; i < rank; ++i) 
                dimensions[i] = new NrbfArrayDimension(lengths[i], offsets[i]);

            var binaryType = ReadBinaryType();
            var type = CompleteMemberType(binaryType);
            var binaryArray = new NrbfArray(type, dimensions);

            _nodes.Add(objectId, binaryArray);

            if (binaryType == NrbfBinaryType.Primitive)
            {
                ReadPrimitiveArrayData(binaryArray);
            }
            else
            {
                ReadObjectArrayItems(binaryArray);
            }

            return binaryArray;
        }

        private NrbfNode ReadPrimitiveTypedValue()
        {
            var type = ReadPrimitiveType();
            return ReadPrimitiveUnTypedValue(type);
        }

        private NrbfNode ReadPrimitiveUnTypedValue(
            NrbfPrimitiveType type)
        {
            switch (type)
            {
                case NrbfPrimitiveType.Boolean:
                    return ReadBooleanNrbfValue();

                case NrbfPrimitiveType.Byte:
                    return ReadByteNrbfValue();

                case NrbfPrimitiveType.Char:
                    return ReadCharNrbfValue();

                case NrbfPrimitiveType.Decimal:
                    return ReadDecimalNrbfValue();

                case NrbfPrimitiveType.Double:
                    return ReadDoubleNrbfValue();

                case NrbfPrimitiveType.Int16:
                    return ReadInt16NrbfValue();

                case NrbfPrimitiveType.Int32:
                    return ReadInt32NrbfValue();

                case NrbfPrimitiveType.Int64:
                    return ReadInt64NrbfValue();

                case NrbfPrimitiveType.SByte:
                    return ReadSByteNrbfValue();

                case NrbfPrimitiveType.Single:
                    return ReadSingleNrbfValue();

                case NrbfPrimitiveType.TimeSpan:
                    return ReadTimeSpanNrbfValue();

                case NrbfPrimitiveType.DateTime:
                    return ReadDateTimeNrbfValue();

                case NrbfPrimitiveType.UInt16:
                    return ReadUInt16NrbfValue();

                case NrbfPrimitiveType.UInt32:
                    return ReadUInt32NrbfValue();

                case NrbfPrimitiveType.UInt64:
                    return ReadUInt64NrbfValue();

                case NrbfPrimitiveType.Null:
                    throw new NrbfMessageReaderException("Unexpected null primitive type found while reading primitive.");

                case NrbfPrimitiveType.String:
                    throw new NrbfMessageReaderException("Unexpected string primitive type found while reading primitive.");

                default:
                    throw new NrbfMessageReaderException("Unknown primitive type found while reading primitive.");
            }
        }

        private void ReadClassData(NrbfClass nrbfClass)
        {
            foreach (var member in nrbfClass.ClassInfo.Members)
                nrbfClass[member.Name] = ReadClassDatum(nrbfClass, member);
        }

        private NrbfNode ReadClassDatum(
            NrbfClass nrbfClass,
            NrbfClassMember member)
        {
            using (TrackCallDepth())
            {
                NrbfMemberType type = member.Type;

                if (!member.SerializeTyped && type.IsPrimitiveBinaryType())
                    return ReadPrimitiveUnTypedValue(type.ToPrimitiveType());

                var recordType = ReadRecordType();
                while (recordType == NrbfRecordType.BinaryLibrary)
                {
                    ReadBinaryLibrary();
                    recordType = ReadRecordType();
                }

                switch (recordType)
                {
                    case NrbfRecordType.SerializedStreamHeader:
                        throw new NrbfMessageReaderException(
                            "Unexpected header record found while reading class member.");

                    case NrbfRecordType.ClassWithId:
                        return ReadClassWithId();

                    case NrbfRecordType.SystemClassWithMembers:
                        return ReadSystemClassWithMembers();

                    case NrbfRecordType.ClassWithMembers:
                        return ReadClassWithMembers();

                    case NrbfRecordType.SystemClassWithMembersAndTypes:
                        return ReadSystemClassWithMembersAndTypes();

                    case NrbfRecordType.ClassWithMembersAndTypes:
                        return ReadClassWithMembersAndTypes();

                    case NrbfRecordType.BinaryObjectString:
                        return ReadBinaryObjectString();

                    case NrbfRecordType.BinaryArray:
                        return ReadBinaryArray();

                    case NrbfRecordType.MemberPrimitiveTyped:
                        return ReadPrimitiveTypedValue();

                    case NrbfRecordType.MemberReference:
                        AddNodeReference(ReadObjectId(), nrbfClass, member.Name);
                        return UnresolvedValueReference;

                    case NrbfRecordType.ObjectNull:
                        return null;

                    case NrbfRecordType.MessageEnd:
                        throw new NrbfMessageReaderException(
                            "Unexpected message end record found while reading class member.");

                    case NrbfRecordType.BinaryLibrary:
                        throw new InvalidOperationException(
                            "Unexpected binary library record found while reading class member.");

                    case NrbfRecordType.ObjectNullMultiple256:
                        throw new NrbfMessageReaderException(
                            "Unexpected null (multiple) record found while reading class member.");

                    case NrbfRecordType.ObjectNullMultiple:
                        throw new NrbfMessageReaderException(
                            "Unexpected null (multiple) record found while reading class member.");

                    case NrbfRecordType.ArraySinglePrimitive:
                        return ReadPrimitiveArray();

                    case NrbfRecordType.ArraySingleObject:
                        return ReadSingleObjectArray();

                    case NrbfRecordType.ArraySingleString:
                        return ReadSingleStringArray();

                    case NrbfRecordType.MethodCall:
                        throw new NrbfMessageReaderException(
                            "Unsupported method call record found while reading class member.");

                    case NrbfRecordType.MethodReturn:
                        throw new NrbfMessageReaderException(
                            "Unsupported method return record found while reading class member.");

                    default:
                        throw new NrbfMessageReaderException("Unknown record type found while reading class member.");
                }
            }
        }

        private bool ReadBoolean()
        {
            var byteRead = _stream.ReadByte();
            if (byteRead == -1)
                throw new NrbfMessageReaderException("Unexpected end of stream while reading Boolean value.");

            return byteRead != 0;
        }

        private byte ReadByte()
        {
            var byteRead = _stream.ReadByte();
            if (byteRead == -1)
                throw new NrbfMessageReaderException("Unexpected end of stream while reading byte.");

            return (byte)byteRead;
        }

        private char ReadChar()
        {
            var characters = new char[1];
            var bytes = new byte[4];

            bytes[0] = ReadByte();
            if ((bytes[0] & 0x80) == 0)
            {
                Encoding.UTF8.GetChars(bytes, 0, 1, characters, 0);
                return characters[0];
            }

            bytes[1] = ReadByte();
            if ((bytes[0] & 0x20) == 0)
            {
                Encoding.UTF8.GetChars(bytes, 0, 2, characters, 0);
                return characters[0];
            }

            bytes[2] = ReadByte();
            if ((bytes[0] & 0x10) == 0)
            {
                Encoding.UTF8.GetChars(bytes, 0, 3, characters, 0);
                return characters[0];
            }

            bytes[3] = ReadByte();
            Encoding.UTF8.GetChars(bytes, 0, 4, characters, 0);
            return characters[0];
        }

        private decimal ReadDecimal()
        {
            var decimalString = ReadLengthPrefixedString();
            return decimal.Parse(decimalString, CultureInfo.InvariantCulture);
        }

        private double ReadDouble()
        {
            var bytes = new byte[8];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 8 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading Double value.");

                total += read;
                if (total == 8)
                    break;
            }

            return BitConverter.ToDouble(bytes, 0);
        }

        private short ReadInt16()
        {
            var bytes = new byte[2];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 2 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading Int16 value.");

                total += read;
                if (total == 2)
                    break;
            }

            return (short)(
                (bytes[0] << 0x00)
                | (bytes[1] << 0x08));
        }

        private int ReadInt32()
        {
            var bytes = new byte[4];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 4 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading Int32 value.");

                total += read;
                if (total == 4)
                    break;
            }

            return
                (bytes[0] << 0x00)
                | (bytes[1] << 0x08)
                | (bytes[2] << 0x10)
                | (bytes[3] << 0x18);
        }

        private long ReadInt64()
        {
            var bytes = new byte[8];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 8 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading Int64 value.");

                total += read;
                if (total == 8)
                    break;
            }

            return
                ((long)bytes[0] << 0x00)
                | ((long)bytes[1] << 0x08)
                | ((long)bytes[2] << 0x10)
                | ((long)bytes[3] << 0x18)
                | ((long)bytes[4] << 0x20)
                | ((long)bytes[5] << 0x28)
                | ((long)bytes[6] << 0x30)
                | ((long)bytes[7] << 0x38);
        }

        private sbyte ReadSByte()
        {
            var byteRead = _stream.ReadByte();
            if (byteRead == -1)
                throw new NrbfMessageReaderException("Unexpected end of stream while reading SByte value.");

            if (byteRead >= 0x0080)
                return (sbyte)(byteRead - 0x0100);

            return (sbyte)byteRead;
        }

        private float ReadSingle()
        {
            var bytes = new byte[4];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 4 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading Single value.");

                total += read;
                if (total == 4)
                    break;
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        private TimeSpan ReadTimeSpan()
        {
            var timeSpanString = ReadInt64();
            return TimeSpan.FromTicks(timeSpanString);
        }

        private DateTime ReadDateTime()
        {
            var dateTimeUInt64 = ReadUInt64();
            var ticks = (long)(dateTimeUInt64 & 0x3FFFFFFFFFFFFFFFUL);
            var kind = (DateTimeKind)(dateTimeUInt64 >> 62);
            return new DateTime(ticks, kind);
        }

        private ushort ReadUInt16()
        {
            var bytes = new byte[2];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 2 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading UInt16 value.");

                total += read;
                if (total == 2)
                    break;
            }

            return (ushort)(
                (bytes[0] << 0x00)
                | (bytes[1] << 0x08));
        }

        private uint ReadUInt32()
        {
            var bytes = new byte[4];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 4 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading UInt32 value.");

                total += read;
                if (total == 4)
                    break;
            }

            return
                ((uint)bytes[0] << 0x00)
                | ((uint)bytes[1] << 0x08)
                | ((uint)bytes[2] << 0x10)
                | ((uint)bytes[3] << 0x18);
        }

        private ulong ReadUInt64()
        {
            var bytes = new byte[8];
            var total = 0;
            while (true)
            {
                var read = _stream.Read(bytes, total, 8 - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading UInt64 value.");

                total += read;
                if (total == 8)
                    break;
            }

            return
                ((ulong)bytes[0] << 0x00)
                | ((ulong)bytes[1] << 0x08)
                | ((ulong)bytes[2] << 0x10)
                | ((ulong)bytes[3] << 0x18)
                | ((ulong)bytes[4] << 0x20)
                | ((ulong)bytes[5] << 0x28)
                | ((ulong)bytes[6] << 0x30)
                | ((ulong)bytes[7] << 0x38);
        }

        private string ReadLengthPrefixedString()
        {
            var length = ReadStringLengthPrefix();
            var characters = new byte[length];

            if (length == 0)
                return Encoding.UTF8.GetString(characters, 0, 0);

            var total = 0;
            while (true)
            {
                var read = _stream.Read(characters, total, length - total);
                if (read == 0)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading length prefixed string.");

                total += read;
                if (total == length)
                    return Encoding.UTF8.GetString(characters, 0, length);
            }
        }

        private int ReadStringLengthPrefix()
        {
            var length = 0;
            for (var i = 0; i < 5; ++i)
            {
                var byteRead = _stream.ReadByte();
                if (byteRead == -1)
                    throw new NrbfMessageReaderException("Unexpected end of stream while reading length prefix.");

                if ((byteRead & 0x80) == 0)
                {
                    if (i == 4 && (byteRead & 0xF8) != 0)
                        throw new NrbfMessageReaderException("Length prefix too large.");
                    return length | (byteRead << (7 * i));
                }

                length = length | ((byteRead ^ 0x80) << (7 * i));
            }

            throw new NrbfMessageReaderException("Invalid length prefix for string.");
        }

        private NrbfObjectId ReadObjectId() =>
            new NrbfObjectId(ReadInt32());

        private NrbfLibraryId ReadLibraryId() =>
            new NrbfLibraryId(ReadInt32());

        private NrbfNode ReadBooleanNrbfValue() => ReadBoolean();

        private NrbfNode ReadByteNrbfValue() => ReadByte();

        private NrbfNode ReadCharNrbfValue() => ReadChar();

        private NrbfNode ReadDecimalNrbfValue() => ReadDecimal();

        private NrbfNode ReadDoubleNrbfValue() => ReadDouble();

        private NrbfNode ReadInt16NrbfValue() => ReadInt16();

        private NrbfNode ReadInt32NrbfValue() => ReadInt32();

        private NrbfNode ReadInt64NrbfValue() => ReadInt64();

        private NrbfNode ReadSByteNrbfValue() => ReadSByte();

        private NrbfNode ReadSingleNrbfValue() => ReadSingle();

        private NrbfNode ReadTimeSpanNrbfValue() => ReadTimeSpan();

        private NrbfNode ReadDateTimeNrbfValue() => ReadDateTime();

        private NrbfNode ReadUInt16NrbfValue() => ReadUInt16();

        private NrbfNode ReadUInt32NrbfValue() => ReadUInt32();

        private NrbfNode ReadUInt64NrbfValue() => ReadUInt64();

        private void AddLibraryReference(NrbfLibraryId libraryId, NrbfMemberType memberType)
        {
            if (!_libraryReferences.TryGetValue(libraryId, out var memberTypes))
            {
                memberTypes = new List<NrbfMemberType>();
                _libraryReferences[libraryId] = memberTypes;
            }
            memberTypes.Add(memberType);
        }

        private void ResolveLibraryReferences()
        {
            foreach (var pair in _libraryReferences) 
                ResolveLibraryReferences(pair.Key, pair.Value);
        }

        private void ResolveLibraryReferences(
            NrbfLibraryId id,
            List<NrbfMemberType> references)
        {
            if (!_libraries.TryGetValue(id, out var library))
                throw new NrbfMessageReaderException($"Library not found in stream: {id.Value}");

            foreach (var reference in references)
            {
                reference.Library = library;
                reference.Freeze();
            }
        }

        private void AddNodeReference(
            NrbfObjectId id,
            NrbfNode node,
            object property)
        {
            if (!_nodeReferences.TryGetValue(id, out var references))
            {
                references = new List<NrbfNodeReference>();
                _nodeReferences[id] = references;
            }
            references.Add(new NrbfNodeReference(node, property));
        }

        private void ResolveNodeReferences()
        {
            foreach (var pair in _nodeReferences) 
                ResolveNodeReferences(pair.Key, pair.Value);
        }

        private void ResolveNodeReferences(
            NrbfObjectId id,
            List<NrbfNodeReference> references)
        {
            if (!_nodes.TryGetValue(id, out var node))
                throw new NrbfMessageReaderException($"Object not found in stream: {id.Value}");

            foreach (var reference in references) 
                reference.Node[reference.Property] = node;
        }


        private CallDepthCounter TrackCallDepth()
        {
            if (_callDepth > _maxRecursionDepth)
                throw new NrbfMessageReaderException("Nrbf file is too recursive.");

            _callDepth++;
            return new CallDepthCounter(this);
        }

        private static List<NrbfClassMember> InferClassMembers(List<string> names) =>
            names.Select(InferClassMember).ToList();

        private static NrbfClassMember InferClassMember(string name) =>
            new NrbfClassMember(name, NrbfMemberType.Object);

        private static NrbfValue UnresolvedValueReference { get; } =
            new NrbfValue("NrbfUnresolvedValueReference");

        private static NrbfLibrary UnresolvedLibraryReference { get; } =
            new NrbfLibrary("NrbfUnresolvedLibraryReference");

        private readonly struct CallDepthCounter : IDisposable
        {
            private readonly NrbfMessageReader _reader;
            public CallDepthCounter(NrbfMessageReader reader)
            {
                _reader = reader;
            }

            public void Dispose() => _reader._callDepth--;
        }
    }
}