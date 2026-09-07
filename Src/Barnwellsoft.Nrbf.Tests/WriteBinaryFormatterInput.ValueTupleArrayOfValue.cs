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

using System.Globalization;
using TUnit.Assertions.Enums;

namespace Barnwellsoft.Nrbf.Tests;

public partial class WriteBinaryFormatterInput
{
    [Test]
    public async Task ValueTupleArrayOfBooleanValues()
    {
        var source = new ValueTuple<bool[]>([true, false, true]);
        var elementType = NrbfMemberType.Boolean;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfByteValues()
    {
        var source = new ValueTuple<byte[]>([0xA0, 0x05, 0xFF]);
        var elementType = NrbfMemberType.Byte;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfCharValues()
    {
        var source = new ValueTuple<char[]>(['X', 'y', 'Z']);
        var elementType = NrbfMemberType.Char;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfDecimalValues()
    {
        var source = new ValueTuple<decimal[]>([123.45m, -234.56m, 345.67m]);
        var elementType = NrbfMemberType.Decimal;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfDoubleValues()
    {
        var source = new ValueTuple<double[]>([123.45, -234.56, 345.67]);
        var elementType = NrbfMemberType.Double;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfInt16Values()
    {
        var source = new ValueTuple<short[]>([0x700E, -0x1234, 0x4321]);
        var elementType = NrbfMemberType.Int16;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfInt32Values()
    {
        var source = new ValueTuple<int[]>([0x7F1234FE, -0x12345678, 0x13572468]);
        var elementType = NrbfMemberType.Int32;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfInt64Values()
    {
        var source = new ValueTuple<long[]>([0x1234567812345678, -0x1357246813572468, 0x7FFF12345678FFFE]);
        var elementType = NrbfMemberType.Int64;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfSByteValues()
    {
        var source = new ValueTuple<sbyte[]>([0x7E, -0x10, 0x5A]);
        var elementType = NrbfMemberType.SByte;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfSingleValues()
    {
        var source = new ValueTuple<float[]>([123.45f, -234.56f, 345.67f]);
        var elementType = NrbfMemberType.Single;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfTimeSpanValues()
    {
        var source = new ValueTuple<TimeSpan[]>([
            TimeSpan.FromSeconds(90),
            TimeSpan.FromMinutes(90),
            TimeSpan.FromHours(90),
        ]);
        var elementType = NrbfMemberType.TimeSpan;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfDateTimeValues()
    {
        var source = new ValueTuple<DateTime[]>([
            DateTime.ParseExact("2026-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
            DateTime.ParseExact("2016-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
            DateTime.ParseExact("2006-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
        ]);
        var elementType = NrbfMemberType.DateTime;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfUInt16Values()
    {
        var source = new ValueTuple<ushort[]>([0x8080, 0x1234, 0xFEDC]);
        var elementType = NrbfMemberType.UInt16;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfUInt32Values()
    {
        var source = new ValueTuple<uint[]>([0x80706050, 0x12345678, 0xFEDCBA98]);
        var elementType = NrbfMemberType.UInt32;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfUInt64Values()
    {
        var source = new ValueTuple<ulong[]>([0x8070605040302010, 0x1234567812345678, 0xFEDCBA98FEDCBA98]);
        var elementType = NrbfMemberType.UInt64;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfStringValues()
    {
        var source = new ValueTuple<string[]>(["abc", "def", "xyz"]);
        var elementType = NrbfMemberType.String;
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfObjectValues()
    {
        var source = new ValueTuple<object[]>([new object(), new object(), new object()]);
        var elementType = NrbfMemberType.Object;
        var elementClassInfo = new NrbfClassInfo(elementType, []);
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(new NrbfClass(elementClassInfo));

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ValueTupleArrayOfValueTupleValues()
    {
        var source = new ValueTuple<ValueTuple[]>(new ValueTuple[3]);
        var elementType = new NrbfMemberType("System.ValueTuple", NrbfLibrary.SystemLibrary);
        var elementClassInfo = new NrbfClassInfo(elementType, []);
        var arrayType = elementType.GetArray();
        var array = new NrbfArray(elementType);
        var type = CreateValueTupleType(arrayType.ClassName);
        var classInfo = new NrbfClassInfo(type, [new NrbfClassMember("Item1", arrayType)]);
        var node = new NrbfClass(classInfo)
        {
            ["Item1"] = array,
        };
        foreach (var element in source.Item1) 
            array.Add(new NrbfClass(elementClassInfo));

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    private static NrbfMemberType CreateValueTupleType(string memberType) =>
        new NrbfMemberType(
            $"System.ValueTuple`1[[{memberType}, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
}