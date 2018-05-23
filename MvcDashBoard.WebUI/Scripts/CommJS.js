var APIUrl = "http://192.168.99.18:8888/api/";
//var APIUrl = "http://devazure.esquel.cn/KnCCSDashBoard/WebAPI/api/";
//var APIUrl = "http://localhost:6500/api/";
function showtime(obj,rate) {
    ss=new Date().format("yyyy-MM-dd EEE HH:mm:ss");
    $("#"+obj).html(ss);
    setTimeout("showtime('" + obj + "');", rate); //设定函数自动执行时间为 1000 ms(1 s)
}
   
/**      
 * 对Date的扩展，将 Date 转化为指定格式的String      
 * 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符      
 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)      
 * eg:      
 * (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423      
 * (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04      
 * (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04      
 * (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04      
 * (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18      
 */        
Date.prototype.format=function(fmt) {         
    var o = {         
        "M+" : this.getMonth()+1, //月份         
        "d+" : this.getDate(), //日         
        "h+" : this.getHours()%12 == 0 ? 12 : this.getHours()%12, //小时         
        "H+" : this.getHours(), //小时         
        "m+" : this.getMinutes(), //分         
        "s+" : this.getSeconds(), //秒         
        "q+" : Math.floor((this.getMonth()+3)/3), //季度         
        "S" : this.getMilliseconds() //毫秒         
    };      
    var week = {         
        "0" : "日",         
        "1" : "一",         
        "2" : "二",         
        "3" : "三",         
        "4" : "四",         
        "5" : "五",         
        "6" : "六"        
    };         
    if(/(y+)/.test(fmt)){         
        fmt=fmt.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length));         
    }         
    if(/(E+)/.test(fmt)){         
        fmt=fmt.replace(RegExp.$1, ((RegExp.$1.length>1) ? (RegExp.$1.length>2 ? "星期" : "周") : "")+week[this.getDay()+""]);         
    }         
    for(var k in o){         
        if(new RegExp("("+ k +")").test(fmt)){         
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length)));         
        }         
    }         
    return fmt;         
}


//背景颜色闪烁效果
jQuery.fn.flash = function (color, duration) {
    var current = this.css('color');
    this.animate({ color: 'rgb(' + color + ')' }, duration / 2);
    this.animate({ color: current }, duration / 2);
}
// http://www.jb51.net Then use the above function as:
//$('#importantElement').flash('255,0,0', 1000);

     
//数据加载是显示弹出框
ReadData = function (msg, closetime) {
    if (closetime == undefined) {
        closetime = 0;
    }
    var layermsg = layer.open({
        shadeClose: false,
        type: 2,
        time: closetime,
        content: msg
    });
};