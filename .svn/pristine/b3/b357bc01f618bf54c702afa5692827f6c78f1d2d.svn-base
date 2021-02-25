<template>
	<view class="main">
		<view class="search">
			<form @submit="formSubmit">
				<input class="section__title" id="search" type="search" placeholder="搜索" v-model="search" @input="check()" @blur="blur()" @focus="focus()"/>
			</form>
			<view class="cancel"><text class="font">取消</text></view>
		</view>
		<view class="content">
			<view class="list">
				<view class="listPer" v-for="(item,index) in 5" :key="index">
					<view class="left">
						<image class="photo"></image>
					</view>
					<view class="content">
						<view class="center">
							<text class="p"><text class="p1">@sasa.baby1235</text>9月vip为两台飞机  享有8.9月百部影片，你的女神...9月vip为两台飞机  享有8.9月百部影片...为两台飞机享有影片... </text>
						</view>
						<view class="bottom">
							<view class="b_left">
								<image class="img" src="../../static/pictures/zuanshi_1.png"></image>
								<text class="p">剩余89小时</text>
							</view>
							<view class="b_right">
								<image class="img" src="../../static/pictures/zuanshi_1.png"></image>
								<text class="p">免费</text>
							</view>
						</view>
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
				time:89
			};
		},
		methods:{
			check:function(){ // 搜索栏输入事件
				console.log(this.search)
			},
			focus:function(){
				console.log("aaa");
			},
			blur:function(){
				console.log("bbb")
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
	width: 100%;
	height: 100%;
	background: #191919;
	.search{ // 搜索栏样式
		width:100%;
		z-index: 9999; // 页面搜索栏 位置
		position: fixed;
		top: 0rpx;
		left: 0rpx;
		right: 0rpx;
		display: flex;
		justify-content:flex-start;
		align-items:center;
		background-color:#3B3B3B;
		padding: 23rpx 28rpx 23rpx 28rpx;
		.section__title{ //搜索栏中的 input
			width: 538rpx;
			height: 54rpx;
			color:#ACACAC;
			border-radius:8px;
			font-size: 30rpx;
			font-weight:400;
			background-color:#343434;
			background-image: url(../../static/pictures/search_1.png);
			background-repeat: no-repeat; /*设置图片不重复*/
			background-position: left; /*图片显示的位置*/
			background-position:12rpx; // 设置图片位置
			padding-left: 70rpx; //设置搜索文字位置
			background-size: 31rpx 31rpx; // 搜索图标的大小
		}
		.cancel{
			margin-left: 15rpx;
			margin-right:30rpx;
			.font{
				color: #FFFFFF;
				font-size: 30rpx;
				font-weight:400;
			}
		}
	}
	.content{ // 页面列表
		.list{
			padding-top:128rpx;
			margin-left:28rpx;
			.listPer{
				// height: 113rpx;
				display:flex;
				.left{ // 头像
					padding: 12rpx 0rpx 11rpx 0rpx;
					height: 90rpx;
					width: 90rpx;
					.photo{ // 头像
						background: #646464;
						height: 90rpx;
						width: 90rpx;
						border-radius: 8rpx;
					}
				}
				.content{
					width:100%;
					border-bottom: 2px solid #343434;
					margin-left:39rpx;
					margin-right:28rpx;
					display:flex;
					flex-direction:column;
					align-items:flex-start;
					.center{ // 内容
						.p{ // 文字内容
							margin-top: 7rpx;
							font-size: 20rpx;
							font-weight:400;
							color:#FFFFFF;
							display: -webkit-box;
							-webkit-box-orient: vertical;
							-webkit-line-clamp: 2;
							overflow: hidden;
							.p1{
								font-size: 26rpx;
								font-weight:500;
								color: #FFFFFF;
								margin-right: 27rpx;
							}
						}
					}
					.bottom{
						width: 100%;
						display: flex;
						justify-content:space-between;
						align-items: center;
						margin-bottom: 12rpx;
						.b_left{
							display:flex;
							align-items:center;
							margin-top:11rpx;
							.img{
								width: 23rpx;
								height: 21rpx;
								margin: 0rpx 12rpx 0rpx 4rpx;
							}
							.p{
								font-size:22rpx;
								font-weight:400;
								color: #FFFFFF;
								line-height: 22rpx;
							}
						}
						.b_right{
							display:flex;
							align-items:center;
							margin-top:12rpx;
							.img{
								width: 22rpx;
								height: 16rpx;
							}
							.p{
								margin: 0rpx 1rpx 0rpx 8rpx;
								color: #FFD600;
								font-size: 20rpx;
								font-weight:400;
								line-height: 20rpx;
							}
						}
					}
				}
			}
		}
	}
}
</style>
