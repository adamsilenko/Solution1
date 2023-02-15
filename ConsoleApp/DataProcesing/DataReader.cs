namespace ConsoleApp.DataProcesing {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ConsoleApp.DataStructure;

    public class DataReader
    {
        public List<ImportedDataBase> AllDataBases { get; private set; }
        public List<ImportedTable> AllTables { get; private set; }
        public List<ImportedColumn> AllColumns { get; private set; }        

        public void ImporFile(string fileToImport, bool printData = true) {
            ImportData(fileToImport);
            BuildTree();            
        }        

        private void BuildTree() {
            if (AllDataBases?.Count > 0 && AllTables?.Count > 0) {
                if (AllColumns?.Count > 0) {
                    var tableColumns = AllColumns.ToLookup(i => i.ParentName);
                    foreach(var table in AllTables)
                        table.Columns = tableColumns[table.Name].ToList();
                }
                var baseTables = AllTables.ToLookup(i => i.ParentName);
                foreach(var baseItem in AllDataBases)
                    baseItem.Tables = baseTables[baseItem.Name].ToList();
            }
        }

        private void ImportData(string fileToImport) {
            AllDataBases = new List<ImportedDataBase>();
            AllTables = new List<ImportedTable>();
            AllColumns = new List<ImportedColumn>();

            using(var streamReader = new StreamReader(fileToImport)) {
                //odczyt nagłówka
                streamReader.ReadLine();

                //Odczyt danych
                while(!streamReader.EndOfStream) {
                    var values = streamReader.ReadLine().Replace(" ", "").Split(';');

                    if(values.Length > 1) {
                        values = values.Select(field => field.Trim()).ToArray();
                        var type = values[0].ToUpper();
                        switch(type) {
                            case "DATABASE":
                                if(TryParse(values, out ImportedDataBase dataBase))
                                    AllDataBases.Add(dataBase);
                                //else
                                //    LogSkiped(values);
                                break;
                            case "TABLE":
                                if(TryParse(values, out ImportedTable table))
                                    AllTables.Add(table);
                                //else
                                //    LogSkiped(values);
                                break;
                            case "COLUMN":
                                if(TryParse(values, out ImportedColumn column))
                                    AllColumns.Add(column);
                                //else
                                //    LogSkiped(values);
                                break;
                            default:
                                //można dodać logowanie pominiętych danych
                                //LogSkiped(values);
                                break;

                        }
                    }
                }
            }
        }

        //private void LogSkiped(string[] values) {
        //    //throw new NotImplementedException();
        //}

        private bool TryParse(string[] values, out ImportedDataBase importedObject) {
            try {
                importedObject = new ImportedDataBase {
                    //Type = values[0],
                    Name = values[1],
                    //Schema = values[2],
                    //ParentName = values[3],
                    //ParentType = values[4],
                    //DataType = values[5],
                    //IsNullable = values[6]
                };
                return true;
            } catch {
                importedObject = null;
                return false;
            }
        }
        private bool TryParse(string[] values, out ImportedTable importedObject) {
            try {
                importedObject = new ImportedTable {
                    //Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    //ParentType = values[4],
                    //DataType = values[5],
                    //IsNullable = values[6]
                };
                return true;
            } catch {
                importedObject = null;
                return false;
            }
        }
        private bool TryParse(string[] values, out ImportedColumn importedObject) {
            try {
                importedObject = new ImportedColumn {
                    //Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    //ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6] != "0"
                };
                return true;
            } catch {
                importedObject = null;
                return false;
            }
        }
    }
}
