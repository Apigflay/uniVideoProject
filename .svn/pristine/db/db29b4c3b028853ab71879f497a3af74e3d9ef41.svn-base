<template>
	<view class="main">
		<!-- top 背景 -->
		<view class="top_bj"></view>
		<!-- top 背景 -->
		<!-- top -->
		<view class="top">
			<image class="img" src="../../static/pictures/houtui_1.png"></image>
			<image class="size" src="../../static/pictures/gengduo1_1.png"></image>
		</view>
		<!-- top -->
		<!-- 头像 关注 -->
		<view class="head">
			<image class="photo" src=""></image>
			<view class="box" :style="show==false?'background:#808080':''" @click="follow()">
				<text v-if="show" class="p">关注</text>
				<text v-else class="p1">关注中</text>
			</view>
		</view>
		<!-- 头像 关注 -->
		<!-- 用户名 国籍 -->
		<view class="name">
			<text class="p">{{id}}</text>
			<image class="img" src="../../static/pictures/en_1.png"></image>
		</view>
		<!-- 用户名 国籍 -->
		<!-- 在线或离线 -->
		<view class="line">
			<image class="img" src="../../static/pictures/tuoyuan_1.png"></image>
			<text class="p">在线</text>
		</view>
		<!-- 在线或离线 -->
		<!-- 个人标签 -->
		<view class="label">
			<view class="box" v-for="(item,index) in table" :key="index" :id="index">
				<image class="img" :style="'background'+':'+item.color" src=""></image>
				<text class="p">{{item.title}}</text>
			</view>
		</view>
		<!-- 个人标签 -->
		<!-- 个人介绍 -->
		<view class="introduce">
			<view class="box" v-for="(item,index) in content" :key="index" :id="index">
				<text class="p">{{item.title}}</text>
			</view>
		</view>
		<!-- 个人介绍 -->
		<!-- 功能 -->
		<view class="function">
			<view class="gift">
				<image class="img" src="../../static/pictures/liwu_1.png"></image>
				<text class="p">送礼</text>
			</view>
			<view class="center_line"></view>
			<view class="gift">
				<image class="img" src="../../static/pictures/chat1_1.png"></image>
				<text class="p">聊天</text>
			</view>
		</view>
		<!-- 功能 -->
	</view>
</template>

<script>
	export default {
		data() {
			return {
				id: '@sasa.baby',
				color: '#89CC25',
				show: true,
				show1: false,
				table:[
					{title:'热门', color:'#89CC25'},
					{title:'全球榜No.1', color:'#F67776'}
				],
				content:[
					{title:'新一代swag长腿女神妮妮'},
					{title:'绝对高颜值  S曲线超长腿女主播'},
					{title:'身高密码165/43/32C  寻数名优质男伴'}
				]
			};
		},
		onLoad(){
			
		},
		methods:{
			follow:function(){
				
				if(this.show == true){
					this.show = false;
				}else{
					this.show = true;
				}
			}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	background: #232323;
}
.main{
	width: 100%;
	height: 100%;
	// ---背景
	.top_bj{
		height: 400rpx;
		width: 100%;
		background:url(../../static/pictures/juxing_1.png);
	}
	// ---- 设置部分
	.top{
		height: 150rpx;
		width: 100%;
		position:fixed;
		top: 0;
		left: 0;
		z-index:9999;
		display: flex;
		justify-content: space-between;
		align-items: center;
		.img{
			width: 36rpx;
			height: 36rpx;
			padding: 10rpx;
			margin-left: 29rpx;
		}
		.size{
			width: 38rpx;
			height: 38rpx;
			padding: 10rpx;
			margin-right: 29rpx;
		}
	}
	.head{
		width: 694rpx;
		display: flex;
		justify-content: flex-start;
		align-items: flex-end;
		margin-top: -40rpx;
		padding: 0rpx 28rpx 0rpx 28rpx;
		width: 100%;
		height: 100rpx;
		.photo{
			width:160rpx;
			height:160rpx;
			background: #151515;
			border:2px solid rgba(255, 255, 255, 1);
			border-radius:50%;
			margin-right: 44rpx;
		}
		.box{
			display: flex;
			justify-content: center;
			align-items: center;
			width:490rpx;
			height:40rpx;
			background:#FFD600;
			border-radius:8rpx;
			.p{
				color: #FFFFFF;
				font-size:26rpx;
				line-height: 26rpx;
			}
			.p1{
				color: #C8C7CC;
				font-size:26rpx;
				line-height: 26rpx;
			}
		}
	}
	.name{
		width: 692rpx;
		display: flex;
		justify-content: flex-start;
		align-items: center;
		margin-top: 18rpx;
		padding: 0rpx 29rpx;
		.p{
			color: #FFFFFF;
			font-size: 26rpx;
			line-height: 26rpx;
		}
		.img{
			width: 25rpx;
			height: 25rpx;
			margin-left: 7rpx;
		}
	}
	.line{
		width: 692rpx;
		display: flex;
		justify-content: flex-start;
		align-items: center;
		padding: 0rpx 29rpx;
		margin-top:20rpx;
		.img{
			width: 20rpx;
			height: 20rpx;
			margin-right: 12rpx;
		}
		.p{
			color: #00FF2A;
			font-size: 22rpx;
			line-height: 22rpx;
		}
	}
	.label{
		width: 694rpx;
		padding: 35rpx 20rpx 13rpx 28rpx;
		display:flex;
		.box{
			background:#FFFFFF;
			border-radius:17rpx;
			line-height: 34rpx;
			margin-right: 8rpx;
			display:flex;
			align-items:center;
			.img{
				// position: relative;
				width: 37rpx;
				height: 37rpx;
				border:2rpx solid rgba(255,255,255,1);
				border-radius:50%;
			}
			.p{
				color: #747474;
				font-size: 22rpx;
				margin-left: 4rpx;
				margin-right: 12rpx;
				line-height: 22rpx;
			}
		}
	}
	.introduce{
		width: 692rpx;
		padding: 27rpx 29rpx;
		.box{
			.p{
				color:#FFFFFF;
				font-size: 24rpx;
				line-height: 24rpx;
				padding-bottom: 27rpx;
			}
		}
	}
	.function{
		display:flex;
		justify-content:space-between;
		align-items: center;
		width: 492rpx;
		height: 80rpx;
		position: fixed;
		bottom: 0rpx;
		left: 0rpx;
		padding: 0rpx 129rpx;
		.gift{
			display:flex;
			align-items: center;
			.img{
				width:41rpx;
				height:39rpx;
			}
			.p{
				font-size:30rpx;
				color: #FFFFFF;
				margin-left: 17rpx;
			}
		}
		.center_line{
			width:2rpx;
			height:44rpx;
			background:#747474;
		}
	}
}
</style>