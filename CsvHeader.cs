namespace CsvTools
{
    public class CsvHeader
    {
        public int Index { get; private set; }
        public string Name { get; private set; }

        public CsvHeader(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}
