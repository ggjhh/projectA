using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVC4cjlee.Web
{
    /// <summary>
    /// 서비스 모드
    /// </summary>
    public enum ServiceMode
    {
        Development,            // 개발
        Test,               // 테스트
        Staging,            // 스테이징
        Release             // 운영
    }

    /// <summary>
    /// 전역 Static Class
    /// </summary>
    public class Global
    {
        /// <summary>
        /// 시스템 운영 환경
        /// </summary>
        public static ServiceMode ServiceMode
        {
            get
            {
                #if(TEST)
                    ServiceMode retVal = ServiceMode.Test;
                #elif (STAGING)
                    ServiceMode retVal = ServiceMode.Staging;
                #elif (DEBUG)
                    ServiceMode retVal = ServiceMode.Development;
                #else
                    ServiceMode retVal = ServiceMode.Release;
                #endif
                return retVal;
            }
        }
    }
}