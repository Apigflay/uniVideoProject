<template>
	<view class="content">
		<image class="more111" src="../../static/imgs/close1.png" mode="" @click="goBackPage"></image>
		<!-- <iframe class="iframe" :src="param" frameborder="0"></iframe> -->
		<!-- pay.mycat1314.com/pay/payList?param={"useridx":"10009788","deviceType":"h5","deviceID":"设备id","buddleID":"h5","version":"1.0"} -->
		<view class="contentbox" id="content" ref="content1">
			
		</view>
		<!-- <view class="" v-if="systemL==0" @click="goNewPages">
			即将跳转支付
		</view> -->
		
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,regMail,navigateTo,productType} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				tabbarLoginLanguage: null, // 用户语言
				loginData:null,//登录信息
				param:null,//url参数集合
				loginLanguage:null,//语言
				tongjiData:null,//统计信息
				systemL:1,//
			};
		},
		onLoad() {
			this.getLoginlanger(); // 获取语言
			this.getLoginData()
			this.getLoginLanguage()
		},
		onReady(){
			this.getParamData()
		},
		methods:{
			getLoginlanger:function(){ // 获取当前语言
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.tabbarLoginLanguage = JSON.parse(res.data);
						// console.log(that.tabbarLoginLanguage);
					}
				});
				// uni.getStorage({
				// 	key: 'storage_login_t',
				// 	success: function (res) {
				// 		that.tongjiData = JSON.parse(res.data);
				// 		// console.log(that.tongjiData);
				// 	},
				// 	fail:function(err){
				// 		console.log(err)
				// 	}
				// });
			},
			getLoginData:function(){//获取socket成功信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						// console.log(JSON.parse(res.data))
						that.loginData = JSON.parse(res.data);
						// console.log(this.loginData.bindmail);
						// if(that.loginData.bindmail==false){
						// 	// console.log("未绑定邮箱");
						// 	that.marskShow = true;
						// 	that.getCheckArea =true;
						// }
						// if(that.loginData.setnick==false){
						// 	console.log('未设置喜好')
						// 	that.marskShow = true;
						// 	that.mainShowArea =true;
						// }
						// that.getParamData()
						
					}
				});
			},
			getLoginLanguage:function(){//获取socket成功信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						// console.log(JSON.parse(res.data))
						that.loginLanguage = JSON.parse(res.data);
						// console.log(this.loginData.bindmail);
						// if(that.loginData.bindmail==false){
						// 	// console.log("未绑定邮箱");
						// 	that.marskShow = true;
						// 	that.getCheckArea =true;
						// }
						// if(that.loginData.setnick==false){
						// 	console.log('未设置喜好')
						// 	that.marskShow = true;
						// 	that.mainShowArea =true;
						// }
						// that.getParamData()
					}
				});
			},
			getParamData:function(){
				var language = "chinese"
				if(this.loginLanguage==0){
					language = "chinese"
				}else if(this.loginLanguage==1){
					language = "chinese"
				}else if(this.loginLanguage==2){
					language = "english"
				}else if(this.loginLanguage==3){
					language = "thailand"
				}
				
				var param = JSON.stringify({
					useridx:JSON.stringify(this.loginData.useridx),
					deviceType:"h5",
					deviceID:'wwww',//systemId()
					buddleID:"h5",
					version:"1.0.0",
					language:language,
				})
				// channelid:this.tongjiData.channelid,
				// random:this.tongjiData.random,
				var url ='https://pay.woopsss.com/Pay/PayList?param='+param;
				document.getElementById("content").innerHTML = '<object type="text/html" data='+url+' width="100%" height="100%"></object>';
				// console.log(window)
				// console.log(param)
				// window.location.href='https://pay.mycat1314.com/pay/PayList?param='+param;
				
				// window.open('pay.mycat1314.com/Pay/PayReturn','_blank')
				
				// var syst = productType();
				// if(syst==0){//ios
				// 	this.systemL=0;
				// 	uni.showModal({
				// 		title: '提示',
				// 		content: '即將跳轉第三方充值',
				// 		success: function (res) {
				// 			if (res.confirm) {
				// 				// console.log('用户点击确定');
				// 				window.location.href='https://pay.mycat1314.com/pay/PayList?param='+param;
				// 			} else if (res.cancel) {
				// 				console.log('用户点击取消');
				// 			}
				// 		}
				// 	});
				// 	// window.open('https://pay.mycat1314.com/pay/PayList?param='+param,'_blank')
				// 	// var winRef = window.open("","_blank");
				// 	// winRef.location = 'https://pay.mycat1314.com/pay/PayList?param='+param;
				// 	// window.location.href='https://pay.mycat1314.com/pay/PayList?param='+param;
				// 	
				// }else{
				// 	window.open('https://pay.mycat1314.com/pay/PayList?param='+param,'_blank')
				// }
				
				   // function open1() {
						// alert(111);
						// window.showModalDialog('https://pay.mycat1314.com/pay/PayList?param='+param);
						// window.open('https://pay.mycat1314.com/pay/PayList?param='+param, 'newwindow', 'height=80%, width=100%, top=20%, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no')
					// }
				// var tempwindow=window.open('_blank'); // 先打开页面
				// tempwindow.location='https://pay.mycat1314.com/pay/PayList?param='+param; // 后更改页面地址
				// this.param ='https://pay.mycat1314.com/pay/PayList?param='+param;//https://pay.mycat1314.com/pay/PayList

				// console.log('http://pay.mycat1314.com/pay/PayList?param={"buddleID":"com.tg.facetalk","deviceID":"7BD1B8D6-6839-456D-9A0A-88E78CEDBA16","deviceType":"ios","language":"thailand","useridx":"10009790","version":"1.0.0"}')
			},
			// goNewPages:function(){
			// 	window.open('https://pay.mycat1314.com/pay/PayList?param='+param,'_blank')
			// },
			goBackPage:function(){
				
				// console.log(11)
				// console.log(this.loginData.isAnchor)
				if(this.loginData.isAnchor==true){
					// uni.navigateTo({
					// 	url: '/pages/anchorme/anchorme'
					// });
					
					navigateTo('/pages/anchorme/anchorme',null);
				}else{
					// uni.navigateTo({
					// 	url: '/pages/my/my'
					// });
					
					navigateTo('/pages/my/my',null);
				}
				// console.log('shuaxin1')
				// location.reload();
				// console.log('shuaxin')
				
				
			}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	// background:rgba(35,35,35,1);#191919
	// background:#191919;
}
.content{
	width: 100%;
	height: 100%;
	position: relative;
	.more111{
		position: absolute;
		z-index: 100009;
		width: 36rpx;
		height: 36rpx;
		padding: 10rpx;
		top: 28rpx;
		right: 28rpx;
	}
	// .iframe{
	// 	width: 100%;
	// 	height: 100%;
	// 	position: absolute;
	// 	z-index: 10;
	// }
	.contentbox{
		width: 100%;
		height: 100%;
	}
}
</style>
