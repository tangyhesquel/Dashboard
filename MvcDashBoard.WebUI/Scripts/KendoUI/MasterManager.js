var MasterManager = {
    _cacheData: {},
    GetMasterData: function (masterName) {
     //   debugger;
        if (!!!MasterManager._cacheData[masterName]) {

            var data;
            
            try {
                data = eval("Get_" + masterName)();
            }
            catch (ex) { }

            if (!$.isArray(data)) {
                var source = crudServiceBaseUrl + "/" + masterDataDirectory + "/" + masterName;
                data = DataReq.CreateNew(source).OrgExe();
            }
           
            MasterManager._cacheData[masterName] = data;
        }
        return MasterManager._cacheData[masterName];
    }
}

//function Get_TemplateDataList() { return []; }
//function Get_SeasonDataList() { return []; }
//function Get_BrandDataList() { return []; }

function Get_PaymentList() {
    return [{ TEXT: "30天付", VALUE: "T30" }, { TEXT: "60天付", VALUE: "T60" }];
}

function Get_WashTypeList() {
    return [{ TEXT: "干洗", VALUE: "D" }, { TEXT: "水洗", VALUE: "W" }];
}

function Get_CountryList() {
    return [{ TEXT: "China", VALUE: "CN" }, { TEXT: "USA", VALUE: "US" }];
}

function Get_ShipModeList() {
    return [{ TEXT: "Sea", VALUE: "Sea" }, { TEXT: "Air", VALUE: "Air" }];
}


