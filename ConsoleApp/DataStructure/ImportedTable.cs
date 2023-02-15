using System.Collections.Generic;

namespace ConsoleApp.DataStructure {
    public class ImportedTable : ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public int NumberOfChildren => Columns.Count;
        public List<ImportedColumn> Columns { get; set; }
    }    
}
