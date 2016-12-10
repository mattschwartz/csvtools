using System.Collections.Generic;

namespace CsvTools
{
    public class CsvRow
    {
        private string[] _cells;
        private CsvTable _table;
        private Dictionary<string, ICsvColumn> _columnCache = new Dictionary<string, ICsvColumn>();

        public CsvRow(string[] row, CsvTable table)
        {
            _cells = row;
            _table = table;
        }

        public ICsvColumn this[int index]
        {
            get
            {
                return GetColumn(index);
            }
        }
        public ICsvColumn this[string name]
        {
            get
            {
                return GetColumn(name);
            }
        }

        private ICsvColumn GetColumn(int index)
        {
            string name = _table.GetHeader(index).Name;

            return GetColumn(name);
        }

        private ICsvColumn GetColumn(string name)
        {
            ICsvColumn column;

            if (_columnCache.TryGetValue(name, out column)) {
                return _columnCache[name];
            }

            CsvHeader header = _table.GetHeader(name);

            column = new CsvColumn(_cells[header.Index]);

            return _columnCache[name] = column;
        }
    }
}
