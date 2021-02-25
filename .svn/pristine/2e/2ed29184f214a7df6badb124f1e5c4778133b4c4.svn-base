/*!
 * JavaScript common v1.0
 * Copyright (c) 2016 ZhaoRui
 * Created: 2016-09-14
 */

function $$(id) {
    return "string" == typeof id ? document.getElementById(id) : id;
};

function getQueryStr(name) {
    var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
    if (reg.test(location.href)) return decodeURIComponent(RegExp.$2.replace(/\+/g, " "));
    return "";
};

String.prototype.format = function () {
    if (arguments.length == 0) return this;
    for (var s = this, i = 0; i < arguments.length; i++)
        s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
    return s;
};

/*
* 智能机浏览器版本信息:
*
*/
var browser = {
    is_weixin: function () {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            return true;
        } else {
            return false;
        }
    },
    is_qq: function () {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.match(/qq/i) == "qq") {
            return true;
        } else {
            return false;
        }
    },
    is_weibo: function () {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.match(/WeiBo/i) == "weibo") {
            return true;
        } else {
            return false;
        }
    },
    is_miaobo: function () {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            return true;
        } else {
            return false;
        }
    },
    versions: function () {
        var u = navigator.userAgent, app = navigator.appVersion;
        return {//移动终端浏览器版本信息 
            trident: u.indexOf('Trident') > -1, //IE内核
            presto: u.indexOf('Presto') > -1, //opera内核
            webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
            mobile: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), //是否为移动终端
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
            iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者QQHD浏览器
            iPad: u.indexOf('iPad') > -1, //是否iPad
            webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部
        };
    }(),
    language: (navigator.browserLanguage || navigator.language).toLowerCase()
};

var live = {
    ShowCard: function (obj, useridx) {
        if (useridx <= 0) {
            alert('获取用户信息失败！');
            return;
        };

        if (browser.versions.ios) {
            obj.setAttribute("href", "miaobowpa:/ShowCard&useridx=" + useridx + "&");
        }
        else if (browser.versions.android) {
            obj.setAttribute("href", "javascript:android.showUserCard(" + useridx + ")");
        }
    },
    EnterGame: function (obj, gameid) {
        if (browser.versions.ios) {

            obj.setAttribute("href", "miaobowpa:/EnterGame&gameid=" + gameid + "&");
        }
        else if (browser.versions.android) {

            obj.setAttribute("href", "javascript:android.showGame(" + gameid + ")");
        }
    },
    SetFollow: function (obj, type, fromidx, touseridx) {
        if (browser.versions.ios) {

            obj.setAttribute("href", "miaobowpa:/SetFollow&type=" + type + "&fromidx=" + fromidx + "&touseridx=" + touseridx + "&");
        }
        else if (browser.versions.android) {

            obj.setAttribute("href", "javascript:setFollow(" + type + "," + fromidx + "," + touseridx + ")");
        }
    },
    ReturnAPP: function (obj) {
        if (browser.versions.ios) {
            obj.setAttribute("href", "miaobowpa:/GoBackAPP&");
        }
        else if (browser.versions.android) {

            obj.setAttribute("href", "javascript:android.goBackAPP()");
        }
    },
    Share: function (obj, title) {
        if (browser.versions.ios) {
            var url = location.href;
            if (url.indexOf('?') > -1) {
                url += '&share=true';
            }
            obj.setAttribute("href", "miaobowpa:/ShareLink&title=" + title + "&linkurl=" + url + "&content=&smallimage=&");
        }
        else if (browser.versions.android) {

            obj.setAttribute("href", "javascript:android.share()");
        }
    },
    ApplyView: function (obj) {
        if (browser.versions.ios) {
            obj.setAttribute("href", "miaobowpa:/GoApplyView&");
        }
        else if (browser.versions.android) {

            obj.setAttribute("href", "javascript:android.goBackAPP()");
        }
    },
    //直播间管理
    ShowLivingRoom: function (obj, useridx) {
        if (browser.versions.ios) {
            obj.setAttribute("href", "miaobowpa:/RoomManage&useridx=" + useridx + "&");
        }
        else if (browser.versions.android) {
            obj.setAttribute("href", "javascript:android.RoomManage(" + useridx + ")");
        }
    },
    //宝宝收益页面
    ShowBabyView: function (obj, useridx) {
        if (browser.versions.ios) {
            obj.setAttribute("href", "miaobowpa:/BabyView&useridx=" + useridx + "&");
        }
        else if (browser.versions.android) {
            obj.setAttribute("href", "javascript:android.babyView(" + useridx + ")");
        }
    },
    GetGradeRank: function (grade) {
        var mylevel = 1;
        if (grade > 0 && grade <= 40) {
            mylevel = 1;
        }
        else if (grade > 40 && grade <= 80) {
            mylevel = 2;
        }
        else if (grade > 80 && grade <= 120) {
            mylevel = 3;
        }
        else if (grade > 120 && grade <= 160) {
            mylevel = 4;
        }
        else if (grade > 160 && grade <= 190) {
            mylevel = 5;
        }
        else if (grade > 190) {
            mylevel = 6;
        }
        return mylevel;
    },
    GetVipName: function (level) {
        var name = "";
        switch (level) {
            case 1:
                name = "普通土豪";
                break;
            case 11:
                name = "红色VIP";
                break;
            case 15:
                name = "紫色VIP";
                break;
            case 30:
                name = "蓝色皇冠";
                break;
            case 31:
                name = "紫色皇冠";
                break;
            case 32:
                name = "超级皇冠";
                break;
            case 34:
                name = "钻石皇冠";
                break;
            case 35:
                name = "王者皇冠";
                break;
            case 36:
                name = "代理";
                break;
            case 39:
                name = "至尊皇冠";
                break;
            default:
                name = "";
                break;
        }
        return name;
    }
};

function getCookie(c_name, isuinfo) {
    if (document.cookie.length > 0) {
        var c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            var c_end = document.cookie.indexOf(';', c_start);
            if (isuinfo) {
                var c_end2 = document.cookie.indexOf('&', c_start);
                if (c_end == -1 || (c_end2 > -1 && c_end > c_end2)) c_end = c_end2;
            }
            if (c_end == -1) { c_end = document.cookie.length; }
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return null;
}

//喵播下载
function download() {
    if (browser.is_weixin()) {
        window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=com.tiange.miaolive";
    }
    else if (browser.versions.ios || browser.versions.iPhone || browser.versions.iPad) {
        console.log('1');

        //window.location = "https://itunes.apple.com/cn/app/miaobo/id1229560692?l=zh&ls=1&mt=8";
        window.location = "http://url.cn/5oIzU6g";
    }
    else if (browser.versions.android) {
        window.location.href = "http://update.9158.com/miaolive/Miaolive.apk";
    } else {
        window.location.href = "http://a.app.qq.com/o/simple.jsp?pkgname=com.tiange.miaolive";
    }
}

//翻译文件
//此段代码要放到$(function(){});
function init_i18n(updateLang) {
    jQuery.i18n.properties({
        name: 'strings',
        path: '/content/i18n/lang/',
        mode: 'map',
        //language:'en-US',// $.i18n.browserLang().replace('-', '_'),
        cache: false,
        callback: function () {
            console.log(updateLang);

            if (updateLang != 0) {
                updateLang();
            }
        }
    });
    //在jQuery.i18n.properties回调函数外面掉要放在$(function(){})里面
    //console.log($.i18n.prop('string_totalrank'));
}

var TgBase = { version: "1.0.0" };
var TgProj = { version: "1.0.0" };

TgBase.Cookie = {
    set: function (name, value, expiresSeconds, path, domain) {
        //console.log(name+"_"+value);
        var expires;
        if (typeof (expiresSeconds) == "undefined" || (expiresSeconds == null)) {
            expires = new Date(new Date().getTime() + 24 * 3600 * 1000);
        } else {
            expires = new Date(new Date().getTime() + expiresSeconds * 1000);
        }
        document.cookie = name + "=" + escape(value) + ((expires) ? "; expires=" + expires.toGMTString() : "") + ((path) ? "; path=" + path : "; path=/") + ((domain) ? ";domain=" + domain : "");
    },
    //sMainName： Cookie名；sSubName：Cookie子键名，留空表示单值Cookie	
    get: function (sMainName, sSubName) {
        var re = new RegExp((sSubName ? sMainName + "=(?:.*?&)*?" + sSubName + "=([^&;$]*)" : sMainName + "=([^;$]*)"), "i");
        return re.test(unescape(document.cookie)) ? RegExp["$1"] : "";
    },
    clear: function (name, path, domain) {
        if (this.get(name)) {
            document.cookie = name + "=" + ((path) ? "; path=" + path : "; path=/") + ((domain) ? "; domain=" + domain : "") + ";expires=Fri, 02-Jan-1970 00:00:00 GMT";
        }
    }
};

//TgBase.Util = {
//    GetLocalAreaData: function (callback) {
//        var cookieName = "LocalArea",
////               province = TgBase.Cookie.get(cookieName, "province"), 
////               city = TgBase.Cookie.get(cookieName,"city"),
//             json = { province: TgBase.Cookie.get(cookieName, "province"), city: TgBase.Cookie.get(cookieName, "city") };
//        if (json.province == "" || json.city == "") {
//            $.getScript('http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js', function () {
//                if (remote_ip_info && remote_ip_info.ret == 1) {
//                    json.province = remote_ip_info.province;
//                    json.city = remote_ip_info.city;
//                    var area = "province=" + json.province + "&city=" + json.city;
//                    TgBase.Cookie.set(cookieName, area, null, "", TgBase.Com.CookieDomain);  // cookie time : 24hour
//                    if (callback != null) {
//                        callback(json);
//                    }
//                }
//            });
//        } else {
//            if (callback != null) {
//                callback(json);
//            }
//        }
//    }
//};