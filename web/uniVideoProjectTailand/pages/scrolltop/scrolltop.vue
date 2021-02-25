<template>
	<view class="content">
		<view class="box">
				<!-- <view class="uni-padding-wrap">
					<view class="uni-title">เดท：{{pT_year}}ปี{{pT_month}}ดวงจันทร์{{pT_day}}วันอาทิตย์</view>   
				</view> -->
				<view class="pT_black">
					
				</view>
				<view class="pT_sure">
					<text class="textL">ยกเลิก</text>
					<text class="textR">ยืนยัน</text>
				</view>
				<picker-view class="pT_choose" v-if="pT_visible" :indicator-style="pT_indicatorStyle" :value="pT_value" @change="bindChange">
					
					<picker-view-column>
						<view class="item" v-for="(item,index) in pT_years" :key="index">{{item}}ป</view>
					</picker-view-column>
					<picker-view-column>
						<view class="item" v-for="(item,index) in pT_months" :key="index">{{item}}ดวงจันทร์</view>
					</picker-view-column>
					<picker-view-column>
						<view class="item" v-for="(item,index) in pT_days" :key="index">{{item}}วันอาทิตย์</view>
					</picker-view-column>
				</picker-view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			const pT_date = new Date()
			const pT_years = []
			const pT_year = pT_date.getFullYear()
			const pT_months = []
			const pT_month = pT_date.getMonth() + 1
			const pT_days = []
			const pT_day = pT_date.getDate()
			for (let i = 1919; i <= pT_date.getFullYear(); i++) {
				pT_years.push(i)
			}
			for (let i = 1; i <= 12; i++) {
				pT_months.push(i)
			}
			for (let i = 1; i <= 31; i++) {
				pT_days.push(i)
			}
			return {
				title: 'picker-view',
				pT_years,
				pT_year,
				pT_months,
				pT_month,
				pT_days,
				pT_day,
				pT_value: [9999, pT_month - 1, pT_day - 1],
				pT_visible: true,
				pT_indicatorStyle: `height: ${Math.round(uni.getSystemInfoSync().screenWidth/(750/100))}px;`,
				
				tabbarLoginLanguage: null, // 用户语言
				style:"",
			};
		},
		onLoad() {//监听页面加载，其参数为上个页面传递的数据，参数类型为Object
			// console.log("页面加载")
			// this.getSwiperData()
			// this.getLoginlanger(); // 获取语言
		},
		onShow(){//监听页面显示。页面每次出现在屏幕上都触发，包括从下级页面点返回露出当前页面	
			// console.log("页面显示")
		},
		onReady(){//监听页面初次渲染完成。注意如果渲染速度快，会在页面进入动画完成前触发
			// console.log("初次渲染完成")
		},
		onHide(){//监听页面隐藏
			// console.log("页面隐藏")
		},
		onUnload(){//监听页面卸载
			// console.log("页面卸载")
		},
		onPullDownRefresh(){
			// console.log("页面下拉")
		},
		onReachBottom(){
			// console.log("页面上啦")
		},
		onPageScroll(){

			if(document.documentElement.scrollTop>=100){
				this.style="background:#000;color:#fff"
			}else{
				this.style=""
			}
		},
		methods:{
			bindChange: function (e) {
				const val = e.detail.value
				this.pT_year = this.pT_years[val[0]]
				this.pT_month = this.pT_months[val[1]]
				if(this.pT_month==4||this.pT_month==6||this.pT_month==9||this.pT_month==11){//4 6 9 11 月
					if(this.pT_days[val[2]]==31){
						this.pT_day = 30
					}else{
						this.pT_day = this.pT_days[val[2]]
					}
				}else if(this.pT_month==2){//2月单独处理
					if(this.pT_years[val[0]]%4==0&&this.pT_years[val[0]]%100!=0){//闰年
						if(this.pT_days[val[2]]==30||this.pT_days[val[2]]==31){
							this.pT_day = 29
						}else{
							this.pT_day = this.pT_days[val[2]]
						}
					}else{//不是闰年
						if(this.pT_days[val[2]]==29||this.pT_days[val[2]]==30||this.pT_days[val[2]]==31){
							this.pT_day = 28
						}else{
							this.pT_day = this.pT_days[val[2]]
						}
					}
				}else{// 1 3 5 7 8 10 12月
					this.pT_day = this.pT_days[val[2]]
				}
				
			},
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
}
.content{
	height: 100%;
	width: 100%;
	//-----------
	.box{
		height: 100%;
		width: 100%;
		.pT_black{
			height: 55%;
			width: 100%;
			// background: rgba(0,0,0,0.4);
			background: #000;
			opacity: 0.7;
		}
		.pT_sure{
			height: 5%;
			width: 100%;
			position:relative;
			.textR{
				line-height: 100%;
				position: absolute;
				bottom: 0rpx;
				right: 60rpx;
				padding: 5rpx;
				color: blue;
				
			}
			.textL{
				line-height: 100%;
				position: absolute;
				bottom: 0rpx;
				left: 60rpx;
				padding: 5rpx;
				
			}
		}
		.pT_choose{
			height: 40%;
			width: 100%;
		}
	}
	//--------------
	
}
</style>
