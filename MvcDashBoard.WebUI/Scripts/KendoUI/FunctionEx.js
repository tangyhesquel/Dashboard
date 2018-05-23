Array.prototype.getItem = function (f) {
    var r = null;
    $.each(this, function (i, e) {
        if (f(i, e)) {
            r = e;
            return false;
        }
    });

    return r;
};

Array.prototype.getItemIndex = function (f) {
    var r = -1;
    //f为判断是否存在的回调函数
    $.each(this, function (i, e) {
        if (f(i, e)) {
            r = i;
            return false;
        }
    });

    return r;
}

Array.prototype.getItemList = function (f) {
    var r = [];
    $.each(this, function (i, e) {
        if (f(i, e)) {
            r.push(e);
        }
    });

    return r;
};


//V1 method
String.prototype.myFormat = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,
        function (m, i) {
            return args[i];
        });
};
//V2 static
String.myFormat = function () {
    if (arguments.length == 0)
        return null;

    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
};

jQuery.extend({
    isString: function () {
        if (arguments.length == 0)
            return null;

        var str = arguments[0];
        return Object.prototype.toString.call(str) === "[object String]";
    }
});

function closeWindow() {
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
}