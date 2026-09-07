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

namespace Barnwellsoft.Nrbf.Tests;

public class NrbfClassTests
{
    [Test]
    public async Task GetNonMember()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        await Assert.That(() => nrbfClass["NonMember"])
            .Throws<KeyNotFoundException>();
    }

    [Test]
    public async Task SetNonMember()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        await Assert.That(() => nrbfClass["NonMember"] = 42)
            .Throws<KeyNotFoundException>();
    }

    [Test]
    public async Task GetItemIndexTypeError()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        await Assert.That(() => nrbfClass[TimeSpan.Zero])
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task SetItemIndexTypeError()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        await Assert.That(() => nrbfClass[TimeSpan.Zero] = 42)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task GetItemIndexCountError()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        await Assert.That(() => nrbfClass["X", "Y"])
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task SetItemIndexCountError()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        await Assert.That(() => nrbfClass["X", "Y"] = 42)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task GetItemObjectIndex()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        nrbfClass[ValueTupleMemberName] = 42;
        await Assert.That(nrbfClass[(object)ValueTupleMemberName])
            .IsEqualTo(42);
    }

    [Test]
    public async Task SetItemObjectIndex()
    {
        var nrbfClass = CreateValueTupleClass(NrbfMemberType.Int32);
        nrbfClass[(object)ValueTupleMemberName] = 42;
        await Assert.That(nrbfClass[ValueTupleMemberName])
            .IsEqualTo(42);
    }

    [Test]
    public async Task BooleanValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Boolean))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(new NrbfValue(false));

    [Test]
    public async Task ByteValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Byte))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo((byte)0);

    [Test]
    public async Task CharValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Char))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo('\0');

    [Test]
    public async Task DecimalValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Decimal))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(0m);

    [Test]
    public async Task DoubleValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Double))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(0d);

    [Test]
    public async Task Int16ValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Int16))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo((short)0);

    [Test]
    public async Task Int32ValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Int32))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(0);

    [Test]
    public async Task Int64ValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Int64))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(0L);

    [Test]
    public async Task SByteValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.SByte))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo((sbyte)0);

    [Test]
    public async Task SingleValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.Single))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(0f);

    [Test]
    public async Task TimeSpanValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.TimeSpan))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(TimeSpan.Zero);

    [Test]
    public async Task DateTimeValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.DateTime))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(default(DateTime));

    [Test]
    public async Task UInt16ValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.UInt16))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo((ushort)0);

    [Test]
    public async Task UInt32ValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.UInt32))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(0U);

    [Test]
    public async Task UInt64ValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.UInt64))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(0UL);

    [Test]
    public async Task StringValueTupleDefaultValue() =>
        await Assert.That(CreateValueTupleClass(NrbfMemberType.String))
            .HasProperty(x => x[ValueTupleMemberName])
            .IsNull();


    [Test]
    public async Task BooleanValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Boolean);
        bool value = true;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task ByteValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Byte);
        byte value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task CharValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Char);
        char value = 'Z';
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task DecimalValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Decimal);
        decimal value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task DoubleValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Double);
        double value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task Int16ValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Int16);
        short value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task Int32ValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Int32);
        int value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task Int64ValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Int64);
        long value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task SByteValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.SByte);
        sbyte value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task SingleValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Single);
        float value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task TimeSpanValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.TimeSpan);
        TimeSpan value = TimeSpan.FromMinutes(42);
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task DateTimeValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.DateTime);
        DateTime value = DateTime.ParseExact("2020-04-22T16:22:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task UInt16ValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.UInt16);
        ushort value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task UInt32ValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.UInt32);
        uint value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task UInt64ValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.UInt64);
        ulong value = 42;
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task StringValueTupleOtherValue()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.String);
        string value = "42";
        tuple[ValueTupleMemberName] = value;
        await Assert.That(tuple)
            .HasProperty(x => x[ValueTupleMemberName])
            .IsEqualTo(value);
    }

    [Test]
    public async Task BooleanValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Boolean);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task ByteValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Byte);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task CharValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Char);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task DecimalValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Decimal);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task DoubleValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Double);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task Int16ValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Int16);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task Int32ValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Int32);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task Int64ValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Int64);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task SByteValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.SByte);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task SingleValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.Single);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task TimeSpanValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.TimeSpan);
        var value = 'Z';
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task DateTimeValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.DateTime);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task UInt16ValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.UInt16);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task UInt32ValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.UInt32);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task UInt64ValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.UInt64);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task StringValueTupleInvalidAssignment()
    {
        var tuple = CreateValueTupleClass(NrbfMemberType.String);
        var value = TimeSpan.FromMinutes(42);
        await Assert.That(() => tuple[ValueTupleMemberName] = value)
            .ThrowsNothing() ;
    }

    private static NrbfClass CreateValueTupleClass(NrbfMemberType elementType)
    {
        var type = new NrbfMemberType(
            GetValueTupleClassName(elementType.ClassName),
            NrbfLibrary.SystemLibrary);
        var member = new NrbfClassMember(ValueTupleMemberName, elementType);
        var classInfo = new NrbfClassInfo(type, [member]);
        return new NrbfClass(classInfo);
    }

    private static string GetValueTupleClassName(string typeName) =>
        $"System.ValueTuple`1[[{typeName}, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";

    private static string ValueTupleMemberName => "Item1";
}
