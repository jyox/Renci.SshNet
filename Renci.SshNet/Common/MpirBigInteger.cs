
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mpir.NET;
using MicrosoftBigInteger = System.Numerics.BigInteger;


namespace Renci.SshNet.Common {
	/// <summary>
	/// Represents an arbitrarily large signed integer.
	/// </summary>
	public class BigInteger : IComparable, IFormattable, IComparable<BigInteger>, IEquatable<BigInteger>, IComparable<MicrosoftBigInteger>, IEquatable<MicrosoftBigInteger> {
		private readonly mpz_t _value;


		/// <summary>
		/// Gets number of bits used by the number.
		/// </summary>
		/// <value>
		/// The number of the bit used.
		/// </value>
		public int BitLength => _value.BitLength + (Sign == -1 ? 1 : 0 );

		#region Constractors

		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(int value) {
			_value = new mpz_t(value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(uint value) {
			_value = new mpz_t(value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(long value) {
			_value = new mpz_t(value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(ulong value) {
			_value = new mpz_t(value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(double value) {
			_value = new mpz_t(value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(float value)
			: this((double)value) {
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(IEnumerable<byte> value) {
			_value = new mpz_t(new MicrosoftBigInteger(value as byte[] ?? value.ToArray()).ToString());
		}
		
		/// <summary>
		/// Provide compatibility with MPIR library
		/// </summary>
		/// <param name="value">MPIR Big Integer</param>
		public BigInteger(mpz_t value) : this() {
			_value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BigInteger"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public BigInteger(string value) : this() {
			_value = new mpz_t(value);
		}


		#endregion

		#region Operators

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to a 32-bit signed integer value.
		/// </summary>
		/// <param name="value">The value to convert to a 32-bit signed integer.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator int(BigInteger value) {
			return (int)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to an unsigned 32-bit integer value.
		/// </summary>
		/// <param name="value">The value to convert to an unsigned 32-bit integer.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator uint(BigInteger value) {
			return (uint)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to a 16-bit signed integer value.
		/// </summary>
		/// <param name="value">The value to convert to a 16-bit signed integer.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator short(BigInteger value) {
			return (short)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to an unsigned 16-bit integer value.
		/// </summary>
		/// <param name="value">The value to convert to an unsigned 16-bit integer.</param>
		/// <returns>
		/// An object that contains the value of the value parameter
		/// </returns>
		public static explicit operator ushort(BigInteger value) {
			return (ushort)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to an unsigned byte value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Byte.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator byte(BigInteger value) {
			return (byte)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to a signed 8-bit value.
		/// </summary>
		/// <param name="value">The value to convert to a signed 8-bit value.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator sbyte(BigInteger value) {
			return (sbyte)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to a 64-bit signed integer value.
		/// </summary>
		/// <param name="value">The value to convert to a 64-bit signed integer.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator long(BigInteger value) {
			return (long)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to an unsigned 64-bit integer value.
		/// </summary>
		/// <param name="value">The value to convert to an unsigned 64-bit integer.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator ulong(BigInteger value) {
			return (ulong)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to a <see cref="System.Double"/> value.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="System.Double"/>.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator double(BigInteger value) {
			return (double)value._value;
		}

		/// <summary>
		/// Defines an explicit conversion of a System.Numerics.BigInteger object to a single-precision floating-point value.
		/// </summary>
		/// <param name="value">The value to convert to a single-precision floating-point value.</param>
		/// <returns>
		/// An object that contains the closest possible representation of the value parameter.
		/// </returns>
		public static explicit operator float(BigInteger value) {
			return (float)value._value;
		}


		/// <summary>
		/// Defines an implicit conversion of a signed 32-bit integer to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(int value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an implicit conversion of a 32-bit unsigned integer to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(uint value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an implicit conversion of a signed 16-bit integer to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(short value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an implicit conversion of a 16-bit unsigned integer to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(ushort value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an implicit conversion of an unsigned byte to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(byte value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an implicit conversion of an 8-bit signed integer to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(sbyte value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an implicit conversion of a signed 64-bit integer to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(long value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an implicit conversion of a 64-bit unsigned integer to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static implicit operator BigInteger(ulong value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an explicit conversion of a <see cref="System.Double"/> value to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator BigInteger(double value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Defines an explicit conversion of a <see cref="System.Single"/> object to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to convert to a System.Numerics.BigInteger.</param>
		/// <returns>
		/// An object that contains the value of the value parameter.
		/// </returns>
		public static explicit operator BigInteger(float value) {
			return new BigInteger(value);
		}

		/// <summary>
		/// Adds the values of two specified <see cref="BigInteger"/> objects.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>
		/// The sum of left and right.
		/// </returns>
		public static BigInteger operator +(BigInteger left, BigInteger right) {
			return new BigInteger(left._value + right._value);
		}

		/// <summary>
		/// Subtracts a <see cref="BigInteger"/> value from another <see cref="BigInteger"/> value.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>
		/// The result of subtracting right from left.
		/// </returns>
		public static BigInteger operator -(BigInteger left, BigInteger right) {
			return new BigInteger(left._value - right._value);
		}

		/// <summary>
		/// Multiplies two specified <see cref="BigInteger"/> values.
		/// </summary>
		/// <param name="left">The first value to multiply.</param>
		/// <param name="right">The second value to multiply.</param>
		/// <returns>
		/// The product of left and right.
		/// </returns>
		public static BigInteger operator *(BigInteger left, BigInteger right) {
			return new BigInteger(left._value * right._value);
		}

		/// <summary>
		/// Divides a specified <see cref="BigInteger"/> value by another specified <see cref="BigInteger"/> value by using integer division.
		/// </summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>
		/// The integral result of the division.
		/// </returns>
		public static BigInteger operator /(BigInteger dividend, BigInteger divisor) {
			return new BigInteger(dividend._value / divisor._value);
		}

		/// <summary>
		/// Returns the remainder that results from division with two specified <see cref="BigInteger"/> values.
		/// </summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>
		/// The remainder that results from the division.
		/// </returns>
		public static BigInteger operator %(BigInteger dividend, BigInteger divisor) {
			return new BigInteger(dividend._value % divisor._value);
		}

		/// <summary>
		/// Negates a specified BigInteger value.
		/// </summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>
		/// The result of the value parameter multiplied by negative one (-1).
		/// </returns>
		public static BigInteger operator -(BigInteger value) {
			return new BigInteger(-value._value);
		}

		/// <summary>
		/// Returns the value of the <see cref="BigInteger"/> operand. (The sign of the operand is unchanged.)
		/// </summary>
		/// <param name="value">An integer value.</param>
		/// <returns>
		/// The value of the value operand.
		/// </returns>
		public static BigInteger operator +(BigInteger value) {
			return value;
		}

		/// <summary>
		/// Increments a <see cref="BigInteger"/> value by 1.
		/// </summary>
		/// <param name="value">The value to increment.</param>
		/// <returns>
		/// The value of the value parameter incremented by 1.
		/// </returns>
		public static BigInteger operator ++(BigInteger value) {
			return new BigInteger(value._value + 1);
		}

		/// <summary>
		/// Decrements a <see cref="BigInteger"/> value by 1.
		/// </summary>
		/// <param name="value">The value to decrement.</param>
		/// <returns>
		/// The value of the value parameter decremented by 1.
		/// </returns>
		public static BigInteger operator --(BigInteger value) {
			return new BigInteger(value._value - 1);
		}

		/// <summary>
		/// Performs a bitwise And operation on two <see cref="BigInteger"/> values.
		/// </summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>
		/// The result of the bitwise And operation.
		/// </returns>
		public static BigInteger operator &(BigInteger left, BigInteger right) {
			return new BigInteger(left._value & right._value);
		}

		/// <summary>
		/// Performs a bitwise Or operation on two <see cref="BigInteger"/> values.
		/// </summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>
		/// The result of the bitwise Or operation.
		/// </returns>
		public static BigInteger operator |(BigInteger left, BigInteger right) {
			return new BigInteger(left._value | right._value);
		}

		/// <summary>
		/// Performs a bitwise exclusive Or (XOr) operation on two <see cref="BigInteger"/> values.
		/// </summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>
		/// The result of the bitwise Or operation.
		/// </returns>
		public static BigInteger operator ^(BigInteger left, BigInteger right) {
			return new BigInteger(left._value ^ right._value);
		}

		/// <summary>
		/// Returns the bitwise one's complement of a <see cref="BigInteger"/> value.
		/// </summary>
		/// <param name="value">An integer value.</param>
		/// <returns>
		/// The bitwise one's complement of value.
		/// </returns>
		public static BigInteger operator ~(BigInteger value) {
			return new BigInteger(~value._value);
		}

		/// <summary>
		/// Shifts a <see cref="BigInteger"/> value a specified number of bits to the left.
		/// </summary>
		/// <param name="value">The value whose bits are to be shifted.</param>
		/// <param name="shift">The number of bits to shift value to the left.</param>
		/// <returns>
		/// A value that has been shifted to the left by the specified number of bits.
		/// </returns>
		public static BigInteger operator <<(BigInteger value, int shift) {
			return new BigInteger(value._value << shift);
		}

		/// <summary>
		/// Shifts a System.Numerics.BigInteger value a specified number of bits to the right.
		/// </summary>
		/// <param name="value">The value whose bits are to be shifted.</param>
		/// <param name="shift">The number of bits to shift value to the right.</param>
		/// <returns>
		/// A value that has been shifted to the right by the specified number of bits.
		/// </returns>
		public static BigInteger operator >>(BigInteger value, int shift) {
			return new BigInteger(value._value >> shift);
		}

		/// <summary>
		/// Returns a value that indicates whether a <see cref="BigInteger"/> value is less than another <see cref="BigInteger"/> value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than right; otherwise, false.
		/// </returns>
		public static bool operator <(BigInteger left, BigInteger right) {
			return left._value < right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a <see cref="BigInteger"/> value is less than a 64-bit signed integer.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than right; otherwise, false.
		/// </returns>
		public static bool operator <(BigInteger left, long right) {
			return left._value < right;
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit signed integer is less than a <see cref="BigInteger"/> value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than right; otherwise, false.
		/// </returns>
		public static bool operator <(long left, BigInteger right) {
			return left < right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a <see cref="BigInteger"/> value is less than a 64-bit unsigned integer.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than right; otherwise, false.
		/// </returns>
		public static bool operator <(BigInteger left, ulong right) {
			return left._value < right;
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit unsigned integer is less than a <see cref="BigInteger"/> value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than right; otherwise, false.
		/// </returns>
		public static bool operator <(ulong left, BigInteger right) {
			return left < right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is less than or equal to another System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than or equal to right; otherwise, false.
		/// </returns>
		public static bool operator <=(BigInteger left, BigInteger right) {
			return left._value <= right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is less than or equal to a 64-bit signed integer.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than or equal to right; otherwise, false.
		/// </returns>
		public static bool operator <=(BigInteger left, long right) {
			return left._value <= right;
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit signed integer is less than or equal to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than or equal to right; otherwise, false.
		/// </returns>
		public static bool operator <=(long left, BigInteger right) {
			return left <= right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is less than or equal to a 64-bit unsigned integer.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than or equal to right; otherwise, false.
		/// </returns>
		public static bool operator <=(BigInteger left, ulong right) {
			return left._value <= right;
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit unsigned integer is less than or equal to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is less than or equal to right; otherwise, false.
		/// </returns>
		public static bool operator <=(ulong left, BigInteger right) {
			return left <= right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is greater than another System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than right; otherwise, false.
		/// </returns>
		public static bool operator >(BigInteger left, BigInteger right) {
			return left._value > right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger is greater than a 64-bit signed integer value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than right; otherwise, false.
		/// </returns>
		public static bool operator >(BigInteger left, long right) {
			return left._value > right;
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit signed integer is greater than a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than right; otherwise, false.
		/// </returns>
		public static bool operator >(long left, BigInteger right) {
			return left > right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is greater than a 64-bit unsigned integer.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than right; otherwise, false.
		/// </returns>
		public static bool operator >(BigInteger left, ulong right) {
			return left._value > right;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is greater than a 64-bit unsigned integer.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than right; otherwise, false.
		/// </returns>
		public static bool operator >(ulong left, BigInteger right) {
			return left > right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is greater than or equal to another System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than or equal  right; otherwise, false.
		/// </returns>
		public static bool operator >=(BigInteger left, BigInteger right) {
			return left._value >= right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is greater than or equal to a 64-bit signed integer value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than or equal right; otherwise, false.
		/// </returns>
		public static bool operator >=(BigInteger left, long right) {
			return left._value >= right;
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit signed integer is greater than or equal to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than or equal right; otherwise, false.
		/// </returns>
		public static bool operator >=(long left, BigInteger right) {
			return left >= right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value is greater than or equal to a 64-bit unsigned integer value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than or equal right; otherwise, false.
		/// </returns>
		public static bool operator >=(BigInteger left, ulong right) {
			return left._value >= right;
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit unsigned integer is greater than or equal to a System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left is greater than or equal right; otherwise, false.
		/// </returns>
		public static bool operator >=(ulong left, BigInteger right) {
			return left >= right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether the values of two System.Numerics.BigInteger objects are equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if the left and right parameters have the same value; otherwise, false.
		/// </returns>
		public static bool operator ==(BigInteger left, BigInteger right) {
			return left._value == right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value and a signed long integer value are equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if the left and right parameters have the same value; otherwise, false.
		/// </returns>
		public static bool operator ==(BigInteger left, long right) {
			return left._value == right;
		}

		/// <summary>
		/// Returns a value that indicates whether a signed long integer value and a System.Numerics.BigInteger value are equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if the left and right parameters have the same value; otherwise, false.
		/// </returns>
		public static bool operator ==(long left, BigInteger right) {
			return left == right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a System.Numerics.BigInteger value and an unsigned long integer value are equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if the left and right parameters have the same value; otherwise, false.
		/// </returns>
		public static bool operator ==(BigInteger left, ulong right) {
			return left._value == right;
		}

		/// <summary>
		/// Returns a value that indicates whether an unsigned long integer value and a System.Numerics.BigInteger value are equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if the left and right parameters have the same value; otherwise, false.
		/// </returns>
		public static bool operator ==(ulong left, BigInteger right) {
			return left == right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether two <see cref="BigInteger"/> objects have different values.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left and right are not equal; otherwise, false.
		/// </returns>
		public static bool operator !=(BigInteger left, BigInteger right) {
			return left._value != right._value;
		}

		/// <summary>
		/// Returns a value that indicates whether a <see cref="BigInteger"/> value and a 64-bit signed integer are not equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left and right are not equal; otherwise, false.
		/// </returns>
		public static bool operator !=(BigInteger left, long right) {
			return !(left._value == right);
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit signed integer and a <see cref="BigInteger"/> value are not equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left and right are not equal; otherwise, false.
		/// </returns>
		public static bool operator !=(long left, BigInteger right) {
			return !(left == right._value);
		}

		/// <summary>
		/// Returns a value that indicates whether a <see cref="BigInteger"/> value and a 64-bit unsigned integer are not equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left and right are not equal; otherwise, false.
		/// </returns>
		public static bool operator !=(BigInteger left, ulong right) {
			return !(left._value == right);
		}

		/// <summary>
		/// Returns a value that indicates whether a 64-bit unsigned integer and a <see cref="BigInteger"/> value are not equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// true if left and right are not equal; otherwise, false.
		/// </returns>
		public static bool operator !=(ulong left, BigInteger right) {
			return !(left == right._value);
		}

		#endregion

		/// <summary>
		/// Indicates whether the value of the current System.Numerics.BigInteger object is an even number.
		/// </summary>
		/// <value>
		///   <c>true</c> if the value of the System.Numerics.BigInteger object is an even number; otherwise, <c>false</c>.
		/// </value>
		public bool IsEven => mpir.mpz_even_p(_value) != 0;

		/// <summary>
		/// Indicates whether the value of the current System.Numerics.BigInteger object is System.Numerics.BigInteger.One.
		/// </summary>
		/// <value>
		///   <c>true</c> if the value of the System.Numerics.BigInteger object is System.Numerics.BigInteger.One; otherwise, <c>false</c>.
		/// </value>
		public bool IsOne => _value == mpz_t.One;

		/// <summary>
		/// Indicates whether the value of the current System.Numerics.BigInteger object is a power of two.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the value of the System.Numerics.BigInteger object is a power of two; otherwise, <c>false</c>.
		/// </value>
		public bool IsPowerOfTwo => mpir.mpz_popcount(_value.Abs()) == 1;

		/// <summary>
		/// Indicates whether the value of the current System.Numerics.BigInteger object is System.Numerics.BigInteger.Zero.
		/// </summary>
		/// <value>
		///   <c>true</c> if the value of the System.Numerics.BigInteger object is System.Numerics.BigInteger.Zero; otherwise, <c>false</c>.
		/// </value>
		public bool IsZero => Sign == 0;

		/// <summary>
		/// Gets a value that represents the number negative one (-1).
		/// </summary>
		public static readonly BigInteger MinusOne = new BigInteger(mpz_t.NegativeOne);

		/// <summary>
		/// Gets a value that represents the number one (1).
		/// </summary>
		public static readonly BigInteger One = new BigInteger(mpz_t.One);

		/// <summary>
		/// Gets a number that indicates the sign (negative, positive, or zero) of the current System.Numerics.BigInteger object.
		/// </summary>
		public int Sign => mpir.mpz_sgn(_value);

		/// <summary>
		/// Gets a value that represents the number 0 (zero).
		/// </summary>
		public static BigInteger Zero = new BigInteger(mpz_t.Zero);

		private static readonly gmp_randstate_t GmpRandomState = new gmp_randstate_t();

		/// <summary>
		/// Gets the absolute value of a System.Numerics.BigInteger object.
		/// </summary>
		/// <param name="value">A number.</param>
		/// <returns>The absolute value of value.</returns>
		public static BigInteger Abs(BigInteger value) {
			return new BigInteger(value._value.Abs());
		}

		/// <summary>
		/// Adds two System.Numerics.BigInteger values and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static BigInteger Add(BigInteger left, BigInteger right) {
			return left + right;
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj"/> is not the same type as this instance. </exception>
		public int CompareTo(object obj) {
			if (obj == null)
				return 1;
			
			if (!(obj is BigInteger || obj is MicrosoftBigInteger))
				return -1;

			return Compare(this, (BigInteger)obj);
		}

		/// <summary>
		/// Compares this instance to a second System.Numerics.BigInteger and returns 
		/// an integer that indicates whether the value of this instance is less than, 
		/// equal to, or greater than the value of the specified object.
		/// </summary>
		/// <param name="other">The object to compare.</param>
		/// <returns>
		/// A signed integer value that indicates the relationship of this instance to 
		/// other, as shown in the following table.Return valueDescriptionLess than zeroThe 
		/// current instance is less than other.ZeroThe current instance equals other.Greater 
		/// than zeroThe current instance is greater than other.
		/// </returns>
		public int CompareTo(BigInteger other) {
			return Compare(this, other);
		}

		/// <summary>
		/// Compares this instance to an unsigned 64-bit integer and returns an integer 
		/// that indicates whether the value of this instance is less than, equal to, 
		/// or greater than the value of the unsigned 64-bit integer.
		/// </summary>
		/// <param name="other">The unsigned 64-bit integer to compare.</param>
		/// <returns>A signed integer that indicates the relative value of this instance and other, 
		/// as shown in the following table.Return valueDescriptionLess than zeroThe 
		/// current instance is less than other.ZeroThe current instance equals other.Greater
		/// than zeroThe current instance is greater than other.</returns>
		public int CompareTo(ulong other) {
			return _value.CompareTo(other);
		}

		/// <summary>
		/// Generates random BigInteger number
		/// </summary>
		/// <param name="bitLength">Length of random number in bits.</param>
		/// <returns>Big random number.</returns>
		public static BigInteger Random(int bitLength) {
			var value = new mpz_t();
			mpir.mpz_rrandomb(value, GmpRandomState, (ulong)bitLength);
			return new BigInteger(value);
		}

		/// <summary>
		/// Divides one System.Numerics.BigInteger value by another and returns the result.
		/// </summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>The quotient of the division.</returns>
		public static BigInteger Divide(BigInteger dividend, BigInteger divisor) {
			return dividend / divisor;
		}

		/// <summary>
		/// Divides one System.Numerics.BigInteger value by another, returns the result, and returns the remainder in an output parameter.
		/// </summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <param name="remainder">When this method returns, contains a System.Numerics.BigInteger value that 
		/// represents the remainder from the division. This parameter is passed uninitialized.</param>
		/// <returns>The quotient of the division.</returns>
		public static BigInteger DivRem(BigInteger dividend, BigInteger divisor, out BigInteger remainder) {
			var q = new mpz_t();
			var r = new mpz_t();
			mpir.mpz_tdiv_qr(q, r, dividend._value, divisor._value);
			remainder = new BigInteger(r);
			return new BigInteger(q);
		}

		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified System.Numerics.BigInteger object have the same value.
		/// </summary>
		/// <param name="other">The object to compare.</param>
		/// <returns>
		/// true if this System.Numerics.BigInteger object and other have the same value; otherwise, false.
		/// </returns>
		public bool Equals(BigInteger other) {
			return _value.Equals(other._value);
		}

		/// <summary>
		/// Returns a value that indicates whether the current instance and a signed 64-bit integer have the same value.
		/// </summary>
		/// <param name="other">The signed 64-bit integer value to compare.</param>
		/// <returns>true if the signed 64-bit integer and the current instance have the same value; otherwise, false.</returns>
		public bool Equals(long other) {
			return _value.Equals(other);
		}

		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <c>true</c> if the obj parameter is a System.Numerics.BigInteger object or a type
		///   capable of implicit conversion to a System.Numerics.BigInteger value, and
		///   its value is equal to the value of the current System.Numerics.BigInteger
		///   object; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			if (!(obj is BigInteger || obj is MicrosoftBigInteger))
				return false;
			return Equals((BigInteger)obj);
		}

		/// <summary>
		/// Returns a value that indicates whether the current instance and an unsigned 64-bit integer have the same value.
		/// </summary>
		/// <param name="other">The unsigned 64-bit integer to compare.</param>
		/// <returns>true if the current instance and the unsigned 64-bit integer have the same value; otherwise, false.</returns>
		public bool Equals(ulong other) {
			return _value.Equals(other);
		}

		/// <summary>
		/// Returns the hash code for the current System.Numerics.BigInteger object.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer hash code.
		/// </returns>
		public override int GetHashCode() {
			return _value.GetHashCode();
		}

		/// <summary>
		/// Finds the greatest common divisor of two System.Numerics.BigInteger values.
		/// </summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>The greatest common divisor of left and right.</returns>
		public static BigInteger GreatestCommonDivisor(BigInteger left, BigInteger right) {
			var result = new mpz_t();
			mpir.mpz_gcd(result, left._value, right._value);
			return new BigInteger(result);
		}

		/// <summary>
		/// Returns the logarithm of a specified number in a specified base.
		/// </summary>
		/// <param name="value">A number whose logarithm is to be found.</param>
		/// <param name="baseValue">The base of the logarithm.</param>
		/// <returns>The base baseValue logarithm of value, as shown in the table in the Remarks section.</returns>
		public static double Log(BigInteger value, double baseValue) {
			return MicrosoftBigInteger.Log(value, baseValue);
		}

		/// <summary>
		/// Returns the natural (base e) logarithm of a specified number.
		/// </summary>
		/// <param name="value">The number whose logarithm is to be found.</param>
		/// <returns>The natural (base e) logarithm of value, as shown in the table in the Remarks section.</returns>
		public static double Log(BigInteger value) {
			return MicrosoftBigInteger.Log(value);
		}

		/// <summary>
		/// Returns the base 10 logarithm of a specified number.
		/// </summary>
		/// <param name="value">A number whose logarithm is to be found.</param>
		/// <returns>The base 10 logarithm of value, as shown in the table in the Remarks section.</returns>
		public static double Log10(BigInteger value) {
			return MicrosoftBigInteger.Log10(value);
		}

		/// <summary>
		/// Returns the larger of two System.Numerics.BigInteger values.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>The left or right parameter, whichever is larger.</returns>
		public static BigInteger Max(BigInteger left, BigInteger right) {

			return left > right ? left : right;

		}

		/// <summary>
		/// Returns the smaller of two System.Numerics.BigInteger values.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>The left or right parameter, whichever is smaller.</returns>
		public static BigInteger Min(BigInteger left, BigInteger right) {

			return left < right ? left : right;
		}

		/// <summary>
		/// Performs modulus division on a number raised to the power of another number.
		/// </summary>
		/// <param name="value">The number to raise to the exponent power.</param>
		/// <param name="exponent">The exponent to raise value by.</param>
		/// <param name="modulus">The value to divide valueexponent by.</param>
		/// <returns>The remainder after dividing valueexponent by modulus.</returns>
		public static BigInteger ModPow(BigInteger value, BigInteger exponent, BigInteger modulus) {
			return new BigInteger(value._value.PowerMod(exponent._value, modulus._value));
			/*
			if (exponent.Sign == -1)
				throw new ArgumentOutOfRangeException("exponent", "power must be >= 0");
			if (modulus.Sign == 0)
				throw new DivideByZeroException();

			BigInteger result = One % modulus;
			while (exponent.Sign != 0) {
				if (!exponent.IsEven) {
					result = result * value;
					result = result % modulus;
				}

				if (exponent.IsOne)
					break;

				value = value * value;
				value = value % modulus;
				exponent >>= 1;
			}
			return result;
			*/
		}

		/// <summary>
		/// Mods the inverse.
		/// </summary>
		/// <param name="bi">The bi.</param>
		/// <param name="modulus">The modulus.</param>
		/// <returns>Modulus inverted number.</returns>
		public static BigInteger ModInverse(BigInteger bi, BigInteger modulus) {
			return new BigInteger(bi._value.InvertMod(modulus._value));
			/*
			BigInteger a = modulus, b = bi % modulus;
			BigInteger p0 = 0, p1 = 1;

			while (!b.IsZero) {
				if (b.IsOne)
					return p1;

				p0 += a / b * p1;
				a %= b;

				if (a.IsZero)
					break;

				if (a.IsOne)
					return modulus - p0;

				p1 += b / a * p0;
				b %= a;

			}
			return 0;
			*/
		}

		/// <summary>
		/// Returns positive remainder that results from division with two specified <see cref="BigInteger"/> values.
		/// </summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>
		/// Positive remainder that results from the division.
		/// </returns>
		public static BigInteger PositiveMod(BigInteger dividend, BigInteger divisor) {
			var result = dividend % divisor;
			if (result < 0)
				result += divisor;

			return result;
		}

		/// <summary>
		/// Returns the product of two System.Numerics.BigInteger values.
		/// </summary>
		/// <param name="left">The first number to multiply.</param>
		/// <param name="right">The second number to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static BigInteger Multiply(BigInteger left, BigInteger right) {
			return left * right;
		}

		/// <summary>
		/// Negates a specified System.Numerics.BigInteger value.
		/// </summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>The result of the value parameter multiplied by negative one (-1).</returns>
		public static BigInteger Negate(BigInteger value) {
			return -value;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its <see cref="BigInteger"/> equivalent.
		/// </summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specify the permitted format of value.</param>
		/// <param name="provider">An object that provides culture-specific formatting information about value.</param>
		/// <returns>Parsed <see cref="BigInteger"/> number</returns>
		public static BigInteger Parse(string value, NumberStyles style, IFormatProvider provider) {
			return MicrosoftBigInteger.Parse(value, style, provider);
		}

		/// <summary>
		/// Converts the string representation of a number in a specified culture-specific format to its System.Numerics.BigInteger equivalent.
		/// </summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="provider">An object that provides culture-specific formatting information about value.</param>
		/// <returns>A value that is equivalent to the number specified in the value parameter.</returns>
		public static BigInteger Parse(string value, IFormatProvider provider) {
			//throw new NotImplementedException();
			return MicrosoftBigInteger.Parse(value,provider);
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style to its System.Numerics.BigInteger equivalent.
		/// </summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specify the permitted format of value.</param>
		/// <returns>A value that is equivalent to the number specified in the value parameter.</returns>
		public static BigInteger Parse(string value, NumberStyles style) {
			//throw new NotImplementedException();
			return MicrosoftBigInteger.Parse(value,style);
		}

		/// <summary>
		/// Raises a System.Numerics.BigInteger value to the power of a specified value.
		/// </summary>
		/// <param name="value">The number to raise to the exponent power.</param>
		/// <param name="exponent">The exponent to raise value by.</param>
		/// <returns>The result of raising value to the exponent power.</returns>
		public static BigInteger Pow(BigInteger value, int exponent) {
			return new BigInteger(value._value.Power(exponent));
			/*
			if (exponent < 0)
				throw new ArgumentOutOfRangeException("exponent", "exp must be >= 0");
			if (exponent == 0)
				return One;
			if (exponent == 1)
				return value;

			BigInteger result = One;
			while (exponent != 0) {
				if ((exponent & 1) != 0)
					result = result * value;
				if (exponent == 1)
					break;

				value = value * value;
				exponent >>= 1;
			}
			return result;
			*/
		}

		/// <summary>
		/// Performs integer division on two System.Numerics.BigInteger values and returns the remainder.
		/// </summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>The remainder after dividing dividend by divisor.</returns>
		public static BigInteger Remainder(BigInteger dividend, BigInteger divisor) {
			return dividend % divisor;
		}

		/// <summary>
		/// Subtracts one System.Numerics.BigInteger value from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left.</returns>
		public static BigInteger Subtract(BigInteger left, BigInteger right) {
			return left - right;
		}

		/// <summary>
		/// Converts a System.Numerics.BigInteger value to a byte array.
		/// </summary>
		/// <returns>The value of the current System.Numerics.BigInteger object converted to an array of bytes.</returns>
		public byte[] ToByteArray() {
			//return _value.ToBigInteger().ToByteArray();
			return ((MicrosoftBigInteger)this).ToByteArray();
		}

		/// <summary>
		/// Converts the numeric value of the current System.Numerics.BigInteger object to its equivalent string representation.
		/// </summary>
		/// <returns>
		/// The string representation of the current System.Numerics.BigInteger value.
		/// </returns>
		public override string ToString() {
			return _value.ToString();
		}

		/// <summary>
		/// Converts the numeric value of the current System.Numerics.BigInteger object 
		/// to its equivalent string representation by using the specified culture-specific 
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>
		/// The string representation of the current System.Numerics.BigInteger value 
		/// in the format specified by the provider parameter.
		/// </returns>
		public string ToString(IFormatProvider provider) {
			return _value.ToBigInteger().ToString(provider);
		}

		/// <summary>
		/// Converts the numeric value of the current System.Numerics.BigInteger object
		/// to its equivalent string representation by using the specified format.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>
		/// The string representation of the current System.Numerics.BigInteger value
		/// in the format specified by the format parameter.
		/// </returns>
		public string ToString(string format) {
			return _value.ToBigInteger().ToString(format);
		}

		/// <summary>
		/// Converts the numeric value of the current System.Numerics.BigInteger object
		/// to its equivalent string representation by using the specified format and
		/// culture-specific format information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>
		/// The string representation of the current System.Numerics.BigInteger value 
		/// as specified by the format and provider parameters.
		/// </returns>
		public string ToString(string format, IFormatProvider provider) {
			return _value?.ToBigInteger().ToString(format, provider) ?? "0";
		}

		/// <summary>
		/// Tries to convert the string representation of a number in a specified style
		/// and culture-specific format to its System.Numerics.BigInteger equivalent,
		/// and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="value">The string representation of a number. The string is interpreted using the style specified by style.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements
		/// that can be present in value. A typical value to specify is System.Globalization.NumberStyles.Integer.</param>
		/// <param name="cultureInfo">An object that supplies culture-specific formatting information about value.</param>
		/// <param name="result">When this method returns, contains the System.Numerics.BigInteger equivalent
		/// to the number that is contained in value, or System.Numerics.BigInteger.Zero
		/// if the conversion failed. The conversion fails if the value parameter is
		/// null or is not in a format that is compliant with style. This parameter is
		/// passed uninitialized.</param>
		/// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string value, NumberStyles style, CultureInfo cultureInfo, out BigInteger result) {
			MicrosoftBigInteger temp;
			if (MicrosoftBigInteger.TryParse(value, style, cultureInfo, out temp)) {
				result = new BigInteger(new mpz_t(temp));
				return true;
			}
			result = new BigInteger();
			return false;
		}

		/// <summary>
		/// Tries to convert the string representation of a number to its System.Numerics.BigInteger
		/// equivalent, and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="value">The string representation of a number.</param>
		/// <param name="result">When this method returns, contains the System.Numerics.BigInteger equivalent
		/// to the number that is contained in value, or zero (0) if the conversion fails.
		/// The conversion fails if the value parameter is null or is not of the correct
		/// format. This parameter is passed uninitialized.</param>
		/// <returns>true if value was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string value, out BigInteger result) {
			MicrosoftBigInteger temp;
			if (!MicrosoftBigInteger.TryParse(value, out temp)) {
				result = new BigInteger();
				return false;
			}
			result = new BigInteger(new mpz_t(temp));
			return true;
		}

		/// <summary>
		/// Compares this instance to a signed 64-bit integer and returns an integer 
		/// that indicates whether the value of this instance is less than, equal to, 
		/// or greater than the value of the signed 64-bit integer.
		/// </summary>
		/// <param name="other">The signed 64-bit integer to compare.</param>
		/// <returns>A signed integer value that indicates the relationship of this instance to 
		/// other, as shown in the following table.Return valueDescriptionLess than zeroThe 
		/// current instance is less than other.ZeroThe current instance equals other.Greater 
		/// than zero.The current instance is greater than other.</returns>
		public int CompareTo(long other) {
			return _value.CompareTo(other);
		}

		/// <summary>
		/// Compares two System.Numerics.BigInteger values and returns an integer that 
		/// indicates whether the first value is less than, equal to, or greater than the second value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>A signed integer that indicates the relative values of left and right, 
		/// as shown in the following table.ValueConditionLess than zeroleft is less than right.Zeroleft 
		/// equals right.Greater than zeroleft is greater than right.</returns>
		public static int Compare(BigInteger left, BigInteger right) {
			return left._value.CompareTo(right._value);
		}

		/*
		private static bool Negative(byte[] v) {
			return (v[7] & 0x80) != 0;
		}

		private static ushort Exponent(byte[] v) {
			return (ushort)(((ushort)(v[7] & 0x7F) << 4) | ((ushort)(v[6] & 0xF0) >> 4));
		}

		private static ulong Mantissa(byte[] v) {
			uint i1 = v[0] | ((uint)v[1] << 8) | ((uint)v[2] << 16) | ((uint)v[3] << 24);
			uint i2 = v[4] | ((uint)v[5] << 8) | ((uint)(v[6] & 0xF) << 16);

			return i1 | ((ulong)i2 << 32);
		}

		/// <summary>
		/// Populations the count.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <returns>Returns the number of bits set in x</returns>
		private static int PopulationCount(uint x) {
			x = x - ((x >> 1) & 0x55555555);
			x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
			x = (x + (x >> 4)) & 0x0F0F0F0F;
			x = x + (x >> 8);
			x = x + (x >> 16);
			return (int)(x & 0x0000003F);
		}

		private string ToStringWithPadding(string format, uint radix, IFormatProvider provider) {
			if (format.Length > 1) {
				int precision = Convert.ToInt32(format.Substring(1), CultureInfo.InvariantCulture.NumberFormat);
				string baseStr = ToString(radix, provider);
				if (baseStr.Length < precision) {
					string additional = new string('0', precision - baseStr.Length);
					if (baseStr[0] != '-') {
						return additional + baseStr;
					}
					return "-" + additional + baseStr.Substring(1);
				}
				return baseStr;
			}
			return ToString(radix, provider);
		}
		
		
		private static uint[] MakeTwoComplement(uint[] v) {
			uint[] res = new uint[v.Length];

			ulong carry = 1;
			for (int i = 0 ; i < v.Length ; ++i) {
				uint word = v[i];
				carry = ~word + carry;
				word = (uint)carry;
				carry = (uint)(carry >> 32);
				res[i] = word;
			}

			uint last = res[res.Length - 1];
			int idx = FirstNonFFByte(last);
			uint mask = 0xFF;
			for (int i = 1 ; i < idx ; ++i)
				mask = (mask << 8) | 0xFF;

			res[res.Length - 1] = last & mask;
			return res;
		}

		private string ToString(uint radix, IFormatProvider provider) {
			const string characterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			if (characterSet.Length < radix)
				throw new ArgumentException("charSet length less than radix", "characterSet");
			if (radix == 1)
				throw new ArgumentException("There is no such thing as radix one notation", "radix");

			if (Sign == 0)
				return "0";
			if (this._data.Length == 1 && this._data[0] == 1)
				return Sign == 1 ? "1" : "-1";

			List<char> digits = new List<char>(1 + this._data.Length * 3 / 10);

			BigInteger a;
			if (Sign == 1)
				a = this;
			else {
				uint[] dt = this._data;
				if (radix > 10)
					dt = MakeTwoComplement(dt);
				a = new BigInteger(1, dt);
			}

			while (a != 0) {
				BigInteger rem;
				a = DivRem(a, radix, out rem);
				digits.Add(characterSet[(int)rem]);
			}

			if (Sign == -1 && radix == 10) {
				NumberFormatInfo info = null;
				if (provider != null)
					info = provider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
				if (info != null) {
					string str = info.NegativeSign;
					for (int i = str.Length - 1 ; i >= 0 ; --i)
						digits.Add(str[i]);
				}
				else {
					digits.Add('-');
				}
			}

			char last = digits[digits.Count - 1];
			if (Sign == 1 && radix > 10 && (last < '0' || last > '9'))
				digits.Add('0');

			digits.Reverse();

			return new string(digits.ToArray());
		}

		private static Exception GetFormatException() {
			return new FormatException("Input string was not in the correct format");
		}

		private static bool ProcessTrailingWhitespace(bool tryParse, string s, int position, ref Exception exc) {
			int len = s.Length;

			for (int i = position ; i < len ; i++) {
				char c = s[i];

				if (c != 0 && !char.IsWhiteSpace(c)) {
					if (!tryParse)
						exc = GetFormatException();
					return false;
				}
			}
			return true;
		}

		private static bool Parse(string s, bool tryParse, NumberStyles style, IFormatProvider provider, out BigInteger result, out Exception exc) {
			int len;
			int i, sign = 1;
			bool digits_seen = false;

			var baseNumber = 10;
			switch (style) {
			case NumberStyles.None:
				break;
			case NumberStyles.HexNumber:
			case NumberStyles.AllowHexSpecifier:
				baseNumber = 16;
				break;
			case NumberStyles.AllowCurrencySymbol:
			case NumberStyles.AllowDecimalPoint:
			case NumberStyles.AllowExponent:
			case NumberStyles.AllowLeadingSign:
			case NumberStyles.AllowLeadingWhite:
			case NumberStyles.AllowParentheses:
			case NumberStyles.AllowThousands:
			case NumberStyles.AllowTrailingSign:
			case NumberStyles.AllowTrailingWhite:
			case NumberStyles.Any:
			case NumberStyles.Currency:
			case NumberStyles.Float:
			case NumberStyles.Integer:
			case NumberStyles.Number:
			default:
				throw new NotSupportedException(string.Format("Style '{0}' is not supported.", style));
			}

			result = Zero;
			exc = null;

			if (s == null) {
				if (!tryParse)
					exc = new ArgumentNullException("value");
				return false;
			}

			len = s.Length;

			char c;
			for (i = 0 ; i < len ; i++) {
				c = s[i];
				if (!char.IsWhiteSpace(c))
					break;
			}

			if (i == len) {
				if (!tryParse)
					exc = GetFormatException();
				return false;
			}

			var info = provider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;

			string negative = info.NegativeSign;
			string positive = info.PositiveSign;

			if (string.CompareOrdinal(s, i, positive, 0, positive.Length) == 0)
				i += positive.Length;
			else if (string.CompareOrdinal(s, i, negative, 0, negative.Length) == 0) {
				sign = -1;
				i += negative.Length;
			}

			BigInteger val = Zero;
			for (; i < len ; i++) {
				c = s[i];

				if (c == '\0') {
					i = len;
					continue;
				}

				if (c >= '0' && c <= '9') {
					byte d = (byte)(c - '0');

					val = val * baseNumber + d;

					digits_seen = true;
				}
				else if (c >= 'A' && c <= 'F') {
					byte d = (byte)(c - 'A' + 10);

					val = val * baseNumber + d;

					digits_seen = true;
				}
				else if (!ProcessTrailingWhitespace(tryParse, s, i, ref exc))
					return false;
			}

			if (!digits_seen) {
				if (!tryParse)
					exc = GetFormatException();
				return false;
			}

			if (val.Sign == 0)
				result = val;
			else if (sign == -1)
				result = new BigInteger(-1, val._data);
			else
				result = new BigInteger(1, val._data);

			return true;
		}

		private int LongCompare(uint low, uint high) {
			uint h = 0;
			if (this._data.Length > 1)
				h = this._data[1];

			if (h > high)
				return 1;
			if (h < high)
				return -1;

			uint l = this._data[0];

			if (l > low)
				return 1;
			if (l < low)
				return -1;

			return 0;
		}

		private bool AsUInt64(out ulong val) {
			val = 0;
			if (this._data.Length > 2 || Sign == -1)
				return false;

			val = this._data[0];
			if (this._data.Length == 1)
				return true;

			uint high = this._data[1];
			val |= (ulong)high << 32;
			return true;
		}

		private bool AsInt32(out int val) {
			val = 0;
			if (this._data.Length > 1)
				return false;
			uint d = this._data[0];

			if (Sign == 1) {
				if (d > int.MaxValue)
					return false;
				val = (int)d;
			}
			else if (Sign == -1) {
				if (d > 0x80000000u)
					return false;
				val = -(int)d;
			}
			return true;
		}

		/// <summary>
		/// Returns the 0-based index of the most significant set bit
		/// </summary>
		/// <param name="word">The word.</param>
		/// <returns>0 if no bit is set</returns>
		private static int BitScanBackward(uint word) {
			for (int i = 31 ; i >= 0 ; --i) {
				uint mask = 1u << i;
				if ((word & mask) == mask)
					return i;
			}
			return 0;
		}

		private static int TopByte(uint x) {
			if ((x & 0xFFFF0000u) != 0) {
				if ((x & 0xFF000000u) != 0)
					return 4;
				return 3;
			}
			if ((x & 0xFF00u) != 0)
				return 2;
			return 1;
		}

		private static int FirstNonFFByte(uint word) {
			if ((word & 0xFF000000u) != 0xFF000000u)
				return 4;
			if ((word & 0xFF0000u) != 0xFF0000u)
				return 3;
			if ((word & 0xFF00u) != 0xFF00u)
				return 2;
			return 1;
		}

		private static byte[] Resize(byte[] v, int len) {
			byte[] res = new byte[len];
			Buffer.BlockCopy(v, 0, res, 0, Math.Min(v.Length, len));
			Array.Copy(v, res, Math.Min(v.Length, len));
			return res;
		}

		private static uint[] Resize(uint[] v, int len) {
			uint[] res = new uint[len];
			Buffer.BlockCopy(v, 0, res, 0, Math.Min(v.Length, len) * sizeof(uint));
			return res;
		}

		private static uint[] CoreAdd(uint[] a, uint[] b) {
			if (a.Length < b.Length) {
				uint[] tmp = a;
				a = b;
				b = tmp;
			}

			int bl = a.Length;
			int sl = b.Length;

			uint[] res = new uint[bl];

			ulong sum = 0;

			int i = 0;
			for (; i < sl ; i++) {
				sum = sum + a[i] + b[i];
				res[i] = (uint)sum;
				sum >>= 32;
			}

			for (; i < bl ; i++) {
				sum = sum + a[i];
				res[i] = (uint)sum;
				sum >>= 32;
			}

			if (sum != 0) {
				res = Resize(res, bl + 1);
				res[i] = (uint)sum;
			}

			return res;
		}

		// invariant a > b
		private static uint[] CoreSub(uint[] a, uint[] b) {
			int bl = a.Length;
			int sl = b.Length;

			uint[] res = new uint[bl];

			ulong borrow = 0;
			int i;
			for (i = 0 ; i < sl ; ++i) {
				borrow = (ulong)a[i] - b[i] - borrow;

				res[i] = (uint)borrow;
				borrow = (borrow >> 32) & 0x1;
			}

			for (; i < bl ; i++) {
				borrow = a[i] - borrow;
				res[i] = (uint)borrow;
				borrow = (borrow >> 32) & 0x1;
			}

			//remove extra zeroes
			for (i = bl - 1 ; i >= 0 && res[i] == 0 ; --i)
				;
			if (i < bl - 1)
				res = Resize(res, i + 1);

			return res;
		}

		private static uint[] CoreAdd(uint[] a, uint b) {
			int len = a.Length;
			uint[] res = new uint[len];

			ulong sum = b;
			int i;
			for (i = 0 ; i < len ; i++) {
				sum = sum + a[i];
				res[i] = (uint)sum;
				sum >>= 32;
			}

			if (sum != 0) {
				res = Resize(res, len + 1);
				res[i] = (uint)sum;
			}

			return res;
		}

		private static uint[] CoreSub(uint[] a, uint b) {
			int len = a.Length;
			uint[] res = new uint[len];

			ulong borrow = b;
			int i;
			for (i = 0 ; i < len ; i++) {
				borrow = a[i] - borrow;
				res[i] = (uint)borrow;
				borrow = (borrow >> 32) & 0x1;
			}

			//remove extra zeroes
			for (i = len - 1 ; i >= 0 && res[i] == 0 ; --i)
				;
			if (i < len - 1)
				res = Resize(res, i + 1);

			return res;
		}

		private static int GetNormalizeShift(uint value) {
			int shift = 0;

			if ((value & 0xFFFF0000) == 0) { value <<= 16; shift += 16; }
			if ((value & 0xFF000000) == 0) { value <<= 8; shift += 8; }
			if ((value & 0xF0000000) == 0) { value <<= 4; shift += 4; }
			if ((value & 0xC0000000) == 0) { value <<= 2; shift += 2; }
			if ((value & 0x80000000) == 0) { value <<= 1; shift += 1; }

			return shift;
		}

		private static void Normalize(uint[] u, int l, uint[] un, int shift) {
			uint carry = 0;
			int i;
			if (shift > 0) {
				int rshift = 32 - shift;
				for (i = 0 ; i < l ; i++) {
					uint ui = u[i];
					un[i] = (ui << shift) | carry;
					carry = ui >> rshift;
				}
			}
			else {
				for (i = 0 ; i < l ; i++) {
					un[i] = u[i];
				}
			}

			while (i < un.Length) {
				un[i++] = 0;
			}

			if (carry != 0) {
				un[l] = carry;
			}
		}

		private static void Unnormalize(uint[] un, out uint[] r, int shift) {
			int length = un.Length;
			r = new uint[length];

			if (shift > 0) {
				int lshift = 32 - shift;
				uint carry = 0;
				for (int i = length - 1 ; i >= 0 ; i--) {
					uint uni = un[i];
					r[i] = (uni >> shift) | carry;
					carry = uni << lshift;
				}
			}
			else {
				for (int i = 0 ; i < length ; i++) {
					r[i] = un[i];
				}
			}
		}

		private static void DivModUnsigned(uint[] u, uint[] v, out uint[] q, out uint[] r) {
			int m = u.Length;
			int n = v.Length;

			if (n <= 1) {
				// Divide by single digit
				//
				ulong rem = 0;
				uint v0 = v[0];
				q = new uint[m];
				r = new uint[1];

				for (int j = m - 1 ; j >= 0 ; j--) {
					rem *= _BASE;
					rem += u[j];

					ulong div = rem / v0;
					rem -= div * v0;
					q[j] = (uint)div;
				}
				r[0] = (uint)rem;
			}
			else if (m >= n) {
				int shift = GetNormalizeShift(v[n - 1]);

				uint[] un = new uint[m + 1];
				uint[] vn = new uint[n];

				Normalize(u, m, un, shift);
				Normalize(v, n, vn, shift);

				q = new uint[m - n + 1];
				r = null;

				// Main division loop
				//
				for (int j = m - n ; j >= 0 ; j--) {
					ulong rr, qq;
					int i;

					rr = _BASE * un[j + n] + un[j + n - 1];
					qq = rr / vn[n - 1];
					rr -= qq * vn[n - 1];

					for (;;) {
						// Estimate too big ?
						//
						if ((qq >= _BASE) || (qq * vn[n - 2] > rr * _BASE + un[j + n - 2])) {
							qq--;
							rr += vn[n - 1];
							if (rr < _BASE)
								continue;
						}
						break;
					}


					// Multiply and subtract
					//
					long b = 0;
					long t = 0;
					for (i = 0 ; i < n ; i++) {
						ulong p = vn[i] * qq;
						t = un[i + j] - (long)(uint)p - b;
						un[i + j] = (uint)t;
						p >>= 32;
						t >>= 32;
						b = (long)p - t;
					}
					t = un[j + n] - b;
					un[j + n] = (uint)t;

					// Store the calculated value
					//
					q[j] = (uint)qq;

					// Add back vn[0..n] to un[j..j+n]
					//
					if (t < 0) {
						q[j]--;
						ulong c = 0;
						for (i = 0 ; i < n ; i++) {
							c = (ulong)vn[i] + un[j + i] + c;
							un[j + i] = (uint)c;
							c >>= 32;
						}
						c += un[j + n];
						un[j + n] = (uint)c;
					}
				}

				Unnormalize(un, out r, shift);
			}
			else {
				q = new uint[] { 0 };
				r = u;
			}
		}
		*/

		/// <summary>
		/// Provides interoperation compatibility between Microsoft BigInteger and this BigInteger class.
		/// </summary>
		/// <param name="value">A Microsoft BigInteger</param>
		public BigInteger(MicrosoftBigInteger value) {
			_value = new mpz_t(value.ToString());
		}

		private BigInteger() {
			// derp
		}

		/// <summary>
		/// Provides interoperation compatibility between Microsoft BigInteger and this BigInteger class.
		/// </summary>
		/// <param name="value">A BigInteger</param>
		/// <returns>A Microsoft BigInteger</returns>
		public static implicit operator MicrosoftBigInteger(BigInteger value) {
			return MicrosoftBigInteger.Parse( value._value.ToString() );
		}
		/// <summary>
		/// Provides interoperation compatibility between Microsoft BigInteger and this BigInteger class.
		/// </summary>
		/// <param name="value">A Microsoft BigInteger</param>
		/// <returns>A BigInteger</returns>
		public static implicit operator BigInteger(MicrosoftBigInteger value) {
			return new BigInteger(value.ToString());
		}
		
		/// <summary>
		/// Provides interoperation compatibility between Microsoft BigInteger and this BigInteger class.
		/// </summary>
		/// <param name="other">A Microsoft BigInteger</param>
		/// <returns>The comparison result</returns>
		public int CompareTo(MicrosoftBigInteger other) {

			var sign = Sign.CompareTo(other.Sign);

			if (sign != 0)
				return sign;

			return CompareTo(new BigInteger(other));
		}
		
		/// <summary>
		/// Provides interoperation compatibility between Microsoft BigInteger and this BigInteger class.
		/// </summary>
		/// <param name="other">A Microsoft BigInteger</param>
		/// <returns>The equality result</returns>
		public bool Equals(MicrosoftBigInteger other) {
			return CompareTo(other) == 0;
		}
	}
}