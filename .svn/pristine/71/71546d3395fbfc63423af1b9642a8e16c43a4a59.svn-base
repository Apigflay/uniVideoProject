<template>
	<view class="main">
		<view class="top" :style="style">
			<view class="flex">
				<text class="p1">{{title}}</text>
				<text class="p2">{{title2}}</text>
			</view>
		</view>
		<view class="movie">
			<view class="video">
				<view class="listPer" v-for="(item,index) in listNum" :key="index">
					<image class="img" :src="item.imgSrc" mode=""></image>
					<image class="photo"></image>
					<view class="status"></view>
					<view class="text">{{item.text}}</view>
				</view>
			</view>
		</view>
		<view class="bottom">
			<view class="share">
				<text class="p1">当个称职的老司机</text>
				<view class="p2">
					<image class="img" src="../../static/pictures/back_1.png"></image>
					<text class="p1">分享给好友</text>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				style: "", // 页面顶部关于透明度的设置
				title: "首页",
				title2: "限时免费",
				listNum:[
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true},
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true}
				],
			};
		},
		onPageScroll(){
			if(document.documentElement.scrollTop>=100){
				this.style="background: rgba(0,0,0,0.3);"
			}else{
				this.style=""
			}
		},
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	background:#191919;
}
.main{
	width: 100%;
	height: 100%;
	background:#191919;
	.top{
		height:100rpx;
		background-color:#3B3B3B;
		position:fixed;
		top:0rpx;
		left:0rpx;
		right:0rpx;
		z-index:9999;
		.flex{
			height: 100%;
			display:flex;
			align-items:center;
			// justify-content:center;
			.p1{
				color:#B5B2B3;
				font-size: 26rpx;
				font-weight: 400;
				margin-left:231rpx;
			}
			.p2{
				color: #FFFFFF;
				font-size:26rpx;
				font-weight:400;
				margin-left:45rpx;
			}
		}
	}
	.movie{
		background-color: #191919;
		.video{
			background-color: #191919;
			height: 100%;
			padding: 128rpx 14rpx 0rpx 28rpx;
			display: flex;
			justify-content: flex-start;
			align-items: center;
			flex-wrap: wrap;
			.listPer{
				width: 222rpx;
				height: 310rpx;
				margin-bottom: 28rpx;
				margin-right: 14rpx;
				position: relative;
				.img{
					border-radius:8rpx;
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
	}
	.bottom{
		// height:134rpx;
		position:fixed;
		left: 0rpx;
		right: 0rpx;
		top:1200rpx;
		// bottom:90rpx;
		z-index:9999;
		.share{
			height:44rpx;
			background-color: rgba($color: #FFD600, $alpha: 0.9);
			display:flex;
			justify-content:center;
			align-items:center;
			.p1{
				
				font-size: 22rpx;
				color: #FFFFFF;
				font-weight:400;
			}
			.p2{
				// width: 166rpx;
				border:2px solid #FFFFFF;
				border-radius:16px;
				display:flex;
				justify-content:center;
				align-items:center;
				margin-left:15rpx;
				margin-top: 6rpx;
				margin-bottom: 6rpx;
				padding-left:10rpx;
				padding-right:11rpx;
				padding-bottom:4rpx;
				padding-top:1rpx;
				.img{
					margin-top:2rpx;
					width:26rpx;
					height:21rpx;
				}
				.p1{
					font-size: 22rpx;
					color:#FFFFFF;
					font-weight:500;
				}
			}
		}
	}
}
</style>
