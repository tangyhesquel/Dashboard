function DashBoardShow_DataProcess(actual, target) {
    var alertflag = "N"
    if (actual == "")
        actual = 0;
    if (target == "")
        target = 0;
    if (parseFloat(actual) < parseFloat(target)) {
        alertflag = "Y";
    }

    var h = document.getElementById("TTR_TOTAL");
    if (alertflag == "Y") {

        h.getAttributeNode("style").value = "color:red";
    }
    else {
        h.getAttributeNode("style").value = "color:yellow";
    }

    totalqty("outqty", "#LBL_Total_Qty");
    totalqty("targetqty", "#LBL_Target_Qty");
    totalqty("defectqty", "#LBL_Defect_Qty");
    setInterval("Go_GETHRData()", HR_REFRESH_INTERVAL * 1000 * 60);
}


function RefreshData(factorycd, linecode, shiftcode, trxdate, only1line) {
    PROC_DASHBOARD_GET_PRODUCTION_QTY(factorycd, linecode);
    GETData(factorycd, linecode, shiftcode, trxdate);
}

function GETData(factorycd, linecode, shiftcode, trxdate) {
    kendo.ui.progress($("#loading"), true);
    $.ajax({
        async: false,
        dataType: 'json',
        type: 'POST',
        url: '../../DASHBOARD/DASHBOARDSHOWDATA/DASHBOARDSHOWInquiry',
        data: {
            FACTORY_CD: factorycd,
            LINE_CODE: linecode,
            SHIFT_CODE: shiftcode,
            TRX_DATE: trxdate,
            REFRESH: "Y"
        },
        success: function (data) {
            if (data.SUCCESS == true) {

                data1 = data.Data;
                if (data1.RUNNING_BASIC_INFORMATION.CHANGESHIFT == "Y") {
                    //reloadPage();
                    location.reload(true);
                    return;
                }

                $('#LBL_Line_2').html(data1.FN_DASHBOARD_SHOW_DATA_Result.line);
                $('#LBL_HeadCount_2').html(data1.FN_DASHBOARD_SHOW_DATA_Result.Actual_HeadCount + "/" + data1.FN_DASHBOARD_SHOW_DATA_Result.HeadCount);
                $('#LBL_GONO_2').html(data1.FN_DASHBOARD_SHOW_DATA_Result.GONO);
                $('#LBL_SAH_2').html(data1.FN_DASHBOARD_SHOW_DATA_Result.SAH);
                var actual = data1.FN_DASHBOARD_SHOW_DATA_Result.ACTUAL_QTY;
                var target = data1.FN_DASHBOARD_SHOW_DATA_Result.Target_QTY;
                $('#LBL_PI_PASS_FAIL_2').html(data1.FN_DASHBOARD_SHOW_DATA_Result.GOOD_GARMENT_QTY + "/" + data1.FN_DASHBOARD_SHOW_DATA_Result.WORKMANSHIP_DEFECT_QTY);

                $('#LBL_FPY_2').html(parseInt(data1.FN_DASHBOARD_SHOW_DATA_Result.Fpy) + "%");
                $('#LBL_EFFICIENCY_2').html(parseInt(data1.FN_DASHBOARD_SHOW_DATA_Result.Eff) + "%");

                if (DISPLAY_TARGET2_DEFECT_flag == "Y") {
                    $('#LBL_ACTUAL_TARGET_2').html(data1.FN_DASHBOARD_SHOW_DATA_Result.ACTUAL_QTY + "/" + data1.FN_DASHBOARD_SHOW_DATA_Result.Target_QTY2);
                }
                else {
                    $('#LBL_ACTUAL_TARGET_2').html(data1.FN_DASHBOARD_SHOW_DATA_Result.ACTUAL_QTY + "/" + data1.FN_DASHBOARD_SHOW_DATA_Result.Target_QTY);
                }

                $('#id_l1').html(getdate(data1.FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result.trsdate, 'S', 'yyyy-MM-dd', 'Y'));
                $('#id_l2').html(parseInt(data1.FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result.Fpy).toString() + '%');
                $('#id_l3').html(parseInt(data1.FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result.ACTUAL_QTY).toString());
                $('#id_l4').html(parseInt(data1.FN_DASHBOARD_SHOW_PREVIOUS_DATA_Result.Eff).toString() + '%');

                Table2Data(data1);
                DashBoardShow_DataProcess(actual, target);

                //debugger;

                var TD_DEFECT_TOP;
                var DefectList = data1.DEFECT_TOP;
                if ((data1.DEFECT_TOP == null) || (data1.DEFECT_TOP.length == 0)) {
                    $('#TD_DEFECT_NO_TOP').show;
                }
                else {
                    //for (var ii in DefectList)
                    // {
                    //     TD_DEFECT_TOP = 'TD_DEFECT_TOP_' + ii.toString();
                    //     $('#' + TD_DEFECT_TOP).html(DefectList[ii].DEFECT_GROUP + "/" + DefectList[ii].PART_NAME + "/" + DefectList[ii].QTY);
                    // }
                    $('#TD_DEFECT_NO_TOP').hidden;
                    $.each(data1.DEFECT_TOP, function (i, item) {
                        TD_DEFECT_TOP = 'TD_DEFECT_TOP_' + i.toString();
                        $('#' + TD_DEFECT_TOP).html(item.DEFECT_GROUP + "/" + item.PART_NAME + "/" + item.QTY);
                    });

                }

                //notification.show({
                //    title: MSG_INQ_OK_T,
                //    message: MSG_INQ_OK  //message: "查询完成！"
                //}, "upload-success");
            }
            else {
                notification.show({
                    title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                    message: MSG_INQ_FAILED + ":0380"  //message: "错误代码：0380"
                }, "error");
            }
            kendo.ui.progress($("#loading"), false);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            notification.show({
                title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                message: MSG_INQ_FAILED + ":0381-" + XMLHttpRequest.status  //message: "错误代码：0381"

            }, "error");
            kendo.ui.progress($("#loading"), false);
        }
    });
}

function Table2Data(data1) {
    var table2 = $("#TB_2");
    var table2row = table2.find("tr").length - 2;//去除第一行和最后一行，然后由于从0开始的所以-1-1
    var LBL_TIME;
    var LBL_QTY;
    var i_bak = 1;
    var currhours = new Date().getHours();

    var seq24oclock=0 ;
    for (var i = 1; i < table2row; i++) {
        var ss = data1.FN_DASHBOARD_TIME_INTERVAL_QTY_Result[i - 1];
        if (parseInt(ss.TIME_PERIOD.substr(0, 2)) == 0)
        {
            seq24oclock = ss.SEQ;//如果班次包括0点，加24比较  
            break;
        }
    }

    for (var i = 1; i < table2row; i++) {
        LBL_TIME = 'LBL_TIME_' + i.toString();
        LBL_QTY = 'LBL_QTY_' + i.toString();

        LBL_TARGET = 'LBL_TARGET_' + i.toString();
        LBL_DEFECTIVE = 'LBL_DEFECTIVE_' + i.toString();

        var ss = data1.FN_DASHBOARD_TIME_INTERVAL_QTY_Result[i - 1];

        $('#' + LBL_TIME).html(ss.TIME_PERIOD);
        $('#' + LBL_TARGET).html(ss.TARGET_QTY);

        if (data1.BASICINFORMATION_data.USE_LINE_TARGET2=="N") 
            { 
            //if (parseInt(ss.TIME_PERIOD.substr(0, 2)) <= parseInt(currhours))
            if ((parseInt(ss.TIME_PERIOD.substr(0, 2)) <= parseInt(currhours) && seq24oclock == 0) ||  //没有零点
                (parseInt(ss.TIME_PERIOD.substr(0, 2)) <= parseInt(currhours) + 24 && seq24oclock > 0 && seq24oclock > ss.SEQ) || //零点之前,加24比较
                (parseInt(ss.TIME_PERIOD.substr(0, 2)) <= parseInt(currhours) && seq24oclock > 0 && seq24oclock <= ss.SEQ) //零点之后，
                            )
                { 
                    $('#' + LBL_QTY).html(ss.ACTUAL_QTY);
                    $('#' + LBL_DEFECTIVE).html(ss.DEFECT_QTY);
                    i_bak = i;
                }
            }
        else {
            $('#' + LBL_QTY).html("");
            $('#' + LBL_DEFECTIVE).html("");
        }

        if (parseInt(currhours) == parseInt(ss.TIME_PERIOD.substr(0, 2))) {
            var current_tr = table2.find("tr").eq(i);

            current_tr.find("td").css("background-color", "#d3d3d3");
            if (i >= 2) //前面的颜色需要还原
            {
                current_tr = table2.find("tr").eq(i - 1);
                current_tr.find("td").css("background-color", "#020321");
            }
        }

    }
}

function highlighttrow() {
    //debugger;
    var currenthourly = $("#currenthourly");
    var i_bak = parseInt(currenthourly.val());
    var table2 = $("#TB_2");
    //debugger;
    //Jquery才能用find
    var current_tr = table2.find("tr").eq(i_bak);
    //debugger;
    current_tr.find("td").css("background-color", "#ca9ac6"); // "#d3d3d3");

    if (i_bak >= 2) //前面的颜色需要还原
    {
        current_tr = table2.find("tr").eq(i_bak - 1);
        current_tr.find("td").css("background-color", "#020321");
    }
    //if (i_bak >= 3) //前面的颜色需要还原
    //{
    //    current_tr = table2.find("tr").eq(i_bak - 2);
    //    current_tr.find("td").css("background-color", "red");
    //}


}

function PROC_DASHBOARD_GET_PRODUCTION_QTY(FACTORY_CD, LINE_CODE) {
    kendo.ui.progress($("#loading"), true);
    $.ajax({
        async: false,
        dataType: 'json',
        type: 'POST',
        url: '../../DASHBOARD/DASHBOARDSHOWDATA/PROC_DASHBOARD_GET_PRODUCTION_QTY',
        data: {
            FACTORY_CD: FACTORY_CD,
            LINE_CODE: LINE_CODE
        },
        success: function (data) {
            if (data.SUCCESS == true) {
                //notification.show({
                //    title: MSG_INQ_OK_T,
                //    message: MSG_INQ_OK  //message: "查询完成！"
                //}, "upload-success");
            }
            else {
                notification.show({
                    title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                    message: MSG_INQ_FAILED + ":0382"  //message: "错误代码：0380"
                }, "error");
            }
            kendo.ui.progress($("#loading"), false);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            notification.show({
                title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                message: MSG_INQ_FAILED + ":0383-" + XMLHttpRequest.status  //message: "错误代码：0381"
            }, "error");
            kendo.ui.progress($("#loading"), false);
        }
    });
}

function PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(FACTORY_CD, LINE_CODE, SHIFT_CODE, TRX_DATE, ONLY1LINE) {
    kendo.ui.progress($("#loading"), true);
    $.ajax({
        async: false,
        dataType: 'json',
        type: 'POST',
        url: '../../DASHBOARD/DASHBOARDSHOWDATA/PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME',
        data: {
            FACTORY_CD: FACTORY_CD,
            LINE_CODE: LINE_CODE,
            SHIFT_CODE: SHIFT_CODE,
            TRX_DATE: TRX_DATE,
            ONLY1LINE: ONLY1LINE
        },
        success: function (data) {
            if (data.SUCCESS == true) {
                //    notification.show({
                //    title: MSG_INQ_OK_T,
                //    message: MSG_INQ_OK  //message: "查询完成！"
                //}, "upload-success");
            }
            else {
                notification.show({
                    title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                    message: MSG_INQ_FAILED + ":0384" //message: "错误代码：0381"
                }, "error");
            }
            kendo.ui.progress($("#loading"), false);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            notification.show({
                title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                message: MSG_INQ_FAILED + ":0385-" + XMLHttpRequest.status  //message: "错误代码：0385"
            }, "error");
            kendo.ui.progress($("#loading"), false);
        }
    });
}

function Go_GETHRData() {
    //重取考勤，处理考勤，刷新数据

    kendo.ui.progress($("#loading"), true);
    $.ajax({
        async: false,
        dataType: 'json',
        type: 'POST',
        url: '../../DASHBOARD/DASHBOARDSHOWDATA/GETEMPLOYEEATTENDTIMEINSERT',
        data: {
            FACTORY_CD: factorycd,
            LINE_CODE: linecode,
            HR_MAX_TIME_DIFFERENCE: HR_MAX_TIME_DIFFERENCE,
            HR_REFRESH_INTERVAL: HR_REFRESH_INTERVAL
        },
        success: function (data) {
            if (data.SUCCESS == true) {
                //    notification.show({
                //        title: MSG_INQ_OK_T,
                //        message: MSG_INQ_OK  //message: "查询完成！"
                //}, "upload-success");
            }
            else {
                notification.show({
                    title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                    message: MSG_INQ_FAILED + ":0390" //message: "错误代码：0381"
                }, "error");
            }
            kendo.ui.progress($("#loading"), false);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            notification.show({
                title: MSG_INQ_FAILED_T,  //title: "取数据错误：",
                message: MSG_INQ_FAILED + ":03890-" + XMLHttpRequest.status  //message: "错误代码：0385"
            }, "error");
            kendo.ui.progress($("#loading"), false);
        }
    });
    //刷新数据
    //RefreshData(factorycd ,linecode,shiftcode,trxdate,only1line)
    PROC_DASHBOARD_GET_EMPLOYEE_ATTENDTIME(factorycd, linecode, shiftcode, trxdate, only1line);

    location.reload();
}

function openwin(address, filename) {
    //window.open(filename, "newwindow", "height=1200, width=1200, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
    var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数
    if (window.screen) {
        var ah = screen.availHeight - 30;
        var aw = screen.availWidth - 10;
        fulls += ",height=" + ah;
        fulls += ",innerHeight=" + ah;
        fulls += ",width=" + aw;
        fulls += ",innerWidth=" + aw;
        fulls += ",resizable"
    } else {
        fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
    }
    //window.open(address,filename,fulls);
    window.open(address, fulls);
}

function Go_DashBoardShow() {
    //var address = "";
    //address = address + "../../DASHBOARD/BASICINFORMATIONDATA//BASICDATAView?";
    ////address = address + "h1ttp://192.168.27.80:6060/DASHBOARD/BASICINFORMATIONDATA/BASICINFORMATIONView?";
    //address = address + "FACTORY_CD =" + factorycd;
    //address = address + "&LINE_CODE = " + linecode;
    //address = address + "&SHIFT_CODE = " + shiftcode;
    //address = address + "&TRX_DATE = " + trxdate;
    //address = address + " target='_blank'";
    ////openwin(address);



    saveflag = "N";
    var dialog = $("#dialog");
    //if (!dialog.data("kendoWindow")) {
    //var dialog = $("<div />").kendoWindow({
    dialog.kendoWindow({
        position: {
            top: 100, // or "100px"
            left: "10%"
        },

        title: "设置:",
        width: "90%",
        height: "90%",
        actions: [
            "Close",
            "refresh"
        ],
        open: function () {
        },
        refresh: function () {
        },
        content: {
            url: "../../DASHBOARD/BASICINFORMATIONDATA/BASICDATAView",
            data: {
                FACTORY_CD: factorycd,
                LINE_CODE: linecode,
                SHIFT_CODE: shiftcode,
                TRX_DATE: trxdate
            },
            dateType: "json"
        },

        close: function (e) {
            if (saveflag == "Y") {
                location.reload();
            }
        },
        modal: true
    }).data("kendoWindow").center().open();
    //}

    //dialog.data("kendoWindow").center();
    //dialog.data("kendoWindow").open();
    // dialog.center();
    // dialog.open();

    //}
}


//function Go_HeadCount() {
//    var s = "";
//    s = s + "../EMPLOYEEATTENDTIME//EMPLOYEEATTENDTIMEView?";
//    s = s + "FACTORY_CD =" + factorycd;
//    s = s + "&LINE_CODE = " + linecode;
//    s = s + "&SHIFT_CODE = " + shiftcode;
//    s = s + "&TRX_DATE = " + trxdate;
//    s = s + " target='_blank'";
//    //openwin(s);
//    return s;
//}
//function Go_BasicInformation() {
//    var s = "";
//    s = s + "../BASICINFORMATIONDATA//BASICINFORMATIONView?";
//    s = s + "FACTORY_CD =" + factorycd;
//    s = s + "&LINE_CODE = " + linecode;
//    s = s + "&SHIFT_CODE = " + shiftcode;
//    s = s + "&TRX_DATE = " + trxdate;
//    s = s + " target='_blank'";
//    //openwin(s);
//    return s;

//}



//@*<div class="col-xs-10">
//            <h3 id="section-2"></h3>
//            @Html.ActionLink("考勤维护", "EMPLOYEE_ATTENDTIMEView", "EMPLOYEE_ATTENDTIME", new { FACTORY_CD = Model.RUNNING_BASIC_INFORMATION.FACTORY_CD, LINE_CODE = Model.RUNNING_BASIC_INFORMATION.LINE_CODE, SHIFT_CODE = Model.RUNNING_BASIC_INFORMATION.SHIFT_CODE, TRX_DATE = Model.RUNNING_BASIC_INFORMATION.TRX_DATE })
//        </div>

//        <div class="col-xs-10">
//            <h3 id="section-3"></h3>
//            @Html.ActionLink("基本信息维护", "BASICINFORMATIONView", "BASICINFORMATION_DATA", new { FACTORY_CD = Model.RUNNING_BASIC_INFORMATION.FACTORY_CD, LINE_CODE = Model.RUNNING_BASIC_INFORMATION.LINE_CODE, SHIFT_CODE = Model.RUNNING_BASIC_INFORMATION.SHIFT_CODE, TRX_DATE = Model.RUNNING_BASIC_INFORMATION.TRX_DATE }, new { target = "_blank" }))
//        </div>*@


//function RefreshData1() {

//    s = "";
//    s = s + "../DASHBOARDSHOWDATA/DASHBOARDSHOWView?";
//    s = s + "FACTORY_CD =" + "@Html.DisplayFor(model=> model.RUNNING_BASIC_INFORMATION.FACTORY_CD)";
//    s = s + "&LINE_CODE = " + "@Html.DisplayFor(model=> model.RUNNING_BASIC_INFORMATION.LINE_CODE)";
//    s = s + "&SHIFT_CODE = " + "@Html.DisplayFor(model=> model.RUNNING_BASIC_INFORMATION.SHIFT_CODE)";
//    s = s + "&TRX_DATE = " + getdate("@Html.DisplayFor(model=> model.RUNNING_BASIC_INFORMATION.TRX_DATE)", "S", "yyyy-MM-dd");
//    s = s + "&REFRESH =Y";

//    alert(s);

//    $.get(s, function () {

//    })
//    //window.open(s);

//    //function scan() {
//    //    $("#文本框的id").click(function () {
//    //        var a = $("#文本框的id").val();
//    //        var b = $("#文本框的id").val();
//    //        $.get('../../system/ygbxcx/ScanIdcard.action', { hospitalname: a, b: b }, function (data) {
//    //            var nameText = data;
//    //        });
//    //    });
//    //}
//}

//function Go_GETHRData() {
//    var factorycd = "@Html.DisplayFor(model=> model.RUNNING_BASIC_INFORMATION.FACTORY_CD)";
//    var linecode = "@Html.DisplayFor(model => model.RUNNING_BASIC_INFORMATION.LINE_CODE)";
//    alert(linecode);
//    var s = "";
//    s = s + "../EMPLOYEEATTENDTIME//GETEMPLOYEEATTENDTIMEINSERT?";
//    s = s + "FACTORY_CD =" + factorycd;
//    s = s + "&LINE_CODE = " + linecode;
//    s = s + "&SHIFT_CODE = " + shiftcode;
//    s = s + "&TRX_DATE = " + trxdate;
//    s = s + " target='_blank'";
//    //openwin(s);
//    alert(s);
//    return s;
//