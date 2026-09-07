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

namespace Barnwellsoft.Nrbf.Tests;

public class NrbfMemberTypeTests
{
    [Test]
    public async Task NewInvalidName() =>
        await Assert.That(() => new NrbfMemberType(null, NrbfLibrary.SystemLibrary))
            .Throws<ArgumentNullException>();

    [Test]
    public async Task NewInvalidLibrary() =>
        await Assert.That(() => new NrbfMemberType("Test", null))
            .Throws<ArgumentNullException>();

    [Test]
    public async Task FrozenInvalidEdit() =>
        await Assert.That(() => NrbfMemberType.Object.Library = new NrbfLibrary("Test"))
            .Throws<InvalidOperationException>();

    [Test]
    public async Task LibraryInvalidValue()
    {
        var library = new NrbfLibrary("Test");
        var type = new NrbfMemberType("Test", library, false);

        await Assert.That(() => type.Library = null)
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task LibraryEdit()
    {
        var library = new NrbfLibrary("Test");
        var type = new NrbfMemberType("Test", library, false);

        type.Library = NrbfLibrary.SystemLibrary;

        await Assert.That(type)
            .HasProperty(x => x.Library)
            .IsEqualTo(NrbfLibrary.SystemLibrary);
    }

    [Test]
    public async Task LibraryReassign() =>
        await Assert.That(() => NrbfMemberType.Object.Library = NrbfMemberType.String.Library)
            .ThrowsNothing();

    [Test]
    public async Task FreezeAndInvalidEdit()
    {
        var library = new NrbfLibrary("Test");
        var type = new NrbfMemberType("Test", library, false);
        type.Freeze();

        await Assert.That(() => type.Library = NrbfLibrary.SystemLibrary)
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task IsArrayArray()
    {
        var library = new NrbfLibrary("Test");
        var type = new NrbfMemberType("Test[]", library);
        await Assert.That(type.IsArray).IsTrue();
    }

    [Test]
    public async Task IsArrayNonArray()
    {
        var library = new NrbfLibrary("Test");
        var type = new NrbfMemberType("Test", library);
        await Assert.That(type.IsArray).IsFalse();
    }

    [Test]
    public async Task GetArray()
    {
        var library = new NrbfLibrary("Test");
        var type = new NrbfMemberType("Test", library, false);
        var array = type.GetArray();

        await Assert.That(array)
            .Member(a => a.ClassName, x => x.IsEqualTo("Test[]"))
            .And.Member(a => a.Library, x => x.IsEqualTo(library))
            .And.Member(a => a.IsFrozen, x => x.IsFalse())
            .And.Member(a => a.IsArray, x => x.IsTrue());
    }

    [Test]
    public async Task NotEqualsNull()
    {
        NrbfMemberType other = null!;
        await Assert.That(NrbfMemberType.Object != other)
            .IsTrue();
    }

    [Test]
    public async Task EqualsSelf()
    {
        var other = NrbfMemberType.Object;
        await Assert.That(NrbfMemberType.Object == other)
            .IsTrue();
    }

    [Test]
    public async Task EqualsSelfObject()
    {
        object other = NrbfMemberType.Object;
        await Assert.That(NrbfMemberType.Object.Equals(other))
            .IsTrue();
    }

    [Test]
    public async Task EqualsString()
    {
        object other = "System.Object";
        await Assert.That(NrbfMemberType.Object.Equals(other))
            .IsFalse();
    }

    [Test]
    public async Task NotEqualsOther() =>
        await Assert.That(NrbfMemberType.Object != NrbfMemberType.String)
            .IsTrue();

    [Test]
    [Arguments("Type[]")]
    [Arguments("Type[][]")]
    [Arguments("Type[,]")]
    [Arguments("Type[,,]")]
    public async Task IsArray(string typeName) =>
        await Assert.That(new NrbfMemberType(typeName, new NrbfLibrary("Test")))
            .Member(
                x => x.IsArray,
                isArray => isArray.IsTrue());

    [Test]
    [Arguments("Type")]
    [Arguments(
        "Type`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
        DisplayName = "IsArrayNot(Type<int>)")]
    public async Task IsArrayNot(string typeName) =>
        await Assert.That(new NrbfMemberType(typeName, new NrbfLibrary("Test")))
            .Member(
                x => x.IsArray,
                isArray => isArray.IsFalse());

    [Test]
    [Arguments("Test", 0)]
    [Arguments("Test[]", 1)]
    [Arguments("Test[,]", 2)]
    [Arguments("Test[,,]", 3)]
    [Arguments("Test[][]", 1)]
    [Arguments("Test[,][]", 1)]
    [Arguments("Test[][,]", 2)]
    public async Task ArrayRank(string typeName, int rank) =>
        await Assert.That(new NrbfMemberType(typeName, new NrbfLibrary("Test")))
            .Member(
                x => x.ArrayRank,
                arrayRank => arrayRank.IsEqualTo(rank));
}