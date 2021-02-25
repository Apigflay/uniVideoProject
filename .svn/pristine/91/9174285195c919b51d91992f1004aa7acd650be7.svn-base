<template>
	<view class="content">
		<view class="nav">
			<image class="img" src="../../static/imgs/more1w.png" mode=""></image>
			<text class="title">信息编辑</text>
			<text class="save">保存</text>
		</view>
		<view class="bgArea">
			<image class="bg" src="../../static/imgs/19032400.jpg" mode=""></image>
			<view class="phoArea">
				<image class="photo" src="../../static/imgs/bird1.png" mode=""></image>
				<image class="camera" src="../../static/imgs/camera1w.png" mode=""></image>
			</view>
			<image class="choosebg" src="../../static/imgs/camera1w.png" mode=""></image>
		</view>
		<view class="introductionArea">
			<view class="name">
				<text class="text">用户名</text>
				<input class="input" type="text" placeholder="请输入用户名" />
			</view>
			<view class="p">
				<text class="tltle">用户说明</text>
				<text class="num">{{textAreaLegth}}/150</text>
			</view>
			<view class="textArea">
				<textarea class="text" v-model="textArea" placeholder-style="color:rgba(116,116,116,1)" placeholder="" :maxlength="150" @input="getTextLength"/>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				textAreaLegth:0,//textarea长度
				textArea:'',//textarea内容
			};
		},
		methods:{
			getTextLength:function(event){
				this.textAreaLegth=event.target.value.length;
			}
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
	background:rgba(25,25,25,1);
	// -----nav---
	.nav{
		height: 100rpx;
		display: flex;
		justify-content: space-between;
		align-items: center;
		background:rgba(37,37,37,1);
		.img{
			height: 30rpx;
			width: 30rpx;
			padding: 10rpx;
			margin-left: 20rpx;
		}
		.title{
			font-size:30rpx;
			font-weight:400;
			color:rgba(255,255,255,1);
		}
		.save{
			font-size:30rpx;
			font-family:PingFang TC;
			font-weight:400;
			color:rgba(255,214,0,1);
			padding: 10rpx;
			margin-right: 30rpx;
		}
	}
	// ----bgArea----
	.bgArea{
		height: 370rpx;
		position: relative;
		border-bottom: 2rpx solid rgba(116,116,116,1);
		.bg{
			position: absolute;
			top:0;
			left: 0;
			width: 100%;
			height: 370rpx;
		}
		.phoArea{
			position: absolute;
			top:100rpx;
			left: 285rpx;
			width: 180rpx;
			height: 180rpx;
			.photo{
				width: 176rpx;
				height: 176rpx;
				border:2rpx solid #fff;
				border-radius: 50%;
			}
			.camera{
				position: absolute;
				top:120rpx;
				left: 120rpx;
				width: 60rpx;
				height: 60rpx;
				border-radius: 50%;
			}
		}
		.choosebg{
			position: absolute;
			bottom:28rpx;
			right: 28rpx;
			width: 60rpx;
			height: 60rpx;
			border-radius: 50%;
		}
	}
	// ---introductionArea---
	.introductionArea{
		width: 680rpx;
		padding:0 35rpx;
		.name{
			height: 85rpx;
			border-bottom:2rpx solid rgba(116,116,116,1);
			display: flex;
			align-items: center;
			.text{
				font-size:30rpx;
				color:rgba(255,255,255,1);
				margin-right:28rpx;
			}
			.input{
				font-size:30rpx;
				color:rgba(116,116,116,1);
			}
		}
		.p{
			height: 30rpx;
			margin:28rpx 0;
			display: flex;
			justify-content: space-between;
			align-items: center;
			.tltle{
				font-size:30rpx;
				color:rgba(255,255,255,1);
			}
			.num{
				font-size:22rpx;
				color:rgba(116,116,116,1);
			}
		}
		.textArea{
			background:rgba(46,46,46,1);
			border-radius:8px;
			.text{
				width: 100%;
				min-height:211rpx;
				border-radius:8px;
				color:rgba(116,116,116,1);
			}
		}
			
	}
}
</style>
