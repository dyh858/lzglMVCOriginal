$(function () {
    $.getJSON("/Department/GetDeptList", {}, function (response) {
        //alert(response);
        var json=JSON.parse(response);
        for (var i = 0; i < json.length ; i++) {
            var obj = json[i];
            //alert('#' + obj['UpperId']);
            //alert($('#'+ obj['UpperId']).length);
            if ($('#' + obj['UpperId']).length == 0) {
                $('#dept').append('<ul><li id="' + obj['DepId'] +
                  '">' + obj['UpperId'] + obj['DepName'] + obj['DepId'] + '</li></ul');
            }
            else {
                $('#' + obj['UpperId']).append('<ul><li id="' + obj['DepId'] +
                 '">' + obj['UpperId'] + obj['DepName'] + obj['DepId'] + '</li></ul');
            }
        }
    });
});