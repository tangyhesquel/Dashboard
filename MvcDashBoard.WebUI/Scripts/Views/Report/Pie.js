//加载默认值 

$(document).ready(function () {
    GetReport();
})

function   GetReport() {
    datasource= [{
        category: "GLE",
        value: 340
    }, {
        category: "GET",
        value: 540
    }, {
        category: "GEW",
        value: 1120
    }, {
        category: "GES",
        value: 2100
    }, {
        category: "GLG",
        value: 860
    }, {
        category: "GEG",
        value: 720
    }]

    CreatePie("#pie", "Pie Charts", '人', datasource, "value", "category");
  //  function (ID, Title, Unit, datasource, field, categoryField
   // AsynRmoteCreateBarChart();
}

function AsynRmoteCreatePieChart() {
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




