<template>
	<view class="content">
		<image class="more111" src="../../static/imgs/close1.png" mode="" @click="goBackPage"></image>
		<image class="bgImg" src="../../static/imgs/EnewUser.jpg" mode="" v-if="loginLanguage==0||loginLanguage==1||loginLanguage==2"></image>
		<image class="bgImg" src="../../static/imgs/TnewUser.jpg" mode="" v-if="loginLanguage==3"></image>
		
		
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,regMail,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				loginLanguage:null,//语言 用户语言
				loginData:null,//登录信息
			};
		},
		onLoad() {
			this.getLoginLanguage(); // 获取语言
		},
		methods:{
			getLoginData:function(){//获取socket成功信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.loginData = JSON.parse(res.data);
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
	// background:#21317E;
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
		height: 3964rpx;
	}
}
</style>
