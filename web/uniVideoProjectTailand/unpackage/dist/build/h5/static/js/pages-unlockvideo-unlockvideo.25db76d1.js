(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["pages-unlockvideo-unlockvideo"],{"1c7e":function(t,e,a){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var i=a("28b9"),n={data:function(){return{tabbarLoginLanguage:null,nowpageid:7,pageId:null,model1Data:null,model2Data:null,Anchordata:null,UserIdx:null,AnchorIdx:null,AnchorName:null,ResourceoId:null,tabbarLoginData:null,Page:1,Type:1}},onLoad:function(t){var e=JSON.parse((0,i.decrypt)(decodeURIComponent(t.action)));this.pageId=e.pageId,this.AnchorName=e.AnchorName,this.AnchorIdx=e.AnchorIdx,this.getLoginlanger(),this.getLoginMsg(),this.getVideo(),this.getInitMsg()},methods:{getLoginlanger:function(){var t=this;uni.getStorage({key:"storage_login_language",success:function(e){t.tabbarLoginLanguage=JSON.parse(e.data)}})},getLoginMsg:function(){var t=this;uni.getStorage({key:"storage_login_str",success:function(e){t.tabbarLoginData=JSON.parse(e.data),console.log(t.tabbarLoginData),t.UserIdx=t.tabbarLoginData.useridx,0==t.tabbarLoginData.isAnchor&&(t.isAnchor=!1)}})},getVideo:function(){if(1==this.tabbarLoginData.isAnchor)var t=1;else if(0==this.tabbarLoginData.isAnchor)t=0;var e=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({UserIdx:this.UserIdx,AnchorIdx:this.AnchorIdx,Page:this.Page,Type:this.Type,identity:t}))),a=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetBuyList",e)));100==a.code&&(console.log(a.data),this.model1Data=a.data.list)},getNewlist:function(){if(this.Page++,1==this.tabbarLoginData.isAnchor)var t=1;else if(0==this.tabbarLoginData.isAnchor)t=0;var e=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({UserIdx:this.UserIdx,AnchorIdx:this.AnchorIdx,Page:this.Page,Type:this.Type,identity:t}))),a=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetBuyList",e)));100==a.code&&(console.log(a.data),this.model2Data=a.data.list,this.model1Data.push.apply(this.model1Data,this.model2Data))},goBackPage:function(){6==this.pageId?(0,i.navigateTo)("/pages/anchorme/anchorme",null):17==this.pageId&&(0,i.navigateTo)("/pages/my/my",null)},goVideo:function(t){uni.navigateTo({url:"/pages/video/video?UserIdx="+this.UserIdx+"&AnchorIdx="+t.AnchorIdx+"&ResourceoId="+t.ResourcesId+"&pageId="+this.nowpageid});var e=encodeURIComponent((0,i.encrypt)(JSON.stringify({UserIdx:this.UserIdx,AnchorIdx:t.AnchorIdx,ResourceoId:t.ResourcesId,pageId:this.nowpageid})));(0,i.navigateTo)("/pages/video/video",e)},getInitMsg:function(t){var e=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({UserIdx:this.UserIdx,AnchorIdx:this.AnchorIdx}))),a=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetAnchorInfo",e)));100==a.code&&(this.Anchordata=a.data,console.log(this.Anchordata))},goGift:function(){var t=encodeURIComponent((0,i.encrypt)(JSON.stringify({useridx:this.Anchordata.Anchor.UserIdx,nickname:this.Anchordata.Anchor.MyName,headpic:this.Anchordata.Anchor.SmallPic,online:this.Anchordata.Anchor.Online,pageId:7,pagebackId:this.pageId})));(0,i.navigateTo)("/pages/chatpop/chatpop",t)},goChat:function(){var t=this;uni.getStorage({key:"storage_login_str",success:function(e){var a=JSON.stringify({userName:JSON.stringify(JSON.parse(e.data).useridx),pwd:JSON.parse(e.data).logintoken,devId:(0,i.systemId)(),devType:(0,i.system)(),productType:3,isRelogin:!0,loginWay:0,language:3,ver:"1.00",ip:"127.0.0.1"}),n=(0,i.sendD)(a);0==t.$Socket.isconnect&&t.$Socket.onReload(),t.$Socket.nsend(n),t.$Socket.eventPatch.onMsg(function(e,a){setTimeout(function(){var a=new FileReader;a.onload=function(e){var a=this.result,n=new Uint32Array(a,0,3),o=new Uint8Array(a,12,n[0]-12-1),d=(new TextDecoder).decode(o),s=JSON.parse(d);if(10002==n[1]){console.log("断线重连成功"),uni.setStorage({key:"storage_login_str",data:d,success:function(){}});var r=encodeURIComponent((0,i.encrypt)(JSON.stringify({useridx:t.Anchordata.Anchor.UserIdx,nickname:t.Anchordata.Anchor.MyName,headpic:t.Anchordata.Anchor.SmallPic,online:t.Anchordata.Anchor.Online,pageId:7,pagebackId:t.pageId})));(0,i.navigateTo)("/pages/chatpop/chatpop",r)}else 10003==n[1]&&((0,i.navigateTo)("/pages/startup/startup",null),uni.showToast({title:s.error,duration:1e3,icon:"none"}))},a.readAsArrayBuffer(e.data)},0)})}})}}};e.default=n},"2c40":function(t,e,a){"use strict";a.r(e);var i=a("1c7e"),n=a.n(i);for(var o in i)"default"!==o&&function(t){a.d(e,t,function(){return i[t]})}(o);e["default"]=n.a},8086:function(t,e,a){e=t.exports=a("2350")(!1),e.push([t.i,'@charset "UTF-8";\n/**\r\n * 这里是uni-app内置的常用样式变量\r\n *\r\n * uni-app 官方扩展插件及插件市场（https://ext.dcloud.net.cn）上很多三方插件均使用了这些样式变量\r\n * 如果你是插件开发者，建议你使用scss预处理，并在插件代码中直接使用这些变量（无需 import 这个文件），方便用户通过搭积木的方式开发整体风格一致的App\r\n *\r\n */\n/**\r\n * 如果你是App开发者（插件使用者），你可以通过修改这些变量来定制自己的插件主题，实现自定义主题功能\r\n *\r\n * 如果你的项目同样使用了scss预处理，你也可以直接在你的 scss 代码中使用如下变量，同时无需 import 这个文件\r\n */\n/* 颜色变量 */\n/* 行为相关颜色 */\n/* 文字基本颜色 */\n/* 背景颜色 */\n/* 边框颜色 */\n/* 尺寸变量 */\n/* 文字尺寸 */\n/* 图片尺寸 */\n/* Border Radius */\n/* 水平间距 */\n/* 垂直间距 */\n/* 透明度 */\n/* 文章场景相关 */em[data-v-85de7718]{font-style:normal}body[data-v-85de7718],dd[data-v-85de7718],dl[data-v-85de7718],h1[data-v-85de7718],h2[data-v-85de7718],h3[data-v-85de7718],h4[data-v-85de7718],h5[data-v-85de7718],h6[data-v-85de7718],hr[data-v-85de7718],ol[data-v-85de7718],p[data-v-85de7718],pre[data-v-85de7718],tbody[data-v-85de7718],td[data-v-85de7718],tfoot[data-v-85de7718],th[data-v-85de7718],thead[data-v-85de7718],ul[data-v-85de7718],uni-form[data-v-85de7718],uni-input[data-v-85de7718],uni-textarea[data-v-85de7718]{margin:0;padding:0}ol[data-v-85de7718],ul[data-v-85de7718]{list-style:none}a[data-v-85de7718]{text-decoration:none}html[data-v-85de7718]{-ms-text-size-adjust:none;-webkit-text-size-adjust:none;-moz-text-size-adjust:none;text-size-adjust:none}body[data-v-85de7718]{line-height:1.5;font-size:14px}body[data-v-85de7718],select[data-v-85de7718],uni-button[data-v-85de7718],uni-input[data-v-85de7718],uni-textarea[data-v-85de7718]{font-family:helvetica neue,tahoma,hiragino sans gb,stheiti,wenquanyi micro hei,\\5FAE\\8F6F\\96C5\\9ED1,\\5B8B\\4F53,sans-serif}b[data-v-85de7718],strong[data-v-85de7718]{font-weight:700}em[data-v-85de7718],i[data-v-85de7718]{font-style:normal}table[data-v-85de7718]{border-collapse:collapse;border-spacing:0}table td[data-v-85de7718],table th[data-v-85de7718]{border:1px solid #ddd;padding:5px}table th[data-v-85de7718]{font-weight:inherit;border-bottom-width:2px;border-bottom-color:#ccc}img[data-v-85de7718]{border:0 none;width:auto\\9;max-width:100%;vertical-align:top;height:auto}select[data-v-85de7718],uni-button[data-v-85de7718],uni-input[data-v-85de7718],uni-textarea[data-v-85de7718]{font-family:inherit;font-size:100%;margin:0;vertical-align:baseline}html uni-input[type=button][data-v-85de7718],uni-button[data-v-85de7718],uni-input[type=reset][data-v-85de7718],uni-input[type=submit][data-v-85de7718]{-webkit-appearance:button;cursor:pointer}uni-button[disabled][data-v-85de7718],uni-input[disabled][data-v-85de7718]{cursor:default}uni-input[type=checkbox][data-v-85de7718],uni-input[type=radio][data-v-85de7718]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}uni-input[type=search][data-v-85de7718]{-webkit-appearance:textfield;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;box-sizing:content-box}uni-input[type=search][data-v-85de7718]::-webkit-search-decoration{-webkit-appearance:none}uni-input[data-v-85de7718]:focus{outline:none}select[multiple][data-v-85de7718],select[size][data-v-85de7718],select[size][multiple][data-v-85de7718]{border:1px solid #aaa;padding:0}article[data-v-85de7718],aside[data-v-85de7718],details[data-v-85de7718],figcaption[data-v-85de7718],figure[data-v-85de7718],footer[data-v-85de7718],header[data-v-85de7718],hgroup[data-v-85de7718],main[data-v-85de7718],nav[data-v-85de7718],section[data-v-85de7718],summary[data-v-85de7718]{display:block}uni-audio[data-v-85de7718],uni-canvas[data-v-85de7718],uni-progress[data-v-85de7718],uni-video[data-v-85de7718]{display:inline-block}body[data-v-85de7718]{background:#fff}uni-input[data-v-85de7718]::-webkit-input-speech-button{display:none}uni-button[data-v-85de7718],uni-input[data-v-85de7718],uni-textarea[data-v-85de7718]{-webkit-tap-highlight-color:transparent}uni-page-body[data-v-85de7718]{width:100%;height:100%;background:#191919}.main[data-v-85de7718]{width:100%;height:100%;background:#191919;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column}.main .nav[data-v-85de7718]{position:relative;height:%?100?%;background:#252525;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:center;-webkit-justify-content:center;-ms-flex-pack:center;justify-content:center;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;font-size:%?30?%;color:#fff}.main .nav .back[data-v-85de7718]{position:absolute;left:%?20?%;height:%?36?%;width:%?36?%;padding:%?10?%}.main .nav .add[data-v-85de7718]{height:%?36?%;width:%?36?%;padding:%?10?%;margin-right:%?19?%}.main .mainArea[data-v-85de7718]{width:%?722?%;padding-left:%?28?%;-webkit-box-flex:1;-webkit-flex:1;-ms-flex:1;flex:1;background:#191919;overflow-y:scroll;padding-bottom:%?80?%}.main .mainArea .listPer[data-v-85de7718]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;border-bottom:2px solid #343434}.main .mainArea .listPer .left[data-v-85de7718]{padding:%?12?% %?0?% %?11?% %?0?%;height:%?90?%;width:%?90?%}.main .mainArea .listPer .left .photo[data-v-85de7718]{background:#646464;height:%?90?%;width:%?90?%;border-radius:%?8?%}.main .mainArea .listPer .content[data-v-85de7718]{width:100%;margin-left:%?39?%;margin-right:%?28?%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between}.main .mainArea .listPer .content .center .p[data-v-85de7718]{margin-top:%?10?%;font-size:%?20?%;font-weight:400;color:#fff;display:-webkit-box;-webkit-box-orient:vertical;-webkit-line-clamp:2;overflow:hidden}.main .mainArea .listPer .content .center .p .p1[data-v-85de7718]{font-size:%?26?%;font-weight:500;color:#fff;margin-right:%?27?%}.main .mainArea .listPer .content .bottom[data-v-85de7718]{width:100%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;margin-bottom:%?12?%}.main .mainArea .listPer .content .bottom .b_left[data-v-85de7718]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;margin-top:%?11?%}.main .mainArea .listPer .content .bottom .b_left .img[data-v-85de7718]{width:%?23?%;height:%?21?%;margin:%?0?% %?12?% %?0?% %?4?%}.main .mainArea .listPer .content .bottom .b_left .imgese[data-v-85de7718]{width:%?23?%;height:%?15?%;margin:%?0?% %?12?% %?0?% %?4?%}.main .mainArea .listPer .content .bottom .b_left .p[data-v-85de7718]{font-size:%?22?%;font-weight:400;color:#fff;line-height:%?22?%}.main .mainArea .listPer .content .bottom .b_right[data-v-85de7718]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;margin-top:%?12?%}.main .mainArea .listPer .content .bottom .b_right .img[data-v-85de7718]{width:%?22?%;height:%?16?%}.main .mainArea .listPer .content .bottom .b_right .p[data-v-85de7718]{margin:%?0?% %?1?% %?0?% %?8?%;color:#ffd600;font-size:%?20?%;font-weight:400;line-height:%?20?%}.main .function[data-v-85de7718]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;width:%?492?%;height:%?80?%;position:fixed;bottom:%?0?%;left:%?0?%;padding:%?0?% %?129?%;background:#252525}.main .function .gift[data-v-85de7718]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center}.main .function .gift .img[data-v-85de7718]{width:%?41?%;height:%?39?%}.main .function .gift .p[data-v-85de7718]{font-size:%?30?%;color:#fff;margin-left:%?17?%}.main .function .center_line[data-v-85de7718]{width:%?2?%;height:%?44?%;background:#747474}body.?%PAGE?%[data-v-85de7718]{background:#191919}',""])},"9e61":function(t,e,a){"use strict";var i=a("ba46"),n=a.n(i);n.a},ba46:function(t,e,a){var i=a("8086");"string"===typeof i&&(i=[[t.i,i,""]]),i.locals&&(t.exports=i.locals);var n=a("4f06").default;n("1cc0f32c",i,!0,{sourceMap:!1,shadowMode:!1})},ccd1:function(t,e,a){"use strict";var i=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-uni-view",{staticClass:"main"},[a("v-uni-view",{staticClass:"nav"},[a("v-uni-image",{staticClass:"back",attrs:{src:"../../static/imgs/more1w.png",mode:""},on:{click:function(e){e=t.$handleEvent(e),t.goBackPage(e)}}}),a("v-uni-text",{staticClass:"title"},[t._v(t._s(t.AnchorName))])],1),a("v-uni-scroll-view",{staticClass:"mainArea",attrs:{"scroll-y":"true"},on:{scrolltolower:function(e){e=t.$handleEvent(e),t.getNewlist(e)}}},t._l(t.model1Data,function(e,i){return a("v-uni-view",{key:i,staticClass:"listPer",on:{click:function(a){a=t.$handleEvent(a),t.goVideo(e)}}},[a("v-uni-view",{staticClass:"left"},[a("v-uni-image",{staticClass:"photo",attrs:{src:e.BackgroundPicUrl}})],1),a("v-uni-view",{staticClass:"content"},[a("v-uni-view",{staticClass:"center"},[a("v-uni-text",{staticClass:"p"},[a("v-uni-text",{staticClass:"p1"}),t._v(t._s(e.Content))],1)],1),a("v-uni-view",{staticClass:"bottom"},[a("v-uni-view",{staticClass:"b_left"},[a("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/bofang_1.png"}}),0!=e.UnLockMoney?a("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/unlock_1.png"}}):t._e(),0!=e.UnLockMoney?a("v-uni-text",{staticClass:"p"},[t._v(t._s(e.UnLockCount))]):t._e(),0==e.UnLockMoney?a("v-uni-image",{staticClass:"imgese",attrs:{src:"../../static/imgs/eyesT.png"}}):t._e(),0==e.UnLockMoney?a("v-uni-text",{staticClass:"p"},[t._v(t._s(e.VisitCount))]):t._e(),a("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/fabulous_1.png"}}),0==e.GoodEvaluate?a("v-uni-text",{staticClass:"p"},[t._v("0%")]):a("v-uni-text",{staticClass:"p"},[t._v(t._s(Math.floor(e.GoodEvaluate/(e.GoodEvaluate+e.BadEvaluate)*100))+"%")])],1)],1)],1)],1)}),1),a("v-uni-view",{staticClass:"function",on:{click:function(e){e=t.$handleEvent(e),t.goChat()}}},[a("v-uni-view",{staticClass:"gift"},[a("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/liwu_1.png"}}),a("v-uni-text",{staticClass:"p"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language75))])],1),a("v-uni-view",{staticClass:"center_line"}),a("v-uni-view",{staticClass:"gift"},[a("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/chat1_1.png"}}),a("v-uni-text",{staticClass:"p"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language76))])],1)],1)],1)},n=[];a.d(e,"a",function(){return i}),a.d(e,"b",function(){return n})},db42:function(t,e,a){"use strict";a.r(e);var i=a("ccd1"),n=a("2c40");for(var o in n)"default"!==o&&function(t){a.d(e,t,function(){return n[t]})}(o);a("9e61");var d=a("2877"),s=Object(d["a"])(n["default"],i["a"],i["b"],!1,null,"85de7718",null);e["default"]=s.exports}}]);