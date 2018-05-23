/**-------------全局变量-------------------*/
var NameCode = "";  //员工资料Code供员工资料弹出框体生成调用

/*--------------------50%高度-----------------------*/
function ShowHeight() {
                        var WinHeiht = ($(window).height());//当前屏幕图形展现高度
                        var ShowDivHEight = WinHeiht - $("#Menu").height()-4;
                        //$("#" + id).height(ShowHeight)
                        $(".FullHeight").height(ShowDivHEight);
                        $(".HalfHeight").height(ShowDivHEight * 0.5 - 5);
                       }
/*--------------------30%高度-----------------------*/
function MessageeHeight() {
    var WinHeiht = ($(window).height());//当前屏幕图形展现高度
    var ShowDivHeight = WinHeiht - $("#Menu").height() - 4;
    $(".FullHeight").height(ShowDivHeight);\
    $(".P3Height").height(ShowDivHeight / 3 );
}
