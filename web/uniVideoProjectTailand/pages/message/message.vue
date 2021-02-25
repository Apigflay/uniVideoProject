<template>
	<view class="main">
		<view class="nav">
			<image class="back" src="../../static/imgs/more1w.png" mode="" @click="goBack"></image>
			<text class="title">{{this.Language.language[this.tabbarLoginLanguage].language221}}</text>
		</view>
		<view class="content">
			<view class="name">
				<text class="text">{{this.Language.language[this.tabbarLoginLanguage].language221}}</text>
				<!-- <input class="input" v-model="money" type="number" /> -->
				<select class="select" v-model="money">
				  <option :value ="item.price" v-for="(item,index) in chatMsgData" :key="index">{{item.price}}{{goldB[tabbarLoginLanguage]}}</option>
				</select>
			</view>
			<view class="btn" @click="goSubmitMsg">
				{{this.Language.language[this.tabbarLoginLanguage].language99}}
			</view>
		</view>
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,sendD07,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				UserIdx: null, // 用户IDX
				money: null, // 金额
				tabbarLoginLanguage: null, // 获取当前语言
				langer: null, // 设置当前语言
				chatMsgData:[],
				goldB:['金币','金幣','Gold coin','เหรียญทอง'],
				successData:['设置成功','設定成功','Set up successfully','ตั้งค่าเรียบร้อยแล้ว'],
				errData:['设置失败','設定失敗','Setup failed','การตั้งค่าล้มเหลว'],
			};
		},
		onLoad(){
			this.getLoginlanger(); // 获取语言
			this.getLoginMsg();
			this.getChatMData()//获取价格
		},
		methods:{
			getLoginMsg:function(){ // 登录信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.tabbarLoginData = JSON.parse(res.data);
						console.log(that.tabbarLoginData);
						that.UserIdx = that.tabbarLoginData.useridx;
						if(that.tabbarLoginData.isAnchor==false){
							that.isAnchor = false;
						}
					}
				});
			},
			
			getLoginlanger:function(){ // 获取当前语言
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.tabbarLoginLanguage = JSON.parse(res.data);
						// console.log(that.tabbarLoginLanguage);
						// that.langer = that.tabbarLoginLanguage;
						if(that.tabbarLoginLanguage == 0){
							that.langer = 0;
						}else if(that.tabbarLoginLanguage == 1){
							that.langer = 1;
						}else if(that.tabbarLoginLanguage == 2){
							that.langer = 2;
						}else if(that.tabbarLoginLanguage == 3){
							that.langer =3;
						}
					}
				});
				
			},
			
			goBack:function(){
				navigateTo('/pages/revenuemanagement/revenuemanagement',null);
			},
			goSubmitMsg:function(){ // 
				console.log(this.money)
				if(this.money==null){
					
				}else{
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({
						userIdx: this.UserIdx, // int 用户IDX
						Money: this.money, // int 金额
						
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/UpdateAnchorChat',array)));
					console.log(res);
					if(res.code==100){
						
						uni.showToast({
							title: this.successData[this.tabbarLoginLanguage],
							icon:'none',
							duration: 1500,
						});
					}else{
						uni.showToast({
							title: this.errData[this.tabbarLoginLanguage],
							icon:'none',
							duration: 1500,
						});
					}
				}
				
			},
			getChatMData:function(){
				var array=base64ToArrayBuffer(encrypt(JSON.stringify({

				})))
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/GetGetChatPrice',array)));
				if(res.code==100){
					console.log(res.data.list);
					this.chatMsgData = res.data.list;
				}
			}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	background:rgba(25,25,25,1);
}
.main{
	width: 100%;
	height: 100%;
	display: flex;
	flex-direction: column;
	
	.nav{
		position:relative;
		height: 100rpx;
		background:rgba(37,37,37,1);
		display: flex;
		justify-content: space-between;
		align-items: center;
		.back{
			height: 36rpx;
			width: 36rpx;
			padding: 10rpx;
			margin-left: 19rpx;
		}
		.title{
			position:absolute;
			left: 320rpx;
			font-size:30rpx;
			color:rgba(255,255,255,1);
		}
		.tip{
			padding: 10rpx;
			margin-right: 19rpx;
			font-size:30rpx;
			color:rgba(255,214,0,1);
		}
	}
	
	.content{
		margin-top:150rpx;
		.name{
			width: 690rpx;
			height: 54rpx;
			padding-top:28rpx;
			margin-left: 30rpx;
			display: flex;
			.text{
				font-size:30rpx;
				color:rgba(172,172,172,1);
				margin-right:28rpx;
			}
			.input{
				font-size:26rpx;
				width:520rpx;
				height:54rpx;
				color:rgba(255,255,255,1);
				background:rgba(52,52,52,1);
				border-radius:8rpx;
				text-indent: 30rpx;
			}
		}
		.btn{
			width:520rpx;
			height:72rpx;
			background:rgba(255,214,0,1);
			border-radius:8rpx;
			margin-top: 160rpx;
			margin-bottom: 140rpx;
			margin-left:115rpx;
			font-size:30rpx;
			color:rgba(0,0,0,1);
			text-align: center;
			font-weight:600;
			line-height: 72rpx;
		}
	}
}
</style>
