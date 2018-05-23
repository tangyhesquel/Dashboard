var FormPage = {
    KendoRoleMapping: {
        dropdownlist: "kendoDropDownList",
        maskedtextbox: "kendoMaskedTextBox",
        combobox: "kendoComboBox",
        datepicker: "kendoDatePicker",
        numerictextbox:"kendoNumericTextBox",
    }
    ,
    //Attribute_Flag
    //A_F: {
    //    valueField:"ValueField"

    //}
    //,
    getParentFlag: function () { return "_parentFlag_"; },
    getMyName: function () { return "_mineFlag_"; },

    SubFormCallback: function (data) {

        var parentHandle = $('#' + FormPage.getParentFlag()).text();
        if (!!parentHandle) {
            var w = window.parent;
            var x = [];
            for (var p in w) {
                if (!!w[p] && !!w[p]._marker && w[p]._marker == parentHandle) {
                    x.push(p.toString());
                    break;
                }
            }

            $.each(x, function (ix, el) {
                if (typeof (eval("window.parent.{0}.SubFormCallbackFun".myFormat(el))) != "undefined") {
                    eval("window.parent.{0}.SubFormCallbackFun".myFormat(el))(data);

                    FormPage.SubFormClose(el);
                }
            });


        }
        else {

            FormPage.SubFormClose();
        }


    },

    SubFormClose: function (winHandle) {
        //   debugger;
        if (document.location != window.parent.location) {

            if (!!winHandle) {
                if (typeof (eval("window.parent.{0}.SubFormClose".myFormat(winHandle))) != "undefined") {
                    eval("window.parent.{0}.SubFormClose".myFormat(winHandle))();
                }
            }
            else {
                var parentHandle = $('#' + FormPage.getParentFlag()).text();
                if (!!parentHandle) {
                    var w = window.parent;
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
                    if (typeof (eval("window.parent.{0}.SubFormClose".myFormat(el))) != "undefined") {
                        eval("window.parent.{0}.SubFormClose".myFormat(el))();
                    }
                });
            }
        }
        else {
            //window.close();
            FormPage.BrowerClose();
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

    GetInputData: function (doc) {
        var container = !!doc ? doc : $('body');
        var data = {};
        $(container).find(':input[field]').each(function (ix, el) {
            var fieldName = $(el).attr('field');
            var fieldType = $(el).attr('type');

            var role = $(el).data("role");
            if (!!role) {
                data[fieldName] = $(el).data(FormPage.KendoRoleMapping[role]).value();
            }
            else {
                if (fieldType == "checkbox" || fieldType == "radio") {
                    data[fieldName] = ($(el).prop('checked')?"Y":"N");
                } else {
                    data[fieldName] = $(el).val();
                }
            }

        });
        return data;
    }
    ,


    GetFilterData: function (doc) {
        var container = !!doc ? doc : $('body');
        var filters = [];
        $(container).find(':input[field]').each(function (ix, el) {
            var filterData = {};
            filterData.field = $(el).attr('field');

            var opt = $(el).attr('opt');
            filterData.operator = !!opt ? opt : "=";

            filterData.operator =escape(filterData.operator);

            var role = $(el).data("role");
            if (!!role) {
                filterData.value = $(el).data(FormPage.KendoRoleMapping[role]).value();

                if (role == "datepicker") {
                    filterData.value = kendo.toString(filterData.value, "yyyy/MM/dd")
                }
            }
            else {
                filterData.value = $(el).val();
            }

            filters.push(filterData);
        });
        return filters;
    }
    ,
    ControlEnable: function (control,enable) {
        var role = $(control).data("role");
        if (!!role) {
            $(control).data(FormPage.KendoRoleMapping[role]).enable(enable);
        }
        else {
            $(control).attr("disable", enable ? null : "disable");
        }
    }
    ,
    GenEditor: function (opt) {
        if (opt.editor.type == "comboBox") {

            return function (container, options) {
                // debugger;
                var input = $("<input/>");
                // set its name to the field to which the column is bound ('name' in this case)
                input.attr("name", options.field);
                // append it to the container
                input.appendTo(container);
                // initialize a Kendo UI AutoComplete
                input.kendoComboBox({
                    dataTextField: "TEXT",
                    dataValueField: "VALUE",
                    dataSource: opt.dataSource
                });
            }

        }
    }
    ,
    GenTemplate: function (opt) {

        return function (dataItem) {
            var item = opt.dataSource.getItem(function (i, e) { return dataItem[opt.field] == e.VALUE });
            //var item = kendo.arrays.find(masterDataList["ORDER_TYPE"], { attr: "VALUE", value: dataItem.ORDER_TYPE });
            if (!!item)
            { return kendo.htmlEncode(item.TEXT); }
            else
            { return "<strong>" + kendo.htmlEncode(dataItem[opt.field]) + "</strong>"; }
        }
    }
    ,

    FormReset: function (e, doc) {

        var container = !!doc ? doc : $('body');
        $(container).find(':input[field]').each(function (ix, el) {

            var role = $(el).data("role");
            if (!!role) {
                if (role == "dropdownlist") {

                    $(el).data(FormPage.KendoRoleMapping[role]).value(
                        $(el).data(FormPage.KendoRoleMapping[role]).dataSource.data()[0][!!$(el).attr("ValueField") ? $(el).attr("ValueField") : "VALUE"]
                    );
                }
                else {
                    $(el).data(FormPage.KendoRoleMapping[role]).value("");
                }
            }
            else {
                $(el).val("");
            }
        });
    }
    ,

    FormShowValue: function (data, doc) {
        var container = !!doc ? doc : $('body');
        $(container).find(':input[field]').each(function (ix, el) {
            var fieldName = $(el).attr('field');
            var role = $(el).data("role");
            if (!!role) {
                $(el).data(FormPage.KendoRoleMapping[role]).value(data[fieldName]);
            }
            else {
                $(el).val(data[fieldName]);
            }

            //if (type.length > 0) {
            //    eval("$(el).{0}('setValue','{1}')".format(type, data[$(el).attr('field')]));
            //}
            //else {
            //    $(el).val(data[$(el).attr('field')]);
            //}
        });
    },

    _Grid_Data_Bound: function (e) {
        //debugger;
        var me = $('#' + FormPage.getMyName()).text();
        var w = window;
        var f = "";
        for (var p in w) {
            if (!!w[p] && !!w[p]._marker && w[p]._marker == me) {
                f=p.toString();
                break;
            }
        } f

        //e.sender.tbody.find("a.btn[action='Edit']").bind("click", function (event) {
        //    eval(f + "._gridRowEdit")(event,e.sender);
        //});

        //debugger;
        e.sender.tbody.find("a.btn[action='Edit']").each(function(ix,el){
            var data = e.sender._data.getItem(function (ixx, ele) { return ele.uid == $(el).attr("uid") }); // kendo.arrays.find(e.sender._data, { attr: "uid", value: $(el).attr("uid") });
            $(el).bind("click", function (e) {
                eval(f + ".GridRowEdit")(data,e);
            });
            //$(el).bind("click", function () {
            //eval(f + ".GridRowEdit")(kendo.arrays.find(e.sender._data, { attr: "uid", value: e.sender.tbody.find("a.btn[action='Edit']").attr("uid") }));
        });

        e.sender.tbody.find("a.btn[action='Delete']").each(function (ix, el) {
            var data = e.sender._data.getItem(function (ixx, ele) { return ele.uid == $(el).attr("uid") }); // kendo.arrays.find(e.sender._data, { attr: "uid", value: $(el).attr("uid") });
            $(el).bind("click", function (e) {
                eval(f + ".GridRowDelete")(data, e);
            });
            //$(el).bind("click", function () {
            //eval(f + ".GridRowEdit")(kendo.arrays.find(e.sender._data, { attr: "uid", value: e.sender.tbody.find("a.btn[action='Edit']").attr("uid") }));
        });

        //debugger;
        //e.sender.tbody.find("a.btn[action='Create']").each(function (ix, el) {
        //  //  var data = e.sender._data.getItem(function (ixx, ele) { return ele.uid == $(el).attr("uid") }); // kendo.arrays.find(e.sender._data, { attr: "uid", value: $(el).attr("uid") });
        //    $(el).bind("click", function (e) {
        //        eval(f + ".GridRowCreate")(e);
        //    });
        //    //$(el).bind("click", function () {
        //    //eval(f + ".GridRowEdit")(kendo.arrays.find(e.sender._data, { attr: "uid", value: e.sender.tbody.find("a.btn[action='Edit']").attr("uid") }));
        //});
    }
,
    CreateNew: function (options, doc) {
        var thisObject = {};
        thisObject._marker = $.guid * 0x10000;
        var _doc = (doc == undefined ? $('body') : doc);

        var flag = $("<label id='{1}' style=\"display:none\">{0}</label>".myFormat(thisObject._marker, FormPage.getMyName()));// 
        flag.appendTo(_doc);

        var _headArea = (!!options && !!options.searchArea ? _doc.find('#' + options.searchArea) : _doc);
        var _detailGrid = (!!options && !!options.gridOptions && !!options.gridOptions.gridID ? _doc.find('#' + options.gridOptions.gridID) : null);

        var _crudServiceBaseURL = (!!options && !!options.BaseURL ? options.BaseURL : crudServiceBaseUrl);
        var _masterDataDirectory = (!!options && !!options.MasterDataDirectory ? options.MasterDataDirectory : masterDataDirectory);
        //存放从服务器取来的MasterData数据
        thisObject.ComboDropdownDataList = (!!options && !!options.ComboDropdownDataList ? options.ComboDropdownDataList : MasterManager);//MasterManager

        //初始化ComboBox
        $(":input[control='ComboBox']").each(function (ix, ele) {

            //if (!thisObject.ComboDropdownDataList[$(this).attr("field")]) {
            //    //获取读DropdownList数据的URL地址（可以用元素的DataURL属性设置，也可以用元素的sourcename属性+Defaul路径设置）
            //    var url = $(this).attr("DataURL");
            //    url = !!url ? url : _crudServiceBaseURL + "/" + _masterDataDirectory + "/" + $(this).attr("sourcename");
            //    //存放到ComboDropdownDataList
            //    thisObject.ComboDropdownDataList[$(this).attr("field")] = DataReq.CreateNew(url).OrgExe();
            //}

            var textField = $(this).attr("TextField");
            var valueField = $(this).attr("ValueField");

            $(this).kendoComboBox({
                dataTextField: !!textField ? textField : "TEXT",
                dataValueField: !!valueField ? valueField : "VALUE",
                dataSource: thisObject.ComboDropdownDataList.GetMasterData($(ele).attr("sourcename")), //thisObject.ComboDropdownDataList[$(this).attr("field")]
            });
        });

        //初始化DropDownList
        $(":input[control='DropDownList']").each(function (ix, ele) {
            //if (!thisObject.ComboDropdownDataList[$(this).attr("field")]) {
            //    //获取读DropdownList数据的URL地址（可以用元素的DataURL属性设置，也可以用元素的sourcename属性+Defaul路径设置）
            //    var url = $(this).attr("DataURL");
            //    url = !!url ? url : _crudServiceBaseURL + "/" + _masterDataDirectory + "/" + $(this).attr("sourcename");
            //    //存放到ComboDropdownDataList
            //    thisObject.ComboDropdownDataList[$(this).attr("field")] = DataReq.CreateNew(url).OrgExe();
            //}

            var textField = $(this).attr("TextField");
            var valueField = $(this).attr("ValueField");

            $(this).kendoDropDownList({
                dataTextField: !!textField ? textField : "TEXT",
                dataValueField: !!valueField ? valueField : "VALUE",
                dataSource:  thisObject.ComboDropdownDataList.GetMasterData($(ele).attr("sourcename"))//thisObject.ComboDropdownDataList[$(this).attr("field")]
            });
        });

        //初始化日期
        $(":input[control='DateBox']").each(function (ix, ele) {
            $(this).kendoDatePicker({ format: "yyyy/MM/dd" });
        });

        //初始化Reset按钮
        var _btnReset = $(_doc).find('.btn[action="FormReset"]');
        if (_btnReset.attr("onclick") == undefined) {
            _btnReset.click(function (e) {
                //e.preventDefault();
                FormPage.FormReset(e, _headArea);
            });
        }

        //初始化搜寻按钮
        var _btnSearch = $(_doc).find('.btn[action="FormSearch"]');
        if (_btnSearch.attr("onclick") == undefined) {
            _btnSearch.click(function (e) {
                thisObject.GridSearch();
            });
        }

        
        //初始化关闭按钮
        var _btnClose = $(_doc).find('.btn[action="FormClose"]');
        if (_btnClose.attr("onclick") == undefined) {
            _btnClose.click(function (e) {
                FormPage.SubFormClose();
            });
        }

        //初始化选择按钮
        var _btnSelect = $(_doc).find('.btn[action="FormSelect"]');
        if (_btnSelect.attr("onclick") == undefined) {
            _btnSelect.click(function (e) {
                //debugger;
                 var data = kendo.arrays.findAll(_detailGrid.data("kendoGrid").dataSource.data(), { attr: "ch", value: "true" });
               // var x =_detailGrid.data("kendoGrid_checkedList");
               // alert(JSON.stringify(x));

                //FormPage.SubFormClose();
                 FormPage.SubFormCallback(data);
            });
        }

        //初始化Grid
        if (_detailGrid != null && !!options && !!options.gridOptions && !!options.gridOptions.options) {
            var checkBox = !!options.gridOptions.checkBox ? (options.gridOptions.checkBox == true || options.gridOptions.checkBox != "single" ? "multi" : "single") : "multi";
                        
            var gOpt = options.gridOptions.options;
            $.each(gOpt.columns, function (ix, ele) {
                if (!!ele.editor && !$.isFunction(ele.editor) && $.isPlainObject(ele.editor)) {
                    //  debugger;
                    if (!ele.editor.dataSource) {
                        ele.dataSource =[]// thisObject.ComboDropdownDataList.GetMasterData($(ele).attr("sourcename"));//thisObject.ComboDropdownDataList[ele.field];
                    }
                    else {
                        if ($.isArray(ele.editor.dataSource)){
                            ele.dataSource = ele.editor.dataSource;
                        }
                        else if($.isString(ele.editor.dataSource))
                        { ele.dataSource = thisObject.ComboDropdownDataList.GetMasterData(ele.editor.dataSource);; }
                    }
                    var editor = FormPage.GenEditor(ele);
                    var template = FormPage.GenTemplate(ele);

                    ele.editor = editor;
                    ele.template = template;
                }

            });
                       

            if (!!options.gridOptions.addMyCommand && (!options.gridOptions.addMyCommand || options.gridOptions.addMyCommand!="false")) {
                var commandOptions=$.extend({edit:true,del:true,create:true,colPoint:0}, options.gridOptions.addMyCommand);
               // debugger;
                var myCommand = {
                    field: "command",
                    title: " ",
                    width: commandOptions.edit&&commandOptions.del? 90:45,
                    template: function (dataItem) {

                        var e = '<a uid="{0}" class="btn btn-info" action="Edit"><span class="glyphicon glyphicon-pencil"></span></a>'.myFormat(dataItem.uid);
                        var d = '<a uid="{0}" class="btn btn-default" action="Delete"><span class="glyphicon glyphicon-trash"></span></a>'.myFormat(dataItem.uid);
                       
                        var g = '<div><div class="btn-group-sm" role="group" aria-label="...">{0}{1}</div></div>'.myFormat(commandOptions.edit?e:"",commandOptions.del?d:"");
                        return $(g).html();
                    }
                };
                var myCreateBTN = {
                    template: function () {
                        var c = '<div><a class="btn btn-info" action="Create"><span style="padding-right: 10px;" class="glyphicon glyphicon-plus-sign"></span>Add New Record</a></div>';
                        return $(c).html();
                    }
                };
                gOpt.columns.splice(commandOptions.colPoint, 0, myCommand);

                if (commandOptions.create) {
                    if (!gOpt.toolbar)
                    { gOpt.toolbar = []; }
                    if ($.isFunction(gOpt.toolbar)) {

                    }
                    else if ($.isArray(gOpt.toolbar)) {
                        gOpt.toolbar.splice(0, 0, myCreateBTN);
                    }
                    else {

                    }
                }
            }

            _detailGrid.kendoGrid(gOpt);
                      
            thisObject.GridObject = _detailGrid;
            _detailGrid.data("kendoGrid").bind("dataBound", FormPage._Grid_Data_Bound);
            _detailGrid.find("a.btn[action='Create']").bind("click", function (e) {
                return thisObject.GridRowCreate(e);
            });

            //check all
            _detailGrid.on('click', '.check-all', function () {
                //debugger;
                if (checkBox == "multi") {

                    var checked = $(this).is(':checked');
                                        
                    var checkedList = _detailGrid.data("kendoGrid_checkedList");
                    if (!checkedList) {
                        checkedList = [];
                    }

                    $.each(checkedList, function (ix, el) {
                        el.ch = false;
                        _detailGrid.data("kendoGrid").tbody.find("tr[data-uid=\"" + el.uid + "\"] .chkbx").attr("checked", false);
                    });
                    checkedList.splice(0, checkedList.length);
                    if (checked)
                    {
                        _detailGrid.data("kendoGrid").tbody.find("tr .chkbx").each(function (ix, el) {
                            $(el).attr("checked", "checked");

                        });
                        $.each(_detailGrid.data("kendoGrid").dataSource.data(), function (ix, el) {
                            el.ch = true;
                            checkedList.push(el);
                            //_detailGrid.data("kendoGrid").tbody.find("tr[data-uid=\"" + el.uid + "\"] .chkbx").attr("checked", "checked");
                        });
                    }
                    _detailGrid.data("kendoGrid_checkedList", checkedList);
                }
                else {
                    //单选
                    $(this).attr('checked',false);
                }
            });

            //ChkeckBox
            _detailGrid.on('click', '.chkbx', function () {
                var checked = $(this).is(':checked');
                //debugger;
                var checkedList = _detailGrid.data("kendoGrid_checkedList");
                if (!checkedList) {
                    checkedList = [];
                }
                
                var dataItem = _detailGrid.data("kendoGrid").dataItem($(this).closest('tr'));
                dataItem["ch"] = checked;

                if (checked) {
                    if (checkBox != "multi") {
                        //单选
                        $.each(checkedList, function (ix, el) {
                            _detailGrid.data("kendoGrid").tbody.find("tr[data-uid=\"" + this.uid + "\"] .chkbx").attr("checked", false);
                            this["ch"] = false;
                        });
                        checkedList.splice(0, checkedList.length);
                    }
                    else { }
                    checkedList.push(dataItem);
                }
                else {
                    checkedList.splice(checkedList.indexOf(dataItem), 1);
                }
                _detailGrid.data("kendoGrid_checkedList", checkedList);               
            });
        }

        thisObject.GridSearch = function (condition) {

            if (!condition) {
                condition = FormPage.GetInputData(_headArea);
            }
            //var d = { data: JSON.stringify(filterData) };
            _detailGrid.data("kendoGrid").dataSource.filter(condition);
            // _detailGrid.data("kendoGrid").dataSource.filter(d);
            //_detailGrid.data("kendoGrid").dataSource.transport.read.data = filterData;
            _detailGrid.data("kendoGrid").refresh();
        };

        ////获取页面上有field属性的Input元素的输入值。如果特殊取值Function，需要通过rules参数定义
        ////[{ field: "field名", method: function(ctrol) { return Number($(ctrol.target[0]).combobox('getSelected').id);} },
        //// { field: "gppc_iMonth", method: function(ctrol) { return Number($(ctrol.target[0]).combobox('getSelected').id);} }]
        //thisObject.GetInputData = function (rules) {
        //    var data = {};
        //    //if (rules != undefined) {
        //    //    if ($.isArray(rules)) {
        //    //        $.merge(thisObject.RulesFrGetInputData, rules);
        //    //    }
        //    //    else { thisObject.RulesFrGetInputData.push(rules); }
        //    //}
        //    //if (thisObject.RulesFrGetInputData.length > 0) {
        //    //    $(_doc).find(':input[field]').each(function (index) {
        //    //        var fieldName = $(this).attr('field');
        //    //        var d = $.grep(thisObject.RulesFrGetInputData, function (e, i) {
        //    //            return e.field == fieldName;
        //    //        });
        //    //        if (d.length > 0) {
        //    //            data[fieldName] = FormPage.GetInputValue(this, d[0].method);
        //    //        }
        //    //        else {
        //    //            data[fieldName] = FormPage.GetInputValue(this);
        //    //        }
        //    //    });
        //    //}
        //    //else {
        //        $(_doc).find(':input[field]').each(function (index) {
        //            data[$(this).attr('field')] = FormPage.GetInputData(this);
        //        });
        //    //}
        //    return data;
        //};

        thisObject.SubFormOpen = function (options, doc) {
            //  var thisObject = {};
            // thisObject._marker = $.guid * 0x10000;

            // var _options = options;

            var windowID = "_w_"; //+ thisObject._marker;

            var container = (doc == undefined ? $('body') : doc);
            var w = $("#" + windowID);

            if (w.length != 1) {
                var c = $('<div id="' + windowID + '"></div>');
                w = c.appendTo(container);
            }

            var ww = !!options & !!options.width ? options.width : "50%";
            var hh = !!options & !!options.height ? options.height : "50%";
            var tt = !!options & !!options.top ? options.top : "25%";
            var ll = !!options & !!options.left ? options.left : "25%";


            var winOptions = {// width: "615px",
                title: !!options & !!options.title ? options.title : "",
                /// content: crudServiceBaseUrl + "/MasterData/CustomerSearch",

                width: ww,// "80%",
                height: hh,//"80%",

                position: {
                    top: tt,//"10%", // or "100px"
                    left: ll,
                },
                modal: !!options & !!options.modal ? options.modal : true,
                visible: false,
                close: function (e) {
                    // debugger;
                    //var frame = w.find('iframe');
                    //if (frame.length > 0) {
                    //    frame.attr('src', '');
                    //}
                    w.data("kendoWindow").destroy();
                    if (!!options & !!options.SubFormCloseFun) {
                        if ($.isFunction(options.SubFormCloseFun)) {
                            options.SubFormCloseFun();
                        }
                        else { eval(options.SubFormCloseFun)(); }
                    }
                },
                open: function (e) {
                    kendo.ui.progress(w, true);                    
                }
                ,
                activate: function (e) {
                    //debugger;url: "/userInfo",
                    //data: { userId: 42 },
                    w.data("kendoWindow").refresh({ url: options.url, data: { uid: "test" } });
                },
                refresh: function () {
                    // new content has been fetched
                    kendo.ui.progress(w, false);
                    var frame = w.find('iframe');
                    if (frame.length > 0) {
                        var chile = $(frame[0].contentWindow.document).find("body");
                        //  debugger;
                        var flag = $("<label id='{1}' style=\"display:none\">{0}</label>".myFormat(thisObject._marker, FormPage.getParentFlag()));// 
                        flag.appendTo(chile);

                        //$(document).find('body').data(windowID, w);
                    }
                }
            };
            w.kendoWindow(winOptions);
            //    thisObject.windowObject = w.data("kendoWindow");

            if (options.CallBackFunction != undefined) {
                thisObject.SubFormCallbackFun = options.CallBackFunction;
            }

            w.data("kendoWindow").open();

            thisObject.SubFormObject = w;

            return w;
        }
        ;

        thisObject.SubFormClose = function (windowHandle) {

            if (!!windowHandle) {
                // windowHandle.window('close');
                windowHandle.data("kendoWindow").close();
            }
            else {
                //$(document).find('#' + windowID).each(function (ix, el) {
                //    $(el).data("kendoWindow").close();
                //})
                thisObject.SubFormObject.data("kendoWindow").close();
            }
        }
        ;

        thisObject.SubFormCallbackFun = function (data) {
            alert(JSON.stringify(data));
        }
        ;

        //thisObject._gridRowEdit = function (e,sender) {
        //   // debugger;

        //    var uid = $(e.target).closest("a.btn").attr("uid");// $(e.target).closest(".btn"); // get the current table row (tr)
        //    // get the data bound to the current table row
        //    var data = sender._data.getItem(function (ix, el) { return el.uid = uid; });

        //    thisObject.GridRowEdit(data);
        //}

        thisObject.GridRowEdit = function (dataItem,e) {
       //     debugger;
            alert("Edit");
        }

        thisObject.GridRowDelete = function (dataItem, e) {
        //    debugger;
            alert("Delete");
        }

        thisObject.GridRowCreate = function (e) {
            //    debugger;
            alert("Create");
        }

        return thisObject;
    }

}
;

//function grid_dataBound(e) {
//    debugger;
//    e.sender.tbody.find("a.btn[action='Edit']").bind("click", function () {
//        eval("_thisPage.GridRowEdit")(e.sender._data);
//        });
//    //e.sender.data("kendoGrid").find('a .btn [action="Edit"]').bind("click", function () {
//    //    eval("thisObject.GridRowEdit")(e);
//    //});

//}