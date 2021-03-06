﻿$(function () {
    //注册表单
    $('#FrmRegist').validate({
        rules: {
            LoginId: {
                required: true,
                remote: {
                    url: '/SysAdmin/IsLoginId',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        empid: function () {
                            return $('#LoginId').val();
                        },
                    },
                }
            },
            LoginPwd: {
                required: true,
            },
            ConfirmPwd: {
                required: true,
                equalTo:'#LoginPwd',
            }
        },
        messages: {
            LoginId: {
                required: "用户ID不能为空！",
                remote: "已经存在用户ID！",
            },
            LoginPwd: {
                required: "密码不能为空！",
            },
            ConfirmPwd: {
                required: "请再输一次密码！",
                equalTo: '两次密码不一致！',
            }
        }
    });
});
function ShowEmp() {
    if ($('#Info').html() != "查询失败！"
            && $('#Info').html() != "您已注册！") {     //控制器返回的数据放在“Info”这个元素里
        var emp = JSON.parse($('#Info').html());

        $('.Name').html(emp["Name"]);           //
        $('#AdminName').val(emp["Name"]);
        $('#Empid').val(emp["Empid"]);
        $('#rid').val("2");  //员工自助角色ID为2

        $('#Verify').css('display', 'block');   //工资卡验证显示
        $('#Info').css('display', 'none');      //
        $(document).delegate("#BankCard", "input", function () {      //每点一下键，验证一次
            if ($('#BankCard').val() != emp["BankCard"]) {  //工资卡验证未成功
                $('#BankCard').css('border-color', 'red');
                $('#FrmRegist').css('display', 'none');
                
            } else {                                         //工资卡验证成功
                $('#BankCard').css('border-color', '#333');
                $('#FrmRegist').css('display', 'block');     //注册界面显示  
            }
        });
        $(document).delegate("#InvitationCode", "input", function () {      //每点一下键，验证一次
            if ($('#InvitationCode').val() != emp["InvitationCode"]) {  //验证未成功
                $('#InvitationCode').css('border-color', 'red');
                $('#FrmRegist').css('display', 'none');

            } else {                                         //工资卡验证成功
                $('#InvitationCode').css('border-color', '#333');
                $('#FrmRegist').css('display', 'block');     //注册界面显示  
            }
        });
    } else {
        $('#Info').css('display', 'block');
        $('#Verify').css('display', 'none');
    }
}
//根据姓名查询，返回emp集合
function ShowEmpByName() {
    if ($('#Info').html() != "查询失败！") {     //控制器返回的数据放在“Info”这个元素里
        var list = JSON.parse($('#Info').html());
        $('#Info').css('display', 'none');      //
        var item = "";
        for (var i = 0; i < list.length; i++) {
            item = '<div class="empItem" id="' + list[i]["Empid"] + '"><a href="#"><td>' +
                list[i]["Name"] + ',' +
                '性别：' + list[i]["Gender"] + ',' +
                '身份证号码：' + list[i]["Idcard"] +
                '</td></a></div>';
            $('#NameList').append(item);
        }

        
    } else {
        $('#Info').css('display', 'block');
    }
}

/*$(document).delegate(".empItem", "click", function (e) {
    e.stopPropagation();
    e.preventDefault();

    $('#NameList').html("");
    $.ajax({
        type: 'POST',
        url: $('#rootpath').val() + "/SysAdmin/Search",
        data: {
            TxtSearch: $(this).attr("id")
        },
        success: function (response, status, xhr) {
            $('#Info').html(response);
            ShowEmp();
        },
        error: function (xhr, errorText, errorStatus) {
            alert(xhr.status + ':' + xhr.statusText);
        }
    });
});*/