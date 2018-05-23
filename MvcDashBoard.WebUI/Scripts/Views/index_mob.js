var count = 1;
var Control = "report/"
var DataSource;
var RepoertName;
function GetReport() {//OEE_Runatio.sql  MachineStatus
    var data = { ReportName: RepoertName };
      $.get(APIUrl+Control, data, function (Data) {
    //$.get("http://localhost:8881/ReportService.asmx/GetReport", data, function (Data) {      
      
         if (Data.success== true)
         {           
             DataSource = Data.ReportTables;
             console.log(RepoertName);
         }
         else
         {
             alert("数据读取错误！" + Data);
         }
     }).error(function (data) { alert("---数据读取错误！"); }

     )
    
   
}
function AutoGetReport( Time) {
    if (Time > 0) {
       setInterval("GetReport()", Time);
     
    }
    else {
      GetReport();
        //console.log("vv:" + dataSource);
    }
}


$(document).ready(function () {


    //$("#Menu").kendoMenu({
    //    //orientation: "vertical"  /*纵向布局*/
    //});
    //$("#MainPanel").kendoSplitter({                    /*打开url*/
    //   panes: [{ contentUrl: "./report/Quality.html" }]
    //});
    ShowHeight();
    //alert(document.body.clientHeight);
    //alert($('#Menu').height());
    //alert($('#MainPanel').height());
    //alert($('#Conte').height());
})


function Goto_Url(path) {
    alert(path);
    window.location.href = path;
}


//Windows事件触发调用方法
window.onresize = function () {
    ShowHeight();
};

function ShowHeight() {
    var WinHeiht = ($(window).height());//当前屏幕图形展现高度
    var ShowDivHEight = WinHeiht - $("#Menu").height() - 4;
    //$("#" + id).height(ShowHeight)
    $(".FullHeight").height(ShowDivHEight+20);
    $(".HalfHeight").height(ShowDivHEight * 0.5 - 8);
}

