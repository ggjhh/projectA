using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace MVC4cjlee.Utility
{
    public class StringValue : System.Attribute
    {
        private string _value, _code;

        public StringValue(string value, string code)
        {
            _value = value;
            _code = code;
        }

        public string Value
        {
            get { return _value; }
        }

        public string Code
        {
            get { return _code; }
        }
    }

    public static class cEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;

            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }

        public static string GetCodeValue(Enum value)
        {
            string output = null;

            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Code;
            }

            return output;
        }


    }
    
    public static class DBCommon
    {
        //DB 연결자 리턴
        public static string DBBConnMsStr()
        {
            string sSrv = "mssql" + MVC4cjlee.Web.Global.ServiceMode.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings[sSrv].ConnectionString;
            return connectionString;
        }

        public static string DBBConnMyStr()
        {
            string sSrv = "mysql" + MVC4cjlee.Web.Global.ServiceMode.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings[sSrv].ConnectionString;
            return connectionString;
        }
    }

    #region DropDownList
    public static class CommonDDL
    {
        public enum ePerPage
        {
            [Display(Name = "20개씩보기")]
            twenty = 20,
            [Display(Name = "30개씩보기")]
            thirty = 30,
            [Display(Name = "40개씩보기")]
            fourty = 40,
            [Display(Name = "50개씩보기")]
            fifty = 50,
            [Display(Name = "100개씩보기")]
            hundred = 100,
            [Display(Name = "150개씩보기")]
            hundredfifty = 150
        }
        
        #region DB 데이터를 DropDownList 바인딩
        public static List<System.Web.Mvc.SelectListItem> getCodeList(DataTable ojbDT, bool isSelectDefault = false)
        {
            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();
            items.Clear();
            if(isSelectDefault)
            {
                items.Add(new System.Web.Mvc.SelectListItem { Text = "-선택-", Value = "" });
            }
            for (int intRowCount = 0; intRowCount <= ojbDT.Rows.Count - 1; intRowCount++)
            {
                items.Add(new System.Web.Mvc.SelectListItem { Text = ojbDT.Rows[intRowCount]["CODE_NAME"].ToString(), Value = ojbDT.Rows[intRowCount]["CODE_SEQ"].ToString() });
            }
            return items;
        }
        #endregion
    }
    #endregion
}