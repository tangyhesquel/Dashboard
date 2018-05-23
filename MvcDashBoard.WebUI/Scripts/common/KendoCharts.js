
//建立饼状图
CreatePie=function (ID, Title, Unit, datasource, field, categoryField) {
    $(ID).kendoChart({
        title: {
            position: "top", //"bottom",
            text: Title,
            color: "#FFFFF",
        },
        dataSource: datasource,//数据格式：[{category: "A",value: 53.8},{category: "B",value: 53.8}]
        legend: {
            visible: true
        },
        chartArea: {
            background: ""
        },
        seriesDefaults: {
            overlay: {
                gradient: "none" //渐变模式取消
            },
            labels: {
                visible: true,
                background: "transparent",
                template: "#= category #:#= value#" + Unit + " 占比#= kendo.format('{0:P}', percentage)#"
                // position: "center",//insideEnd
            }
        },
        seriesClick: function (e) {  //点击事件
            alert("Oh");
        },
        series: [{
            type: "pie",
            startAngle: 150,
            border: {
                width: 1,
                color: "#fff"
            },
            highlight: {
                border: {
                    width: 15,
                    padding: 0
                }
            },
            connectors: {
                padding: 1
            }, 
            field: field,//分类所属字段值,数据格式取数据源中字段，例如：value
            categoryField: categoryField,//分类名称,数据格式取数据源中字段，例如：category
            template: "#= category #:#= value#" + Unit
            
        }],
        //seriesColors: ["#5b9bd5", "#a5a5a5", "#4472c4", "#255e91", "#636363", "#264478", "#7cafdd", "#b7b7b7", "#698ed0", "#327dc2"],
        tooltip: {
            visible: true,
            padding: 10,
            template: "#= category #:#=value#，占比#= kendo.format('{0:P}', percentage)#"
            //format: "{0}%",
        }
    });
}

//建立简单柱状图
CreateCol=function (ID, Title, Unit, CategoriesStr, ValueStr) {
    $(ID).kendoChart({
        title: {
            position: "top",
            text: Title,
            //color: "#666666",
        },
        legend: {
            visible: true,
            position: "top",
            //font: "1.4em 微软雅黑",
        },
        categoryAxis: [{
            categories: CategoriesStr//Y轴显示类，数据格式["test1","test2","test3"]
        }],
        chartArea: {
            background: ""
        },
        transitions: false,   //动画效果  
        seriesDefaults: {
            //type: "column",
            //missingValues: "interpolate",
            overlay: {
                gradient: "none" //渐变模式取消
            },
            labels: {
                visible: true,
                background: "transparent",
                font: "0.9em 微软雅黑",
                template:"#= value#" + Unit
                //position: "center",//insideEnd
            }
        },
        series:ValueStr,
        //seriesColors: ["#5b9bd5", "#a5a5a5", "#4472c4", "#255e91", "#636363", "#264478", "#7cafdd", "#b7b7b7", "#698ed0", "#327dc2"],
        tooltip: {
            visible: true,
            padding: 10,
            template: "#= category #:#=value#" + Unit
            //format: "{0}%",
        }    
    });
}

//折线图
CreateLine = function (ID, Title, LegendShow, Unit, Ymin, CategoriesStr, DataSource) {  //调用CreateLine（标签ID,表头，是否显示说明信息，单位，Y轴最小值，X轴数据，Y轴数据源）
    $(ID).kendoChart({
        legend: {
            position: "custom",
            offsetY: 10,
            offsetX: 50,
            visible: LegendShow, //显示列说明，属性为：true/false
        },
        chartArea: {
            background: ""
        },
        seriesDefaults: {
            type: "line",
            missingValues: "interpolate",  //y轴null数据显示：gap=空缺显示，interpolate=连续显示，zero=0值显示
            style: "smooth",
            overlay: {
                gradient: "none" //渐变模式取消
            },
            labels: {             //折线数据显示
                visible: true,
                //format: this.name+'',
                background: "transparent",
                font: "0.9em 微软雅黑",
                template: "#= value#" + Unit,
            }
        },
        categoryAxis: {
            categories: CategoriesStr,//横坐标显示列，数据格式["test1","test2","test3"]
            majorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto"
            }
        },
        //seriesClick: function (e) {            //点击系列事件获取对象
        //    alert("category" + e.category),
        //    alert("dataItem" + e.dataItem),
        //    alert("element" + e.element),
        //    alert("originalEvent" + e.originalEvent),
        //    alert("percentage" + e.percentage),
        //    alert("sender" + e.sender),
        //    alert("series" + e.series),
        //    alert(e.series.type),
        //    alert("name" + e.series.name),
        //    alert(e.series.data),
        //    alert(e.value)
        //},
        series: DataSource,//数据线值，数据格式[ { name: '线条一', data: [1,2,3] };]：
        // seriesHover: seriesHoverFun,//线条显示方法
        title: {
            text: Title
        },
        valueAxis: {
            min: Ymin,
            labels: {
                //format: "{0}%"
            },
            line: {
                visible: true
            },
            //axisCrossingValue: -10
        },
        tooltip: {
            visible: true,
            //format: "{0}%",
            template: "#= category #: #= value #" + Unit,
        },
        transitions: false,   //动画效果 
    });
}

//建立复合柱状图
CreateCols=function (ID, Title, Unit, CategoriesStr, DataSource, tooltipTemp, seriesHoverFun) {
    if (tooltipTemp == undefined) {
        tooltipTemp = "#= category #:#=value#" + Unit;
    }
    $(ID).kendoChart({
        title: {
            position: "top",
            text: Title
        },
        legend: {
            visible: true,
            position: "top"
        },
        categoryAxis: [{
            categories: CategoriesStr//Y轴显示类，数据格式["test1","test2","test3"]
        }],
        chartArea: {
            background: ""
        },
        seriesDefaults: {
            overlay: {
                gradient: "none" //渐变模式取消
            },
            labels: {
                visible: true,
                background: "transparent",
                template: "#= value#" + Unit
                //"#= value#" + Unit
                //position: "center",//insideEnd
            }
        },
        series: DataSource,//X轴格式，数据格式:[{name:"bar1",data:[1,2,3]},{name:"bar2",data:[1,2,3]}]
        seriesHover: seriesHoverFun,
        //seriesColors: ["#5b9bd5", "#a5a5a5", "#4472c4", "#255e91", "#636363", "#264478", "#7cafdd", "#b7b7b7", "#698ed0", "#327dc2"],

        tooltip: {
            visible: true,
            padding: 10,
            template: tooltipTemp,

        },

    });
}


//建立复合横向Bar状图
CreateBar=function (ID, Title, Unit, CategoriesStr, DataSource, tooltipTemp, seriesHoverFun) {
    if (tooltipTemp == undefined) {
        tooltipTemp = "#= category #:#=value#" + Unit;
    }
    $(ID).kendoChart({
        title: {
            text: Title
        },
        legend: {
            visible: false
        },
        chartArea: {
            background: ""
        },
        seriesDefaults: {
            overlay: {
                gradient: "none" //渐变模式取消
            },
            type: "bar",
            labels: {
                visible: true,
                background: "transparent",
                template: "#= value#" + Unit
                //position: "center",//insideEnd
            }
        },
        series: DataSource,//X轴格式，数据格式:[{name:"bar1",data:[1,2,3]},{name:"bar2",data:[1,2,3]}]
        seriesHover: seriesHoverFun,
        //seriesColors: ["#5b9bd5", "#a5a5a5", "#4472c4", "#255e91", "#636363", "#264478", "#7cafdd", "#b7b7b7", "#698ed0", "#327dc2"],

        valueAxis: {
            //max: 140000,
            line: {
                visible: false
            },
            minorGridLines: {
                visible: true
            },
            labels: {
                rotation: "auto"
            }
        },
        categoryAxis: {
            categories: CategoriesStr,//Y轴显示类，数据格式["test1","test2","test3"]
            majorGridLines: {
                visible: false  //辅助线
            }
        },
        tooltip: {
            visible: true,
            template: tooltipTemp
        }
    });
}
////建立复合横向堆栈Bar状图
CreateStackBar=function (ID, Title, Unit, CategoriesStr, DataSource, tooltipTemp, seriesHoverFun) {
    if (tooltipTemp == undefined) {
        tooltipTemp = "#= series.name #: #= value #" + Unit;
    }
    $(ID).kendoChart({
        title: {
            text: Title
        },
        legend: {
            visible: true,
            position: "top"
        },
        chartArea: {
            background: ""
        },
        seriesDefaults: {
            overlay: {
                gradient: "none" //渐变模式取消
            },
            type: "bar",
            stack: {
                type: "100%"
            },
            labels: {
                visible: true,
                background: "transparent",
                template: "#= value#" + Unit + "",//"#= series.name #\r\n
                position: "center"
            }
        },
        series: DataSource,//X轴格式，数据格式:[{name:"bar1",data:[1,2,3]},{name:"bar2",data:[1,2,3]}]
        valueAxis: {
            line: {
                visible: false
            },
            minorGridLines: {
                visible: true
            }
        },
        categoryAxis: {
            categories: CategoriesStr,//Y轴显示类，数据格式["test1","test2","test3"]
            majorGridLines: {
                visible: false
            }
        },
        //seriesColors: ["#5b9bd5", "#a5a5a5", "#4472c4", "#255e91", "#636363", "#264478", "#7cafdd", "#b7b7b7", "#698ed0", "#327dc2"],
        seriesHover: seriesHoverFun,
        tooltip: {
            visible: true,
            template: tooltipTemp
        }
    });
}

//创建折线、柱形标准复合图
CreateLineBandard = function (ID, Title, CategoriesStr, DataSource,valueAxes) {
    $(ID).kendoChart({
        chartArea: {
            background: ""
        },
        title: {
            text: Title
        },
        legend: {
            position: "top",
        },
        seriesDefaults: {
            type: "column",
            overlay: {
                gradient: "none" //渐变模式取消
            },
        },
        categoryAxis: {
            categories: CategoriesStr,        //数据例子：["Mon", "Tue", "Wed", "Thu", "Fri", "San"],
            axisCrossingValues: [0, 10],
            labels: {
                rotation: "auto",
                visible: true
            },
            tooltip: {
                visible: true,
                template: "#= category ##= series.name #: #= value #",
                //format: "N0"
            },
        },
        series: DataSource,      //  [{ name: "Total Visits",data: [56000, 63000, 74000, 91000, 117000, 138000] },{type: "line",data: [30, 38, 40, 32, 42, 60],name: "mpg",color: "#0099CC",axis: "mpg"}  ],        
        valueAxes:valueAxes,
        //valueAxes: [{
        //    visible: false,
        //}, {
        //     name: "mpg",
        //    color: "#ec5e0a"
        //}, {
        //     name: "l100km",
        //    title: { text: "" },
        //    color: "#4e4141"
        //}],
        tooltip: {
            visible: true,
            template: "#= category ##= series.name #: #= value #",
            //format: "N0"
        },
        transitions: false,   //动画效果  
    });
}

//创建折线、柱形堆积复合图
CreateLineBar = function (ID, Title, CategoriesStr, DataSource) {
    $(ID).kendoChart({
        chartArea: {
            background: ""
        },
        title: {
            text: Title
        },
        legend: {
            position: "top",
        },
        seriesDefaults: {
            type: "column",
            stack: true,
            overlay: {
                gradient: "none" //渐变模式取消
            },
            labels: {
                visible: true,
                position: "center",//数据在图形的显示位置（内：insideEnd/外：outside/中间：center）
                background: ""
            },
        },
        categoryAxis: {
            categories: CategoriesStr,        //数据例子：["Mon", "Tue", "Wed", "Thu", "Fri", "San"],
            axisCrossingValues: [0, 10],
            tooltip: {
                visible: true,
                template: "#= category ##= series.name #: #= value #",
                //format: "N0"
            },
        },
        series: DataSource,      //  [{ name: "Total Visits",data: [56000, 63000, 74000, 91000, 117000, 138000] },{type: "line",data: [30, 38, 40, 32, 42, 60],name: "mpg",color: "#0099CC",axis: "mpg"}  ],        
        valueAxes: [{
                      visible: false,
                    },
                    {
                      name: "mpg",
                    },
                   {
                   }
        ],
        tooltip: {
            visible: true,
            template: "#= category ##= series.name #: #= value #",
            //format: "N0"
        },
        transitions: false,   //动画效果  
    });
}