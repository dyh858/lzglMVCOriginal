$(function () {
    $.getJSON("/Department/GetDeptList", {}, function (response) {
        //alert(response);
        var json=JSON.parse(response);
        toTreeData(json,0);
        $('#1 ul').toggle();
        //alert(json.filter(function(item){return item.UpperId===2}).length);
    });
    $(document).delegate('#0 li', 'dblclick', function (e) {
        e.stopPropagation();
        $(this).children('ul').toggle();
        //alert($(this).children('ul').attr('id'));
    });
    $(document).delegate('#0 li', 'click', function (e) {
        $("#txt_search_departmentname").val($(this).attr('name'));
        e.stopPropagation(); 
        var oTable =new TableInit();
        oTable.Init();
        $('#tb_emp').bootstrapTable('refresh');
    });
    $(document).delegate('#0 li', 'mouseover', function (e) {
        e.stopPropagation();
        $(this).css('cursor','pointer');
        //alert($(this).children('ul').attr('id'));
    });
    $(document).delegate('#0 span', 'click', function (e) {
        e.stopPropagation();
        $(this).nextAll('ul').toggle();
        if($(this).hasClass('glyphicon glyphicon-plus')){
            $(this).removeClass('glyphicon glyphicon-plus');
            $(this).addClass('glyphicon glyphicon-minus');
        }
        else if($(this).hasClass('glyphicon glyphicon-minus')){
            $(this).removeClass('glyphicon glyphicon-minus');
            $(this).addClass('glyphicon glyphicon-plus');
        }
    });
});
function toTreeData(data, pid) {
    function tree(id) {
        data.filter(item =>{
            return item.UpperId===id; //筛选出上级id等于id的记录，箭头函数，item是参数，data的子对象
    }).forEach(obj =>{ //每一条记录是一个li元素，obj是参数，一条记录
        var len=data.filter(item=>{return item.UpperId===obj.DepId}).length;
            $("#"+obj.UpperId).append('<ul style="list-style: outside none none;">'+         
                '<li style="white-space: nowrap;" name="' +obj['DepName'] + '" id="' + obj['DepId'] +'">' +
                (len>0?'<span class="glyphicon glyphicon-plus"></span> ':'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;')+
                obj['DepName'] + '</li></ul>');
            tree(obj.DepId);
        });
    }
    tree(pid);
}