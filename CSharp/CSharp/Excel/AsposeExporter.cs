using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Excel
{
    public class AsposeExporter
    {
        public void Export()
        {
            var workbook = new Workbook(@".\xslxTemplate\报警明细.xlsx");
            var sheet = workbook.Worksheets[0];
            var cell = sheet.Cells[0, 0];
            cell.Value = "hehehehehe";
            workbook.Save(@".\Excel\xxx.xlsx");
        }
    }
}
