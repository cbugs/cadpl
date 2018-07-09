var serviceURLSet = "/Admin/Context/ChangeContext";
var serviceURLGet = "/Admin/Context/GetContext";

$(".js-dialog").each(function() {
    $(this).dialog({
        autoOpen: false
    });
});

function ChangeContext() {
    var e = document.getElementById("select-context");
    var selectedContext = e.options[e.selectedIndex].value;

    $.ajax({
        type: "POST",
        url: serviceURLSet,
        data: "{newContext: '" + selectedContext + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        console.log(data);
        if (data.Result == "OK") {
            location.reload();
        }
        else {
            $("#dialog-context").find('p').html(data.Message);
            $("#dialog-context").dialog("open");
        }
    }

    function errorFunc() {
        alert("Error on context switching !");
    }
}

function GetContext() {
    $.ajax({
        type: "GET",
        url: serviceURLGet,
        contentType: "application/json; charset=utf-8",
        success: successFuncGet,
        error: errorFuncGet
    });

    function successFuncGet(data, status) {
        console.log("Context : " + data);
        $("#select-context").val(data);
    }

    function errorFuncGet() {
        alert("Error on getting context !");
    }
}

$(document).ready(function () {
    GetContext();
});