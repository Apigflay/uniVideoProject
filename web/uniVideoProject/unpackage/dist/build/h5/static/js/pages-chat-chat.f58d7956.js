(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["pages-chat-chat"],{"36c6":function(t,a,e){a=t.exports=e("2350")(!1),a.push([t.i,'@charset "UTF-8";\n/**\r\n * 这里是uni-app内置的常用样式变量\r\n *\r\n * uni-app 官方扩展插件及插件市场（https://ext.dcloud.net.cn）上很多三方插件均使用了这些样式变量\r\n * 如果你是插件开发者，建议你使用scss预处理，并在插件代码中直接使用这些变量（无需 import 这个文件），方便用户通过搭积木的方式开发整体风格一致的App\r\n *\r\n */\n/**\r\n * 如果你是App开发者（插件使用者），你可以通过修改这些变量来定制自己的插件主题，实现自定义主题功能\r\n *\r\n * 如果你的项目同样使用了scss预处理，你也可以直接在你的 scss 代码中使用如下变量，同时无需 import 这个文件\r\n */\n/* 颜色变量 */\n/* 行为相关颜色 */\n/* 文字基本颜色 */\n/* 背景颜色 */\n/* 边框颜色 */\n/* 尺寸变量 */\n/* 文字尺寸 */\n/* 图片尺寸 */\n/* Border Radius */\n/* 水平间距 */\n/* 垂直间距 */\n/* 透明度 */\n/* 文章场景相关 */em[data-v-11923616]{font-style:normal}body[data-v-11923616],dd[data-v-11923616],dl[data-v-11923616],h1[data-v-11923616],h2[data-v-11923616],h3[data-v-11923616],h4[data-v-11923616],h5[data-v-11923616],h6[data-v-11923616],hr[data-v-11923616],ol[data-v-11923616],p[data-v-11923616],pre[data-v-11923616],tbody[data-v-11923616],td[data-v-11923616],tfoot[data-v-11923616],th[data-v-11923616],thead[data-v-11923616],ul[data-v-11923616],uni-form[data-v-11923616],uni-input[data-v-11923616],uni-textarea[data-v-11923616]{margin:0;padding:0}ol[data-v-11923616],ul[data-v-11923616]{list-style:none}a[data-v-11923616]{text-decoration:none}html[data-v-11923616]{-ms-text-size-adjust:none;-webkit-text-size-adjust:none;-moz-text-size-adjust:none;text-size-adjust:none}body[data-v-11923616]{line-height:1.5;font-size:14px}body[data-v-11923616],select[data-v-11923616],uni-button[data-v-11923616],uni-input[data-v-11923616],uni-textarea[data-v-11923616]{font-family:helvetica neue,tahoma,hiragino sans gb,stheiti,wenquanyi micro hei,\\5FAE\\8F6F\\96C5\\9ED1,\\5B8B\\4F53,sans-serif}b[data-v-11923616],strong[data-v-11923616]{font-weight:700}em[data-v-11923616],i[data-v-11923616]{font-style:normal}table[data-v-11923616]{border-collapse:collapse;border-spacing:0}table td[data-v-11923616],table th[data-v-11923616]{border:1px solid #ddd;padding:5px}table th[data-v-11923616]{font-weight:inherit;border-bottom-width:2px;border-bottom-color:#ccc}img[data-v-11923616]{border:0 none;width:auto\\9;max-width:100%;vertical-align:top;height:auto}select[data-v-11923616],uni-button[data-v-11923616],uni-input[data-v-11923616],uni-textarea[data-v-11923616]{font-family:inherit;font-size:100%;margin:0;vertical-align:baseline}html uni-input[type=button][data-v-11923616],uni-button[data-v-11923616],uni-input[type=reset][data-v-11923616],uni-input[type=submit][data-v-11923616]{-webkit-appearance:button;cursor:pointer}uni-button[disabled][data-v-11923616],uni-input[disabled][data-v-11923616]{cursor:default}uni-input[type=checkbox][data-v-11923616],uni-input[type=radio][data-v-11923616]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}uni-input[type=search][data-v-11923616]{-webkit-appearance:textfield;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;box-sizing:content-box}uni-input[type=search][data-v-11923616]::-webkit-search-decoration{-webkit-appearance:none}uni-input[data-v-11923616]:focus{outline:none}select[multiple][data-v-11923616],select[size][data-v-11923616],select[size][multiple][data-v-11923616]{border:1px solid #aaa;padding:0}article[data-v-11923616],aside[data-v-11923616],details[data-v-11923616],figcaption[data-v-11923616],figure[data-v-11923616],footer[data-v-11923616],header[data-v-11923616],hgroup[data-v-11923616],main[data-v-11923616],nav[data-v-11923616],section[data-v-11923616],summary[data-v-11923616]{display:block}uni-audio[data-v-11923616],uni-canvas[data-v-11923616],uni-progress[data-v-11923616],uni-video[data-v-11923616]{display:inline-block}body[data-v-11923616]{background:#fff}uni-input[data-v-11923616]::-webkit-input-speech-button{display:none}uni-button[data-v-11923616],uni-input[data-v-11923616],uni-textarea[data-v-11923616]{-webkit-tap-highlight-color:transparent}uni-page-body[data-v-11923616]{width:100%;height:100%;background:#191919}.contentBox[data-v-11923616]{width:100%;height:100%;overflow:hidden}.scrollView[data-v-11923616]{width:100%;height:100%}.scrollView .main[data-v-11923616]{width:100%;height:100%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column}.scrollView .main .bottomFix[data-v-11923616]{width:100%;height:%?90?%}.scrollView .main .top[data-v-11923616]{position:fixed;top:%?0?%;z-index:999;width:100%;height:%?100?%;background:#252525;font-size:%?30?%;color:#fff;line-height:%?100?%;text-align:center}.scrollView .main .center[data-v-11923616]{margin-top:%?100?%}.scrollView .main .center .new[data-v-11923616]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;width:%?694?%;height:%?129?%;padding:%?0?% %?28?%}.scrollView .main .center .new .img[data-v-11923616]{width:%?90?%;height:%?90?%;margin-right:%?40?%}.scrollView .main .center .new .p[data-v-11923616]{width:%?565?%;border-bottom:%?1?% solid #343434;line-height:%?129?%;font-size:%?26?%;color:#fff}.scrollView .main .center .list[data-v-11923616]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;padding:%?0?% %?28?%;width:%?694?%;height:%?113?%;position:relative}.scrollView .main .center .list .photo[data-v-11923616]{background:#646464;height:%?90?%;width:%?90?%;border-radius:50%;margin-right:%?40?%}.scrollView .main .center .list .newMsgNum[data-v-11923616]{width:%?44?%;height:%?44?%;line-height:%?44?%;text-align:center;font-size:%?20?%;position:absolute;color:#fff;background:red;border-radius:50%;left:%?92?%;top:%?12?%}.scrollView .main .center .list .content[data-v-11923616]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column;-webkit-box-pack:center;-webkit-justify-content:center;-ms-flex-pack:center;justify-content:center;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;height:%?113?%;border-bottom:%?1?% solid #343434}.scrollView .main .center .list .content .name[data-v-11923616]{width:%?565?%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between}.scrollView .main .center .list .content .name .nameArea[data-v-11923616]{width:%?300?%;height:%?44?%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center}.scrollView .main .center .list .content .name .nameArea .id[data-v-11923616]{color:#fff;font-size:%?26?%;line-height:%?48?%;max-width:%?275?%;overflow:hidden;-o-text-overflow:ellipsis;text-overflow:ellipsis;white-space:nowrap}.scrollView .main .center .list .content .name .nameArea .status[data-v-11923616]{margin-left:%?11?%;height:%?12?%;width:%?12?%;background:#00ff2a;border-radius:50%}.scrollView .main .center .list .content .name .data[data-v-11923616]{color:#acacac;font-size:%?22?%;line-height:%?22?%;float:right}.scrollView .main .center .list .content .p[data-v-11923616]{width:%?565?%;color:#acacac;font-size:%?22?%;line-height:%?36?%;margin-top:%?9?%;overflow:hidden;-o-text-overflow:ellipsis;text-overflow:ellipsis;white-space:nowrap}.scrollView .main .tabbarArea[data-v-11923616]{height:%?90?%;width:100%;position:fixed;bottom:0;left:0;z-index:9999;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-justify-content:space-around;-ms-flex-pack:distribute;justify-content:space-around;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;background:#191919}.scrollView .main .tabbarArea .per[data-v-11923616]{width:%?54?%;height:%?50?%}.scrollView .main .tabbarArea .per .img[data-v-11923616]{width:%?54?%;height:%?50?%}body.?%PAGE?%[data-v-11923616]{background:#191919}',""])},5297:function(t,a,e){"use strict";var n=e("c144"),i=e.n(n);i.a},"691f":function(t,a,e){"use strict";var n=function(){var t=this,a=t.$createElement,e=t._self._c||a;return e("v-uni-view",{staticClass:"contentBox"},[e("v-uni-scroll-view",{staticClass:"scrollView",attrs:{"scroll-y":"true"},on:{scrolltolower:function(a){a=t.$handleEvent(a),t.getScrollmsg(a)}}},[e("v-uni-view",{staticClass:"main"},[e("v-uni-view",{staticClass:"top"},[e("v-uni-text",[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language76))])],1),e("v-uni-view",{staticClass:"center"},[e("v-uni-view",{staticClass:"new",style:0==t.no?"":"background:#343434",on:{click:function(a){a=t.$handleEvent(a),t.new_chat()},touchstart:function(a){a=t.$handleEvent(a),t.new_chat_start()},touchend:function(a){a=t.$handleEvent(a),t.new_chat_end()}}},[e("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/newChat_1.png"}}),e("v-uni-text",{staticClass:"p"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language178))])],1),t._l(t.chatListData,function(a,n){return e("v-uni-view",{key:n,staticClass:"list",attrs:{id:n}},[e("v-uni-image",{staticClass:"photo",attrs:{src:a.headpic},on:{click:function(e){e=t.$handleEvent(e),t.goChatpopPage(a.useridx,a.nickname,a.headpic,a.online)}}}),a.newMsgData.length>0&&100>a.newMsgData.length?e("v-uni-text",{staticClass:"newMsgNum"},[t._v(t._s(a.newMsgData.length))]):t._e(),a.newMsgData.length>99?e("v-uni-text",{staticClass:"newMsgNum"},[t._v("99+")]):t._e(),e("v-uni-view",{staticClass:"content",on:{click:function(e){e=t.$handleEvent(e),t.goChatpopPage(a.useridx,a.nickname,a.headpic,a.online)}}},[e("v-uni-view",{staticClass:"name"},[e("v-uni-view",{staticClass:"nameArea"},[e("v-uni-text",{staticClass:"id"},[t._v(t._s(a.nickname))]),a.online?e("v-uni-view",{staticClass:"status"}):t._e()],1),e("v-uni-text",{staticClass:"data"},[t._v(t._s(a.lastTime))])],1),e("v-uni-view",{staticClass:"p"},[e("v-uni-text",[t._v(t._s(a.fy_content))])],1)],1)],1)})],2),e("v-uni-view",{staticClass:"tabbarArea"},t._l(t.tabbarData,function(a,n){return e("v-uni-view",{key:n,staticClass:"per",attrs:{id:n},on:{click:function(a){a=t.$handleEvent(a),t.getClickPer(a)}}},[e("v-uni-image",{staticClass:"img",attrs:{src:a.imgsrcW,mode:""}})],1)}),1),e("v-uni-view",{staticClass:"bottomFix"})],1)],1)],1)},i=[];e.d(a,"a",function(){return n}),e.d(a,"b",function(){return i})},c144:function(t,a,e){var n=e("36c6");"string"===typeof n&&(n=[[t.i,n,""]]),n.locals&&(t.exports=n.locals);var i=e("4f06").default;i("7176aa10",n,!0,{sourceMap:!1,shadowMode:!1})},d17e:function(t,a,e){"use strict";e.r(a);var n=e("edbb"),i=e.n(n);for(var o in n)"default"!==o&&function(t){e.d(a,t,function(){return n[t]})}(o);a["default"]=i.a},eb65:function(t,a,e){"use strict";e.r(a);var n=e("691f"),i=e("d17e");for(var o in i)"default"!==o&&function(t){e.d(a,t,function(){return i[t]})}(o);e("5297");var s=e("2877"),r=Object(s["a"])(i["default"],n["a"],n["b"],!1,null,"11923616",null);a["default"]=r.exports},edbb:function(t,a,e){"use strict";Object.defineProperty(a,"__esModule",{value:!0}),a.default=void 0;var n=e("a84a"),i={data:function(){return{tabbarLoginLanguage:null,show_spot:!0,show_chat:!1,no:!1,table:[{id:0,data:"09/09",name:"@sasa.baby1235",content:"9月vip为两台飞机  享有8.9月百部影片，你的女神...",no:!1},{id:1,data:"09/09",name:"@sasa.baby1236",content:"9月vip为两台飞机  享有8.9月百部影片，你的女神...",no:!1},{id:2,data:"09/09",name:"@sasa.baby1237",content:"9月vip为两台飞机  享有8.9月百部影片，你的女神...",no:!1}],page:1,tabbarLoginData:null,isAnchor:null,tabbarCurrent:0,tabbarData:[{imgsrcB:"../../static/imgs/room1w.png",imgsrcW:"../../static/imgs/room1w.png",text:"1"},{imgsrcB:"../../static/imgs/fang1w.png",imgsrcW:"../../static/imgs/fang1w.png",text:"2"},{imgsrcB:"../../static/imgs/mail1w.png",imgsrcW:"../../static/imgs/mail1w.png",text:"3"},{imgsrcB:"../../static/imgs/xin1w.png",imgsrcW:"../../static/imgs/xin1y.png",text:"4"},{imgsrcB:"../../static/imgs/my1w.png",imgsrcW:"../../static/imgs/my1w.png",text:"5"}]}},onLoad:function(){this.getLoginlanger(),this.getLoginMsg()},onPageScroll:function(){},computed:{chatListData:function(){return this.$store.getters["AllallChatList"]},allChatPageIsNew:function(){return this.$store.getters["AllallChatPageIsNew"]}},watch:{allChatPageIsNew:function(t,a){console.log("change"),console.log(t+"-----"+a),this.getChatList()}},methods:{getLoginlanger:function(){var t=this;uni.getStorage({key:"storage_login_language",success:function(a){t.tabbarLoginLanguage=JSON.parse(a.data),0==t.tabbarLoginLanguage?t.langer=0:1==t.tabbarLoginLanguage?t.langer=1:2==t.tabbarLoginLanguage?t.langer=2:3==t.tabbarLoginLanguage&&(t.langer=3)}})},getLoginMsg:function(){var t=this;uni.getStorage({key:"storage_login_str",success:function(a){t.tabbarLoginData=JSON.parse(a.data),0==t.tabbarLoginData.isAnchor&&(t.isAnchor=!1),t.getChatList()}})},getClickPer:function(t){this.tabbarCurrent=t.currentTarget.id,0==t.currentTarget.id?(0,n.navigateTo)("/pages/home/home",null):1==t.currentTarget.id?(0,n.navigateTo)("/pages/search/search",null):2==t.currentTarget.id?(0,n.navigateTo)("/pages/news/news",null):3==t.currentTarget.id?(0,n.navigateTo)("/pages/chat/chat",null):4==t.currentTarget.id&&(0==this.isAnchor?(0,n.navigateTo)("/pages/my/my",null):(0,n.navigateTo)("/pages/anchorme/anchorme",null))},chat_start:function(t){0==this.table[t].no&&(this.table[t].no=!0)},chat_end:function(t){1==this.table[t].no&&(this.table[t].no=!1)},new_chat_start:function(){0==this.no&&(this.no=!0)},new_chat_end:function(){1==this.no&&(this.no=!1)},getChatList:function(){var t=(0,n.base64ToArrayBuffer)((0,n.encrypt)(JSON.stringify({UserIdx:this.tabbarLoginData.useridx,Page:this.page,Limit:20}))),a=JSON.parse((0,n.decrypt)((0,n.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetChatList",t)));if(100==a.code){var e=JSON.parse(JSON.stringify(a.data.list)),i=this;e.forEach(function(t,a){var e=new Date(t.lastTime).getMonth()+1,n=new Date(t.lastTime).getDate();t.lastTime=e+"/"+n;var o=t.content;if("[图片]"==t.content&&0==i.tabbarLoginLanguage)o="[图片]";else if("[视频]"==t.content&&0==i.tabbarLoginLanguage)o="[视频]";else if("[作品]"==t.content&&0==i.tabbarLoginLanguage)o="[作品]";else if("[礼物]"==t.content&&0==i.tabbarLoginLanguage)o="[礼物]";else if("[图片]"==t.content&&1==i.tabbarLoginLanguage)o="[圖片]";else if("[视频]"==t.content&&1==i.tabbarLoginLanguage)o="[視頻]";else if("[作品]"==t.content&&1==i.tabbarLoginLanguage)o="[作品]";else if("[礼物]"==t.content&&1==i.tabbarLoginLanguage)o="[禮物]";else if("[图片]"==t.content&&2==i.tabbarLoginLanguage)o="[Image]";else if("[视频]"==t.content&&2==i.tabbarLoginLanguage)o="[Video]";else if("[作品]"==t.content&&2==i.tabbarLoginLanguage)o="[Work]";else if("[礼物]"==t.content&&2==i.tabbarLoginLanguage)o="[Gift]";else if("[图片]"==t.content&&3==i.tabbarLoginLanguage)o="[รูปภาพ]";else if("[视频]"==t.content&&3==i.tabbarLoginLanguage)o="[วิดีโอ]";else if("[作品]"==t.content&&3==i.tabbarLoginLanguage)o="[ผลงาน]";else if("[礼物]"==t.content&&3==i.tabbarLoginLanguage)o="[ของขวัญ]";t.fy_content=o}),this.getNewChatData(e)}else uni.showToast({title:a.msg,duration:1500,icon:"none"})},new_chat:function(){(0,n.navigateTo)("/pages/newchat/newchat",null)},goAchormePage:function(t){var a=encodeURIComponent((0,n.encrypt)(JSON.stringify({AnchorIdx:t,Type:2,pageId:13})));(0,n.navigateTo)("/pages/anchorpersonal/anchorpersonal",a)},goChatpopPage:function(t,a,e,i){var o=encodeURIComponent((0,n.encrypt)(JSON.stringify({useridx:t,nickname:a,headpic:e,online:i,pageId:13})));(0,n.navigateTo)("/pages/chatpop/chatpop",o)},getNewChatData:function(t){t.forEach(function(t,a){t.newMsgData=[]}),console.log(t),this.$store.commit("set_allChatList",t);var a=JSON.stringify({appId:100,useridx:this.tabbarLoginData.useridx}),e={str1:a,str2:20007};this.$store.commit("set_allTryLoginData",e),uni.onSocketOpen(function(t){console.log("WebSocket连接已打开！"),uni.sendSocketMessage({data:(0,n.sendDSocket)(a,20007),success:function(t){},complete:function(t){console.log(t)}})}),uni.sendSocketMessage({data:(0,n.sendDSocket)(a,20007),success:function(t){},complete:function(t){console.log(t)}})},getBottomNewmsg:function(){var t=this;window.onscroll=function(){var a=document.documentElement.scrollTop,e=document.documentElement.clientHeight,i=document.documentElement.scrollHeight;if(a+e==i){t.page++;var o=(0,n.base64ToArrayBuffer)((0,n.encrypt)(JSON.stringify({UserIdx:t.tabbarLoginData.useridx,Page:t.page,Limit:20}))),s=JSON.parse((0,n.decrypt)((0,n.sendData)("POST",t.GLOBAL.urlPoint+"/userinfo/GetChatList",o)));if(100==s.code){var r=JSON.parse(JSON.stringify(s.data.list));r.forEach(function(t,a){var e=new Date(t.lastTime).getMonth()+1,n=new Date(t.lastTime).getDate();t.lastTime=e+"/"+n}),t.chatListData.push(r),t.getNewChatData(t.chatListData)}else uni.showToast({title:s.msg,duration:1500,icon:"none"})}}},getScrollmsg:function(){this.page++;var t=(0,n.base64ToArrayBuffer)((0,n.encrypt)(JSON.stringify({UserIdx:this.tabbarLoginData.useridx,Page:this.page,Limit:20}))),a=JSON.parse((0,n.decrypt)((0,n.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetChatList",t)));if(100==a.code)if(0==a.data.list.length);else{var e=JSON.parse(JSON.stringify(a.data.list));e.forEach(function(t,a){var e=new Date(t.lastTime).getMonth()+1,n=new Date(t.lastTime).getDate();t.lastTime=e+"/"+n}),this.chatListData.push(e),console.log(e),this.getNewChatData(this.chatListData),console.log(this.chatListData)}else uni.showToast({title:a.msg,duration:1500,icon:"none"})}}};a.default=i}}]);