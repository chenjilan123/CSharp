using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.F40
{
    class Program
    {
        static void Main(string[] args)
        {
            var workbook = new Workbook(@".\xslxTemplate\报警明细.xlsx");
            var sheet = workbook.Worksheets[0];
            var cell = sheet.Cells[0, 0];
            cell.Value = "hehehehehe";
            workbook.Save(@"xxx.xlsx");
            //Application
        }
    }
}
