namespace Game
{
    /// <summary>
    /// 操作缩放倍数为倍的类型
    /// </summary>
    public struct NumberX1000
    {
        private long _valueX100;
        private const int SCALE = 1000;
        private const float SCALEf = 1000f;
        private const double SCALEd = 1000d;

        public static NumberX1000 Zero = new NumberX1000 { _valueX100 = 0 };
        public static readonly NumberX1000 One = new NumberX1000 { _valueX100 = SCALE * 1 };

        #region Implicit Operator & Explicit Operator
        public static implicit operator long(NumberX1000 number)
        {
            return number._valueX100 / SCALE;
        }
        public static implicit operator NumberX1000(long number)
        {
            return new NumberX1000 { _valueX100 = number * SCALE };
        }
        public static explicit operator int(NumberX1000 number)
        {
            return (int)(number._valueX100 / SCALE);
        }
        public static explicit operator NumberX1000(int number)
        {
            return new NumberX1000 { _valueX100 = number * SCALE };
        }
        public static explicit operator float(NumberX1000 number)
        {
            return number._valueX100 / SCALEf;
        }
        public static explicit operator NumberX1000(float number)
        {
            return new NumberX1000 { _valueX100 = (long)(number * SCALE) };
        }
        public static explicit operator double(NumberX1000 number)
        {
            return number._valueX100 / SCALEd;
        }
        public static explicit operator NumberX1000(double number)
        {
            return new NumberX1000 { _valueX100 = (long)(number * SCALE) };
        }
        #endregion

        #region Operator Overloading & Equals
        // operator +
        public static NumberX1000 operator +(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 + b._valueX100 };
        }
        public static NumberX1000 operator +(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 + b * SCALE };
        }
        public static NumberX1000 operator +(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * SCALE + b._valueX100 };
        }
        public static NumberX1000 operator +(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 + b * SCALE };
        }
        public static NumberX1000 operator +(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * SCALE + b._valueX100 };
        }
        public static NumberX1000 operator +(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 + (long)(b * SCALE) };
        }
        public static NumberX1000 operator +(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * SCALE) + b._valueX100 };
        }
        public static NumberX1000 operator +(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 + (long)(b * SCALE) };
        }
        public static NumberX1000 operator +(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * SCALE) + b._valueX100 };
        }

        // operator -
        public static NumberX1000 operator -(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 - b._valueX100 };
        }
        public static NumberX1000 operator -(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 - b * SCALE };
        }
        public static NumberX1000 operator -(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * SCALE - b._valueX100 };
        }
        public static NumberX1000 operator -(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 - b * SCALE };
        }
        public static NumberX1000 operator -(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * SCALE - b._valueX100 };
        }
        public static NumberX1000 operator -(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 - (long)(b * SCALE) };
        }
        public static NumberX1000 operator -(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * SCALE) - b._valueX100 };
        }
        public static NumberX1000 operator -(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 - (long)(b * SCALE) };
        }
        public static NumberX1000 operator -(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * SCALE) - b._valueX100 };
        }

        // operator *
        public static NumberX1000 operator *(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 * b._valueX100 / SCALE };
        }
        public static NumberX1000 operator *(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 * b };
        }
        public static NumberX1000 operator *(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * b._valueX100 };
        }
        public static NumberX1000 operator *(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 * b };
        }
        public static NumberX1000 operator *(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * b._valueX100 };
        }
        public static NumberX1000 operator *(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX100 = (long)(a._valueX100 * b) };
        }
        public static NumberX1000 operator *(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * b._valueX100) };
        }
        public static NumberX1000 operator *(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX100 = (long)(a._valueX100 * b) };
        }
        public static NumberX1000 operator *(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * b._valueX100) };
        }

        // operator /
        public static NumberX1000 operator /(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 * SCALE / b._valueX100 };
        }
        public static NumberX1000 operator /(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 / b };
        }
        public static NumberX1000 operator /(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * SCALE / b._valueX100 };
        }
        public static NumberX1000 operator /(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX100 = a._valueX100 / b };
        }
        public static NumberX1000 operator /(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = a * SCALE / b._valueX100 };
        }
        public static NumberX1000 operator /(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX100 = (long)(a._valueX100 / b) };
        }
        public static NumberX1000 operator /(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * SCALE / b._valueX100) };
        }
        public static NumberX1000 operator /(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX100 = (long)(a._valueX100 / b) };
        }
        public static NumberX1000 operator /(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX100 = (long)(a * SCALE / b._valueX100) };
        }

        // operator == & operator !=
        public static bool operator ==(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX100 == b._valueX100;
        }
        public static bool operator !=(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX100 != b._valueX100;
        }
        public static bool operator ==(NumberX1000 a, long b)
        {
            return a._valueX100 == b * SCALE;
        }
        public static bool operator !=(NumberX1000 a, long b)
        {
            return a._valueX100 != b * SCALE;
        }
        public static bool operator ==(long a, NumberX1000 b)
        {
            return a * SCALE == b._valueX100;
        }
        public static bool operator !=(long a, NumberX1000 b)
        {
            return a * SCALE != b._valueX100;
        }
        public static bool operator ==(NumberX1000 a, int b)
        {
            return a._valueX100 == b * SCALE;
        }
        public static bool operator !=(NumberX1000 a, int b)
        {
            return a._valueX100 != b * SCALE;
        }
        public static bool operator ==(int a, NumberX1000 b)
        {
            return a * SCALE == b._valueX100;
        }
        public static bool operator !=(int a, NumberX1000 b)
        {
            return a * SCALE != b._valueX100;
        }
        public static bool operator ==(NumberX1000 a, float b)
        {
            return a._valueX100 == (long)(b * SCALE);
        }
        public static bool operator !=(NumberX1000 a, float b)
        {
            return a._valueX100 != (long)(b * SCALE);
        }
        public static bool operator ==(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) == b._valueX100;
        }
        public static bool operator !=(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) != b._valueX100;
        }
        public static bool operator ==(NumberX1000 a, double b)
        {
            return a._valueX100 == (long)(b * SCALE);
        }
        public static bool operator !=(NumberX1000 a, double b)
        {
            return a._valueX100 != (long)(b * SCALE);
        }
        public static bool operator ==(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) == b._valueX100;
        }
        public static bool operator !=(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) != b._valueX100;
        }

        // operator > & operator <
        public static bool operator >(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX100 > b._valueX100;
        }
        public static bool operator <(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX100 < b._valueX100;
        }
        public static bool operator >(NumberX1000 a, long b)
        {
            return a._valueX100 > b * SCALE;
        }
        public static bool operator <(NumberX1000 a, long b)
        {
            return a._valueX100 < b * SCALE;
        }
        public static bool operator >(long a, NumberX1000 b)
        {
            return a * SCALE > b._valueX100;
        }
        public static bool operator <(long a, NumberX1000 b)
        {
            return a * SCALE < b._valueX100;
        }
        public static bool operator >(NumberX1000 a, int b)
        {
            return a._valueX100 > b * SCALE;
        }
        public static bool operator <(NumberX1000 a, int b)
        {
            return a._valueX100 < b * SCALE;
        }
        public static bool operator >(int a, NumberX1000 b)
        {
            return a * SCALE > b._valueX100;
        }
        public static bool operator <(int a, NumberX1000 b)
        {
            return a * SCALE < b._valueX100;
        }
        public static bool operator >(NumberX1000 a, float b)
        {
            return a._valueX100 > (long)(b * SCALE);
        }
        public static bool operator <(NumberX1000 a, float b)
        {
            return a._valueX100 < (long)(b * SCALE);
        }
        public static bool operator >(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) > b._valueX100;
        }
        public static bool operator <(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) < b._valueX100;
        }
        public static bool operator >(NumberX1000 a, double b)
        {
            return a._valueX100 > (long)(b * SCALE);
        }
        public static bool operator <(NumberX1000 a, double b)
        {
            return a._valueX100 < (long)(b * SCALE);
        }
        public static bool operator >(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) > b._valueX100;
        }
        public static bool operator <(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) < b._valueX100;
        }
        #endregion

        public int ToInt()
        {
            return (int)(_valueX100 / SCALE);
        }
        public long ToLong()
        {
            return _valueX100 / SCALE;
        }
        public float ToFloat()
        {
            return _valueX100 / SCALEf;
        }
        public double ToDouble()
        {
            return _valueX100 / SCALEd;
        }
        public override string ToString()
        {
            return (_valueX100 / SCALE).ToString();
        }
        public string ToFloatString()
        {
            return (_valueX100 / SCALEf).ToString();
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                NumberX1000 number => this == number,
                long number => this == number,
                int number => this == number,
                float number => this == number,
                double number => this == number,
                decimal number => this == number,
                _ => false
            };
        }
        public override int GetHashCode()
        {
            return _valueX100.GetHashCode();
        }
    }
}
