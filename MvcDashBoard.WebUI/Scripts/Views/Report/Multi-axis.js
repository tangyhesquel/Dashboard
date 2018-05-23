$(document).ready(function () {
    GetReport();
})
function   GetReport() {
    var XaxisDay = ["星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
    var BarData = [{ name: "Output;", data: [3200, 6300, 7400, 9100, 11700, 13800] }, { type: "line", data: [30, 38, 40, 32, 42, 60], name: "Equipment", color: "#0099CC", axis: "Equipment" }];
    var valueAxes = [{ visible: true, }, { name: "Equipment", color: "#ec5e0a" }];
    CreateLineBandard("#multi-axis", "Multi-axis Charts", XaxisDay, BarData, valueAxes);

}
//Windows事件触发调用方法
window.onresize = function () {
    ShowHeight();
  
};




