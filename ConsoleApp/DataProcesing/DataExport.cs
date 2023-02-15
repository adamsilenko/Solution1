using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.DataStructure;

namespace ConsoleApp.DataProcesing {
    internal static class DataExport {
        public static void PrintDataBaseObjects(IEnumerable<ImportedDataBase> dataBases) {
            foreach(var database in dataBases) {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                PrintDataBaseObjects(database.Tables);
            }
        }

        public static void PrintDataBaseObjects(IEnumerable<ImportedTable> tables) {
            foreach(var table in tables) {
                Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                PrintDataBaseObjects(table.Columns);
            }
        }

        public static void PrintDataBaseObjects(IEnumerable<ImportedColumn> columns) {
            foreach(var column in columns) 
                Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable ? "accepts nulls" : "with no nulls")}");            
        }
    }
}
