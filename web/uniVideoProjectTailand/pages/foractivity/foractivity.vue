<template>
	<view class="content">
		<image class="more111" src="../../static/imgs/close1.png" mode="" @click="goBackPage"></image>
		<image :style="'height:'+height+'rpx'" class="bgImg" :src="imgUrl" mode="" ></image>
		<!-- <image :style="'height:'+height+'rpx'" class="bgImg" src="../../static/imgs/Emerry.jpg" mode="" v-if="loginLanguage==0||loginLanguage==1||loginLanguage==2"></image>
		<image :style="'height:'+height+'rpx'" class="bgImg" src="../../static/imgs/Tmerry.jpg" mode="" v-if="loginLanguage==3"></image> -->
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,regMail,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				loginLanguage:null,//语言 用户语言
				loginData:null,//登录信息
				height:0,//动态高度
				imgUrl:'',//imgurl
			};
		},
		onLoad() {
			this.getLoginLanguage(); // 获取语言
			this.getLoginData()
		},
		methods:{
			getLoginData:function(){//获取socket成功信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.loginData = JSON.parse(res.data);
						that.getinitMsg()
						that.goForTotal()
					}
				});
			},
			getLoginLanguage:function(){//获取socket成功信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.loginLanguage = JSON.parse(res.data);
					}
				});
			},
			getinitMsg:function(){//获取登录信息
				var that = this;
				if(this.loginLanguage==0){
					var languageT='Chinese';
				}else if(this.loginLanguage==1){
					var languageT='Chinese';
				}else if(this.loginLanguage==2){
					var languageT='English';
				}else if(this.loginLanguage==3){
					var languageT='Thailand';
				}
				uni.request({
					url: this.GLOBAL.urlPoint+'Activity/ActivityHomeAd' ,//仅为示例，并非真实接口地址。
					method:"GET",
					data: {
						token:this.loginData.webtoken,//	string	
						userIdx:this.loginData.useridx,//	int
						type:languageT,//Chinese English Thailand  string  默认  Thailand
					},
					success: (res) => {
						var msgData = JSON.parse(decrypt(res.data))
						// console.log(msgData)
						var urllong = msgData.data.ad[0].picAddr;
						that.imgUrl = urllong;
						uni.getImageInfo({
							src:urllong,
							success: function (image) {
								that.height = image.height;
							}
						});
					}
				});
			},
			goForTotal:function(){//统计活动
				uni.request({
					url: this.GLOBAL.urlPoint+'Activity/ActivityDataIn' ,//仅为示例，并非真实接口地址。
					method:"GET",
					data: {
						token:this.loginData.webtoken,//	string	
						userIdx:this.loginData.useridx//	int
						// type:languageT,//Chinese English Thailand  string  默认  Thailand
					},
					success: (res) => {
						// console.log(res)
						console.log(decrypt(res.data))
					}
				});
			},
			goBackPage:function(){
				navigateTo('/pages/home/home',null);
			}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	// background:rgba(35,35,35,1);#191919
	background:#21317E;
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
		background: #fff;
		
	}
	.bgImg{
		width: 750rpx;
		// height: 2283rpx;
	}

}
</style>
