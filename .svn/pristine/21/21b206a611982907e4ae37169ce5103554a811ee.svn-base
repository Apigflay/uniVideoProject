<template>
	<view class="content">
		<view class="scroll">
			<!-- <progress style="width:100%; border-radius:5rpx;" activeColor="#FFFFFF" percent="60" active stroke-width="8" backgroundColor="rgba($color: #FFFFFF, $alpha: 0.7)"/> -->
		</view>
		<view class="title">
			<view class="img">
				<image class="photo" src="" mode=""></image>
				<view class="status"></view>
			</view>
			<view class="name">
				<text class="id">{{id}}</text>
				<text class="date">{{date}}</text>
			</view>
			<view class="follow">
				<text class="font">关注</text>
			</view>
			<view class="delete">
				<image class="del" src="../../static/pictures/delete_1.png"></image>
			</view>
		</view>
		<view class="introduce">
			<view class="p">
				<text class="font">{{txt}}
				<image class="img" src="../../static/pictures/more2_1.png"></image>
				</text>
			</view>
			<view class="label">
				<view class="span" v-for="(item,index) in 4" :key="index" :id="index">
					<text class="p">#性感</text>
				</view>
			</view>
		</view>
		<view class="details">
			<view class="left">
				<view class="time">
					<image class="img" src="../../static/pictures/totlist.png"></image>
					<text class="p">{{time}}</text>
				</view>
				<view class="time">
					<image class="img" src="../../static/pictures/unlock_1.png"></image>
					<text class="p">{{unlock}}</text>
				</view>
				<view class="time">
					<image class="img" src="../../static/pictures/fabulous_1.png"></image>
					<text class="p">{{like}}</text>
				</view>
			</view>
			<view class="right">
				<view class="new">
					<image class="left" src="../../static/pictures/lcon_1.png"></image>
					<text class="center">{{newnum}}新帖子</text>
					<image class="right" src="../../static/pictures/more2_2.png"></image>
				</view>
			</view>
		</view>
		<view class="unlock">
			<view class="button">
				<text class="b1">解锁</text>
				<text class="b2">|</text>
				<image class="b3" src="../../static/pictures/zuanshi_1.png"></image>
				<text class="b4">{{money}}</text>
			</view>
			<view class="more">
				<image class="img" src="../../static/pictures/more3_1.png"></image>
			</view>
		</view>
		<view class="movie">
			<video class="video" :objectFit="fill" :loop="true" :autoplay='true' muted='muted' :controls='false' :show-center-play-btn="true" src="../../static/promotionalVideo.mp4"></video>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				id: "@feifei.baby",
				date: "17h ago",
				txt: '网咖八项突击粉丝干炮，露脸，太爽意识模糊付费看你们的女神希望大家关注我，更多精彩的视频等你来看  哔哩啪啦  噼里啪啦',
				time: '30:30:03', // 剩余时间
				unlock: '658', // 解锁数
				like: '658' ,// 点赞数
				newnum: '28' ,// 新帖子数
				money: '240'
			};
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
}
.content{
	width:100%;
	height:100%;
	.scroll{
		position: absolute;
		top: 16rpx;
		left: 20rpx;
		right: 20rpx;
		z-index: 9999;
	}
	.title{ // 进度条下方标题
		height: 65rpx;
		position:absolute;
		top:50rpx;
		left:22rpx;
		z-index: 9999;
		display:flex;
		// justify-content:space-between;
		align-items:flex-start;
		.img{
			.photo{
				background: #877DF5;
				height: 60rpx;
				width: 60rpx;
				border:2px solid #FFFFFF;
				border-radius: 50%;
			}
			.status{
				height: 20rpx;
				width: 20rpx;
				background: #17FF2A;
				position: absolute;
				top:41rpx;
				left: 41rpx;
				border-radius: 50%;
			}
		}
		.name{
			display:flex;
			flex-direction:column;
			justify-content:center;
			margin-left:13rpx;
			.id{
				font-size: 26rpx;
				color: #FFFFFF;
				font-weight:500;
				margin-bottom: 5rpx;
			}
			.date{
				font-size: 22rpx;
				color: #FFFFFF;
				font-weight:400;
				opacity:0.5;
			}
		}
		.follow{
			display:flex;
			align-items:flex-start;
			margin-top: 7rpx;
			margin-left:32rpx;
			.font{
				color: #FFD600;
				font-size: 26rpx;
				font-weight: 500;
			}
		}
		.delete{
			align-self:flex-end;
			margin-left:349rpx;
			.del{
				width:34rpx;
				height: 34rpx;
			}
		}
	}
	.introduce{ // 标题下方的介绍
		// height: 97rpx;
		position:absolute;
		// top:135rpx;
		top:140rpx;
		left:22rpx;
		z-index: 9999;
		display:flex;
		flex-direction:column;
		padding: 0px 29rpx 0rpx 20rpx;
		.p{
			.font{
				color:#FFFFFF;
				line-height:36rpx;
				font-size:26rpx;
				font-weight:400;
				opacity:0.9;
				.img{
					width: 24rpx;
					height: 24rpx;
					vertical-align: text-bottom; // 图表与父元素内的文字的垂直对齐的方式
					margin-left:27rpx;
				}
			}
		}
		.label{
			
			display:flex;
			flex-wrap:wrap;
			.span{
				margin-top: 20rpx;
				// height: 30rpx ;
				padding:4rpx 11rpx 4rpx 11rpx;
				margin-right: 10rpx;
				background:#000000;
				opacity:0.5;
				border-radius:15px;
				
				display:flex;
				
				justify-content: center;
				align-items:center;
				.p{
					font-size: 22rpx;
					color: #FFFFFF;
					font-weight:400;
					// line-height:36px;
					// vertical-align: middle;
				}
			}
		}
	}
	.details{ // 视频下方关于视频的详细情况
		position: absolute;
		top:1158rpx;
		left: 0rpx;
		right: 0rpx;
		z-index: 9999;
		// padding: 0rpx 20rpx 0rpx 20rpx;
		display: flex;
		justify-content: space-between;
		align-items: center;
		.left{
			display:flex;
			.time{
				display:flex;
				justify-content:space-between;
				align-items:center;
				margin:0rpx 14rpx 0rpx 20rpx;
				.img{
					width: 23rpx;
					height: 22rpx;
				}
				.p{
					font-size: 22rpx;
					color: #FFFFFF;
					font-weight:400;
					margin-left: 13rpx;
					line-height: 18rpx;
				}
			}		
		}
		.right{
			display:flex;
			align-items: center;
			background:#00D8FF;
			border-radius:22rpx;
			margin-right:20rpx;
			.new{
				display:flex;
				align-items: center;
				justify-content: space-around;
				
				.left{
					margin:2rpx 0rpx 2rpx 2rpx;
					width: 40rpx;
					height: 40rpx;
					border-radius: 50%;
				}
				.center{
					margin-left: 15rpx;
					margin-right: 16rpx;
					font-size: 22rpx;
					color: #FFFFFF;
					font-weight:400;
				}
				.right{
					margin: 16rpx 23rpx 16rpx 0rpx;
					width: 24rpx;
					height: 12rpx;
				}
			}
		}
	}
	.unlock{ // 页面解锁部分
		position: absolute;
		top: 1270rpx;
		left:0rpx;
		right: 0rpx;
		z-index: 9999;
		height: 0rpx;
		display: flex;
		align-items:center;
		.button{
			margin-left: 20rpx;
			border:2rpx solid #FFD600;
			border-radius: 36rpx;
			padding: 19rpx 220rpx 19rpx 218rpx;
			display: flex;
			// justify-content: center;
			align-items: center;
			.b1{
				color: #FFD600;
				font-size: 36rpx;
				font-weight: 500;
				line-height: 34rpx;
				margin-right: 12rpx;
			}
			.b2{
				font-size: 36rpx;
				color: #FFD600;
				line-height: 34rpx;
				margin-right: 10rpx;
			}
			.b3{
				width: 34rpx;
				height: 25rpx;
				margin-right: 15rpx;
				// align-self: flex-end;
				align-self: center;
			}
			.b4{
				color: #FFD600;
				font-size: 36rpx;
				font-weight: 500;
				line-height: 34rpx;
				align-self: flex-end;
			}
		}
		.more{
			width: 36rpx;
			height: 36rpx;
			margin-left: 33rpx;
			.img{
				width: 36rpx;
				height: 36rpx;
			}
		}
	}
	.movie{
		width: 100%;
		height: 100%;
		.video{
			width:100%;
			height: 100%;
		}
	}
}
</style>
