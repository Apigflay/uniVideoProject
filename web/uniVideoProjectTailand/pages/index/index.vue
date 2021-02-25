<template>
	<view class="content">
		<image src="../../static/imgs/fang1y.png" mode=""></image>
		<image src="../../static/imgs/mail1y.png" mode=""></image>
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
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,fillstr2ab,sendD,navigateTo} from "../../lib/js/GlobalFunction.js"
	import uniIcon from "@/components/uni-icon/uni-icon.vue"
	export default {
		components: {uniIcon},
		data() {
			return {
				tabbarLoginLanguage: null, // 用户语言
				title: 'Hello',
				aaa:null,
			}
		},
		onLoad() {
			this.getLoginlanger(); // 获取语言
		},
		methods: {
			getLoginlanger:function(){ // 获取当前语言
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.tabbarLoginLanguage = JSON.parse(res.data);
						console.log(that.tabbarLoginLanguage);
					}
				});
			},
			getLoginMsg:function(){
				
			},
			goPage:function(page){
				console.log(page)
					uni.navigateTo({
					url: '../'+page+'/'+page
				});
				// navigateTo('/pages/chatpop/chatpop',obj);
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
				console.log(this.str2UTF8('72tBoL2h0Nfdx9vr6O9Coyl9TCSwV+ziICCATukKP58z4TbagQsFHBYswhkkj1ZW1LZH7ltXbXD147yop9G1uEA8+TmtfjyvPun0wfzSdDCJzBa/RjFsPccMM/Eh63bdjS0R0CB7r/usAIMfAnLL2Zw3UfYR1fjX/ISAZ0IQBsyh8ueZ5i8IL82vmonBTGlNk9dPv8aX3Orc/kWRiYZk0g=='))
				// var array =this.str2UTF8('72tBoL2h0Nfdx9vr6O9Coyl9TCSwV+ziICCATukKP58z4TbagQsFHBYswhkkj1ZW1LZH7ltXbXD147yop9G1uEA8+TmtfjyvPun0wfzSdDCJzBa/RjFsPccMM/Eh63bdjS0R0CB7r/usAIMfAnLL2Zw3UfYR1fjX/ISAZ0IQBsyh8ueZ5i8IL82vmonBTGlNk9dPv8aX3Orc/kWRiYZk0g==')
				
				console.log(this.base64ToArrayBuffer('72tBoL2h0Nfdx9vr6O9Coyl9TCSwV+ziICCATukKP58z4TbagQsFHBYswhkkj1ZW1LZH7ltXbXD147yop9G1uEA8+TmtfjyvPun0wfzSdDCJzBa/RjFsPccMM/Eh63bdjS0R0CB7r/usAIMfAnLL2Zw3UfYR1fjX/ISAZ0IQBsyh8ueZ5i8IL82vmonBTGlNk9dPv8aX3Orc/kWRiYZk0g=='))
				var array =this.base64ToArrayBuffer('72tBoL2h0Nfdx9vr6O9Coyl9TCSwV+ziICCATukKP58z4TbagQsFHBYswhkkj1ZW1LZH7ltXbXD147yop9G1uEA8+TmtfjyvPun0wfzSdDCJzBa/RjFsPccMM/Eh63bdjS0R0CB7r/usAIMfAnLL2Zw3UfYR1fjX/ISAZ0IQBsyh8ueZ5i8IL82vmonBTGlNk9dPv8aX3Orc/kWRiYZk0g==')

				console.log(this.stringToBytes('LI5D75tfGYKk99euqcoA4A=='))
				// var array =this.stringToBytes('LI5D75tfGYKk99euqcoA4A==')
				
				// {'user':1}
				// var array = [56, 97, 51, 100, 50, 52, 52, 48, 49, 56, 50, 51, 48, 53, 57, 101, 54, 52, 97, 57, 56, 100, 49, 99, 55, 54, 51, 55, 52, 54, 50, 101, 56, 55, 98, 102, 57, 55, 53, 101, 97, 51, 57, 99, 56, 101, 101, 99, 49, 54, 50, 56, 51, 101, 101, 100, 53, 54, 50, 98, 56, 55, 50, 55]
				var xhr = new XMLHttpRequest;
				xhr.open("POST",'http://60.191.222.11:8044/Account/userRegisterPhone', true);
				xhr.send(array);
				
				
			},
			// socket
			creatWebsocket:function(){
					var array =JSON.stringify({
						"userName": "15713801628",
						"pwd": "058566",
						"devId": "wwww",
						"devType": "eeee",
						"productType": 3,
						"isRelogin": false,
						"loginWay": 1,
						"language": 0,
						"ver": "1.00",
						"ip":"127.0.0.1"
					})
					


					



//============================================接受消息块=======================================
				function con(arr1,arr2){
					console.log(arr1)
					console.log(arr2)
					return arr1
				}
				var that =this;
				function work(data) {
					var fileReader = new FileReader();
					fileReader.onload = function (progressEvent) {
						var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
						var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
						var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
						var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
						
						console.log(HeadRecv[1])
						console.log(JSON.parse(str))
						// console.log(HeadRecv[1],JSON.parse(str))
					};
					fileReader.readAsArrayBuffer(data);
				}
			
					var arr = sendD(array)
					// console.log(arr)
				// struct msg  //8
				// {
				// 	int nLen;            //整个包的长度
				// 	int nCMD;            //协议id
				// 	int nZipLen;        
				// 	char[] jsData        //长度:nLen-4*3
				// };
				
				var socketOpen = false;
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
					method: 'GET',
					success:function(res){
						// console.log(res)
						
					}
				});
				
					

				
// encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData
				uni.onSocketOpen(function (res) {
					socketOpen = true;
					// console.log(res)
				  console.log('WebSocket连接已打开！');
				  uni.sendSocketMessage({
				    data:arr,
				    success:function(res1){
						console.log(res1)
				    },
				    fail:function(err){
				  	 console.log(err)
				    },
				    complete:function(com){
				  	  // console.log(com)
				    }
				  });
				  uni.onSocketMessage(function (res2) {
					// console.log(res2.data)
			  
				   work(res2.data);
					
				    
				  });
					  
				});

// 				uni.onSocketError(function (res) {
// 				  console.log('WebSocket连接打开失败，请检查！');
// 				});
				// send-----



				

				//send-----
				// 
			}
		}
	}
</script>

<style lang="scss">
@import '../../iconfont/iconfont.css';
page{
	width: 100%;
	height: 100%;
	background: #fff;
}
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
