namespace ConsoleApp.DataStructure {
    public class ImportedColumn :ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
    }
}
