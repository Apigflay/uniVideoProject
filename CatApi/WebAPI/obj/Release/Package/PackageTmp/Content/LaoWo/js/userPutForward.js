var userPutForward = {
    dome: function () {//元素
        var obj = {};
        obj.userIdx = $("#userIdx");
        obj.money = $("#money");
        obj.sale = $("#sale");
        obj.chbank = $("#chbank");
        obj.bankLayer = $("#bankLayer");
        obj.loginLayer = $("#loginLayer");
        obj.bank_info = $("#bank_info");
        obj.moneyNum = $("#moneyNum");
        obj.extractMoney = $("#extractMoney");
        obj.extractBankInfo = $("#extractBankInfo");
        obj.pwd = $("#pwd");
        obj.extract = $("#extract");
        obj.submit = $("#submit");
        return obj;
    }(),
    data: {//数据
        userIdx: function () {

            return userPutForward.dome.userIdx.val();
        },
        money: function () {
            var num = userPutForward.dome.money.attr("money");
            if (num == "" || num == null) {
                num = 0;
            }
            return parseInt(num);
        },
        sale: function () {
            var num = userPutForward.dome.sale.attr("sale");
            if (num == "" || num == null) {
                num = 0;
            }
            return parseInt(num);
        },
        pwd: function () {
            return userPutForward.dome.pwd.val();
        },
        bankList: Object,
        getBankList: function () {//取得银行卡列表
            userPutForward.help.ajaxs("POST", "/UserInfo/LaoWo_UserBankInfoGetSet", "JSON", { userIdx: userPutForward.data.userIdx }, function (data) {
             
                if (data.data == "" || data.code!="100") {
                    userPutForward.help.addBankCard();
                    return;
                }
                userPutForward.data.bankList = data.data;
                userPutForward.htmlLode.bankList.bankListHtmlLode(data.data);
                userPutForward.htmlLode.bankList.bankOnclick(data.data[0].bankId);
            });
        },
        moneyNum: function () {//获取提现金额
            var num = userPutForward.dome.moneyNum.val();
            if (num == "" || num == null) {
                num = 0;
            }
            return parseInt(num);
        },
        bankInfo: Object,
        bankInfoByBankId: function (bankId) {//获取银行卡信息
            for (var i = 0; i < userPutForward.data.bankList.length; i++) {
                if (userPutForward.data.bankList[i].bankId == bankId) {
                    return userPutForward.data.bankList[i];
                }
            }
            return null;
        },


    },
    htmlLode: {
        load: function () {
            //userPutForward.data.getBankList();
            userPutForward.dome.extract.click(function () {
                userPutForward.htmlLode.extract.extractBtnOnclick();
            });
            userPutForward.dome.submit.click(function () {
                userPutForward.submit();
            });
            //userPutForward.dome.bank_info.click(function () {
            //    userPutForward.htmlLode.bankList.bankListShow();
            //});
        },
        bankList: {
            bankListHtmlLode: function (data) {
                var strHtml = "";
                for (var i = 0; i < data.length; i++) {
                    strHtml += "  <li ><dl class=\"bank_info\" onclick=\"userPutForward.htmlLode.bankList.bankOnclick(" + data[i].bankId + ")\"><dt class=\"icon_bk_zgyh\" style=\"background:url(" + data[i].bankIcon + ") no-repeat;    background-size: 100%;\"></dt><dd>" + data[i].bankName + " （尾号****" + data[i].bankId.substring(data[i].bankId.length - 4, data[i].bankId.length) + "）</dd></dl><b></b></li>";
                }
                userPutForward.dome.chbank.html(strHtml);
            },

            bankListShow: function () {
                userPutForward.dome.bankLayer.show();
            },
            bankListHide: function () {
                userPutForward.dome.bankLayer.hide();
            },
            bankOnclick: function (bankId) {
                var bankInfo = userPutForward.data.bankInfoByBankId(bankId);
                userPutForward.data.bankInfo = bankInfo;
                var strHtml = " <dt class=\"icon_bk_zgyh\" style=\"background:url(" + bankInfo.bankIcon + ") no-repeat;    background-size: 100%;\"></dt><dd>" + bankInfo.bankName + " （" + bankInfo.bankId.substring(bankInfo.bankId.length - 4, bankInfo.bankId.length) + "）</dd>";
                userPutForward.dome.bank_info.html(strHtml);
                userPutForward.dome.bankLayer.hide();
            }
        },
        extract: {
            extractBtnOnclick: function () {
                //console.log(userPutForward.data.moneyNum() + "/" + userPutForward.data.money() + "/" + userPutForward.data.sale());
                var moneyNum = userPutForward.data.moneyNum();
                var money = userPutForward.data.money();
                var sale = userPutForward.data.sale();

                if (moneyNum == null || moneyNum == "" || moneyNum < 50000) {
                    userPutForward.help.prompt("请输入金额", 1);
                    userPutForward.dome.moneyNum.val("");
                    return;
                }
               
                if (moneyNum > money) {
                    userPutForward.help.prompt("超出可提现额度", 1);
                    userPutForward.dome.moneyNum.val("");
                    return;
                }
                //if (moneyNum > sale) {
                //    userPutForward.help.prompt("超出可提现额度", 1);
                //    userPutForward.dome.moneyNum.val("");
                //    return;
                //}
                userPutForward.dome.extractMoney.val(moneyNum);
                userPutForward.dome.loginLayer.show();
                var bankInfo = userPutForward.data.bankInfo;
                var strHtml = "<dt class=\"icon_bk_zgyh\" style=\"background:url(" + bankInfo.bankIcon + ") no-repeat;    background-size: 100%;\"></dt><dd>" + bankInfo.bankName + "<strong>尾号****" + bankInfo.bankId.substring(bankInfo.bankId.length - 4, bankInfo.bankId.length) + "储</strong></dd>";
                userPutForward.dome.extractBankInfo.html(strHtml);
            }
        }

    },
    submit: function () {
        if (userPutForward.data.pwd() == "") {
            userPutForward.help.prompt("请输入登录密码", 1);
            return;
        }
        userPutForward.help.ajaxs("POST", "/Pay/submitExtract", "JSON", { userIdx: userPutForward.data.userIdx, pwd: userPutForward.data.pwd(), money: userPutForward.data.moneyNum, bankId: userPutForward.data.bankInfo.bankId }, function (data) {

            userPutForward.help.prompt(data.msg, 1);
            if (data.code = "100") {
                userPutForward.dome.money.html("已蜜化：" + data.data.sale).attr("money", data.data.sale);
                userPutForward.dome.sale.html("总计可体现：" + data.data.sale / 100 + "HK$").attr("sale", data.data.sale/100);
                userPutForward.data.money();
                userPutForward.data.sale();
                userPutForward.dome.loginLayer.hide();
            }
        });
    },
    help: {//帮助
        ajaxs: function (types, urls, dataTypes, datas, callBack) {
            $.ajax({
                type: types,
                url: urls,
                dataType: dataTypes,
                data: datas,
                cache: false,
                success: function (data) {
                    callBack && callBack(data)
                }
            });
        },
        prompt: function (msg, num) {
            layer.open({
                content: msg,
                style: 'background:rgba(0,0,0,0.6); color:#fff; border:none;font-family:微软雅黑; margin-top: 70%;',
                time: num
            });
        },
        client: function () {
            var u = navigator.userAgent.toLowerCase();
            var isAndroid = u.indexOf('android') > -1 || u.indexOf('adr') > -1; //android终端
            var isiOS = !!u.match(/\(i[^;]+;( u;)? cpu.+mac os x/); //ios终端
            if (isiOS) {
                phone = "ios";
            } else {
                phone = "android";
            }
            return phone;
        },
        addBankCard: function (coni, mb) {
            var u = navigator.userAgent.toLowerCase();
            if (u.indexOf("laowo") >= 0) {
                if (userPutForward.help.client() == "ios") {
                    window.location.href = "beautifulApp://addBankCard";
                } else {
                    android.addBankCard();
                }
            }
        }

    }

}
