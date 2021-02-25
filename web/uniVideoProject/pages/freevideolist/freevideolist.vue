<template>
	<view class="main">
		<view class="top">
			<image class="img" src="../../static/pictures/back_1.png" @click="goback()"></image>
			<text>限時免費</text>
			<!-- {{ModelName}} -->
		</view>
		<scroll-view :scroll-y="true" @scrolltolower="getNewmsg" class="content">
			<view v-for="(item,index) in model1Data" :id="index" :key="index" class="list">
				<image class="bgimg" :src="item.BackgroundPicUrl" @click="goVideo(item)"></image>
				<image class="photo" :src="item.SmallPic" @click="goAction(item)"></image>
				<view v-if="item.Online" class="status"></view>
				<view class="cont">{{item.Content}}</view>
			</view>
		</scroll-view>
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,sendD07,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				tabbarLoginLanguage: null, // 用户语言
				pageId: 9, // 页面ID 视频页面回退时使用
				title: null, // 页面标题
				type: null, // 页面类型
				tabbarLoginData: null, // 页面加载获取到的用户信息
				UserIdx: null, // 用户IDX
				Page: 1, // 页数
				likeid: null, // 喜好   喜好 0-女性 1-男性 2-所有 没有喜好默认喜好女性 传0
				model1Data: null, // 加载页面获取的数据
				model2Data: null, // 滚动加载页面获取的数据
				ModelName: null, // 标签类名
			};
		},
		onLoad:function(obj){
			// var option=JSON.parse(decrypt(decodeURIComponent(obj.action)))
			
			// console.log(JSON.parse(decrypt(decodeURIComponent(obj.action))))
			

			
			// this.title = option.title;
			// this.type = option.type;
			// this.ModelName =option.ModelName;
			this.getLoginlanger(); // 获取语言
			this.getLoginMsg();
		},
		onShow(){//监听页面显示。页面每次出现在屏幕上都触发，包括从下级页面点返回露出当前页面	
			
			// window.location.href=""
			// console.log("页面显示")
		},
		onReady(){//监听页面初次渲染完成。注意如果渲染速度快，会在页面进入动画完成前触发
			// console.log(window.location)
			// console.log(window.location.search)
			// console.log(window.location)
			// window.location.hash='#/pages/homevideolist/homevideolist'
			// console.log("初次渲染完成")
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
			},
			// --------------------------页面初始加载用户数据-----------------------------------------
			getLoginMsg:function(){ //登录信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.tabbarLoginData = JSON.parse(res.data);
						console.log(that.tabbarLoginData);
						that.loginMsgData = that.tabbarLoginData;
						that.UserIdx = that.tabbarLoginData.useridx;
						that.likeid = that.tabbarLoginData.sex;
						if(that.tabbarLoginData.isAnchor==false){
							that.isAnchor = false;
						}
						that.getInitMsg();
					}
				});
			},
			// --------------------------页面初始加载用户数据-----------------------------------------
			
			//----------------------------页面首次加载的数据------------------------------------------
			getInitMsg:function(e){ //页面初始化加载的数据
				var array=base64ToArrayBuffer(encrypt(JSON.stringify({
					UserIdx:this.UserIdx,//	int
					Page:this.Page,//	int
					
				})))
				// type: 0,
				// useridx: this.UserIdx,
				// page: this.Page,
				// limit: 20,
				// likeid: this.likeid
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/GetFreeResource',array)));
				console.log(res);
				if(res.code==100){
					console.log(res);
					this.model1Data=res.data;
					console.log(this.model1Data)
				}
			},
			//----------------------------页面首次加载的数据------------------------------------------
			
			//-----------------------------视频播放--------------------------------------------------
			goVideo:function(e){
				// console.log(e);
				// uni.navigateTo({
				// 	url: "/pages/video/video?AnchorIdx="+ e.UserIdx + "&ResourceoId=" + e.RescourceId + "&pageId=" + this.pageId
				// })
				var obj = encodeURIComponent(encrypt(JSON.stringify({
					AnchorIdx:e.UserIdx,
					ResourceoId:e.RescourceId,
					pageId:this.pageId
				})))
				navigateTo('/pages/video/video',obj);
			},
			//-----------------------------视频播放--------------------------------------------------
			
			//-----------------------------主播主页--------------------------------------------------
			goAction:function(e){
				// uni.navigateTo({
				// 	url: "/pages/anchorpersonal/anchorpersonal?AnchorIdx=" + e.UserIdx + "&Type=" + 2 + "&pageId=" + this.pageId
				// })
				var obj = encodeURIComponent(encrypt(JSON.stringify({
					AnchorIdx:e.UserIdx,
					Type:2,
					pageId:this.pageId
				})))
				navigateTo('/pages/anchorpersonal/anchorpersonal',obj);
			},
			//-----------------------------主播主页--------------------------------------------------
			
			//----------------------------滚动加载---------------------------------------------------
			getNewmsg:function(){
				this.Page++;
				var array=base64ToArrayBuffer(encrypt(JSON.stringify({
					UserIdx:this.UserIdx,//	int
					Page:this.Page,//	int
					
				})))
				// type: this.type,
				// useridx: this.UserIdx,
				// page: this.Page,
				// limit: 20,
				// likeid: this.likeid
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/GetFreeResource',array)));
				if(res.code==100){
					console.log(res);
					this.model2Data=res.data;
					this.model1Data.push.apply(this.model1Data,this.model2Data);
				}
			},
			//----------------------------滚动加载---------------------------------------------------
			
			//------------------------------回退-----------------------------------------------------
			goback:function(){
				// uni.navigateTo({
				// 	url: "/pages/home/home"
				// })
				navigateTo('/pages/home/home',null);
			},
			//------------------------------回退-----------------------------------------------------
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	// background:rgba(35,35,35,1);#191919
	background:#191919;
}
.main{
	width:100%;
	height: 100%;
	display:flex;
	flex-direction: column;
	.top{
		position: fixed;
		z-index: 999;
		top:0rpx;
		left:0rpx;
		width:100%;
		padding: 32rpx 0rpx;
		background-color:#252525;
		text-align: center;
		font-size: 30rpx;
		color:#FFFFFF;
		.img{
			position: absolute;
			left: 10rpx;
			width: 36rpx;
			height: 36rpx;
			padding: 10rpx;
		}
	}
	.content{
		margin-top: 100rpx;
		height:100%;
		.list{
			float: left;
			margin-top: 28rpx;
			margin-left: 28rpx;
			position: relative;
			width:333rpx;
			height: 466rpx;
			border-radius:8rpx;
			background: #2C405A;
			.bgimg{
				width:333rpx;
				height: 466rpx;
			}
			.photo{
				position:absolute;
				top:10rpx;
				left: 10rpx;
				width: 60rpx;
				height: 60rpx;
				border-radius: 50%;
				background: #232323;
				border:2rpx solid #FFFFFF;
			}
			.status{
				position:absolute;
				top:51rpx;
				left: 51rpx;
				width:20rpx;
				height: 20rpx;
				border-radius: 50%;
				background: #17FF2A;
			}
			.cont{
				position: absolute;
				width:309rpx;
				height:80rpx;
				bottom: 9rpx;
				left: 12rpx;
				font-size:28rpx;
				font-weight:400;
				color:#FFFFFF;
				line-height:40rpx;
				display: -webkit-box;
				-webkit-box-orient: vertical;
				-webkit-line-clamp: 2;
				overflow: hidden;
			}
		}
	}
}
</style>
