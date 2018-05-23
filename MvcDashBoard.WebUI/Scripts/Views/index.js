
var Control = "report/"
var DataSource;
var AlertTimer;
var Times;
//Ajax 连接超时5分钟
$.ajaxSetup({ timeout: 180000, cache: false });
//开启警报
function Runalert() {
    if ($("#alert").is(':hidden')) {
        AlertTimer = setInterval(function () {
            $("#alert").fadeOut(200).fadeIn(200);
        }, 500);
    }
}
//关闭警报
function Closealert() {
    $("#alert").hide();
    if (AlertTimer != undefined) {
        clearInterval(AlertTimer);//
    };
    $("#alert").hide();

}
//启动默认事件
$(document).ready(function () {
    ShowHeight();
    $("#Menu").kendoMenu({
        //orientation: "vertical"  /*纵向布局*/
    });
    $("#MainPanel").kendoSplitter({                    /*打开url*/
        panes: [{ contentUrl: "./report/Default.html" }]
    });
    showtime("showtime", 1000);
})

function Goto_SetUrl(path) {
    var Conte = $("#MainPanel").data("kendoSplitter");
    Conte.ajaxRequest("#Conte", path);
}

//Windows事件触发调用方法
window.onresize = function () {
    ShowHeight();
};

