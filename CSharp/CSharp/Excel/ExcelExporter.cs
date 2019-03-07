using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace CSharp.Excel
{
    public class ExcelExporter
    {
        public void Export()
        {
            FileStream file = new FileStream("", FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            file.Close();
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;
            style.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style.ShrinkToFit = true;
            style.WrapText = true;

            NPOI.SS.UserModel.IFont sheetfont = hssfworkbook.CreateFont();
            //font.Boldweight = (short)FontBoldWeight.BOLD;
            sheetfont.FontName = ("宋体");
            sheetfont.FontHeightInPoints = 9;// 22;
            style.SetFont(sheetfont);

            ISheet sheet1 = hssfworkbook.GetSheet("hello oha!");

        }
    }
}
