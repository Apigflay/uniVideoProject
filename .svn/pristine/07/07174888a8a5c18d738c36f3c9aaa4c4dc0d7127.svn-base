var tabIndex = 0;
var tabid = 6;//默认要展示排行榜当前为日榜获取标签对应的data-id 属性
var isLoading = true;
var Rank = {
    version: "1.0",
    tabArrary: [{ page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false }]
}

Rank.Config = {
    pixel: "http://liveimg.9158.com/pixel.gif",
    mbpic: "http://liveimg.9158.com/default.png",
    showtype: getQueryStr('showtype'),
    curuseridx: getQueryStr('curuseridx'),
    useridx: getQueryStr('useridx'),
}

$(function () {

    //templete 模板方法调用
    template.helper('GetTotalText', function (total) {
        return Rank.Logic.GetTotalText(tabid, total);
    });

    template.helper('GetGradeRank', function (grade) {
        return live.GetGradeRank(grade);
    });


    //自己排行榜不显示自己，分享出去的页面不显示我的排名
    if (Rank.Config.curuseridx == 0 || Rank.Config.useridx == Rank.Config.curuseridx) {
        $('.myRank_con').css('display', 'none');
        $('.myRank_con').hide();
    }
    if (Rank.Config.curuseridx == 0) {
        $('.fix_bottom').show();
    }

    $('.tab .tab_item').on('click', function () {
        $('body,html').animate({ scrollTop: 0 }, 100);//返回页面顶部（ps要求增加）

        var $this = $(this);
        if (isLoading == true) {
            return;
        }
        tabIndex = $this.index();
        tabid = $this.attr("data-id");

        $(this).addClass('tab_sel').siblings('.tab_item').removeClass('tab_sel');

        $('.miao_rank').eq(tabIndex).show().addClass('fadeIn').siblings('.miao_rank').hide().removeClass('fadeIn');
        $('.myRank').eq(tabIndex).show().siblings('.myRank').hide();

        if (!Rank.tabArrary[tabIndex].loadEnd) {
            dropload.unlock();
            dropload.noData(false);
        } else {
            dropload.lock('down');
            dropload.noData();
        }

        dropload.resetload();
    });

    var dropload = $('.rank_con').dropload({
        scrollArea: window,
        distince: 100,
        threshold: 150,
        domDown: {
            domClass: 'dropload-down',
            domRefresh: '<div class="dropload-refresh">↑上拉加载更多</div>',
            domLoad: '<div class="dropload-load"><span class="loading"></span>Loading...</div>',
            domNoData: '<div class="dropload-noData"></div>'
        },
        //上拉列表容器
        loadDownFn: function (me) {

            Rank.Data.GetRankListData(me);
        }
    });

});//$(jquery)

Rank.Data = {
    GetRankListData: function (me) {
        var uidx = Rank.Config.useridx;
        var loginidx = Rank.Config.curuseridx;
        var param = { ranktype: tabid, tabindex: tabIndex, useridx: uidx, curuseridx: loginidx, page: Rank.tabArrary[tabIndex].page };

        $.ajax({
            timeout: 6000,
            type: 'POST',
            dataType: 'json',
            url: '/Rank/UserRankData',
            data: param,
            beforeSend: function () {
                isLoading = true;
            },
            success: function (response) {
                Rank.tabArrary[tabIndex].pageEnd = response.data.totalPage;
                Rank.tabArrary[tabIndex].page++;

                //After All Data Load
                if (Rank.tabArrary[tabIndex].page > Rank.tabArrary[tabIndex].pageEnd) {
                    Rank.tabArrary[tabIndex].loadEnd = true;
                    me.lock();// 锁定
                    me.noData();// 无数据
                }

                Rank.Logic.LoadRankHtml(tabid, response.data);
                Rank.Logic.LoadMyRankHtml(tabid, response.data);

                me.resetload();
            },
            error: function (xhr, type) {
                me.lock();
                me.noData();
                me.resetload();
            }
        });
    }
};

Rank.Logic = {
    LoadRankHtml: function (ranktype, data) {
        var html = '';
        var time = new Date();

        if (data.totalCount <= 0) {
            if (Rank.useridx == Rank.curuseridx) {
                html = '<div class="nodata"><span class="img"></span>你的排行榜无人守护，快去开播吧</div>';
            }
            else if (time.getDate() == 1 && ranktype == 2) {
                html = '<div class="nodata"><span class="img"></span>程序喵玩命统计数据中...</div>';
            }
            else {
                html = '<div class="nodata"><span class="img"></span>快来成为TA的铁杆粉丝</div>';
            }

        } else {
            html = template('rank_con_templete', data);
        }

        $('.miao_rank').eq(tabIndex).append(html);
        
        echo.init({
            offset: 100,//离可视区域多少像素的图片可以被加载
            throttle: 0//设置图片延迟加载的时间
        });
        isLoading = false;
    },
    LoadMyRankHtml: function (ranktype, data) {

        var html = template('myrank_con_templete', data.myRankData);

        $('.myRank').eq(tabIndex).html(html);
    },
    GetTotalText: function (ranktype, total) {
        var total_text = '';

        var time = '';
        if (total < 60) {
            time = total + '分钟';
        } else if (total >= 60) {
            time = Math.floor(total / 60) + '小时';
        }

        if (tabid == 4) {
            total_text = '分享：' + total + '次';
        } else if (tabid == 5) {
            total_text = '观看：' + time;
        } else {
            if (total >= 100000000) {
                total = (total / 100000000).toFixed(2) + '亿';
            }
            else if (total >= 10000) {
                total = (total / 10000).toFixed(2) + '万';
            }
            total_text = '流水：' + total;
        }
        return total_text;
    }
}

//上下滑动操作
//Rank.Logic.MenuIn();