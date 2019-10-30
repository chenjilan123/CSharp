﻿using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
        private readonly string commentFile = GetPath("comment.xls");
        private readonly string cellFormatFile = GetPath("cell format.xls");

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

        #region xls
        #region 创建工作表
        private static HSSFWorkbook CreateWorkbooWithSheet()
        {
            var workbook = new HSSFWorkbook();
            workbook.CreateSheet("sheet1");
            workbook.CreateSheet("sheet2");
            workbook.CreateSheet("sheet3");
            workbook.CreateSheet("sheet4");
            return workbook;
        }
        #endregion

        #region 保存
        private void Save(HSSFWorkbook workbook, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fs);
            }
        }
        #endregion

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
            return this.CreateComment();
        }
        #endregion

        #region 创建批注
        /// <summary>
        /// 创建批注：
        /// 位置: HSSFClientAnchor - dx1, dy1, dx2, dy2
        /// 大小: HSSFClientAnchor - col1, row1, col2, row2
        /// 文本: IComment.String
        /// 作者: IComment.Author
        /// </summary>
        /// <returns></returns>
        public ExcelExporter CreateComment()
        {
            var workbook = CreateWorkbooWithSheet();

            var sheet = workbook.GetSheetAt(0);

            var patr = sheet.CreateDrawingPatriarch();
            var anchor = new HSSFClientAnchor(0, 0, 0, 0, 1, 2, 4, 4);
            var comment = patr.CreateCellComment(anchor);
            comment.String = new HSSFRichTextString("NPOI Demo string");
            comment.Author = "NPOI Demo Author";
            comment.Visible = true; //Default: false
            var cell = sheet.CreateRow(1).CreateCell(1);
            cell.CellComment = comment;

            Save(workbook, this.commentFile);
            return this.SetCellFormat();
        }
        #endregion

        #region 设置单元格格式
        public ExcelExporter SetCellFormat()
        {
            var workbook = CreateWorkbooWithSheet();


            Save(workbook, this.cellFormatFile);
            return this;
        }
        #endregion
        #endregion

        #region xlsx
        public void CreateWorkBook()
        {
            //IWorkbook workbook = new XSSFWorkbook();
            //workbook.CreateSheet("Sheet A1");
            //workbook.CreateSheet("Sheet A2");
            //workbook.CreateSheet("Sheet A3");
            //var sheet = workbook.GetSheetAt(0);
            ////sheet.SetDefaultColumnStyle();
            //IRow row = sheet.CreateRow(5);
            //ICell cell = row.CreateCell(5);
            //cell.SetCellValue("hello");

            //FileStream sw = File.Create("test.xlsx");
            //workbook.Write(sw);
            //sw.Close();
            //return;

            //var workbook1 = new XSSFWorkbook("报警明细.xls");
            ////using (var fs = new FileStream("车辆不在线明细.xlsx", FileMode.OpenOrCreate, FileAccess.Write))
            ////{
            ////    workbook1.Write(fs);
            ////}
            //FileStream sw = File.Create("test.xlsx");
            //workbook1.Write(sw);
            //sw.Close();
            ////workbook1.Close();

            //SetCellValuesInXlsx();
            //return;

            var files = Directory.GetFiles("xslxTemplate");
            foreach (var filePath in files)
            {
                if (!File.Exists(filePath))
                {
                    continue;
                }

                //var file = new FileInfo(filePath);
                //var modePath = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite);
                //IWorkbook workbook = new XSSFWorkbook(filePath);
                //IWorkbook workbook = new XSSFWorkbook(modePath);
                //var sheet = workbook.GetSheetAt(0);
                //sheet.SetDefaultColumnStyle()
                //IRow row = sheet.CreateRow(5);
                //ICell cell = row.CreateCell(5);
                //cell.SetCellValue("hello");

                //var sheetName = "hehe";
                //ISheet outputSheet = workbook?.GetSheet(sheetName) ?? workbook?.CreateSheet(sheetName);

                IWorkbook workbook;
                using (var fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    workbook = WorkbookFactory.Create(fs);
                    fs.Close();
                }

                //var sheetName = "hehe";
                //ISheet outputSheet = workbook?.GetSheet(sheetName) ?? workbook?.CreateSheet(sheetName);

                //ISheet s1 = workbook.GetSheetAt(2);
                //IRow row = s1.CreateRow(5);
                //ICell cell = row.CreateCell(5);
                //cell.SetCellValue("hello");

                ISheet s1 = workbook.GetSheetAt(3);
                //Console.WriteLine(s1.SheetName);

                var row = s1.GetRow(0);
                //导致错误
                row.
                row.HeightInPoints = 10;

                var sFile = $"Excel/{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.tmp";
                using (var fs = new FileStream(sFile, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                    fs.Close();
                }

                File.Move(sFile, sFile.Substring(0, sFile.Length - 4) + ".xlsx");
            }
        }

        void SetCellValuesInXlsx()
        {
            //IWorkbook workbook = new XSSFWorkbook();
            //ISheet sheet1 = workbook.CreateSheet("Sheet1");
            //sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
            //int x = 1;
            //for (int i = 1; i <= 15; i++)
            //{
            //    IRow row = sheet1.CreateRow(i);
            //    for (int j = 0; j < 15; j++)
            //    {
            //        row.CreateCell(j).SetCellValue(x++);
            //    }
            //}
            //FileStream sw = File.Create("test.xlsx");
            //workbook.Write(sw);
            //sw.Close();

            IWorkbook workbook = new XSSFWorkbook("test.xlsx");
            ISheet sheet1 = workbook.GetSheet("Sheet1");
            sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
            int x = 1;
            for (int i = 1; i <= 15; i++)
            {
                IRow row = sheet1.CreateRow(i);
                for (int j = 0; j < 15; j++)
                {
                    row.CreateCell(j).SetCellValue(x++);
                }
            }
            FileStream sw = File.Create("test1.xlsx");
            workbook.Write(sw);
            sw.Close();
        }
        #endregion
    }
}