<template>
	<view class="main">
		<view class="top">
			<text>聊天</text>
		</view>
		<view class="center">
			<view  :style="no == false?'':'background:#343434'" class="new" @change="new_chat()" @touchstart="new_chat_start()" @touchend="new_chat_end()">
				<image class="img" src="../../static/pictures/newChat_1.png"></image>
				<text class="p">建立聊天室</text>
			</view>
			<view :class="item.no == false?'list':'list1'" v-for="(item,index) in table" :key="index" :id="index" @change="chat(index)" @touchstart="chat_start(index)" @touchend="chat_end(index)">
				<image class="photo" src=""></image>
				<view class="content">
					<view class="name">
						<text class="id">{{item.name}}</text>
						<view class="status"></view>
						<text class="data">{{item.data}}</text>
					</view>
					<view class="p">
						<text>{{item.content}}</text>
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
				show_spot: true ,// 是否在线的值
				show_chat: false ,// 控制列表是否为点击状态
				no: false,
				table:[
					{id:0,data:'09/09', name:'@sasa.baby1235',content:'9月vip为两台飞机  享有8.9月百部影片，你的女神...', no:false},
					{id:1,data:'09/09', name:'@sasa.baby1236',content:'9月vip为两台飞机  享有8.9月百部影片，你的女神...', no:false},
					{id:2,data:'09/09', name:'@sasa.baby1237',content:'9月vip为两台飞机  享有8.9月百部影片，你的女神...', no:false},
				]
			};
		},
		methods:{
			chat:function(e){
				this.index = e;
				console.log(this.index)
			},
			chat_start:function(e){
				if(this.table[e].no == false){
					this.table[e].no = true;
				}else{
					console.log(this.table[e].no)
				}
			},
			chat_end:function(e){
				if(this.table[e].no == true){
					this.table[e].no = false;
				}else{
					console.log(this.table[e].no)
				}
			},
			
			new_chat_start:function(){
				console.log(this.no)
				if(this.no == false){
					this.no = true;
				}else{
					console.log(this.no)
				}
			},
			new_chat_end:function(){
				console.log(this.no)
				if(this.no == true){
					this.no = false;
				}else{
					console.log(this.no)
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
		position:relative;
		width: 100%;
		height:100rpx;
		background: #252525;
		font-size: 30rpx;
		color:#FFFFFF;
		line-height: 100rpx;
		text-align: center;
	}
	.center{
		.new{
			display:flex;
			align-items: center;
			width:694rpx;
			height:129rpx;
			padding: 0rpx 28rpx;
			
			.img{
				width: 90rpx;
				height: 90rpx;
				margin-right: 40rpx;
			}
			.p{
				width:565rpx;
				border-bottom: 2rpx solid #343434;
				line-height: 129rpx;
				font-size:26rpx;
				color:#FFFFFF;
			}
		}
		.list{
			display:flex;
			align-items:center;
			padding: 0rpx 28rpx;
			width:694rpx;
			height:113rpx;
			.photo{
				background: #646464;
				height: 90rpx;
				width: 90rpx;
				border-radius: 50%;
				margin-right: 40rpx;
			}
			.content{
				display:flex;
				flex-direction: column;
				justify-content:center;
				align-items:center;
				height:113rpx;
				border-bottom: 2rpx solid #343434;
				.name{
					width: 565rpx;
					display:flex;
					align-items:center;
					.id{
						color:#FFFFFF;
						font-size:26rpx;
						line-height:26rpx;
					}
					.status{
						margin-left: 11rpx;
						height: 12rpx;
						width: 12rpx;
						background: #00FF2A;
						border-radius: 50%;
					}
					.data{
						color: #ACACAC;
						font-size: 22rpx;
						line-height: 22rpx;
						margin-left: 262rpx;
					}
				}
				.p{
					width:565rpx;
					color: #ACACAC;
					font-size: 22rpx;
					line-height: 22rpx;
					margin-top: 9rpx;
				}
			}
		}
		
		.list1{
			display:flex;
			align-items:center;
			padding: 0rpx 28rpx;
			width:694rpx;
			height:113rpx;
			background:#343434;
			.photo{
				background: #646464;
				height: 90rpx;
				width: 90rpx;
				border-radius: 50%;
				margin-right: 40rpx;
			}
			.content{
				display:flex;
				flex-direction: column;
				justify-content:center;
				align-items:center;
				height:113rpx;
				border-bottom: 2rpx solid #343434;
				.name{
					width: 565rpx;
					display:flex;
					align-items:center;
					.id{
						color:#FFFFFF;
						font-size:26rpx;
						line-height:26rpx;
					}
					.status{
						margin-left: 11rpx;
						height: 12rpx;
						width: 12rpx;
						background: #00FF2A;
						border-radius: 50%;
					}
					.data{
						color: #ACACAC;
						font-size: 22rpx;
						line-height: 22rpx;
						margin-left: 262rpx;
					}
				}
				.p{
					width:565rpx;
					color: #ACACAC;
					font-size: 22rpx;
					line-height: 22rpx;
					margin-top: 9rpx;
				}
			}
		}
	}
}

</style>
