#region The MIT License (MIT)
//
// Copyright (c) 2018 Atif Aziz. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class Box
{
    public static Box<T> Create<T>(T value) where T : struct => new Box<T>(value);
}

[DebuggerDisplay("{" + nameof(_value) + "}")]
partial class Box<T> : IEquatable<Box<T>>, IEquatable<T> where T : struct
{
    readonly T _value;

    public Box(T value) => _value = value;

    public bool Equals(Box<T> other) =>
        !(other is null) && (ReferenceEquals(this, other) || Equals(other._value));

    public bool Equals(T other) =>
        EqualityComparer<T>.Default.Equals(other, _value);

    public override bool Equals(object obj)
        => obj is Box<T> box && Equals(box)
        || obj is T t && Equals(t);

    public override int GetHashCode() => _value.GetHashCode();

    public static bool Equals(Box<T> left, Box<T> right)
        => left is null && right is null
        || left is Box<T> lb && lb.Equals(right)
        || right.Equals(left);

    public override string ToString() => _value.ToString();

    public static bool Equals(Box<T> left, T right)
        => left is Box<T> lb && lb.Equals(right);

    public static bool Equals(Box<T> left, T? right)
        => left is null && right is null
        || left is Box<T> a && right is T b && a.Equals(b);

    public static implicit operator T(Box<T> box) => box._value;
    public static implicit operator Box<T>(T value) => Box.Create(value);

    public static bool operator ==(Box<T> left, Box<T> right) => Equals(left, right);
    public static bool operator !=(Box<T> left, Box<T> right) => !Equals(left, right);

    public static bool operator ==(Box<T> left, T right) => Equals(left, right);
    public static bool operator !=(Box<T> left, T right) => !Equals(left, right);

    public static bool operator ==(Box<T> left, T? right) => Equals(left, right);
    public static bool operator !=(Box<T> left, T? right) => !Equals(left, right);
}
