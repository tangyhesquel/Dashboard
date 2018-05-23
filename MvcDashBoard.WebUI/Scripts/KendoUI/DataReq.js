////Sample
//var Cat = {
//    createNew: function() {
//        var cat = {};
//        cat.name = "大毛";
//        cat.makeSound = function() { alert("喵喵喵"); };
//        return cat;
//    }
//};
//
/// <reference path="jquery-1.8.3.js" />

//var _maxUploadFileSize = null;

var DataReq = {
    CreateNew: function (url) {
        var req = {};
        req.URL = url;
        req.OrgExe = function (actionName, postData, options) {
            var ret;
           
            $.ajax({
                url: this.URL,
                type: !!options && !!options.type ? options.type : "POST",
                //                timeout:??,//要求为Number类型的参数，设置请求超时时间（毫秒）。此设置将覆盖$.ajaxSetup()方法的全局设         置。
                async: !!options && !!options.async ? options.async : false,
                //                cache:true/false,//要求为Boolean类型的参数，默认为true（当dataType为script时，默认为false）。       设置为false将不会从浏览器缓存中加载请求信息。
                data: { action: actionName, data: !!postData ? escape(encodeURIComponent(JSON.stringify(postData))) : "" },
                //                dataType:"xml"/"html"/"script"/"json"/"jsonp"/"text"
                dataType: !!options && !!options.dataType ? options.dataType : "json",
                //                contentType："application/x-www-form-urlencoded",//要求为String类型的参数，当发送信息至服务器时，内容编码类型默认             为"application/x-www-form-urlencoded"。该默认值适合大多数应用场合。
                //                global: true, //要求为Boolean类型的参数，默认为true。表示是否触发全局ajax事件。设置为false将不会触发全局        ajax事件，ajaxStart或ajaxStop可用于控制各种ajax事件。
                //                ifModified:false,//要求为Boolean类型的参数，默认为false。仅在服务器数据改变时获取新数据。            服务器数据改变判断的依据是Last-Modified头信息。默认值是false，即忽略头信息。
                //                jsonp:"",//要求为String类型的参数，在一个jsonp请求中重写回调函数的名字。       该值用来替代在"callback=?"这种GET或POST请求中URL参数里的"callback"部分，例如      {jsonp:'onJsonPLoad'}会导致将"onJsonPLoad=?"传给服务器。
                //                username: "", //要求为String类型的参数，用于响应HTTP访问认证请求的用户名。
                //                password: "", //要求为String类型的参数，用于响应HTTP访问认证请求的密码。
                //                processData: true, //要求为Boolean类型的参数，默认为true。默认情况下，发送的数据将被转换为对象（从技术角度             来讲并非字符串）以配合默认内容类型"application/x-www-form-urlencoded"。如果要发送DOM             树信息或者其他不希望转换的信息，请设置为false。
                //                scriptCharset: "", //要求为String类型的参数，只有当请求时dataType为"jsonp"或者"script"，并且type是GET时               才会用于强制修改字符集(charset)。通常在本地和远程的内容编码不同时使用。
                //                beforeSend: function (XMLHttpRequest) { return true/false; },
                //                dataFilter: function (data, type) {
                //                    //返回处理后的数据
                //                    return data;
                //                },
                //                complete: function (XMLHttpRequest, textStatus) {
                //                    this; //调用本次ajax请求时传递的options参数
                //                },
                beforeSend: function (XMLHttpRequest) {
              ////      debugger;
              //      if (!!options && !!options.showProcess)
              //      {
              //          kendo.ui.progress($(document.body), true);
              //      }
                },
                complete: function (XMLHttpRequest, textStatus) {
                //    debugger;
                    //if (!!options && !!options.showProcess)
                    //{
                    //    kendo.ui.progress($(document.body), false);
                    //}
                },
                success: function (data, textStatus) {
                    //data可能是xmlDoc、jsonObj、html、text等等
                    // this;  //调用本次ajax请求时传递的options参数
                    ret = data;
                    this.async && !!options && options.success != undefined ? options.success(data, textStatus) : void (0);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //   debugger;
                    alert($(jqXHR.responseText).text());
                    // alert(textStatus + " " + errorThrown);
                }

            });
            //$("body").mask("hide");            
            return ret;
        };

        //req.GetValue = function (actionName, submitData, expFun) {
        //    try {

        //        var ret = this.OrgExe(actionName, submitData);
        //        if (!!ret) {

        //            if (ret.IsOK) {
        //                return ret.ReturnData;
        //            }
        //            else {
        //                if (!(expFun == undefined || expFun == null)) {
        //                    expFun(ret.Message, ret.ReturnData);
        //                }
        //                else {
        //                    var msg = "Server error occurred: {0}<br /><a onclick=\"$.messager.show({title:\'StackTrace\',msg:\'{1}\',width:document.body.clientWidth/4.0*3,height:document.body.clientHeight/4.0*3,style:{right:\'\',bottom:\'\'}});\" href=\"javascript:void(0);\">StackTrace</a>".format(ret.Message ? ret.Message : "", ret.StackTrace ? ret.StackTrace : "[]");
        //                    if (typeof ($.messager) != "undefined") {
        //                        $.messager.alert('Error', msg, 'error');
        //                    }
        //                    else {
        //                        alert(msg);
        //                    }
        //                }
        //                return null;
        //            }
        //        }
        //    }
        //    catch (ex) {
        //        var msg = 'Client error occurred: {0}<br />{1}'.format(ex.message, ex.name);
        //        if (typeof ($.messager) != "undefined") {
        //            $.messager.alert('Error', msg, 'error');
        //        }
        //        else {
        //            alert(msg);
        //        }
        //        return null;
        //    }
        //};

        return req;
    }
};
