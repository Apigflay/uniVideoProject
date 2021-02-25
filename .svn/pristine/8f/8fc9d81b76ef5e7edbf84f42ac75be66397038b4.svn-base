<template>
	<view class="content">
		<view class="nav">
			<image class="back" src="../../static/imgs/more1w.png" mode=""></image>
			<text class="title">商品管理</text>
			<image class="add" src="../../static/imgs/add1w.png" mode=""></image>
		</view>
		<view class="mainArea">
			<view class="per" v-for="(item,index) in 8" :key="index">
				<view class="textArea">
					<image class="img" src="../../static/imgs/bird1.png" mode=""></image>
					<view class="text">
						<view class="top">
							的所发生的的所发生的的所发生的的所发生的
							的所发生的的所发生的的所发生的的所发生的的所发生的的所发生的的所发生的的所发生的
							的所发生的的所发生的的所发生的的所发生的
						</view>
						<view class="bottom">
							<view class="hour">
								<image class="img" src="../../static/imgs/bofang1h.png" mode=""></image>
								<text>剩余98小时</text>
							</view>
							<view class="num">
								<image class="img" src="../../static/imgs/lock1h.png" mode=""></image>
								<text>2323</text>
							</view>
							<view class="prise">
								<image class="img" src="../../static/imgs/zan1h.png" mode=""></image>
								<text>100%</text>
							</view>
							<view class="gold">
								<image class="img" src="../../static/imgs/zuan2.png" mode=""></image>
								<text>免费</text>
							</view>
						</view>
					</view>
				</view>
				<view class="btmArea">
					<view class="remove">删除</view>
					<view class="rechange">编辑</view>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				
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
	width: 100%;
	height: 100%;
	display: flex;
	flex-direction: column;
	.nav{
		height: 100rpx;
		background:rgba(37,37,37,1);
		display: flex;
		justify-content: space-between;
		align-items: center;
		.back{
			height: 36rpx;
			width: 36rpx;
			padding: 10rpx;
			margin-left: 19rpx;
		}
		.title{
			font-size:30rpx;
			color:rgba(255,255,255,1);
		}
		.add{
			height: 36rpx;
			width: 36rpx;
			padding: 10rpx;
			margin-right: 19rpx;
		}
	}
	.mainArea{
		flex: 1;
		background:rgba(25,25,25,1);
		overflow-y:scroll;
		.per{
			height: 170rpx;
			width: 694rpx;
			margin-left: 28rpx;
			padding-top:28rpx;
			border-bottom: 2rpx solid rgba(52,52,52,1);
			.textArea{
				height: 90rpx;
				display: flex;
				justify-content: space-between;
				align-items: center;
				.img{
					height: 90rpx;
					width: 90rpx;
				}
				.text{
					height: 90rpx;
					width: 564rpx;
					.top{
						height: 60rpx;
						font-size:20rpx;
						line-height: 30rpx;
						color:rgba(255,255,255,1);
						display: -webkit-box;
						-webkit-box-orient: vertical;
						-webkit-line-clamp: 2;//几行省略就写几
						overflow: hidden;
					}
					.bottom{
						height: 30rpx;
						display: flex;
						justify-content: space-between;
						align-items: center;
						font-size:22rpx;
						color:rgba(255,255,255,1);
						.hour,.num,.prise,.gold{
							display: flex;
							justify-content: space-between;
							align-items: center;
							.img{height: 24rpx;width: 24rpx;margin-right:10rpx;}
						}
						.gold{
							color: #FFD600;
							.img{height: 16rpx;idth: 22rpx;}
						}
					}
				}
			}
			// -----btn----
			.btmArea{
				height: 80rpx;
				display: flex;
				justify-content: space-between;
				align-items: center;
				.remove{
					font-size:26rpx;
					color:rgba(255,118,118,1);
					padding: 8rpx 30rpx;
					border:2rpx solid rgba(255,118,118,1);
					border-radius:8rpx;
					margin-left: 150rpx;
				}
				.rechange{
					font-size:26rpx;
					color:rgba(255,214,0,1);
					padding: 8rpx 30rpx;
					border:2rpx solid rgba(255,214,0,1);
					border-radius:8rpx;
					margin-right: 150rpx;
				}
			}
				
		}
	}
}
</style>
