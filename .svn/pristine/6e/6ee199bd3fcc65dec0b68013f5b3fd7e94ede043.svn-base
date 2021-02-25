<template>
	<div class="main" style="background:rgba(35,35,35,1);">
		<div class="top">
			<div class="main_top">
				<img class="top_img" src="../../static/pictures/lcon_1.png"/>
				<img class="top_img1" src="../../static/pictures/lcon_1.png"/>
				<span class="top_title">首页</span>
				<span class="top_title1">Happy Your</span>
				<div class="top_box">
					<img class="top_img2" src="../../static/pictures/lcon_1.png"/>
					<span class="top_title2">30</span>
				</div>
			</div>
			<div class="main_center">
				<div class="center_img"></div>
				<div class="center_title">With my girl friend</div>
			</div>
			<div class="main_bottom">
				<div class="bottom_left">
					<img class="left" src="../../static/pictures/unlock_1.png"/>
					<span class="unlock_num">196</span>
				</div>
				<div class="bottom_center">
					<div class="center_title">解锁</div>
					<div class="center_title1">|</div>
					<img class="center_img" src="../../static/pictures/unlock_1.png"/>
					<div class="center_title2">240</div>
				</div>
				<div class="bottom_right">
					<img class="left" src="../../static/pictures/fabulous_1.png"/>
					<span class="unlock_num">100%</span>
				</div>
			</div>	
		</div>
		
		<div class="center">
			<div class="video">
				<div class="video_title">
					<div class="img"><img src="../../static/pictures/lcon_1.png"/></div>
					<div class="title">大家都在看</div>
					<div class="title2">
						
						<div class="title2_img"><img src="../../static/pictures/more_1.png"></img></div>
						<div class="title2_tit">更多</div>
					</div>
				</div>
				<div class="video_box">
					<div class="box" scroll-x="true" @scroll="scroll" >
						<div class="forimg" v-for="(item,index) in 15" :key='index'>
							<img class="img" src="../../static/pictures/box.jpeg"/>
						</div>
					</div>
				</div>
			</div>
			
			<div class="total">
				<div class="video_title">
					<div class="img"><img src="../../static/pictures/lcon_1.png"/></div>
					<div class="title">全球榜</div>
					<div class="title2">
						<div class="title2_img"><img src="../../static/pictures/more_1.png"></img></div>
						<div class="title2_tit">更多</div>
					</div>
				</div>
				<div class="video_chose">
					<div class="chose_style">
						<div class="chose_img"><img src="../../static/pictures/totlist.png"/></div>
						<div class="chose_title">总排行榜</div>
					</div>
					<div class="chose_style">
						<div class="chose_img"><img src="../../static/pictures/totlist.png"/></div>
						<div class="chose_title">解锁榜</div>
					</div>
					<div class="chose_style">
						<div class="chose_img"><img src="../../static/pictures/totlist.png"/></div>
						<div class="chose_title">聊天榜</div>
					</div>
					<div class="chose_style">
						<div class="chose_img"><img src="../../static/pictures/totlist.png"/></div>
						<div class="chose_title">礼物榜</div>
					</div>
				</div>
				<div class="video_box">
					<div class="box">
						<div class="forimg" v-for="(item,index) in 6" :key='index'>
							<div></div>
							<img class="img" src="../../static/pictures/box.jpeg"/>
						</div>
					</div>
					
				</div>
			</div>
			
		</div>
	
	</div>
</template>

<script>
	export default {
		data() {
			return {
				// scrollTop: 0,
				// old: {
				// 	scrollTop: 0
				// }
			};
		},
		methods: {
			// upper: function(e) {
			//             console.log(e)
			//         },
			//         lower: function(e) {
			//             console.log(e)
			//         },
			//         scroll: function(e) {
			//             console.log(e)
			//             this.old.scrollTop = e.detail.scrollTop
			//         },
			//         goTop: function(e) {
			//             this.scrollTop = this.old.scrollTop
			//             this.$nextTick(function() {
			//                 this.scrollTop = 0
			//             });
			//             uni.showToast({
			//                 icon:"none",
			//                 title:"纵向滚动 scrollTop 值已被修改为 0"
			//             })
			//         }
		}
	}
</script>

<style lang="scss">
.main{
	.top{
		// width:750rpx;
		// display:inline-block;
		// background:rgba(35,35,35,1);
		overflow-x:scroll;
		background:url(../../static/pictures/bgtop.jpg) no-repeat;background-size:100% 100%;
		.main_top{//页面顶部钻石数量以及图标
		// display: flex;
			width: 750rpx;
			// display:inline-block;
			// display: table-cell;
			// vertical-align: center;

			// background:rgba(0,0,0,1);
			background: black;
			background:rgba(0,0,0,.3);
			// opacity:0.3;
			.top_img{
				width:44rpx; 
				height:37rpx;
				
				margin-top: 32rpx;
				margin-bottom: 31rpx;
				margin-left: 23rpx;
			}
			.top_img1{
				width:97rpx;
				height: 29rpx;
				margin-top: 36rpx;
				margin-bottom: 35rpx;
				margin-left: 17rpx;
			}
			.top_title{
				font-size:26rpx;
				font-family:PingFangTC-Medium;
				font-weight:400;
				color:rgba(255,255,255,1);
				display: inline-block;
				margin-top: 37rpx;
				margin-bottom: 38rpx;
				margin-left: 50rpx;
				line-height: 25rpx;
			}
			.top_title1{
				font-size:26rpx;
				font-family:PingFangTC-Medium;
				font-weight:400;
				color:rgba(181,179,180,1);
				
				display: inline-block;
				margin-top: 37rpx;
				margin-bottom: 38rpx;
				margin-left: 45rpx;
				line-height: 25rpx;
			}
			.top_box{
				display:inline-block;
				// display: table-cell;
				// vertical-align: center;
				
				margin-top:25rpx;
				margin-bottom: 25rpx;
				margin-left: 140rpx;
				// margin-right:28rpx;
				
				.top_img2{
					width: 30rpx;
					height: 22rpx;
					
					margin-top: 14rpx;
					margin-bottom: 14rpx;
					margin-left: 22rpx;
				}
				.top_title2{
					font-size:26rpx;
					font-family:PingFangTC-Medium;
					font-weight:500;
					color:rgba(255,214,0,1);
					
					margin-top: 14rpx;
					margin-bottom: 14rpx;
					margin-left: 13rpx;
					margin-right:18rpx;
					
					line-height: 22rpx;
				}
			}
		}
		.main_center{//页面中间头像和文字	
			.center_img{
				width:88rpx;
				height:88rpx;
				background:rgba(25,25,25,1);
				border:2rpx solid rgba(255,255,255,1);
				border-radius:50%;
				
				margin-top: 411rpx;
				margin-left: 331rpx;
				margin-right: 331rpx;
			}
			.center_title{
				display: flex;
				width:292rpx;
				height:35rpx;
				font-size:36rpx;
				font-family:PingFangTC-Medium;
				font-weight:500;
				text-align: center;
				justify-content: center;
				color:rgba(255,255,255,1);
				white-space:nowrap;
				margin-top:30rpx;
				margin-left:235rpx;
				margin-right: 223rpx;
			}
		}
		.main_bottom{//页面底部解锁，钻石数量
			margin-top:48rpx;
			// display:inline-block;
			display:flex;
			justify-content:center;
			.bottom_left{
				// margin-left:72rpx;
				margin-right:114rpx;
				.left{
					width:44rpx;
					height:44rpx;
				}
				.unlock_num{
					display:block; 
					font-size: 22rpx; 
					font-family:PingFangTC-Medium; 
					font-weight:500; 
					color:rgba(255,255,255,1);
					// margin-left: 5rpx;
				}
				
			}
			.bottom_center{
				// margin-left:100rpx;
				// display:inline-block;
				display: flex;
				// flex-direction:row;
				// align-items : center; 
				justify-content: center;
				
				// width:290rpx;
				height:64rpx;
				border:2rpx solid #FFD600;
				border-radius:32rpx;
				
				// margin-left: 230rpx;
				// padding-left:50rpx;
				.center_title{
					font-family:PingFangTC-Medium;
					color:rgba(255,214,0,1);
					font-size:26rpx;
					
					margin-left: 68rpx;
					margin-top:20rpx;
					margin-bottom: 19rpx;
				}
				
				.center_title1{
					font-family:PingFangTC-Medium;
					color:rgba(255,214,0,1);
					font-size:26rpx;
					
					margin-left: 7rpx;
					margin-top:17rpx;
					margin-bottom: 17rpx;
				}
				.center_title2{
					font-family:PingFangTC-Medium;
					color:rgba(255,214,0,1);
					font-size:26rpx;
					
					margin-left: 13rpx;
					margin-top:21rpx;
					margin-bottom: 22rpx;
					margin-right: 67rpx;
				}
				.center_img{
					width: 37rpx;
					height: 32rpx;
					
					margin-left: 8rpx;
					margin-top:21rpx;
					margin-bottom: 21rpx;
				}
					
			}
			.bottom_right{
				margin-left:114rpx;
				// margin-right:72rpx;
				.left{
					width:44rpx;
					height:44rpx;
				}
				.unlock_num{
					display:block; 
					font-size: 22rpx; 
					font-family:PingFangTC-Medium; 
					font-weight:500; 
					color:rgba(255,255,255,1);
					// margin-left: 5rpx;
				}
				
			}
		}
	}

	.center{ // 中间部分视频 
		overflow-x:scroll;
		margin-top: 46rpx;
		.video{	// 大家都在看 视频栏
			.video_title{
				display: flex;
				align-items: center;
				margin-top:10rpx;
				.img{
					width: 38rpx;
					height:38rpx;
					border-radius:50%;
					
					margin-left: 28rpx;
					margin-top:10rpx;
				}
				.title{
					font-size:30rpx;
					font-weight:500;
					color:rgba(255,255,255,1);
					
					margin-left: 8rpx;
					
					
				}
				.title2{
					flex: 1;
					padding-right: 24rpx;
					.title2_tit{
						font-size:22rpx;
						font-family:PingFangTC-Regular;
						color:rgba(255,255,255,1);
						float:right;
					}
					.title2_img{
						width:24rpx;
						height: 24rpx;
						float: right;
						margin-top: 7rpx;
						margin-left: 9rpx;
						
					}
				}
			}
			.video_box{
				margin-top:22rpx;
				
				.box{
					display: flex;
					margin-left: 14rpx;
					overflow-x:scroll;
					.forimg{
						margin-left:14rpx;
						width: 222rpx;
						height: 310rpx;
						flex-shrink: 0;
						margin-bottom: 28rpx;
						.img{
							width: 222rpx;
							height: 310rpx;
						}
					}
				}
				.box::-webkit-scrollbar {display:none}
			}
		}
		.total{ // 全球榜
			margin-top:24rpx;
			.video_title{
				display: flex;
				align-items: center;
				margin-top:10rpx;
				.img{
					width: 38rpx;
					height:38rpx;
					border-radius:50%;
					
					margin-left: 28rpx;
					margin-top:10rpx;
				}
				.title{
					font-size:30rpx;
					font-family:PingFangTC-Medium;
					font-weight:500;
					color:rgba(255,255,255,1);
					
					margin-left: 8rpx;
					
					
				}
				.title2{
					flex: 1;
					padding-right: 24rpx;
		
					.title2_tit{
						font-size:22rpx;
						font-family:PingFangTC-Regular;
						color:rgba(255,255,255,1);
						float:right;
						
					}
					.title2_img{
						width:24rpx;
						height: 24rpx;
						float: right;
						// line-height: 22rpx;
						margin-top: 7rpx;
						margin-left: 9rpx;
						
					}
				}
			}
			.video_box{
				margin-top:20rpx;
				.box{ // 在视频循环外
					display: flex;
					flex-wrap:wrap; // 允许 flex 换行
					
					margin-left: 14rpx;
					.forimg{
						margin-left:14rpx;
						margin-bottom: 28rpx;
						width: 222rpx;
						height:310rpx;
						// flex-shrink: 0; // 让图片不能缩小0， 1可以缩小
						.img{
							width: 222rpx;
							height:310rpx;
						}
					}
				}
			}
			.video_chose{ // 全球帮 排行榜选项
				display: flex;
				align-items:center;
				justify-content : space-around;
				.chose_style{
					display: flex;
					align-items:center;
					justify-content : space-around;
					
					.chose_title{ // 排行榜选项字体样式
						font-size:22rpx;
						font-family:PingFangTC-Regular;
						font-weight:400;
						color:rgba(255,255,255,1);
						
						margin-left: 8rpx;
					}
					.chose_img{
						width: 24rpx;
						height: 24rpx;
					}
				}
				
			}
		}

	}
}
</style>
