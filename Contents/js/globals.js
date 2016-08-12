/*
    정산시스템 전역 JS 라이브러리
*/
var WMP = WMP || {};

WMP.Validate = {
    IsNumeric: function (val) {
        var temp = new String(val);

        if (temp.search(/\D/) != -1) {
            return false;
        }
        return true;
    },

    IsNullOrEmpty: function (val) {
        if (val == null) {
            return true;
        }
        var strTemp = $.trim(val);
        strTemp = strTemp.replace(/\r\n/g, '');
        strTemp = strTemp.replace(/\n/g, '');

        if (strTemp == "") {
            return true;
        }
        return false;
    }
};