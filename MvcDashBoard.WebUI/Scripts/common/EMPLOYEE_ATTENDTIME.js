function RefreshGridData(datasource) {
    //alert(JSON.stringify(AttendTimeUpdateGrid_Datasource));
    var GriddataSource = new kendo.data.DataSource({
        data: datasource,
        batch: true,
        schema: {
            model: {
                fields: {
                    SEQNO: { editable: false },
                    FACTORY_CD: { editable: false },
                    EMPLOYEE_NO: { editable: false },
                    ATTEND_DATE: { editable: false, type: "date" },
                    CREATE_TIME: { editable: false, type: "date" },
                    SHIFT: { editable: true },
                    TRX_DATE: { editable: true, type: "date" },
                    PRODUCTION_LINE_CD: { editable: true }
                }
            }
        },
        pageSize: 10,
    });
    GriddataSource.fetch();
    

    var grid = $("#AttendTimeUpdateGrid").data("kendoGrid");
    grid.setDataSource(GriddataSource);
    //grid.dataSource.sync();
    //或者
    //grid.dataSource.read();
    //grid.refresh();
    GriddataSource.read();
    GriddataSource.refresh;
}

function SHIFTEdit(container, options) {
    //debugger;
    // create an input element
    var input = $("<input/>");
    // set its name to the field to which the column is bound ('name' in this case)
    input.attr("name", "SHIFT_CODE");
    // append it to the container
    //debugger;
    input.appendTo(container);
    // initialize a Kendo UI AutoComplete
    //debugger;
    input.kendoDropDownList({
        dataTextField: "SHIFT_CODE",
        dataValueField: "SHIFT_CODE",
        dataSource: SHIFT_CODE_dataSource,
        change: function (e) {
            var thisvalue = this.value();
            changedrowdata1.set("SHIFT", thisvalue);
        }
    });

}

function LINEEdit(container, options) {
    //debugger;
    // create an input element
    var input = $("<input/>");
    // set its name to the field to which the column is bound ('name' in this case)
    input.attr("name", "PRODUCTION_LINE_CD");
    // append it to the container
    //debugger;
    input.appendTo(container);
    // initialize a Kendo UI AutoComplete
    //debugger;
    input.kendoDropDownList({
        dataTextField: "PRODUCTION_LINE_CD",
        dataValueField: "PRODUCTION_LINE_CD",
        dataSource: LINE_CD_dataSource,
        change: function (e) {
            var thisvalue = this.value();
            changedrowdata1.set("PRODUCTION_LINE_CD", thisvalue);
        }
    });
}
