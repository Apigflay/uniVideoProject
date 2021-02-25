<template>
	<view class="main">
		<view class="top">
			<image src="../../static/pictures/back_1.png" class="img"></image>
			<text>封锁名单</text>
		</view>
		<view class="center">
			<view class="list" v-for="(item,index) in 12" :key="index" :id="index">
				<image src="" class="photo" ></image>
				<text class="p">{{item.id}}</text>
				<view :class="item.change == 1?'button':'button1'" @click="c_change()" @touchstart="t_change(index)" @touchend="t_change_end(index)"><text class="p1">解除封锁</text></view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				table:[
					{id:'@sasa.baby1235',change:1}
					// {id:'@sasa.baby1235',change:1},
					// {id:'@sasa.baby1235',change:1}
				]
			};
		},
		onLoad(){
			
		},
		methods:{
			c_change: function() {
				// console.log('a')
			},
			t_change: function(e) {
				// console.log('b')
				if(this.table[e].change == 1){
					this.table[e].change = 0;
				}else{
					console.log(this.table[e].change)
				}
			},
			t_change_end:function(e) {
				if(this.table[e].change == 0){
					this.table[e].change = 1;
				}else{
					console.log(this.table[e].change)
				}
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
	display: flex;
	align-items:center;
	flex-direction:column;
	.top{
		height:100rpx;
		width: 100%;
		background: #252525;
		// background: red;
		position:relative;
		color: #FFFFFF;
		font-size: 30rpx;
		line-height:100rpx;
		text-align:center;
		.img{
			width:36rpx;
			height: 36rpx;
			padding: 10rpx;
			position: absolute;
			top: 22rpx;
			left: 20rpx;
		}
	}
	.center{
		flex: 1;
		overflow-y:scroll;
		.list{
			display: flex;
			justify-content: space-between;
			align-items: center;
			height:122rpx;
			border-bottom: 1rpx solid #747474;
			width: 694rpx;
			padding:0rpx 28rpx;
			.photo{
				width:90rpx;
				height:90rpx;
				background: #646464;
				border-radius:50%;
			}
			.p{
				color: #747474;
				font-size:26rpx;
				line-height: 26rpx;
				margin-left: 40rpx;
				margin-right: 221rpx;
			}
			.button{
				width:134rpx;
				height:50rpx;
				border:2px solid #FFD600;
				border-radius:8rpx;
				display:flex;
				justify-content: center;
				align-items: center;
				.p1{
					color: #FFD600;
					font-size: 26rpx;
					line-height: 26rpx;
				}
			}
			.button1{
				width:134rpx;
				height:50rpx;
				border:2px solid #FFD600;
				background: #FFD600;
				border-radius:8rpx;
				display:flex;
				justify-content: center;
				align-items: center;
				.p1{
					color: #191919;
					font-size: 26rpx;
					line-height: 26rpx;
				}
			}
		}
	}
}
</style>
