const CryptoJS = require('../crypto-js/crypto-js.js');  //引用AES源码js
import {webMsg} from '../socket/modules/handle.js'
/**
 * get（）--api
 * @param obj 参数 url 地址
 * @returns {*}
 */
export function Get(obj,url){
	return new Promise((resolve, reject) => {
		uni.request({
			url: '/api'+url, //仅为示例，并非真实接口地址。 https://live.mycat1314.com/
			data: obj,
			method:'GET',
			success: (res) => {
				resolve(res);
			},
			fail(err) {
				reject(err);
			}
		});
	})
}
/**
 * post（）--api
 * @param obj 
 * @returns {*}
 */
export function Post (obj,url) {
	return new Promise((resolve, reject) => {
		uni.request({
			// url: '/api'+url, //仅为示例，并非真实接口地址。 https://live.mycat1314.com/
			url: 'http://60.191.222.11:8022/'+url, //仅为示例，并非真实接口地址。 https://live.mycat1314.com/
			data: obj,
			method:'POST',
			success: (res) => {
				resolve(res);
			},
			fail(err) {
				reject(err)
			}
		});
	})
}
/**
 * get（）--api
 * @param obj 参数 url 地址
 * @returns {*}
 */
export function Get1(obj,aid,url){
	return new Promise((resolve, reject) => {
		uni.request({
			url: '/api'+url, //仅为示例，并非真实接口地址。
			data: obj,
			method:'GET',
			header: { aid: aid },
			success: (res) => {
				resolve(res);
			},
			fail(err) {
				reject(err);
			}
		});
	})
}
/**
 * post1（）--api  接口使用
 * @param obj 
 * @returns {*}
 */
export function Post1 (obj,aid,url) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: '/api'+url, //仅为示例，并非真实接口地址。
			data: obj,
			method:'POST',
			header: { aid: aid },
			success: (res) => {
				resolve(res);
			},
			fail(err) {
				reject(err)
			}
		});
	})
}
/**
 * delete1（）--api  接口使用
 * @param obj 
 * @returns {*}
 */
export function Delete1 (obj,aid,url) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: '/api'+url, //仅为示例，并非真实接口地址。
			data: obj,
			method:'DELETE',
			header: { aid: aid },
			success: (res) => {
				resolve(res);
			},
			fail(err) {
				reject(err)
			}
		});
	})
}
/**
 * 加密（）--api
 * @param word
 * @returns {*}
 */
export function encrypt(word){
	var key = CryptoJS.enc.Utf8.parse("hangzhoutiangekeji9158miaobolive");
	var srcs = CryptoJS.enc.Utf8.parse(word);
	var encrypted = CryptoJS.AES.encrypt(srcs, key, {mode:CryptoJS.mode.ECB,padding: CryptoJS.pad.Pkcs7});
	return encrypted.toString();
}
/**
 * 解密 --api
 * @param word
 * @returns {*}
 */
export function decrypt(word){
	var key = CryptoJS.enc.Utf8.parse("hangzhoutiangekeji9158miaobolive");
	var decrypt = CryptoJS.AES.decrypt(word, key, {mode:CryptoJS.mode.ECB,padding: CryptoJS.pad.Pkcs7});
	return CryptoJS.enc.Utf8.stringify(decrypt).toString();
}
/**
 * 跳转 --api
 * @param word
 * @returns {*}
 */
export function navigateTo(url,param){//string  string-obj or null
	// console.log(url)
	// console.log(param)
	if(param===null){
		uni.navigateTo({
			url: url
		})
	}else if(param!=null){
		uni.navigateTo({
			url: url+ "?action="+param
		})
	}
}
/**
 * 判断系统类型 --api
 * @param null
 * @returns android or ios  pc
 */
export function systemall(){
	var ios = 'ios';
	var android = 'android';
	var pc = 'pc';
	var u = navigator.userAgent, app = navigator.appVersion;
	var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Linux') > -1; //g
	var isIOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
	if (isAndroid) {
	   //这个是安卓操作系统
	   return android;
	}
	if (isIOS) {
	　　//这个是ios操作系统
		return ios;
	}
	if (isIOS==false&&isAndroid==false) {
	　　//这个是pc操作系统
		return pc;
	}
}
/**
 * 判断系统类型 --api
 * @param null
 * @returns android or ios
 */
export function system(){
	var ios = 'ios';
	var android = 'android';
	var u = navigator.userAgent, app = navigator.appVersion;
	var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Linux') > -1; //g
	var isIOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
	if (isAndroid) {
	   //这个是安卓操作系统
	   return android;
	}
	if (isIOS) {
	　　//这个是ios操作系统
		return ios;
	}
}
/**
 * 判断系统类型 --api  0--ios 1--android
 * @param null
 * @returns android or ios
 */
export function productType(){
	var ios = 'ios';
	var android = 'android';
	var u = navigator.userAgent, app = navigator.appVersion;
	var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Linux') > -1; //g
	var isIOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
	if (isAndroid) {
	   //这个是安卓操作系统
	   return 1;
	}
	if (isIOS) {
	　　//这个是ios操作系统
		return 0;
	}
}
/**
 * 判断设备号 --api
 * @param word
 * @returns {*}
 */
export function systemId(){
	var canvas = document.createElement('canvas'),
	gl = canvas.getContext('experimental-webgl'),
	debugInfo = gl.getExtension('WEBGL_debug_renderer_info');
	//console.log(gl.getParameter(debugInfo.UNMASKED_RENDERER_WEBGL));
	return gl.getParameter(debugInfo.UNMASKED_RENDERER_WEBGL);
}
/**
 * 转二进制流--api
 * @param base64
 * @returns [*]
 */
export function base64ToArrayBuffer(base64) {
	var binaryString = window.atob(base64);
	var binaryLen = binaryString.length;
	var bytes = new Uint8Array(binaryLen);
	for (var i = 0; i < binaryLen; i++) {
		var ascii = binaryString.charCodeAt(i);
		bytes[i] = ascii;
	}
	return bytes;
}
 /**
  * 二进制流传输 --api
  * @param base64
  * @returns [*]
  */
 export function sendData(type,url,array){
	var xhr = new XMLHttpRequest;
	xhr.open(type,url, false);
	xhr.send(array);
	return xhr.responseText;//or response
 }
 /**
 * socket通信转流 字符串处理 并转流 socket send 信息通用
 * @param string
 * @returns [*]
 */
 export function fillstr2abc(str, buf, offset) {//----通用 防止字符串过长
 	var uint8array = new TextEncoder().encode(str);
 	var strLen = uint8array.length;
 	var bufView = new Uint8Array(buf, offset, strLen);
 	for (var i = 0; i < strLen; i++) {
 		bufView[i] = uint8array[i];
 	}
	return strLen;
}
export function sendDSocket(content,num) {//params1 json string params2 num 协议号------
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len*3);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + fillstr2abc(content, bufferRes,12); //len
	// console.log(Head[0])
 	Head[1] = num;   //cmd
 	return bufferRes.slice(0,Head[0]);
}

 
  /**
  * socket通信转流
  * @param string
  * @returns [*]
  */
 export function fillstr2ab(str, buf, offset) {
 	var uint8array = new TextEncoder().encode(str);
 	var strLen = uint8array.length;
 	var bufView = new Uint8Array(buf, offset, strLen);
 	for (var i = 0; i < strLen; i++) {
 		bufView[i] = uint8array[i];
 	}
}
 export function fillstr2ab15(str, buf, offset) {//-------------
 	var uint8array = new TextEncoder().encode(str);
 	var strLen = uint8array.length;
 	var bufView = new Uint8Array(buf, offset, strLen);
 	for (var i = 0; i < strLen; i++) {
 		bufView[i] = uint8array[i];
 	}
	return strLen;
}
 export function fillstr2ab201(str, buf, offset) {//-------------
 	var uint8array = new TextEncoder().encode(str);
 	var strLen = uint8array.length;
 	var bufView = new Uint8Array(buf, offset, strLen);
 	for (var i = 0; i < strLen; i++) {
 		bufView[i] = uint8array[i];
 	}
	return strLen;
}
export function sendD(content) {//10001 用户登录协议
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 10001;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD06(content) {//11006 更新币值（服务端->客户端））
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 11006;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD110(content) {//11000 心跳发包（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 11000;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD07(content) {//11007 关注或取消关注（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 11007;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD09(content) {//11009 拉黑或取消拉黑（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 11009;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD11(content) {//11011 设置喜好（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 11011;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD13(content) {//11013 设置语言（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 11013;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD15(content) {//11015 设置个性签名------
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len*3);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + fillstr2ab15(content, bufferRes,12); //len
	// console.log(Head[0])
 	Head[1] = 11015;   //cmd
 	return bufferRes.slice(0,Head[0]);
}
export function sendD17(content) {//11017 获取主播私信价格（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 11017;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD201(content) {//20001 用户聊天消息发送（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len*3);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + fillstr2ab201(content, bufferRes,12); //len
 	Head[1] = 20001;   //cmd
 	return bufferRes.slice(0,Head[0]);
}
export function sendD207(content) {//20007 客户端拉离线消息（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 20007;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD209(content) {//20009 客户端读取消息确认，对应消息20008（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 20009;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD217(content) {//20017 客户端查询消息（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 20017;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
export function sendD204(content) {//20004 B读取消息后，告诉服务端已读（客户端->服务端）
 	var len = content.length
 	//=========================
 	var bufferRes = new ArrayBuffer(12 + len);
 	var Head = new Int32Array(bufferRes, 0, 3);
 	Head[0] = 12 + len; //len
 	Head[1] = 20004;   //cmd
 	fillstr2ab(content, bufferRes,12);
 	return bufferRes;
}
  /**
  * 流转string
  * @param Blob
  * @returns [string]
  */
 export function work(data) {// someErr
 	var fileReader = new FileReader();
 	fileReader.onload = function (progressEvent) {
 		var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
 		var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
 		var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
 		var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
 		// console.log(HeadRecv[1],JSON.parse(str))
		//to do
 		console.log(HeadRecv[1])
 		console.log(JSON.parse(str))
 	};
 	fileReader.readAsArrayBuffer(data);
 }
 
 
 
 /**
  * get（）--api
  * @param obj 参数 url 地址
  * @returns {*}
  */
 export function Do(aaaa){
	 var resultData = {
	 	code:null,
	 	msg:null
	 };
	 var fileReader = new FileReader();
	 fileReader.onload = function (progressEvent) {
	 	var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
	 	var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
	 	var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
	 	var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
	 	// console.log(HeadRecv[1],JSON.parse(str))
	 	//to do
	 	// resultData.code = HeadRecv[1];
	 	// resultData.msg = JSON.parse(str);
	 	// console.log(HeadRecv[1])
	 	// console.log(JSON.parse(str))
		// console.log(resultData)
		webMsg(HeadRecv[1],JSON.parse(str))
	 };
	 
	 fileReader.readAsArrayBuffer(aaaa);
 }
  /**
  * 流转string  最新
  * @param Blob
  * @returns [string]
  */


  /**
  * 国籍通用邮箱正则
  * @param string
  * @returns [true?false]
  */
 export function regMail(str){
	 var reg = new RegExp("^[a-z0-9A-Z]+[- | a-z0-9A-Z . _]+@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\\.)+[a-z]{2,}$"); 
	 var test =reg.test(str);
	 // console.log(test);
	 if(test){
		 return true;
	 }else{
		 return false;
	 }
 }
   /**
 * 将图片转换为Base64   暂未使用    使用的是其他方法详见上传函数
 * @param string url 图片链接或者是blob对象 callback 回调函数
 * @returns base64
 */
 export function getImgToBase64(url,callback){
  var canvas = document.createElement('canvas'),
	ctx = canvas.getContext('2d'),
	img = new Image;//通过构造函数来创建的 img 实例，在赋予 src 值后就会立刻下载图片，相比 createElement() 创建 <img> 省去了 append()，也就避免了文档冗余和污染
  img.crossOrigin = 'Anonymous';
  img.onload = function(){//要先确保图片完整获取到，这是个异步事件
	canvas.height = img.height;//确保canvas的尺寸和图片一样
	canvas.width = img.width;
	ctx.drawImage(img,0,0);//将图片绘制到canvas中
	var dataURL = canvas.toDataURL('image/png');//转换图片为dataURL,传第二个参数可压缩图片,前提是图片格式jpeg或者webp格式的
	callback(dataURL);//调用回调函数
	canvas = null;
  };
  img.src = url;
}
