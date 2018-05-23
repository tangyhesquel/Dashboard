var popupWindow = {
    getParentFlag: function () { return "_parentFlag_"; },
   // getkendoWindowFlag: function () { return "kendoWindow"; },
    

    WindowCallback: function (data) {
        

        var parentHandle = $('#' + popupWindow.getParentFlag()).text();
        if (!!parentHandle) {
            var w = window.parent
            var x = [];
            for (var p in w) {
                if (!!w[p] && !!w[p]._marker && w[p]._marker == parentHandle) {
                    x.push(p.toString());
                    break;
                }
            }

            $.each(x, function (ix, el) {
                if (typeof (eval("window.parent."+el+".Callback")) != "undefined") {
                    eval("window.parent." + el + ".Callback")(data);

                    popupWindow.WindowClose(el);
                }
            });

            
        }
        else {

            popupWindow.WindowClose();
        }


    },

    WindowClose: function (winHandle) {
     //   debugger;
        if (document.location != window.parent.location) {
           
            if (!!winHandle)
            {
                if (typeof (eval("window.parent." + winHandle + ".Close")) != "undefined") {
                    eval("window.parent." + winHandle + ".Close")();
                }
            }
            else {
                var parentHandle = $('#' + popupWindow.getParentFlag()).text();
                if (!!parentHandle) {
                    var w = window.parent
                    var x = [];
                    for (var p in w) {
                        if (!!w[p] && !!w[p]._marker && w[p]._marker == parentHandle) {
                            x.push(p.toString());
                            break;
                        }
                    }

                }
                // alert(_p);

                $.each(x, function (ix, el) {
                    if (typeof (eval("window.parent." + el + ".Close")) != "undefined") {
                        eval("window.parent." + el + ".Close")();
                    }
                });
            }
        }
        else {
            //window.close();
            popupWindow.BrowerClose();
        }
    },

    BrowerClose: function () {
        var browserName = navigator.appName;
        if (browserName == "Netscape") {
            window.opener = "whocares";
            window.close();
        } else {
            if (browserName == "Microsoft Internet Explorer") {
                window.open('', '_parent', '');
                window.close();
            }
        }
    },

    CreateNew: function (options, doc) {
        //debugger;
        var thisObject = {};
        thisObject._marker = $.guid * 0x10000;
       
        var _options = options;

        var _windowID = "_w_" + thisObject._marker;

        var _doc = (doc == undefined ? $('body') : doc);
        var w = $("#" + _windowID);

        if (w.length != 1) {
            var c = $('<div id="' + _windowID + '"></div>');
            w = c.appendTo(_doc);
        }
     
        var winOptions = {// width: "615px",
            title: !!options & !!options.title ? options.title : "",
            /// content: crudServiceBaseUrl + "/MasterData/CustomerSearch",
            position: {
                top: !!options & !!options.top ? options.top : "10%", // or "100px"
                left: !!options & !!options.left ? options.left : "10%"
            },
            width: !!options & !!options.width ? options.width : "80%",
            height: !!options & !!options.height ? options.height : "80%",
            modal: !!options & !!options.modal ? options.modal : true,
            visible: false,
            close: function (e) {
                // debugger;
            },
            activate: function (e) {
                //debugger;url: "/userInfo",
                //data: { userId: 42 },
                w.data("kendoWindow").refresh({ url: options.url, data: { uid: "test" } });                
            },
            refresh: function () {
                // new content has been fetched
                var frame = w.find('iframe');
                if (frame.length > 0) {
                                       
                    var chile = $(frame[0].contentWindow.document).find("body");
                    
                    var flag = $("<label id='_parentFlag_' style=\"display:none\">" + thisObject._marker + "</label>");// 
                    flag.appendTo(chile);

                   //$(document).find('body').data(_windowID, w);
                }
            }
        };        
        w.kendoWindow(winOptions);
        thisObject.windowObject = w.data("kendoWindow");

        thisObject.Show = function () {
           
            if (_options.CallBackFunction != undefined) {
                thisObject.Callback = _options.CallBackFunction;
            }

            w.data("kendoWindow").open();
        }
        ;

        thisObject.Callback = function (data) {
            debugger;
        }
        ;

        thisObject.Close = function (windowHandle) {

            if (!!windowHandle) {
                // windowHandle.window('close');
                windowHandle.data("kendoWindow").close();
            }
            else {
                $(document).find('#' + _windowID).each(function (ix, el) {
                    $(el).data("kendoWindow").close();
                })
            }
        }
        ;

        return thisObject;
    }


}
;

var MsgBox = {
    Show: function (message, type,doc) {
        var _msgBoxID = "_notification_";
        
        var m = $("#" + _msgBoxID);

        var _doc = (doc == undefined ? $('body') : doc);

        if (m.length != 1) {
            var c = $('<div id="' + _msgBoxID + '"></div>');
            m = c.appendTo(_doc);
        }
        getNotifications

        m.kendoNotification({

        });

        var n = m.data("kendoNotification");

        debugger;
        n.getNotifications();
        n.show(
            message
            , type);

    }
};


(function (kendo, $) {

    kendo.arrays = {
        /// <signature>
        ///   <summary>
        ///   Extend the kendo namespace with additional functions.
        ///   </summary>
        /// </signature>

        find: function (array, criteria) {
            /// <signature>
            ///   <summary>Find a JSON Object in an array.</summary>
            ///   <param name="array" type="Array">Array of JSON objects.</param>
            ///   <param name="criteria" type="Object">
            ///     Criteria to find the JSON Object.
            ///     - attr: the name of the JSON attribute to search on.
            ///     - value: the value of to find.
            ///   </param>
            ///   <returns type="JSON Object or null if not found" />
            /// </signature>
            
            var result = null;
            $.each(array, function (idx, item) {
                if (item[criteria.attr] != undefined) {
                    if (item[criteria.attr].toString() == criteria.value) {
                        result = item;
                        return false;
                    }
                }
            });
            return result;
        },

        findAll: function (array, criteria) {
            /// <signature>
            ///   <summary>Find a JSON Object in an array.</summary>
            ///   <param name="array" type="Array">Array of JSON objects.</param>
            ///   <param name="criteria" type="Object">
            ///     Criteria to find the JSON Objects.
            ///     - attr: the name of the JSON attribute to search on.
            ///     - value: the value of to find.
            ///   </param>
            ///   <returns type="JSON Objects or null if not found" />
            /// </signature>

            var results = [];
            $.each(array, function (idx, item) {
                
                if (item[criteria.attr] != undefined) {
                    if (item[criteria.attr].toString() == criteria.value) {
                        results.push(item);
                    }
                }
            });
            return results.length == 0 ? null : results;
        }
    };
})(window.kendo, window.kendo.jQuery);
