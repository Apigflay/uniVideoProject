<template>
	<view class="content">
		<uni-icon type="person" size="30"></uni-icon>
		<!-- <span class="iconfont icon-paihangbang"></span> -->
		<!-- @click="goPage('login')"
		@click="goPage('tabbar')"
		@click="goPage('swiperbar')"
		@click="goPage('scrolltop')" -->
		<view class="title" @click="goPage('login')">login 下方弹出popular</view>
		<view class="title" @click="goPage('tabbar')">scroll tabbar</view>
		<view class="title" @click="goPage('swiperbar')">swiperbar </view>
		<view class="title" @click="goPage('scrolltop')">scrolltop </view>
		<view class="websocket" @click="creatWebsocket">
			websocket
		</view>
		<view class="btn" @click="goTestApi">
			btnaaa
		</view>
	<!-- 	<view class="home" @click="goPage('home')">
			首页
		</view>
		<view class="home" @click="goPage('home1')">
			首页1
		</view> -->
		
		

	</view>
</template>

<script>
	import uniIcon from "@/components/uni-icon/uni-icon.vue"
	export default {
		components: {uniIcon},
		data() {
			return {
				title: 'Hello'
			}
		},
		onLoad() {

		},
		methods: {
			goPage:function(page){
				console.log(page)
					uni.navigateTo({
					url: '../'+page+'/'+page
				});
			},
			// ----111111
			stringToBytes:function(str) {  
				var ch, st, re = []; 
				for (var i = 0; i < str.length; i++ ) { 
					ch = str.charCodeAt(i);  // get char  
					st = [];                 // set up "stack"  
				   do {  
						st.push( ch & 0xFF );  // push byte to stack  
						ch = ch >> 8;          // shift value down by 1 byte  
					}    
					while ( ch );  
					// add stack contents to result  
					// done because chars have "wrong" endianness  
					re = re.concat( st.reverse() ); 
				}  
				// return an array of bytes  
				return re;  
			} ,
			// ---------22222
			base64ToArrayBuffer:function(base64) {
	            var binaryString = window.atob(base64);
	            var binaryLen = binaryString.length;
	            var bytes = new Uint8Array(binaryLen);
	            for (var i = 0; i < binaryLen; i++) {
	                var ascii = binaryString.charCodeAt(i);
	                bytes[i] = ascii;
	            }
	            return bytes;
	        },
			// -------3333
			str2UTF8:function(str){
				var bytes = new Array(); 
				var len,c;
				len = str.length;
				for(var i = 0; i < len; i++){
					c = str.charCodeAt(i);
					if(c >= 0x010000 && c <= 0x10FFFF){
						bytes.push(((c >> 18) & 0x07) | 0xF0);
						bytes.push(((c >> 12) & 0x3F) | 0x80);
						bytes.push(((c >> 6) & 0x3F) | 0x80);
						bytes.push((c & 0x3F) | 0x80);
					}else if(c >= 0x000800 && c <= 0x00FFFF){
						bytes.push(((c >> 12) & 0x0F) | 0xE0);
						bytes.push(((c >> 6) & 0x3F) | 0x80);
						bytes.push((c & 0x3F) | 0x80);
					}else if(c >= 0x000080 && c <= 0x0007FF){
						bytes.push(((c >> 6) & 0x1F) | 0xC0);
						bytes.push((c & 0x3F) | 0x80);
					}else{
						bytes.push(c & 0xFF);
					}
				}
				return bytes;
			},
	// ij0kQBgjBZ5kqY0cdjdGLoe/l16jnI7sFig+7VYrhyc=
			// ----gXn4uTUfkPhq1G7dMW7Y2gVcTCj8eIwqiAW18JJWzpw=
			goTestApi:function(){
				console.log(this.str2UTF8('gXn4uTUfkPhq1G7dMW7Y2gVcTCj8eIwqiAW18JJWzpw'))
				var array =this.str2UTF8('gXn4uTUfkPhq1G7dMW7Y2gVcTCj8eIwqiAW18JJWzpw')
				
				console.log(this.base64ToArrayBuffer('gXn4uTUfkPhq1G7dMW7Y2gVcTCj8eIwqiAW18JJWzpw'))
				var array =this.base64ToArrayBuffer('gXn4uTUfkPhq1G7dMW7Y2gVcTCj8eIwqiAW18JJWzpw')

				// console.log(this.stringToBytes('gXn4uTUfkPhq1G7dMW7Y2gVcTCj8eIwqiAW18JJWzpw'))
				// var array =this.stringToBytes('gXn4uTUfkPhq1G7dMW7Y2gVcTCj8eIwqiAW18JJWzpw')
				
				// {'user':1}
				// var array = [56, 97, 51, 100, 50, 52, 52, 48, 49, 56, 50, 51, 48, 53, 57, 101, 54, 52, 97, 57, 56, 100, 49, 99, 55, 54, 51, 55, 52, 54, 50, 101, 56, 55, 98, 102, 57, 55, 53, 101, 97, 51, 57, 99, 56, 101, 101, 99, 49, 54, 50, 56, 51, 101, 101, 100, 53, 54, 50, 98, 56, 55, 50, 55]
				var xhr = new XMLHttpRequest;
				xhr.open("POST",'http://60.191.222.11:8044/account/test', false);
				xhr.send(array);
				
				
			},
			// socket
			creatWebsocket:function(){
				uni.connectSocket({
					url: 'ws://192.168.1.101:17400',
					data() {
						return {
							x: '',
							y: ''
						};
					},
					header: {
						'content-type': 'application/json'
					},
					// protocols: ['protocol1'],
					method: 'GET',
					success:function(res){
						console.log(res)
					},
					fail:function(err){
						console.log(err)
					},
					complete:function(com){
						console.log(com)
					}
				});
// struct msg  //8
// {
// 	int nLen;            //整个包的长度
// 	int nCMD;            //协议id
// 	int nZipLen;        
// 	char[] jsData        //长度:nLen-4*3
// };
				uni.onSocketOpen(function (res) {
				  console.log('WebSocket连接已打开！');
				});
				// send
				var socketOpen = false;
				var socketMsgQueue = ["少时诵诗书所所"];

				// uni.connectSocket({
				//   url: 'ws://192.168.1.101:17400'
				// });

				uni.onSocketOpen(function (res) {
				  socketOpen = true;
				  for (var i = 0; i < socketMsgQueue.length; i++) {
					sendSocketMessage(socketMsgQueue[i]);
				  }
				  socketMsgQueue = [];
				});

				function sendSocketMessage(msg) {
				  if (socketOpen) {
					uni.sendSocketMessage({
					  data: msg
					});
				  } else {
					socketMsgQueue.push(msg);
				  }
				}
				// 
			}
		}
	}
</script>

<style lang="scss">
@import '../../iconfont/iconfont.css';
.content {
	// display: flex;
	// flex-direction: column;
	// align-items: center;
	// justify-content: center;
	text-align: center;
	.btn{
		padding-top:40px;
		background: #cecece;
	}
}
</style>
