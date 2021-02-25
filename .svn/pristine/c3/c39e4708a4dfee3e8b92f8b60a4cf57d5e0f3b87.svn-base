<template>
	<view class="content">
		<!-- navBar -->
		<view class="navBar" :style="style">
			<view class="logo">
				<image class="img_logo1" src="../../static/imgs/zuan2.png" mode=""></image>
				<image class="img_logo2" src="../../static/imgs/zuan2.png" mode=""></image>
			</view>
			<view class="text">
				<text class="text_cn">首页</text>
				<text class="text_en">Happy Your</text>
			</view>
			<view class="num">
				<image class="num_img" src="../../static/imgs/zuan2.png" mode=""></image>
				<text class="num_text">30</text>
			</view>
		</view>
		<!-- videoArea -->
		<view class="videoArea">
			<view class="video">
				<video class="promotionalVideo" :autoplay="true" :loop="true" objectFit="cover" :controls="false" src="../../static/promotionalVideo.mp4" ></video>
			</view>
			<view class="videoMessageArea">
				<view class="photoArea">
					<image class="photo" src="../../static/imgs/zuan2.png" mode=""></image>
				</view>
				<view class="nameArea">
					With my friend
				</view>
				<view class="lockArea">
					<view class="locked">
						<image class="img" src="../../static/imgs/lock1w.png" mode=""></image>
						<view class="text">
							196
						</view>
					</view>
					<view class="btn">
						<text class="text">解锁</text>
						<text class="text text2">|</text>
						<image class="img" src="../../static/imgs/zuan2.png" mode=""></image>
						<text class="text text3">240</text>
					</view>
					<view class="prise">
						<image class="img" src="../../static/imgs/zan1w.png" mode=""></image>
						<view class="text">
							100%
						</view>
					</view>
				</view>
				
			</view>
		</view>
		<!-- popularArea -->
		<view class="popularArea">
			<view class="popular_title">
				<view class="left">
					<image class="img" src="../../static/imgs/more2.png" mode=""></image>
					<text class="text">大家都在看</text>
				</view>
				<view class="right">
					<text class="text">更多</text>
					<image class="img" src="../../static/imgs/more2.png" mode=""></image>
				</view>
			</view>
			<view class="scrollArea">
				<view class="scroll">
					<view class="item" v-for="(item,index) in 6" :key="index">
						<image class="bg" src="../../static/imgs/19032400.jpg" mode=""></image>
						<image class="photo" src="" mode=""></image>
						<view class="status"></view>
						<view class="text">
							他他他他他他他他他他他他他他他他他他
						</view>
					</view>
				</view>
			</view>
		</view>
		<!-- worldList -->
		<view class="worldList">
			<view class="world_title">
				<view class="left">
					<image class="img" src="../../static/imgs/more2.png" mode=""></image>
					<text class="text">大家都在看</text>
				</view>
				<view class="right">
					<text class="text">更多</text>
					<image class="img" src="../../static/imgs/more2.png" mode=""></image>
				</view>
			</view>
			<!--  -->
			<!-- tab -->
			<view class="tab">
				<view :class="index==current?'tabActive':''" class="item" v-for="(item,index) in tabData" :key="index" :id="index" @click="changeTab">
					<image class="img" :src="item.imgsrcW" mode="" v-if="index==current"></image>
					<image class="img" :src="item.imgsrcB" mode="" v-if="index!=current"></image>
					<text class="text">{{item.text}}</text>
				</view>
			</view>
			<!-- swiper -->
			<view class="swiperArea">
				<swiper class="swiper" :current="current" :circular="circular" :indicator-dots="indicatorDots" :autoplay="autoplay" :interval="interval" :duration="duration" @animationfinish="getChangeMsg">
					<swiper-item>
						<view class="swiper-item uni-bg-red">
							<view class="listPer" v-for="(item,index) in listNum" :key="index">
								<image class="img" :src="item.imgSrc" mode=""></image>
								<view class="text">
									{{item.text}}
								</view>
							</view>
						</view>
					</swiper-item>
					<swiper-item>
						<view class="swiper-item uni-bg-green">BBBBBB</view>
					</swiper-item>
					<swiper-item>
						<view class="swiper-item uni-bg-blue">CCCCC</view>
					</swiper-item>
					<swiper-item>
						<view class="swiper-item uni-bg-black">DDDDD</view>
					</swiper-item>
				</swiper>		
			</view>
			<!--  -->
		</view>
		<!-- bannerArea -->
		<view class="banner">
			<image class="img" src="../../static/imgs/19032400.jpg" mode=""></image>
		</view>
		<!--  -->
	</view>
</template>

<script>
	export default {
		data() {
			return {
				style:"",//顶部固定导航
				tabData:[{imgsrcB:'../../static/imgs/zuan2b.png',imgsrcW:'../../static/imgs/zuan2w.png',text:'总排行榜'},
				{imgsrcB:'../../static/imgs/lock2b.png',imgsrcW:'../../static/imgs/lock2w.png',text:'解锁榜'},
				{imgsrcB:'../../static/imgs/chat2b.png',imgsrcW:'../../static/imgs/chat2w.png',text:'聊天榜'},
				{imgsrcB:'../../static/imgs/gift2b.png',imgsrcW:'../../static/imgs/gift2w.png',text:'礼物榜'}],//
				listNum:[{imgSrc:'../../static/imgs/first.png',text:'No.1'},
					{imgSrc:'../../static/imgs/second.png',text:'No.2'},
					{imgSrc:'../../static/imgs/thrird.png',text:'No.3'},
					{imgSrc:'../../static/imgs/456.png',text:'No.4'},
					{imgSrc:'../../static/imgs/456.png',text:'No.5'},
					{imgSrc:'../../static/imgs/456.png',text:'No.6'}],
				indicatorDots: false,//指示点显示
				autoplay: false,//自动播放
				interval: 2000,//间隔
				duration: 500,//动画时长
				circular:true,//衔接
				current:0,//当前activity的滑块
			};
		},
		onLoad() {//监听页面加载，其参数为上个页面传递的数据，参数类型为Object
		
				// console.log("页面加载")
				// this.getSwiperData()
		},
		onShow(){//监听页面显示。页面每次出现在屏幕上都触发，包括从下级页面点返回露出当前页面	
			// console.log("页面显示")
		},
		onReady(){//监听页面初次渲染完成。注意如果渲染速度快，会在页面进入动画完成前触发
			// console.log("初次渲染完成")
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
				this.style="background: rgba(0,0,0,0.3);"
			}else{
				this.style=""
			}
		},
		methods:{
			changeTab:function(e){
				// console.log(e.currentTarget.id);
				this.current=e.currentTarget.id;
				// console.log(this.current)
			},
			getChangeMsg:function(e){
				// console.log(e)
				// console.log(e.detail.current)
				this.current=e.detail.current;
			}
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
.content{
	width: 100%;
	height: 100%;
	.navBar{
		z-index: 9999;
		position: fixed;
		top: 0;
		left: 0;
		height: 100rpx;
		width: 700rpx;
		padding:0 25rpx;
		display: flex;
		justify-content: space-between;
		align-items: center;
		.logo{
			.img_logo1{
				height: 37rpx;
				width: 44rpx;
			}
			.img_logo2{
				margin-left: 17rpx;
				height: 29rpx;
				width: 97rpx;
			}
		}
		.text{
			font-size: 26rpx;
			.text_cn{
				color: #FFFFFF;
			}
			.text_en{
				margin-left: 45rpx;
				color: #B5B3B4;
			}
		}
		.num{
			display: flex;
			justify-content: space-between;
			align-items: center;
			padding:0 18rpx;
			height:50rpx;
			background:rgba(0,0,0,1);
			opacity:0.5;
			border-radius:6rpx;
			color:#FFD600;
			.num_img{
				height: 22rpx;
				width: 30rpx;
			}
			.num_text{
				font-size: 26rpx;
				color: #FFD600;
				margin-left: 13rpx;
			}
		}
	}
	// videoArea
	.videoArea{
		height: 808rpx;
		// background-image: url(../../static/imgs/19032400.jpg);
		// background-size: 100%;
		position: relative;
		.video{
			height: 100%;
			.promotionalVideo{
				width: 750rpx;
				height: 808rpx;
				// height: 100%;
			}
		}
		.videoMessageArea{
			height: 298rpx;
			width: 100%;
			position: absolute;
			bottom:0;
			left: 0;
			.photoArea{
				width: 84rpx;
				height: 84rpx;
				border:2px solid #FFFFFF; 
				border-radius: 50%;
				margin:auto;
				.photo{
					width:84rpx;
					height: 84rpx;
				}
			}
			.nameArea{
				height: 36rpx;
				line-height: 36rpx;
				font-size: 36rpx;
				margin-top: 30rpx;
				text-align: center;
				color:#FFFFFF;
			}
			.lockArea{
				width: 606rpx;
				height: 74rpx;
				margin-top:24rpx;
				padding:0 72rpx;
				display: flex;
				justify-content: space-between;
				align-items: center;
				.locked{
					height: 100%;
					display: flex;
					flex-direction: column;
					justify-content: space-between;
					align-items: center;
					.img{
						height: 44rpx;
						width: 44rpx;
					}
					.text{
						height: 20rpx;
						font-size: 20rpx;
						line-height: 20rpx;
						text-align: center;
						color:#fff;
					}
				}
				.btn{
					border:2rpx solid #FFD600;
					border-radius: 35rpx;
					padding: 20rpx 68rpx;
					display: flex;
					justify-content: center;
					align-items: center;
					color: #FFD600;
					.text{
						height: 30rpx;
						line-height: 30rpx;
						text-align: center;
						font-size: 26rpx;
					}
					.text2{
						margin-left: 7rpx;
						margin-right: 8rpx;
					}
					.text3{
						margin-left: 13rpx;
					}
					.img{
						height: 30rpx;
						width: 30rpx;
					}
				}
				.prise{
					height: 100%;
					display: flex;
					flex-direction: column;
					justify-content: space-between;
					align-items: center;
					.img{
						height: 44rpx;
						width: 44rpx;
					}
					.text{
						height: 20rpx;
						font-size: 22rpx;
						line-height: 22rpx;
						text-align: center;
						color:#fff;
					}
				}
			}
			// 
		}
	}
	// --popularArea--
	.popularArea{
		height: 398rpx;
		padding-top: 10rpx;
		margin-bottom: 24rpx;
		background: #232323;
		.popular_title{
			height: 38rpx;
			width: 700rpx;
			padding:0 24rpx 0 26rpx;
			display: flex;
			justify-content: space-between;
			align-items: center;
			margin-bottom: 22rpx;
			.left{
				display: flex;
				justify-content: space-between;
				align-items: center;
				.text{
					height:28rpx;
					font-size: 28rpx;
					line-height: 28rpx;
					color: #FFFFFF;
				}
				.img{
					border-radius: 50%;
					background: red;
					height: 38rpx;
					width:38rpx;
					margin-right: 8rpx;
				}
			}
			.right{
				display: flex;
				justify-content: space-between;
				align-items: center;
				.text{
					height: 22rpx;
					font-size: 22rpx;
					line-height: 22rpx;
					color:#FFFFFF;
				}
				.img{
					height: 24rpx;
					width: 24rpx;
					margin-left:9rpx;
				}
			}
		}
		// -----
		.scrollArea{
			height: 310rpx;
			width: 724rpx;
			margin-left:26rpx;
			border-radius: 8rpx;
			overflow-x: scroll;
			overflow-y:hidden;
			// ---
			.scroll{
				width: 1416rpx;
				display: flex;
				flex-wrap: nowrap;
				.item{
					width: 222rpx;
					height: 310rpx;
					margin-right: 14rpx;
					background: #FFFFFF;
					border-radius: 8rpx;
					position: relative;
					.bg{
						width: 222rpx;
						height: 310rpx;
					}
					.photo{
						background: #232323;
						position: absolute;
						height: 60rpx;
						width: 60rpx;
						border:2px solid #fff;
						border-radius: 50%;
						top: 10rpx;
						left: 10rpx;
					}
					.status{
						height: 20rpx;
						width: 20rpx;
						background: #17FF2A;
						position: absolute;
						top:55rpx;
						left: 55rpx;
						border-radius: 50%;
					}
					.text{
						position: absolute;
						width:197rpx;
						height:68rpx;
						bottom: 9rpx;
						left: 12rpx;
						font-size:22rpx;
						font-weight:400;
						color:#FFFFFF;
						line-height:34rpx;
						display: -webkit-box;
						-webkit-box-orient: vertical;
						-webkit-line-clamp: 2;
						overflow: hidden;
					}
				}	
			}
			// ---
		}
	}
	// ---worldList---
	.worldList{
		height: 812rpx;
		margin-bottom: 24rpx;
		background: #232323;
		padding-top: 28rpx;
		// ---worldTile---
		.world_title{
			height: 38rpx;
			width: 700rpx;
			padding:0 24rpx 0 26rpx;
			display: flex;
			justify-content: space-between;
			align-items: center;
			margin-bottom: 30rpx;
			.left{
				display: flex;
				justify-content: space-between;
				align-items: center;
				.text{
					height:28rpx;
					font-size: 28rpx;
					line-height: 28rpx;
					color: #FFFFFF;
				}
				.img{
					border-radius: 50%;
					background: red;
					height: 38rpx;
					width:38rpx;
					margin-right: 8rpx;
				}
			}
			.right{
				display: flex;
				justify-content: space-between;
				align-items: center;
				.text{
					height: 22rpx;
					font-size: 22rpx;
					line-height: 22rpx;
					color:#FFFFFF;
				}
				.img{
					height: 24rpx;
					width: 24rpx;
					margin-left:9rpx;
				}
			}
		}
		// tab--
		.tab{
			padding:0 25rpx;
			height: 52rpx;
			display: flex;
			justify-content:space-between;
			align-items: center;
			.item{
				display: flex;
				justify-content: center;
				align-items: center;
				padding:12rpx 0 14rpx 0;
				border-bottom: 2px solid #232323;
				transition:all 0.2s;
				.img{
					height: 24rpx;
					width: 24rpx;
					margin-right: 8rpx;
				}
				.text{
					font-size: 22rpx;
					line-height: 22rpx;
					color: #707070;
				}
			}
			.tabActive{
				box-sizing: border-box;
				border-bottom: 2px solid #FFFFFF;
				.text{
					color: #fff;
				}
			}
		}
		.swiperArea{
			padding:0 25rpx;
			height: 676rpx;
			.swiper{
				width: 100%;
				height: 100%;
				.swiper-item{
					display: flex;
					justify-content: space-between;
					align-items: center;
					flex-wrap: wrap;
					.listPer{
						width: 222rpx;
						height: 310rpx;
						background: #000000;
						color: #fff;
						margin-top: 20rpx;
						margin-bottom: 8px;
						position: relative;
						.img{
							position: absolute;
							width: 42rpx;
							height: 42rpx;
							bottom: 4rpx;
							left: 4rpx;
							z-index: 10;
						}
						.text{
							height: 30rpx;
							width: 82rpx;
							background: #FFFFFF;
							font-size: 22rpx;
							line-height: 30rpx;
							color:#383838;
							text-align: center;
							border-radius:15rpx; 
							position: absolute;
							bottom: 10rpx;
							left:28rpx;
							z-index: 9;
						}
					}
				}				
			}
		}
		// swiper---end--
		
	}
	// --bannerArea--
	.banner{
		height: 240rpx;
		margin-bottom: 24px;
		.img{
			width: 100%;
			height: 240rpx;
		}
	}
	// 
	
}
</style>
