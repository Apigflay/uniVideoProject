(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["pages-my-my"],{"470e":function(t,e,a){"use strict";var i=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-uni-view",{staticClass:"contentBox"},[a("v-uni-scroll-view",{staticClass:"scrollView",attrs:{"scroll-y":"true"},on:{scrolltolower:function(e){e=t.$handleEvent(e),t.getNewlist(e)},scroll:function(e){e=t.$handleEvent(e),t.getScrollTopMsg(e)}}},[a("v-uni-view",{staticClass:"main"},[a("v-uni-view",{staticClass:"top"},[a("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/shezhi_1.png"},on:{click:function(e){e=t.$handleEvent(e),t.goDatasetPage(e)}}}),a("v-uni-view",{staticClass:"imgArea",on:{click:function(e){e=t.$handleEvent(e),t.goPay()}}},[a("v-uni-image",{staticClass:"size",attrs:{src:"../../static/imgs/goldM2.png"}}),a("v-uni-text",{staticClass:"font"},[t._v(t._s(this.$store.getters["AllallLoginInfo"].cash))])],1)],1),t.show?a("v-uni-view",{staticClass:"move"},[a("v-uni-text",{staticClass:"p"},[t._v(t._s(t.name))])],1):t._e(),t.show1?a("v-uni-view",{staticClass:"move1"},[a("v-uni-text",{staticClass:"p"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language65))])],1):t._e(),a("v-uni-view",{staticClass:"top_bj",style:{backgroundImage:"url("+t.tabbarLoginData.background+")",backgroundSize:"cover",backgroundPosition:"center"}}),a("v-uni-view",{staticClass:"center"},[a("v-uni-image",{staticClass:"photo",attrs:{src:t.tabbarLoginData.headpic}}),a("v-uni-view",{staticClass:"follow",on:{click:function(e){e=t.$handleEvent(e),t.goFollow()}}},[a("v-uni-text",{staticClass:"p"},[t._v(t._s(t.mymsg.Follow))]),a("v-uni-text",{staticClass:"p1"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language62))])],1),a("v-uni-view",{staticClass:"follow",on:{click:function(e){e=t.$handleEvent(e),t.goFans()}}},[a("v-uni-text",{staticClass:"p"},[t._v(t._s(t.mymsg.Fans))]),a("v-uni-text",{staticClass:"p1"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language63))])],1),a("v-uni-view",{staticClass:"edit",on:{click:function(e){e=t.$handleEvent(e),t.goUpdateMsg(e)}}},[a("v-uni-image",{staticClass:"img",attrs:{src:"../../static/pictures/edit_1.png"}}),a("v-uni-text",{staticClass:"p"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language64))])],1)],1),a("v-uni-view",{staticClass:"center1"}),a("v-uni-view",{staticClass:"name"},[t._v(t._s(t.tabbarLoginData.nickname))]),a("v-uni-view",{staticClass:"purchased "},[a("v-uni-view",{staticClass:"p"},[a("v-uni-text",{staticClass:"p1"},[t._v(t._s(this.Language.language[this.tabbarLoginLanguage].language65))])],1),a("v-uni-view",{staticClass:"list"},t._l(t.model1Data,function(e,i){return a("v-uni-view",{key:i,staticClass:"pro",attrs:{id:i},on:{click:function(a){a=t.$handleEvent(a),t.goVideolist(e)}}},[a("v-uni-image",{staticClass:"photo",attrs:{src:e.SmallPic}}),a("v-uni-view",{staticClass:"id"},[a("v-uni-text",{staticClass:"p1"},[t._v(t._s(e.MyName))]),a("v-uni-view",{directives:[{name:"show",rawName:"v-show",value:e.status,expression:"item.status"}],staticClass:"status"})],1),a("v-uni-text",{staticClass:"p2"},[t._v(t._s(e.Cont)+t._s(t.fy_tw[t.tabbarLoginLanguage]))])],1)}),1)],1),a("v-uni-view",{staticClass:"tabbarArea"},t._l(t.tabbarData,function(e,i){return a("v-uni-view",{key:i,staticClass:"per",attrs:{id:i},on:{click:function(e){e=t.$handleEvent(e),t.getClickPer(e)}}},[a("v-uni-image",{staticClass:"img",attrs:{src:e.imgsrcW,mode:""}})],1)}),1),a("v-uni-view",{staticClass:"bottomFix"})],1)],1)],1)},n=[];a.d(e,"a",function(){return i}),a.d(e,"b",function(){return n})},"51ae":function(t,e,a){var i=a("c483");"string"===typeof i&&(i=[[t.i,i,""]]),i.locals&&(t.exports=i.locals);var n=a("4f06").default;n("f9bc6abc",i,!0,{sourceMap:!1,shadowMode:!1})},7660:function(t,e,a){"use strict";a.r(e);var i=a("470e"),n=a("c278");for(var s in n)"default"!==s&&function(t){a.d(e,t,function(){return n[t]})}(s);a("d347");var o=a("2877"),d=Object(o["a"])(n["default"],i["a"],i["b"],!1,null,"0ddfc844",null);e["default"]=d.exports},aaf6:function(t,e,a){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var i=a("a84a"),n={data:function(){return{tabbarLoginLanguage:null,show:!1,show1:!1,UserIdx:null,AnchorIdx:null,Page:1,Type:0,pageId:17,tabbarLoginData:null,isAnchor:null,tabbarCurrent:0,tabbarData:[{imgsrcB:"../../static/imgs/room1w.png",imgsrcW:"../../static/imgs/room1w.png",text:"1"},{imgsrcB:"../../static/imgs/fang1w.png",imgsrcW:"../../static/imgs/fang1w.png",text:"2"},{imgsrcB:"../../static/imgs/mail1w.png",imgsrcW:"../../static/imgs/mail1w.png",text:"3"},{imgsrcB:"../../static/imgs/xin1w.png",imgsrcW:"../../static/imgs/xin1w.png",text:"4"},{imgsrcB:"../../static/imgs/my1w.png",imgsrcW:"../../static/imgs/my1y.png",text:"5"}],model1Data:null,model2Data:null,mymsg:null,fy_tw:["贴文","貼文","post","โพสต"],url_down:"/userinfo/GetBuyList"}},onLoad:function(){this.getLoginlanger(),this.getLoginMsg(),this.getVideo(),this.getMyMsg(),this.getCashChange()},onReady:function(){this.reSocket()},onPageScroll:function(){},methods:{reSocket:function(){var t=this;setTimeout(function(){console.log(t),uni.getStorage({key:"storage_login_str",success:function(e){var a=JSON.stringify({userName:JSON.stringify(JSON.parse(e.data).useridx),pwd:JSON.parse(e.data).logintoken,devId:(0,i.systemId)(),devType:(0,i.system)(),productType:3,isRelogin:!0,loginWay:0,language:0,ver:"1.00",ip:"127.0.0.1"}),n=(0,i.sendD)(a);console.log(t),console.log(t.$Socket),0==t.$Socket.isconnect&&t.$Socket.onReload(),t.$Socket.nsend(n),t.$Socket.eventPatch.onMsg(function(t,e){setTimeout(function(){var e=new FileReader;e.onload=function(t){var e=this.result,a=new Uint32Array(e,0,3),n=new Uint8Array(e,12,a[0]-12-1),s=(new TextDecoder).decode(n),o=JSON.parse(s);10002==a[1]?(console.log("断线重连成功"),uni.setStorage({key:"storage_login_str",data:s,success:function(){}})):10003==a[1]&&((0,i.navigateTo)("/pages/startup/startup",null),uni.showToast({title:o.error,duration:1e3,icon:"none"}))},e.readAsArrayBuffer(t.data)},0)})}})},3e3)},getLoginlanger:function(){var t=this;uni.getStorage({key:"storage_login_language",success:function(e){t.tabbarLoginLanguage=JSON.parse(e.data)}})},getLoginMsg:function(){var t=this;uni.getStorage({key:"storage_login_str",success:function(e){t.tabbarLoginData=JSON.parse(e.data),t.UserIdx=t.tabbarLoginData.useridx,0==t.tabbarLoginData.isAnchor&&(t.isAnchor=!1)}})},getMyMsg:function(){var t=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({UserIdx:this.UserIdx}))),e=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetCatUserInfo",t)));100==e.code&&(this.mymsg=e.data)},getClickPer:function(t){this.tabbarCurrent=t.currentTarget.id,0==t.currentTarget.id?(0,i.navigateTo)("/pages/home/home",null):1==t.currentTarget.id?(0,i.navigateTo)("/pages/search/search",null):2==t.currentTarget.id?(0,i.navigateTo)("/pages/news/news",null):3==t.currentTarget.id?(0,i.navigateTo)("/pages/chat/chat",null):4==t.currentTarget.id&&(console.log(this.isAnchor),0==this.isAnchor?(console.log("不是主播"),(0,i.navigateTo)("/pages/my/my",null)):(0,i.navigateTo)("/pages/anchorme/anchorme",null))},getVideo:function(){var t=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({UserIdx:this.UserIdx,AnchorIdx:0,Page:this.Page,Type:this.Type}))),e=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetBuyList",t)));100==e.code&&(console.log(e.data),this.model1Data=e.data.list)},goVideolist:function(t){var e=encodeURIComponent((0,i.encrypt)(JSON.stringify({AnchorName:t.MyName,AnchorIdx:t.AnchorIdx,pageId:this.pageId})));(0,i.navigateTo)("/pages/unlockvideo/unlockvideo",e)},goUpdateMsg:function(){(0,i.navigateTo)("/pages/changeintroduction/changeintroduction",null)},goProductPage:function(){(0,i.navigateTo)("/pages/commoditymanagement/commoditymanagement",null)},goRecivePage:function(){(0,i.navigateTo)("/pages/revenuemanagement/revenuemanagement",null)},goDatasetPage:function(){(0,i.navigateTo)("/pages/dataset/dataset",null)},goFollow:function(){var t=encodeURIComponent((0,i.encrypt)(JSON.stringify({pageId:this.pageId})));(0,i.navigateTo)("/pages/followpopup/followpopup",t)},goFans:function(){var t=encodeURIComponent((0,i.encrypt)(JSON.stringify({pageId:this.pageId})));(0,i.navigateTo)("/pages/fanspopup/fanspopup",t)},goPay:function(){(0,i.navigateTo)("/pages/paymoney/paymoney",null)},getNewlist:function(){this.Page++;var t=(0,i.base64ToArrayBuffer)((0,i.encrypt)(JSON.stringify({UserIdx:this.UserIdx,AnchorIdx:0,Page:this.Page,Type:0}))),e=JSON.parse((0,i.decrypt)((0,i.sendData)("POST",this.GLOBAL.urlPoint+this.url_down,t)));console.log(e),100==e.code&&(0==e.data.list.length&&(this.url_down=""),this.model2Data=e.data.list,this.model1Data.push.apply(this.model1Data,this.model2Data))},getScrollTopMsg:function(t){t.detail.scrollTop>=100?this.show=!0:this.show=!1,t.detail.scrollTop>=210?this.show1=!0:this.show1=!1},getCashChange:function(){var t=this;0==this.$Socket.isconnect&&this.$Socket.onReload(),this.$Socket.eventPatch.onMsg(function(e,a){setTimeout(function(){t.$Socket.eventPatch.events.onMsg=[t.$Socket.eventPatch.events.onMsg[0],t.$Socket.eventPatch.events.onMsg[t.$Socket.eventPatch.events.onMsg.length-1]];var a=new FileReader;a.onload=function(e){var a=this.result,i=new Uint32Array(a,0,3),n=new Uint8Array(a,12,i[0]-12-1),s=(new TextDecoder).decode(n),o=JSON.parse(s);11006==i[1]&&(t.tabbarLoginData.cash=o.cash,uni.getStorage({key:"storage_login_str",success:function(t){var e=JSON.parse(t.data);e.cash=o.cash;var a=JSON.stringify(e);uni.setStorage({key:"storage_login_str",data:a,success:function(){}})}}))},a.readAsArrayBuffer(e.data)},0)})}}};e.default=n},c278:function(t,e,a){"use strict";a.r(e);var i=a("aaf6"),n=a.n(i);for(var s in i)"default"!==s&&function(t){a.d(e,t,function(){return i[t]})}(s);e["default"]=n.a},c483:function(t,e,a){e=t.exports=a("2350")(!1),e.push([t.i,'@charset "UTF-8";\n/**\r\n * 这里是uni-app内置的常用样式变量\r\n *\r\n * uni-app 官方扩展插件及插件市场（https://ext.dcloud.net.cn）上很多三方插件均使用了这些样式变量\r\n * 如果你是插件开发者，建议你使用scss预处理，并在插件代码中直接使用这些变量（无需 import 这个文件），方便用户通过搭积木的方式开发整体风格一致的App\r\n *\r\n */\n/**\r\n * 如果你是App开发者（插件使用者），你可以通过修改这些变量来定制自己的插件主题，实现自定义主题功能\r\n *\r\n * 如果你的项目同样使用了scss预处理，你也可以直接在你的 scss 代码中使用如下变量，同时无需 import 这个文件\r\n */\n/* 颜色变量 */\n/* 行为相关颜色 */\n/* 文字基本颜色 */\n/* 背景颜色 */\n/* 边框颜色 */\n/* 尺寸变量 */\n/* 文字尺寸 */\n/* 图片尺寸 */\n/* Border Radius */\n/* 水平间距 */\n/* 垂直间距 */\n/* 透明度 */\n/* 文章场景相关 */em[data-v-0ddfc844]{font-style:normal}body[data-v-0ddfc844],dd[data-v-0ddfc844],dl[data-v-0ddfc844],h1[data-v-0ddfc844],h2[data-v-0ddfc844],h3[data-v-0ddfc844],h4[data-v-0ddfc844],h5[data-v-0ddfc844],h6[data-v-0ddfc844],hr[data-v-0ddfc844],ol[data-v-0ddfc844],p[data-v-0ddfc844],pre[data-v-0ddfc844],tbody[data-v-0ddfc844],td[data-v-0ddfc844],tfoot[data-v-0ddfc844],th[data-v-0ddfc844],thead[data-v-0ddfc844],ul[data-v-0ddfc844],uni-form[data-v-0ddfc844],uni-input[data-v-0ddfc844],uni-textarea[data-v-0ddfc844]{margin:0;padding:0}ol[data-v-0ddfc844],ul[data-v-0ddfc844]{list-style:none}a[data-v-0ddfc844]{text-decoration:none}html[data-v-0ddfc844]{-ms-text-size-adjust:none;-webkit-text-size-adjust:none;-moz-text-size-adjust:none;text-size-adjust:none}body[data-v-0ddfc844]{line-height:1.5;font-size:14px}body[data-v-0ddfc844],select[data-v-0ddfc844],uni-button[data-v-0ddfc844],uni-input[data-v-0ddfc844],uni-textarea[data-v-0ddfc844]{font-family:helvetica neue,tahoma,hiragino sans gb,stheiti,wenquanyi micro hei,\\5FAE\\8F6F\\96C5\\9ED1,\\5B8B\\4F53,sans-serif}b[data-v-0ddfc844],strong[data-v-0ddfc844]{font-weight:700}em[data-v-0ddfc844],i[data-v-0ddfc844]{font-style:normal}table[data-v-0ddfc844]{border-collapse:collapse;border-spacing:0}table td[data-v-0ddfc844],table th[data-v-0ddfc844]{border:1px solid #ddd;padding:5px}table th[data-v-0ddfc844]{font-weight:inherit;border-bottom-width:2px;border-bottom-color:#ccc}img[data-v-0ddfc844]{border:0 none;width:auto\\9;max-width:100%;vertical-align:top;height:auto}select[data-v-0ddfc844],uni-button[data-v-0ddfc844],uni-input[data-v-0ddfc844],uni-textarea[data-v-0ddfc844]{font-family:inherit;font-size:100%;margin:0;vertical-align:baseline}html uni-input[type=button][data-v-0ddfc844],uni-button[data-v-0ddfc844],uni-input[type=reset][data-v-0ddfc844],uni-input[type=submit][data-v-0ddfc844]{-webkit-appearance:button;cursor:pointer}uni-button[disabled][data-v-0ddfc844],uni-input[disabled][data-v-0ddfc844]{cursor:default}uni-input[type=checkbox][data-v-0ddfc844],uni-input[type=radio][data-v-0ddfc844]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}uni-input[type=search][data-v-0ddfc844]{-webkit-appearance:textfield;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;box-sizing:content-box}uni-input[type=search][data-v-0ddfc844]::-webkit-search-decoration{-webkit-appearance:none}uni-input[data-v-0ddfc844]:focus{outline:none}select[multiple][data-v-0ddfc844],select[size][data-v-0ddfc844],select[size][multiple][data-v-0ddfc844]{border:1px solid #aaa;padding:0}article[data-v-0ddfc844],aside[data-v-0ddfc844],details[data-v-0ddfc844],figcaption[data-v-0ddfc844],figure[data-v-0ddfc844],footer[data-v-0ddfc844],header[data-v-0ddfc844],hgroup[data-v-0ddfc844],main[data-v-0ddfc844],nav[data-v-0ddfc844],section[data-v-0ddfc844],summary[data-v-0ddfc844]{display:block}uni-audio[data-v-0ddfc844],uni-canvas[data-v-0ddfc844],uni-progress[data-v-0ddfc844],uni-video[data-v-0ddfc844]{display:inline-block}body[data-v-0ddfc844]{background:#fff}uni-input[data-v-0ddfc844]::-webkit-input-speech-button{display:none}uni-button[data-v-0ddfc844],uni-input[data-v-0ddfc844],uni-textarea[data-v-0ddfc844]{-webkit-tap-highlight-color:transparent}uni-page-body[data-v-0ddfc844]{width:100%;height:100%;background:#232323}.contentBox[data-v-0ddfc844]{width:100%;height:100%;overflow:hidden}.scrollView[data-v-0ddfc844]{width:100%;height:100%}.scrollView .main[data-v-0ddfc844]{width:100%;height:100%}.scrollView .main .bottomFix[data-v-0ddfc844]{width:100%;height:%?90?%}.scrollView .main .top_bj[data-v-0ddfc844]{height:%?400?%;width:100%;background:#151515}.scrollView .main .top[data-v-0ddfc844]{height:%?150?%;width:100%;position:fixed;top:0;left:0;z-index:9999;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center}.scrollView .main .top .img[data-v-0ddfc844]{width:%?42?%;height:%?40?%;padding:%?10?%;margin-left:%?25?%;background-image:-webkit-gradient(linear,left top,left bottom,from(rgba(0,0,0,.5)));background-image:-o-linear-gradient(rgba(0,0,0,.5));background-image:linear-gradient(rgba(0,0,0,.5));border-radius:50%}.scrollView .main .top .imgArea[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;background:rgba(0,0,0,.5);border-radius:%?6?%;margin-right:%?28?%}.scrollView .main .top .imgArea .size[data-v-0ddfc844]{width:%?30?%;height:%?30?%;margin-right:%?13?%;margin-left:%?20?%;margin-top:%?14?%;margin-bottom:%?14?%;vertical-align:text-bottom}.scrollView .main .top .imgArea .font[data-v-0ddfc844]{font-size:%?26?%;color:#ffd600;line-height:%?22?%;margin-right:%?18?%;margin-top:%?14?%;margin-bottom:%?14?%}.scrollView .main .move[data-v-0ddfc844]{position:fixed;top:0;left:0;z-index:9998;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:center;-webkit-justify-content:center;-ms-flex-pack:center;justify-content:center;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;width:100%;height:%?150?%;background-color:#252525;-o-transition:all 5s;transition:all 5s;-webkit-transition:all 5s\n      /* Safari 和 Chrome */}.scrollView .main .move .p[data-v-0ddfc844]{color:#fff;font-size:%?30?%}.scrollView .main .move1[data-v-0ddfc844]{position:fixed;top:%?150?%;left:0;z-index:9998;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:center;-webkit-justify-content:center;-ms-flex-pack:center;justify-content:center;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;width:100%;height:%?50?%;background-color:#343434}.scrollView .main .move1 .p[data-v-0ddfc844]{font-size:%?22?%;color:#fff}.scrollView .main .center[data-v-0ddfc844],.scrollView .main .center1[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:start;-webkit-justify-content:flex-start;-ms-flex-pack:start;justify-content:flex-start;-webkit-box-align:start;-webkit-align-items:flex-start;-ms-flex-align:start;align-items:flex-start;margin-top:%?-100?%;padding:%?0?% %?28?% %?0?% %?28?%;width:100%;height:%?100?%}.scrollView .main .center1 .photo[data-v-0ddfc844],.scrollView .main .center .photo[data-v-0ddfc844]{width:%?160?%;height:%?160?%;background:#151515;border:2px solid #fff;border-radius:50%;margin-right:%?23?%}.scrollView .main .center1 .follow[data-v-0ddfc844],.scrollView .main .center .follow[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;padding:%?10?%;margin-top:%?8?%;margin-left:%?8?%}.scrollView .main .center1 .follow .p[data-v-0ddfc844],.scrollView .main .center .follow .p[data-v-0ddfc844]{color:#fff;font-size:%?26?%}.scrollView .main .center1 .follow .p1[data-v-0ddfc844],.scrollView .main .center .follow .p1[data-v-0ddfc844]{color:#fff;font-size:%?22?%}.scrollView .main .center1 .edit[data-v-0ddfc844],.scrollView .main .center .edit[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:start;-webkit-justify-content:flex-start;-ms-flex-pack:start;justify-content:flex-start;-webkit-box-align:start;-webkit-align-items:flex-start;-ms-flex-align:start;align-items:flex-start;margin-left:%?40?%;padding:%?10?%;padding:%?11?% %?19?%;border:2px solid #fff;border-radius:%?8?%;margin-top:%?20?%}.scrollView .main .center1 .edit .img[data-v-0ddfc844],.scrollView .main .center .edit .img[data-v-0ddfc844]{width:%?29?%;height:%?29?%;vertical-align:middle}.scrollView .main .center1 .edit .p[data-v-0ddfc844],.scrollView .main .center .edit .p[data-v-0ddfc844]{color:#fff;font-size:%?26?%;line-height:%?25?%;margin-left:%?16?%}.scrollView .main .center1[data-v-0ddfc844]{background-image:-webkit-gradient(linear,left top,left bottom,from(rgba(35,35,35,0)),to(#232323));background-image:-o-linear-gradient(rgba(35,35,35,0),#232323);background-image:linear-gradient(rgba(35,35,35,0),#232323);padding-bottom:%?5?%}.scrollView .main .name[data-v-0ddfc844]{max-width:%?500?%;overflow:hidden;-o-text-overflow:ellipsis;text-overflow:ellipsis;white-space:nowrap;background:#232323;padding-top:%?89?%;padding-left:%?60?%;padding-bottom:%?35?%;font-size:%?30?%;color:#fff}.scrollView .main .commodity[data-v-0ddfc844]{height:%?100?%;background:#252525;border-top:%?1?% solid #747474;padding:%?0?% %?37?%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between}.scrollView .main .commodity .p[data-v-0ddfc844]{color:#fff;font-size:%?26?%;line-height:%?26?%}.scrollView .main .commodity .img[data-v-0ddfc844]{width:%?30?%;height:%?30?%;padding:%?10?%}.scrollView .main .profit[data-v-0ddfc844]{height:%?100?%;background:#252525;border-top:%?1?% solid #747474;border-bottom:%?1?% solid #747474;padding:%?0?% %?37?%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between}.scrollView .main .profit .p[data-v-0ddfc844]{color:#fff;font-size:%?26?%;line-height:%?26?%}.scrollView .main .profit .img[data-v-0ddfc844]{width:%?30?%;height:%?30?%;padding:%?10?%}.scrollView .main .purchased .p[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:center;-webkit-justify-content:center;-ms-flex-pack:center;justify-content:center;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;width:100%;height:%?50?%;background-color:#343434}.scrollView .main .purchased .p .p1[data-v-0ddfc844]{font-size:%?22?%;color:#fff}.scrollView .main .purchased .list[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:horizontal;-webkit-box-direction:normal;-webkit-flex-direction:row;-ms-flex-direction:row;flex-direction:row;-webkit-flex-wrap:wrap;-ms-flex-wrap:wrap;flex-wrap:wrap;-webkit-box-pack:start;-webkit-justify-content:flex-start;-ms-flex-pack:start;justify-content:flex-start;padding:%?0?% %?20?%}.scrollView .main .purchased .list .pro[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;-webkit-box-pack:center;-webkit-justify-content:center;-ms-flex-pack:center;justify-content:center}.scrollView .main .purchased .list .pro .photo[data-v-0ddfc844]{width:%?220?%;height:%?220?%;background:#343434;border-radius:50%;margin:%?28?% %?8?% %?16?% %?8?%}.scrollView .main .purchased .list .pro .id[data-v-0ddfc844]{display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:center;-webkit-justify-content:center;-ms-flex-pack:center;justify-content:center;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;height:%?22?%;margin-bottom:%?11?%}.scrollView .main .purchased .list .pro .id .p1[data-v-0ddfc844]{max-width:%?220?%;overflow:hidden;-o-text-overflow:ellipsis;text-overflow:ellipsis;white-space:nowrap;color:#fff;font-size:%?22?%;line-height:%?22?%}.scrollView .main .purchased .list .pro .id .status[data-v-0ddfc844]{height:%?14?%;width:%?14?%;background:#17ff2a;border-radius:50%;margin-left:%?9?%}.scrollView .main .purchased .list .pro .p2[data-v-0ddfc844]{max-width:%?220?%;overflow:hidden;-o-text-overflow:ellipsis;text-overflow:ellipsis;white-space:nowrap;color:#747474;font-size:%?22?%;margin-bottom:%?11?%}.scrollView .main .tabbarArea[data-v-0ddfc844]{height:%?90?%;width:100%;position:fixed;bottom:0;left:0;z-index:9999;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-justify-content:space-around;-ms-flex-pack:distribute;justify-content:space-around;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;background:#191919}.scrollView .main .tabbarArea .per[data-v-0ddfc844]{width:%?54?%;height:%?50?%}.scrollView .main .tabbarArea .per .img[data-v-0ddfc844]{width:%?54?%;height:%?50?%}body.?%PAGE?%[data-v-0ddfc844]{background:#232323}',""])},d347:function(t,e,a){"use strict";var i=a("51ae"),n=a.n(i);n.a}}]);