using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace ConsoleAppCheck
{
    internal class TableVisualisation
    {
        internal static void ShowTable<T>(List<T> tableData) where T: class{
            Console.WriteLine("\n\n");

            ConsoleTableBuilder.From(tableData)
            .WithTitle("Coding")
            .ExportAndWriteLine();

            Console.WriteLine("\n\n");
        }
        
    }
}