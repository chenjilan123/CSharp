using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

//NPOI.Util 基础辅助库
//NPOI.POIFS OLE2格式读写库
//NPOI.DDF Microsoft Drawing格式读写库
//NPOI.SS   Excel公式计算库
//NPOI.HPSF OLE2的Summary Information和Document Summary Information属性读写库
//NPOI.HSSF Excel BIFF格式读写库
namespace CSharp.Excel
{
    //主要名称空间: 
    //  NPOI.HSSF
    //  NPOI.SS
    public class ExcelExporter
    {
        private const string fileDirectory = "Excel";
        private readonly string emptyFile = GetPath("empty.xls");
        private readonly string summaryFile = GetPath("summary.xls");
        private readonly string cellFile = GetPath("cell.xls");

        static ExcelExporter()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileDirectory);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        private static string GetPath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileDirectory, fileName);
        }

        public ExcelExporter Export()
        {
            //1 创建工作簿
            //2 创建工作表
            //3 创建单元格

            //FileStream file = new FileStream("net.xls", FileMode.OpenOrCreate, FileAccess.Read);

            //HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            //file.Close();
            //ICellStyle style = hssfworkbook.CreateCellStyle();
            //style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //style.VerticalAlignment = VerticalAlignment.Center;
            //style.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
            //style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.ShrinkToFit = true;
            //style.WrapText = true;

            //NPOI.SS.UserModel.IFont sheetfont = hssfworkbook.CreateFont();
            ////font.Boldweight = (short)FontBoldWeight.BOLD;
            //sheetfont.FontName = ("宋体");
            //sheetfont.FontHeightInPoints = 9;// 22;
            //style.SetFont(sheetfont);

            //ISheet sheet1 = hssfworkbook.GetSheet("hello oha!");


            return this;
        }

        private static HSSFWorkbook CreateWorkbooWithSheet()
        {
            var workbook = new HSSFWorkbook();
            workbook.CreateSheet("sheet1");
            workbook.CreateSheet("sheet2");
            workbook.CreateSheet("sheet3");
            workbook.CreateSheet("sheet4");
            return workbook;
        }

        private void Save(HSSFWorkbook workbook, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fs);
            }
        }

        #region 创建Excel
        public ExcelExporter CreaterExcel()
        {
            HSSFWorkbook workbook = CreateWorkbooWithSheet();

            Save(workbook, this.emptyFile);
            return this.CreateSummary();
        }
        #endregion

        #region 创建摘要
        /// <summary>
        /// Ref: DocummentSummaryInformation - https://docs.microsoft.com/zh-cn/windows/desktop/Stg/the-documentsummaryinformation-and-userdefined-property-sets
        /// Ref: SummaryInformation - https://docs.microsoft.com/zh-cn/windows/desktop/Msi/manage-summary-information
        /// </summary>
        /// <returns></returns>
        public ExcelExporter CreateSummary()
        {
            //OLE2格式，都可以拥有这两个头信息

            var workbook = CreateWorkbooWithSheet();

            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Corp";
            var si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI Excel";
            workbook.DocumentSummaryInformation = dsi;
            workbook.SummaryInformation = si;

            Save(workbook, this.summaryFile);

            return this.CreateCell();
        }
        #endregion

        #region 创建单元格
        public ExcelExporter CreateCell()
        {
            var workbook = CreateWorkbooWithSheet();
            var sheet = workbook.GetSheetAt(0);

            //var row1 = sheet.CreateRow(0);
            //var cell1 = row1.CreateCell(0);
            //cell1.SetCellValue("Hello World!");

            //Simple
            sheet.CreateRow(0).CreateCell(0).SetCellValue("Halo");
            sheet.GetRow(0).CreateCell(1).SetCellValue("Halo-Ha");

            Save(workbook, this.cellFile);
            return this;
        }
        #endregion
    }
}
