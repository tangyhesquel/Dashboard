//begin of tangyh
//DASHBOARDSHOWView
function NotificationInit() {
    notification = $("#notification").kendoNotification({
        autoHideAfter: 3000,
        stacking: "up",
        templates: [{
            type: "error",
            template: $("#errorTemplate").html()
        }, {
            type: "upload-success",
            template: $("#successTemplate").html()
        }]

    }).data("kendoNotification");
}


//function getnow(obj)
//{
//    //var date = new Date();
//    //var da = date.format("MM/dd HH:mm:ss");
    
//    var da=getdate('', 'L', 'mm/dd','Y');
//    //当前时间
//    $(obj).text(da);
//   // $("#LBL_NowTime").text(da);
//}

function getnow(obj,vardatetime) {
    //var date = new Date();
    //var da = date.format("MM/dd HH:mm:ss");
    //if ((vardatetime == undefined)||(vardatetime == ""))
        //vardatetime = new Date(System.DateTime.Now.Year, System.DateTime.Now.Month - 1, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second, System.DateTime.Now.Millisecond);        
       // vardatetime=new Date('<%=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")%>');

    var da = getdate("", 'L', 'mm/dd', 'Y');
    //当前时间
    $(obj).text(da);
    // $("#LBL_NowTime").text(da);
}



function totalqty(objname,objid) {
    var t_o = document.getElementsByName(objname);
    var totalqty = 0;    
    for (var i = 0; i < t_o.length; i++) {
        if (t_o[i].innerText.trim() != "")
           totalqty = totalqty + parseInt(t_o[i].innerText.trim());
    }

    $(objid).text(totalqty);
}

function checkTime(i) {
    if (i<10) 
    {i="0"+i} 
    return i 
}

function getdate(vardate,shortorlong,format,ifdate) {
    var d;
    if ((vardate=='undefined')||(vardate==''))
        d = new Date() //新建一个Date对象 
    else
       if (ifdate=="Y")
            d = new Date(vardate) //新建一个Date对象 
        else
            d = new Date(Date.parse(vardate.replace(/-/g, "/")));
    
    

    var year = d.getFullYear() //获取年份 
    var month = d.getMonth() + 1 //获取月份 
    var day = d.getDate() //获取日期 
    var hour = d.getHours() //获取小时 
    var min = d.getMinutes() //获取分钟 
    var sec = d.getSeconds() //获取秒数 

    format=format.toLowerCase();
    var vdate;
    if (format=="yyyy-mm-dd")
        vdate = checkTime(year) + "-" + checkTime(month) + "-" + checkTime(day)
    if (format=="yyyy-dd-mm")
        vdate=checkTime(year) + "-" + checkTime(day) + "-" + checkTime(month) 
    if (format=="yyyy/mm/dd")
        vdate=checkTime(year) + "/" + checkTime(month) + "/" + checkTime(day) 
    if (format=="yyyy/dd/mm")
        vdate=checkTime(year) + "/" + checkTime(day) + "/" + checkTime(month) 
    if (format=="mm-dd-yyyy")
        vdate=checkTime(month) + "-" + checkTime(day) + "-" + checkTime(year) 
    if (format=="dd-mm-yyyy")
        vdate=checkTime(day) + "-" + checkTime(month) + "-" + checkTime(year) 
    if (format=="mm/dd/yyyy")
        vdate=checkTime(month) + "/" + checkTime(day) + "/" + checkTime(year) 
    if (format=="dd/mm/yyyy")
        vdate=checkTime(day) + "/" + checkTime(month) + "/" + checkTime(year) 
    if (format == "mm-dd")
        vdate = checkTime(month) + "-" + checkTime(day)
    if (format == "mm/dd")
        vdate = checkTime(month) + "/" + checkTime(day)

    var time="";
    if (shortorlong=="S")
        time = vdate;
    if (shortorlong=="L")
        time = vdate + " " + checkTime(hour) + ":" + checkTime(min) + ":" + checkTime(sec)
    if (shortorlong == "H")
        time =checkTime(hour) + ":" + checkTime(min) + ":" + checkTime(sec)

    return time;
}

//end of tangyh




// Format the date as the parameter.
Date.prototype.format = function (formatStr) {
    var date = this;
    /*  
    Function： Fill the char about "0"  
    Parameter：value-need filling string, length-total length 
    Return：   Filled string
    */
    var zeroize = function (value, length) {
        if (!length) {
            length = 2;
        }
        value = new String(value);
        for (var i = 0, zeros = ''; i < (length - value.length) ; i++) {
            zeros += '0';
        }
        return zeros + value;
    };
    return formatStr.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|M{1,4}|yy(?:yy)?|([hHmstT])\1?|[lLZ])\b/g, function ($0) {
        switch ($0) {
            case 'd': return date.getDate();
            case 'dd': return zeroize(date.getDate());
            case 'ddd': return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][date.getDay()];
            case 'dddd': return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][date.getDay()];
            case 'M': return date.getMonth() + 1;
            case 'MM': return zeroize(date.getMonth() + 1);
            case 'MMM': return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][date.getMonth()];
            case 'MMMM': return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][date.getMonth()];
            case 'yy': return new String(date.getFullYear()).substr(2);
            case 'yyyy': return date.getFullYear();
            case 'h': return date.getHours() % 12 || 12;
            case 'hh': return zeroize(date.getHours() % 12 || 12);
            case 'H': return date.getHours();
            case 'HH': return zeroize(date.getHours());
            case 'm': return date.getMinutes();
            case 'mm': return zeroize(date.getMinutes());
            case 's': return date.getSeconds();
            case 'ss': return zeroize(date.getSeconds());
            case 'l': return date.getMilliseconds();
            case 'll': return zeroize(date.getMilliseconds());
            case 'tt': return date.getHours() < 12 ? 'am' : 'pm';
            case 'TT': return date.getHours() < 12 ? 'AM' : 'PM';
        }
    });
}


function isUpperLetterDigital(item) {
    var str = $(item).val();
    var pattern = /[^a-zA-Z0-9_\-_\(_\)_]/g;
    if (pattern.test(str)) {
        str = str.replace(pattern, '')
        $(item).val(str.toUpperCase());
        return false;
    } else {
        $(item).val(str.toUpperCase());
        return true;
    }
}

function isTelephone(item) {

    var str = $(item).val();
    var pattern = /[^0-9\-\+]/g;
    if (pattern.test(str)) {
        str = str.replace(pattern, '')
        $(item).val(str);
        return false;
    } else {
        return true;
    }
}

function setMaxRemarkContent(controlId, MaxLength) {
    var control = document.getElementById(controlId);
    if (control.value.length > MaxLength) {
        control.value = control.value.substring(0, MaxLength);
    }
}

function isInteger(item) {
    var str = $(item).val();
    if (str.length > 0) {
        var v = parseInt(str, 10);
        if (isNaN(v)) {
            var pattern = /[^0-9]/g;
            str = str.replace(pattern, '');
            $(item).val(str);
            return false;
        }
        if (v < 0) {
            v *= -1;
        }
        $(item).val(v);
    }
    return true;
    //    var pattern = /[^0-9]/g;
    //    if (pattern.test(str)) {
    //        str = str.replace(pattern, '');
    //        $(item).val(str);
    //        return false;
    //    } else {
    //        return true;
    //    }
}

//function isInteger(item, allowN) {
//    var v = $(item).val();
//    if (v.length == 0) {
//        return true;
//    }
//    if (v.length == 1 && v == '-') {
//        if (allowN) {
//            return true;
//        }
//    }
//    if (isInteger(item)) {
//        if (allowN) {
//            return true;
//        } else {
//            var value = parseInt($(item).val(), 10);
//            if (value >= 0) {
//                return true;
//            } else {
//                $(item).val(value * -1);
//                return false;
//            }
//        }
//    }
//    return false;
//}

function isCalendarHour(item) {
    var str = $(item).val();
    var pattern = /[^0-9]/g;
    if (pattern.test(str)) {
        str = str.replace(pattern, '')
        $(item).val(str);
        return false;
    }
    else if (str > 23) {
        $(item).val('');
        return false;
    }
    else {
        return true;
    }
}

function isCalendarMinute(item) {
    var str = $(item).val();
    var pattern = /[^0-9]/g;
    if (pattern.test(str)) {
        str = str.replace(pattern, '')
        $(item).val(str);
        return false;
    }
    else if (str > 59) {
        $(item).val('');
        return false;
    }
    else {
        return true;
    }
}

//function isIntegerOrDecimal(item) {

//    var str = $(item).val();
//    var pattern = /[^0-9\.]/g;
//    if (pattern.test(str)) {
//        str = str.replace(pattern, '')
//        $(item).val(str);
//        return false;
//    } else {

//        return true;
//    }
//}

//Modify By Randy
function isIntegerOrDecimal(obj, decimalPlaces, allowNegative) {
    var temp = obj.value;
    //   var allowNegative = true;
    var integerPlaces = 0;

    var reg0Str = '[0-9]*';
    if (decimalPlaces > 0) {
        reg0Str += '\\.?[0-9]{0,' + decimalPlaces + '}';
    } else if (decimalPlaces < 0) {
        reg0Str += '\\.?[0-9]*';
    }
    reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
    reg0Str = reg0Str + '$';

    var reg0 = new RegExp(reg0Str);
    if (reg0.test(temp)) return true;

    // first replace all non numbers
    var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (allowNegative ? '-' : '') + ']';
    var reg1 = new RegExp(reg1Str, 'g');
    temp = temp.replace(reg1, '');

    if (allowNegative) {
        // replace extra negative
        var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
        var reg2 = /-/g;
        temp = temp.replace(reg2, '');
        if (hasNegative) temp = '-' + temp;
    }

    var index = temp.indexOf('.');

    if (decimalPlaces != 0) {
        var reg3 = /\./g;
        var reg3Array = reg3.exec(temp);
        if (reg3Array != null) {
            // keep only first occurrence of . 
            //  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
            reg3Right = reg3Right.replace(reg3, '');
            reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
            temp = temp.substring(0, reg3Array.index) + '.' + reg3Right;
        }
    }


    obj.value = temp;
}

function isLetterDigital(item) {
    var str = $(item).val();
    var pattern = /[^a-zA-Z0-9_-_,\.;\:"']/g;
    if (pattern.test(str)) {
        str = str.replace(pattern, '')
        $(item).val(str);
        return false;
    } else {
        $(item).val(str);
        return true;
    }
}

//Check paste content when paste
function checkPasteOfLetterOrDigital(item) {
    return isLetterDigital(item);
}

function isNotValidDateRange(fromObj, toObj, dateFormat) {
    var currentDate = new Date();
    var toDate = Date.parseInvariant(toObj.val(), dateFormat);
    if (fromObj.val().length > 0 && toObj.val().length == 0) {  // just input from date
        toObj.val(fromObj.val());
    } else if (fromObj.val().length == 0 && toObj.val().length > 0) { // just input to date
        if (currentDate.setHours(0, 0, 0, 0) > toDate) {
            return true;
        } else {
            fromObj.val(currentDate.format(dateFormat));
            return false;
        }
    }
    return false;
}

function validateMaxLength(item, event) {
    var maxLength = $(item).attr("Len"); //control.attributes["Len"].value;
    var str = $.trim($(item).val()); //control.value;
    if (maxLength && str.length > maxLength - 1) {
        event = (event) ? event : ((window.event) ? window.event : ""); //Get the compatible event.
        var key = event.keyCode ? event.keyCode : event.which; //Get the event object for compatible key values.
        if (key != 8)//keys except 'Backspace'
        {
            if (event.which)//If the browser is firefox.
                event.preventDefault();
            else
                event.returnValue = false;
            //set the texterea's value
            $(item).val(str.substring(0, maxLength));
        }
    }
}

function FilterSameItem(arr) {
    for (var i = 0; i < arr.length; i++) {
        for (var j = arr.length - 1; j > i; j--) {
            if (arr[j] == arr[i]) {
                arr.splice(j, 1);
            }
        }
    }
    return arr;
}

//set selectedValues by all item checked or not        
function SelectAll(listViewID) {
    //set all checkbox checked
    $("#" + listViewID).find("input[type='checkbox'][id*='chkCheckItem']").attr("checked", $("#" + listViewID).find("input[type='checkbox'][id*='chkSelectAll']").attr("checked"));

    //set the hidden varible value
    var number = "";
    var resultsString = $("#" + listViewID).find("input[type='hidden'][id*='hidSelectedKeyValues']").val();

    var checkBoxes = $("#" + listViewID).find("input[type='checkbox'][id*='chkCheckItem']");
    var resultArray = new Array();
    if (resultsString != "")
        resultArray = resultsString.split('|');

    var len = resultArray.length;

    if (len == 0) {
        for (var i = 0; i < checkBoxes.length; i++) {
            number = $(checkBoxes[i]).prev("input[type='hidden'][id*='hidKeyValueField']").val();
            if ($(checkBoxes[i]).attr("checked")) {
                resultArray.push(number);
            }
        }
    } else {
        for (var i = 0; i < len; i++) {
            for (var j = 0; j < checkBoxes.length; j++) {
                number = $(checkBoxes[j]).prev("input[type='hidden'][id*='hidKeyValueField']").val();
                if ($(checkBoxes[j]).attr("checked")) {
                    if (resultArray[i] != number) {
                        resultArray.push(number);
                    }
                } else {
                    if (resultArray[i] == number) {
                        resultArray.splice(i, 1);
                    }
                }
            }
        }
    }
    resultArray = FilterSameItem(resultArray);
    resultsString = resultArray.join('|');
    $("#" + listViewID).find("input[type='hidden'][id*='hidSelectedKeyValues']").val(resultsString);
    //alert($("#" + listViewID).find("input[type='hidden'][id*='hidSelectedKeyValues']").val())
}

//set selectedValues by item checked or not
function SetSelectedValues(currentCheckBox, listViewID) {
    var resultsString = $("#" + listViewID).find("input[type='hidden'][id*='hidSelectedKeyValues']").val();
    var resultArray = new Array();
    if (resultsString != "")
        resultArray = resultsString.split('|');
    //set the hidden varible value
    var number = $(currentCheckBox).prev("input[type='hidden'][id*='hidKeyValueField']").val();
    var len = resultArray.length;

    if ($(currentCheckBox).attr("checked")) {
        for (var i = 0; i < len; i++) {
            if (resultArray[i] != number) {
                resultArray.push(number);
                break;
            }
        }
        if (resultArray.length == 0)
            resultArray.push(number);
    } else {
        for (var i = 0; i < len; i++) {
            if (resultArray[i] == number) {
                resultArray.splice(i, 1);
                break;
            }
        }
    }
    resultsString = resultArray.join('|');
    $("#" + listViewID).find("input[type='hidden'][id*='hidSelectedKeyValues']").val(resultsString);
    //alert($("#" + listViewID).find("input[type='hidden'][id*='hidSelectedKeyValues']").val())
}

//Raise the element event
function DoPostForm(objIdEvent) {
    if (objIdEvent == null || objIdEvent == undefined || objIdEvent == 'undefined' || objIdEvent.length == 0) {
        return false;
    }
    var objId = "";
    var objEvent = "click";
    var objInfos = objIdEvent.split('.'); //[objId,objEvent]
    if (objInfos.length != 2)
        return false;
    objId = objInfos[0];
    objEvent = objInfos[1];
    $(objId).click();
    switch (objEvent) {
        case "click": $(objId).click(); break;
    }
    return false;
}


//获取QueryString的数组
function getQueryString() {
    var result = location.search.match(new RegExp("[\?\&][^\?\&]+=[^\?\&]+", "g"));
    if (result == null) {
        return "";
    }
    for (var i = 0; i < result.length; i++) {
        result[i] = result[i].substring(1);
    }
    return result;
}

//根据QueryString参数名称获取值
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}
//根据QueryString参数索引获取值
function getQueryStringByIndex(index) {
    if (index == null) {
        return "";
    }
    var queryStringList = getQueryString();
    if (index >= queryStringList.length) {
        return "";
    }
    var result = queryStringList[index];
    var startIndex = result.indexOf("=") + 1;
    result = result.substring(startIndex);
    return result;
}

// 通过该方法打开模态窗口时
// 注意子窗体获取父窗体传过来的参数更改为：window.dialogArguments.vArguments;
function openModalDialog(url, vArguments, width, height) {

    var search = "rand=" + Math.random() + "&isModalDialog=1";
    if (url.indexOf("?") < 0) {
        url = url + "?" + search;
    }
    else {
        url = url + "&" + search;
    }

    var args = { window: window.top, vArguments: vArguments };

    //    var features = "dialogWidth:" + width + "px;dialogHeight:" + height + "px;resizable:yes;scroll:no;status:no;center=yes";
    var features = "dialogWidth:" + width + "px;dialogHeight:" + height + "px;resizable:yes;scroll:yes;status:no;center=yes";
    return window.showModalDialog(url, args, features);
}






var contexts = {};
var dialogCount = 0;
var app = {
    getDialog: function (obj) {
        if (obj) {
            return obj.__dialog__;
        }

        return undefined;
    },
    close: function (obj) {
        var theDialog = this.getDialog(obj);
        if (theDialog) {
            var rest = Array.prototype.slice.call(arguments, 1);
            theDialog.close.apply(theDialog, rest);
        }
    },
    initOption: function (obj) {
        var kendowindOption = {
            actions: ["Pin", "Refresh", "Maximize", "Minimize", "Close"],
            draggable: true,
            pinned: true,
            height: "450px",
            modal: true,
            resizable: true,
            title: "新增",
            width: "700px"
        };
        if (obj) {
            if (obj.actions) {
                kendowindOption.actions = obj.actions;
            }
            if (obj.height) {
                kendowindOption.height = obj.height;
            }
            if (obj.width) {
                kendowindOption.width = obj.width;
            }
            if (obj.title) {
                kendowindOption.title = obj.title;
            }
        }
        return kendowindOption;
    },
    showDialog: function (obj) {
        var dialogid = "dialog" + dialogCount;
        var dialogFilter = "#" + dialogid;
        if ($(dialogFilter) == undefined || $(dialogFilter).length === 0)
            $("body").append('<div id="' + dialogid + '"></div>');
        var instance = $(dialogFilter).data("kendoWindow");
        if (instance == undefined) {
            var kendowindOption = this.initOption(obj);
            $(dialogFilter).kendoWindow(kendowindOption);
        }
        var dfd = $.Deferred();
        instance = $(dialogFilter).data("kendoWindow");
        instance.__dialog__ = {
            close: function () {
                dialogCount = dialogCount - 1;
                delete instance.__dialog__;
                instance.close();
                $(dialogFilter).remove();
                var args = arguments;
                if (args.length === 0) {
                    dfd.resolve();
                } else if (args.length === 1) {
                    dfd.resolve(args[0]);
                } else {
                    dfd.resolve.apply(dfd, args);
                }
            }
        };
        dialogCount = dialogCount + 1;
        instance.center();
        var url = obj.url;
        instance.refresh({
            url: url
        });
        instance.open();
        return dfd.promise();
    }
};
