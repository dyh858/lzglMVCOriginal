/*
$(function () {
    $(window).on("load resize", function () {
        $footer = $("#footer");
        footerHeight = $footer.height();
        footerTop = ($(window).scrollTop() + $(window).height() - footerHeight - 40.15) + "px";
        //如果页面内容高度小于屏幕高度，div#footer将绝对定位到屏幕底部，否则div#footer保留它的正常静态定位 
        if (($(document.body).height()) < $(window).height()) {// - footerHeight
            $footer.css({
                position: "absolute",
                top: footerTop,
                width:$(document).width()
            });
        } else {
            $footer.css({
                position: "relative"
            });
        }
    });
    var App = new Vue({
        el: "a",
        data:{
            rootPath: getRootPath()
        }
    })
});*/
//获得根路径，在服务器部署时避免因有中间路径而出现错误
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}
/* // 窗口加载事件仅用于窗口高度取决于图像 
    $(window).bind("load", function () {
        var footerHeight = 0,
        footerTop = 0,
        $footer = $("#footer");
        positionFooter();
        //定义positionFooter function 
        function positionFooter() {
            //取到div#footer高度 
            footerHeight = $footer.height();
            //div#footer离屏幕顶部的距离,[本人注：还要减去固定的导航栏高度60px] 
            footerTop = ($(window).scrollTop() + $(window).height() - footerHeight-60) + "px";
             DEBUGGING STUFF 
          　　console.log("Document height: ", $(document.body).height()); 
          　　console.log("Window height: ", $(window).height()); 
          　 console.log("Window scroll: ", $(window).scrollTop()); 
          　　console.log("Footer height: ", footerHeight); 
          　　console.log("Footer top: ", footerTop); 
          　　console.log("-----------") 
          　　
            //如果页面内容高度小于屏幕高度，div#footer将绝对定位到屏幕底部，否则div#footer保留它的正常静态定位 
            if (($(document.body).height()) < $(window).height()) {// - footerHeight
                $footer.css({
                    position: "absolute"
                }).stop().animate({
                    top: footerTop
                });
            } else {
                $footer.css({
                    position: "relative"
                });
            }
        }
        $(window).scroll(positionFooter).resize(positionFooter);
    });
    $(document).on("load mousedown click mouseover", function () {
        $(document).find("div:last-of-type").last().css("height", 0);
    });*/