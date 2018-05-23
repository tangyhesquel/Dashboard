function NotificationInit1() {
    notification = $("#notification").kendoNotification({
        autoHideAfter: 3000,
        stacking: "up",
        position: {
            pinned: true,
            top: null,
            left: null,
            bottom: 20,
            right: 20
        },

        templates: [{
            type: "error",
            template: $("#errorTemplate").html()
        }, {
            type: "upload-success",
            template: $("#successTemplate").html()
        }]

    }).data("kendoNotification");
}


function CheckBeforeSave() {
    var s;
    if (($('#FACTORY_CD').val() == '') || ($('#GARMENT_TYPE').val() == '') || ($('#REFRESH_INTERVAL').val() == '')) {
        //s = "工厂、成衣类型、刷新时间不能为空!" + "\n\r";
        //s = s + "FACTORY CODE,GARMENT TYPE,REFRESH TIME CAN NOT BE EMPTY";
        //alert(s);
        //return false;
        return "CK1";
    }

    if ($('#REFRESH_INTERVAL').val() == '0') {
        //s = "刷新时间不能为0!" + "\n\r";
        //s = s + "REFRESH TIME CAN NOT BE ZERO !";
        //alert(s);
        //return false;
        return "CK2";
    }
    // remark by sunny 20180328
    //if (($('#LINE_CODE1').val() == '') && ($('#LINE_CODE2').val() == '')) {
    //    s = "请设置组别!" + "\n\r";
    //    s = s + "PLEASE SET THE LINE!";
    //    alert(s);
    //    return false;
    //    return "CK3";
    //}

    if (($('#SHIFT_CODE1').val() == '') && ($('#SHIFT_CODE2').val() == '')) {
        //s = "请设置班次!" + "\n\r";
        //s = s + "PLEASE SET THE SHIFT!";
        //alert(s);
        //return false;
        return "CK4";
    }
    // remark by sunny 20180328
    //if ($('#LINE_CODE1').val() != '') {
    //    if ($('#SHIFT_CODE1').val() == '') {
    //        //s = "请设置组1班次!" + "\n\r";
    //        //s = s + "PLEASE SET THE SHIFT OF LINE 1!";
    //        //alert(s);
    //        //return false;
    //        return "CK5";
    //    }
    //}
    // remark by sunny 20180328
    //if ($('#LINE_CODE2').val() != '') {
    //    if ($('#SHIFT_CODE2').val() == '') {
    //        //s = "请设置组2班次!" + "\n\r";
    //        //s = s + "PLEASE SET THE SHIFT OF LINE 2!";
    //        //alert(s);
    //        //return false;
    //        return "CK6";
    //    }
    //}
    // remark by sunny 20180328
    //if ($('#SHIFT_CODE1').val() != '') {
    //    if ($('#LINE_CODE1').val() == '') {
    //        //s = "请设置班次1的组别!" + "\n\r";
    //        //s = s + "PLEASE SET THE LINE OF SHIFT 1!";
    //        //alert(s);
    //        //return false;
    //        return "CK7";
    //    }
    //}
    // remark by sunny 20180328
    //if ($('#SHIFT_CODE2').val() != '') {
    //    if ($('#LINE_CODE2').val() == '') {
    //        //s = "请设置班次2的组别!" + "\n\r";
    //        //s = s + "PLEASE SET THE LINE OF SHIFT 2!";
    //        //alert(s);
    //        //return false;
    //        return "CK8";
    //    }
    //}
    // remark by sunny 20180328
    //if ($('#LINE_CODE1').val() == $('#LINE_CODE2').val()) {
    //    //s = "THE LINES CAN NOT BE THE SAME!" + "\n\r";
    //    //s = s + "组别不能相同!";
    //    //alert(s);
    //    //return false;
    //    return "CK9";
    //}

    if ($('#SHIFT_CODE1').val() == $('#SHIFT_CODE2').val()) {
        //s = "THE SHIFTs CAN NOT BE THE SAME!" + "\n\r";
        //s = s + "班次不能相同!";
        //alert(s);
        //return false;
        return "CK10";
    }

    if ($('#SHIFT_CODE3').val() != '') {
        if ($('#LINE_CODE3').val() == '') {
            //s = "特别设置信息不完整!" + "\n\r";
            //s = s + "THE SPECIAL SETTING INFORMATION IS NOT COMPLETE!";
            //alert(s);
            //return false;
            return "CK11";
        }
    }
    // remark by sunny 20180328
    //if ( $('#TARGET_TOTAL_QTY').val() == '' || $('#TARGET_TOTAL_QTY').val() == '0') {
    //        return "CK13";
    //}

    if ( $('#TARGET_WORK_HOUR').val() == '' || $('#TARGET_WORK_HOUR').val() == '0') {
            return "CK14";
    }
    return "";
}
