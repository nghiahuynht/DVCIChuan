function GenrateDataTableSearch(tableId, URL, columnsData, searchParams) {
    $(tableId).DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "searching": false,
        "lengthChange": false,
        "iDisplayLength": 20,
        "columns": columnsData,
        "scrollY": "400px",
        "scrollX": true,
        "scrollCollapse": true,
        "fixedHeader": true,
        //"fixedColumns": {
        //    leftColumns: 4,
        //    rightColumns: 1
        //},
        "language": {
            "zeroRecords": "Không tìm thấy dữ liệu",
            "infoEmpty": "0/0 Kết quả"
        },
        "ajax": {
            "url": URL,
            "type": "POST",
            "data": searchParams
        },

        "destroy": true
    });

}

function ShowWaiting() {
    $("#popup-overlay").css("display", "block");
}
function HideWaiting() {
    $("#popup-overlay").css("display", "none");
}


function FormToObject(formName, suffixReplace) {
    var array = $(formName).serializeArray();
    var json = {};

    jQuery.each(array, function () {
        var nameobj = "";
        if (suffixReplace !== null && suffixReplace !== "") {
            nameobj = this.name.replace(suffixReplace, "");
        } else {
            nameobj = this.name;
        }
        json[nameobj] = this.value || '';
    });
    return json;
}

function AlertSuccess(contentMessage) {
    return "<span class='alertsuccess'><i class='glyphicon glyphicon-ok'></i>&nbsp;" + contentMessage + "</span>";
}
function AlertFail(contentMessage) {
    return "<span class='alertfail'><i class='glyphicon glyphicon-info-sign'></i>&nbsp;" + contentMessage + "</span>";
}


function ConverFormatDate(currentFormatDate) {
    if (currentFormatDate == null || currentFormatDate == "") {
        return null;
    } else {
        var arr = currentFormatDate.split("/");
        var newFormatDate = arr[2] + "/" + arr[1] + "/" + arr[0];
        return newFormatDate;
    }
   
}

function RenderNumerFormat(data) {
    var res = "";
    if (data !== undefined && data != null) {
        res = data.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    }
    return "<div style='width:100%;text-align:right;'>" + res + "</div>";
}

function SubStrinColumn(data, len) {
    let textReturn = "";
    if (data != null && data.length > len) {
        textReturn = data.substring(0, len);
    } else {
        textReturn = data;
    }
    return "<span title='" + data+"'>" + textReturn+"</span>";
}