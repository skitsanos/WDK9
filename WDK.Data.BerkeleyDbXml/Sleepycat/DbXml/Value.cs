namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;
    using System.Text;

    public class Value : IDisposable, ICloneable
    {
        private XmlValue v_;

        private Value(XmlValue v)
        {
            this.v_ = null;
            if (v == null)
            {
                throw new NullReferenceException();
            }
            this.v_ = v;
        }

        public Value(Node node) : this(Node.CreateXmlValue(node))
        {
        }

        public Value(bool v) : this(new XmlValue(v))
        {
        }

        public Value(DateTime dt) : this(ValueType.DATE_TIME, DateTimeToSchemaString(dt))
        {
        }

        public Value(double v) : this(new XmlValue(v))
        {
        }

        public Value(string v) : this(new XmlValue(v))
        {
        }

        public Value(ValueType type, Data v) : this(new XmlValue(ValueTypeToInt(type), v.Internal))
        {
        }

        public Value(ValueType type, string v) : this(new XmlValue(ValueTypeToInt(type), v))
        {
        }

        public Value Clone()
        {
            return Create(new XmlValue(this.v_));
        }

        internal static Value Create(XmlValue v)
        {
            if (v == null)
            {
                return null;
            }
            return new Value(v);
        }

        private static string DateTimeToSchemaString(DateTime dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(minimumDigits(dt.Year, 4, true));
            builder.Append('-');
            builder.Append(minimumDigits(dt.Month, 2, false));
            builder.Append('-');
            builder.Append(minimumDigits(dt.Day, 2, false));
            builder.Append('T');
            builder.Append(minimumDigits(dt.Hour, 2, false));
            builder.Append(':');
            builder.Append(minimumDigits(dt.Minute, 2, false));
            builder.Append(':');
            builder.Append(minimumDigits(dt.Second, 2, false));
            builder.Append('.');
            builder.Append(minimumDigits(dt.Millisecond, 3, false));
            return builder.ToString();
        }

        public void Dispose()
        {
            if (this.v_ != null)
            {
                this.v_.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public override bool Equals(object o)
        {
            if (o is Value)
            {
                return this.v_.equals(((Value) o).Internal);
            }
            if (o is Node)
            {
                using (XmlValue value2 = Node.CreateXmlValue((Node) o))
                {
                    return this.v_.equals(value2);
                }
            }
            return false;
        }

        ~Value()
        {
            this.Dispose();
        }

        public override int GetHashCode()
        {
            return (this.ToString().GetHashCode() ^ this.Type.GetHashCode());
        }

        private static ValueType IntToValueType(int type)
        {
            if (type == XmlValue.NODE)
            {
                return ValueType.NODE;
            }
            if (type == XmlValue.ANY_URI)
            {
                return ValueType.ANY_URI;
            }
            if (type == XmlValue.BASE_64_BINARY)
            {
                return ValueType.BASE_64_BINARY;
            }
            if (type == XmlValue.BOOLEAN)
            {
                return ValueType.BOOLEAN;
            }
            if (type == XmlValue.DATE)
            {
                return ValueType.DATE;
            }
            if (type == XmlValue.DATE_TIME)
            {
                return ValueType.DATE_TIME;
            }
            if (type == XmlValue.DAY_TIME_DURATION)
            {
                return ValueType.DAY_TIME_DURATION;
            }
            if (type == XmlValue.DECIMAL)
            {
                return ValueType.DECIMAL;
            }
            if (type == XmlValue.DOUBLE)
            {
                return ValueType.DOUBLE;
            }
            if (type == XmlValue.DURATION)
            {
                return ValueType.DURATION;
            }
            if (type == XmlValue.FLOAT)
            {
                return ValueType.FLOAT;
            }
            if (type == XmlValue.G_DAY)
            {
                return ValueType.G_DAY;
            }
            if (type == XmlValue.G_MONTH)
            {
                return ValueType.G_MONTH;
            }
            if (type == XmlValue.G_MONTH_DAY)
            {
                return ValueType.G_MONTH_DAY;
            }
            if (type == XmlValue.G_YEAR)
            {
                return ValueType.G_YEAR;
            }
            if (type == XmlValue.G_YEAR_MONTH)
            {
                return ValueType.G_YEAR_MONTH;
            }
            if (type == XmlValue.HEX_BINARY)
            {
                return ValueType.HEX_BINARY;
            }
            if (type == XmlValue.NOTATION)
            {
                return ValueType.NOTATION;
            }
            if (type == XmlValue.QNAME)
            {
                return ValueType.QNAME;
            }
            if (type == XmlValue.STRING)
            {
                return ValueType.STRING;
            }
            if (type == XmlValue.TIME)
            {
                return ValueType.TIME;
            }
            if (type == XmlValue.YEAR_MONTH_DURATION)
            {
                return ValueType.YEAR_MONTH_DURATION;
            }
            if (type != XmlValue.UNTYPED_ATOMIC)
            {
                throw new Exception("Value type not covered in nested if statements: " + type);
            }
            return ValueType.UNTYPED_ATOMIC;
        }

        private static string minimumDigits(int num, int digits, bool canBeNegative)
        {
            string str = Math.Abs(num).ToString();
            if (str.Length < digits)
            {
                str = new string('0', digits - str.Length) + str;
            }
            if (canBeNegative && (num < 0))
            {
                str = "-" + str;
            }
            return str;
        }

        private static DateTime SchemaStringToDateTime(string dt)
        {
            DateTime time;
            try
            {
                dt = dt.Trim();
                int index = dt.IndexOf('-');
                switch (index)
                {
                    case 0:
                        index = dt.IndexOf('-', 1);
                        break;

                    case -1:
                        throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
                }
                int year = int.Parse(dt.Substring(0, index));
                int num3 = dt.IndexOf('-', index + 1);
                if (num3 == -1)
                {
                    throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
                }
                int month = int.Parse(dt.Substring(index + 1, (num3 - index) - 1));
                int num5 = dt.IndexOf('T', num3 + 1);
                if (num5 == -1)
                {
                    throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
                }
                int day = int.Parse(dt.Substring(num3 + 1, (num5 - num3) - 1));
                int num7 = dt.IndexOf(':', num5 + 1);
                if (num7 == -1)
                {
                    throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
                }
                int hour = int.Parse(dt.Substring(num5 + 1, (num7 - num5) - 1));
                int num9 = dt.IndexOf(':', num7 + 1);
                if (num9 == -1)
                {
                    throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
                }
                int minute = int.Parse(dt.Substring(num7 + 1, (num9 - num7) - 1));
                int num11 = dt.IndexOf('.', num9 + 1);
                if (num11 == -1)
                {
                    throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
                }
                int second = int.Parse(dt.Substring(num9 + 1, (num11 - num9) - 1));
                int length = dt.Length - num11;
                if (length > 3)
                {
                    length = 3;
                }
                int millisecond = int.Parse(dt.Substring(num11 + 1, length));
                time = new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            catch (ArgumentNullException)
            {
                throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
            }
            catch (FormatException)
            {
                throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
            }
            catch (OverflowException)
            {
                throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new DbXmlException(DbXmlException.ExceptionCode.INVALID_VALUE, "The dateTime string is lexically invalid");
            }
            return time;
        }

        object ICloneable.Clone()
        {
            return Create(new XmlValue(this.v_));
        }

        public bool ToBoolean()
        {
            return this.v_.asBoolean();
        }

        public DateTime ToDateTime()
        {
            return SchemaStringToDateTime(this.v_.asString());
        }

        public Document ToDocument()
        {
            return Document.Create(this.v_.asDocument());
        }

        public double ToDouble()
        {
            return this.v_.asNumber();
        }

        internal static XmlValue ToInternal(Value v)
        {
            if (v == null)
            {
                return null;
            }
            return v.Internal;
        }

        public Node ToNode()
        {
            return Node.CreateNode(new XmlValue(this.v_));
        }

        public override string ToString()
        {
            return this.v_.asString();
        }

        private static int ValueTypeToInt(ValueType type)
        {
            switch (type)
            {
                case ValueType.NODE:
                    return XmlValue.NODE;

                case ValueType.ANY_URI:
                    return XmlValue.ANY_URI;

                case ValueType.BASE_64_BINARY:
                    return XmlValue.BASE_64_BINARY;

                case ValueType.BOOLEAN:
                    return XmlValue.BOOLEAN;

                case ValueType.DATE:
                    return XmlValue.DATE;

                case ValueType.DATE_TIME:
                    return XmlValue.DATE_TIME;

                case ValueType.DAY_TIME_DURATION:
                    return XmlValue.DAY_TIME_DURATION;

                case ValueType.DECIMAL:
                    return XmlValue.DECIMAL;

                case ValueType.DOUBLE:
                    return XmlValue.DOUBLE;

                case ValueType.DURATION:
                    return XmlValue.DURATION;

                case ValueType.FLOAT:
                    return XmlValue.FLOAT;

                case ValueType.G_DAY:
                    return XmlValue.G_DAY;

                case ValueType.G_MONTH:
                    return XmlValue.G_MONTH;

                case ValueType.G_MONTH_DAY:
                    return XmlValue.G_MONTH_DAY;

                case ValueType.G_YEAR:
                    return XmlValue.G_YEAR;

                case ValueType.G_YEAR_MONTH:
                    return XmlValue.G_YEAR_MONTH;

                case ValueType.HEX_BINARY:
                    return XmlValue.HEX_BINARY;

                case ValueType.NOTATION:
                    return XmlValue.NOTATION;

                case ValueType.QNAME:
                    return XmlValue.QNAME;

                case ValueType.STRING:
                    return XmlValue.STRING;

                case ValueType.TIME:
                    return XmlValue.TIME;

                case ValueType.YEAR_MONTH_DURATION:
                    return XmlValue.YEAR_MONTH_DURATION;

                case ValueType.UNTYPED_ATOMIC:
                    return XmlValue.UNTYPED_ATOMIC;
            }
            throw new Exception("Value type not covered in switch: " + type);
        }

        private XmlValue Internal
        {
            get
            {
                return this.v_;
            }
        }

        public ValueType Type
        {
            get
            {
                return IntToValueType(this.v_.getType());
            }
        }

        public enum ValueType
        {
            NODE,
            ANY_URI,
            BASE_64_BINARY,
            BOOLEAN,
            DATE,
            DATE_TIME,
            DAY_TIME_DURATION,
            DECIMAL,
            DOUBLE,
            DURATION,
            FLOAT,
            G_DAY,
            G_MONTH,
            G_MONTH_DAY,
            G_YEAR,
            G_YEAR_MONTH,
            HEX_BINARY,
            NOTATION,
            QNAME,
            STRING,
            TIME,
            YEAR_MONTH_DURATION,
            UNTYPED_ATOMIC
        }
    }
}

