<template>
	<view class="content">
		<sy-video :videoUri="videoUri1" style="width: 100%;"></sy-video>
		<sy-video :videoUri="videoUri2" style="width: 100%;"></sy-video>
		<!-- <video class="course_image" id="myVideo" :src="videoUri" controls v-if="videoUri">
			<cover-view class="video-control">
				<cover-view class="multi rate" @tap="showSwitchRate">x {{ currentRate }}</cover-view>
			</cover-view>
			<cover-view class="multi-list rate" :class="{ active: rateShow }">
				<cover-view
					v-for="(item, index) in ['0.5', '0.8', '1.0', '1.25', '1.5']"
					:key="index"
					class="multi-item rate"
					:data-rate="item"
					@tap="switchRate"
					:class="{ active: item == currentRate }"
				>
					{{ item }}
				</cover-view>
			</cover-view>
		</video> -->
	</view>
</template>

<script>
	// import video from "../../lib/tools/video/video.js";
	// require("../../lib/tools/video/video.js")
	import SyVideo from '@/components/shoyu-video/shoyu-video';
	export default {
		components: { SyVideo },
		data() {
			return {
				tabbarLoginLanguage: null, // 用户语言
				style:"",
				title: '视频倍速播放',
				videoUri1: 'http://vjs.zencdn.net/v/oceans.mp4',
				videoUri2: 'https://img.mycat1314.com/cut/352/F76896933DA6CFAD8420D83E5320E665.m3u8',
				rateShow: false,
				currentRate: 1.0
			};
		},
		onLoad() {//监听页面加载，其参数为上个页面传递的数据，参数类型为Object
			// console.log("页面加载")
			// this.getSwiperData()
			this.getLoginlanger(); // 获取语言
		},
		onShow(){//监听页面显示。页面每次出现在屏幕上都触发，包括从下级页面点返回露出当前页面	
			// console.log("页面显示")
		},
		onReady(){//监听页面初次渲染完成。注意如果渲染速度快，会在页面进入动画完成前触发
			// console.log("初次渲染完成")
			// this.goCreatVideo()
			this.initVideo()
		},
		onHide(){//监听页面隐藏
			// console.log("页面隐藏")
		},
		onUnload(){//监听页面卸载
			// console.log("页面卸载")
		},
		onPullDownRefresh(){
			// console.log("页面下拉")
		},
		onReachBottom(){
			// console.log("页面上啦")
		},
		onPageScroll(){

			if(document.documentElement.scrollTop>=100){
				this.style="background:#000;color:#fff"
			}else{
				this.style=""
			}
		},
		methods:{
			showSwitchRate(rate) {
				let that = this;
				console.log('rateShow');
				that.rateShow = true;
			},
			switchRate(e) {
				let that = this;
				let rate = Number(e.currentTarget.dataset.rate);
				that.currentRate = rate;
				that.rateShow = false;
				this.videoContext.playbackRate(rate);
			},
			initVideo() {
				//初始化视频方法
				
				
				
				
				// console.log(this.$video)
				// let myPlayer = this.$video(myVideo, {
				// 	//确定播放器是否具有用户可以与之交互的控件。没有控件，启动视频播放的唯一方法是使用autoplay属性或通过Player API。
				// 	controls: true,
				// 	//自动播放属性,muted:静音播放
				// 	autoplay: "muted",
				// 	//建议浏览器是否应在<video>加载元素后立即开始下载视频数据。
				// 	preload: "auto",
				// 	//设置视频播放器的显示宽度（以像素为单位）
				// 	width: "800px",
				// 	//设置视频播放器的显示高度（以像素为单位）
				// 	height: "400px"
				// });
				// console.log(myPlayer)
				
				// var player = videojs('example_video_1',{
				// 	muted: true,
				// 	controls : true,      
				// 	height:300, 
				// 	width:300,
				// 	loop : true,
				// 	// 更多配置.....
				// });
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
			goCreatVideo:function(){//创造视频播放器
				
				// var div = document.getElementById("box");
				// console.log(div)
			
				// console.log(video)
				// console.log("视频初始化")
				// console.log(that)
				// console.log(that.topVideoSrc)
				// console.log(document.getElementById("video"))
			// 
				// const player = new QPlayer({
				// 	url: 'http://demo-videos.qnsdk.com/movies/qiniu.mp4',
				// 	container: document.getElementById("video"),
				// 	autoplay: video.browser.versions.ios ? true : false,
				// 	// isLive: false,
				// 	loggerLevel: 2,
				// 	defaultViewConfig: {
				// 		noControls: true
				// 	}
				// });
				// // 
				// console.log(player)
			},
		}
	}
</script>

<style lang="scss">
.content{
	.header{
		position: fixed;
		top: 0;
		left: 0;
		height: 60px;
		width: 100%;
		background: #cecece;
	}
	.box{
		height: 2083.33rpx;
		width: 100%;
	}
	
}
</style>
