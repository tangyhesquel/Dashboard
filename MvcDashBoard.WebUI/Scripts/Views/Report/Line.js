//加载默认值 
$(document).ready(function () {  
    ShowHeight();
    GetReport();
})

function GetReport() {

    var XaxisDay = ["01日", "02日", "03日", "04日", "05日", "06日", "07日"];
    var linesData = [{ name: "Line_A", data: [38.3, 34.9, 30.3, 29.8, 43.6, 31.1, 32.9] },
                     { name: "Line_B", data: [31.3, 45.9, 20.6, 25.8, 23.6, 41.1, 36.9] },
                     { name: "Line_C", data: [35.3, 43.9, 24.6, 27.8, 33.6, 38.1, 34.9] },
                     { name: "Line_D", data: [33.3, 44.9, 27.6, 28.8, 29.6, 35.1, 38.9] },
                     { name: "Line_E", data: [33.3, 44.9, 27.6, 28.8, 29.6, 35.1, 38.9] }];
    CreateLine("#line", "Line Charts", true, '元', 10, XaxisDay, linesData) //生成日数据折线图      
    //AsynRmoteCreateLineChart();从接口拿数据
    Closealert();     

}
  /*异步方式获取数据生成line图*/
function AsynRmoteCreateLineChart()
{
  
    var data = { ReportName: "OEERun" };
    $.get(APIUrl + Control, data, function (Data) {
        if (Data.success == true) {
            layer.closeAll(); //关闭弹层 //关闭弹层
            DataSource = Data.ReportTables;
            var OEERunData = DataSource.ds;//绑定异步返回数据集
            var XaxisDay = [];
            var linesOEE = [];
            var linesRun = [];
            var OEE = { name: 'OEE', data: [] };
            var Run = { name: '时间开动率', data: [] };
            $.each(OEERunData, function (k, v) {    //绑定后台输出的集合                                      
                XaxisDay.push(v.DataDate);                         //指定输出的列值绑定到变量中
                OEE.data.push(v.OEE)
                Run.data.push(v.RunRatio)
            });
            linesOEE.push(OEE)               
            CreateLine("#line", "Line Charts", false, '%', 20, XaxisDay, linesOEE) //生成日数据折线图               
            Closealert();
        }
        else {
            layer.closeAll(); //关闭弹层
            time = kendo.toString(new Date(), "yyyy/MM/dd HH:mm:ss");
            $("#MemoList").append("<li class=\"get_error\">" + time + " " + RepoertCaption + "读取失败</li>")
            Runalert();
        }
    })
}

//Windows事件触发调用方法
window.onresize = function () {
    ShowHeight();
};



