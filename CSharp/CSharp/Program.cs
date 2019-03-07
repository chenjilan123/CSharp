using CSharp.Excel;
using NPOI.HSSF.UserModel;
using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel();
        }

        private static void Excel()
        {
            var excelExporter = new ExcelExporter();
            excelExporter.Export();
        }
    }
}
