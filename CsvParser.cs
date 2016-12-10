using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsvTools
{
    public class CsvParser
    {
        private CsvTable _table;
        private StreamReader _reader;

        private CsvParser()
        {
        }

        public static CsvTable ParseTable(byte[] data, bool normalizeHeaderNames = true)
        {
            return new CsvParser().InternalParse(data, normalizeHeaderNames);
        }

        private CsvTable InternalParse(byte[] data, bool normalizeHeaderNames)
        {
            _table = new CsvTable();
            _reader = new StreamReader(new MemoryStream(data));
            string[] row = null;

            ReadHeaders(normalizeHeaderNames);

            for (;;) {
                row = GetLineValues();

                if (row == null) {
                    break;
                }

                _table.AddRow(row);
            }

            return _table;
        }

        private void ReadHeaders(bool normalizeHeaderNames)
        {
            string[] line = GetLineValues();

            int hIndex = 0;
            foreach (var name in line) {
                string columnName = name;
                if (normalizeHeaderNames) {
                    columnName = new string(name.ToLower()
                        .Where(c => char.IsLetterOrDigit(c)).ToArray());
                }
                var header = new CsvHeader(hIndex++, columnName);
                _table.AddHeader(header);
            }
        }

        private string[] GetLineValues()
        {
            string[] result;
            string line = _reader.ReadLine();

            if (line == null) {
                return null;
            }

            result = Regex.Split(line, @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))");

            return result;
        }
    }
}
