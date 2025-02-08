function CallMethod(url, data, successCallback) {
    //show loading... image
    try {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json; chartset:utf-8',
            dataType: 'json',
            success: successCallback,
            error: function (XmlHttpError, error, description) {
                if (XmlHttpError.status) {
                    alert(XmlHttpError.responseText);
                }
            },
            async: true,
        });
    }
    catch (e) {
        message = "no";
        alert('Error ' + e);
        process = false;
    }
}

$(document).on({
    ajaxStart: function () { $("body").addClass("loading"); },
    ajaxStop: function () { $("body").removeClass("loading"); }
});



Array.prototype.sum = function (prop) {
    var total = 0
    for (var i = 0, _len = this.length; i < _len; i++) {
        total += this[i][prop]
    }
    return total
}

function FillSelect(selectName, arrData, key, name, defOption, defKey, defValue) {
    try {
        var objSelect = $('#' + selectName);
        if (!$(objSelect).length) {
            console.log('FillSelect - "' + selectName + '" does not exists');
            return;
        }

        objSelect.empty();
        if (defOption) {
            objSelect.append('<option value="' + defKey + '">' + defValue + '</option>');
        }

        arrData.forEach(function (objData) {
            objSelect.append('<option value="' + objData[key] + '">' + objData[name] + '</option>');
        });
        console.log('FillSelect - "' + selectName + '" complete');
    }
    catch (err) {
        console.log(err.message);
    }
}


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function GetData(url, data, async) {
    var dataR = [];
    try {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json; chartset:utf-8',
            dataType: 'json',
            success: function (result) {
                var message = result.d[0];
                if (message == "ok") {
                    dataR = JSON.parse(result.d[1]);
                } else {
                    alert('Error al cargar los datos')
                }
            },
            error: function (XmlHttpError, error, description) {
                if (XmlHttpError.status) {
                    alert(XmlHttpError.responseText);
                }
            },
            async: async,
        });
    }
    catch (e) {
        message = "no";
        alert('Error ' + e);
    }
    return dataR;
}


