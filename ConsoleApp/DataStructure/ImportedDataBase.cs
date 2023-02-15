using System.Collections.Generic;

namespace ConsoleApp.DataStructure {
    public class ImportedDataBase :ImportedObjectBaseClass
    {
        public int NumberOfChildren => Tables.Count;
        public List<ImportedTable> Tables { get; set; }
    }
}
