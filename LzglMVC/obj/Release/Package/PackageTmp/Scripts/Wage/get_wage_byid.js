
$(function () {
    //$('#rootpath').val()获得非根目录人中间目录，web程序移植后，目录发生改变
    $(window).on("load", ShowTable('#tb'));
    $(window).on("resize", function () {
        ShowTable('#tb');
    });
    $(document).delegate('#YearMonthOk', 'click', function (e) {
        e.stopPropagation();
        ShowTable('#tb');
    });
    
});
function ShowTable(TableId) {
    $.getJSON($('#rootpath').val() + "/Wage/GetWageById",
        {
            empid: $('#empid').val(),
            StartDate: $('#StartMonth option:selected').text(),
            EndDate: $('#EndMonth option:selected').text()
        },
        function (response) {
            var json = JSON.parse(response);
            if ($(window).width() >= 992) {
                $("tr").remove();
                //标题栏
                tr = "<tr>";
                th = "";
                for (var i = 0; i < Object.keys(json[0]).length; i++) {
                    th += "<th>" + Object.keys(json[0])[i] + "</th>"
                }
                var s = tr + th + "</tr>"
                $(TableId).append(s);
                //填充内容
                for (var i = 0; i < json.length; i++) {
                    var tr = "<tr>";
                    var td = "";
                    for (var j = 0; j < Object.values(json[i]).length; j++) {
                        td += "<td>" + Object.values(json[i])[j] + "</td>"
                    }
                    var s = tr + td + "</tr>";
                    $(TableId).append(s);
                }
            } else {
                $("tr").remove();
                for (var i = 0; i < json.length; i++) {
                    var td = "<tr><td>";
                    var content = ""
                    for (var j = 0; j < Object.values(json[i]).length; j++) {
                        content += "<p>" + Object.keys(json[i])[j] + ": " + Object.values(json[i])[j] + "</p>";
                    }
                    td += content + "</td></tr>"
                    $(TableId).append(td);
                }
            }
        }
    );
}
