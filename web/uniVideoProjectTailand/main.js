import Vue from 'vue'
import App from './App'
import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,regMail,navigateTo,sendD110} from "./lib/js/GlobalFunction.js"
// 全局socket  103.219.36.136  17400   new ip 61.164.160.54
import socket from './lib/tools/chat/socket.js'
const Socket =new socket({
    url: 'wss://server.woopsss.com:17400', //连接地址 必填 server.mycat1314.com  ws://103.219.36.136:17400
})
// window.addEventListener("popstate", function(e) { 
// alert("我监听到了浏览器的返回按钮事件啦");//根据自己的需求实现自己的功能
//  // location.reload()
//  // location.replace(document.referrer);
//  history.go(0)
// 
// }, false);
// setInterval(function(){
// 	// console.log("pilian")
// 	var counter = 5;
// 	var array =JSON.stringify({
// 		"counter": counter
// 	})
// 	var arr = sendD110(array);
// 	// ---------------socket-login---------
// 	// alert(this.$Socket.isconnect)
// 	// console.log(Socket)
// 	// console.log(111)
// 	if(Socket.isconnect==false){
// 		Socket.onReload()
// 	}
// 	Socket.nsend(arr)
// 	// ---------------------------
// 	Socket.eventPatch.onMsg((msg,sk)=>{    //监听是否接受消息
// 		Socket.eventPatch.events.onMsg = [Socket.eventPatch.events.onMsg[0],Socket.eventPatch.events.onMsg[Socket.eventPatch.events.onMsg.length-1]];
// 		// 消息处理---
// 		setTimeout(function() {
// 		var fileReader = new FileReader();
// 		fileReader.onload = function (progressEvent) {
// 			var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
// 			var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
// 			var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
// 			var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
// 			// console.log(HeadRecv[1],JSON.parse(str))
// 			//to do
// 			// console.log(HeadRecv[1])
// 			// console.log(JSON.parse(str))
// 			var msgdata =JSON.parse(str);
// 			if(HeadRecv[1]==10002){
// 				// console.log('登录成功'+111)
// 				// clearInterval()
// 				// var totalTimer = setInterval(function(){
// 				// 	console.log('keep')
// 				// 	// -----断线--首次--
// 				// 	
// 				// 	var array =JSON.stringify({
// 				// 		"counter": 5
// 				// 	})
// 				// 	// console.log(array)
// 				// 	var arr = sendD110(array);
// 				// 	// ---------------socket-login---------
// 				// 	// alert(this.$Socket.isconnect)
// 				// 	console.log(Socket)
// 				// 	// console.log(111)
// 				// 	if(Socket.isconnect==false){
// 				// 		Socket.onReload()
// 				// 	}
// 				// 	Socket.nsend(arr)
// 				// 	// console.log("yijing")
// 				// 	Socket.eventPatch.onMsg((msg,sk)=>{    //监听是否接受消息
// 				// 		setTimeout(function() {
// 				// 		// console.log(msg)
// 				// 		// console.log(sk)
// 				// 		 var fileReader = new FileReader();
// 				// 		     fileReader.onload = function (progressEvent) {
// 				// 		     	var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
// 				// 		     	var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
// 				// 		     	var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
// 				// 		     	var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
// 				// 		     	// console.log(HeadRecv[1],JSON.parse(str))
// 				// 		     	//to do
// 				// 				console.log(HeadRecv[1])
// 				// 				console.log(JSON.parse(str))
// 				// 				var msgdata =JSON.parse(str);
// 				// 				if(HeadRecv[1]==10002){
// 				// 					
// 				// 				}else if(HeadRecv[1]==10003){
// 				// 					
// 				// 				}
// 				// 		     	
// 				// 		     };
// 				// 		     fileReader.readAsArrayBuffer(msg.data);
// 				// 			 }, 0); 
// 				// 	})
// 				// 	// -----断线--首次--
// 				// },5000)
// 			}else if(HeadRecv[1]==11001){
// 				var nowCounter = JSON.parse(str).counter
// 				if(nowCounter==6){
// 				}else{
// 					// console.log(5+'重载')
// 					Socket.onReload()
// 				}
// 			}
// 		};
// 		fileReader.readAsArrayBuffer(msg.data);
// 		}, 0); 
// 		// 消息处理---
// 	});
// 	// -------------------------------
// },20000)
Socket.eventPatch.onOpen((msg,sk)=>{        //监听是否连接成功
    console.log('连接成功')
});
Socket.eventPatch.onMsg((msg,sk)=>{    //监听是否接受消息
	// console.log(Socket)
    // console.log(msg)
	// console.log(Socket.eventPatch.events.onMsg[0])
	Socket.eventPatch.events.onMsg = [Socket.eventPatch.events.onMsg[0],Socket.eventPatch.events.onMsg[Socket.eventPatch.events.onMsg.length-1]]
	console.log(Socket)
	// Socket.eventPatch.events.onMsg = [Socket.eventPatch.events.onMsg[0]];
	// Socket.eventPatch.events.onMsg = [];
	// var SocketSocket = Socket.eventPatch.events.onMsg;
	// console.log(SocketSocket)
	// 	var obj = {};
	// 	SocketSocket = SocketSocket.reduce(function(item, next) {
	// 	  obj[next.key] ? '' : obj[next.key] = true && item.push(next);
	// 	   return item;
	// 	}, []);
	// 	console.log(SocketSocket)
		// Socket.eventPatch.events.onMsg = SocketSocket;
	// 消息处理---
	setTimeout(function() {
	var fileReader = new FileReader();
	fileReader.onload = function (progressEvent) {
		var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
		var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
		var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
		var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
		// console.log(HeadRecv[1],JSON.parse(str))
		//to do
		// console.log(1119000)
		console.log(HeadRecv[1])
		console.log(JSON.parse(str))
		var msgdata =JSON.parse(str);
		if(HeadRecv[1]==11002){//系统消息
			if(JSON.parse(str).type==1){
				uni.showToast({
					title: JSON.parse(str).content,
					duration: 1500,
					icon:"none",
					success: function () {
					}
				});
			}else if(JSON.parse(str).type==2){
				uni.showToast({
					title: JSON.parse(str).content,
					duration: 1500,
					icon:"none",
					success: function () {
						// window.location.href='http://www.mycat1314.com'
						// navigateTo('/pages/paymoney/paymoney',null);
					}
				});
			}
			
		}else if(HeadRecv[1]==11006){
			uni.getStorage({
				key: 'storage_login_str',
				success: function (res) {
					var storage_login_str = JSON.parse(res.data);
					storage_login_str.cash=msgdata.cash;
					var str16 =JSON.stringify(storage_login_str)
					// console.log(str)
					uni.setStorage({
						key: 'storage_login_str',
						data: str16,
						success: function () {
							// console.log('success');
						}
					});
				}
			});
			// -----
			uni.setStorage({//2 为展示  1为隐藏
				key: 'storage_login_downs',
				data: '2',
				success: function () {
					console.log('down success');
				}
			});
			
		}else if(HeadRecv[1]==10010){
			// ---先重连---
			uni.getStorage({
				key: 'storage_login_str',
				success: function (res) {
					// console.log(JSON.parse(res.data).logintoken)
					// console.log(JSON.parse(res.data).useridx)
					// -----断线--首次--
					
					var array =JSON.stringify({
						"userName": JSON.stringify(JSON.parse(res.data).useridx),
						"pwd": JSON.parse(res.data).logintoken,
						"devId": systemId(),
						"devType": system(),
						"productType": 3,
						"isRelogin": true,
						"loginWay": 0,
						"language": 3,
						"ver": "1.00",
						"ip":"127.0.0.1"
					})
					// console.log(array)
					var arr = sendD(array);
					// ---------------socket-login---------
					// alert(this.$Socket.isconnect)
					console.log(Socket)
					// console.log(111)
					if(Socket.isconnect==false){
						Socket.onReload()
					}
					Socket.nsend(arr)
					// console.log("yijing")
					Socket.eventPatch.onMsg((msg,sk)=>{    //监听是否接受消息
						setTimeout(function() {
						// console.log(msg)
						// console.log(sk)
						 var fileReader = new FileReader();
						     fileReader.onload = function (progressEvent) {
						     	var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
						     	var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
						     	var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
						     	var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
						     	// console.log(HeadRecv[1],JSON.parse(str))
						     	//to do
								// console.log(HeadRecv[1])
								// console.log(JSON.parse(str))
								var msgdata =JSON.parse(str);
								if(HeadRecv[1]==10002){
									console.log("断线重连成功")
									uni.setStorage({
										key: 'storage_login_str',
										data: str,
										success: function () {
											// console.log('success');
										}
									});
								}else if(HeadRecv[1]==10003){
									navigateTo('/pages/startup/startup',null);
									uni.showToast({
										title: msgdata.error,
										duration: 1000,
										icon:"none",
									});
								}
						     	
						     };
						     fileReader.readAsArrayBuffer(msg.data);
							 }, 0); 
					})
					// -----断线--首次--
				}
			});
			// ---先重连---
		}else if(HeadRecv[1]==10002){
			// console.log('登录成功')
			// clearInterval()
			
		}else if(HeadRecv[1]==11024){
			uni.setStorage({
				key: 'storage_login_tVideo',
				data: str,
				success: function () {
					console.log('推荐视频获取成功');
				}
			});
		}
		
	};
	fileReader.readAsArrayBuffer(msg.data);
	}, 0); 
	// 消息处理---
});
Socket.eventPatch.onClose((err,sk)=>{    //监听是否关闭连接
    console.log('关闭了连接')
	if(Socket.isconnect==false){
		Socket.onReload()
	}
});
Socket.eventPatch.onError((err,sk)=>{    //监听是否发生了错误
    console.log('连接出错')
	if(Socket.isconnect==false){
		Socket.onReload()
	}
});
Socket.eventPatch.onReload((res,sk)=>{    //监听是否重连成功
    console.log('重载：' + res)
	if(Socket.isconnect==false){
		Socket.onReload()
	}
});
Socket.eventPatch.onRdFinsh((count,sk)=>{        //监听最大重连次数是否已完成
    console.log('最大重连次数已完成' + count)
	if(Socket.isconnect==false){
		Socket.onReload()
	}
});
Vue.prototype.$Socket = Socket;



import Global_ from './lib/js/GlobalObj.js'   //全局对象
Vue.prototype.GLOBAL = Global_; //添加Global_到Vue的原型对象上
import Language_ from './lib/js/LanguageObj.js'   //全局对象
Vue.prototype.Language = Language_; //添加Global_到Vue的原型对象上
// 以下是注册组件的方法
import TagBar from './components/tabbar/tabbar.vue';
Vue.component("TagBar",TagBar); // 全局注册组件
import InputFile from './components/inputfile/inputfile.vue';
Vue.component("InputFile",InputFile); // 全局注册组件

Vue.config.productionTip = false

App.mpType = 'app'

const app = new Vue({
    ...App
})
app.$mount()
