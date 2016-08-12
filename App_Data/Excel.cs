using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using ClosedXML.Excel;
using Excel;

namespace MVC4cjlee.Utility
{
    /// <summary>
    /// Excel의 요약 설명입니다.
    /// </summary>
    public class Excel : ClosedXML.Excel.XLWorkbook
    {
        public Excel()
        {
        }

        /// <summary>
        /// 엑셀 시트를 추가
        /// </summary>
        /// <param name="objDT">엑셀 시트에 추가될 DataTable</param>
        /// <param name="objDictionary">엑셀 시트에 추가될 컬럼명 Dictionary</param>
        /// <param name="strWorkSheetName">시트명</param>
        public void Add(DataTable objDT, Dictionary<string, string> objDictionary, string strWorkSheetName)
        {
            objDT.TableName = strWorkSheetName;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\""
                                                 + HttpUtility.UrlEncode(strWorkSheetName + ".xlsx", Encoding.GetEncoding("UTF-8")).Replace("+", "%20") + "\"");

            string[] arrColumnName = new string[objDictionary.Count()];
            int intLoop = 0;

            foreach (KeyValuePair<string, string> kvp in objDictionary)
            {
                objDT.Columns[kvp.Key].ColumnName = kvp.Value;
                arrColumnName[intLoop] = kvp.Value;
                intLoop = intLoop + 1;
            }

            XLWorkbook objWorkbook = new XLWorkbook();
            objWorkbook.Worksheets.Add(objDT.DefaultView.ToTable(false, arrColumnName));
            objWorkbook.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            objWorkbook.Style.Font.Bold = true;

            //파일 저장
            using (System.IO.MemoryStream MyMemoryStream = new System.IO.MemoryStream())
            {
                objWorkbook.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 엑셀파일 다운로드
        /// </summary>
        /// <param name="objDT">엑셀 시트에 추가될 DataTable</param>
        /// <param name="objDictionary"></param>
        /// <param name="intRowNum"></param>
        /// <param name="intColumnNum"></param>
        public void Add(DataTable objDT, Dictionary<string, string> objDictionary, string strWorkSheetName, int intRowNum, int intColumnNum)
        {
            objDT.TableName = strWorkSheetName;
            string[] arrColumnName = new string[objDictionary.Count()];
            int intLoop = 0;

            foreach (KeyValuePair<string, string> kvp in objDictionary)
            {
                objDT.Columns[kvp.Key].ColumnName = kvp.Value;
                arrColumnName[intLoop] = kvp.Value;
                intLoop = intLoop + 1;
            }

            if (Worksheets.Count == 0)
            {
                Worksheets.Add(strWorkSheetName);
                Worksheets.Worksheet(strWorkSheetName).Cell(intRowNum, intColumnNum).InsertTable(objDT.DefaultView.ToTable(false, arrColumnName).AsEnumerable());
            }
            else
            {
                Worksheets.Worksheet(strWorkSheetName).Cell(intRowNum, intColumnNum).InsertTable(objDT.DefaultView.ToTable(false, arrColumnName).AsEnumerable());
            }

            //파일 저장
            using (System.IO.MemoryStream MyMemoryStream = new System.IO.MemoryStream())
            {
                SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 엑셀파일 다운로드
        /// </summary>
        /// <param name="objDT">엑셀 시트에 추가될 DataTable</param>
        /// <param name="strFileName">엑셀파일명</param>
        public static void ExportExcel(DataTable objDT, string strFileName)
        {
            //시트명은 31자까지 가능
            objDT.TableName = (strFileName.Length > 31) ? strFileName.Substring(0, 31) : strFileName;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\""
                                                 + HttpUtility.UrlEncode(strFileName + ".xlsx", Encoding.GetEncoding("UTF-8")).Replace("+", "%20") + "\"");

            XLWorkbook objWorkbook = new XLWorkbook();
            objWorkbook.Worksheets.Add(objDT);
            objWorkbook.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            objWorkbook.Style.Font.Bold = true;

            //파일 저장
            using (System.IO.MemoryStream MyMemoryStream = new System.IO.MemoryStream())
            {
                objWorkbook.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }


        /// <summary>
        /// 엑셀파일 다운로드
        /// </summary>
        /// <param name="objDT">엑셀 시트에 추가될 DataTable</param>
        /// <param name="strFileName">엑셀파일명</param>
        public void ModelToExcel(System.Web.UI.WebControls.GridView objGrid, string FileName, string strEncoding = "EUC-KR")
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            HttpContext.Current.Response.AddHeader("charset", strEncoding);
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            HttpContext.Current.Response.Write("<style>td.text{mso-number-format:\\@} td.date{mso-number-format:'mm\\/dd\\/yy'} td.percent{mso-number-format:Percent}</style>"); //셀서식
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(strEncoding); //한글 인코딩
            HttpContext.Current.Response.Charset = strEncoding;
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            objGrid.RenderControl(htw);

            if (objGrid.Rows.Count > 0)
            {
                HttpContext.Current.Response.Write(sw.ToString());
            }
            else
            {
                HttpContext.Current.Response.Write("<br /><br /><Table><tr><td colspan='10'>데이터가 없습니다.</td></tr></table>");
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 업로드된 파일을 DataSet으로 리턴
        /// </summary>
        /// <param name="upload">Request file</param>
        public DataSet ExcelToDataSet(HttpPostedFileBase upload)
        {
            DataSet result = null;
            System.IO.Stream stream = upload.InputStream;
            IExcelDataReader excelReader = null;

            #region setExcelReader
            if (upload.FileName.EndsWith(".xls"))
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (upload.FileName.EndsWith(".xlsx"))
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {
                return result;
            }
            #endregion

            excelReader.IsFirstRowAsColumnNames = true;
            result = excelReader.AsDataSet();
            excelReader.Close();
            excelReader = null;
            stream = null;

            return result;
        }

        public static DataTable ToDataTable<T>(IList<T> data)   //T is any generic type
        {
            System.ComponentModel.PropertyDescriptorCollection props = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));

            var table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                System.ComponentModel.PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            var values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static DataTable ToDataTable(System.Web.UI.WebControls.GridView grid)
        {
            DataTable objDT = new DataTable();
            if (grid.HeaderRow != null)
            {
                for (int i = 0; i < grid.HeaderRow.Cells.Count; i++)
                {
                    objDT.Columns.Add(grid.HeaderRow.Cells[i].Text);
                }
            }
            foreach (System.Web.UI.WebControls.GridViewRow row in grid.Rows)
            {
                DataRow dr;
                dr = objDT.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                objDT.Rows.Add(dr);
            }
            return objDT;
        }
    }
}