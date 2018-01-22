function showWindow(title, href, width, height) {
    $('#myWindow').window({
        title: title,
        width: width,
        height: height,
        content: '<iframe scrolling="no" frameborder="0"  src="' + href + '" style="width:100%;height:98%;"></iframe>',
        shadow: false,
        cache: false,
        closed: false,
        collapsible: false,
        resizable: false,
        loadingMessage: '正在加载数据，请稍等片刻......'
    });
}
function showMyWindow(title, href, width, height, modal, minimizable, maximizable, isScrolling) {
    var isScroll = isScrolling == undefined ? "yes" : "no";
    $('#myWindow').window({
        title: title,
        width: width === undefined ? 600 : width,
        height: height === undefined ? 450 : height,
        content: '<iframe scrolling="' + isScroll + '" frameborder="0"  src="' + href + '" style="width:100%;height:98%;"></iframe>',
        //        href: href === undefined ? null : href, 
        modal: modal === undefined ? true : modal,
        minimizable: minimizable === undefined ? false : minimizable,
        maximizable: maximizable === undefined ? false : maximizable,
        shadow: false,
        cache: false,
        closed: false,
        collapsible: false,
        resizable: false,
        loadingMessage: '正在加载数据，请稍等片刻......'
    });
}
/*日期格式*/
function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-'
    + (d < 10 ? ('0' + d) : d);
}

function myparser(s) {
    if (!s)
        return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
       return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}