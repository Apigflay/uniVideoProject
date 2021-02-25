<template>
	<view class="main">
		<view class="top">
			<view class="flex">
				<view class="image">
					<image class="img" src="../../static/pictures/back_1.png"></image>
				</view>
				<view class="title">
					<text class="p1">{{label}}</text>
					<text class="p2">{{num}}则帖子</text>
				</view>
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
	</view>
</template>

<script>
	export default {
		data() {
			return {
				label: '#性感',
				num: 1159,
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
					{imgSrc:'../../static/pictures/bgtop.jpg',text:'内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容',icon:'../../static/pictures/box.jpeg',online:true}
				],
			};
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
	height: 100%;;
	background: #191919;
	.top{
		position:fixed;
		top:0rpx;
		left:0rpx;
		right:0rpx;
		z-index:9999;
		
		.flex{
			height: 100rpx;
			padding-left:29rpx;
			display: flex;
			justify-content: flex-start;
			align-items: center;
			background-color:#3B3B3B;
			.image{
				width:30rpx;
				height:30rpx;
				margin-right:255rpx;
				.img{
					width:30rpx;
					height:30rpx;
				}
			}
			.title{
				display:flex;
				flex-direction:column;
				justify-content:center;
				.p1{
					font-size: 30rpx;
					font-weight:500;
					color: #FFFFFF;
					margin-left: 21rpx;
				}
				.p2{
					font-size: 22rpx;
					font-weight:400;
					color: #ACACAC;
				}
			}
		}
	}
	.movie{
		.video{
			background: #191919;
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
}
</style>
