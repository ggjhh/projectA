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
    }
}