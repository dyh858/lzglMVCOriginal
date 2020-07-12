$(function () {
    //获取数据
    $.getJSON($('#rootpath').val()+"/Right/GetRightList", {}, function (response) {
        //alert(response);
        var json=JSON.parse(response);
        toTreeData(json,0);
    });
    //双击菜单
    $(document).delegate('li', 'dblclick', function (e) {
        e.stopPropagation();
        $(this).children('ul').toggle();
        //alert($(this).children('ul').attr('id'));
    });
    //单击菜单
    $(document).delegate('li', 'click', function (e) {
        alert($(this).parent.attr('name'));
        $("#txt_search_departmentname").val($(this).attr('name'));
        e.stopPropagation(); 
        var oTable =new TableInit();
        oTable.Init();
        $('#tb_emp').bootstrapTable('refresh');
    });
});
//构建树形菜单
function toTreeData(data, pid) {
    function tree(id) {
        data.filter(item =>{
            return item.UpperId===id; //筛选出上级id等于id的记录，箭头函数，item是参数，data的子对象
        }).forEach(obj =>{ //每一条记录是一个li元素，obj是参数，一条记录
            $("#"+obj.UpperId).append('<ul><li name="'+obj['Type']+'_' +obj['ID'] + '" id="' + obj['RightId'] +
                  '"><a href="#">' + obj['RightName'] + '</a></li></ul>');
            tree(obj.RightId);
        });
    }
    tree(pid);
}

