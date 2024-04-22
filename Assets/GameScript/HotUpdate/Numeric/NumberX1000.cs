namespace Game
{
    /// <summary>
    /// 操作缩放倍数为倍的类型
    /// </summary>
    public struct NumberX1000
    {
        private long _valueX1000;
        private const int SCALE = 1000;
        private const float SCALEf = 1000f;
        private const double SCALEd = 1000d;

        public static NumberX1000 Zero = new NumberX1000 { _valueX1000 = 0 };
        public static readonly NumberX1000 One = new NumberX1000 { _valueX1000 = SCALE * 1 };
        public static NumberX1000 CreateFromX1000Value(long value)
        {
            return new NumberX1000 { _valueX1000 = value };
        }

        #region Implicit Operator & Explicit Operator
        public static implicit operator long(NumberX1000 number)
        {
            return number._valueX1000 / SCALE;
        }
        public static implicit operator NumberX1000(long number)
        {
            return new NumberX1000 { _valueX1000 = number * SCALE };
        }
        public static implicit operator int(NumberX1000 number)
        {
            return (int)(number._valueX1000 / SCALE);
        }
        public static implicit operator NumberX1000(int number)
        {
            return new NumberX1000 { _valueX1000 = number * SCALE };
        }
        public static implicit operator float(NumberX1000 number)
        {
            return number._valueX1000 / SCALEf;
        }
        public static implicit operator NumberX1000(float number)
        {
            return new NumberX1000 { _valueX1000 = (long)(number * SCALE) };
        }
        public static implicit operator double(NumberX1000 number)
        {
            return number._valueX1000 / SCALEd;
        }
        public static implicit operator NumberX1000(double number)
        {
            return new NumberX1000 { _valueX1000 = (long)(number * SCALE) };
        }
        #endregion

        #region Operator Overloading & Equals
        // operator +
        public static NumberX1000 operator +(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 + b._valueX1000 };
        }
        public static NumberX1000 operator +(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 + b * SCALE };
        }
        public static NumberX1000 operator +(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * SCALE + b._valueX1000 };
        }
        public static NumberX1000 operator +(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 + b * SCALE };
        }
        public static NumberX1000 operator +(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * SCALE + b._valueX1000 };
        }
        public static NumberX1000 operator +(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 + (long)(b * SCALE) };
        }
        public static NumberX1000 operator +(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * SCALE) + b._valueX1000 };
        }
        public static NumberX1000 operator +(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 + (long)(b * SCALE) };
        }
        public static NumberX1000 operator +(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * SCALE) + b._valueX1000 };
        }

        // operator -
        public static NumberX1000 operator -(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 - b._valueX1000 };
        }
        public static NumberX1000 operator -(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 - b * SCALE };
        }
        public static NumberX1000 operator -(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * SCALE - b._valueX1000 };
        }
        public static NumberX1000 operator -(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 - b * SCALE };
        }
        public static NumberX1000 operator -(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * SCALE - b._valueX1000 };
        }
        public static NumberX1000 operator -(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 - (long)(b * SCALE) };
        }
        public static NumberX1000 operator -(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * SCALE) - b._valueX1000 };
        }
        public static NumberX1000 operator -(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 - (long)(b * SCALE) };
        }
        public static NumberX1000 operator -(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * SCALE) - b._valueX1000 };
        }

        // operator *
        public static NumberX1000 operator *(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 * b._valueX1000 / SCALE };
        }
        public static NumberX1000 operator *(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 * b };
        }
        public static NumberX1000 operator *(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * b._valueX1000 };
        }
        public static NumberX1000 operator *(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 * b };
        }
        public static NumberX1000 operator *(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * b._valueX1000 };
        }
        public static NumberX1000 operator *(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a._valueX1000 * b) };
        }
        public static NumberX1000 operator *(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * b._valueX1000) };
        }
        public static NumberX1000 operator *(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a._valueX1000 * b) };
        }
        public static NumberX1000 operator *(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * b._valueX1000) };
        }

        // operator /
        public static NumberX1000 operator /(NumberX1000 a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 * SCALE / b._valueX1000 };
        }
        public static NumberX1000 operator /(NumberX1000 a, long b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 / b };
        }
        public static NumberX1000 operator /(long a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * SCALE / b._valueX1000 };
        }
        public static NumberX1000 operator /(NumberX1000 a, int b)
        {
            return new NumberX1000 { _valueX1000 = a._valueX1000 / b };
        }
        public static NumberX1000 operator /(int a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = a * SCALE / b._valueX1000 };
        }
        public static NumberX1000 operator /(NumberX1000 a, float b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a._valueX1000 / b) };
        }
        public static NumberX1000 operator /(float a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * SCALE / b._valueX1000) };
        }
        public static NumberX1000 operator /(NumberX1000 a, double b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a._valueX1000 / b) };
        }
        public static NumberX1000 operator /(double a, NumberX1000 b)
        {
            return new NumberX1000 { _valueX1000 = (long)(a * SCALE / b._valueX1000) };
        }

        // operator == & operator !=
        public static bool operator ==(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX1000 == b._valueX1000;
        }
        public static bool operator !=(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX1000 != b._valueX1000;
        }
        public static bool operator ==(NumberX1000 a, long b)
        {
            return a._valueX1000 == b * SCALE;
        }
        public static bool operator !=(NumberX1000 a, long b)
        {
            return a._valueX1000 != b * SCALE;
        }
        public static bool operator ==(long a, NumberX1000 b)
        {
            return a * SCALE == b._valueX1000;
        }
        public static bool operator !=(long a, NumberX1000 b)
        {
            return a * SCALE != b._valueX1000;
        }
        public static bool operator ==(NumberX1000 a, int b)
        {
            return a._valueX1000 == b * SCALE;
        }
        public static bool operator !=(NumberX1000 a, int b)
        {
            return a._valueX1000 != b * SCALE;
        }
        public static bool operator ==(int a, NumberX1000 b)
        {
            return a * SCALE == b._valueX1000;
        }
        public static bool operator !=(int a, NumberX1000 b)
        {
            return a * SCALE != b._valueX1000;
        }
        public static bool operator ==(NumberX1000 a, float b)
        {
            return a._valueX1000 == (long)(b * SCALE);
        }
        public static bool operator !=(NumberX1000 a, float b)
        {
            return a._valueX1000 != (long)(b * SCALE);
        }
        public static bool operator ==(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) == b._valueX1000;
        }
        public static bool operator !=(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) != b._valueX1000;
        }
        public static bool operator ==(NumberX1000 a, double b)
        {
            return a._valueX1000 == (long)(b * SCALE);
        }
        public static bool operator !=(NumberX1000 a, double b)
        {
            return a._valueX1000 != (long)(b * SCALE);
        }
        public static bool operator ==(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) == b._valueX1000;
        }
        public static bool operator !=(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) != b._valueX1000;
        }

        // operator > & operator <
        public static bool operator >(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX1000 > b._valueX1000;
        }
        public static bool operator <(NumberX1000 a, NumberX1000 b)
        {
            return a._valueX1000 < b._valueX1000;
        }
        public static bool operator >(NumberX1000 a, long b)
        {
            return a._valueX1000 > b * SCALE;
        }
        public static bool operator <(NumberX1000 a, long b)
        {
            return a._valueX1000 < b * SCALE;
        }
        public static bool operator >(long a, NumberX1000 b)
        {
            return a * SCALE > b._valueX1000;
        }
        public static bool operator <(long a, NumberX1000 b)
        {
            return a * SCALE < b._valueX1000;
        }
        public static bool operator >(NumberX1000 a, int b)
        {
            return a._valueX1000 > b * SCALE;
        }
        public static bool operator <(NumberX1000 a, int b)
        {
            return a._valueX1000 < b * SCALE;
        }
        public static bool operator >(int a, NumberX1000 b)
        {
            return a * SCALE > b._valueX1000;
        }
        public static bool operator <(int a, NumberX1000 b)
        {
            return a * SCALE < b._valueX1000;
        }
        public static bool operator >(NumberX1000 a, float b)
        {
            return a._valueX1000 > (long)(b * SCALE);
        }
        public static bool operator <(NumberX1000 a, float b)
        {
            return a._valueX1000 < (long)(b * SCALE);
        }
        public static bool operator >(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) > b._valueX1000;
        }
        public static bool operator <(float a, NumberX1000 b)
        {
            return (long)(a * SCALE) < b._valueX1000;
        }
        public static bool operator >(NumberX1000 a, double b)
        {
            return a._valueX1000 > (long)(b * SCALE);
        }
        public static bool operator <(NumberX1000 a, double b)
        {
            return a._valueX1000 < (long)(b * SCALE);
        }
        public static bool operator >(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) > b._valueX1000;
        }
        public static bool operator <(double a, NumberX1000 b)
        {
            return (long)(a * SCALE) < b._valueX1000;
        }
        #endregion

        public int ToInt()
        {
            return (int)(_valueX1000 / SCALE);
        }
        public long ToLong()
        {
            return _valueX1000 / SCALE;
        }

        public float ToFloat()
        {
            return _valueX1000 / SCALEf;
        }
        public double ToDouble()
        {
            return _valueX1000 / SCALEd;
        }

        public NumberX1000 AbsTo(NumberX1000 other)
        {
            if (this > other)
            {
                return this - other;
            }
            else
            {
                return other - this;
            }
        }

        public bool ApproximatelyTo(NumberX1000 other)
        {
            var tolerance = CreateFromX1000Value(20);
            return AbsTo(other) < tolerance;
        }

        public NumberX1000 Ceil()
        {
            var remainder = _valueX1000 % SCALE;
            if (remainder == 0)
            {
                return this;
            }
            else
            {
                return new NumberX1000 { _valueX1000 = _valueX1000 + SCALE - remainder };
            }
        }

        public NumberX1000 Floor()
        {
            var remainder = _valueX1000 % SCALE;
            if (remainder == 0)
            {
                return this;
            }
            else
            {
                return new NumberX1000 { _valueX1000 = _valueX1000 - remainder };
            }
        }

        /// <summary>
        /// 输出浮点数字符串，默认保留3位小数
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString("F3");
        }
        public string ToString(string format)
        {
            return (_valueX1000 / SCALEf).ToString(format);
        }
        public string ToIntegerString()
        {
            return (_valueX1000 / SCALE).ToString();
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
            return _valueX1000.GetHashCode();
        }
    }
}
