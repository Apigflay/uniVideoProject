(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["pages-changeintroduction-changeintroduction"],{1847:function(a,t,e){"use strict";var n=function(){var a=this,t=a.$createElement,e=a._self._c||t;return e("v-uni-view",{staticClass:"content"},[e("v-uni-view",{staticClass:"nav"},[e("v-uni-image",{staticClass:"img",attrs:{src:"../../static/imgs/more1w.png",mode:""},on:{click:function(t){t=a.$handleEvent(t),a.goBackPage(t)}}}),e("v-uni-text",{staticClass:"title"},[a._v(a._s(this.Language.language[this.tabbarLoginLanguage].language70))]),e("v-uni-text",{staticClass:"save",on:{click:function(t){t=a.$handleEvent(t),a.keepMsg(t)}}},[a._v(a._s(this.Language.language[this.tabbarLoginLanguage].language67))])],1),e("v-uni-view",{staticClass:"bgArea"},[e("v-uni-image",{staticClass:"bg",attrs:{src:a.background,mode:""}}),e("v-uni-view",{staticClass:"phoArea"},[e("v-uni-image",{staticClass:"photo",attrs:{src:a.headpic,mode:""}}),e("v-uni-image",{staticClass:"camera",attrs:{src:"../../static/imgs/camera1w.png",mode:""},on:{click:function(t){t=a.$handleEvent(t),a.upLoadImg(1)}}})],1),e("v-uni-image",{staticClass:"choosebg",attrs:{src:"../../static/imgs/camera1w.png",mode:""},on:{click:function(t){t=a.$handleEvent(t),a.upLoadImg(2)}}})],1),e("v-uni-view",{staticClass:"introductionArea"},[e("v-uni-view",{staticClass:"name"},[e("v-uni-text",{staticClass:"text"},[a._v(a._s(this.Language.language[this.tabbarLoginLanguage].language68))]),e("v-uni-text",{staticClass:"text"},[a._v(a._s(this.myMsgData.MyName))])],1),e("v-uni-view",{staticClass:"p"},[e("v-uni-text",{staticClass:"tltle"},[a._v(a._s(this.Language.language[this.tabbarLoginLanguage].language69))]),e("v-uni-text",{staticClass:"num"},[a._v(a._s(a.textAreaLegth)+"/150")])],1),e("v-uni-view",{staticClass:"textArea"},[e("v-uni-textarea",{staticClass:"text",attrs:{"placeholder-style":"color:rgba(116,116,116,1)",placeholder:"",maxlength:150},on:{input:function(t){t=a.$handleEvent(t),a.getTextLength(t)}},model:{value:a.textArea,callback:function(t){a.textArea=t},expression:"textArea"}})],1)],1)],1)},i=[];e.d(t,"a",function(){return n}),e.d(t,"b",function(){return i})},"243d":function(a,t,e){"use strict";var n=e("4284"),i=e.n(n);i.a},4152:function(a,t,e){"use strict";e.r(t);var n=e("fd61"),i=e.n(n);for(var o in n)"default"!==o&&function(a){e.d(t,a,function(){return n[a]})}(o);t["default"]=i.a},4284:function(a,t,e){var n=e("6324");"string"===typeof n&&(n=[[a.i,n,""]]),n.locals&&(a.exports=n.locals);var i=e("4f06").default;i("0cc2a7d2",n,!0,{sourceMap:!1,shadowMode:!1})},6324:function(a,t,e){t=a.exports=e("2350")(!1),t.push([a.i,'@charset "UTF-8";\n/**\r\n * 这里是uni-app内置的常用样式变量\r\n *\r\n * uni-app 官方扩展插件及插件市场（https://ext.dcloud.net.cn）上很多三方插件均使用了这些样式变量\r\n * 如果你是插件开发者，建议你使用scss预处理，并在插件代码中直接使用这些变量（无需 import 这个文件），方便用户通过搭积木的方式开发整体风格一致的App\r\n *\r\n */\n/**\r\n * 如果你是App开发者（插件使用者），你可以通过修改这些变量来定制自己的插件主题，实现自定义主题功能\r\n *\r\n * 如果你的项目同样使用了scss预处理，你也可以直接在你的 scss 代码中使用如下变量，同时无需 import 这个文件\r\n */\n/* 颜色变量 */\n/* 行为相关颜色 */\n/* 文字基本颜色 */\n/* 背景颜色 */\n/* 边框颜色 */\n/* 尺寸变量 */\n/* 文字尺寸 */\n/* 图片尺寸 */\n/* Border Radius */\n/* 水平间距 */\n/* 垂直间距 */\n/* 透明度 */\n/* 文章场景相关 */em[data-v-fb2e65a6]{font-style:normal}body[data-v-fb2e65a6],dd[data-v-fb2e65a6],dl[data-v-fb2e65a6],h1[data-v-fb2e65a6],h2[data-v-fb2e65a6],h3[data-v-fb2e65a6],h4[data-v-fb2e65a6],h5[data-v-fb2e65a6],h6[data-v-fb2e65a6],hr[data-v-fb2e65a6],ol[data-v-fb2e65a6],p[data-v-fb2e65a6],pre[data-v-fb2e65a6],tbody[data-v-fb2e65a6],td[data-v-fb2e65a6],tfoot[data-v-fb2e65a6],th[data-v-fb2e65a6],thead[data-v-fb2e65a6],ul[data-v-fb2e65a6],uni-form[data-v-fb2e65a6],uni-input[data-v-fb2e65a6],uni-textarea[data-v-fb2e65a6]{margin:0;padding:0}ol[data-v-fb2e65a6],ul[data-v-fb2e65a6]{list-style:none}a[data-v-fb2e65a6]{text-decoration:none}html[data-v-fb2e65a6]{-ms-text-size-adjust:none;-webkit-text-size-adjust:none;-moz-text-size-adjust:none;text-size-adjust:none}body[data-v-fb2e65a6]{line-height:1.5;font-size:14px}body[data-v-fb2e65a6],select[data-v-fb2e65a6],uni-button[data-v-fb2e65a6],uni-input[data-v-fb2e65a6],uni-textarea[data-v-fb2e65a6]{font-family:helvetica neue,tahoma,hiragino sans gb,stheiti,wenquanyi micro hei,\\5FAE\\8F6F\\96C5\\9ED1,\\5B8B\\4F53,sans-serif}b[data-v-fb2e65a6],strong[data-v-fb2e65a6]{font-weight:700}em[data-v-fb2e65a6],i[data-v-fb2e65a6]{font-style:normal}table[data-v-fb2e65a6]{border-collapse:collapse;border-spacing:0}table td[data-v-fb2e65a6],table th[data-v-fb2e65a6]{border:1px solid #ddd;padding:5px}table th[data-v-fb2e65a6]{font-weight:inherit;border-bottom-width:2px;border-bottom-color:#ccc}img[data-v-fb2e65a6]{border:0 none;width:auto\\9;max-width:100%;vertical-align:top;height:auto}select[data-v-fb2e65a6],uni-button[data-v-fb2e65a6],uni-input[data-v-fb2e65a6],uni-textarea[data-v-fb2e65a6]{font-family:inherit;font-size:100%;margin:0;vertical-align:baseline}html uni-input[type=button][data-v-fb2e65a6],uni-button[data-v-fb2e65a6],uni-input[type=reset][data-v-fb2e65a6],uni-input[type=submit][data-v-fb2e65a6]{-webkit-appearance:button;cursor:pointer}uni-button[disabled][data-v-fb2e65a6],uni-input[disabled][data-v-fb2e65a6]{cursor:default}uni-input[type=checkbox][data-v-fb2e65a6],uni-input[type=radio][data-v-fb2e65a6]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}uni-input[type=search][data-v-fb2e65a6]{-webkit-appearance:textfield;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;box-sizing:content-box}uni-input[type=search][data-v-fb2e65a6]::-webkit-search-decoration{-webkit-appearance:none}uni-input[data-v-fb2e65a6]:focus{outline:none}select[multiple][data-v-fb2e65a6],select[size][data-v-fb2e65a6],select[size][multiple][data-v-fb2e65a6]{border:1px solid #aaa;padding:0}article[data-v-fb2e65a6],aside[data-v-fb2e65a6],details[data-v-fb2e65a6],figcaption[data-v-fb2e65a6],figure[data-v-fb2e65a6],footer[data-v-fb2e65a6],header[data-v-fb2e65a6],hgroup[data-v-fb2e65a6],main[data-v-fb2e65a6],nav[data-v-fb2e65a6],section[data-v-fb2e65a6],summary[data-v-fb2e65a6]{display:block}uni-audio[data-v-fb2e65a6],uni-canvas[data-v-fb2e65a6],uni-progress[data-v-fb2e65a6],uni-video[data-v-fb2e65a6]{display:inline-block}body[data-v-fb2e65a6]{background:#fff}uni-input[data-v-fb2e65a6]::-webkit-input-speech-button{display:none}uni-button[data-v-fb2e65a6],uni-input[data-v-fb2e65a6],uni-textarea[data-v-fb2e65a6]{-webkit-tap-highlight-color:transparent}uni-page-body[data-v-fb2e65a6]{width:100%;height:100%}.content[data-v-fb2e65a6]{width:100%;height:100%;background:#191919}.content .nav[data-v-fb2e65a6]{height:%?100?%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center;background:#252525}.content .nav .img[data-v-fb2e65a6]{height:%?30?%;width:%?30?%;padding:%?10?%;margin-left:%?20?%}.content .nav .title[data-v-fb2e65a6]{font-size:%?30?%;font-weight:400;color:#fff}.content .nav .save[data-v-fb2e65a6]{font-size:%?30?%;font-family:PingFang TC;font-weight:400;color:#ffd600;padding:%?10?%;margin-right:%?30?%}.content .bgArea[data-v-fb2e65a6]{height:%?370?%;position:relative;border-bottom:%?2?% solid #747474}.content .bgArea .bg[data-v-fb2e65a6]{position:absolute;top:0;left:0;width:100%;height:%?370?%}.content .bgArea .phoArea[data-v-fb2e65a6]{position:absolute;top:%?100?%;left:%?285?%;width:%?180?%;height:%?180?%}.content .bgArea .phoArea .photo[data-v-fb2e65a6]{width:%?176?%;height:%?176?%;border:%?2?% solid #fff;border-radius:50%}.content .bgArea .phoArea .camera[data-v-fb2e65a6]{position:absolute;top:%?120?%;left:%?120?%;width:%?60?%;height:%?60?%;border-radius:50%}.content .bgArea .choosebg[data-v-fb2e65a6]{position:absolute;bottom:%?28?%;right:%?28?%;width:%?60?%;height:%?60?%;border-radius:50%}.content .introductionArea[data-v-fb2e65a6]{width:%?680?%;padding:0 %?35?%}.content .introductionArea .name[data-v-fb2e65a6]{height:%?85?%;border-bottom:%?2?% solid #747474;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center}.content .introductionArea .name .text[data-v-fb2e65a6]{font-size:%?30?%;color:#fff;margin-right:%?28?%}.content .introductionArea .name .input[data-v-fb2e65a6]{font-size:%?30?%;color:#747474}.content .introductionArea .p[data-v-fb2e65a6]{height:%?30?%;margin:%?28?% 0;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-webkit-justify-content:space-between;-ms-flex-pack:justify;justify-content:space-between;-webkit-box-align:center;-webkit-align-items:center;-ms-flex-align:center;align-items:center}.content .introductionArea .p .tltle[data-v-fb2e65a6]{font-size:%?30?%;color:#fff}.content .introductionArea .p .num[data-v-fb2e65a6]{font-size:%?22?%;color:#747474}.content .introductionArea .textArea[data-v-fb2e65a6]{background:#2e2e2e;border-radius:8px;padding:%?20?% %?15?%}.content .introductionArea .textArea .text[data-v-fb2e65a6]{width:100%;min-height:%?211?%;border-radius:8px;font-size:%?28?%;color:#fff;padding-right:%?66?%}',""])},8373:function(a,t,e){"use strict";e.r(t);var n=e("1847"),i=e("4152");for(var o in i)"default"!==o&&function(a){e.d(t,a,function(){return i[a]})}(o);e("243d");var s=e("2877"),r=Object(s["a"])(i["default"],n["a"],n["b"],!1,null,"fb2e65a6",null);t["default"]=r.exports},fd61:function(a,t,e){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),t.default=void 0;var n=e("28b9"),i={data:function(){return{tabbarLoginLanguage:null,loginMsgData:null,myMsgData:null,textAreaLegth:0,textArea:"",headpic:"",background:""}},onLoad:function(){this.getLoginMsg(),this.getMyInfoMsg(),this.getLoginlanger()},methods:{getLoginlanger:function(){var a=this;uni.getStorage({key:"storage_login_language",success:function(t){a.tabbarLoginLanguage=JSON.parse(t.data),0==a.tabbarLoginLanguage?a.langer=0:1==a.tabbarLoginLanguage?a.langer=1:2==a.tabbarLoginLanguage?a.langer=2:3==a.tabbarLoginLanguage&&(a.langer=3)}})},getLoginMsg:function(){var a=this;uni.getStorage({key:"storage_login_str",success:function(t){a.loginMsgData=JSON.parse(t.data),a.headpic=a.loginMsgData.headpic,a.background=a.loginMsgData.background,a.textArea=a.loginMsgData.signature}})},getTextLength:function(a){this.textAreaLegth=a.target.value.length,a.target.value.length>150&&(this.textAreaLegth=150)},goBackPage:function(){1==this.loginMsgData.isAnchor?(0,n.navigateTo)("/pages/anchorme/anchorme",null):0==this.loginMsgData.isAnchor&&(0,n.navigateTo)("/pages/my/my",null)},testSocket:function(){var a=this,t=JSON.stringify({useridx:10009829,toidx:10009827,status:!0}),e=(0,n.sendD07)(t);this.$Socket.nsend(e),this.$Socket.eventPatch.onMsg(function(t,e){setTimeout(function(){var e=new FileReader;e.onload=function(t){var e=this.result,i=new Uint32Array(e,0,3),o=new Uint8Array(e,12,i[0]-12-1),s=(new TextDecoder).decode(o),r=JSON.parse(s);if(10002==i[1]);else if(10003==i[1])uni.showToast({title:r.error,duration:1e3,icon:"none"});else if(10010==i[1]){var d=(0,n.sendD)(array1);a.$Socket.nsend(d),a.$Socket.eventPatch.onMsg(function(t,e){setTimeout(function(){console.log(t);var e=new FileReader;e.onload=function(t){var e=this.result,i=new Uint32Array(e,0,3),o=new Uint8Array(e,12,i[0]-12-1),s=(new TextDecoder).decode(o);JSON.parse(s);if(10002==i[1])uni.setStorage({key:"storage_login_str",data:s,success:function(){console.log("success")}});else if(10003==i[1]){if(0==a.tabbarLoginLanguage||1==a.tabbarLoginLanguage)var r="請重新登入";else if(2==a.tabbarLoginLanguage)r="Please login";else if(3==a.tabbarLoginLanguage)r="กรุณาเข้าสู่ระบบอีกครั้ง";uni.showToast({title:r,duration:1e3,icon:"none",success:function(){setTimeout(function(){(0,n.navigateTo)("/pages/startup/startup",null)},1e3)}})}},e.readAsArrayBuffer(t.data)},0)})}},e.readAsArrayBuffer(t.data)},0)})},keepMsg:function(){this.textArea=this.textArea.substring(0,150);var a=JSON.stringify({useridx:this.loginMsgData.useridx,signature:this.textArea}),t=(0,n.sendD15)(a);0==this.$Socket.isconnect&&this.$Socket.onReload(),this.$Socket.nsend(t);var e=this;this.$Socket.eventPatch.onMsg(function(a,t){setTimeout(function(){var t=new FileReader;t.onload=function(a){var t=this.result,n=new Uint32Array(t,0,3),i=new Uint8Array(t,12,n[0]-12-1),o=(new TextDecoder).decode(i),s=JSON.parse(o);if(11016==n[1]){if(0==s.code){e.signature=e.textArea;var r=e.loginMsgData;r.signature=e.textArea;o=JSON.stringify(r);if(uni.setStorage({key:"storage_login_str",data:o,success:function(){}}),0==e.tabbarLoginLanguage||1==e.tabbarLoginLanguage)var d="成功";else if(2==e.tabbarLoginLanguage)d="success";else if(3==e.tabbarLoginLanguage)d="ประสบความสำเร็จ";uni.showToast({title:d,icon:"none",duration:1500})}}else n[1]},t.readAsArrayBuffer(a.data)},0)})},upLoadImg:function(a){var t=this;uni.chooseImage({count:1,sizeType:["compressed "],sourceType:["album","camera"],success:function(e){uni.request({url:e.tempFilePaths[0],method:"GET",responseType:"arraybuffer",success:function(e){var i=wx.arrayBufferToBase64(e.data);function o(a,t){var e=a.split(","),n=e[0].match(/:(.*?);/)[1],i=atob(e[1]),o=i.length,s=new Uint8Array(o);while(o--)s[o]=i.charCodeAt(o);return new File([s],t,{type:n})}i="data:image/jpeg;base64,"+i;var s=o(i,"imgHeader"),r=new FormData;r.append("picture",s),r.append("userIdx",t.loginMsgData.useridx),r.append("token",t.loginMsgData.webtoken),r.append("Type",a);var d=new XMLHttpRequest;d.open("POST",t.GLOBAL.urlPoint+"/userinfo/UploadHeaderImg",!1),d.send(r);var u=JSON.parse((0,n.decrypt)(d.responseText));if(100==u.code){if(0==t.tabbarLoginLanguage||1==t.tabbarLoginLanguage)var g="成功";else if(2==t.tabbarLoginLanguage)g="success";else if(3==t.tabbarLoginLanguage)g="ประสบความสำเร็จ";if(uni.showToast({title:g,duration:1500,icon:"none",success:function(a){}}),1==a){t.headpic=u.data.pic_140;var c=t.loginMsgData;c.headpic=u.data.pic_140;var f=JSON.stringify(c);uni.setStorage({key:"storage_login_str",data:f,success:function(){}})}else if(2==a){t.background=u.data.pic_140;c=t.loginMsgData;c.background=u.data.pic_140;f=JSON.stringify(c);uni.setStorage({key:"storage_login_str",data:f,success:function(){}})}}else uni.showToast({title:u.msg,duration:1500,icon:"none",success:function(a){}})}})}})},getMyInfoMsg:function(){var a=(0,n.base64ToArrayBuffer)((0,n.encrypt)(JSON.stringify({UserIdx:this.loginMsgData.useridx}))),t=JSON.parse((0,n.decrypt)((0,n.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetCatUserInfo",a)));100==t.code&&(this.textAreaLegth=t.data.Signature.length,this.myMsgData=t.data)}}};t.default=i}}]);