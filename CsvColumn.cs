using System;
using System.Globalization;

namespace CsvTools
{
    public interface ICsvColumn
    {
        string Name { get; }
        string GetString();
        Guid GetGuid();
        DateTime GetDate(string format = "yyyyMMdd");
        int GetInt();
        decimal GetDecimal();
        double GetDouble();
        bool GetBool();
    }

    internal class CsvColumn : ICsvColumn
    {
        private readonly string _data;

        public string Name { get; private set; }

        public CsvColumn(string data)
        {
            _data = data;
        }

        public string GetString() => _data;

        public bool GetBool()
        {
            if (string.IsNullOrWhiteSpace(_data)) {
                return false;
            }

            bool result;
            if (!bool.TryParse(_data, out result)) {
                throw new InvalidCastException(string.Format(
                    "Value {0} is not a boolean",
                    _data
                    ));
            }
            return result;
        }

        public DateTime GetDate(string format = "yyyyMMdd")
        {
            if (string.IsNullOrWhiteSpace(_data)) {
                return DateTime.MinValue;
            }

            DateTime result;
            if (!DateTime.TryParseExact(_data, format, null, DateTimeStyles.AllowWhiteSpaces, out result)) {
                throw new InvalidCastException(string.Format(
                    "Value {0} is not a date time",
                    _data
                    ));
            }
            return result;
        }

        public decimal GetDecimal()
        {
            if (string.IsNullOrWhiteSpace(_data)) {
                return 0;
            }

            decimal result;
            if (!decimal.TryParse(_data, out result)) {
                throw new InvalidCastException(string.Format(
                    "Value {0} is not a decimal",
                    _data
                    ));
            }
            return result;
        }

        public double GetDouble()
        {
            if (string.IsNullOrWhiteSpace(_data)) {
                return 0;
            }

            double result;
            if (!double.TryParse(_data, out result)) {
                throw new InvalidCastException(string.Format(
                    "Value {0} is not a decimal",
                    _data
                    ));
            }
            return result;
        }

        public Guid GetGuid()
        {
            if (string.IsNullOrWhiteSpace(_data)) {
                return new Guid();
            }

            Guid result;
            if (!Guid.TryParse(_data, out result)) {
                throw new InvalidCastException(string.Format(
                    "Value {0} is not a GUID",
                    _data
                    ));
            }
            return result;
        }

        public int GetInt()
        {
            if (string.IsNullOrWhiteSpace(_data)) {
                return 0;
            }

            int result;
            if (!int.TryParse(_data, out result)) {
                throw new InvalidCastException(string.Format(
                    "Value {0} is not an int",
                    _data
                    ));
            }
            return result;
        }

    }
}
