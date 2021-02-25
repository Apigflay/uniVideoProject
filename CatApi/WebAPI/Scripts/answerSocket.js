/// <reference path="lib/jquery-1.8.2.min.js" />

var Answer = {};
var layer = {};

layer.msg = {
    open: function (type, msg) {
        var html = ''
        + '<p>' + msg + '</p>'
        + '<div class="btn_w">'
        + '    <a href="javascript:layer.msg.close();">确定</a>'
        //+'    <a href="">取消</a>'
        + '</div>'

        $('#layer .layer .pop').html(html);
        $('#layer').css('display', 'block');
    },
    close: function () {
        $('#layer').css('display', 'none');
    }
};

Answer.Config = {
    useridx: getQueryStr('idx'),
    anchoridx: getQueryStr('anchor'),
    token: getQueryStr('token'),
    gameid: getQueryStr('gameid'),
    gametype: 0,//游戏类型1:1对多，2:1对1
    turnid: 0,
    uAnswerNum: 0
};

Answer.Data = {
    //主播开启游戏
    anchorStartGame: function (gtype, callback) {
        var param = {
            gametype: gtype,
            useridx: Answer.Config.useridx,
            anchoridx: Answer.Config.anchoridx
        };

        $.ajax({
            timeout: 8000, //超时时间设置，单位毫秒
            type: 'POST',
            dataType: 'json',
            url: '/GameCenter/startGameData',
            data: param,
            beforeSend: function () { },
            success: function (response) {

                callback(response);
            },
            error: function (xhr, type) { }
        });
    },
    //多人,1v1挑战用户加入
    userJoinGameData: function (gtype, callback) {
        var param = {
            gametype: gtype,
            useridx: Answer.Config.useridx,
            anchoridx: Answer.Config.anchoridx
        };

        $.ajax({
            timeout: 8000, //超时时间设置，单位毫秒
            type: 'POST',
            dataType: 'json',
            url: '/GameCenter/userJoinAnswerData',
            data: param,
            beforeSend: function () { },
            success: function (response) {
                callback(response);
            },
            error: function (xhr, type) {
            }
        });
    },
    //排行榜数据
    rankingData: function (callback) {

        var param = {
            turnid: Answer.Config.turnid,
            useridx: Answer.Config.useridx,
            anchoridx: Answer.Config.anchoridx
        };

        $.ajax({
            timeout: 8000,
            type: 'POST',
            dataType: 'json',
            url: '/GameCenter/rankingData',
            data: param,
            beforeSend: function () { },
            success: function (response) {
                callback(response);
            },
            error: function (xhr, type) { }
        });
    },
    //1v1挑战申请列表
    singleInviteList: function (callback) {
        var param = {
            useridx: Answer.Config.useridx,
        };

        $.ajax({
            timeout: 8000,
            type: 'POST',
            dataType: 'json',
            url: '/GameCenter/SingleInviteListData',
            data: param,
            beforeSend: function () { },
            success: function (response) {
                callback(response);
            },
            error: function (xhr, type) { }
        });
    },
    //1v1发起挑战申请
    singleInviteInfo: function (toidx, callback) {
        var param = {
            anchoridx: Answer.Config.anchoridx,
            fromidx: Answer.Config.useridx,
            toidx: toidx
        };

        $.ajax({
            timeout: 8000,
            type: 'POST',
            dataType: 'json',
            url: '/GameCenter/SingleInviteInfo',
            data: param,
            beforeSend: function () { },
            success: function (response) {
                callback(response);
            },
            error: function (xhr, type) { }
        });
    },
    //1v1挑战信息,用户显示对战双方的头像和昵称
    singleStartGameInfo: function (type, useridx, callback) {
        var param = {
            turnid: Answer.Config.turnid,
            useridx: Answer.Config.useridx,
            type: type
        };
        $.ajax({
            timeout: 8000,
            type: 'POST',
            dataType: 'json',
            url: '/GameCenter/SingleStartGameData',
            data: param,
            beforeSend: function () { },
            success: function (response) {
                callback(response);
            },
            error: function (xhr, type) { }
        });
    }
};

//页面逻辑
Answer.Logic = {
    //1、compere setupGame preparatory phase
    setupGame: function (gtype) {

        if (gtype == 1) {
            Answer.Data.anchorStartGame(gtype, function (response) {

                if (response.code == -1) {
                    layer.msg.open(1, '今日开启游戏次数已用完');
                    return;
                }

                Answer.Page.openPreLayer('open');

                Answer.Config.turnid = response.data.turnid;
                Answer.Config.gametype = 1;
                //创建比赛局
                WebSocketObj.tgCreateGame(1, Answer.Config.turnid);

            });
        } else if (gtype == 2) {
            var toidx = $('#txtidx').val();
            if (toidx == '') {
                Answer.Page.openChallengeInviteLayer('open');
                layer.msg.open(1, '请输入对方的idx');
                return;
            }
            if (Answer.Config.useridx == toidx) {
                layer.msg.open(1, '自己不能邀请自己'); return;
            }

            //加入游戏币值判断
            Answer.Data.userJoinGameData(2, function (response) {

                if (response.code == -3) {
                    layer.msg.open(1, '币不足，请充值');
                } else if (response.code == -4) {
                    layer.msg.open(1, '创建游戏失败');
                } else if (response.code == -5) {
                    layer.msg.open(1, '当前为游客状态，请先绑定账号');
                } else if (response.code == 1) {

                    //双方比赛信息查询
                    Answer.Data.singleInviteInfo(toidx, function (resp) {
                        var data = resp.data;

                        if (resp.code == 102) {
                            $('#txtidx').val('');
                            layer.msg.open(1, '输入的用户idx有误');
                        } else if (resp.code == 100) {

                            Answer.Config.turnid = data.turnid;
                            Answer.Config.gametype = 2;

                            //创建比赛局
                            WebSocketObj.tgCreateGame(2, Answer.Config.turnid);

                            //答题页面查询1v1比赛局信息
                            Answer.Logic.load1v1UserInfoHtml(1, data);
                            Answer.Page.openInviteWatiLayer('open');
                            //记录上一次发送邀请人的idx
                            localStorage.setItem('toidx', toidx);
                        } else {
                            layer.msg.open(1, '程序出错啦！');
                        }
                    });

                }
            })
        }
    },
    //2、主播正式开启游戏向用户推送题目开始答题
    startGame: function () {
        //websocket向用户下发答题题目，判断人数是否足够
        //var num = WebSocketObj.peopleNum;
        //if (num < 6) {
        //    layer.msg.open(1, '还未达到开启条件'); return
        //}

        //开始比赛
        //console.log('startGame,turnid:' + Answer.Config.turnid);
        WebSocketObj.tgStartGame(Answer.Config.anchoridx, Answer.Config.turnid);
    },
    //取消游戏
    cancelGame: function (gametype, isAnchor) {

        var useridx = 0;

        if (isAnchor == 1) {
            useridx = Answer.Config.anchoridx;
        } else {
            useridx = Answer.Config.useridx;
        }

        //退出游戏
        WebSocketObj.tgQuitGame(useridx, Answer.Config.turnid);

        Answer.Config.turnid = 0;
        Answer.Config.gametype = 0;

        if (gametype == 1) {
            Answer.Page.openPreLayer('close');

        } else {
            Answer.Page.openInviteWatiLayer('close');
        }
    },
    ///加载排行榜HTML
    LoadRankingHtml: function () {
        Answer.Data.rankingData(function (resp) {

            var html = '';

            if (resp.data.totalCount <= 0) {
                html = '本轮答题暂无排名';
            } else {
                html = template('rank_con_templete', resp.data);
            }

            $('.rk_con ul').html(html);
        })

    },
    //加载题目HTML
    LoadSubjectHtml: function (curQues) {

        Answer.Page.openSubjectLayer('open');

        var allSubject = WebSocketObj.totalSubject;
        var html = '<p>哇没有题目了<p>'

        if (allSubject.length > 0) {
            var item = allSubject[curQues - 1];
            var totalQues = allSubject.length;//总题数

            var question = item.question;
            var answer1 = item.option1;
            var answer2 = item.option2;
            var answer3 = item.option3;
            var answer4 = item.option4;
            var questionid = item.questionid;

            //+ '<div class="b_con">'
            //+ '<p><span>(' + curQues + '/' + totalQues + ')</span>' + question + '</p>'
            //+ '</div>'
            //+ '<div class="b_bot">'
            //+ '    <a data-id="1" href="javascript:selectAnswer(1,' + questionid + ');" class="btn_org">' + answer1 + '</a>'
            //+ '    <a data-id="2" href="javascript:selectAnswer(2,' + questionid + ');" class="btn_org">' + answer2 + '</a>'
            //+ '    <a data-id="3" href="javascript:selectAnswer(3,' + questionid + ');" class="btn_org">' + answer3 + '</a>'
            //+ '    <a data-id="4" href="javascript:selectAnswer(4,' + questionid + ');" class="btn_org">' + answer4 + '</a>'
            //+ '</div>';

            html = template('subject_con_template', item);
        }
        $('#questions').html(html);
    },
    //1v1挑战邀请列表Html
    LoadSingleInviteHtml: function () {
        Answer.Data.singleInviteList(function (response) {
            var data = response.data;
            var html = '';

            if (data.totalCount <= 0) {
                html = '<b></b><p>未收到挑战邀请，快去发起挑战吧！</p>';

            } else {
                $.each(data.list, function (index, item) {
                    html += '<li><dl><dd><span>' + item.myname + '：</span>发起脑洞挑战，快去应战吧发起脑洞挑战，快去应战吧<strong>' + item.TimeStr + '</strong></dd><dt><a href="javascript:Answer.Logic.receiveChallenge(' + item.turnid + ')" class="btn_org">去应战</a></dt></dl></li>';
                })
            }

            $('#singleInviteList .message ul').html(html);
        });
    },
    //多人挑战用户加入
    morePeopleJoin: function () {

        if (WebSocketObj.peopleNum >= 100) {
            layer.msg.open(1, '当前游戏人数已满。');
            return;
        }

        Answer.Data.userJoinGameData(1, function (response) {

            if (response.code == -1) {
                layer.msg.open(1, '主播正在游戏中或还未开启游戏');
            } else if (response.code == -2) {
                layer.msg.open(1, '当日答题次数已用完');
            } else if (response.code == -3) {
                layer.msg.open(1, '币不足，请充值');
            } else if (response.code == -4) {
                layer.msg.open(1, '加入游戏失败');
            } else if (response.code == -5) {
                layer.msg.open(1, '当前为游客状态，请先绑定账号');
            } else if (response.code == 1) {

                //退出游戏时使用
                Answer.Config.turnid = response.data.turnid;
                //退出游戏时使用
                Answer.Config.gametype = 1;
                //用户加入比赛局
                WebSocketObj.tgJoinGame(Answer.Config.useridx, response.data.turnid);
            }
        })
    },
    //1v1接收挑战邀请
    receiveChallenge: function (turnid) {

        Answer.Config.turnid = turnid;
        Answer.Config.gametype = 2;

        //加入游戏币值判断
        Answer.Data.userJoinGameData(2, function (response) {
            if (response.code == -1) {
                layer.msg.open(1, '游戏已经结束');
            } else if (response.code == -3) {
                layer.msg.open(1, '币不足，请充值');
            } else if (response.code == -4) {
                layer.msg.open(1, '加入游戏失败');
            } else if (response.code == -5) {
                layer.msg.open(1, '当前为游客状态，请先绑定账号');
            } else if (response.code == 1) {
                Answer.Data.singleStartGameInfo(2, 0, function (response) {
                    if (response.code == 100 && response.data.gameStatus == 2) {
                        //刷新被邀请列表数据
                        Answer.Logic.LoadSingleInviteHtml();

                        layer.msg.open(1, '当前游戏已结束或已取消'); return;
                    }

                    if (response.code == 100) {

                        //显示双方比赛得分信息
                        Answer.Logic.load1v1UserInfoHtml(2, response.data);

                        //用户加入比赛局
                        WebSocketObj.tgJoinGame(Answer.Config.useridx, turnid);

                        //接收方开始挑战局
                        WebSocketObj.tgStartGame(Answer.Config.useridx, turnid);
                    } else {
                        layer.msg.open(1, '对方已离开或已加入游戏');
                        //刷新被邀请列表数据
                        Answer.Logic.LoadSingleInviteHtml();
                    }
                });
            }
        })
    },
    //1v1游戏开始双方头像昵称信息
    load1v1UserInfoHtml: function (type, data) {

        //1v1邀请人准备页面展示信息
        if (type == 1) {
            //邀请人信息
            $('.pkarea .k_left img').attr('src', data.myPhoto);
            $('.pkarea .k_left dd').html(data.myName);

            //被邀请人信息
            $('.pkarea .k_rig img').attr('src', data.toUserPhoto);
            $('.pkarea .k_rig dd').html(data.toName);
        }

        $("#subject .subtit2").hide();

        //题目页面以及答题结果页展示双方信息
        var html = template('user_con_templete', data);

        $('.b_user').append(html);
    },
    //继续挑战
    continueChallenge: function () {
        if (localStorage.getItem('toidx') > 0) {
            Answer.Logic.setupGame(2);
        } else {
            Answer.Page.open1v1InviteLayer('open');
        }
    },
    //结束挑战
    finishChallenge: function () {
        localStorage.setItem('toidx', '');
        console.log('结束挑战：' + localStorage.getItem('toidx'));

        Answer.Page.openIndexLayer('open');
    }
};


//打开页面关闭页面操作
Answer.Page = {
    //打开首页
    openIndexLayer: function (type) {
        $('#answerIndex').css('display', 'block').siblings('.wrap').css('display', 'none');;
    },
    //打开一对多准备页
    openPreLayer: function (type) {
        if (type == 'open') {
            $('#prePage').css('display', 'block');
            $('#answerIndex').css('display', 'none');
        } else {
            $('#prePage').css('display', 'none');
            $('#answerIndex').css('display', 'block');
        }
    },
    //打开帮助页
    openHelpPageLayer: function (type) {
        if (type == "open") {
            $('#help').css("display", "block").siblings('.wrap').css('display', 'none');
        } else {
            $('#answerIndex').css('display', 'block');
            $('#help').css("display", "none");
        }
    },
    //打开邀请页
    openInviteLayer: function (type) {

        if (type == "open") {
            $('#answerIndex').css('display', 'none');
            $('#share').css('display', 'block');
            $('#shareLetters').select();
        } else {
            $('#answerIndex').css('display', 'block');
            $('#share').css('display', 'none');
        }
    },
    //账户余额页面
    openAccountLayer: function (type) {
        if (type == "open") {
            $('#answerIndex').css('display', 'none');
            $('#account').css('display', 'block');
        } else {
            $('#answerIndex').css('display', 'block');
            $('#account').css('display', 'none');
        }
    },
    //打开答题页
    openSubjectLayer: function (type) {
        if (type == "open") {

            $('#subject').css('display', 'block').siblings('.wrap').css('display', 'none');
        } else {
            $('#answerIndex').css('display', 'block');
            $('#subject').css('display', 'none');
        }
    },
    //打开排行榜页
    openRankingLayer: function (type) {
        if (type == "open") {
            $('#answerIndex').css('display', 'none');
            $('#ranking').css('display', 'block').siblings('.wrap').css('display', 'none');

            Answer.Logic.LoadRankingHtml();
        } else {
            $('#answerIndex').css('display', 'block').siblings('.wrap').css('display', 'none');
            $('#ranking').css('display', 'none');
        }
    },
    //1v1挑战邀请列表
    open1v1InviteLayer: function (type) {
        if (type == 'open') {
            $('#answerIndex').css('display', 'none');
            $('#singleInviteList').css('display', 'block').siblings('.wrap').css('display', 'none');

            Answer.Logic.LoadSingleInviteHtml();
        } else {
            $('#answerIndex').css('display', 'block').siblings('.wrap').css('display', 'none');
            $('#singleInviteList').css('display', 'none');
        }
    },
    //打开1v1挑战邀请页，返回到上一个方法
    openChallengeInviteLayer: function (type) {
        if (type == "open") {
            $('#singleInviteList').css('display', 'none');
            $('#challengeApply').css('display', 'block').siblings('.wrap').css('display', 'none');
        } else {
            $('#challengeApply').css('display', 'none');

            $('#singleInviteList').css('display', 'block');
        }
    },
    //打开答题结果页
    openChallengeResult: function (type) {
        if (type == "open") {
            $('#answerIndex').css('display', 'none');
            $('#challengeResult').css('display', 'block').siblings('.wrap').css('display', 'none');

            Answer.Logic.LoadRankingHtml();
        } else {
            $('#answerIndex').css('display', 'block').siblings('.wrap').css('display', 'none');
            $('#challengeResult').css('display', 'none');
        }
    },
    //1v1挑战等待对方接受邀请页面
    openInviteWatiLayer: function (type) {
        if (type == 'open') {
            $('#inviteWait').css('display', 'block').siblings('.wrap').css('display', 'none');

        } else {
            $('#inviteWait').css('display', 'none');

            //放弃游戏回到列表页面
            $('#singleInviteList').css('display', 'block');
            $('#txtidx').val('');
        }
    },
    //1v1挑战结果页
    openChallengeResult2: function (type) {
        if (type == "open") {
            $('#answerIndex').css('display', 'none');
            $('#challengeResult2').css('display', 'block').siblings('.wrap').css('display', 'none');
        } else {
            $('#answerIndex').css('display', 'block').siblings('.wrap').css('display', 'none');
            $('#challengeResult2').css('display', 'none');
        }
    }
};


//================================================WebSocket模块===========================================
var WebSocketObj = {};//页面操作方法对象
var HEADLENGTH = 12;//发送消息头部大小
WebSocketObj.websocket = Object;//服务对象
WebSocketObj.XinTiaoFun = Object; //心跳线程对象
WebSocketObj.XinTiao = Object; //心跳线程对象
WebSocketObj.peopleNum = 0;
WebSocketObj.socketStatus = 0;//websocket连接状态1：success，0：fail
WebSocketObj.totalSubject = [];//题目存储

//==================================================websocket连接块========================================
WebSocketObj.connect = function (host) {
    try {
        websocket = new WebSocket(host);
        //连接
        websocket.onopen = function (e) {
            //socket状态
            WebSocketObj.socketStatus = 1;

            WebSocketObj.Heart();
            //用户打开页面进行心跳连接
            WebSocketObj.XinTiaoFun();
            //注册用户
            WebSocketObj.RegUser();

            //开始登陆房间
            console.log('open connecting...');
        };
        //接收
        websocket.onmessage = function (msg) {
            WebSocketObj.work(msg.data);
        };
        //断开
        websocket.onclose = function () {
            //失败重连3次
            //readyState 0：正在建立连接连接，还没有完成。 1：连接成功建立，可以进行通信。2：连接正在进行关闭握手，即将关闭。3：连接已经关闭或者根本没有建立。
            //if (websocket.readyState == 2 || websocket.readyState == 3) {

            setTimeout(function () {
                //WebSocketObj.connect("ws://192.168.0.100:11001");
                WebSocketObj.connect(host);

                Answer.Page.openIndexLayer('open');
                WebSocketObj.totalSubject = [];

                console.log('reConnecting....');

                clearInterval(WebSocketObj.XinTiao);
            }, 100);
        };
    }
    catch (exception) {

        //公了显示异常消息
        //MiaoboView.chatlist("服务异常！" + exception, 0);
        clearInterval(WebSocketObj.XinTiao);//清理心跳线程
    }
};

//============================================接受消息块=======================================
WebSocketObj.work = function (data) {
    var fileReader = new FileReader();
    fileReader.onload = function (progressEvent) {
        var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
        var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
        var cmd = HeadRecv[1];

        if (cmd != 200000) {
            //console.log('接收消息命令：' + cmd);
        }

        if (HeadRecv[1] == 200000) //心跳
        {
            //console.log('gametype:' + Answer.Config.gametype);
        }
        else if (HeadRecv[1] == 100002)//创建比赛返回
        {
            var epublishAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH, 2);
            var GameStatus = epublishAnswerArray[0]; //游戏状态 -1,//删除  0,//创建  1,//开始 2//结束
            var GameID = epublishAnswerArray[1];  //gameid

            if (GameStatus == 0 && Answer.Config.gametype == 1) {
                //加入比赛局
                WebSocketObj.tgJoinGame(Answer.Config.anchoridx, Answer.Config.turnid);
            }

            if (Answer.Config.gametype == 2)   //1对1发起方也加入游戏局
            {
                WebSocketObj.tgJoinGame(Answer.Config.useridx, Answer.Config.turnid);
            }

            console.log('创建比赛返回结果：gametype:' + Answer.Config.gametype + ',turnid:' + GameID);
        }
        else if (HeadRecv[1] == 100005)//1 v More Join Game PeopleNum
        {
            var epublishAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH, 2);
            var GameID = epublishAnswerArray[0];  //gameid
            var num = epublishAnswerArray[1];

            $('#prePage .pc_in p strong').html(num);
        }
        else if (HeadRecv[1] == 100006)//加入赛返回
        {
            var epublishAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH, 2);
            var GameID = epublishAnswerArray[0];  //gameid
            var ret = epublishAnswerArray[1]; 	  //0:失败，1：成功

            //加入比赛局成功才显示对应的准备页面
            if (ret == 1) {
                if (Answer.Config.gametype == 1) {
                    Answer.Page.openPreLayer('open');
                }
                //else {
                //    Answer.Page.openInviteWatiLayer('open');
                //}
            } else {
                layer.msg.open(1, '加入比赛局失败，请稍后再试！ID:' + GameID);
            }

            //console.log('加入比赛返回，result:' + ret+',gameid:'+ GameID);
        }
        else if (HeadRecv[1] == 100007)//退出赛返回
        {
            var epublishAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH, 2);
            var GameID = epublishAnswerArray[0];  //gameid
            var ret = epublishAnswerArray[1]; 	  //0:失败，1：关闭游戏；2：退出游戏

            if (ret != 0) {
                Answer.Page.openPreLayer('close');
            }
        }
        else if (HeadRecv[1] == 100009)//开始比赛返回
        {
            var epublishAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH, 2);
            var GameID = epublishAnswerArray[0];  //gameid
            var ret = epublishAnswerArray[1]; 	  //0:失败，1：成功 ，2：人数不足

            if (ret == 0) {
                layer.msg.open(1, '开始比赛失败，请稍后再试！');
            } else if (ret == 2) {
                layer.msg.open(1, '未达到开启游戏条件限制');
            }
        }
        else if (HeadRecv[1] == 100010)//开始比赛返回题目
        {
            var sLength = HeadRecv[2];//题数

            for (var i = 0; i < sLength; i++) {
                var answerArray = new Uint32Array(arrayBuffer, i * 268 + HEADLENGTH, 2);
                var questionId = answerArray[0];
                //var answerId = answerArray[1];

                var qTypeArray = new Uint8Array(arrayBuffer, i * 268 + (12 + 8), 32);
                var qType = htmlLode.arryTostr(new TextDecoder("utf-8").decode(qTypeArray));

                var option1Arrary = new Uint8Array(arrayBuffer, i * 268 + 12 + 8 + 32, 32);
                var option1 = htmlLode.arryTostr(new TextDecoder("utf-8").decode(option1Arrary));

                var option2Arrary = new Uint8Array(arrayBuffer, i * 268 + 12 + 8 + 32 + 32, 32);
                var option2 = htmlLode.arryTostr(new TextDecoder("utf-8").decode(option2Arrary));

                var option3Arrary = new Uint8Array(arrayBuffer, i * 268 + 12 + 8 + 32 + 32 + 32, 32);
                var option3 = htmlLode.arryTostr(new TextDecoder("utf-8").decode(option3Arrary));

                var option4Arrary = new Uint8Array(arrayBuffer, i * 268 + 12 + 8 + 32 + 32 + 32 + 32, 32);
                var option4 = htmlLode.arryTostr(new TextDecoder("utf-8").decode(option4Arrary));

                var questionArrary = new Uint8Array(arrayBuffer, i * 268 + 12 + 8 + 32 + 32 + 32 + 32 + 32, 100);
                var question = htmlLode.arryTostr(new TextDecoder("utf-8").decode(questionArrary));

                var subjectModel = { subLen: sLength, index: i + 1, questionid: questionId, question: question, option1: option1, option2: option2, option3: option3, option4: option4 };

                WebSocketObj.totalSubject.push(subjectModel)
            }
        }
        else if (HeadRecv[1] == 100012)//每回合答题结果返回
        {
            if (Answer.Config.gametype == 2)//答题结果目前只有1对1答题使用
            {
                var epublishAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH, 5);
                var nUserIdx1 = epublishAnswerArray[1];
                var nSCore1 = epublishAnswerArray[3];//总得分

                //console.log('Answer Result Return：' + nGameID);
                //layer.msg.open(1,'答题结果第一个人:{' +epublishAnswerArray.join(',')+ '}');

                var anothorAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH + 20, 5);
                var nUserIdx2 = anothorAnswerArray[1];
                var nSCore2 = anothorAnswerArray[3];//总得分
                //layer.msg.open(1,'答题结果第二个人:{' +anothorAnswerArray.join(',')+ '}');

                if (nUserIdx1 == Answer.Config.useridx) //用gameid来存每一轮的胜负(负数为负，0为平局，证书
                {
                    Answer.Config.gameid = (nSCore1 - nSCore2);
                    $(".left_user dd").html(nSCore1);
                    $(".rig_user dd").html(nSCore2);
                    //layer.msg.open(1,'我的答题:{' +epublishAnswerArray.join(',')+ '},另一个答题:{' + anothorAnswerArray.join(',') + '}');
                }
                else {
                    Answer.Config.gameid = (nSCore2 - nSCore1);
                    $(".left_user dd").html(nSCore2);
                    $(".rig_user dd").html(nSCore1);
                    //layer.msg.open(1,'我的答题:{' +anothorAnswerArray.join(',')+ '},另一个答题:{' + epublishAnswerArray.join(',') + '}');
                }
                console.log('本轮答题胜负:' + Answer.Config.gameid + ',gtype:' + Answer.Config.gametype);
            }
        }
        else if (HeadRecv[1] == 100013)//开启下一回合
        {
            var epublishAnswerArray = new Uint32Array(arrayBuffer, HEADLENGTH, 7);

            var nQuestionID = epublishAnswerArray[0];
            var nRestAnswerTime = epublishAnswerArray[1];//剩余答题时间 10s
            var nRestNextTime = epublishAnswerArray[2];//剩余下一题目开启时间 15s
            var nScore = epublishAnswerArray[3];
            var nCurIndex = epublishAnswerArray[4];//当前第几题
            var nAnswerNum = epublishAnswerArray[5];//true answer
            var nGameType = epublishAnswerArray[6];//当前局的类型 1：一对多，2：一对一

            localStorage.setItem('uAnswerNum', 0);
            Answer.Config.uAnswerNum = 0;           //每次下发题目时清空上一次用户所选答案
            Answer.Config.gametype = nGameType;     //每次下发的时候重新赋值，避免断线重连或异常退出的时候无法区分游戏类型

            //防止先开启了1v1挑战后，在开启一对多挑战用户信息还展示bug
            //if (nGameType = 1) {
            //    $('.b_user').html('');
            //}

            //判断答题类型是否要加双方用户信息
            //var left_info = $("#subject .left_user");
            //if(nGameType == 1)  
            //{                    
            //    if(left_info.html() == "")
            //    {
            //        Answer.Data.singleStartGameInfo(type, function(response){
            //            if(response.code=="100")
            //            {
            //                Answer.Logic.load1v1UserInfoHtml(response.data);                    
            //            }
            //        });
            //    }
            //}
            //else
            //{
            //    left_info.html("");
            //    $("#subject .rig_user").html("");
            //}

            countdown(nRestAnswerTime, nAnswerNum);

            Answer.Logic.LoadSubjectHtml(nCurIndex);

            if (Answer.Config.useridx == 63583358) {

                $('#questions .b_bot a').eq(nAnswerNum - 1).attr('class', 'btn_gre');
            }
            console.log('题目下发：gametype:' + nGameType);
        }
        else if (HeadRecv[1] == 100014)//1v多 游戏结束后比赛结果下发
        {
            var resultArrary = new Uint32Array(arrayBuffer, HEADLENGTH, 6);

            var nGameID = resultArrary[0];
            var nUseridx = resultArrary[1];
            var nScore = resultArrary[2];
            var nRank = resultArrary[3];
            var nRewardType = resultArrary[4];
            var fRewardCash = resultArrary[5];

            var rewardText = '';
            if (nRewardType == 1) {
                rewardText = '恭喜你获得' + (fRewardCash / 100) + '元现金';

            } else if (nRewardType == 2) {
                rewardText = '恭喜你获得' + fRewardCash + '喵币';
            } else {
                rewardText = '再接再厉吧！！';

                $('#challengeResult .success').addClass('finish');
            }

            $('#challengeResult .success h5').html(rewardText);

            Answer.Config.turnid = nGameID;

            //游戏结束
            //n道答题完之后主播直接展示排行榜
            if (Answer.Config.useridx == Answer.Config.anchoridx) {
                Answer.Page.openRankingLayer('open');
            } else {
                Answer.Page.openChallengeResult('open');
            }

            //console.log('比赛结果' + fRewardCash + ',' + ',' + nUseridx + ',gid:' + nGameID)
            WebSocketObj.totalSubject = [];

        } else if (cmd == 100015) {//1v1 游戏结束

            var gameOverArray = new Uint32Array(arrayBuffer, HEADLENGTH, 1);
            var GameID = gameOverArray[0];  //gameid

            //if (Answer.Config.gametype == 2) {
            //1对1，不发100014状态，然后跳到结果页

            var rewardText = '';
            if (Answer.Config.gameid > 0) {
                rewardText = 'finish';
            } else if (Answer.Config.gameid == 0) {
                rewardText = 's_equ';
            } else {
                rewardText = 's_fail';
            }

            console.log('1v1比赛结果：' + Answer.Config.gameid);

            $('#challengeResult2 .success b').attr('class', rewardText);
            Answer.Page.openChallengeResult2('open');

            WebSocketObj.totalSubject = [];
            //}
            Answer.Config.gametype = 0;
        }
    };
    fileReader.readAsArrayBuffer(data);
};

//========================================WebSocket发送模块===========================================
//心跳连接
WebSocketObj.XinTiaoFun = function () {

    WebSocketObj.XinTiao = setInterval(function () {
        WebSocketObj.Heart();
    }, 2000);
};

//心跳包
WebSocketObj.Heart = function () {
    //组消息
    //alert(4);
    var bufferRes = new ArrayBuffer(16);
    var Head = new Int32Array(bufferRes, 0, 4);
    Head[0] = 16; //len
    Head[1] = 200000;//cmd
    Head[3] = Answer.Config.useridx;//60068188
    websocket.send(bufferRes);
};

//注册用户
WebSocketObj.RegUser = function () {
    var totalLen = HEADLENGTH + 4 + 4;
    var bufferRes = new ArrayBuffer(totalLen);//总长度
    var Head = new Int32Array(bufferRes, 0, 3);
    Head[0] = 20; //len
    Head[1] = 100000;//cmd

    var likeArray = new Uint32Array(bufferRes, 12, 2);
    likeArray[0] = Answer.Config.useridx;//60068188;//nUserIdx
    likeArray[1] = 0;//nGameID

    websocket.send(bufferRes);
};

//创建比赛局
WebSocketObj.tgCreateGame = function (gtype, turnid) {
    var totalLen = HEADLENGTH + 16;
    var bufferRes = new ArrayBuffer(totalLen);//总长度
    var Head = new Int32Array(bufferRes, 0, 3);
    Head[0] = totalLen; //len
    Head[1] = 100001;//cmd
    var paramArray = new Uint32Array(bufferRes, HEADLENGTH, 4);
    paramArray[0] = turnid;//nGameID
    paramArray[1] = Answer.Config.anchoridx;//nActhorIdx
    paramArray[2] = gtype;//nGameType
    paramArray[3] = 6;//peopleNum限定人数

    console.log('create Game Success ,gtype:' + gtype + ',' + turnid);
    websocket.send(bufferRes);
};

//加入比赛局
WebSocketObj.tgJoinGame = function (useridx, turnid) {
    var totalLen = HEADLENGTH + 8;
    var bufferRes = new ArrayBuffer(totalLen);//总长度
    var Head = new Int32Array(bufferRes, 0, 3);
    Head[0] = totalLen; //len
    Head[1] = 100003;//cmd
    var paramArray = new Uint32Array(bufferRes, HEADLENGTH, 2);
    paramArray[0] = useridx;//nUserIdx
    paramArray[1] = turnid;//nGameID

    console.log('join game :' + useridx + ',' + turnid);
    if (WebSocketObj.socketStatus == 1) {
        websocket.send(bufferRes);
    }
};

//退出比赛局
WebSocketObj.tgQuitGame = function (useridx, turnid) {
    var totalLen = HEADLENGTH + 8;
    var bufferRes = new ArrayBuffer(totalLen);//总长度
    var Head = new Int32Array(bufferRes, 0, 3);
    Head[0] = totalLen; //len
    Head[1] = 100004;//cmd
    var paramArray = new Uint32Array(bufferRes, HEADLENGTH, 2);
    paramArray[0] = useridx;//nUserIdx
    paramArray[1] = turnid;//nGameID

    if (WebSocketObj.socketStatus == 1) {
        console.log('cancel game:' + turnid);
        websocket.send(bufferRes);
    }
};

//开始比赛局
WebSocketObj.tgStartGame = function (useridx, turnid) {
    var totalLen = HEADLENGTH + 8;
    var bufferRes = new ArrayBuffer(totalLen);//总长度
    var Head = new Int32Array(bufferRes, 0, 3);
    Head[0] = totalLen; //len
    Head[1] = 100008;//cmd
    var paramArray = new Uint32Array(bufferRes, HEADLENGTH, 2);
    paramArray[0] = useridx;//nUserIdx
    paramArray[1] = turnid;//nGameID

    if (WebSocketObj.socketStatus == 1) {
        websocket.send(bufferRes);
    }
};

//每回合答题
WebSocketObj.tgAnswerGame = function (useridx, turnid, questionidx, anserid) {
    var totalLen = HEADLENGTH + 16;
    var bufferRes = new ArrayBuffer(totalLen);//总长度
    var Head = new Int32Array(bufferRes, 0, 3);
    Head[0] = totalLen; //len
    Head[1] = 100011;//cmd
    var paramArray = new Uint32Array(bufferRes, HEADLENGTH, 4);
    paramArray[0] = turnid;//nGameID
    paramArray[1] = useridx;//nUserIdx
    paramArray[2] = questionidx;//nQuestionIdx
    paramArray[3] = anserid;//nAnswerID
    if (WebSocketObj.socketStatus == 1) {
        websocket.send(bufferRes);
    }
};
var htmlLode = {
    fillstr2ab: function (str, buf, offset) {
        var uint8array = new TextEncoder().encode(str);
        var strLen = uint8array.length;
        var bufView = new Uint8Array(buf, offset, strLen);
        for (var i = 0; i < strLen; i++) {
            bufView[i] = uint8array[i];
        }
    },
    // 去除ArrayBuffer多余占位转为字符串后的编码，
    arryTostr: function arryTostr(str) {

        var num = str.indexOf("\0");
        if (num > 0)
            str = str.substring(0, num);
        return str;
    }
};


//审题10秒倒计时
function countdown(time, answerid) {
    //答题的时候隐藏掉下一题倒计时
    $('.subtit2').css('display', 'none');

    var inter = setInterval(function () {
        $('.time b').html(time);

        if (time == 0) {
            clearInterval(inter);

            countdown5(5, answerid);
        }

        time--;
    }, 1000);

};

//下一题5秒倒计时
function countdown5(time, answerNum) {

    var inter = setInterval(function () {
        if (time == 0) { clearInterval(inter); }

        $('#mes').html(time);
        time--;
    }, 900);

    //一对一答题继续隐藏下一题倒计时
    if (Answer.Config.gametype == 1) {
        //展示结果时显示 下一题倒计时
        $('.subtit2').css('display', 'block');
    }

    //显示题目正确答案
    $('#questions .b_bot a').eq(answerNum - 1).attr('class', 'btn_gre');
    $('#questions .b_bot a').attr('href', 'javascript:;');

    if (Answer.Config.uAnswerNum > 0) {

        //答错了，用户所选
        if (Answer.Config.uAnswerNum != answerNum) {

            $('#questions .b_bot a').eq(Answer.Config.uAnswerNum - 1).attr('class', 'btn_red');
            $('#questions .b_bot a').eq(Answer.Config.uAnswerNum - 1).append('<i class="cho_wrong"></i>');

        } else {
            $('#questions .b_bot a').eq(answerNum - 1).attr('class', 'btn_gre');
            $('#questions .b_bot a').eq(answerNum - 1).append('<i class="cho_ok"></i>');
        }
    }

};
