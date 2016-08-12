using System;
using System.Web;
using Newtonsoft.Json;

namespace MVC4cjlee.Models
{
    ///================================================================
    /// <summary>
    /// API Response
    /// </summary>
    ///================================================================
    public class ApiResponse
    {
        #region Variable
        public int     error_code       { get; set; }
        public string  error_message    { get; set; }
        public dynamic return_data      { get; set; }

        public const string ERR_MSG_ACCESS_DENIED       = "Access denied ";
        public const string ERR_MSG_OTHERS              = "Unexpected error occurred: ";

        public const int    ERR_CODE_FORM               = 100;        
        public const int    ERR_CODE_CATCH_EXCEPTION    = 900;

        public const int    DEFAULT_START_NO            = 0;
        public const int    DEFAULT_PAGE_NO             = 1;
        public const int    DEFAULT_PAGE_SIZE           = 20;
        public const int    DEFAULT_MAX_DATE            = 31;
        
        public ApiResponse()
        {
            error_code    = 0;
            error_message = "OK";
            return_data   = null;
        }
        #endregion
        
        ///================================================================
        /// <summary>
        /// JSON으로 파싱(에러코드 및 메시지 입력)
        /// </summary>
        /// <param name="intErrCode">에러코드 <c>==0:Success,  &lt;&gt;0:fail</c></param>
        /// <param name="strErrorMsg">에러메시지</param>
        /// <param name="objResponseData">상세데이터</param>
        ///================================================================
        public static void convertJson(int intErrCode, string strErrorMsg, object objResponseData)
        {
            ApiResponse objApiResult  = null;
            string      strJsonResult = string.Empty;

            objApiResult = new ApiResponse();
            if (intErrCode.Equals(0))
            {
                objApiResult.return_data = objResponseData;
            }
            else
            {
                objApiResult.error_code    = intErrCode;
                objApiResult.error_message = strErrorMsg;
                objApiResult.return_data   = null;
            }
            convertJson(objApiResult);
        }

        ///================================================================
        /// <summary>
        /// JSON으로 파싱(에러코드 및 메시지 입력)
        /// </summary>
        /// <param name="intErrCode">에러코드 <c>==0:Success,  &lt;&gt;0:fail</c></param>
        /// <param name="strErrorMsg">에러메시지</param>
        /// <param name="intPageNum">현재 페이지 번호</param>
        /// <param name="intPageSize">한 화면에 보여질 페이지 수</param>
        /// <param name="intTotalRowCnt">페이징용 전체 행의 수</param>
        /// <param name="objResponseData">상세데이터</param>
        ///================================================================
        public static void convertJson(int intErrCode, string strErrorMsg, int intStartNum,int intPageNum, int intPageSize, int intTotalRowCnt, object objResponseData)
        {
            ApiResponse objApiResult = null;
            ApiPagingList objList = null;
            string strJsonResult = string.Empty;

            objApiResult = new ApiResponse();
            objList = new ApiPagingList ();
            if (intErrCode.Equals(0))
            {
                objList.start_num        = intStartNum;
                objList.end_num          = intPageNum;
                objList.page_num         = intPageNum;
                objList.page_size        = intPageSize;
                objList.total_row_cnt    = intTotalRowCnt;
                objList.list             = objResponseData;
                objApiResult.return_data = objList;
            }
            else
            {
                objApiResult.error_code = intErrCode;
                objApiResult.error_message = strErrorMsg;
                objApiResult.return_data = null;
            }
            convertJson(objApiResult);
        }
                
        ///================================================================
        /// <summary>
        /// JSON으로 파싱
        /// </summary>
        /// <param name="objResponseData">파싱할 데이터</param>
        ///================================================================
        public static void convertJson(object objResponseData)
        {
            string strJsonResult = string.Empty;
            if (objResponseData != null)
            {
                strJsonResult = JsonConvert.SerializeObject(objResponseData);
            }
            objResponseData = null;

            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Write(strJsonResult);
            HttpContext.Current.Response.End();
        }

        ///================================================================
        /// <summary>
        /// API 공통 View page
        /// </summary>
        ///================================================================
        public static string setApiCommonView()
        {
            return "~/Views/Api/Index.cshtml";
        }
    }

    ///================================================================
    /// <summary>
    /// 페이징 처리가 필요한 API
    /// </summary>
    ///================================================================
    public class ApiPagingList
    {
        #region
        public int     start_num { get; set; }
        public int     end_num { get; set; }
        public int     page_num         { get; set; }
        public int     page_size        { get; set; }
        public int     total_row_cnt    { get; set; }
        public dynamic list             { get; set; }
        #endregion

        public ApiPagingList()
        {
            start_num     = 0;
            end_num       = 0;
            page_num      = 0;
            page_size     = 0;
            total_row_cnt = 0;
            list          = null;
        }
    }
}