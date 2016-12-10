using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvTools
{
    public class CsvTable : IEnumerable<CsvRow>
    {
        private List<CsvHeader> _headers = new List<CsvHeader>();
        private List<CsvRow> _rows = new List<CsvRow>();

        public CsvRow this[int index]
        {
            get
            {
                return _rows[index];
            }
        }
        public int NumColumns
        {
            get
            {
                return _headers.Count;
            }
        }
        public int NumRows
        {
            get
            {
                return _rows.Count;
            }
        }

        public bool HasColumn(string name)
        {
            return _headers.Any(t => t.Name == name);
        }

        public CsvHeader GetHeader(int index)
        {
            return _headers[index];
        }

        public CsvHeader GetHeader(string name)
        {
            return _headers.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerator<CsvRow> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        internal void AddHeader(CsvHeader header)
        {
            _headers.Add(header);
        }

        internal void AddRow(string[] row)
        {
            _rows.Add(new CsvRow(row, this));
        }
    }
}
