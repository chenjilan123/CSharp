using CSharp.Excel;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;

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
            //Aspose
            new AsposeExporter().Export();
            return;

            //NPOI
            var excelExporter = new NPOIExcelExporter();
            //excelExporter
            //    .CreaterExcel()
            //    .Export();
            excelExporter
                .CreateWorkBook();
        }
    }
}
