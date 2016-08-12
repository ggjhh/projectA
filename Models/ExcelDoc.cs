using System;
using System.Data;
using System.Web;
using System.Text;
using ClosedXML.Excel;

///================================================================
/// <summary>
/// FileName        : ExcelDoc.cs
/// Description     : 서버의 엑셀문서 가져와서 특정 셀에 데이터 넣기
/// Author          : cjlee@wemakeprice.com, 2016-02-06
/// Modify History  : Just Created.
/// </summary>
///================================================================
namespace MVC4cjlee.Models
{
    ///================================================================
    /// <summary>
    /// API Response
    /// </summary>
    ///================================================================
    public class ExcelDoc
    {
        #region Make Excel Document
        ///================================================================
        /// <summary>
        /// Load ExcelDoc file and insert database data
        /// </summary>
        ///================================================================
        public void ExcelDoc_Export(string strExcelDocSave)
        {
            string strExcelDocReadFile = ""; //엑셀 파일 경로
            string strSaveFileName = "";
            try
            {
                var workbook = new XLWorkbook(strExcelDocReadFile);  // 기존 엑셀 열기 
                var worksheet = workbook.Worksheet(1);               // 첫번째 sheet열기 
                strSaveFileName = string.Concat("정산내역서_", DateTime.Now.ToShortDateString()); //파일명 설정

                //각 시트별로 보호 및 프린트 설정 가능함
                worksheet.Protect("X912_KD91K@3406!L1YD#21X912_KD91K@3406!L1YD#21SX912_KD91K@3406!L1YD#21SS"); //엑셀 시트 보호하기(편집막기)

                #region 프린트 설정
                worksheet.PageSetup.PaperSize       = XLPaperSize.A4Paper;         //인쇄 사이즈 설정
                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape; //가로로 인쇄
                worksheet.PageSetup.Margins.Top     = 0.1;                         //인쇄여백설정
                worksheet.PageSetup.Margins.Bottom  = 0.1;
                worksheet.PageSetup.Margins.Left    = 0.1;
                worksheet.PageSetup.Margins.Right   = 0.1;
                worksheet.PageSetup.Margins.Footer  = 0;
                worksheet.PageSetup.Margins.Header  = 0;
                worksheet.PageSetup.FitToPages(1, 0);                               //한페이지에 모든 열 맞추기 Fit all columns on one page
                #endregion

                #region 데이터 입력
                int intStartIndex = 24;
                int intRowCnt = 0;                
                worksheet.Cell("I9").Value = "";    //데이터 입력하기
                worksheet.Row(intStartIndex).InsertRowsBelow(intRowCnt - 1);  //빈 Row 추가하기

                worksheet.Range(string.Concat("A", intStartIndex.ToString(), ":D", intStartIndex.ToString())).Style.Fill.BackgroundColor = XLColor.LightGray; //색상

                worksheet.Cell("B" + intStartIndex.ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //정렬 변경하기
                #endregion
                                
                #region 엑셀 문서 작성하기
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(strSaveFileName + ".xlsx", Encoding.GetEncoding("UTF-8")).Replace("+", "%20") + "\"");

                using (System.IO.MemoryStream MyMemoryStream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
                #endregion
            }
            catch
            {
                HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.NoContent; //HTTPCode 200이 아닌 값 세팅
            }
        }
        #endregion
    }
}