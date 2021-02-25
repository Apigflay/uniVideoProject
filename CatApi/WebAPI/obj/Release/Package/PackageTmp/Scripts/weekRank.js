
//(function (a) { a.fn.scrollLoading = function (b) { var c = { attr: "data-url", container: a(window), callback: a.noop }; var d = a.extend({}, c, b || {}); d.cache = []; a(this).each(function () { var h = this.nodeName.toLowerCase(), g = a(this).attr(d.attr); var i = { obj: a(this), tag: h, url: g }; d.cache.push(i) }); var f = function (g) { if (a.isFunction(d.callback)) { d.callback.call(g.get(0)) } }; var e = function () { var g = d.container.height(); if (d.container.get(0) === window) { contop = a(window).scrollTop() } else { contop = d.container.offset().top } a.each(d.cache, function (m, n) { var p = n.obj, j = n.tag, k = n.url, l, h; if (p) { l = p.offset().top - contop, h = l + p.height(); if ((l >= 0 && l < g) || (h > 0 && h <= g)) { if (k) { if (j === "img") { f(p.attr("src", k)) } else { p.load(k, {}, function () { f(p) }) } } else { f(p) } n.obj = null } } }) }; e(); d.container.bind("scroll", e) } })(jQuery);

/*!
 *weekRank v1.0消费周榜
 *Author:zhaorui
 *Time:2016-7-21
 */

var scale = $(window).width() / 640;
$('html').css('font-size', 120 * scale + '%');

var tabIndex = 0;
var pagesize = 20;
var Rank = {
    version: "1.0"
}
var isLoading = true;//正在请求数据
var tabArrary = [{ page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false },
                 { page: 1, pageEnd: 0, total: 0, loadEnd: false }]
Rank.Config = {
    boyurl: "http://liveimg.9158.com/pic/week/boy2x.png",
    girlurl: "http://liveimg.9158.com/pic/week/girl2x.png",
    pixel: "http://liveimg.9158.com/pixel.gif"
}

//注册jquery事件，等到页面加载完毕时才执行
//DOM加载完成以后才执行里面操作
$(function () {
    //选项卡切换
    $('.tab .tab_item').on('click', function () {
        $('body,html').animate({ scrollTop: 0 }, 100);//返回页面顶部（ps要求增加）

        //防止当前标签数据还未加载完成去点击其他标签数据加载错位问题
        if (isLoading == true) {
            return;
        }
        var $this = $(this);
        
        tabIndex = $this.index();

        var percent = -(tabIndex * 100);

        $(this).addClass('tab_sel').siblings('.tab_item').removeClass('tab_sel');
        $('.miao_rank').eq(tabIndex).show().addClass('fadeIn').siblings('.miao_rank').hide().removeClass('fadeIn');
        $('#rank_con').css({ '-webkit-transform': 'translate3d(' + percent + '%, 0px, 0px)', '-webkit-transition': '300ms' });
        $('.dropload-up,.dropload-down,.dropload-refresh').css({ 'left': -percent + '%' });

        if (!tabArrary[tabIndex].loadEnd) {
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
        distince: 50,//拉动距离
        threshold: 150,//提前加载距离,加载区高度2/3
        domDown: {
            domClass: 'dropload-down',
            domRefresh: '<div class="dropload-refresh">上拉加载更多</div>',
            domLoad: '<div class="dropload-load"><span class="loading"></span>正在加载数据中...</div>',
            domNoData: '<div class="dropload-noData">已经到底了</div>'
        },
        //laodUpFn: function (me) {

        //},
        loadDownFn: function (me) {

            var param = { ranktype: tabIndex, pagesize: pagesize, page: tabArrary[tabIndex].page };
            $.ajax({
                timeout: 6000, //超时时间设置，单位毫秒
                type: 'POST',
                dataType: 'json',
                url: '/Rank/RankList',
                data: param,
                beforeSend: function () {
                    isLoading = true;
                },
                success: function (d) {
                    tabArrary[tabIndex].total = d.data.total;
                    tabArrary[tabIndex].pageEnd = d.data.totalPage;
                    tabArrary[tabIndex].page++;

                    if (tabArrary[tabIndex].page > tabArrary[tabIndex].pageEnd) {
                        tabArrary[tabIndex].loadEnd = true;//所有数据加载完成之后调用dropload的锁定，和无数据方法。
                        me.lock();// 智能锁定，锁定上一次加载的方向//me.lock('up')
                        me.noData();// 无数据
                    }

                    Rank.Logic.LoadRankHtml(tabIndex, d.data);
                    me.resetload();
                    layer.closeAll('loading');
                },
                error: function (xhr, type) {
                    me.lock();
                    me.noData();
                    me.resetload();

                    isLoading = false;
                }
            });
        }
    });//dropload
});//$(jquery)


Rank.Logic = {
    LoadRankHtml: function (ranktype, d) {
        var list = d.rankList;
        var str = '';
        if (d.total <= 0) {
            str = '';
        } else {
            $.each(list, function (i, o) {
                var obj = list[i];
                var sex = (obj.gender == 0) ? "female" : "male";
                var item = '';
                var icon = '';
                var row_no = obj.pos;
                var level = 'level' + obj.level;
                var total_text = Rank.Logic.GetTotalText(ranktype, obj.sumprice);

                if (row_no > 0 && row_no < 4) {
                    item = 'item' + row_no;
                    icon = 'icon' + row_no;
                    row_no = '';
                }

                str += '<div class="item ' + item + '">'
                    + '<a href="javscript:;" onclick="Rank.Logic.ShowCard(this,' + obj.useridx + ')">'
                    + '<div class="icon ' + icon + '">' + row_no + '</div>'
                    + '      <div class="the_img"><div class="img"></div>'
                    + '          <div class="img_img"><img class=\"imgLoading\" src="http://liveimg.9158.com/pixel.gif" data-echo=' + obj.smallpic + '></div></div>'
                    + '       <div class="name">'
                    + '            <span class="name_name">' + obj.myname + '</span>'
                    + '            <span class="' + sex + '"></span>'
                    + '            <div class="the_level">'
                    + '                 <span class="level ' + level + '"></span>'
                    + '                 <span class="num_bg bg' + Rank.Logic.GetGradeRank(obj.grade) + '"></span>'
                    + '                 <span class="num">' + obj.grade + '</span>'
                    + '            </div>'
                    + '            <div class="food">' + total_text + '</div>'
                    + '        </div>'
                    + '</a></div>'
            });
        }

        $('.miao_rank').eq(ranktype).append(str);
        isLoading = false;
        if (tabArrary[tabIndex].page > tabArrary[tabIndex].pageEnd) {
            var html = '<div class="rank_text"><span>每周消费Top3的土豪可获得下周1的封神榜展示特权1整天哦！</span>'
            html += '<span class="update">该榜单于每周一 3：00更新显示上一周的排名</span><span class="blank"></span></div>';

            $('.miao_rank').eq(ranktype).append(html);
        }
        //$(".scrollLoading").scrollLoading();
        echo.init({
            offset: 0,
            throttle: 0
        });
    },
    GetTotalText: function (ranktype, total) {
        var total_text = '';

        if (ranktype == 0) {
            total_text = '消费：' + total;
        } else if (ranktype == 1) {
            total_text = '猫粮：' + total;
        } else if (ranktype == 2) {
            total_text = '猫粮：' + total;
        }
        return total_text;
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
    Touch: function () {
        var obj = document.getElementById('rank_con');
        var startPos, endPos, isScrolling;

        obj.addEventListener('touchstart', function (e) {
            var touch = event.targetTouches[0]; //touches数组对象获得屏幕上所有的touch，取第一个touch
            startPos = { x: touch.pageX, y: touch.pageY, time: +new Date }; //取第一个touch的坐标值
            isScrolling = 0; //这个参数判断是垂直滚动还是水平滚动
            obj.addEventListener('touchmove', this, false);
            obj.addEventListener('touchend', this, false);
        });

        obj.addEventListener('touchmove', function (e) {
            //当屏幕有多个touch或者页面被缩放过，就不执行move操作
            if (event.targetTouches.length > 1 || event.scale && event.scale !== 1) return;
            var touch = event.targetTouches[0];
            endPos = { x: touch.pageX - startPos.x, y: touch.pageY - startPos.y };
            console.log(startPos.y, endPos.y, isScrolling, $(window).scrollTop());

            isScrolling = Math.abs(endPos.x) < Math.abs(endPos.y) ? 1 : 0; //isScrolling为1时，表示纵向滑动，0为横向滑动
        });

        obj.addEventListener('touchend', function (e) {
            if ($(window).scrollTop() < 50) {
                $("#tab").slideDown(800);
                return;
            }

            if ($(window).scrollTop() > 20) {
                if (endPos.y > 0) {
                    $("#tab").slideDown(800);
                }
                else if ($(window).scrollTop() > 20) {
                    $("#tab").slideUp(800);
                }
            }

        });
    },
    ShowCard: function (obj, useridx) {

        if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)) {
            obj.setAttribute("href", "miaobowpa:/ShowCard&useridx=" + useridx + "&");
        }
        else if (navigator.userAgent.indexOf('Android') > -1 || navigator.userAgent.indexOf('Adr') > -1) {
            obj.setAttribute("href", "javascript:android.showUserCard(" + useridx + ")");
        }
    }
}
