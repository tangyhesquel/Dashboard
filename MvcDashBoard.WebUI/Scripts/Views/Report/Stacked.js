$(document).ready(function () {
    GetReport();
})
function   GetReport() {
    var XaxisDay = ["01日", "02日", "03日", "04日", "05日", "06日", "07日"];
    var BarData = [{ name: "Bar_A", data: [20, 24, 22, 31, 29, 26, 14] },
                   { name: "Bar_B", data: [31, 45, 20, 25, 23, 41, 38] },
                   { name: "Bar_C", data: [11, 25, 10, 45, 13, 12, 28] }];
    CreateStackBar("#stacked", "Stacked Bar Charts", '台', XaxisDay, BarData)
   
  
}
//Windows事件触发调用方法
window.onresize = function () {
    ShowHeight();
  
};




