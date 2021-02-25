import store from '../../store/index.js'
// console.log(store)
import {sendD110,doBlob,Do,web} from "..//js/GlobalFunction.js"
// uni.connectSocket({
//     url: 'wss://server.mycat1314.com:17400',
// });
// uni.onSocketOpen(function (res) {
//   console.log('WebSocket连接已打开！');
// });
// uni.onSocketError(function (res) {
//   console.log('WebSocket连接打开失败，请检查！');
// });
// 发送消息
var socketOpen = false;
var socketMsgQueue = [66];

uni.connectSocket({
  url: 'wss://server.mycat1314.com:17400'//ws://60.191.222.11:17400 wss://server.mycat1314.com:17400 servertest.mycat1314.com
});

uni.onSocketOpen(function (res) {
  socketOpen = true;
  for (var i = 0; i < socketMsgQueue.length; i++) {
    sendSocketMessage(socketMsgQueue[i]);
  }
  socketMsgQueue = [];
});

// uni.onSocketOpen(function (res) {
//   console.log('WebSocket连接已打开！');
// 	var msg = JSON.stringify({"counter": 66})
// 	var arr = sendD110(msg)
// 	uni.sendSocketMessage({
//       data: arr,
// 	  success(res) {
// 	  	console.log(res)
// 	  },
// 	  fail(err) {
// 	  	console.log(err)
// 	  },
// 	  complete(com) {
// 	  	console.log(com)
// 	  }
//     });
// 	// console.log(res)
//  //  socketOpen = true;
//  //  for (var i = 0; i < socketMsgQueue.length; i++) {
//  //    sendSocketMessage(socketMsgQueue[i]);
//  //  }
//  //  socketMsgQueue = [];
// });

function sendSocketMessage(msg) {
		var msgS = JSON.stringify({"counter": 66})
		var arr = sendD110(msgS)
		setInterval(function(){
			uni.sendSocketMessage({
			  data: arr,
			  success(res) {
			  	// console.log(res)
			  },
			  complete(com) {
			  	// console.log(com)
				
			  }
			});
		},5000)
		
  if (socketOpen) {
    
  } else {
    socketMsgQueue.push(msg);
  }
}
// 发送消息
uni.onSocketMessage(function (res) {
	// console.log(res)
	// console.log(res.data)
	// var result = doBlob(res.data)
	// console.log(result)
	// let base64 =  doBlob(res.data);
	// console.log(base64)
	
	Do(res.data)
// let base64 = Do(res);
// console.log(base64)
});