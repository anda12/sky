function dateTimeConvert(value) {
    if (value == undefined || value == null || value == "") {
        return "";
    }
    var reg = new RegExp('/', 'g');
    var d = eval('new ' + value.replace(reg, ''));
    return new Date(d).format('yyyy-MM-dd hh:mm:ss')
}
function dateConvert(value) {
    if (value == undefined || value == "") {
        return "";
    }
    var reg = new RegExp('/', 'g');
    var d = eval('new ' + value.replace(reg, ''));
    return new Date(d).format('yyyy-MM-dd')
}