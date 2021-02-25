/// <reference path="common.js" />
/// <reference path="lib/jquery-1.8.2.min.js" />

/*!
 * JavaScript totalrank v2.0
 * Copyright (c) 2017 ZhaoRui
 * Created: 2017-7-25
 * 日榜在前版本
 */
var Rank = {
    version: "1.0",
    isLoading: true,
    tabIndex: 0,//0
    tabid: $('.tab .tab_item').eq(0).attr('data-id'),//获取第一个排行榜时间类型的值
    tabArrary: [{ page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false }]
};
Rank.Config = {
    ranktype: getQueryStr('ranktype'),/*1:消费，2：主播，3：家族，4：周星*/
    useridx: getQueryStr('useridx'),
    photo: getQueryStr('photo'),
    showtype: getQueryStr('showtype'),/*hall,*/

    tabNum: $('.tab').children().length,
};

$('.tab_item').css('width', 95 / Rank.Config.tabNum + '%');//动态调整排行榜时间类型 item宽度

//注册jquery事件，等到页面加载完毕时才执行
//DOM加载完成以后才执行里面操作(页面第一次加载)
$(function () {
    $('.tab_item').eq(0).addClass('tab_sel');//
    $('.miao_rank').eq(Rank.tabIndex).show().siblings('.miao_rank').hide();

    //时间选项卡切换
    $('.tab .tab_item').on('click', function () {

        //$('body,html').animate({ scrollTop: 0 }, 100);//返回页面顶部（ps要求增加）

        //防止当前标签数据还未加载完成去点击其他标签数据加载错位问题
        if (Rank.isLoading == true) {
            console.log('正在加载数据中...')
            return;
        }
        var $this = $(this);

        Rank.tabIndex = $this.index();
        Rank.tabid = $this.attr("data-id");

        $(this).addClass('tab_sel').siblings('.tab_item').removeClass('tab_sel');
        $('.miao_rank').eq(Rank.tabIndex).show().siblings('.miao_rank').hide();

        var percent = -(Rank.tabIndex * 100);

        //$('#list-wrapper').css({ '-webkit-transform': 'translate3d(' + percent + '%, 0px, 0px)', '-webkit-transition': '300ms' });
        //$('.dropload-up,.dropload-down,.dropload-refresh').css({ 'left': -percent + '%' });

        if (!Rank.tabArrary[Rank.tabIndex].loadEnd) {
            dropload.unlock();
            dropload.noData(false);
        } else {
            dropload.lock('down');
            dropload.noData();
        }
        dropload.resetload();
    });

    //templete 模板方法调用
    template.helper('GetTotalText', function (total) {
        return Rank.Logic.GetTotalText(1, total);
    });

    template.helper('GetGradeRank', function (grade) {
        return live.GetGradeRank(grade);
    });

});//$(jquery)

//上拉列表容器
var dropload = $('.rank_con').dropload({
    scrollArea: window,//滑动区域
    autoLoad: true,//自动加载
    distince: 150,//拉动距离
    threshold: 150,//提前加载距离,加载区高度2/3
    domUp: {
        domClass: 'dropload-up',
        domRefresh: '<div class="dropload-refresh">↓下拉刷新</div>',
        domUpdate: '<div class="dropload-update">↑释放更新</div>',
        domLoad: '<div class="dropload-load">○加载中...</div>'
    },
    domDown: {
        domClass: 'dropload-down',
        domRefresh: '<div class="dropload-refresh">上拉加载更多</div>',
        domLoad: '<div class="dropload-load"><span class="loading"></span>Loading...</div>',
        domNoData: '<div class="dropload-noData"></div>'
    },
    //loadUpFn: function (me) {
    //    me.resetload();// 每次数据加载完，必须重置
    //    me.unlock();  // 智能锁定，锁定上一次加载的方向//me.lock('up')
    //    me.noData();// 无数据
    //},
    loadDownFn: function (me) {

        loadRankData(me);
    }
});//dropload

function loadRankData(me) {
    var param = {
        ranktype: Rank.Config.ranktype,
        timetype: Rank.tabid,
        useridx: Rank.Config.useridx,
        page: Rank.tabArrary[Rank.tabIndex].page
    };

    $.ajax({
        timeout: 8000, //超时时间设置，单位毫秒
        type: 'POST',
        dataType: 'json',
        url: '/Rank/totalRankData',
        data: param,
        beforeSend: function () {
            Rank.isLoading = true;
        },
        success: function (response) {

            Rank.tabArrary[Rank.tabIndex].pageEnd = response.data.totalPage;
            Rank.tabArrary[Rank.tabIndex].page++;

            if (Rank.tabArrary[Rank.tabIndex].page > Rank.tabArrary[Rank.tabIndex].pageEnd) {
                Rank.tabArrary[Rank.tabIndex].loadEnd = true;//所有数据加载完成之后调用dropload的锁定，和无数据方法。
                me.lock();  // 智能锁定，锁定上一次加载的方向//me.lock('up')
                me.noData();// 无数据
            }

            Rank.Logic.LoadRankHtml(Rank.tabIndex, response.data);

            me.resetload();// 每次数据加载完，必须重置
        },
        error: function (xhr, type) {
            me.lock();
            me.noData();
            me.resetload();

            Rank.isLoading = false;
            if (type == 'timeout') {
                $('.dropload-noData').html('Timeout！');
            }
        }
    });
}


Rank.Logic = {
    LoadRankHtml: function (ranktype, data) {
        var ranklist = data.rankList;
        var totalCount = data.totalCount;
        var html = '';

        if (totalCount > 0) {
            console.log(Rank.tabArrary[Rank.tabIndex].page);

            //消费，主播排行榜套用模板
            if (Rank.Config.ranktype != 3) {

                //排行榜前三名模板
                if (Rank.tabArrary[Rank.tabIndex].page == 2) {
                    html += template('rankTop_con_templete', data);
                }
            } else {
                //家族排行榜前三名模板
                if (Rank.tabArrary[Rank.tabIndex].page == 2) {
                    html += template('Top3_Family_con_template', data);
                }
            }
            html += template('rank_con_templete', data);
        } else {
            html = '<div class="nodata"><span class="img"></span>程序喵玩命统计数据中...</div>';
            $(".dropload-down").css("display", "none");
            $('.miao_rank').eq(ranktype).removeClass('content');
        }

        /*数据加载完显示提示*/
        if (Rank.tabArrary[Rank.tabIndex].page > Rank.tabArrary[Rank.tabIndex].pageEnd && Rank.tabid == 2) {
            html += '<div class="rank_text"><span>每周消费Top3的土豪可获得下周1的封神榜展示特权1整天哦！</span><span class="update">该榜单于每周一 3：00更新显示上一周的排名</span></div>';
        }

        $('.miao_rank').eq(ranktype).append(html);
        //如果当前榜是消费榜并且当前时间类型是日榜时才显示排名变化
        if (Rank.tabid != 1) {
            $('.u_state').css('display', 'none');
        }

        Rank.isLoading = false;

        echo.init({
            offset: 0,
            throttle: 0
        });
    },
    GetTotalText: function (ranktype, total) {
        var lbl_consume = '消费', lbl_catfood = '喵粮';
        var lang = (navigator.browserLanguage || navigator.language).toLowerCase().substr(0, 2);

        if (lang != 'zh') {
            lbl_consume = 'Consume';
            lbl_catfood = 'Catfood';
        }

        var total_text = '';

        if (total >= 100000000) {
            total = (total / 100000000).toFixed(2) + '亿';
        }
        else if (total >= 10000) {
            total = (total / 10000).toFixed(2) + '万';
        }

        if (Rank.Config.ranktype == 1) {
            total_text = lbl_consume + '：' + total;
        } else {
            total_text = lbl_catfood + '：' + total;
        }
        return total_text;
    }
}