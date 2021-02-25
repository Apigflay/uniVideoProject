/// <reference path="lib/jquery-1.8.2.min.js" />
/// <reference path="lib/template.js" />
///喵播全球版明星榜js

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
    template.helper('GetGradeRank', function (grade) {
        return live.GetGradeRank(grade);
    });

    //加载上周礼物排名
    StarRank.Data.LoadltwkStarData();

    //周一3点以前，周末20点以后显示倒计时
    var date = new Date();
    if ((date.getDay() == 1 && date.getHours() <= 3) || (date.getDay() == 0 && date.getHours() >= 20)) {
        StarRank.Data.LoadTimeData();

        setInterval("StarRank.Logic.ShowTimeHtml();", 1000); //每隔一秒执行一次
    }
});

StarRank.Logic = {
    //createLiveFlagEle: function (item) {
    //    return item.isOnline == 1 ? '<div class="sta_live"></div>' : '';
    //},
    LoadStarHtml: function (data) {

        //var html = template('rank_tab_template', data);
        //$('.swiper-wrapper').append(html);

        if (data.totalCount <= 0) {
            $('.nodata').css('display', 'block');
        } else {
            $.each(data.rankList, function (index, item) {

                var sex = (item.gender == 0) ? "female" : "male";
                //var level = 'level' + item.level;

                var display = (index == 0) ? 'block' : 'none';
                //加载礼物tab
                var sel = (index == 0) ? 'tab_sel' : '';
                var percent = index * 100;
                $('.swiper-wrapper').append('<div onclick="tabEvent(this)" id="' + index + '" data-id="' + item.giftId + '" class="swiper-slide tab_item ' + sel + '">' + item.name.replace('幸运', '') + '</div>');

                var html = '<div class="ct_top">'
                    + '<a href="javascript:;" onclick="live.ShowCard(this,' + item.useridx + ')">'
                    + '    <div class="region">'
                    + '        <div class="head">'
                    + '            <div class="vary_gift"><img src="' + item.icon + '" /></div>'
                    + '            <div class="in">'
                    + '                <b></b>'
                    + '                <img src="' + item.smallpic + '" onerror="javascript:this.src=http://liveimg.9158.com/default.png" class="h_img" />'
                    + '            </div>'
                    + '        </div>'
                    + '        <div class="these">'
                    + '            <div class="in">'
                    + '                <span class="name">' + item.myname + '</span>'
                    + '                <span class="' + sex + '"></span>'
                    + '                <span>'
                    + '                    <div class="the_level">'
                    + '                        <span class="level level' + item.level + '"></span>'
                    + '                        <span class="num_bg bg' + live.GetGradeRank(item.grade) + '"></span>'
                    + '                        <span class="num">' + item.grade + '</span>'
                    + '                    </div>'
                    + '                </span>'
                    + '            </div>'
                    + '            <div class="txt">数量：' + item.totalNum + '</div>'
                    + '        </div>'
                    + '    </div>'
                    + '</a>'
                + '</div><H5></H5>';
                //html = template('Top_template', data);

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

        //output thisWeek ranking and myRank ranking
        var html = template('rank_con_templete', data);

        $('.miao_rank').eq(StarRank.tabIndex).append(html);
        $('.loading').remove();

        StarRank.isLoading = false;
    },
    //LoadMyRankHtml: function (myRank) {
    //    var photo = getCookie('photo');

    //    var html = template('myRank_con_template', myRank);

    //    $('.miao_rank').eq(StarRank.tabIndex).append(html);
    //},
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

                $('.content').append('<div class="loading">Loading...</div>');
            },
            success: function (response) {
                StarRank.tabArrary[StarRank.tabIndex].pageEnd = response.data.totalPage;
                StarRank.tabArrary[StarRank.tabIndex].page++;

                if (StarRank.tabArrary[StarRank.tabIndex].page > StarRank.tabArrary[StarRank.tabIndex].pageEnd) {
                    StarRank.tabArrary[StarRank.tabIndex].loadEnd = true;
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

///礼物标签页切换
function tabEvent(obj) {
    //防止当前标签数据还未加载完成去点击其他标签数据加载错位问题
    if (StarRank.isLoading == true) {
        alert('请等待当前操作完成...');
        return;
    }

    StarRank.tabIndex = obj.getAttribute('id');
    StarRank.giftid = obj.getAttribute('data-id');

    var percent = -(StarRank.tabIndex * 100);

    $(obj).addClass('tab_sel').siblings('.tab_item').removeClass('tab_sel');
    $('.miao_rank').eq(StarRank.tabIndex).show().siblings('.miao_rank').hide();
    $('.rank_con').css({ '-webkit-transform': 'translate3d(' + percent + '%, 0px, 0px)', '-webkit-transition': '300ms' });

    //如果当前页面数据没有请求完则不去调用下一个标签页数据
    if (!StarRank.tabArrary[StarRank.tabIndex].loadEnd) {

        StarRank.Data.LoadtswkStarData(StarRank.giftid);
    }
}