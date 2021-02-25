<template>
	<view class="main">
		<view class="top">
			<image class="img" src="../../static/pictures/back_1.png" @click="goback()"></image>
			<text>{{ModelName}}</text>
		</view>
		<scroll-view :scroll-y="true" @scrolltolower="getNewmsg" class="content">
			<view v-for="(item,index) in model1Data" :id="index" :key="index" class="list">
				<image class="bgimg" :src="item.BackgroundPicUrl" @click="goVideo(item)"></image>
				<image class="photo" :src="item.SmallPic" @click="goAction(item)"></image>
				<view v-if="item.Online" class="status"></view>
				<view class="cont">{{item.Content}}</view>
			</view>
		</scroll-view>
		<!-- 引导遮罩层 -->
		<view class="marskisLock" v-if="islockA"></view>
				<!-- 直播间确认输入密码 -->
		<view class="liveRoomArea" v-if="isLiveRoom">
			<view class="roomTitle">谁谁谁的直播间</view>
			<view class="roomInput">
				<text class="text">房间密码</text>
				<input class="input" v-model="liveRoomPass" type="text" value="" />
			</view>
			<view class="roomBtnArea">
				<button class="sureBtn" type="primary" @click="goLivePages">进入</button>
				<button class="cancleBtn" type="primary" @click="hideLiveRoomArea">取消</button>
			</view>
		</view>
	</view>
</template>

<script>
	import {sendDSocket,encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,sendD07,navigateTo,socketarray} from "../../lib/js/GlobalFunction.js"
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
				islockA:false,//引导用户弹框
				isLiveRoom:false,//直播间是否需要密码
				liveRoomPass:'',//密码
				liveplaybackId:null,//点击的modelid
				liveAchorData:null,//点击的用户信息
				liveVideoType:null,//直播 回播 类型判断
				countTotal:null,//总数
			};
		},
		onLoad:function(obj){
			var option=JSON.parse(decrypt(decodeURIComponent(obj.action)))
			console.log(JSON.parse(decrypt(decodeURIComponent(obj.action))))
			this.title = option.title;
			this.type = option.type;
			this.ModelName =option.ModelName;
			this.getLoginlanger(); // 获取语言
			this.getLoginMsg();
		},
		onShow(){//监听页面显示。页面每次出现在屏幕上都触发，包括从下级页面点返回露出当前页面	
		},
		onReady(){//监听页面初次渲染完成。注意如果渲染速度快，会在页面进入动画完成前触发
			console.log(window.location)
		},
		methods:{
			goLivePages:function(){//直播间
				console.log(this.liveRoomPass)
				console.log(this.$store.getters['AllallLoginInfo'].useridx)
				console.log(this.liveplaybackId)
				console.log(this.$store.getters['AllallLiveidx'])
				console.log(this.liveVideoType)
				if(this.liveVideoType==1){//回放地址
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({
						useridx:this.$store.getters['AllallLoginInfo'].useridx,//	是	int	用户名
						playbackId:this.liveplaybackId,//	是	int	回放id
						password:JSON.stringify(this.liveRoomPass),//	否	string	密码
						type:this.liveVideoType,//	是	int	-0-直播流地址 1-回放流地址
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/UserInfo/getPlayBackLiveUrl',array)));
					if(res.code==100){
						this.$store.commit("set_allLiveRevideoUrl",res.data)
						navigateTo('/pages/livereview/livereview',null);
					}else{
						uni.showToast({
							title:res.msg,
							duration: 1000,
							icon:"none",
						});
					}
				}else if(this.liveVideoType==0){//直播
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({
						useridx:this.$store.getters['AllallLoginInfo'].useridx,//	是	int	用户名
						playbackId:this.liveplaybackId,//	是	int	回放id
						password:JSON.stringify(this.liveRoomPass),//	否	string	密码
						type:this.liveVideoType,//	是	int	-0-直播流地址 1-回放流地址
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/UserInfo/getPlayBackLiveUrl',array)));
					if(res.code==100){
						this.$store.commit("set_allRoomid",res.data)
						navigateTo('/pages/liveroom/liveroom',null);
						var socketarray = JSON.stringify({
							"useridx": this.$store.getters['AllallLoginInfo'].useridx,//当前用户
							// "statidx":  11000057,//主播id
							"statidx":  this.$store.getters['AllallLiveidx'],//主播id
							})
						console.log(socketarray)
						uni.onSocketOpen(function (res) {
						  console.log('WebSocket连接已打开！');
						  uni.sendSocketMessage({//31005 进入直播间（客户端->服务端）
						    data: sendDSocket(socketarray,31005),
						    success(res) {},
						    complete(com) {
						    	console.log(com)
						    }
						  });
						});
						uni.sendSocketMessage({//31005 进入直播间（客户端->服务端）
						  data: sendDSocket(socketarray,31005),
						  success(res) {
						  },
						  complete(com) {
						  	console.log(com)
						  }
						});
						
					}else{
						uni.showToast({
							title:res.msg,
							duration: 1000,
							icon:"none",
						});
					}
				}
			},
			hideLiveRoomArea:function(){
				this.isLiveRoom=false;
				this.marskShow=false;
			},
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
					type: this.type,
					useridx: this.UserIdx,
					page: this.Page,
					limit: 20,
					likeid: this.likeid
				})))
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/UserInfo/HomeList',array)));
				if(res.code==100){
					console.log(res);
					this.countTotal=res.data.count;
					this.model1Data=res.data.list;
					console.log(this.model1Data)
				}
			},
			//----------------------------页面首次加载的数据------------------------------------------
			
			//-----------------------------视频播放--------------------------------------------------
			goVideo:function(e){
				console.log(e)
				console.log(this.type)
				
				this.liveplaybackId=e.ModuleId;
				this.$store.commit("set_allLiveidx",e.UserIdx)
				if(this.type==22){//回放
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({						
						useridx:this.tabbarLoginData.useridx,//	是	int	当前用户
						playbackId:e.ModuleId,//	是	int	回放id
						type:1,	//是	int	type -0-直播流 1-回放流地址
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/getLiveUrlToUser',array)));
					console.log(res)
					if(res.code==100){
						this.$store.commit("set_allLiveRevideoUrl",res.data);//
						navigateTo('/pages/livereview/livereview',null);
					}else if(res.code==101){//需要密码
						this.isLiveRoom=true;
						this.marskShow=true;
						this.liveVideoType = 1;
					}
				}else if(this.type==23){//直播
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({						
						useridx:this.tabbarLoginData.useridx,//	是	int	当前用户
						playbackId:e.ModuleId,//	是	int	回放id
						type:0,	//是	int	type -0-直播流 1-回放流地址
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/getLiveUrlToUser',array)));
					console.log(res)
					if(res.code==100){
						this.$store.commit("set_allRoomid",res.data)
						navigateTo('/pages/liveroom/liveroom',null);
						var socketarray = JSON.stringify({
							"useridx": this.tabbarLoginData.useridx,//当前用户
							// "statidx":  11000057,//主播id
							"statidx":  e.UserIdx,//主播id
							})
							console.log(socketarray)
						uni.onSocketOpen(function (res) {
						  console.log('WebSocket连接已打开！');
						  uni.sendSocketMessage({//31005  进入直播间（客户端->服务端）
						    data: sendDSocket(socketarray,31005),
						    success(res) {},
						    complete(com) {
						    	console.log(com)
						    }
						  });
						});
						uni.sendSocketMessage({//31005  进入直播间（客户端->服务端）
						  data: sendDSocket(socketarray,31005),
						  success(res) {
						  },
						  complete(com) {
						  	console.log(com)
						  }
						});
					}else if(res.code==101){//需要密码
						this.isLiveRoom=true;
						this.marskShow=true;
						this.liveVideoType = 0;
					}
				}else{
					var obj = encodeURIComponent(encrypt(JSON.stringify({
						AnchorIdx:e.UserIdx,
						ResourceoId:e.RescourceId,
						pageId:this.pageId
					})))
					navigateTo('/pages/video/video',obj);
				}
				
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
				console.log(this.Page,this.countTotal,this.Page*20<this.countTotal);
				if(this.Page*20<this.countTotal){//大于当前展示继续请求
					this.Page++;
					var array=base64ToArrayBuffer(encrypt(JSON.stringify({
						type: this.type,
						useridx: this.UserIdx,
						page: this.Page,
						limit: 20,
						likeid: this.likeid
					})))
					var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/UserInfo/HomeList',array)));
					if(res.code==100){
						console.log(res);
						this.countTotal=res.data.count;
						this.model2Data=res.data.list;
						this.model1Data.push.apply(this.model1Data,this.model2Data);
					}
				}else{//
					
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
	position: relative;
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
	.marskisLock{//
		position: fixed;
		top: 0;
		left: 0;
		z-index: 900;
		width: 100%;
		height: 100%;
		background:rgba(0,0,0,0.8);
	}
	// 直播间确认
	.liveRoomArea{
		width:574rpx;
		height:300rpx;
		background:rgba(58,58,58,1);
		border-radius:8rpx;
		position: fixed;
		top: 476rpx;
		left: 74rpx;
		z-index: 901;
		padding-left: 28rpx;
		// background: #17FF2A;
		.roomTitle{
			height: 30rpx;
			line-height: 30rpx;
			margin-top:30rpx;
			font-size:30rpx;
			font-weight:500;
			color:rgba(255,214,0,1);
		}
		.roomInput{
			width: 100%;
			height: 62rpx;
			margin-top: 47rpx;
			display: flex;
			align-items: center;
			.text{
				font-size:30rpx;
				font-weight:400;
				color:rgba(255,255,255,1);
			}
			.input{
				margin-left: 18rpx;
				width:395rpx;
				height:62rpx;
				background:rgba(35,35,35,1);
				border-radius:8rpx;
				border: 0;
				outline: none;
				font-size:30rpx;
				font-weight:500;
				color:rgba(169,169,169,1);
				text-indent: 24rpx;
			}
		}
		.roomBtnArea{
			height: 62rpx;
			display: flex;
			margin-top: 43rpx;
			font-size:30rpx;
			font-weight:500;
			color:rgba(255,255,255,1);
			border-radius:8rpx;
			.sureBtn{
				width:231rpx;
				height:62rpx;
				background:rgba(255,214,0,1);
				margin-left: 24rpx;
				line-height: 62rpx;
			}
			.cancleBtn{
				width:231rpx;
				height:62rpx;
				background:rgba(125,125,125,1);
				line-height: 62rpx;
				margin-left: 36rpx;	
			}
		}
	}
}
</style>
