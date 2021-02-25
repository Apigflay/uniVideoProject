<template>
	<view class="main">
		<view class="top">
			<image class="img" src="../../static/pictures/back_1.png" @click="goBack()"></image>
			{{titleData[tabbarLoginLanguage]}}
		</view>
		<view class="setMoney">
			{{videoChatMoney==0?'':videoChatMoney}}
			<!-- <img class="img" src="../../static/imgs/goldM1.png" alt=""> -->
			<view class="sanjiao1" v-if="sanjiaoStatus" @click="getmsg"></view>
			<view class="sanjiao2" v-else @click="getmsg"></view>
			
		</view>
		<view class="list" v-if="sanjiaoStatus==false">
			<view class="per" v-for="(item,index) in moneyData" :key="index" @click="choose(item)">
				{{item.showPrice}}
			</view>
		</view>
		<view class="bottom" @click="getDelete()">
			<view :class="show == true?'p1':'p2'">{{this.Language.language[this.tabbarLoginLanguage].language173}}</view>
		</view>
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				titleData:['设置视频聊天价格','設置視頻聊天價格','Set video chat price','ตั้งค่าราคาวิดีโอแชท'],
				tabbarLoginLanguage: null, // 用户语言
				tabbarLoginData:null, // 用户初始化页面加载的登录信息
				nickname: null, // 用户呢称
				useridx: null, // 用户IDX
				show: false, // 确认按钮的状态值
				place: '', // 搜索栏默认提示文字
				sanjiaoStatus:true,//下三角
				moneyData:[],//价格表
				videoChatMoney:0,
				
			};
		},
		onLoad:function(){
			this.getLoginlanger(); // 获取语言
			this.getLoginMsg();
			this.getlistData()
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
						console.log(that.tabbarLoginData);
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
			getmsg:function(){//
				if(this.sanjiaoStatus){
					this.sanjiaoStatus=false;
				}else{
					this.sanjiaoStatus=true;
				}
			},
			choose:function(item){
				console.log(item)
				this.sanjiaoStatus=true;
				this.videoChatMoney=item.price;
			},
			getlistData:function(){//获取价格
				var array=base64ToArrayBuffer(encrypt(JSON.stringify({
				})))
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/GetVideoPrice',array)));
				console.log(res)
				if(res.code==100){
					this.moneyData=res.data.list;
				}
			},
			
			//----------------------------确认删除事件---------------------------------
			getDelete:function(){
				var that = this;
				console.log(this.useridx)
				console.log(this.videoChatMoney)
				
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({
						userIdx: this.useridx,
						Money :this.videoChatMoney
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/UpdateAnchorVideoMoney',array)));
					console.log(res)
					if(res.code==100){
						var str1=['设置成功','設置成功','Set up successfully','ตั้งค่าเรียบร้อยแล้ว']
						uni.showToast({
							title: str1[that.tabbarLoginLanguage],
							duration: 1000,
							icon:"none",
						});
					}else{
						var str1=['设置失败','設置失敗','Setup failed','การตั้งค่าล้มเหลว']
						uni.showToast({
							title: str1[that.tabbarLoginLanguage],
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
	// ---
	.setMoney{
		
		line-height: 100rpx;
		text-indent: 20rpx;
		font-size: 36rpx;
		background: #fff;
		width: 590rpx;
		height: 100rpx;
		margin-left:80rpx;
		margin-top: 40px;
		position: relative;
		border-radius: 10rpx;
		.sanjiao1{
			width:0;
			height:0;
			border-left:40rpx solid transparent;
			border-right:40rpx solid transparent;
			border-top:80rpx solid black;
			position: absolute;
			top: 10rpx;
			right: 10rpx;
		}
		.sanjiao2{
			width:0;
			height:0;
			border-left:40rpx solid transparent;
			border-right:40rpx solid transparent;
			border-bottom:80rpx solid black;
			position: absolute;
			top: 10rpx;
			right: 10rpx;
		}
	}
	.list{
		width: 390rpx;
		margin-left: 80rpx;
		.per{
			width: 390rpx;
			height: 100rpx;
			background: #fff;
			border-radius: 10rpx;
			border:2rpx solid #747474;
			line-height: 100rpx;
			text-align:center
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
