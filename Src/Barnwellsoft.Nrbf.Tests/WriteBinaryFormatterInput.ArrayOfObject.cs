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
    public async Task ArrayOfObjectWithBooleanValue()
    {
        var source = new object[] { true };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((bool)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithByteValue()
    {
        var source = new object[] { (byte)0x87 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((byte)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithCharValue()
    {
        var source = new object[] { 'X' };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((char)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithDecimalValue()
    {
        var source = new object[] { 1234.5678m };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((decimal)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithDoubleValue()
    {
        var source = new object[] { 1234.5678d };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((double)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithInt16Value()
    {
        var source = new object[] { (short)-0x1234 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((short)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithInt32Value()
    {
        var source = new object[] { -0x12345678 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((int)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithInt64Value()
    {
        var source = new object[] { -0x1234567812345678 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((long)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithSByteValue()
    {
        var source = new object[] { (sbyte)-0x40 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((sbyte)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithSingleValue()
    {
        var source = new object[] { 123.456f };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((float)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithTimeSpanValue()
    {
        var source = new object[] { TimeSpan.FromSeconds(365) };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((TimeSpan)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithDateTimeValue()
    {
        var source = new object[]
        {
            DateTime.ParseExact(
                "2026-05-15T11:27:23.3012265Z",
                "O",
                CultureInfo.InvariantCulture)
        };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((DateTime)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithUInt16Value()
    {
        var source = new object[] { (ushort)0xFE12 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((ushort)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithUInt32Value()
    {
        var source = new object[] { 0xFE12DC34 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((uint)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithUInt64Value()
    {
        var source = new object[] { 0xFE12DC34BA564321 };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((ulong)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithStringValue()
    {
        var source = new object[] { "ABC" };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add((string)element);

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }

    [Test]
    public async Task ArrayOfObjectWithValueTupleValue()
    {
        var source = new object[] { new ValueTuple() };
        var valueTupleClassInfo =
            new NrbfClassInfo(
                new NrbfMemberType("System.ValueTuple", NrbfLibrary.SystemLibrary),
                []);
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source) 
            node.Add(new NrbfClass(valueTupleClassInfo));

        var writer = new NrbfMessageWriter();
        using var memory = new MemoryStream();

        writer.WriteMessage(memory, node);

        await Verify(memory.ToArray(), VerificationSettings);
    }
}