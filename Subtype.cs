#region The MIT License (MIT)
//
// Copyright (c) 2016 Atif Aziz. All rights reserved.
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

public static class Subtype
{
    public static Subtype<TType, TValue> Create<TType, TValue>(TValue value, TType subtype) =>
        new Subtype<TType, TValue>(value, subtype);
}

public struct Subtype<TType, TValue> : IEquatable<Subtype<TType, TValue>>
{
    public TValue Value { get; }
    public TType Type { get; }

    public Subtype(TValue value, TType type) =>
        (Value, Type) = (value, type);

    public bool Equals(Subtype<TType, TValue> other) =>
        EqualityComparer<TValue>.Default.Equals(Value, other.Value);

    public override bool Equals(object obj) =>
        obj is Subtype<TType, TValue> subtyped && Equals(subtyped);

    public override int GetHashCode() =>
        unchecked((typeof(TType).GetHashCode() * 397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value));

    public static bool operator ==(Subtype<TType, TValue> left, Subtype<TType, TValue> right) => left.Equals(right);
    public static bool operator !=(Subtype<TType, TValue> left, Subtype<TType, TValue> right) => !left.Equals(right);

    public static implicit operator TValue(Subtype<TType, TValue> value) => value.Value;

    public override string ToString() => $"{Value}";
}
