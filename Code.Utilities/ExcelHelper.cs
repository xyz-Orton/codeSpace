using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Data;

using Aspose.Cells;
using System.Drawing;

namespace CPTP.Utilities
{
    /// <summary>
    /// 导出文件助手类
    /// </summary>
    public class ExcelHelper
    {
        #region 导出Excel文件

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="dt">数据源Table</param>
        /// <param name="templateFile">模板文件</param>
        /// <param name="outputFileName">导出文件名</param>
        /// <param name="fileTitle">导出文件标题</param>
        /// <returns></returns>
        public static string OutputExcel(DataTable dt, string templateFile, string outputFileName, string fileTitle)
        {
            return OutputExcel(dt, templateFile, outputFileName, fileTitle, Encoding.UTF8, FileFormatType.Excel2003, null);
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="dt">数据源Table</param>
        /// <param name="templateFile">模板文件</param>
        /// <param name="outputFileName">导出文件名</param>
        /// <param name="fileTitle">导出文件标题</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static string OutputExcel(DataTable dt, string templateFile, string outputFileName, string fileTitle, string[,] param)
        {
            return OutputExcel(dt, templateFile, outputFileName, fileTitle, Encoding.UTF8, FileFormatType.Excel2003, param);
        }

        /// <summary>
        /// 导出Excel文件（指定编码格式与Excel版本）
        /// </summary>
        /// <param name="dt">数据源Table</param>
        /// <param name="templateFile">模板文件</param>
        /// <param name="outputFileName">导出文件名</param>
        /// <param name="fileTitle">导出文件标题</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="excelVersion">Excel版本</param>
        /// <returns></returns>
        public static string OutputExcel(DataTable dt, string templateFile, string outputFileName, string fileTitle, Encoding encoding, FileFormatType excelVersion, string[,] param)
        {
            if (dt.Rows.Count > 0)
            {
                //当模板文件里面设置的输出变量或函数不正确时，生成Excel容易产生异常。
                try
                {
                    dt.TableName = "ExcelTable";
                    WorkbookDesigner designer = new WorkbookDesigner();
                    designer.Open(PageHelper.GetMapPath(templateFile));
                    designer.SetDataSource(dt);
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length / 2; i++)
                        {
                            designer.SetDataSource(param.GetValue(i, 0).ToString(), param.GetValue(i, 1).ToString());
                        }
                    }
                    designer.SetDataSource("reportName", fileTitle);
                    designer.Process();
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Charset = encoding.ToString(); //设置了类型为中文防止乱码的出现 
                    //string fileName = String.Format("attachment;FileName={0} ", HttpUtility.UrlEncode(outputFileName, System.Text.Encoding.UTF8));
                    string fileName = HttpUtility.UrlEncode(outputFileName, System.Text.Encoding.UTF8);
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", fileName); //定义输出文件和文件名 
                    //HttpContext.Current.Response.ContentEncoding = Encoding.Default;
                    HttpContext.Current.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
                    designer.Save(fileName, SaveType.OpenInExcel, excelVersion, HttpContext.Current.Response);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.Close();
                    designer = null;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //HttpContext.Current.Response.End();
                    return "";
                }
                catch (Exception ex)
                {
                    return "Error : 很抱歉，当前操作发生异常错误！";// +ex.Message;
                }
            }
            else
            {
                return "<script type='text/javascript'>alert('暂无查询到符合条件的数据！');window.close();</script>";
            }
        }

        /// <summary>
        ///  保存工作簿为客户端文件
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="outputFileName">输出文件</param>
        public static string SaveExcelFile(Workbook workbook, string outputFileName)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = Encoding.UTF8.ToString(); //设置了类型为中文防止乱码的出现 
                //string fileName = String.Format("attachment;FileName={0} ", HttpUtility.UrlEncode(outputFileName, System.Text.Encoding.UTF8));
                string fileName = HttpUtility.UrlEncode(outputFileName, System.Text.Encoding.UTF8);
                HttpContext.Current.Response.AppendHeader("Content-Disposition", fileName); //定义输出文件和文件名 
                //HttpContext.Current.Response.ContentEncoding = Encoding.Default;
                HttpContext.Current.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
                workbook.Save(fileName, SaveType.OpenInExcel, FileFormatType.Excel2003, HttpContext.Current.Response);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();
                workbook = null; 
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                //HttpContext.Current.Response.End();
                return "";
            }
            catch (Exception ex)
            {
                return "Error : 很抱歉，当前操作发生异常错误！";// +ex.Message;
            }
        }

        #region 定义样式

        public static Style CreateCellStyle(CellStyleType cellStyleType)
        {
            Workbook workbook = new Workbook();
            Style style = null;
            switch (cellStyleType)
            {
                case CellStyleType.Title:
                    //样式-表头 
                    //为标题设置样式     
                    style = workbook.Styles[workbook.Styles.Add()];//新增样式 
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 20;//文字大小 
                    style.Font.IsBold = true;//粗体 
                    break;
                case CellStyleType.TH:
                    //样式-表头 
                    //为标题设置样式     
                    style = workbook.Styles[workbook.Styles.Add()];//新增样式 
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 11;//文字大小 
                    style.Font.IsBold = true;//粗体 
                    style.IsTextWrapped = true;//单元格内容自动换行 
                    style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                    style.Pattern = BackgroundType.Solid;   //设置背景色必需先设置背景类型
                    style.ForegroundColor = ColorTranslator.FromHtml("#969696");
                    break;
                case CellStyleType.TD:
                    //样式-表内容 
                    //为标题设置样式     
                    style = workbook.Styles[workbook.Styles.Add()];//新增样式 
                    style.HorizontalAlignment = TextAlignmentType.Right;//文字右对齐 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 9;//文字大小 
                    style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case CellStyleType.SumTotal:
                    //样式-表内容-总计行样式
                    //为标题设置样式     
                    style = workbook.Styles[workbook.Styles.Add()];//新增样式 
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 10;//文字大小 
                    style.Font.IsBold = true;
                    style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                    style.Pattern = BackgroundType.Solid;   //设置背景色必需先设置背景类型
                    style.ForegroundColor = ColorTranslator.FromHtml("#969696");
                    break;
                case CellStyleType.Total:
                    //样式-表内容-合计行样式
                    //为标题设置样式     
                    style = workbook.Styles[workbook.Styles.Add()];//新增样式 
                    style.HorizontalAlignment = TextAlignmentType.Right;//文字右对齐 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 9;//文字大小 
                    style.Font.IsBold = true;
                    style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                    style.Pattern = BackgroundType.Solid;   //设置背景色必需先设置背景类型
                    style.ForegroundColor = ColorTranslator.FromHtml("#CCCCFF");
                    break;
                case CellStyleType.SubTotal:
                    //样式-表内容-小计行样式
                    //为标题设置样式     
                    style = workbook.Styles[workbook.Styles.Add()];//新增样式 
                    style.HorizontalAlignment = TextAlignmentType.Right;//文字右对齐 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 9;//文字大小 
                    style.Font.IsBold = true;
                    style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                    style.Pattern = BackgroundType.Solid;   //设置背景色必需先设置背景类型
                    style.ForegroundColor = Color.Silver;
                    break;
            }
            return style;
        }

        #endregion 定义样式

        #region 填充Excel单元格数值

        #region 填充String类型数值

        /// <summary>
        /// 填充Excel单元格数值
        /// </summary>
        /// <param name="cells">表格对象</param>
        /// <param name="rowIndex">行号（从0开始）</param>
        /// <param name="colIndex">列号（从0开始）</param>
        /// <param name="cellContent">单元格内容</param>
        /// <param name="style">应用样式</param>
        public static void PutExcelCell(Cells cells, int rowIndex, int colIndex, string cellContent)
        {
            PutExcelCell(cells, rowIndex, colIndex, cells.GetColumnWidth(colIndex), cellContent, cells[rowIndex, colIndex].Style);
        }

        /// <summary>
        /// 填充Excel单元格数值
        /// </summary>
        /// <param name="cells">表格对象</param>
        /// <param name="rowIndex">行号（从0开始）</param>
        /// <param name="colIndex">列号（从0开始）</param>
        /// <param name="cellContent">单元格内容</param>
        /// <param name="style">应用样式</param>
        public static void PutExcelCell(Cells cells, int rowIndex, int colIndex, string cellContent, Style style)
        {
            PutExcelCell(cells, rowIndex, colIndex, cells.GetColumnWidth(colIndex), cellContent, style);
        }

        /// <summary>
        /// 填充Excel单元格数值
        /// </summary>
        /// <param name="cells">表格对象</param>
        /// <param name="rowIndex">行号（从0开始）</param>
        /// <param name="colIndex">列号（从0开始）</param>
        /// <param name="colWidth">列宽</param>
        /// <param name="cellContent">单元格内容</param>
        /// <param name="style">应用样式</param>
        public static void PutExcelCell(Cells cells, int rowIndex, int colIndex, double colWidth, string cellContent, Style style)
        {
            cells[rowIndex, colIndex].PutValue(cellContent);
            cells.SetColumnWidth(colIndex, colWidth);
            cells[rowIndex, colIndex].SetStyle(style);
        }

        #endregion 填充String类型数值

        #region 填充Int32类型数值

        /// <summary>
        /// 填充Excel单元格数值
        /// </summary>
        /// <param name="cells">表格对象</param>
        /// <param name="rowIndex">行号（从0开始）</param>
        /// <param name="colIndex">列号（从0开始）</param>
        /// <param name="cellContent">单元格内容</param>
        /// <param name="style">应用样式</param>
        public static void PutExcelCell(Cells cells, int rowIndex, int colIndex, Int32 cellContent)
        {
            PutExcelCell(cells, rowIndex, colIndex, cells.GetColumnWidth(colIndex), cellContent, cells[rowIndex, colIndex].Style);
        }

        /// <summary>
        /// 填充Excel单元格数值
        /// </summary>
        /// <param name="cells">表格对象</param>
        /// <param name="rowIndex">行号（从0开始）</param>
        /// <param name="colIndex">列号（从0开始）</param>
        /// <param name="cellContent">单元格内容</param>
        /// <param name="style">应用样式</param>
        public static void PutExcelCell(Cells cells, int rowIndex, int colIndex, Int32 cellContent, Style style)
        {
            PutExcelCell(cells, rowIndex, colIndex, cells.GetColumnWidth(colIndex), cellContent, style);
        }

        /// <summary>
        /// 填充Excel单元格数值
        /// </summary>
        /// <param name="cells">表格对象</param>
        /// <param name="rowIndex">行号（从0开始）</param>
        /// <param name="colIndex">列号（从0开始）</param>
        /// <param name="colWidth">列宽</param>
        /// <param name="cellContent">单元格内容</param>
        /// <param name="style">应用样式</param>
        public static void PutExcelCell(Cells cells, int rowIndex, int colIndex, double colWidth, Int32 cellContent, Style style)
        {
            cells[rowIndex, colIndex].PutValue(cellContent);
            cells.SetColumnWidth(colIndex, colWidth);
            cells[rowIndex, colIndex].SetStyle(style);
        }

        #endregion 填充Int32类型数值

        #endregion 填充Excel单元格数值

        #endregion 导出Excel文件
    }
}
