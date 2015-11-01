// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

/*============================================================
**
**
**
** Purpose: Platform independent integer
**
** 
===========================================================*/

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
    // CONTRACT with Runtime
    // The IntPtr type is one of the primitives understood by the compilers and runtime
    // Data Contract: Single field of type void *

    public struct IntPtr
    {
        unsafe private void* _value; // The compiler treats void* closest to uint hence explicit casts are required to preserve int behavior

        [Intrinsic]
        public static readonly IntPtr Zero;

        [Intrinsic]
        [NonVersionable]
        public unsafe IntPtr(int value)
        {
#if BIT64
            _value = (void*)(long)value;
#else
            _value = (void*)value;
#endif
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe IntPtr(long value)
        {
#if BIT64
            _value = (void*)value;
#else
            _value = (void*)checked((int)value);
#endif
        }

        [CLSCompliant(false)]
        [Intrinsic]
        [SecurityCritical] // required to match contract
        [NonVersionable]
        public unsafe IntPtr(void* value)
        {
            _value = value;
        }

        [CLSCompliant(false)]
        [Intrinsic]
        [NonVersionable]
        public unsafe void* ToPointer()
        {
            return _value;
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe int ToInt32()
        {
#if BIT64
            long l = (long)_value;
            return checked((int)l);
#else
            return (int)_value;
#endif
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe long ToInt64()
        {
#if BIT64
            return (long)_value;
#else
            return (long)(int)_value;
#endif
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static explicit operator IntPtr(int value)
        {
            return new IntPtr(value);
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static explicit operator IntPtr(long value)
        {
            return new IntPtr(value);
        }

        [CLSCompliant(false)]
        [Intrinsic]
        [SecurityCritical] // required to match contract
        [NonVersionable]
        public unsafe static explicit operator IntPtr(void* value)
        {
            return new IntPtr(value);
        }

        [CLSCompliant(false)]
        [Intrinsic]
        [NonVersionable]
        public unsafe static explicit operator void* (IntPtr value)
        {
            return value._value;
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static explicit operator int (IntPtr value)
        {
#if BIT64
            long l = (long)value._value;
            return checked((int)l);
#else
            return (int)value._value;
#endif
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static explicit operator long (IntPtr value)
        {
#if BIT64
            return (long)value._value;
#else
            return (long)(int)value._value;
#endif
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static bool operator ==(IntPtr value1, IntPtr value2)
        {
            return value1._value == value2._value;
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static bool operator !=(IntPtr value1, IntPtr value2)
        {
            return value1._value != value2._value;
        }

        internal unsafe bool IsNull()
        {
            return (_value == null);
        }

        [NonVersionable]
        public static IntPtr Add(IntPtr pointer, int offset)
        {
            return pointer + offset;
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static IntPtr operator +(IntPtr pointer, int offset)
        {
#if BIT64
            return new IntPtr((long)pointer._value + offset);
#else
            return new IntPtr((int)pointer._value + offset);
#endif
        }

        [NonVersionable]
        public static IntPtr Subtract(IntPtr pointer, int offset)
        {
            return pointer - offset;
        }

        [Intrinsic]
        [NonVersionable]
        public unsafe static IntPtr operator -(IntPtr pointer, int offset)
        {
#if BIT64
            return new IntPtr((long)pointer._value - offset);
#else
            return new IntPtr((int)pointer._value - offset);
#endif
        }

        public unsafe static int Size
        {
            [Intrinsic]
            [NonVersionable]
            get
            {
#if BIT64
                return 8;
#else
                return 4;
#endif
            }
        }

        public unsafe override String ToString()
        {
#if BIT64
            return ((long)_value).ToString(FormatProvider.InvariantCulture);
#else
            return ((int)_value).ToString(FormatProvider.InvariantCulture);
#endif
        }

        public unsafe String ToString(String format)
        {
#if BIT64
            return ((long)_value).ToString(format, FormatProvider.InvariantCulture);
#else
            return ((int)_value).ToString(format, FormatProvider.InvariantCulture);
#endif
        }

        public unsafe override bool Equals(Object obj)
        {
            if (obj is IntPtr)
            {
                return (_value == ((IntPtr)obj)._value);
            }
            return false;
        }

        public unsafe override int GetHashCode()
        {
            // QUESTION: This HashCode seems to neglect the high order bits in calculating the hashcode?
            return unchecked((int)((long)_value));
        }
    }
}
