/// <reference path="lib/jquery-1.8.2.min.js" />

var tabIndex = 0;
var StarRank = {
    tabIndex: 0,
    giftid: 0,
    isLoading: true,
    tabArrary: [{ page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                { page: 1, pageEnd: 0, total: 0, loadEnd: false }]
};

StarRank.Config = {
    useridx: getQueryStr('useridx'),
    photo: getQueryStr('photo'),
    showtype: getQueryStr('showtype'),
    defaultPhoto: 'http://liveimg.9158.com/default.png'
};

$(function () {

    //加载上周礼物排名
    StarRank.Data.LoadltwkStarData();

    //周一3点以前，周末20点以后显示倒计时
    var date = new Date();
    if ((date.getDay() == 1 && date.getHours() <= 3) || (date.getDay() == 0 && date.getHours() >= 20)) {
        StarRank.Data.LoadTimeData();

        setInterval("StarRank.Logic.ShowTimeHtml();", 1000); //每隔一秒执行一次
    }
});


StarRank.Data = {
    LoadltwkStarData: function () {
        $.ajax({
            timeout: 9000,
            type: 'POST',
            dataType: 'json',
            url: '/Rank/starRankData?datatype=1',
            data: { useridx: StarRank.Config.useridx },
            beforeSend: function () { },
            success: function (response) {

                StarRank.Logic.LoadStarHtml(response.data);
            },
            error: function (xhr, type) { }
        });
    },
    LoadtswkStarData: function () {
        $.ajax({
            timeout: 9000,
            type: 'POST',
            dataType: 'json',
            url: '/Rank/starRankData?datatype=2',
            data: { useridx: StarRank.Config.useridx, giftid: StarRank.giftid },
            beforeSend: function () {
                StarRank.isLoading = true;

                $('.rank_body').append('<div class="loading">Loading...</div>');
            },
            success: function (response) {
                StarRank.tabArrary[tabIndex].pageEnd = response.data.totalPage;
                StarRank.tabArrary[tabIndex].page++;

                if (StarRank.tabArrary[tabIndex].page > StarRank.tabArrary[tabIndex].pageEnd) {
                    StarRank.tabArrary[tabIndex].loadEnd = true;
                }

                StarRank.Logic.LoadtswkStarHtml(response.data);
            },
            error: function (xhr, type) {
                StarRank.isLoading = false;
                $('.loading').remove();
            }
        });
    },
    LoadTimeData: function () {
        $.ajax({
            timeout: 9000,
            type: 'POST',
            dataType: 'json',
            url: '/Rank/GetTime',
            success: function (response) {
                $('#time').html(response.shortTime);
            }
        });
    }
};
StarRank.Logic = {
    createLiveFlagEle: function (item) {
        var html = item.isOnline == 1 ? '<div class="live"><span class="livetext">直播中</span></div>' : '';
        return html;
        return html;
    },
    LoadStarHtml: function (data) {
        var rankList = data.rankList;

        if (data.totalCount <= 0) {
            $('.nodata').css('display', 'block');
        } else {
            $.each(rankList, function (index, item) {

                var giftName = item.name.replace('幸运', '');
                var sex = (item.gender == 0) ? "female" : "male";
                var level = 'level' + item.level;

                var display = (index == 0) ? 'block' : 'none';
                //加载礼物tab
                var sel = (index == 0) ? 'tab_sel' : '';
                var percent = index * 100;
                $('.swiper-wrapper').append('<div onclick="tabEvent(this)" id="' + index + '" data-id="' + item.giftId + '" class="swiper-slide tab_item ' + sel + '">' + giftName + '</div>');
                $('.rank_con').append('<div class="miao_rank" id="rank' + (index + 1) + '" style="left:' + percent + '%;display:' + display + ';"></div>');

                var html = '';
                html += '<div class="item item1">'
                + '<a href="javascript:;" onclick="live.ShowCard(this,' + item.useridx + ')">'
                + '        <div class="title_star"><span class="star_name">上期' + giftName + '周星</span><span class="img"><img src="' + item.icon + '" /></span></div>'
                + '        <div class="the_img">'
                + '            <div class="img"></div>'
                + '           <div class="img_img"><img src="' + item.smallpic + '" onerror="javascript:this.src=http://liveimg.9158.com/default.png" /></div>'
                + '       </div>'
                + '       <div class="name">'
                + '           <span class="name_name">' + item.myname + '</span><span class="' + sex + '"></span>'
                + '           <div class="the_level"><span class="level ' + level + '"></span><span class="num_bg bg' + live.GetGradeRank(item.grade) + '"></span><span class="num">' + item.grade + '</span></div>'
                + '           <div class="food">数量：' + item.totalNum + '</div>'
                + '      </div>'
                + '</a>'
                + '</div>'
                + '<div class="title_thisweek"><span>本期' + giftName + '排名</span></div>';

                $('.miao_rank').eq(index).append(html);
                echo.init({
                    offset: 100,//离可视区域多少像素的图片可以被加载
                    throttle: 0//设置图片延迟加载的时间
                });
            });
        }
        //页面一加载获取第一个礼物的id，以便根据当前礼物获取本周数据
        StarRank.giftid = $('.tab .tab_item').eq(0).attr('data-id');

        StarRank.Data.LoadtswkStarData();
        StarRank.Logic.Module_GiftwkStar();
    },
    LoadtswkStarHtml: function (data) {
        var tswkList = data.curWeek;
        var myRank = data.myRank;

        $.each(tswkList, function (index, item) {
            var sex = (item.gender == 0) ? "female" : "male";
            var level = 'level' + item.level;
            var html2 = '', icon_num = 'icon_num' + (index + 1);

            html2 += '<div class="item">'
            + '<a href="javascript:;" onclick="live.ShowCard(this,' + item.useridx + ')">'
            + '    <div class="icon ' + icon_num + '">' + (index + 1) + '</div>'
            + '    <div class="the_img">'
            + '        <div class="img"></div>'
            + '        <div class="img_img"><img src="' + item.smallpic + '" onerror="this.src=http://liveimg.9158.com/default.png" /></div>'
            + '    </div>'
            + '    <div class="name">'
            + '       <span class="name_name">' + item.myname + '</span><span class="' + sex + '"></span>'
            + '       <div class="the_level"><span class="level ' + level + '"></span>'
            + '       <span class="num_bg bg' + live.GetGradeRank(item.grade) + '"></span>'
            + '       <span class="num">' + item.grade + '</span></div>'
            //+ '       <div class="food">数量：' + item.totalNum + '</div>'
            + '    </div>'
            + StarRank.Logic.createLiveFlagEle(item);
            + '</a>'
            + '</div>';

            $('.miao_rank').eq(tabIndex).append(html2);
        });

        //我的排名,当前用户idx不等于0时才追加元素到页面上
        if (StarRank.Config.useridx != 0) {
            StarRank.Logic.LoadMyRankHtml(myRank);
        }
        StarRank.isLoading = false;
        $('.loading').remove();
    },
    LoadMyRankHtml: function (myRank) {
        var rankText = '', rankValue = '';
        var photo = getCookie('photo');

        if (myRank != null) {
            rankText = '第' + myRank.rowNo + '名';
            rankValue = '数量：' + myRank.totalNum;
        } else {
            rankText = '50名以后';
            rankValue = '未上榜';
        }
        if (photo == null) {
            photo = StarRank.Config.photo;
        }

        var html3 = '';
        html3 += '<div class="self">'
            + '<div class="item"><a href="javascript:;">'
            + '<div class="icon">我</div>'
            + '<div class="the_img">'
            + '    <div class="img"></div>'
            + '    <div class="img_img"><img src="' + photo + '" /></div>'
            + '</div>'
            + '<div class="rankText">' + rankText + '</div>'
            + '<div class="rankValue">' + rankValue + '</div>'
            + '</div>'
            + '</a></div></div></div>';

        $('.miao_rank').eq(tabIndex).append(html3);
    },
    Module_GiftwkStar: function () {
        var swiper = new Swiper('.swiper-container', {
            pagination: '.swiper-pagination',
            //scrollbar: '.swiper-scrollbar',
            slidesPerView: 5,//'auto'/
            paginationClickable: true,
            spaceBetween: 5,
            freeMode: true
        });
    },
    ShowTimeHtml: function () {
        $(".showtime").css("display", "block");

        var time = new Array();
        time = document.getElementById("time").innerHTML.split(':');
        var hours = parseInt(time[0]);
        var f = parseInt(time[1]);
        var m = parseInt(time[2]);

        var hh, ff, mm;
        m = parseInt(m) + 1;
        if (m >= 60) {
            m = 0;
            f = parseInt(f) + 1;
            if (f >= 60) {
                f = 0;
                hours = parseInt(hours) + 1;
                if (hours >= 24) {
                    hours = 0;
                }
            }
        }
        if (hours < 10) {
            hh = "0" + hours;
        } else {
            hh = hours;
        }
        if (f < 10) {
            ff = "0" + f;
        } else {
            ff = f;
        }
        if (m < 10) {
            mm = "0" + m;
        } else {
            mm = m;
        }

        $("#time").html(hh + ":" + ff + ":" + mm);
    }
};


///礼物标签页切换
function tabEvent(obj) {
    //防止当前标签数据还未加载完成去点击其他标签数据加载错位问题
    if (StarRank.isLoading == true) {
        return;
    }

    tabIndex = obj.getAttribute('id');
    StarRank.giftid = obj.getAttribute('data-id');

    $(obj).addClass('tab_sel').siblings('.tab_item').removeClass('tab_sel');

    var percent = -(tabIndex * 100);

    $('.miao_rank').eq(tabIndex).show().siblings('.miao_rank').hide();
    $('#rank_con').css({ '-webkit-transform': 'translate3d(' + percent + '%, 0px, 0px)', '-webkit-transition': '300ms' });

    //如果当前页面数据没有请求完则不去调用下一个标签页数据
    if (!StarRank.tabArrary[tabIndex].loadEnd) {

        StarRank.Data.LoadtswkStarData(StarRank.giftid);
    }
}