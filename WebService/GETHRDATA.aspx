<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GETHRDATA.aspx.cs" Inherits="MvcDashBoard.WebUI.GETHRDATA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery-1.8.2.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

    <script>
    var webUrl = "http://192.168.27.80:8066/DashBoard_WebService.asmx";

    $.ajax({
        dataType: "json",
        type:'POST',
        //data: { "v_Employee": $("#txtEmpNo").val(), "v_CardTime": $("#txtFDate").val(), "tdate": $("#txtToDate").val(), "status": $("#selStatus").val(), },
        data: { "v_Employee": "0152785", "v_CardTime": "2016-12-21" },
        //url: webUrl + "/GetEmployeeLeaveApproveListMobile?jsoncallback=?",
        url: webUrl + "/DashBoard_01",
        beforeSend: function () {
        },
        success: function (json) {
            alert("Insert OK");
        },
        error: function (XMLHttpRequest, textStatus) {
            if (XMLHttpRequest.status == "500") {
                var result = eval("(" + XMLHttpRequest.responseText + ")");
                alert("error:" + result.Message);
            }
        }
    });


</script>
</body>
</html>
