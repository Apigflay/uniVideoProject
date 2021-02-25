/// <reference path="lib/jquery-1.8.2.min.js" />
/// <reference path="E:\Tiange\live.9158.com\live-2017-07-31\live\WebAPI\Content/tipAlert/Alert.min.js" />

var income = {
    closeBindAlipayMask: function () {
        $('#bindAlipay').hide();
        $('.masklayer').hide();
    },
    openBindAlipayMask: function () {
        $('#bindAlipay').show();
        $('.masklayer').show();
    },
    openSuccess: function () {
        $('#bindSuccess').css('display', 'block');
        $('.masklayer').css('display', 'block');
    },
    closeSuccess: function () {
        window.location.reload();

        $('#bindSuccess').css('display', 'none');
        $('.masklayer').css('display', 'none');
    },
    withdrawMoney: function (idx, c) {

        $.ajax({
            timeout: 9000,
            type: 'POST',
            dataType: 'JSON',
            url: '/Pay/withdrawWalletData',
            data: { useridx: idx, chkcode: c },
            beforeSend: function () { },
            success: function (response) {
                if (response.code == 100) {
                    var num = response.data;
                    //第一次提现只能1元
                    if (num == '0') {
                        $('.da_with h3 span').html('1');
                    }
                    income.openSuccess();

                } else if (response.code == 103) {
                    Tips.alert('请先绑定支付宝账号');
                } else if (response.code == 104) {
                    Tips.alert('操作过于频繁，请稍后再试！');
                } else if (response.code == -1) {
                    Tips.alert('您还未满5级，快去升级取出自己的血汗钱！');
                } else if (response.code == -3) {
                    Tips.alert('早晚是你的钱，快攒够足够Money再来提现！');
                } else if (response.code == -5) {
                    Tips.alert('请于明日再来提现');
                } else if (response.code == -6) {
                    Tips.alert('提交太过频繁');
                } else if (response.code == -7) {
                    Tips.alert('你已经被拉黑，请联系官方运营');
                } else if (response.code == -8) {
                    Tips.alert('首次仅能提现1块钱！');
                } else if (response.code == -1000) {
                    Tips.alert('收款账号不存在！');
                }
                else {
                    Tips.alert('提现失败');
                }

                console.log(response);
            }
        });
    }
};