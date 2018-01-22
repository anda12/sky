

$(function () {

    //openPwd();

    $('#editpass').click(function () {
        //$('#w').window('open');
        showWindow("修改密码", "/Login/ChangePassword", 600, 400);
    });

    $('#loginOut').click(function () {
        $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {

            if (r) {
                location.href = '/Login/LogOff';
            }
        });
    })
});

