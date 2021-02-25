// import Vue from 'vue'
// import socket from "./socket.js"; //引入socket.js 重要
// // import store from "@/common/chat/store.js";
// const Socket =new socket({
//     url: 'ws://192.168.1.104:17400', //连接地址 必填
// 	onOpen(res) {
//         console.log('连接成功')
//     },
//     onClose(err) {
//         console.log('关闭了连接')
//     },
//     onReload(res) {
//         console.log('重载：' + res)
//     },
//     onMsg(msg) {
//         console.log(msg)
//     }
// })
// 
// // const Socket = new socket({
// // 	url: this.GLOBAL.urlSocketPoint, //连接地址 必填
// // 	// maxInterValCount: 5,
// // 	// interValTime: 2000,
// // 	onOpen (res) {
// // 		console.log('连接成功+1111  ')
// // 		console.log(this.GLOBAL.urlSocketPoint)
// // 		// let msg = {
// // 		// 	type: 'self',
// // 		// 	selfName: '老司机',
// // 		// 	text: '连接成功了',
// // 		// 	time: new Date().toLocaleTimeString()
// // 		// };
// // 		// this.nsend(JSON.stringify(msg));
// // 	},
// // 	onClose(err){
// // 		console.log('关闭了连接')
// // 	},
// // 	onReload(res){
// // 		console.log('重载：' + res)
// // 	},
// // 	onRdFinsh(count) {
// // 		console.log(count + '次重连已完成')
// // 	},
// // 	onMsg(msg) {
// // 		const res=JSON.parse(msg.data);
// // 		// const keys=Object.keys(res);
// // 		// const State=Object.assign({},store.state.SocketState)
// // 		// for(let i=0;i<keys.length;i++){
// // 		// 	let key=keys[i];
// // 		// 	if(!State[key]){
// // 		// 		State[key]=[];
// // 		// 	}
// // 		// 	State[key].push(res[key])
// // 		// }
// // 		// store.commit('setSocketState',State)
// // 	}
// // })
// Vue.prototype.$Socket = Socket;
// 
// 