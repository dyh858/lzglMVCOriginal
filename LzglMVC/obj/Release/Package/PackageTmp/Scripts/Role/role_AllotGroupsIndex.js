$(function(){
    //$(window).on('load resize', function () {
    //    if ($(window).width() > 992) {
    //    }
    //    else {
    //    }
    //});
    //点击角色列表选项
    $(".opt-role").click(function () {
        //添加角色名称
        $("#selected-role").html($(this).html()).attr("value", $(this).val());
        //该角色下的所有权限组列表
        $.getJSON($("#rootpath").val() + "/Groups/GetGroupsList", { RoleId: $(this).val() }, function (data) {
            $('#show-groups option').remove();
            $('#show-groups').attr('size', data.length);
            for (var i = 0; i < data.length;i++){
                var opt = '<option class="list-group-item" title="'+data[i]['Note']+'" value="'+data[i]['Gid']+
                    '">' + data[i]['Title'] + '</option>';
                $('#show-groups').append(opt);
            }
        });
    });

    //给角色分配权限
    $('#GroupsToRole').click(function(){
        $.getJSON(
            $("#rootpath").val() + "/Role/AllotGroups",
            {
                rid: $("#selected-role").val(),
                gid: $("#selected-group-item").val()
            },
            function (data) {
                $('#show-groups option').remove();
                $('#show-groups').attr('size', data.length);
                for (var i = 0; i < data.length; i++) {
                    var opt = '<option class="list-group-item" title="' + data[i]['Note'] + '" value="' + data[i]['Gid'] +
                        '">' + data[i]['Title'] + '</option>';
                    $('#show-groups').append(opt);
                }
            }
        );
    });
    //移去角色的权限组
    $('#RemoveGroups').click(function () {
        var groups = $('#show-groups option:selected');
        if (groups.length <= 0) { return; }
        $.confirm({
            title: "分配权限组",
            content: '确定要移去选定的权限组吗？',
            columnClass: 'small',
            buttons: {
                确定: function () {
                    groups.each(function () {
                        //
                        $.getJSON(
                            $("#rootpath").val() + "/Role/RemoveGroups",
                            {
                                rid: $("#selected-role").val(),
                                gid: $(this).val()
                            }
                        );
                    });
                    //在移去权限组之后，刷新该角色限的所有权限组
                    $.getJSON($("#rootpath").val() + "/Groups/GetGroupsList", { RoleId: $("#selected-role").val() }, function (data) {
                        $('#show-groups option').remove();
                        $('#show-groups').attr('size', data.length);
                        for (var i = 0; i < data.length; i++) {
                            var opt = '<option class="list-group-item" title="' + data[i]['Note'] + '" value="' + data[i]['Gid'] +
                                '">' + data[i]['Title'] + '</option>';
                            $('#show-groups').append(opt);
                        }

                    });
                },
                取消: function () { }
            },
        });
    });
    //点击全部权限组列表选项
    $(".opt-groups").click(function () {
        //添加角色名称
        $("#selected-group-item").html($(this).html()).attr("value", $(this).val());
        //该角色下的所有权限组列表
        $.getJSON($("#rootpath").val() + "/Actions/GetActionsList", { GroupsId: $(this).val() }, function (data) {
            $('#show-actions option').remove();
            $('#show-actions').attr('size', data.length);
            for (var i = 0; i < data.length; i++) {
                var opt = '<option class="list-group-item"' + '" value="' + data[i]['Actid'] +
                    '">' + data[i]['Title'] + '</option>';
                $('#show-actions').append(opt);
            }
        });
    });
    //点击全部权限列表选项
    $(".opt-actions").click(function () {
        //添加权限名称，隐藏域
        $("#selected-action").attr("value", $(this).val());
    });
    //给权限组分配权限
    $('#ActionsToGroups').click(function () {
        $.getJSON(
            $("#rootpath").val() + "/Groups/AllotActions",
            {
                gid: $("#selected-group-item").val(),
                actid: $("#selected-action").val()
            },
            function (data) {
                $('#show-actions option').remove();
                $('#show-actions').attr('size', data.length);
                for (var i = 0; i < data.length; i++) {
                    var opt = '<option class="list-group-item"' + '" value="' + data[i]['Actid'] +
                        '">' + data[i]['Title'] + '</option>';
                    $('#show-actions').append(opt);
                }
            }
        );
    });
    //移去权限组的权限
    $('#RemoveActions').click(function () {
        var groups = $('#show-actions option:selected');
        if (groups.length <= 0) { return; }
        $.confirm({
            title: "分配权限",
            content: '确定要移去选定的权限吗？',
            columnClass: 'small',
            buttons: {
                确定: function () {
                    groups.each(function () {
                        //
                        $.getJSON(
                            $("#rootpath").val() + "/Groups/RemoveActions",
                            {
                                gid: $("#selected-group-item").val(),
                                actid: $(this).val()
                            }
                        );
                    });
                    //在移去权限组之后，刷新该角色限的所有权限组
                    $.getJSON($("#rootpath").val() + "/Actions/GetActionsList", { GroupsId: $("#selected-group-item").val() }, function (data) {
                        $('#show-actions option').remove();
                        $('#show-actions').attr('size', data.length);
                        for (var i = 0; i < data.length; i++) {
                            var opt = '<option class="list-group-item"' + '" value="' + data[i]['Actid'] +
                                '">' + data[i]['Title'] + '</option>';
                            $('#show-actions').append(opt);
                        }

                    });
                },
                取消: function () { }
            },
        });
    });
});