namespace ConsoleApp
{
    using ConsoleApp.DataProcesing;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataProcesing.DataReader();
            reader.ImporFile("data.csv");
            DataExport.PrintDataBaseObjects(reader.AllDataBases);
            Console.ReadLine();
        }
    }
}
