<template>
	<view class="main">
		<view class="top">
			<image src="../../static/pictures/back_1.png" class="img"></image>
			<text>设定</text>
		</view>
		<view class="center">
			<view>
				<text class="font">个人</text>
				<view class="list" v-for="(item,index) in personal" :key="index" :id="index">
					<text class="p1">{{item.title}}</text>
					<text v-show="item.no" class="p2">女性</text>
					<image class="img" src="../../static/pictures/gengduo_1.png"></image>
				</view>
			</view>
			<view>
				<text class="font">钱包</text>
				<view class="list" v-for="(item,index) in wallet" :key="index" :id="index">
					<text class="p1">{{item.title}}</text>
					<image class="img" src="../../static/pictures/gengduo_1.png"></image>
				</view>
			</view>
			<view>
				<text class="font">关于</text>
				<view class="list" v-for="(item,index) in about" :key="index" :id="index">
					<text class="p1">{{item.title}}</text>
					<text v-show="item.no" class="p2">简体中文</text>
					<image class="img" src="../../static/pictures/gengduo_1.png"></image>
				</view>
			</view>
			
			<view class="exit">
				<text class="p">登出</text>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				personal:[
					{title:'探索页喜好',no:'true'},
					{title:'已连结账号',no:''},
					{title:'封锁名单',no:''},
					{title:'删除账号',no:''}
				],
				wallet:[
					{title:'日志'},
					{title:'支付方式'},
					{title:'我的电子收据'},
					{title:'购买钻石'}
				], 
				about:[
					{title:'语言',no:'true'},
					{title:'常见问题',no:''},
					{title:'联系客服',no:''},
					{title:'系统问题反馈',no:''},
					{title:'免费下载 catr 应用程序',no:''},
					{title:'更多',no:''}
				]
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
	width: 100%;
	height: 100%;
	display:flex;
	align-items:center;
	flex-direction:column;
	.top{
		position:relative;
		width:100%;
		height:100rpx;
		background: #252525;
		font-size:30rpx;
		color:#FFFFFF;
		text-align:center;
		line-height:100rpx;
		.img{
			width: 36rpx;
			height: 36rpx;
			position: absolute;
			top: 32rpx;
			left: 28rpx;
		}
	}
	.center{
		flex: 1;
		overflow-y:scroll;
		.font{
			color: #747474;
			font-size: 22rpx;
			float:left;
			align-self: flex-start;
			padding: 20rpx 36rpx;
			width: 678rpx;
			border-bottom: 1rpx solid #747474;
		}
		.list{
			width: 678rpx;
			height: 100rpx;
			padding: 0rpx 36rpx;
			background-color: #252525;
			border-bottom: 1rpx solid #747474;
			display:flex;
			align-items: center;
			justify-content:space-between;
			position:relative;
			.p1{
				font-size: 26rpx;
				color: #FFFFFF;
			}
			.p2{
				font-size: 26rpx;
				color: #747474;
				position:absolute;
				right: 79rpx;
				
			}
			.img{
				width:30rpx;
				height: 30rpx;
			}
		}
		.exit{
			width: 100%;
			height: 100rpx;
			background-color:#252525;
			display:flex;
			align-items:center;
			justify-content:center;
			margin:84rpx 0rpx 39rpx 0rpx;
			.p{
				color: #FF7676;
				font-size: 30rpx;
				line-height: 30rpx;
			}
		}
	}
}
</style>
