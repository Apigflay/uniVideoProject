/// <reference path="common.js" />
/// <reference path="lib/jquery-1.8.2.min.js" />
/// <reference path="totalrank_v3.js" />

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

//注册jquery事件，等到页面加载完毕时才执行
//DOM加载完成以后才执行里面操作(页面第一次加载)
$(function () {
    $('#menu' + Rank.Config.ranktype).addClass('sel').siblings('li').removeClass('sel');//排行榜类型选中状态
    $('.tab_item').css('width', 96 / Rank.Config.tabNum + '%');//动态调整排行榜时间类型 item宽度
    $('.tab_item').eq(0).addClass('tab_sel');//
    //$('.miao_rank').eq(Rank.tabIndex).show().siblings('.miao_rank').hide();

    //时间选项卡切换
    $('.tab .tab_item').on('click', function () {

        $('body,html').animate({ scrollTop: 0 }, 100);//返回页面顶部（ps要求增加）
        //$('.miao_rank').eq(Rank.tabIndex).empty().siblings('.miao_rank').empty();
        //防止当前标签数据还未加载完成去点击其他标签数据加载错位问题
        if (Rank.isLoading == true) {
            return;
        }
        var $this = $(this);

        Rank.tabIndex = $this.index();
        Rank.tabid = $this.attr("data-id");

        $(this).addClass('tab_sel').siblings('.tab_item').removeClass('tab_sel');
        $('.miao_rank').eq(Rank.tabIndex).show().siblings('.miao_rank').hide();
        loadtop3();
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
        var week = new Date().getDay();

        var param = { ranktype: Rank.Config.ranktype, timetype: Rank.tabid, useridx: Rank.Config.useridx, page: Rank.tabArrary[Rank.tabIndex].page };
        $.ajax({
            timeout: 8000, //超时时间设置，单位毫秒
            type: 'POST',
            dataType: 'json',
            url: '/Rank/meuRankData',
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

                setTimeout(function () {
                    Rank.Logic.LoadRankHtml(Rank.tabIndex, response.data);

                    me.resetload();// 每次数据加载完，必须重置
                    //layer.closeAll('loading');
                }, 700);
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
});//dropload

function loadtop3()
{
    var week = new Date().getDay();
    var param = { ranktype: Rank.Config.ranktype, timetype: Rank.tabid, useridx: Rank.Config.useridx, page: Rank.tabArrary[Rank.tabIndex].page };
     $.ajax({
        timeout: 8000, //超时时间设置，单位毫秒
        type: 'POST',
        dataType: 'json',
        url: '/Rank/meuRankData',
        data: param,
        beforeSend: function () {
            Rank.isLoading = true;
        },
        success: function (response) { 
                //Rank.Logic.LoadRankHtml(Rank.tabIndex, response.data); 
            //layer.closeAll('loading'); 
                var ranklist = response.data.rankList;
                var totalCount = response.data.totalCount;
                var ranktype = Rank.tabIndex;
                var top1 = "";
                var top2 = "";
                var top3 = "";
                $('.top3tab').empty();
                $.each(ranklist, function (i, item) {
                    if (item.row == 2) {
                        var rankNum1 = Rank.Logic.GetTotalText(ranktype, item.rankNum);
                        top1 += '<div class="tab_item">'
                            + ' <div class="row' + item.row + '">'
                            + '          <div class="huangguan"  ><img src="http://img.imeyoo.com/pic/rank/皇冠' + item.row + '.png"> </div>'
                            + '<div class="row' + item.row + 'pic"></div>'
                            + '      <div class="the_img"><div class="img"></div>'
                            + '          <div class="img_img"><img class=\"imgLoading\" onerror="javascript:this.src=http://liveimg.9158.com/default.png" src="http://liveimg.9158.com/pixel.gif" data-echo=' + item.smallpic + '></div></div>'
                            + '<div class="row' + item.row + 'Name">' + item.myName + '</div>'
                            + '<div class="row' + item.row + 'Flow">' + rankNum1 + '</div>'
                            + '</div></div>'
                    }

                });
                $.each(ranklist, function (i, item) {
                    if (item.row < 4 && item.row != 2) {
                        var rankNum1 = Rank.Logic.GetTotalText(ranktype, item.rankNum);
                        top1 += '<div class="tab_item">'
                            + ' <div class="row' + item.row + '">'
                            + '          <div class="huangguan"  ><img src="http://img.imeyoo.com/pic/rank/皇冠' + item.row + '.png"> </div>'
                            + '<div class="row' + item.row + 'pic"></div>'
                            + '      <div class="the_img"><div class="img"></div>'
                            + '          <div class="img_img"><img class=\"imgLoading\" onerror="javascript:this.src=http://liveimg.9158.com/default.png" src="http://liveimg.9158.com/pixel.gif" data-echo=' + item.smallpic + '></div></div>'
                            + '<div class="row' + item.row + 'Name">' + item.myName + '</div>'
                            + '<div class="row' + item.row + 'Flow">' + rankNum1 + '</div>'
                            + '</div></div>'
                    }

                });
                $('.top3tab').append(top1);
                Rank.isLoading = false;

                echo.init({
                    offset: 0,
                    throttle: 0
                });
               
        }, 
    });
}
Rank.Logic = {
    createLiveFlagEle: function (item) {
        var html = item.isOnline == 1 ? '<div class="live"><span class="livetext">直播中</span></div>' : '';
        return html;
    },
    createRankChangeEle: function (item) {
        var html = item.rankChange == 1 ? '<div class="up"></div>' : '<div class="down"></div>';
        return html;
    },
    createLevelEle: function (item) {
        var sexClass = item.gender == 1 ? "male" : "female";
        var html = '';

        html += '<span class="' + sexClass + '"></span>'
            + '  <div class="the_level">'
            + '       <span class="level level' + item.level + '"></span>'
            + '       <span class="num_bg bg' + live.GetGradeRank(item.grade) + '"></span>'
            + '       <span class="num">' + item.grade + '</span>'
            + '  </div>';

        return html;
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
            total_text = lbl_consume + '：' + total;
            //total_text = '<span class="lbl_consume">消费：</span>' + total;
        } else {
            total_text = lbl_catfood + '：' + total;
            //total_text = '<span class="lbl_catfood">猫粮：</span>' + total;
        }
        return total_text;
    },
    LoadRankHtml: function (ranktype, data) {
        var ranklist = data.rankList;
        var totalCount = data.totalCount;
        var str = '';

        //if (totalCount > 0) {
        //    $.each(ranklist, function (i, item) {
        //        var sex = item.gender == 1 ? "male" : "female";
        //        var itemCss = '', iconCss = '', familyCss = 'name_name2';
        //        var row_no = item.pos;
        //        var followHtml = '', familyHtml = '', changeHtml = '', liveHtml = '';
        //        var total_text = Rank.Logic.GetTotalText(ranktype, item.sumprice);

        //        //前3名样式
        //        if (row_no > 0 && row_no < 4) {
        //            itemCss = 'item' + row_no;
        //            iconCss = 'icon' + row_no;
        //            row_no = '';
        //        }
        //        //如果当前榜是消费榜并且当前时间类型是日榜时才显示排名变化
        //        if (Rank.tabid == 1) {
        //            changeHtml = Rank.Logic.createRankChangeEle(item);
        //        }
        //        //家族排行榜不显示姓名，皇冠等级
        //        if (Rank.Config.ranktype != 3) {
        //            familyCss = '';
        //            familyHtml = Rank.Logic.createLevelEle(item);
        //        }
        //        //针对主播排行榜显示是否在线标识
        //        if (Rank.Config.ranktype == 2) {
        //            liveHtml = Rank.Logic.createLiveFlagEle(item);
        //        }

        //        str += '<div class="item ' + itemCss + '">'
        //            + '<a href="javscript:;" onclick="live.ShowCard(this,' + item.useridx + ')">'
        //            + '<div class="icon ' + iconCss + '">' + row_no + '</div>'
        //            + changeHtml
        //            + '      <div class="the_img"><div class="img"></div>'
        //            + '          <div class="img_img"><img class=\"imgLoading\" onerror="javascript:this.src=http://liveimg.9158.com/default.png" src="http://liveimg.9158.com/pixel.gif" data-echo=' + item.smallpic + '></div></div>'
        //            + '      <div class="name">'
        //            + '          <span class="name_name ' + familyCss + '">' + item.myname + '</span>'
        //            + familyHtml
        //            + '          <div class="food">' + total_text + '</div>'
        //            + '      </div>'
        //            + liveHtml
        //            + '</a>'
        //            + '</div>'
        //    });
        //} else {
        //    str = '<div class="nodata"><span class="img"></span>程序喵熬夜统计数据中...</div>';
        //    $(".dropload-down").css("display", "none");
        //}
        var top1 = "";
        var top2 = "";
        var top3 = "";
        $('.top3tab').empty();
        $.each(ranklist, function (i, item) {
            if (item.row == 2) {
                var rankNum1 = Rank.Logic.GetTotalText(ranktype, item.rankNum);
                top1 += '<div class="tab_item">'
                    + ' <div class="row' + item.row + '">'
                    + '          <div class="huangguan"  ><img src="http://img.imeyoo.com/pic/rank/皇冠' + item.row + '.png"> </div>'
                    + '<div class="row' + item.row + 'pic"></div>'
                    + '      <div class="the_img"><div class="img"></div>'
                    + '          <div class="img_img"><img class=\"imgLoading\" onerror="javascript:this.src=http://liveimg.9158.com/default.png" src="http://liveimg.9158.com/pixel.gif" data-echo=' + item.smallpic + '></div></div>'
                    + '<div class="row' + item.row + 'Name">' + item.myName + '</div>'
                    + '<div class="row' + item.row + 'Flow">' + rankNum1 + '</div>'
                    + '</div></div>'
            }

        });
        $.each(ranklist, function (i, item) {
            if (item.row < 4 && item.row != 2) {
                var rankNum1 = Rank.Logic.GetTotalText(ranktype, item.rankNum);
                top1 +='<div class="tab_item">'
                    + ' <div class="row' + item.row + '">'
                    + '          <div class="huangguan"  ><img src="http://img.imeyoo.com/pic/rank/皇冠' + item.row + '.png"> </div>'
                    + '<div class="row' + item.row + 'pic"></div>'
                    + '      <div class="the_img"><div class="img"></div>'
                    + '          <div class="img_img"><img class=\"imgLoading\" onerror="javascript:this.src=http://liveimg.9158.com/default.png" src="http://liveimg.9158.com/pixel.gif" data-echo=' + item.smallpic + '></div></div>'
                    + '<div class="row' + item.row + 'Name">' + item.myName + '</div>'
                    + '<div class="row' + item.row + 'Flow">' + rankNum1 + '</div>'
                    +'</div></div>'
            }

        });
        $('.top3tab').append(top1);

        //$('#top3').eq(ranktype).append(str);
        if (totalCount > 0) {
            if (Rank.Config.ranktype != 3) {
                str = template('rank_con_templete', data);
            } else {
                str = template('familyRank_con_templete', data);
            }
        } else {
            str = '<div class="nodata"><span class="img"></span>程序喵熬夜统计数据中...</div>';
            $(".dropload-down").css("display", "none");
        }

        /*数据加载完显示提示*/
        //if (Rank.tabArrary[Rank.tabIndex].page > Rank.tabArrary[Rank.tabIndex].pageEnd && Rank.tabid == 2) {
        //    str += '<div class="rank_text"><span>每周消费Top3的土豪可获得下周1的封神榜展示特权1整天哦！</span>'
        //    + '<span class="update">该榜单于每周一 3：00更新显示上一周的排名</span></div>';
        //}

        $('.miao_rank').eq(ranktype).append(str);
        //如果当前榜是消费榜并且当前时间类型是日榜时才显示排名变化
        if (Rank.tabid != 1) {
            $('.miao_rank .down').eq(ranktype).css('display', 'none');
        }

        Rank.isLoading = false;

        echo.init({
            offset: 0,
            throttle: 0
        });
    }
   
}