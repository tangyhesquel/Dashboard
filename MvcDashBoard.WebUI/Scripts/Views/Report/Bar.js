//加载默认值 

$(document).ready(function () {
    GetReport();
})

function   GetReport() {
    var XaxisDay = ["01日", "02日", "03日", "04日", "05日", "06日", "07日"];
    var BarData = [{ name: "Bar_A", data: [20, 24, 22, 31, 29, 26, 14] },
                   { name: "Bar_B", data: [31, 45, 20, 25, 23, 41, 38] },
                   { name: "Bar_C", data: [26, 36, 20, 34, 26, 29, 19] }];
    CreateCol("#bar", "Bar Charts", '台', XaxisDay, BarData)
   // AsynRmoteCreateBarChart();
}

function AsynRmoteCreateBarChart() {
    var data = { ReportName: "AdjFPY" };
    $.get(APIUrl + Control, data, function (Data) {
        DataSource = Data.ReportTables;
        var AdjData = DataSource.ds;//绑定异步返回数据集
        var XaxisDay = [];
        var linesAdj = [];
        var linesFPY = [];
        var line1 = { name: '交机数', data: [] };
        var line2 = { name: '调机FPY', data: [] };
        $.each(AdjData, function (k, v) {    //绑定后台输出的集合
            XaxisDay.push(v.DataDate);                         //指定输出的列值绑定到变量中
            line1.data.push(v.HandNo)
            line2.data.push(v.Ratio)
            //CreateLine.
        });
        linesAdj.push(line1)
        CreateCol("#bar", "针织部圆机当月交机量", '台', XaxisDay, linesAdj)
    });
}
//Windows事件触发调用方法
window.onresize = function () {
    ShowHeight();
  
};




