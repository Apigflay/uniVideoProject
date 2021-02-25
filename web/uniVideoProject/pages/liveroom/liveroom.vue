<template>
	<view class="liveroom">
		<!-- {{this.$store.getters['liveroom/get_liveInfo']}} -->
		<!-- 底层视频模块 -->
		<!-- src="../../static/promotionalVideo.mp4" src="../../static/promotionalVideo.mp4" src="../../static/promotionalVideo.mp4" src="../../static/promotionalVideo.mp4"  :controls="false" :srcObject="srcObject" -->
		<view class="videoArea">
			<!-- <video ref="video"  class="video" id="my_video" controls="controls" autoplay="autoplay" loop="loop" muted playsinline=""> -->
			<video ref="video" id="my_video" class="video" x5-video-player-type="h5-page" :x5-video-player-fullscreen='true'
			 :webkit-playsinline="true" :playsinline="true" :autoplay="true" :loop="true" objectFit="cover" :muted="true"
			 :controls="false" src="../../static/promotionalVideo.mp4"
			 :show-center-play-btn="false">
				
			</video>
			<!-- <video src="../../staticpromotionalVideo.mp4" controls></video> -->
		</view>
		<!-- 上不显示模块 -->
		<!-- <cover-view class="coverArea"> -->
		<view class="coverArea">

			<!-- 头像 -->
			<view class="PhotoArea">
				<view class="photo">
					<image class="photoimg" @click="goBackPages(1)" :src="nowAchorMsgData.SmallPic" mode=""></image>
					<view class="textInfo">
						<text class="name">{{nowAchorMsgData.MyName}}</text>
						<text class="num">{{roomOnlineNum}}人</text>
					</view>
				</view>
				<image class="closeBtn" @click="goBackPages(2)" src="../../static/liveimgs/close.png" mode=""></image>
			</view>
			<!-- 礼物展示 -->
			<view class="giftShowArea" v-if="roomOnlineGift.length!=0">
				<image class="img" :src="roomOnlineGift[0].iconCartoon" v-if="xitongid==1" mode=""></image>
				<image class="img" :src="roomOnlineGift[0].icon" v-if="xitongid==0" mode=""></image>
			</view>
			<!-- 礼物 -->
			<view class="giftArea" v-show="show_gift">
				<view class="gift" >
					<!-- tab 组件 -->
					<view class="tab">
						<view :class="index==current?'tabActive':'item'" v-for="(item,index) in GiftsData" :key="index" :id="index" @click="changeTab(index)">
							<text class="text">{{item.tabName}}</text>
						</view>
						<image class="closeBtn" @click="goBackPages(5)" src="../../static/liveimgs/close.png" mode=""></image>
					</view>
					<!-- swiper 滑动 -->
					<view class="swiperArea">
						<swiper class="swiper" :current="current" :circular="circular" :indicator-dots="indicatorDots" :autoplay="autoplay" :interval="interval" :duration="duration" @animationfinish="getChangeMsg">
							<swiper-item v-for="(item,index) in GiftsData" :key="index" :id="index">
								<view class="swiper-item uni-bg-green">
									<view class="listPer" :class="acGiftid==item1.giftId?'aclistPer':''" v-for="(item1,index1) in item.list" :key="index1" :id="index1" @click="sendPic(item1.giftId,$event)">
										<view class="gift_img"><image class="photo" :src="item1.icon"></image></view>
										<view class="gift_name">{{item1.name}}</view>
										<view class="gift_num"><image class="img" src="../../static/imgs/goldM1.png"></image><text class="text">{{item1.price}}</text></view>
									</view>
									
								</view>
							</swiper-item>
							
						</swiper>		
					</view>
					<!-- -礼物发送 -->
					<view class="giftSendArea">
						<view class="cashArea">
							<text class="cash">CAT币: </text>
							<text class="num">{{this.$store.getters['AllallLoginInfo'].cash}}</text>
							<text class="pay"  @click="goBackPages(6)">充值</text>
						</view>
						<text class="send" @click="sendGiftBtn">赠送</text>
					</view>
				</view>
			</view>
			<!-- 弹幕 -->
			<view class="chatMsgArea">
				<view class="warntips">
					{{systemMsgTip}}<text class="underline"></text>
				</view>
				<view class="scrollArea">
					<scroll-view class="scrollview" scroll-y>
						<view class="perMsg" v-for="(item,index) in roomOnlineChat" :key="index">
							<view class="per" v-if="item.type==1">
								<text class="name">{{item.usernick}}:</text>
								<text class="str">{{item.content}}</text> 
							</view>
							<view class="per" v-if="item.type==2">
								<text class="name">{{item.usernick}}:</text>
								<text class="str strred">送出一个{{item.giftName}}</text>
							</view>
						</view>
					</scroll-view>
				</view>
			</view>
			<!-- 发送信息 -->
			<view class="sendMsgArea">
				<view class="inputMsg">
					<input class="input" v-model.trim="inputChat" maxlength="20" confirm-type="send" @confirm="sendPublicMsg" type="text" value="" />
					<image class="send" src="../../static/liveimgs/send.png" @click="sendPublicMsg" mode=""></image>
				</view>
				<view class="btnArea">
					<image class="img" v-if="muted" @click="goBackPages(4)" src="../../static/liveimgs/muted.png" mode=""></image>
					<image class="img" v-else @click="goBackPages(4)" src="../../static/liveimgs/source.png" mode=""></image>
					<image class="img img3" @click="goBackPages(3)" src="../../static/liveimgs/gift.png" mode=""></image>
				</view>
			</view>
		<!-- </cover-view> -->
					
		</view>
	</view>
</template>

<script>
	import {sendDSocket,encrypt,decrypt,base64ToArrayBuffer,sendData,navigateTo,productType} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				msgList:[{name:'微笑',str:'je坚实的分水岭杰弗里斯解放了'},
					{name:'微笑',str:'je坚实的分水岭'},
					{name:'微笑',str:'je坚实的分水岭杰弗里斯解放了的分水岭杰弗里斯解放了'},
					{name:'微笑',str:'je坚实'},
					{name:'微笑',str:'je坚实的分水岭杰弗里斯解放了斯解放水岭杰弗里了'},
					{name:'微笑',str:'je坚实的分'},
					{name:'微笑',str:'je坚实的分水岭杰弗里斯解放了'}],
				GiftsData:[],
				NoGiftsData:[],
				show_gift:false,//礼物显示
				// liwu
				indicatorDots: false,//指示点显示
				autoplay: false,//自动播放
				interval: 2000,//间隔
				duration: 500,//动画时长
				circular:true,//衔接
				current:0,//当前activity的滑块
				// ---
				muted:true,//默认静音
				srcObject:null,//no use
				nowAchorMsgData:null,//当前主播信息
				inputChat:'',//消息
				acGiftid:null,//当前选中的礼物id 
				hostType:'0',//host_type 默认是0
				enterLive:false,//是否进入直播画面
				streamMediaUrl:'',
				
			};
		},
		created() {
			this.getAnchorMsg()//获取主播信息
			this.getGiftsMsg()//礼物信息
			// console.log(this.streamMediaUrl)
			// this.streamMediaUrl = this.$store.getters['AllallRoomid'];
			// console.log(this.$store.getters['AllallRoomid'])
		},
		onReady() {
			this.getInitMsg()
		},
		computed:{
			roomOnlineNum(){
				return this.$store.getters['AllallLiveRoomNum']
			},
			roomOnlineChat(){//在线聊天列表
				return this.$store.getters['AllallLiveChatList']
			},
			roomOnlineGift(){//在线礼物展示列表
				console.log(this.$store.getters['AllallLiveGiftList'])
				return this.$store.getters['AllallLiveGiftList']
			},
			xitongid(){
				return productType();
			},
			roomLiveId(){
				console.log('roomid---'+this.$store.getters['AllallRoomid'])
				return this.$store.getters['AllallRoomid'];
			},
			systemMsgTip(){
				return this.$store.getters['AllallSystemMsg'];
			}
		},
		watch:{
			roomOnlineGift:function(){
				console.log('变化')
				
			},
			roomLiveId:function(){
				
				this.getInitMsg()
			}
		},
		methods:{
			getAnchorMsg:function(){
				console.log(this.$store.getters['AllallRoomid'])
				console.log(this.$store.getters['AllallLiveidx'])
				console.log(this.$store.getters['AllallLoginInfo'].useridx)
				var array=base64ToArrayBuffer(encrypt(JSON.stringify({
					UserIdx: this.$store.getters['AllallLoginInfo'].useridx, // int
					AnchorIdx: this.$store.getters['AllallLiveidx'] ,// int 11000057
					// AnchorIdx: 11000036 // int 
				})))
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/GetAnchorInfo',array)));
				console.log(res.data)
				if(res.code==100){
					this.nowAchorMsgData=res.data.Anchor
				}
			},
			getGiftsMsg:function(){//礼物列表  拉取聊天信息
				uni.request({
					url: this.GLOBAL.urlPoint+'/living/GetGiftList' ,//仅为示例，并非真实接口地址。
					method:"GET",
					data: {
					},
					success: (res) => {
						if(JSON.parse(decrypt(res.data)).code==100){
							// console.log(JSON.parse(decrypt(res.data)))
							var jRes =JSON.parse(decrypt(res.data)).data;
							jRes.tabList.forEach(function (value,index) {
								value.list=[]
							});
							jRes.tabList.forEach(function (value1,index1) {
								jRes.giftList.forEach(function (value2,index2) {
										if(value1.tabid==value2.tabid){
											value1.list.push(value2)
										}
								});
									
							});
							this.GiftsData=jRes.tabList;
							this.NoGiftsData=JSON.parse(decrypt(res.data)).data.giftList;
							this.$store.commit("set_allNoGiftList",JSON.parse(decrypt(res.data)).data.giftList)
							// console.log(JSON.parse(decrypt(res.data)).data.giftList)
							// console.log(jRes.tabList)
							// console.log(this.GiftsData)
							
							// console.log(this.NoGiftsData)
							
						}
					}
				});
			},
			changeTab:function(e){ // tab 部分
				this.current = e;
				// console.log(this.current);
			},
			getChangeMsg:function(e){ // swiper 给 tab传值
				this.current = e.detail.current
			},
			sendPic:function(giftId,event){//礼物图片点击
				console.log(event.currentTarget.id)
				// this.show_gift=false;
				this.acGiftid=giftId;
				console.log(giftId)
				var that =this;
				
			
				
				
				// ----------------
				
				// ---------
			},
			sendGiftBtn:function(){
				console.log(this.acGiftid)
				if(this.acGiftid==null){
					uni.showToast({
						title: '请选择礼物',
						duration: 1500,
						icon:"none",
					});
				}else{
					var that =this;
					var array =JSON.stringify({
						"useridx": this.$store.getters['AllallLoginInfo'].useridx,            //当前用户
						"usernick":  this.$store.getters['AllallLoginInfo'].nickname,   //用户昵称
						"statidx": this.$store.getters['AllallLiveidx'],            //主播id
						// "statidx": 11000057,            //主播id
						"attach": {"giftid": this.acGiftid,"num": 1} //{"giftid": 11,"num": 5} //礼物内容 礼物id 礼物数量
					})
					console.log(array)
					uni.onSocketOpen(function (res) {
					  console.log('WebSocket连接已打开！');
					  uni.sendSocketMessage({//31009 赠送礼物（客户端->服务端
					    data: sendDSocket(array,31009),
					    success(res) {
					    },
					    complete(com) {
							that.show_gift=false;
					    	console.log(com)
					    }
					  });
					});
					uni.sendSocketMessage({//31009 赠送礼物（客户端->服务端
					  data: sendDSocket(array,31009),
					  success(res) {
					  },
					  complete(com) {
						that.show_gift=false;
					  	console.log(com)
					  }
					});
				}
				
			},
			goBackPages:function(pageid){ //1 个人  2主页 3礼物 4静音 5 礼物消失 6充值
				if(pageid==1){
					var pageId = 113;
					var obj = encodeURIComponent(encrypt(JSON.stringify({
						AnchorIdx:this.$store.getters['AllallLiveidx'],
						Type:2,
						pageId:pageId
					})))
					navigateTo('/pages/anchorpersonal/anchorpersonal',obj);
				}else if(pageid==2){
					var array =JSON.stringify({
						"useridx": this.$store.getters['AllallLoginInfo'].useridx,            //当前用户
						"statidx": this.$store.getters['AllallLiveidx'],            //主播id
					})
					console.log(array)
					uni.onSocketOpen(function (res) {
					  console.log('WebSocket连接已打开！');
					  uni.sendSocketMessage({//31007 离开直播间（客户端->服务端）
					    data: sendDSocket(array,31007),
					    success(res) {
					    },
					    complete(com) {
					    	console.log(com)
					    }
					  });
					});
					uni.sendSocketMessage({//31007 离开直播间（客户端->服务端）
					  data: sendDSocket(array,31007),
					  success(res) {
					  },
					  complete(com) {
					  	console.log(com)
					  }
					});
					uni.redirectTo({
						url: '/pages/home/home'
					});
					// navigateTo('/pages/home/home',null)
				}else if(pageid==3){
					this.show_gift=true;
				}else if(pageid==4){
					var oUl = document.getElementById("my_video");        //加上它的上级元素，可以避免我们筛选出许多无用的节点出来
					var aLi = oUl.getElementsByTagName("video");
					if(this.muted==true){
						this.muted=false;
						aLi[0].muted=false;
					}else{
						this.muted=true;
						aLi[0].muted=true;
					}
					// this.muted==true?false:true;
				}else if(pageid==5){
					this.show_gift=false;
				}else if(pageid==6){
					// navigateTo('/pages/paymoney/paymoney',null)
				}
			},
			getParameterByName:function(name) {
				name = name.replace(/[\[]/, '\\\[').replace(/[\]]/, '\\\]');
				var regex = new RegExp('[\\?&]' + name + '=([^&#]*)'),
					results = regex.exec(location.search);
				return results === null ? '' : decodeURIComponent(results[1].replace(
					/\+/g, ' '));
			},
			getStream:function(stream) {
				this.enterLive = true;
				console.log('zhing------------')
				  var oUl = document.getElementById("my_video");        //加上它的上级元素，可以避免我们筛选出许多无用的节点出来
				  // console.log(oUl)
				  var aLi = oUl.getElementsByTagName("video");
				  // console.log(aLi)
				  // console.log(stream.mediaStream)
				  aLi[0].srcObject = stream.mediaStream;
				  aLi[0].muted=true;
				  aLi[0].autoplay=true;
					  /*
					  stream.addEventListener('ended', () => { });
					  stream.addEventListener('updated', () => { });
					  */
			},
			getInitMsg:function(){
				console.log('拉流执行------+1')
				window.onbeforeunload = UnInitStream;
				window.onunload = UnInitStream;
				console.log(this.$store.getters['AllallRoomid'])
				// let roomid = this.getParameterByName('roomid');
				let roomid = this.$store.getters['AllallRoomid'];// 5ea12db2c1519b2e4e9c1e1a  5ea13924e7baad2e507a2746
				// console.log(roomid)
				let host_type = this.hostType;
				let streamType = 'mixed';//mixed forward
				
				console.log(host_type)
				InitStream(roomid, this.getStream, function errCB(err) { console.log(err) },host_type,streamType);//streamType
				// var that =this;
				// setTimeout(function(){
				// 	console.log(that.enterLive)
				// 	console.log(12455,that.hostType)
				// 	if(that.enterLive==true){//进入
				// 		
				// 	}else{
				// 		that.hostType = '1';
				// 		that.resetHostType()
				// 	}
				// },3000)
			},
			resetHostType:function(){
				console.log(this.hostType)
				// this.getInitMsg()
			},
			sendPublicMsg:function(event){
				// console.log(event)
				if(this.inputChat!=''){
					var that = this;
					console.log(this.$store.getters['AllallLiveidx'])
					console.log(this.$store.getters['AllallLoginInfo'].useridx)
					console.log(this.inputChat)
					console.log(this.$store.getters['AllallLoginInfo'].nickname)
					var array =JSON.stringify({
						"useridx": this.$store.getters['AllallLoginInfo'].useridx,            //当前用户
						"usernick":  this.$store.getters['AllallLoginInfo'].nickname,   //用户昵称
						"statidx": this.$store.getters['AllallLiveidx'],            //主播id
						// "statidx": 11000057,            //主播id
						"content":  this.inputChat //消息内容
								
					})
					console.log(array)
					uni.onSocketOpen(function (res) {
					  console.log('WebSocket连接已打开！');
					  uni.sendSocketMessage({//31011 公屏文字聊天（客户端->服务端）
					    data: sendDSocket(array,31011),
					    success(res) {
							that.inputChat = '';
					    },
					    complete(com) {
					    	console.log(com)
					    }
					  });
					});
					uni.sendSocketMessage({//31011 公屏文字聊天（客户端->服务端）
					  data: sendDSocket(array,31011),
					  success(res) {
						  that.inputChat = '';
					  },
					  complete(com) {
					  	console.log(com)
					  }
					});
				}else{
					uni.showToast({
						title: '请不要发送空白信息',
						duration: 1000,
						icon:"none",
					});
				}
			},
			showT:function(){
				this.$loading();
				// this.$loading(false);
				console.log(this.$store)
				console.log(this.$store.getters['Allid'])
				
				this.$store.commit("set_id",4444)
				console.log(this.$store.getters['Allid'])
				console.log(this.$store.state)
			},
			noneT:function(){
				// this.$loading();
				this.$loading(false);
			},
			setSex:function(){
				var str =JSON.stringify({
					"useridx": 10000001,
					"toidx": 10000002,
					"status":true
				})
				uni.sendSocketMessage({
				  data: sendDSocket(str,11007)
				});
		
			}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
}
.liveroom{
	width: 100%;
	height: 100%;
	// background:rgba(0,0,0,0.2);
	color: #fff;
	.videoArea{//底层视频模块
		width: 100%;
		height: 100%;
		.video{
			width: 100%;
			height: 100%;
		}
	}
	// ----------
	.coverArea{//上  显示
	// display: none;
		position: absolute;
		top: 0;
		z-index: 1;
		width: 100%;
		height: 100%;
		// background: #747474;
		.PhotoArea{//头像区域
			width: 700rpx;
			height: 80rpx;
			display: flex;
			align-items: center;
			justify-content: space-between;
			padding:0 25rpx;
			margin-top: 30rpx;
			.photo{
				width:270rpx;
				height:80rpx;
				background:rgba(0,0,0,0.3);
				border-radius:40rpx;
				display: flex;
				align-items: center;
				.photoimg{
					width:72rpx;
					height:72rpx;
					background:rgba(255,255,255,1);
					border:2rpx solid rgba(255, 255, 255, 1);
					border-radius:50%;
					margin-left: 2rpx;
				}
				.textInfo{
					width:150rpx;
					height:80rpx;
					color:rgba(255,255,255,1);
					display: flex;
					flex-direction: column;
					margin-left: 5rpx;
					.name{
						height: 40rpx;
						font-size: 26rpx;
						line-height: 40rpx;
						overflow: hidden;
						text-overflow:ellipsis;
						white-space: nowrap;
					}
					.num{
						height: 40rpx;
						font-size: 22rpx;
						line-height: 40rpx;
						overflow: hidden;
						text-overflow:ellipsis;
						
					}
				}
			}
			.closeBtn{
				width: 72rpx;
				height: 72rpx;
			}
		}
		.giftShowArea{//送礼展示区域
			position: absolute;
			z-index: 2;
			right:225rpx;
			// background: #000;
			bottom:650rpx;
			width: 300rpx;
			height:300rpx;
			.img{
				width: 300rpx;
				height:300rpx;
			}
		}
		.giftArea{//礼物区域
			position: absolute;
			z-index: 2;
			right:0rpx;
			background: #000;
			bottom:0rpx;
			width: 100%;
			height:485rpx;//+90
			.gift{
				width: 100%;
				height: 100%;
				// ---
				.swiperArea{
					// padding:0 25rpx;
					height: 305rpx;
					.swiper{
						width: 100%;
						height: 100%;
						.swiper-item{
							height: 100%;
							display: flex;
							justify-content: flex-start;
							align-items: center;
							flex-wrap: wrap;
							overflow-y: scroll;
							.listPer{
								width: 240rpx;
								// height: 160rpx;
								color: #fff;
								margin-top: 20rpx;
								margin-bottom: 8rpx;
								display: flex;
								flex-direction:column;
								align-items: center;
								margin-left: 10rpx;
								.gift_img{ // 礼物图片div大小
									width: 140rpx;
									height: 140rpx;
									.photo{
										width: 140rpx;
										height: 140rpx;
									}
								}
								.gift_name{
									font-size:20rpx;
									color:rgba(116,116,116,1);
									line-height:36rpx;
								}
								.gift_num{
									display: flex;
									justify-content: center;
									align-items: center;
									.img{
										width:20rpx;
										height:20rpx;
										margin-right: 5rpx;
									}
									.text{
										font-size:18rpx;
										font-family:PingFang TC;
										font-weight:400;
										color:rgba(255,255,255,1);
										line-height:36rpx;
									}
								}
							}
							.aclistPer{
								border:1rpx solid #FFD600;
								box-sizing: border-box;
							}
						}				
					}
				}
				.tab{
					width: 660rpx;
					padding-right: 90rpx;
					overflow-x: scroll;
					height: 90rpx;
					background: #191919;
					display:flex;
					align-items:center;
					position: relative;
					.closeBtn{
						position: absolute;
						right: 0;
						top: 0;
						padding: 15rpx;
						width: 60rpx;
						height: 60rpx;
					}
					.item{
						color: #747474;
						font-size: 26rpx;
						font-weight: 400;
						padding:32rpx 40rpx 33rpx 40rpx;
						height: 25rpx;
						line-height: 36rpx;
						white-space:nowrap
					}
					.tabActive{
						color:#FFFFFF;
						font-size: 26rpx;
						font-weight: 400;
						margin:0rpx 40rpx 0rpx 40rpx;
						height: 25rpx;
						line-height: 36rpx;
						white-space:nowrap
					}
				}
				.giftSendArea{
					width: 100%;
					height: 90rpx;
					background:rgba(25,25,25,1);
					display:flex;
					align-items:center;
					justify-content: space-between;
					font-size: 30rpx;
					.cashArea{
						display:flex;
						align-items:center;
						margin-left: 28rpx;
						.num{
							color:#FFD600;
						}
						.pay{
							width:100rpx;
							height:46rpx;
							border:2px solid rgba(255,214,0,1);
							border-radius:8rpx;
							margin-left: 20rpx;
							line-height: 46rpx;
							text-align: center;
						}
					}
					.send{
						width:100rpx;
						height:46rpx;
						background:rgba(255,214,0,1);
						border-radius:8rpx;
						line-height: 46rpx;
						text-align: center;
						margin-right: 28rpx;
					}
				}
				// ---
			}
		}
		.chatMsgArea{//公聊区域
			position: absolute;
			width: 520rpx;
			height: 352rpx;
			left: 25rpx;
			bottom: 125rpx;
			font-size:24rpx;
			display: flex;
			flex-direction: column;
			.warntips{
				height: 64rpx;
				line-height: 32rpx;
				white-space: pre-wrap;
				font-size:24rpx;
				font-weight:400;
				color:rgba(253,106,105,1);
				text-shadow:0px 1px 0px rgba(102,102,102,1);
				.underline{
					text-decoration: underline;
					margin-left: 10rpx;
				}
			}
			.scrollArea{
				height: 288rpx;
				width: 520rpx;
				// flex: 1;
				// display: flex;
				// flex-direction: column;
				.scrollview{
					height: 288rpx;
					width: 520rpx;
					.perMsg{
						margin:6rpx 0;
						line-height: 40rpx;
						white-space: pre-wrap;
						overflow-wrap: break-word;
						word-break: break-all;
						display: flex;
						.per{
							padding-left:20rpx; 
							border-radius:20rpx;
							background:rgba(0,0,0,0.3);
							.name{
								color:rgba(255,214,0,1);
								// margin-left:20rpx;
							}
							.str{
								color:#FFFFFF;
								padding-left:10rpx;
								padding-right:20rpx;
							}
							.strred{
								color:rgba(253,106,105,1);
							}
						}
					}
				}
					
			}
		}
		.sendMsgArea{//发送信息区域
			position: absolute;
			left: 25rpx;
			bottom: 30rpx;
			width:700rpx;
			height:72rpx;
			display: flex;
			align-items: center;
			justify-content: space-between;
			.inputMsg{
				width:320rpx;
				height:72rpx;
				background:rgba(0,0,0,0.3);
				// background: #fff;
				border-radius:36rpx;
				position:relative;
				.input{
					width:250rpx;
					height:72rpx;
					margin-left:36rpx;
					border:0;
					outline: none;
					font-size: 30rpx;
					line-height: 72rpx;
				}
				.send{
					position: absolute;
					height: 72rpx;
					width: 72rpx;
					top: 0;
					right: -82rpx;
				}
			}
			.btnArea{
				width:160rpx;
				.img{
					width:72rpx;
					height: 72rpx;
				}
				.img3{
					margin-left: 16rpx;
				}
			}
		}
	}//上  显示
	// --------------
}
</style>
