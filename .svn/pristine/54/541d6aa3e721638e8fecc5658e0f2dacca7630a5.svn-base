<template>
	<view class="main">
		<view class="content">
			<!-- 页面上部标题 -->
			<view class="top"> 
				<view class="left">
					<image class="img" src="../../static/pictures/back_1.png"></image>
				</view>
				<view class="center1">
					<view class="title">
						<view class="spot"></view>
						<text class="p">{{name}}</text>
					</view>
				</view>
				<view class="right">
					<view class="box">
						<image class="img" src="../../static/pictures/zuanshi_1.png"></image>
						<text class="p">{{num}}</text>
					</view>
				</view>
			</view>
			<!-- 页面中部聊天内容 -->
			<view class="center" @click="close()">
				<view class="chat">
					<view class="span_hi" v-if="showhi">
						<text class="p">{{hi}}</text>
						<text class="p">{{hi1}}</text>
					</view>
					<view class="span_date" v-if="showdate">
						<text class="p">{{date}}</text>
					</view>
					<view class="span_chat">
						<view :class="item.self == 1?'right':'left'"  v-for="(item,index) in chats" :key="index" :id="index">
							<image class="img" :src="item.imgsrc"></image>
							
							<view class="div">
								<image class="png" :src="item.pngsrc" v-if="item.pngshow"></image>
								<text class="p">{{item.text}}</text>
							</view>
							<text class="p2">{{item.time}}</text>
						</view>
					</view>
				</view>
			</view>
			<!-- 页面下部礼物 输入部分 -->
			<view class="bottom">
				<image class="img" src="../../static/pictures/liwu_1.png" @click="liwu()"></image>
				<view class="p"></view>
				<image class="img1" src="../../static/pictures/xiangji_1.png" @click="xiangji()"></image>
				<view class="span" @click="chat()">
					<text class="p1">以</text><image class="img2" src="../../static/pictures/zuanshi_1.png"></image><text class="p1">{{money}}传送信息</text>
				</view>
			</view>
			<view class="gift" v-show="show_gift">
				<!-- swiper 滑动 -->
				<view class="swiperArea">
					<swiper class="swiper" :current="current" :circular="circular" :indicator-dots="indicatorDots" :autoplay="autoplay" :interval="interval" :duration="duration" @animationfinish="getChangeMsg">
						<swiper-item>
							<view class="swiper-item uni-bg-green">
								<view class="listPer" v-for="(item,index) in 6" :key="index" :id="index">
									<view class="gift_img"><image class="photo" src="../../static/pictures/lcon_1.png"></image></view>
									<view class="gift_name"><text class="p">aa</text></view>
									<view class="gift_num"><text>aa</text></view>
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
						<swiper-item>
							<view class="swiper-item uni-bg-black">DDDDD</view>
						</swiper-item>
						<swiper-item>
							<view class="swiper-item uni-bg-black">DDDDD</view>
						</swiper-item>
					</swiper>		
				</view>
				<!-- tab 组件 -->
				<view class="tab">
					<view :class="index==current?'tabActive':'item'" v-for="(item,index) in tabData" :key="index" :id="index" @click="changeTab(item.id)">
						<text class="text">{{item.text}}</text>
					</view>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				name: '@sasa.baby1235', // 用户ID
				num: 30, // 剩余钻石数
				money:3500, // 通话价格
				show_gift: false,  // 页面礼物部分开关
				showhi: true, // 新建聊天系统自带文字
				showdate: true, // 是否显示上次聊天时间
				style:"",//顶部固定导航
				indicatorDots: false,//指示点显示
				autoplay: false,//自动播放
				interval: 2000,//间隔
				duration: 500,//动画时长
				circular:true,//衔接
				current:0,//当前activity的滑块
				hi:'跟对方说声嗨！',
				hi1:' 开始你们的 1 对 1 聊天',
				date: '08/26',
				tabData:[ // tab 标题
					{id: 0,text:'应援'},
					{id: 1,text:'情趣用品'},
					{id: 2,text:'角色扮演'},
					{id: 3,text:'生日快乐'},
					{id: 4,text:'漂亮宝贝'},
					{id: 5,text:'奢华宝贝'}
				],
				chats:[
					{
						self: 0,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 1,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 1,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 1,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 1,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 1,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 0,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 0,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						time: '15:18'
					},
					{
						self: 0,
						imgsrc: "../../static/pictures/search_1.png",
						text: '想看看视频9月vip为两台飞机 享有8.9月百部影片，你的女... ',
						pngsrc: "../../static/pictures/bgtop.jpg",
						time: '15:18',
						pngshow: true
					},
					{
						self: 1,
						imgsrc: "../../static/pictures/search_1.png",
						pngsrc: "../../static/pictures/bgtop.jpg",
						text: '想看看视频',
						time: '15:18',
						pngshow: true
					},
				],
				
				scrollheight: null
			};
		},
		onLoad() {
		},
		methods:{
			
			liwu:function(){ // 礼物页面
				// console.log("aaa");
				if(this.show_gift == false){
					this.show_gift = true
				}else{
					this.show_gift = false
				}
			},
			close:function() { // 点击页面body部分 关闭礼物页面
				this.show_gift = false;
			},
			xiangji:function(){ // 相机
				console.log("bbb");
			},
			chat:function(){ //聊天
				console.log("abc");
			},
			changeTab:function(e){ // tab 部分
				this.current = e;
				console.log(this.current);
			},
			getChangeMsg:function(e){ // swiper 给 tab传值
				this.current = e.detail.current
			}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	background: #191919;
}
.main{
	width:100%;
	height:100%;
	.content{
		width:100%;
		height:100%;
		display:flex;
		flex-direction:column;
		align-items:center;
		.top{
			width:100%;
			height: 100rpx;
			background: #252525;
			display:flex;
			flex-direction:row;
			justify-content:space-between;
			align-items:center;
			.left{
				display:flex;
				flex-direction:row;
				align-items:center;
				width:30rpx;
				height:30rpx;
				margin-left:29rpx;
				.img{
					width:30rpx;
					height: 30rpx;
				}
			}
			.center1{
				.title{
					display:flex;
					flex-direction:row;
					align-items:center;
					.spot{
						display: inline-block;
						vertical-align: middle;
						margin-right: 12rpx;
						height: 12rpx;
						width: 12rpx;
						background: #00FF2A;
						border-radius: 50%;
					}
					.p{
						color: #FFFFFF;
						font-size: 26rpx;
						font-weight: 500;
					}
				}
			}
			.right{
				margin-right:28rpx;
				.box{
					display:flex;
					flex-direction:row;
					align-items:center;
					heigth: 40rpx;
					background:rgba(0,0,0,0.5);
					border-radius: 6rpx;
					padding: 9rpx 12rpx 9rpx 13rpx;
					.img{
						width:30rpx;
						height:22rpx;
						vertical-align: middle;
						margin-right: 6rpx;
					}
					.p{
						color: #FFD600;
						font-size: 26rpx;
						font-weight: 500;
						line-height: 22rpx;
					}
				}
			}
		}
		.center{
			width:100%;
			flex: 1;
			display: flex;
			flex-direction: column;
			align-items:center;
			overflow-y:scroll;
			.chat{
				width:100%;
				.span_hi{
					margin-top: 39rpx;
					height: 62rpx;
					display:flex;
					flex-direction: column;
					align-items:center;
					.p{
						line-height:36rpx;
						color:#ACACAC;
						font-size:26rpx;
						font-weight:400;
					}
				}
				.span_date{
					margin: 28rpx 0rpx 35rpx 0rpx;
					height: 30rpx;
					display:flex;
					flex-direction: column;
					align-items:center;
					.p{
						color: #FFFFFF;
						font-size: 20rpx;
						font-weight: 400;
						line-height: 36rpx;
						border-radius: 15rpx;
						background: rgba(172,172,172,0.3);
						padding:6rpx 13rpx 6rpx 14rpx;
						// margin-bottom:12rpx;
					}
				}
				.span_chat{
					// display:flex;
					// flex-direction: row;
					// align-items: flex-start;
					// justify-content: flex-start;
					.left{
						display:flex;
						flex-direction: row;
						align-items: flex-start;
						padding:0rpx 28rpx 0rpx 28rpx;
						margin-bottom:16rpx;
						.img{
							width:50rpx;
							height: 50rpx;
							border-radius:50%;
							background: #C0C0C0;
							margin-right: 8rpx;
						}
						.div{
							max-width: 418rpx;
							background: #FFFFFF;
							border-radius:8rpx;
							padding:16rpx 26rpx 16rpx 25rpx;
							display:flex;
							flex-direction: column;
							.p{
								color: #000000;
								font-size: 28rpx;
								font-weight: 200;
								line-height: 36rpx;
								// margin:16rpx 26rpx 16rpx 25rpx;
							}
							.png{
								width: 110rpx;
								height: 154rpx;
								border-radius: 8rpx;
							}
						}
						
						.p2{
							align-self:flex-end;
							color: #ACACAC;
							font-size: 16rpx;
							font-weight: 200;
							margin-left: 14rpx;
							// line-height:36px;
						}
					}
					.right{
						display:flex;
						flex-direction: row-reverse;
						align-items: flex-end;
						padding:0rpx 28rpx 0rpx 28rpx;
						margin-bottom:16rpx;
						.img{
							// width:50rpx;
							// height: 50rpx;
							// border-radius:50%;
							// background: #C0C0C0;
							// margin-right: 8rpx;
							display: none;
						}
						.div{
							max-width: 418rpx;
							background: #343434;
							border-radius:8rpx;
							padding:16rpx 26rpx 16rpx 25rpx;
							display:flex;
							flex-direction: column;
							.p{
								color: #FFFFFF;
								font-size: 28rpx;
								font-weight: 200;
								line-height: 36rpx;
							}
							.png{
								width: 110rpx;
								height: 154rpx;
								border-radius: 8rpx;
							}
						}
						
						.p2{
							align-self:flex-end;
							color: #ACACAC;
							font-size: 16rpx;
							font-weight: 200;
							margin-right: 14rpx;
							// line-height:36px;
						}
					}
					
				}
			}
		}
		.bottom{
			width:100%;
			height: 90rpx;
			background:#252525;
			display:flex;
			flex-direction:row;
			align-items:center;
			justify-content:flex-start;
			.img{
				width: 46rpx;
				height: 46rpx;
				margin-left: 28rpx;
			}
			.p{
				width: 2rpx;
				height: 46rpx;
				background: rgba(255,255,255,0.5);
				margin: 0rpx 21rpx 0rpx 20rpx;
			}
			.img1{
				width: 46rpx;
				height: 46rpx;
				margin-right: 28rpx;
			}
			.span{
				display: flex;
				flex-direction:row;
				align-items:center;
				justify-content:flex-start;
				.p1{
					color: #ACACAC;
					font-size: 26rpx;
					font-weight: 400;
					line-height: 36rpx;
				}
				.img2{
					width: 30rpx;
					height: 22rpx;
					vertical-align: middle;
					margin:0rpx 5rpx 0rpx 5rpx;
				}
			}
		}
		.gift{
			width: 100%;
			height:395rpx;
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
							height: 160rpx;
							color: #fff;
							margin-top: 20rpx;
							margin-bottom: 8px;
							position: relative;
							display: flex;
							flex-direction:column;
							align-items: center;
							.gift_img{ // 礼物图片div大小
								width: 140rpx;
								height: 140rpx;
								background:#636363;
								.photo{
									width: 140rpx;
									height: 140rpx;
								}
							}
							.gift_name{
								
							}
							.gift_num{
								
							}
						}
					}				
				}
			}
			.tab{
				width: 100%;
				overflow-x: scroll;
				height: 90rpx;
				background: #191919;
				display:flex;
				flex-direction:row;
				justify-content:space-between;
				align-items:center;
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
		}
		
	}
}
</style>
