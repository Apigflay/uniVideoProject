<template>
	
	<view class="tabbarArea">
		<view class="per" v-for="(item,index) in tabData" :key="index" :id="index" @click="getClickPer">
			<image class="img" :src="item.imgsrcW" mode="" v-if="index==tabbarCurrent"></image>
			<!-- <image class="img" :src="item.imgsrcB" mode="" v-else></image> -->
			<image class="img" :src="item.imgsrcB" mode="" v-if="index!=tabbarCurrent"></image>
		</view>
	</view>

</template>

<script>
	export default {
		name:'tabbar',
		data() {
			return {
				loginData:null,//loginMsg
				isAnchor:null,//是否为主播
				tabbarCurrent:0,//点击的索引
				tabData:[{imgsrcB:'../../static/imgs/room1w.png',imgsrcW:'../../static/imgs/fang1y.png',text:'1'},
					{imgsrcB:'../../static/imgs/fang1w.png',imgsrcW:'../../static/imgs/fang1y.png',text:'2'},
					{imgsrcB:'../../static/imgs/mail1w.png',imgsrcW:'../../static/imgs/mail1y.png',text:'3'},
					{imgsrcB:'../../static/imgs/xin1w.png',imgsrcW:'../../static/imgs/xin1y.png',text:'4'},
					{imgsrcB:'../../static/imgs/my1w.png',imgsrcW:'../../static/imgs/my1y.png',text:'5'}],//
				};
		},
		created() {//监听页面加载，其参数为上个页面传递的数据，参数类型为Object
				this.getLoginMsg()
		},
		methods:{
			getLoginMsg:function(){
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.loginData = JSON.parse(res.data);
						if(that.loginData.isAnchor==false){
							that.isAnchor = false;
						}
						
					}
				});
			},
			getClickPer:function(e){
				this.tabbarCurrent=e.currentTarget.id;
				console.log(e.currentTarget.id)
				// if(e.currentTarget.id==4){
				// 	if(this.isAnchor==false){
				// 		console.log("不是主播")
				// 		uni.navigateTo({
				// 			url: '/pages/my/my'
				// 		});
				// 	}else{
				// 		console.log("是主播")
				// 		uni.navigateTo({
				// 			url: '/pages/anchorme/anchorme'
				// 		});
				// 	}
				// }else if(e.currentTarget.id==0){
				// 	console.log("首页点击")
				// 	uni.navigateTo({
				// 		url: '/pages/home/home'
				// 	});
				// }else if(e.currentTarget.id==1){
				// 	console.log("搜索点击")
				// 	uni.navigateTo({
				// 		url: '/pages/search/search'
				// 	});
				// }else if(e.currentTarget.id==2){
				// 	console.log("信息点击")
				// 	uni.navigateTo({
				// 		url: '/pages/news/news'
				// 	});
				// }else if(e.currentTarget.id==3){
				// 	console.log("聊天点击")
				// }
			}
			
		}
	}
</script>

<style lang="scss">
.tabbarArea{
	height: 90rpx;
	width: 100%;
	position: fixed;
	bottom: 0;
	left: 0;
	z-index: 9999;
	display: flex;
	justify-content: space-around;
	align-items: center;
	background:rgba(25,25,25,1);
	.per{
		width: 54rpx;
		height: 50rpx;
		.img{
			width: 54rpx;
			height: 50rpx;
		}
	}
}
</style>
