
//获得根路径，在服务器部署时避免因有中间路径而出现错误
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}
$(function () {
    $(window).on('load resize', function () {
        //alert('hello');
        //设置页脚在文档短于窗口的情况下的位置
        var hBody = $('body').height(); //body的高
        var h = $(window).height() - hBody;
        console.log("window:" + $(window).height() + ",body:" + hBody + ",差："+h);
        if (h >= 0) { //如果body的高小于窗口的高度
           // $('.footer').css('position', 'absolute').css('bottom', '0');
            //$('body').css('height', hBody + 97)
        } else {
            //$('.footer').css('position', 'static');
            //$('body').css('height', hBody - 97);//增加body的高度和窗口一致
        }
    });
   
});