<template>
	<view class="content">
		<!-- tab -->
		<view class="tab">
			<view :class="index==current?'tabActive':''" class="item" v-for="(item,index) in tabData" :key="index" :id="index" @click="changeTab">
				{{item}}
			</view>
		</view>
		<!-- swiper -->
		 <view>
	<!-- 		<view class="uni-padding-wrap">
				<view class="page-section swiper">
					<view class="page-section-spacing"> -->
						<swiper class="swiper" :current="current" :circular="circular" :indicator-dots="indicatorDots" :autoplay="autoplay" :interval="interval" :duration="duration" @animationfinish="getChangeMsg">
							<swiper-item>
								<view class="swiper-item uni-bg-red">AAAAAAA</view>
							</swiper-item>
							<swiper-item>
								<view class="swiper-item uni-bg-green">BBBBBB</view>
							</swiper-item>
							<swiper-item>
								<view class="swiper-item uni-bg-blue">CCCCC</view>
							</swiper-item>
							<swiper-item>
								<view class="swiper-item uni-bg-black">DDDDD</view>
							</swiper-item>
						</swiper>
<!-- 					</view>
				</view>
			</view> -->
			
		</view>
	</view>
	
</template>

<script>
	export default {
		data() {
			return {
				tabData:['推荐','全球榜','礼物榜','月榜'],//
				background: ['color1', 'color2', 'color3'],
				indicatorDots: false,//指示点显示
				autoplay: false,//自动播放
				interval: 2000,//间隔
				duration: 500,//动画时长
				circular:true,//衔接
				current:0,//当前activity的滑块
			};
		},
		methods:{
			changeTab:function(e){
				// console.log(e.currentTarget.id);
				this.current=e.currentTarget.id;
				// console.log(this.current)
			},
			getChangeMsg:function(e){
				// console.log(e)
				// console.log(e.detail.current)
				this.current=e.detail.current;
			}
			


		}
	}
</script>

<style lang="scss">
.content{
	.tab{
		display: flex;
		justify-content: space-around;
		align-items: center;
		background: #cecece;
		.item{
			padding: 5px 0;
		}
		.tabActive{
			border-bottom: 3px solid fuchsia;
		}
	}
	.swiper{
		// background: #cecece;
	}
}
</style>
