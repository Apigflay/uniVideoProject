(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["pages-homevideolist-homevideolist"],{2838:function(t,a,e){"use strict";var i=function(){var t=this,a=t.$createElement,e=t._self._c||a;return e("v-uni-view",{staticClass:"main"},[e("v-uni-view",{staticClass:"top"},[e("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/back_1.png"},on:{click:function(a){a=t.$handleEvent(a),t.goback()}}}),e("v-uni-text",[t._v(t._s(t.ModelName))])],1),e("v-uni-scroll-view",{staticClass:"content",attrs:{"scroll-y":!0},on:{scrolltolower:function(a){a=t.$handleEvent(a),t.getNewmsg(a)}}},t._l(t.model1Data,function(a,i){return e("v-uni-view",{key:i,staticClass:"list",attrs:{id:i}},[e("v-uni-image",{staticClass:"bgimg",attrs:{src:a.BackgroundPicUrl},on:{click:function(e){e=t.$handleEvent(e),t.goVideo(a)}}}),e("v-uni-image",{staticClass:"photo",attrs:{src:a.SmallPic},on:{click:function(e){e=t.$handleEvent(e),t.goAction(a)}}}),a.Online?e("v-uni-view",{staticClass:"status"}):t._e(),e("v-uni-view",{staticClass:"cont"},[t._v(t._s(a.Content))])],1)}),1)],1)},n=[];e.d(a,"a",function(){return i}),e.d(a,"b",function(){return n})},"2c41":function(t,a,e){"use strict";Object.defineProperty(a,"__esModule",{value:!0}),a.default=void 0;var i=e("28b9"),n={data:function(){return{tabbarLoginLanguage:null,pageId:9,title:null,type:null,tabbarLoginData:null,UserIdx:null,Page:1,likeid:null,model1Data:null,model2Data:null,ModelName:null}},onLoad:function(t){var a=JSON.parse((0,i.decrypt)(decodeURIComponent(t.action)));this.title=a.title,this.type=a.type,this.ModelName=a.ModelName,this.getLoginlanger(),this.getLoginMsg()},onShow:function(){},onReady:function(){},methods:{getLoginlanger:function(){var t=this;uni.getStorage({key:"storage_login_language",success:function(a){t.tabbarLoginLanguage=JSON.parse(a.data)}})},getLoginMsg:function(){var t=this;uni.getStorage({key:"storage_login_str",success:function(a){t.tabbarLoginData=JSON.parse(a.data),t.loginMsgData=t.tabbarLoginData,t.UserIdx=t.tabbarLoginData.useridx,t.likeid=t.tabbarLoginData.sex,0==t.tabbarLoginData.isAnchor&&(t.isAnchor=!1),t.getInitMsg()}})},getInitMsg:function(t){var a=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({type:this.type,useridx:this.UserIdx,page:this.Page,limit:20,likeid:this.likeid}))),e=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+"/UserInfo/HomeList",a)));100==e.code&&(this.model1Data=e.data.list)},goVideo:function(t){var a=encodeURIComponent((0,i.encrypt)(JSON.stringify({AnchorIdx:t.UserIdx,ResourceoId:t.RescourceId,pageId:this.pageId})));(0,i.navigateTo)("/pages/video/video",a)},goAction:function(t){var a=encodeURIComponent((0,i.encrypt)(JSON.stringify({AnchorIdx:t.UserIdx,Type:2,pageId:this.pageId})));(0,i.navigateTo)("/pages/anchorpersonal/anchorpersonal",a)},getNewmsg:function(){this.Page++;var t=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({type:this.type,useridx:this.UserIdx,page:this.Page,limit:20,likeid:this.likeid}))),a=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+"/UserInfo/HomeList",t)));100==a.code&&(this.model2Data=a.data.list,this.model1Data.push.apply(this.model1Data,this.model2Data))},goback:function(){(0,i.navigateTo)("/pages/home/home",null)}}};a.default=n},"8e80":function(t,a,e){"use strict";e.r(a);var i=e("2838"),n=e("d8ef");for(var o in n)"default"!==o&&function(t){e.d(a,t,function(){return n[t]})}(o);e("b852");var b=e("2877"),d=Object(b["a"])(n["default"],i["a"],i["b"],!1,null,"3220bfcb",null);a["default"]=d.exports},b852:function(t,a,e){"use strict";var i=e("e79a"),n=e.n(i);n.a},d8ef:function(t,a,e){"use strict";e.r(a);var i=e("2c41"),n=e.n(i);for(var o in i)"default"!==o&&function(t){e.d(a,t,function(){return i[t]})}(o);a["default"]=n.a},e274:function(t,a,e){a=t.exports=e("2350")(!1),a.push([t.i,'@charset "UTF-8";\n/**\r\n * 这里是uni-app内置的常用样式变量\r\n *\r\n * uni-app 官方扩展插件及插件市场（https://ext.dcloud.net.cn）上很多三方插件均使用了这些样式变量\r\n * 如果你是插件开发者，建议你使用scss预处理，并在插件代码中直接使用这些变量（无需 import 这个文件），方便用户通过搭积木的方式开发整体风格一致的App\r\n *\r\n */\n/**\r\n * 如果你是App开发者（插件使用者），你可以通过修改这些变量来定制自己的插件主题，实现自定义主题功能\r\n *\r\n * 如果你的项目同样使用了scss预处理，你也可以直接在你的 scss 代码中使用如下变量，同时无需 import 这个文件\r\n */\n/* 颜色变量 */\n/* 行为相关颜色 */\n/* 文字基本颜色 */\n/* 背景颜色 */\n/* 边框颜色 */\n/* 尺寸变量 */\n/* 文字尺寸 */\n/* 图片尺寸 */\n/* Border Radius */\n/* 水平间距 */\n/* 垂直间距 */\n/* 透明度 */\n/* 文章场景相关 */em[data-v-3220bfcb]{font-style:normal}body[data-v-3220bfcb],dd[data-v-3220bfcb],dl[data-v-3220bfcb],h1[data-v-3220bfcb],h2[data-v-3220bfcb],h3[data-v-3220bfcb],h4[data-v-3220bfcb],h5[data-v-3220bfcb],h6[data-v-3220bfcb],hr[data-v-3220bfcb],ol[data-v-3220bfcb],p[data-v-3220bfcb],pre[data-v-3220bfcb],tbody[data-v-3220bfcb],td[data-v-3220bfcb],tfoot[data-v-3220bfcb],th[data-v-3220bfcb],thead[data-v-3220bfcb],ul[data-v-3220bfcb],uni-form[data-v-3220bfcb],uni-input[data-v-3220bfcb],uni-textarea[data-v-3220bfcb]{margin:0;padding:0}ol[data-v-3220bfcb],ul[data-v-3220bfcb]{list-style:none}a[data-v-3220bfcb]{text-decoration:none}html[data-v-3220bfcb]{-ms-text-size-adjust:none;-webkit-text-size-adjust:none;-moz-text-size-adjust:none;text-size-adjust:none}body[data-v-3220bfcb]{line-height:1.5;font-size:14px}body[data-v-3220bfcb],select[data-v-3220bfcb],uni-button[data-v-3220bfcb],uni-input[data-v-3220bfcb],uni-textarea[data-v-3220bfcb]{font-family:helvetica neue,tahoma,hiragino sans gb,stheiti,wenquanyi micro hei,\\5FAE\\8F6F\\96C5\\9ED1,\\5B8B\\4F53,sans-serif}b[data-v-3220bfcb],strong[data-v-3220bfcb]{font-weight:700}em[data-v-3220bfcb],i[data-v-3220bfcb]{font-style:normal}table[data-v-3220bfcb]{border-collapse:collapse;border-spacing:0}table td[data-v-3220bfcb],table th[data-v-3220bfcb]{border:1px solid #ddd;padding:5px}table th[data-v-3220bfcb]{font-weight:inherit;border-bottom-width:2px;border-bottom-color:#ccc}img[data-v-3220bfcb]{border:0 none;width:auto\\9;max-width:100%;vertical-align:top;height:auto}select[data-v-3220bfcb],uni-button[data-v-3220bfcb],uni-input[data-v-3220bfcb],uni-textarea[data-v-3220bfcb]{font-family:inherit;font-size:100%;margin:0;vertical-align:baseline}html uni-input[type=button][data-v-3220bfcb],uni-button[data-v-3220bfcb],uni-input[type=reset][data-v-3220bfcb],uni-input[type=submit][data-v-3220bfcb]{-webkit-appearance:button;cursor:pointer}uni-button[disabled][data-v-3220bfcb],uni-input[disabled][data-v-3220bfcb]{cursor:default}uni-input[type=checkbox][data-v-3220bfcb],uni-input[type=radio][data-v-3220bfcb]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}uni-input[type=search][data-v-3220bfcb]{-webkit-appearance:textfield;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;box-sizing:content-box}uni-input[type=search][data-v-3220bfcb]::-webkit-search-decoration{-webkit-appearance:none}uni-input[data-v-3220bfcb]:focus{outline:none}select[multiple][data-v-3220bfcb],select[size][data-v-3220bfcb],select[size][multiple][data-v-3220bfcb]{border:1px solid #aaa;padding:0}article[data-v-3220bfcb],aside[data-v-3220bfcb],details[data-v-3220bfcb],figcaption[data-v-3220bfcb],figure[data-v-3220bfcb],footer[data-v-3220bfcb],header[data-v-3220bfcb],hgroup[data-v-3220bfcb],main[data-v-3220bfcb],nav[data-v-3220bfcb],section[data-v-3220bfcb],summary[data-v-3220bfcb]{display:block}uni-audio[data-v-3220bfcb],uni-canvas[data-v-3220bfcb],uni-progress[data-v-3220bfcb],uni-video[data-v-3220bfcb]{display:inline-block}body[data-v-3220bfcb]{background:#fff}uni-input[data-v-3220bfcb]::-webkit-input-speech-button{display:none}uni-button[data-v-3220bfcb],uni-input[data-v-3220bfcb],uni-textarea[data-v-3220bfcb]{-webkit-tap-highlight-color:transparent}uni-page-body[data-v-3220bfcb]{width:100%;height:100%;background:#191919}.main[data-v-3220bfcb]{width:100%;height:100%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column}.main .top[data-v-3220bfcb]{position:fixed;z-index:999;top:%?0?%;left:%?0?%;width:100%;padding:%?32?% %?0?%;background-color:#252525;text-align:center;font-size:%?30?%;color:#fff}.main .top .img[data-v-3220bfcb]{position:absolute;left:%?10?%;width:%?36?%;height:%?36?%;padding:%?10?%}.main .content[data-v-3220bfcb]{margin-top:%?100?%;height:100%}.main .content .list[data-v-3220bfcb]{float:left;margin-top:%?28?%;margin-left:%?28?%;position:relative;width:%?333?%;height:%?466?%;border-radius:%?8?%;background:#2c405a}.main .content .list .bgimg[data-v-3220bfcb]{width:%?333?%;height:%?466?%}.main .content .list .photo[data-v-3220bfcb]{position:absolute;top:%?10?%;left:%?10?%;width:%?60?%;height:%?60?%;border-radius:50%;background:#232323;border:%?2?% solid #fff}.main .content .list .status[data-v-3220bfcb]{position:absolute;top:%?51?%;left:%?51?%;width:%?20?%;height:%?20?%;border-radius:50%;background:#17ff2a}.main .content .list .cont[data-v-3220bfcb]{position:absolute;width:%?309?%;height:%?80?%;bottom:%?9?%;left:%?12?%;font-size:%?28?%;font-weight:400;color:#fff;line-height:%?40?%;display:-webkit-box;-webkit-box-orient:vertical;-webkit-line-clamp:2;overflow:hidden}body.?%PAGE?%[data-v-3220bfcb]{background:#191919}',""])},e79a:function(t,a,e){var i=e("e274");"string"===typeof i&&(i=[[t.i,i,""]]),i.locals&&(t.exports=i.locals);var n=e("4f06").default;n("5bf6deac",i,!0,{sourceMap:!1,shadowMode:!1})}}]);