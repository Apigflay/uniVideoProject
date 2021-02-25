<template>
	<view>
		<button @click="facebook()">facebook</button>
		<button @click="twitter()">twitter</button>
		<button @click="google()">google</button>
		<button @click="line()">line</button>
		<button @click="aaa()">实验接口</button>
	</view>
</template>

<script>
	import facebook from '../../lib/js/facebook.js' //facebook 的引入文件
	import twitter from '../../lib/js/oauth.js' // twitter google line 的引入文件
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				userid: 111111111, // facebook
				
			};
		},
		
		onLoad() { //监听页面加载，
			//facebook 开始启动项
			window.fbAsyncInit = function() {
				FB.init({
				  appId      : 2473254342920717,
				  cookie     : true,                     // Enable cookies to allow the server to access the session.
				  xfbml      : true,                     // Parse social plugins on this webpage.
				  version    : 'v5.0'                    // Use this Graph API version for this call.
				});
				FB.AppEvents.logPageView();
			}
			
			// 页面开始检测用户是否已经使用Facebook登录页面
			// FB.getLoginStatus(function(response) {
			//      console.log("getLoginStatus")
			//      console.log(response);
			// 	 if (response.status === 'connected') {   // Logged into your webpage and Facebook.
			// 	   console.log('Welcome!  Fetching your information.... ');
			// 	   // FB.api('/me', function(response) {
			// 	   //   console.log('Successful login for: ' + response.name);
			// 	   // });
			// 	 } else if(response.status === 'not_authorized'){
			// 		 console.log('用户没有登录页面')
			// 	 }else if(response.status === 'unknown') {
			// 		 console.log("用户未登录")
			// 	 }else{                                 // Not logged into your webpage or we are unable to tell.
			// 	   console.log("未获取到数据")
			// 	 }
			// });
		},
		
		methods:{
			// 接口测试
			aaa:function(){
				var array=base64ToArrayBuffer(encrypt(JSON.stringify({
					Unionid: '1172886913692254209',
					Type: '3',
					DeciveId: systemId(),
					DeviceType: system()
				})))
				
				console.log(decrypt("LDZVGiiPVwnl9jDOuakGIOl++yQIKW2fjyGBknmT1/gJzs0TphUg8kGqOoOJzu0c9x0z5jzQIceiSklRhabs6GawNtcWdlDZByQ4IAnvsM/4wcA6JNo+zHiIrDBdO6vR"))
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/account/ThirdLogin',array)));
				
				if(res.code==100){
					console.log(res.data)
					this.model1Data=res.data;
					// ---------------------------
					var array =JSON.stringify({
						// "userName": response.authResponse.userID,
						// "pwd":response.authResponse.accessToken,
						// facebook
						// "userName": "133269254740411",
						// "pwd":"EAAGaTZBRtH5UBADTsW4PH6xQdaB1UGneWZCFzbtlK5vxt4yAFgGtGI74puCAqOCA87cRNCfv8KNBN2e3uWWkWnZCWCCZBojBSaBBjBP94XDr1k5nfRBKisHTG9kCHRIW61Cf0iUUvam4BZAVS0pmACdoLd9sNj7gfuzeneA789X00R1TsNtY7fn05BmeMKv0ZD",
						// google
						"userName": "1172886913692254209",
						"pwd": "1172886913692254209-7P0L1DnkSGYywfEZ5OFRbcOlVSSVNb",
						"devId": systemId(),
						"devType": system(),
						"productType": 3,
						"isRelogin": false,
						"loginWay": 1,
						"language": 0,
						"ver": "1.00",
						"ip":"127.0.0.1"
					})
					console.log(array)
					var arr = sendD(array);
					uni.connectSocket({
						url: this.GLOBAL.urlSocketPoint,
					});
					uni.onSocketOpen(function (res) {
						
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
					  	console.log(res2.data)
					  			  
					     var fileReader = new FileReader();
					     fileReader.onload = function (progressEvent) {
					     	var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
					     	var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
					     	var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
					     	var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
					     	// console.log(HeadRecv[1],JSON.parse(str))
					     	//to do
							if(HeadRecv[1]==10002){
								uni.switchTab({
									url: '/pages/home1/home1'
								});
							}
					     	console.log(HeadRecv[1])
					     	console.log(JSON.parse(str))
					     };
					     fileReader.readAsArrayBuffer(res2.data);
					  	
					      
					    });
					  	  
					  });
					// ------------------------------
				}
				
				
				
				
				
				
				
				// -----socket----
				// var array =JSON.stringify({
				// 	// "userName": response.authResponse.userID,
				// 	// "pwd":response.authResponse.accessToken,
				// 	"userName": "107728597293332",
				// 	"pwd":"EAAQZBHuMiEdABAGZAsB2amqcduQYNi16wPuSZAqTA8VTZAJZCw0LXZAhvZAOGGKchKWwpwlp9pmajTtcaHJPSt8XTAeeTWGgoh6k4RnwmsYgl8wRnPvdxmiFzn0imblA2E8iTV4QKWnaRZCzJfYhDmf63G6BLYT3XjebiJGud86J5ajud2lrPLd5m4AHGGM40xNZA4OeKUJkxm53qWeFFduXT",
				// 	"devId": systemId(),
				// 	"devType": system(),
				// 	"productType": 3,
				// 	"isRelogin": false,
				// 	"loginWay": 1,
				// 	"language": 0,
				// 	"ver": "1.00",
				// 	"ip":"127.0.0.1"
				// })
				// console.log(array)
				// var arr = sendD(array);
				// uni.connectSocket({
				// 	url: 'ws://account/ThirdLogin',
				// });
				// uni.onSocketOpen(function (res) {
				// 	
				// 	// console.log(res)
				//   console.log('WebSocket连接已打开！');
				//   uni.sendSocketMessage({
				//     data:arr,
				//     success:function(res1){
				// 		console.log(res1)
				//     },
				//     fail:function(err){
				//   	 console.log(err)
				//     },
				//     complete:function(com){
				//   	  // console.log(com)
				//     }
				//   });
				//   uni.onSocketMessage(function (res2) {
				//   	console.log(res2.data)
				//   			  
				//      var fileReader = new FileReader();
				//      fileReader.onload = function (progressEvent) {
				//      	var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
				//      	var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
				//      	var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
				//      	var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
				//      	// console.log(HeadRecv[1],JSON.parse(str))
				//      	//to do
				// 		if(HeadRecv[1]==10002){
				// 			uni.switchTab({
				// 				url: '/pages/home1/home1'
				// 			});
				// 		}
				//      	console.log(HeadRecv[1])
				//      	console.log(JSON.parse(str))
				//      };
				//      fileReader.readAsArrayBuffer(res2.data);
				//   	
				//       
				//     });
				//   	  
				//   });
				// -----socket----
			},
			// facebook 登录按钮
			// facebook:function(){
			// 	FB.login(function(response) {
			// 		if (response.status === 'connected') {
			// 		  console.log(response);
			// 		  // console.log('Successful login for: ' + response.authResponse.userID);
			// 	    } else if(response.status === 'not_authorized'){
			// 			console.log('用户没有登录页面')
			// 		} else if(response.status === 'unknown'){
			// 			console.log("用户未登录")
			// 			console.log(response);
			// 		} else {
			// 		  console.log('未获取到数据')
			// 	    }
			// 	},{scope:'public_profile,email'});
			// },
			facebook:function(){
				// FB.login(function(response) {
				//   // handle the response
				//   console.log(response);
				// },{scope:'email, public_profile'});
				FB.login(function(response) {
					console.log(response)
				    if (response.authResponse) {
				     console.log('Welcome!  Fetching your information.... ');
				     FB.api('/me', function(response) {
				       console.log('Good to see you, ' + response.name + '.');
					   console.log(response);
				     });
				    } else {
				     console.log('User cancelled login or did not fully authorize.');
				    }
				},{scope: 'public_profile,email'});
			},
	
		
			// twitter 第三方登录
			twitter: function() { // twitter第三方登录
			  // Initialize with your OAuth.io app public key
				OAuth.initialize('ppTUrql0j5UPSUUJhbHX0fGhE34'); // 连接OAuth站点
			  // Use popup for OAuth
			  // OAuth.popup('twitter').then(twitter => {
			  //   console.log(twitter);
			  //   // Retrieves user data from oauth provider
			  //   console.log(twitter.me());
			  // });
				OAuth.popup('twitter').done(function(result) {
					console.log(result)
					// do some stuff with result
					result.me().done(function(data) {
			  	    // do something with `data`, e.g. print data.name
			  		console.log(data)
					})
				})
			  
			},
			
			// google 第三方登录
			google: function(){
				OAuth.initialize('ppTUrql0j5UPSUUJhbHX0fGhE34')
				OAuth.popup('google').done(function(result) {
				    console.log(result)
				    // do some stuff with result
					result.me().done(function(data) {
					    // do something with `data`, e.g. print data.name
						console.log(data)
					})
				})
			},
			
			// line 第三方登录
			line: function(){
				OAuth.initialize('ppTUrql0j5UPSUUJhbHX0fGhE34')
				OAuth.popup('line').done(function(result) {
				    console.log(result)
				    // do some stuff with result
					result.me().done(function(data) {
					    // do something with `data`, e.g. print data.name
						console.log(data)
					})
					
				})
			}
		}
	}
</script>

<style lang="scss">

</style>

