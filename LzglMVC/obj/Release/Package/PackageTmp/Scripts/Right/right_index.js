$(function () {
    $.getJSON("/Right/GetRightList", {}, function (response) {
        //alert(response);
        var json=JSON.parse(response);
        toTreeData(json,0);
    });
    $(document).delegate('li', 'dblclick', function (e) {
        e.stopPropagation();
        $(this).children('ul').toggle();
        //alert($(this).children('ul').attr('id'));
    });
    $(document).delegate('li', 'click', function (e) {
        $("#txt_search_departmentname").val($(this).attr('name'));
        e.stopPropagation(); 
        var oTable =new TableInit();
        oTable.Init();
        $('#tb_emp').bootstrapTable('refresh');
    });
});
function toTreeData(data, pid) {
    function tree(id) {
        data.filter(item =>{
            return item.UpperId===id; //筛选出上级id等于id的记录，箭头函数，item是参数，data的子对象
        }).forEach(obj =>{ //每一条记录是一个li元素，obj是参数，一条记录
            $("#"+obj.UpperId).append('<ul><li name="' +obj['RightName'] + '" id="' + obj['RightId'] +
                  '">' + obj['RightName'] + '</li></ul');
            tree(obj.RightId);
        });
    }
    tree(pid);
}