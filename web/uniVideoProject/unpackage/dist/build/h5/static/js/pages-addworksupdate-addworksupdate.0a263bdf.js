(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["pages-addworksupdate-addworksupdate"],{"296c":function(e,t,a){t=e.exports=a("2350")(!1),t.push([e.i,'@charset "UTF-8";\n/**\r\n * 这里是uni-app内置的常用样式变量\r\n *\r\n * uni-app 官方扩展插件及插件市场（https://ext.dcloud.net.cn）上很多三方插件均使用了这些样式变量\r\n * 如果你是插件开发者，建议你使用scss预处理，并在插件代码中直接使用这些变量（无需 import 这个文件），方便用户通过搭积木的方式开发整体风格一致的App\r\n *\r\n */\n/**\r\n * 如果你是App开发者（插件使用者），你可以通过修改这些变量来定制自己的插件主题，实现自定义主题功能\r\n *\r\n * 如果你的项目同样使用了scss预处理，你也可以直接在你的 scss 代码中使用如下变量，同时无需 import 这个文件\r\n */\n/* 颜色变量 */\n/* 行为相关颜色 */\n/* 文字基本颜色 */\n/* 背景颜色 */\n/* 边框颜色 */\n/* 尺寸变量 */\n/* 文字尺寸 */\n/* 图片尺寸 */\n/* Border Radius */\n/* 水平间距 */\n/* 垂直间距 */\n/* 透明度 */\n/* 文章场景相关 */em[data-v-ed704404]{font-style:normal}body[data-v-ed704404],dd[data-v-ed704404],dl[data-v-ed704404],h1[data-v-ed704404],h2[data-v-ed704404],h3[data-v-ed704404],h4[data-v-ed704404],h5[data-v-ed704404],h6[data-v-ed704404],hr[data-v-ed704404],ol[data-v-ed704404],p[data-v-ed704404],pre[data-v-ed704404],tbody[data-v-ed704404],td[data-v-ed704404],tfoot[data-v-ed704404],th[data-v-ed704404],thead[data-v-ed704404],ul[data-v-ed704404],uni-form[data-v-ed704404],uni-input[data-v-ed704404],uni-textarea[data-v-ed704404]{margin:0;padding:0}ol[data-v-ed704404],ul[data-v-ed704404]{list-style:none}a[data-v-ed704404]{text-decoration:none}html[data-v-ed704404]{-ms-text-size-adjust:none;-webkit-text-size-adjust:none;-moz-text-size-adjust:none;text-size-adjust:none}body[data-v-ed704404]{line-height:1.5;font-size:14px}body[data-v-ed704404],select[data-v-ed704404],uni-button[data-v-ed704404],uni-input[data-v-ed704404],uni-textarea[data-v-ed704404]{font-family:helvetica neue,tahoma,hiragino sans gb,stheiti,wenquanyi micro hei,\\5FAE\\8F6F\\96C5\\9ED1,\\5B8B\\4F53,sans-serif}b[data-v-ed704404],strong[data-v-ed704404]{font-weight:700}em[data-v-ed704404],i[data-v-ed704404]{font-style:normal}table[data-v-ed704404]{border-collapse:collapse;border-spacing:0}table td[data-v-ed704404],table th[data-v-ed704404]{border:1px solid #ddd;padding:5px}table th[data-v-ed704404]{font-weight:inherit;border-bottom-width:2px;border-bottom-color:#ccc}img[data-v-ed704404]{border:0 none;width:auto\\9;max-width:100%;vertical-align:top;height:auto}select[data-v-ed704404],uni-button[data-v-ed704404],uni-input[data-v-ed704404],uni-textarea[data-v-ed704404]{font-family:inherit;font-size:100%;margin:0;vertical-align:baseline}html uni-input[type=button][data-v-ed704404],uni-button[data-v-ed704404],uni-input[type=reset][data-v-ed704404],uni-input[type=submit][data-v-ed704404]{-webkit-appearance:button;cursor:pointer}uni-button[disabled][data-v-ed704404],uni-input[disabled][data-v-ed704404]{cursor:default}uni-input[type=checkbox][data-v-ed704404],uni-input[type=radio][data-v-ed704404]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}uni-input[type=search][data-v-ed704404]{-webkit-appearance:textfield;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;box-sizing:content-box}uni-input[type=search][data-v-ed704404]::-webkit-search-decoration{-webkit-appearance:none}uni-input[data-v-ed704404]:focus{outline:none}select[multiple][data-v-ed704404],select[size][data-v-ed704404],select[size][multiple][data-v-ed704404]{border:1px solid #aaa;padding:0}article[data-v-ed704404],aside[data-v-ed704404],details[data-v-ed704404],figcaption[data-v-ed704404],figure[data-v-ed704404],footer[data-v-ed704404],header[data-v-ed704404],hgroup[data-v-ed704404],main[data-v-ed704404],nav[data-v-ed704404],section[data-v-ed704404],summary[data-v-ed704404]{display:block}uni-audio[data-v-ed704404],uni-canvas[data-v-ed704404],uni-progress[data-v-ed704404],uni-video[data-v-ed704404]{display:inline-block}body[data-v-ed704404]{background:#fff}uni-input[data-v-ed704404]::-webkit-input-speech-button{display:none}uni-button[data-v-ed704404],uni-input[data-v-ed704404],uni-textarea[data-v-ed704404]{-webkit-tap-highlight-color:transparent}uni-page-body[data-v-ed704404]{width:100%;height:100%;background:#191919}.content[data-v-ed704404]{width:100%;height:100%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex;-webkit-box-orient:vertical;-webkit-box-direction:normal;-webkit-flex-direction:column;-ms-flex-direction:column;flex-direction:column}.content .nav[data-v-ed704404]{height:%?100?%;background:#252525;line-height:%?100?%;text-align:center}.content .nav .back[data-v-ed704404]{position:absolute;top:%?22?%;left:%?20?%;height:%?36?%;width:%?36?%;padding:%?10?%}.content .nav .title[data-v-ed704404]{font-size:%?30?%;color:#fff}.content .mainArea[data-v-ed704404]{-webkit-box-flex:1;-webkit-flex:1;-ms-flex:1;flex:1;background:#191919;overflow-y:scroll}.content .mainArea .chooseImgArea[data-v-ed704404],.content .mainArea .name[data-v-ed704404],.content .mainArea .selectArea[data-v-ed704404],.content .mainArea .timeArea[data-v-ed704404],.content .mainArea .tipArea[data-v-ed704404],.content .mainArea .uploadArea[data-v-ed704404]{width:%?690?%;height:%?54?%;padding-top:%?28?%;margin-left:%?30?%;display:-webkit-box;display:-webkit-flex;display:-ms-flexbox;display:flex}.content .mainArea .chooseImgArea .text[data-v-ed704404],.content .mainArea .name .text[data-v-ed704404],.content .mainArea .selectArea .text[data-v-ed704404],.content .mainArea .timeArea .text[data-v-ed704404],.content .mainArea .tipArea .text[data-v-ed704404],.content .mainArea .uploadArea .text[data-v-ed704404]{font-size:%?30?%;color:#acacac;margin-right:%?28?%}.content .mainArea .name .input[data-v-ed704404]{font-size:%?26?%;width:%?520?%;height:%?54?%;color:#fff;background:#343434;border-radius:%?8?%;text-indent:%?30?%}.content .mainArea .selectArea .input[data-v-ed704404]{font-size:%?26?%;height:%?54?%;color:#fff;background:rgba(52,52,52,.4);border-radius:%?8?%;text-indent:%?30?%}.content .mainArea .selectArea .select[data-v-ed704404]{width:%?248?%;height:%?54?%;background:#343434;border-radius:%?8?%;border:none;text-indent:%?30?%;font-size:%?26?%;color:#fff}.content .mainArea .uploadArea .input[data-v-ed704404]{font-size:%?26?%;height:%?54?%;color:#fff;background:rgba(52,52,52,.4);border-radius:%?8?%;text-indent:%?30?%}.content .mainArea .uploadArea .select[data-v-ed704404]{width:%?248?%;height:%?54?%;background:#343434;border-radius:%?8?%;border:none;text-indent:%?30?%;font-size:%?26?%;color:#fff}.content .mainArea .chooseImgArea[data-v-ed704404]{height:%?592?%}.content .mainArea .chooseImgArea .choose[data-v-ed704404]{width:%?520?%;height:%?592?%}.content .mainArea .chooseImgArea .choose .btnUp[data-v-ed704404]{width:%?516?%;height:%?50?%;border:%?2?% solid #ffd600;border-radius:%?8?%;font-size:%?26?%;color:#ffd600;text-align:center;line-height:%?50?%}.content .mainArea .chooseImgArea .choose .imgBox[data-v-ed704404]{width:%?520?%;height:%?520?%;background:#343434;border-radius:%?8?%;margin-top:%?28?%}.content .mainArea .chooseImgArea .choose .imgBox .img[data-v-ed704404]{width:%?520?%;height:%?520?%}.content .mainArea .chooseImgArea .choose .imgBox .audio[data-v-ed704404]{width:%?520?%;height:%?520?%}.content .mainArea .timeArea .input[data-v-ed704404]{font-size:%?26?%;height:%?54?%;color:#fff;background:rgba(52,52,52,.4);border-radius:%?8?%;text-indent:%?30?%}.content .mainArea .timeArea .select[data-v-ed704404]{width:%?248?%;height:%?54?%;background:#343434;border-radius:%?8?%;border:none;text-indent:%?30?%;font-size:%?26?%;color:#fff}.content .mainArea .tipArea[data-v-ed704404]{height:%?180?%;-webkit-box-align:start;-webkit-align-items:flex-start;-ms-flex-align:start;align-items:flex-start}.content .mainArea .tipArea .input[data-v-ed704404]{width:%?520?%;height:%?180?%;background:#343434;border-radius:%?8?%;border:none;text-indent:%?30?%;font-size:%?26?%;color:#fff}.content .mainArea .btn[data-v-ed704404]{width:%?520?%;height:%?72?%;background:#ffd600;border-radius:%?8?%;margin-top:%?60?%;margin-bottom:%?140?%;margin-left:%?115?%;font-size:%?30?%;color:#000;text-align:center;font-weight:600;line-height:%?72?%}body.?%PAGE?%[data-v-ed704404]{background:#191919}',""])},"3c77":function(e,t,a){"use strict";a.r(t);var n=a("82f9"),i=a("80a0");for(var o in i)"default"!==o&&function(e){a.d(t,e,function(){return i[e]})}(o);a("3c8d");var d=a("2877"),s=Object(d["a"])(i["default"],n["a"],n["b"],!1,null,"ed704404",null);t["default"]=s.exports},"3c8d":function(e,t,a){"use strict";var n=a("8710"),i=a.n(n);i.a},"80a0":function(e,t,a){"use strict";a.r(t);var n=a("d534"),i=a.n(n);for(var o in n)"default"!==o&&function(e){a.d(t,e,function(){return n[e]})}(o);t["default"]=i.a},"82f9":function(e,t,a){"use strict";var n=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("v-uni-view",{staticClass:"content"},[a("v-uni-view",{staticClass:"nav"},[a("v-uni-image",{staticClass:"back",attrs:{src:"../../static/imgs/more1w.png",mode:""},on:{click:function(t){t=e.$handleEvent(t),e.goBackPage(t)}}}),a("v-uni-text",{staticClass:"title"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language93))])],1),a("v-uni-view",{staticClass:"mainArea"},[a("v-uni-view",{staticClass:"name"},[a("v-uni-text",{staticClass:"text"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language94))]),a("v-uni-input",{staticClass:"input",attrs:{type:"text",placeholder:e.place1[e.tabbarLoginLanguage]},model:{value:e.content,callback:function(t){e.content=t},expression:"content"}})],1),a("v-uni-view",{staticClass:"selectArea"},[a("v-uni-text",{staticClass:"text"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language95))]),a("v-uni-input",{staticClass:"input",attrs:{type:"text",placeholder:"",disabled:"disabled"},model:{value:e.price,callback:function(t){e.price=t},expression:"price"}})],1),a("v-uni-view",{staticClass:"uploadArea"},[a("v-uni-text",{staticClass:"text"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language96))]),0==e.type?a("v-uni-input",{staticClass:"input",attrs:{value:e.fy_tp[this.tabbarLoginLanguage],type:"text",placeholder:"",disabled:"disabled"}}):e._e(),1==e.type?a("v-uni-input",{staticClass:"input",attrs:{value:e.fy_sp[this.tabbarLoginLanguage],type:"text",placeholder:"",disabled:"disabled"}}):e._e()],1),a("v-uni-view",{staticClass:"chooseImgArea"},[0==e.type?a("v-uni-text",{staticClass:"text"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language97))]):e._e(),1==e.type?a("v-uni-text",{staticClass:"text"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language203))]):e._e(),a("v-uni-view",{staticClass:"choose"},[a("v-uni-view",{staticClass:"imgBox"},[0==e.type?a("v-uni-image",{staticClass:"img",attrs:{src:e.localImgSrc,mode:""}}):e._e(),1==e.type?a("v-uni-video",{staticClass:"audio",attrs:{src:e.localVideoSrc,loop:!0,objectFit:"cover",muted:"muted","show-center-play-btn":!1,controls:!1,autoplay:!0}}):e._e()],1)],1)],1),a("v-uni-view",{staticClass:"timeArea"},[a("v-uni-text",{staticClass:"text"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language98))]),a("v-uni-input",{staticClass:"input",attrs:{type:"text",placeholder:"",disabled:"disabled"},model:{value:e.Prescription,callback:function(t){e.Prescription=t},expression:"Prescription"}})],1),a("v-uni-view",{staticClass:"tipArea"},[a("v-uni-text",{staticClass:"text"},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language57))]),a("v-uni-textarea",{staticClass:"input",attrs:{value:"",placeholder:e.place2[e.tabbarLoginLanguage]},on:{input:function(t){t=e.$handleEvent(t),e.getTextAreaMsg(t)}},model:{value:e.Evaluate,callback:function(t){e.Evaluate=t},expression:"Evaluate"}})],1),a("v-uni-view",{staticClass:"btn",on:{click:function(t){t=e.$handleEvent(t),e.goSubmitMsg(t)}}},[e._v(e._s(this.Language.language[this.tabbarLoginLanguage].language99))])],1)],1)},i=[];a.d(t,"a",function(){return n}),a.d(t,"b",function(){return i})},8710:function(e,t,a){var n=a("296c");"string"===typeof n&&(n=[[e.i,n,""]]),n.locals&&(e.exports=n.locals);var i=a("4f06").default;i("d3e6a062",n,!0,{sourceMap:!1,shadowMode:!1})},d534:function(e,t,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),t.default=void 0;var n=a("a84a"),i={data:function(){return{tabbarLoginLanguage:null,loginMsgData:null,content:"",price:null,type:0,Resources:null,Prescription:0,Evaluate:"",upLoadFileData:null,list1:[],list2:[],localImgSrc:"",localVideoSrc:"",UserIdx:0,ResourceoId:0,PerProductMsg:null,ResourcesEdit:null,place1:["可以复制粘贴","可以復制粘貼","you can copy and paste","สามารถคัดลอกได้"],place2:["#色诱挑逗#欧美淫娃","#色誘挑逗#歐美淫娃","#Seductive teasing#Sexting","#แกล้งอ่อย#สาวยุโรปสุดเซ็กซี่"],fy_tp:["图片","圖片","image","รูปภาพ"],fy_sp:["视频","視頻","Video","วิดีโอ"]}},onLoad:function(e){var t=JSON.parse((0,n.decrypt)(decodeURIComponent(e.action)));this.UserIdx=Number(t.UserIdx),this.ResourceoId=Number(t.ResourceoId),this.ResourcesEdit=Number(t.ResourcesEdit),this.getLoginlanger(),this.getLoginMsg(),this.getPerProductMsg()},methods:{getLoginlanger:function(){var e=this;uni.getStorage({key:"storage_login_language",success:function(t){e.tabbarLoginLanguage=JSON.parse(t.data),console.log(e.tabbarLoginLanguage),0==e.tabbarLoginLanguage?e.langer=0:1==e.tabbarLoginLanguage?e.langer=1:2==e.tabbarLoginLanguage?e.langer=2:3==e.tabbarLoginLanguage&&(e.langer=3)}})},getLoginMsg:function(){var e=this;uni.getStorage({key:"storage_login_str",success:function(t){e.loginMsgData=JSON.parse(t.data)}})},getPerProductMsg:function(){var e=(0,n.base64ToArrayBuffer)((0,n.encrypt)(JSON.stringify({UserIdx:this.UserIdx,ResourceoId:this.ResourceoId}))),t=JSON.parse((0,n.decrypt)((0,n.sendData)("POST",this.GLOBAL.urlPoint+"/userinfo/GetResourceSimple",e)));if(100==t.code){if(0==this.tabbarLoginLanguage)var a="金币",i="小时";else if(1==this.tabbarLoginLanguage)a="金幣",i="小時";else if(2==this.tabbarLoginLanguage)a="coins",i="Hour";else if(3==this.tabbarLoginLanguage)a="เหรียญทอง ",i="ชม.";this.PerProductMsg=t.data,this.content=t.data.Content,this.Evaluate=t.data.Label,this.price=t.data.UnLockMoney+a,this.Prescription=t.data.Prescription+i,this.type=t.data.Type,0==this.type?this.localImgSrc=t.data.ResourceUrl:1==this.type&&(this.localVideoSrc=t.data.ResourceUrl)}},goBackPage:function(){(0,n.navigateTo)("/pages/commoditymanagement/commoditymanagement",null)},goSubmitMsg:function(){uni.request({url:this.GLOBAL.urlPoint+"/userinfo/ResourcesEdit",method:"POST",data:{userIdx:this.UserIdx,ResourcesId:this.ResourceoId,token:this.loginMsgData.webtoken,content:this.content,price:JSON.stringify(this.PerProductMsg.UnLockMoney),Prescription:this.PerProductMsg.Prescription,Evaluate:this.Evaluate,OperateType:this.ResourcesEdit},success:function(e){var t=JSON.parse((0,n.decrypt)(e.data));100==t.code?uni.showToast({title:e.msg,duration:1500,icon:"none",success:function(){(0,n.navigateTo)("/pages/commoditymanagement/commoditymanagement",null)}}):uni.showToast({title:t.msg,duration:1500,icon:"none",success:function(){}})}})},uploadFile:function(){if(0==this.type){var e=this;uni.chooseImage({count:1,sizeType:["compressed "],sourceType:["album","album"],success:function(t){e.localImgSrc=t.tempFilePaths[0],uni.request({url:t.tempFilePaths[0],method:"GET",responseType:"arraybuffer",success:function(t){var a=wx.arrayBufferToBase64(t.data);function n(e,t){var a=e.split(","),n=a[0].match(/:(.*?);/)[1],i=atob(a[1]),o=i.length,d=new Uint8Array(o);while(o--)d[o]=i.charCodeAt(o);return new File([d],t,{type:n})}a="data:image/jpeg;base64,"+a;var i=n(a,"imgHeader");e.Resources=i}})}})}else if(1==this.type){e=this;uni.chooseVideo({compressed:!0,maxDuration:15,count:1,sourceType:["album"],success:function(t){console.log(t.tempFilePath),e.localVideoSrc=t.tempFilePath,uni.request({url:t.tempFilePath,method:"GET",responseType:"arraybuffer",success:function(t){console.log(t.data);var a=wx.arrayBufferToBase64(t.data);function n(e,t){var a=e.split(","),n=a[0].match(/:(.*?);/)[1],i=atob(a[1]),o=i.length,d=new Uint8Array(o);while(o--)d[o]=i.charCodeAt(o);return new File([d],t,{type:n})}a="data:video/mp4;base64,"+a;var i=n(a,"aaa.mp4");e.Resources=i}})}})}},getTextAreaMsg:function(e){"#"==e.target.value[0]?this.Evaluate=e.target.value:this.Evaluate=""}}};t.default=i}}]);