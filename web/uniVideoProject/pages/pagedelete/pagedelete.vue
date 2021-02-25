<template>
	<view class="main">
		<view class="top">
			<image class="img" src="../../static/pictures/back_1.png" @click="goBack()"></image>
			{{this.Language.language[this.tabbarLoginLanguage].language124}}
		</view>
		<view class="content">
			<text class="p1">{{this.Language.language[this.tabbarLoginLanguage].language124}}</text>
			<view></view>
			<view class="p2">
				<text class="p1">{{this.Language.language[this.tabbarLoginLanguage].language171}}</text>
			</view>
		</view>
		<view class="search">
			<text class="p3">@</text>
			<form @submit="formSubmit">
				<input class="section__title" id="search" type="search" placeholder="place" placeholder-class="placeclass" @input="check" />
			</form>
		</view>
		<view class="bottom" @click="getDelete()">
			<view :class="show == true?'p2':'p1'">{{this.Language.language[this.tabbarLoginLanguage].language173}}</view>
		</view>
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				tabbarLoginLanguage: null, // 用户语言
				tabbarLoginData:null, // 用户初始化页面加载的登录信息
				nickname: null, // 用户呢称
				useridx: null, // 用户IDX
				show: false, // 确认按钮的状态值
				place: '', // 搜索栏默认提示文字
			};
		},
		onLoad:function(){
			this.getLoginlanger(); // 获取语言
			this.getLoginMsg();
		},
		methods:{
			getLoginlanger:function(){ // 获取当前语言
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.tabbarLoginLanguage = JSON.parse(res.data);
						console.log(that.tabbarLoginLanguage);
					}
				});
				this.place = this.Language.language[this.tabbarLoginLanguage].language163;
			},
			// ---------------------------获取用户信息-------------------------------------
			getLoginMsg:function(){//登录信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.tabbarLoginData = JSON.parse(res.data);
						// console.log(that.tabbarLoginData);
						that.loginMsgData = that.tabbarLoginData
						that.nickname = that.tabbarLoginData.nickname
						that.useridx = that.tabbarLoginData.useridx
						if(that.tabbarLoginData.isAnchor==false){
							that.isAnchor = false;
						}
					}
				});
			},
			// ---------------------------获取用户信息------------------------------------- 
			
			// ---------------------------输入框-------------------------------------
			check:function(event){ 
				// console.log(event.target.value);
				// console.log(this.nickname)
				if(event.target.value == this.nickname){
					this.show = true;
				}else{
					this.show =false;
				}
			},
			// ---------------------------输入框------------------------------------- 
			
			//----------------------------确认删除事件---------------------------------
			getDelete:function(){
				var that = this;
				if(this.show == true){
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({
						userIdx: this.useridx,
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/account/UserDelete',array)));
					if(res.code==100){
						// console.log(res);
						// uni.navigateTo({
						// 	url: "/pages/startup/startup"
						// }); 
						navigateTo('/pages/startup/startup',null);
					}
				}else if(this.show == false){
					if(that.tabbarLoginLanguage == 0 || that.tabbarLoginLanguage == 1){
						var str1 = '用戶名錯誤'
					}else if(that.tabbarLoginLanguage == 2){
						var str1 = 'username error'
					}else if(that.tabbarLoginLanguage == 3){
						var str1 = 'ชื่อผู้ใช้ผิดพลาด'
					}
					uni.showToast({
						title: str1,
						duration: 1000,
						icon:"none",
					});
				}
				
			},
			//----------------------------确认删除事件---------------------------------
			
			//------------------------------后退功能-----------------------------------
			goBack:function(){
				// uni.navigateTo({
				// 	url: "/pages/dataset/dataset"
				// }); 
				navigateTo('/pages/dataset/dataset',null);
			},
			//------------------------------后退功能-----------------------------------
		}
	}
</script>

<style lang="scss">
page{
	width:100%;
	height: 100%;
	background: #191919;
}
.main{
	width: 100%;
	height: 100%;
	display:flex;
	flex-direction:column;
	.top{
		position:relative;
		width: 100%;
		padding: 36rpx 0rpx;
		background: #252525;
		font-size: 30rpx;
		color: #FFFFFF;
		line-height: 30rpx;
		text-align:center;
		.img{
			position: absolute;
			top: 22rpx;
			left: 28rpx;
			width: 36rpx;
			height: 36rpx;
			padding: 36rpx 0rpx;
			padding: 10rpx;
		}
	}
	.content{
		width: 100%;
		background:#252525;
		margin-top: 60rpx;
		padding:36rpx 0rpx 60rpx 0rpx;
		border-top: 1rpx solid #747474;
		.p1{
			color:#FFFFFF;
			font-size:26rpx;
			line-height: 26rpx;
			padding: 0rpx 36rpx;
		}
		.p2{
			padding: 0rpx 36rpx;
			color: #FFFFFF;
			font-size: 22rpx;
			line-height: 37rpx;
		}
	}
	.search{ // 搜索栏样式
		background-color:#232323;
		padding: 37rpx;
		border-top: 1rpx solid #747474;
		border-bottom: 1rpx solid #747474;
		display:flex;
		.p3{
			color: #FFFFFF;
			font-size: 28rpx;
		}
		.section__title{ //搜索栏中的 input
			color:#FFFFFF;
			font-size: 28rpx;
			border-radius: 10rpx;
			// background-image: url(../../static/pictures/search_1.png);
			// background-repeat: no-repeat; /*设置图片不重复*/
			// background-position: left; /*图片显示的位置*/
			// background-position:12rpx; // 设置图片位置
			padding-left: 15rpx; //设置搜索文字位置
			background-size: 31rpx 31rpx; // 搜索图标的大小
		}
		.placeclass{
			color:#747474;
		}
	}
	.bottom{
		padding: 80rpx 115rpx;
		.p1,.p2{
			font-size: 30rpx;
			color: #747474;
			padding: 22rpx 0rpx;
			text-align: center;
			border: 2rpx solid #747474;
			border-radius:8rpx;
		}
		.p2{
			color: #000000;
			background: #FFD600;
		}
	}
}
</style>
