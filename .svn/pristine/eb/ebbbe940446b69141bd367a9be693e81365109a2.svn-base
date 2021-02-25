/// <reference path="common.js" />

/*!
 *RoomInRank v1.0
 *Aurhor:zhaorui 
 *Time:2016-12-10 
 *Copyright 2016
 */

var scale = $(window).width() / 640;
$('html').css('font-size', 120 * scale + '%');

var tabIndex = 0;
var pagesize = 20;
var Rank = {
    version: "1.0",
    useridx: getQueryStr('useridx'),
    roomid: getQueryStr('roomid')
}
var tabArrary = [{ page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false }]

$(function () {
    //上拉列表容器
    var dropload = $('.rank_con').dropload({
        scrollArea: window,
        distince: 50,
        domDown: {
            domClass: 'dropload-down',
            domRefresh: '<div class="dropload-refresh">↑上拉加载更多</div>',
            domLoad: '<div class="dropload-load"><span class="loading"></span>正在加载数据...</div>',
            domNoData: '<div class="dropload-noData">已经到底了</div>'
        },
        loadDownFn: function (me) {

            var param = { type: tabIndex, useridx: Rank.useridx, roomid: Rank.roomid, pagesize: pagesize, page: tabArrary[tabIndex].page };
            $.ajax({
                timeout: 6000, //超时时间设置，单位毫秒
                type: 'GET',
                dataType: 'json',
                url: '/Rank/RoomRank',
                data: param,
                beforeSend: function () {
                    //layer.open({
                    //    type: 2
                    //});
                },
                success: function (d) {
                    //tabArrary[tabIndex].total = d.data.totalCount;
                    tabArrary[tabIndex].pageEnd = d.data.totalPage;
                    tabArrary[tabIndex].page++;

                    if (tabArrary[tabIndex].page > tabArrary[tabIndex].pageEnd) {
                        tabArrary[tabIndex].loadEnd = true;
                        me.lock();// 锁定

                        me.noData();// 无数据
                    }

                    Rank.Logic.LoadRankHtml(tabIndex, d.data);

                    me.resetload();
                },
                error: function (xhr, type) {
                    me.lock();
                    me.noData();
                    me.resetload();
                }
            });
        }
    });//dropload

    Rank.Logic = {
        LoadRankHtml: function (ranktype, data) {
            var list = data.rankList;
            var totalPage = data.totalPage;

            var str = '';
            if (totalPage <= 0) {
                str = '<div class="nodata">排行榜暂无数据</div>';
                $(".dropload-down").css("display", "none");
            } else {
                $.each(list, function (index, item) {
                    console.log(item.pos + ',' + item.useridx);
                   
                    var row_no = item.pos;
                    var sex_Css = (item.gender == 0) ? "female" : "male";
                    var item_Css = '';
                    var icon_Css = '';
                    var level_Css = 'level' + item.level;
                    var total_text = Rank.Logic.GetRankText(ranktype, item.sumprice);

                    if (row_no > 0 && row_no < 4) {
                        item_Css = 'item' + row_no;
                        icon_Css = 'icon' + row_no;
                        //row_no = '';
                    }

                    str += '<div class="item ' + item_Css + '">'
                        + '<a href="javscript:;" onclick="live.ShowCard(this,' + item.useridx + ')">'
                        + '<div class="icon ' + icon_Css + '">' + row_no + '</div>'
                        + '      <div class="the_img"><div class="img"></div>'
                        + '          <div class="img_img"><img class="imgLoading" src="http://liveimg.9158.com/pixel.gif" data-echo=' + item.smallpic + '></div></div>'
                        + '       <div class="name">'
                        + '            <span class="name_name">' + item.myname + '</span>'
                        + '            <span class="' + sex_Css + '"></span>'
                        + '            <div class="the_level">'
                        + '                 <span class="level ' + level_Css + '"></span>'
                        + '                 <span class="num_bg bg' + live.GetGradeRank(item.grade) + '"></span>'
                        + '                 <span class="num">' + item.grade + '</span>'
                        + '            </div>'
                        + '            <div class="food">' + total_text + ' </div>'
                        + '        </div>'
                        + '</a></div>'
                });
            }

            $('.miao_rank').eq(ranktype).append(str);

            echo.init({
                offset: 100,//离可视区域多少像素的图片可以被加载
                throttle: 0//设置图片延迟加载的时间 
            });
        },
        GetRankText: function (type, total) {
            var html = '';
            if (tabIndex == 0) {
                html = '奉献：' + total + '喵币';
            } else if (tabIndex == 1) {
                html = '分享：' + total + '次';
            } else {
                html = '观看：' + Rank.Logic.GetWatchTime(total);
            }
            return html;
        }
    }
});//$(jquery)
