<template>
	<view class="liveroom">
		<!-- {{this.$store.getters['liveroom/get_liveInfo']}}      -->
		<view class="videoArea">
			<video ref="video" id="my_video" class="video" x5-video-player-type="h5-page" :x5-video-player-fullscreen='true'
			 :webkit-playsinline="true" :playsinline="true" :autoplay="true" :loop="true" objectFit="cover" :muted="true"
			  :controls="false" :src="backstreamMediaUrl"
			 :show-center-play-btn="false" @timeupdate='timeupdateFun'>
			<!-- <video ref="video" id="my_video" class="video" x5-video-player-type="h5-page" :x5-video-player-fullscreen='true'
			 :webkit-playsinline="true" :playsinline="true" :autoplay="true" :loop="true" objectFit="cover" muted="muted"
			  :controls="false" :src="videoReUrl"
			 :show-center-play-btn="false"> -->
				<cover-view class="coverArea">
					<!-- 头像 -->
					<view class="PhotoArea">
						<view class="left"></view>
						<image class="closeBtn" @click="goBackPages(2)" src="../../static/liveimgs/close.png" mode=""></image>
					</view>
					<!-- 发送信息 -->
					<view class="sendMsgArea">
							<view class="playArea btnArea">
								<image class="img" v-if="play" @click="setplay" src="../../static/liveimgs/play.png" mode=""></image>
								<image class="img" v-else @click="setpause" src="../../static/liveimgs/pouse.png" mode=""></image>
							</view>
							<view class="soundnum btnArea">
								{{parseInt(parseInt(nowlong.currentTime)/60)}}:{{parseInt(nowlong.currentTime)%60<10?'0'+parseInt(nowlong.currentTime)%60:parseInt(nowlong.currentTime)%60}}/{{parseInt(parseInt(nowlong.duration)/60)}}:{{parseInt(nowlong.duration)%60<10?'0'+parseInt(nowlong.duration)%60:parseInt(nowlong.duration)%60}}
							</view>
							<view class="soundArea btnArea">
								<image class="img" v-if="muted" @click="goBackPages(4)" src="../../static/liveimgs/muted.png" mode=""></image>
								<image class="img" v-else @click="goBackPages(4)" src="../../static/liveimgs/source.png" mode=""></image>
							</view>
							
					</view>
					<view class="progressArea">
						  <slider :value="parseInt(nowlong.currentTime/nowlong.duration*100)" @change="sliderChange" activeColor="blue" backgroundColor="red" block-color="#8A6DE9" block-size="20" />
						<!-- <view class="progress" :style="progressStyle"></view> -->
					</view>
					
					

				</cover-view>
			</video>
		</view>
		<!-- 上不显示模块 -->
		
	</view>
</template>

<script>
	import {sendDSocket,encrypt,decrypt,base64ToArrayBuffer,sendData,navigateTo,productType} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				
				// liwu
				indicatorDots: false,//指示点显示
				autoplay: false,//自动播放
				interval: 2000,//间隔
				duration: 500,//动画时长
				circular:true,//衔接
				current:0,//当前activity的滑块
				// ---
				muted:true,//默认静音
				play:true,//默认播放
				nowlong:{currentTime: 0, duration: 0},
				progressStyle:'',//进度
				backstreamMediaUrl:'',
			};
		},
		created() {
			// this.getAnchorMsg()//获取主播信息
			// this.getGiftsMsg()//礼物信息
			this.backstreamMediaUrl = this.$store.getters['AllallLiveRevideoUrl'];
			
		},
		onReady() {
			this.creatVideoUrl()
			this.videoContext = uni.createVideoContext('my_video')
		},
		computed:{
			videoReUrl(){
				return this.$store.getters['AllallLiveRevideoUrl']
			}
			
		},
		watch:{
			videoReUrl:function(){
				console.log('变化')
				
			}
		},
		methods:{
			goBackPages:function(pageid){ //1 个人  2主页 3礼物 4静音 5 礼物消失
				if(pageid==2){
					// navigateTo('/pages/home/home',null)
					uni.redirectTo({
						url: '/pages/home/home'
					});
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
				}
			},
			creatVideoUrl:function(){//视频播放
				console.log(this.videoReUrl)
				 var oUl = document.getElementById("my_video");        //加上它的上级元素，可以避免我们筛选出许多无用的节点出来
				console.log(oUl)
				var aLi = oUl.getElementsByTagName("video");
				console.log(aLi)
				aLi[0].src = this.videoReUrl;
				aLi[0].muted=true;
				aLi[0].autoplay=true;
				console.log(aLi)
				
				// -----
			},
			timeupdateFun:function(event){
				// console.log('进度刷新',event.detail)
				this.nowlong = event.detail;
				var long = event.detail.currentTime/event.detail.duration*700;
				this.progressStyle = 'width:'+long+'rpx;'
			},
			setplay:function(){
				this.play = false;
				this.videoContext.pause()
				
			},
			setpause:function(){
				this.play = true;
				this.videoContext.play()
			},
			sliderChange(e) {
				// console.log('value 发生变化：' + e.detail.value)
				this.videoContext.seek(e.detail.value/100*this.nowlong.duration)
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
			.coverArea{//上  显示
			// display: none;
				position: relative;
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
					.closeBtn{
						width: 72rpx;
						height: 72rpx;
					}
				}
				.sendMsgArea{//发送信息区域
					position: absolute;
					left: 25rpx;
					bottom: 102rpx;
					width:700rpx;
					height:72rpx;
					display: flex;
					align-items: center;
					justify-content: space-between;
					color: #000;
					.btnArea{
						width:72rpx;
						height: 72rpx;
						// background:rgba(0,0,0,0.3);
						// border-radius:36rpx;
						line-height: 72rpx;
						vertical-align: top;
						.img{
							width:72rpx;
							height: 72rpx;
						}
					}
					.soundnum{
						margin-left: -460rpx;
					}
				}
				.progressArea{
					position: absolute;
					left: 25rpx;
					bottom: 30rpx;
					width:700rpx;
					height:72rpx;
					// background: red;
					// display: flex;
					.progress{
						height:10rpx;
						background: blue;
					}
				}
			}//上  显示
		}
	}
	
}
</style>
