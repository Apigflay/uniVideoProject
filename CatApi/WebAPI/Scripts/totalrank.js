/// <reference path="lib/jquery-1.8.2.min.js" />
/*!
 * JavaScript totalrank v1.0
 * Copyright (c) 2017 ZhaoRui
 * Created: 2017-5-11
 */

//var tabNum = $('.tab').children().length;
//$('.tab_item').css('width', 96 / tabNum + '%');

var Rank = {
    version: "1.0",
    isLoading: true,
    tabIndex: $('.tab .tab_sel').attr('id') - 1,//0
    tabid: $('.tab .tab_sel').attr('data-id'),//Rank.Config.ranktype == 1 ? 4 : 2;//如果当前榜是消费榜默认时间类型是日榜其他则是周榜开始,
    tabArrary: [{ page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false }]
}
Rank.Config = {
    ranktype: getQueryStr('ranktype'),/*1:消费，2：主播，3：家族，4：周星*/
    useridx: getQueryStr('useridx'),
    photo: getQueryStr('photo'),
    showtype: getQueryStr('showtype'),/*hall,*/

    tabNum: $('.tab').children().length,
}
//var tabIndex = 0;
//var tabid = 4;//Rank.Config.ranktype == 1 ? 4 : 2;//如果当前榜是消费榜默认时间类型是日榜其他则是周榜开始
//var isLoading = true;//正在请求数据

//注册jquery事件，等到页面加载完毕时才执行
//DOM加载完成以后才执行里面操作
$(function () {
    $('#menu' + Rank.Config.ranktype).addClass('sel').siblings('li').removeClass('sel');
    $('.tab_item').css('width', 96 / Rank.Config.tabNum + '%');
    $('.miao_rank').eq(Rank.tabIndex).show().siblings('.miao_rank').hide();

    //var percent = -(Rank.tabIndex * 100);

    //$('#rank_con').css({ '-webkit-transform': 'translate3d(' + percent + '%, 0px, 0px)', '-webkit-transition': '300ms' });


    //时间选项卡切换
    $('.tab .tab_item').on('click', function () {
        $('body,html').animate({ scrollTop: 0 }, 100);//返回页面顶部（ps要求增加）

        //防止当前标签数据还未加载完成去点击其他标签数据加载错位问题
        if (Rank.isLoading == true) {
            return;
        }
        var $this = $(this);

        Rank.tabIndex = $this.index();
        Rank.tabid = $this.attr("data-id");
        console.log(Rank.Config.ranktype);

        $(this).addClass('tab_sel').siblings('.tab_item').removeClass('tab_sel');
        $('.miao_rank').eq(Rank.tabIndex).show().siblings('.miao_rank').hide();

        var percent = -(Rank.tabIndex * 100);
        $('#rank_con').css({ '-webkit-transform': 'translate3d(' + percent + '%, 0px, 0px)', '-webkit-transition': '300ms' });
        $('.dropload-up,.dropload-down,.dropload-refresh').css({ 'left': -percent + '%' });

        if (!Rank.tabArrary[Rank.tabIndex].loadEnd) {
            dropload.unlock();
            dropload.noData(false);
        } else {
            dropload.lock('down');
            dropload.noData();
        }
        dropload.resetload();
    });

    //上拉列表容器
    var dropload = $('.rank_con').dropload({
        scrollArea: window,//滑动区域
        autoLoad: true,//自动加载
        distince: 150,//拉动距离
        threshold: 150,//提前加载距离,加载区高度2/3
        domDown: {
            domClass: 'dropload-down',
            domRefresh: '<div class="dropload-refresh">上拉加载更多</div>',
            domLoad: '<div class="dropload-load"><span class="loading"></span>正在加载数据中...</div>',
            domNoData: '<div class="dropload-noData">我是有底线的啦！</div>'
        },
        loadDownFn: function (me) {
            var week = new Date().getDay();

            var param = { ranktype: Rank.Config.ranktype, timetype: Rank.tabid, useridx: Rank.Config.useridx, page: Rank.tabArrary[Rank.tabIndex].page };
            $.ajax({
                timeout: 8000, //超时时间设置，单位毫秒
                type: 'POST',
                dataType: 'json',
                url: '/Rank/rankData',
                data: param,
                beforeSend: function () {
                    Rank.isLoading = true;

                    if (Rank.tabArrary[Rank.tabIndex].page == 1 && week == 1) {
                        layer.open({
                            type: 2,
                            content: '加载中,么么哒~',
                            shadeClose: false//是否触摸遮罩层关闭
                        });
                    }
                },
                success: function (response) {

                    Rank.tabArrary[Rank.tabIndex].pageEnd = response.data.totalPage;
                    Rank.tabArrary[Rank.tabIndex].page++;

                    if (Rank.tabArrary[Rank.tabIndex].page > Rank.tabArrary[Rank.tabIndex].pageEnd) {
                        Rank.tabArrary[Rank.tabIndex].loadEnd = true;//所有数据加载完成之后调用dropload的锁定，和无数据方法。
                        me.lock();  // 智能锁定，锁定上一次加载的方向//me.lock('up')
                        me.noData();// 无数据
                    }

                    //如果不处理3个排行榜周一特效展示样式会一样
                    if (week == 1) {
                        Rank.tabIndex = Rank.Config.ranktype - 1;
                        $('.miao_rank').eq(Rank.tabIndex).css('left', '0').show().siblings('.miao_rank').hide();
                    }

                    setTimeout(function () {
                        Rank.Logic.LoadRankHtml(Rank.tabIndex, response.data);

                        me.resetload();// 每次数据加载完，必须重置
                        layer.closeAll('loading');
                    }, 700);
                },
                error: function (xhr, type) {
                    me.lock();
                    me.noData();
                    me.resetload();

                    Rank.isLoading = false;
                    if (type == 'timeout') {
                        $('.dropload-noData').html('操作超时，请稍后再试！');
                    }

                }
            });
        }
    });//dropload
});//$(jquery)


Rank.Logic = {
    LoadRankHtml: function (ranktype, data) {
        var ranklist = data.rankList;
        var totalCount = data.totalCount;
        var str = '';

        if (totalCount > 0) {

            $.each(ranklist, function (i, item) {
                var sex = item.gender == 1 ? "male" : "female";
                var itemCss = '', iconCss = '', familyCss = 'name_name2';
                var row_no = item.pos;
                var followHtml = '', familyHtml = '', changeHtml = '';
                var total_text = Rank.Logic.GetTotalText(ranktype, item.sumprice);

                if (row_no > 0 && row_no < 4) {
                    itemCss = 'item' + row_no;
                    iconCss = 'icon' + row_no;
                    row_no = '';
                }
                //如果当前榜是消费榜并且当前时间类型是日榜时才显示排名变化
                if (Rank.tabid == 1) {
                    if (item.rankChange == 1) {
                        changeHtml = '<div class="up"></div>';
                    } else {
                        changeHtml = '<div class="down"></div>';
                    }
                }
                //家族排行榜不显示姓名，皇冠等级
                if (Rank.Config.ranktype != 3) {
                    familyHtml = '<span class="' + sex + '"></span>'
                        + '       <div class="the_level">'
                        + '            <span class="level level' + item.level + '"></span>'
                        + '            <span class="num_bg bg' + live.GetGradeRank(item.grade) + '"></span>'
                        + '            <span class="num">' + item.grade + '</span>'
                        + '       </div>'
                    familyCss = "";
                }

                str += '<div class="item ' + itemCss + '">'
                    + '<a href="javscript:;" onclick="live.ShowCard(this,' + item.useridx + ')">'
                    + '<div class="icon ' + iconCss + '">' + row_no + '</div>'
                    + changeHtml
                    + '      <div class="the_img"><div class="img"></div>'
                    + '          <div class="img_img"><img class=\"imgLoading\" onerror="javascript:this.src=http://liveimg.9158.com/default.png" src="http://liveimg.9158.com/pixel.gif" data-echo=' + item.smallpic + '></div></div>'
                    + '       <div class="name">'
                    + '            <span class="name_name ' + familyCss + '">' + item.myname + '</span>'
                    + familyHtml
                    + '            <div class="food">' + total_text + '</div>'
                    //+ followHtml
                    + '        </div>'
                    + '</a></div>'
            });
        } else {
            str = '<div class="nodata"><span class="img"></span>程序喵熬夜统计数据中...</div>';
            $(".dropload-down").css("display", "none");
        }

        /*数据加载完显示提示*/
        //if (Rank.tabArrary[Rank.tabIndex].loadEnd) {
        if (Rank.tabArrary[Rank.tabIndex].page > Rank.tabArrary[Rank.tabIndex].pageEnd) {
            console.log('加载完毕')
            str += '<div class="rank_text"><span>每周消费Top3的土豪可获得下周1的封神榜展示特权1整天哦！</span>'
            + '<span class="update">该榜单于每周一 3：00更新显示上一周的排名</span></div>';
        }
        //console.log(Rank.tabArrary[Rank.tabIndex].loadEnd);

        $('.miao_rank').eq(ranktype).append(str);
        Rank.isLoading = false;

        echo.init({
            offset: 0,
            throttle: 0
        });
    },
    GetTotalText: function (ranktype, total) {
        var total_text = '';

        if (total >= 100000000) {
            total = (total / 100000000).toFixed(2) + '亿';
        }
        else if (total >= 10000) {
            total = (total / 10000).toFixed(2) + '万';
        }

        if (Rank.Config.ranktype == 1) {

            total_text = '消费：' + total;
        } else {
            total_text = '猫粮：' + total;
        }
        return total_text;
    }
}
