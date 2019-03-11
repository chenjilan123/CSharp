using CSharp.Excel;
using NPOI.HSSF.UserModel;
using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Readonly filed is not shared to every instance
            //var jet1 = new Jet("Jet1");
            //var jet2 = new Jet("Jet2");
            //jet1.PrintName();
            //jet2.PrintName();
            //return;

            Excel();
        }

        private static void Excel()
        {
            var excelExporter = new ExcelExporter();
            //excelExporter
            //    .CreaterExcel()
            //    .Export();

            excelExporter
                .CreateWorkBook();
        }
    }
}
