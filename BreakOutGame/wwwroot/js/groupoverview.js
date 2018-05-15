window.onload = function () {
    var doingstuff = false;
    var wanttoreload = false;
    console.log("page entered");
    $(".grouplist").find(".group").each(function (index, item) {
        if (!$(item).hasClass("groupselected")) {
            $(item).click(function (e) {
                if (!$(e.target).hasClass("nonclick")) {
                    console.log("click");
                    console.log($(e.target));
                    if ($(this).hasClass("sideDiv")) {
                        console.log("snap");
                        doingstuff = false;
                    }
                    else
                        doingstuff = true;
                    console.log($(item).attr("id"));

                    $(".grouplist").find(".group").each(function (index2, item2) {
                        //$(item2).removeClass("hidden");
                        $(item2).attr("hidden", false);
                    });

                    $(".grouplist").find(".confirmation").each(function (index2, item2) {
                        // $(item2).addClass("hidden");
                        $(item2).attr("hidden", true);
                    });

                    //$(item).addClass("hidden");
                    $(item).attr("hidden", true);
                    //$("#" + $(item).attr("id") + "hid").removeClass("hidden");
                    $("#" + $(item).attr("id") + "hid").attr("hidden", false);
                    console.log("#" + $(item).attr("id") + "hid");
                    $("html, body").animate({
                        scrollTop: $("#groupparent_" + $(item).attr("id")).offset().top - 80
                    }, 'slow');

                }
            });
            $(item).mouseenter(function(e) {
                doingstuff = true;
                console.log("enter");
            });
            $(item).mouseleave(function (e) {
                if (wanttoreload)
                    location.reload();
                doingstuff = false;
                console.log("leave");
            });
        }
    });
    /*$(document).bind("ajaxSend", function () {
        console.log("waiting for all requests to complete...");
        // ajaxStop (Global Event)
        // This global event is triggered if there are no more Ajax requests being processed.
    }).bind("ajaxStop", function () {
        // maybe reload here?
        location.reload();
    });*/
   /*setInterval(function () {
        console.log(doingstuff);
        if (!doingstuff)
            location.reload();
        else
            wanttoreload = true;
        console.log(wanttoreload + "reload");
    }, 10000);*/
   

};
