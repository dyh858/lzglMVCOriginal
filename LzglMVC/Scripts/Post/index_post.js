$(function () {
    var vm=new Vue({
        el: "#post",
        data: {
            lists: [],
            checkAll: false,//全选checkAll
        },
        methods: {
            getLists: function () {
                var self = this;
                //axios({
                //    method: "get",
                //    url:"/Post/GetPostList"
                //}).then(function (res) {
                //    this.lists = JSON.parse(res);
                //    console.log(res);
                //}).catch(function (err) {
                //    console.log(err);
                //})
                $.getJSON("/Post/GetPostTable", {}, function (response) {
                    self.lists = response;
                });
            },
            checkALL: function () {
                var bool = document.getElementById("all").checked;
                var items = document.getElementsByName("check");
                items.forEach(item=>item.checked=bool);
            },
            checkItem:function(){
                var allItem=document.getElementById("all");
                var items=document.getElementsByName("check");
                var countAll=items.length;

                var countChecked=function(){
                    var count=0;
                    items.forEach(function(item,index){
                        if(item.checked){
                            count++;
                        }
                    });
                    return count;
                }();
                if(countChecked==countAll){
                    allItem.checked=true;
                }else{
                    allItem.checked=false;
                }
            },
        },
        mounted: function () {
            this.getLists();
        }

    });
    var parent = document.getElementById("dept");
    var ele = document.getElementById("drag");
    ele.onmousedown = function (e) {
        disX = e.clientX - ele.offsetLeft;
        document.onmousemove = function (ev) {
            $('#dept').css('flex-basis',ev.clientX - disX + ele.offsetWidth);
        }
        document.onmouseup = function () {
            document.onmousemove = document.onmouseup = null;
        }
    }

})
//$(function () {
//    $.getJSON("/Post/GetPostList", {}, function (response) {
//        console.log(response);

//    });



//})