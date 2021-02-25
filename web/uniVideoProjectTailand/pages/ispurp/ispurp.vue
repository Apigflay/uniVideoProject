<template>
	<view class="main">
		<text class="p">{{this.Language.language[this.tabbarLoginLanguage].language162}}</text>
		<input class="input" placeholder-style="color:#ACACAC;" :placeholder="place_name[tabbarLoginLanguage]" v-model="input" />
		<view class="p1">{{this.Language.language[this.tabbarLoginLanguage].language164}}</view>
		
		
		<view class="uni-list">
			<view class="uni-list-cell">
				<view class="uni-list-cell-db">
					<picker mode="date" :value="date" :start="startDate" :end="endDate" @change="bindDateChange">
						<image class="img" src="../../static/pictures/18_1.png"></image>
						<view class="uni-input">{{date}}</view>
					</picker>
				</view>
				<view class="p2" v-if="red">{{this.Language.language[this.tabbarLoginLanguage].language165}}</view>
			</view>
		</view>
		<view class="like">{{this.Language.language[this.tabbarLoginLanguage].language166}}</view>
		<view class="p3">{{this.Language.language[this.tabbarLoginLanguage].language167}}</view>
		<view class="sex">
			<image v-if="woman" @click="w_chose()" class="woman" src="../../static/pictures/cwoman_1.png"></image>
			<image v-else @click="w_chose()" class="woman" src="../../static/pictures/woman_1.png"></image>
			<image v-if="man" @click="m_chose()" class="man" src="../../static/pictures/cman_1.png"></image>
			<image v-else @click="m_chose()" class="man" src="../../static/pictures/man_1.png"></image>
		</view>
		<view class="sex_p">
			<text class="woman">{{this.Language.language[this.tabbarLoginLanguage].language121}}</text>
			<text class="man">{{this.Language.language[this.tabbarLoginLanguage].language136}}</text>
		</view>
		<view class="chose" @click="clause()">
			<image v-if="box" class="chose_img" src="../../static/pictures/chose_1.png"></image>
			<image v-else class="chose_img" src="../../static/pictures/choseno_1.png"></image>
			<view  class="chose_p">{{this.Language.language[this.tabbarLoginLanguage].language168}}</view>
		</view>
		<view @click="sub()" class="button">{{this.Language.language[this.tabbarLoginLanguage].language169}}</view>
	</view>
</template>

<script>
	export default {
		data() {
			const currentDate = this.getDate({
			            format: true
			        })
			return {
				tabbarLoginLanguage: null, // 用户语言
				input: '',
				index: 0,
				date: currentDate,
				woman: true, // 女性
				man: false, // 男性
				like: null ,//提交事件中的喜欢类型
				box: false ,// 用户是否同意条款
				red: null ,// 判断是否满足18岁
				
				place_name:['@请输入您的用户名称','@請輸入您的用戶名稱','@Please enter your username','@กรุณาใส่ชื่อบัญชีของคุณ'],
			}
		},
		onLoad(){
			this.getLoginlanger(); // 获取语言
		},
		computed: {
		        startDate() {
		            return this.getDate('start');
		        },
		        endDate() {
		            return this.getDate('end');
		        }
		    },
		methods: {
			getLoginlanger:function(){ // 获取当前语言
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.tabbarLoginLanguage = JSON.parse(res.data);
						console.log(that.tabbarLoginLanguage);
					}
				});
			},
			// 日期选择函数
			bindPickerChange: function(e) { 
				console.log('picker发送选择改变，携带值为', e.target.value)
				this.index = e.target.value
			},
			bindDateChange: function(e) {
				this.date = e.target.value;
				var time =  new Date(this.date); // 算则的日期
				var time1 = time.getTime(); //选择的日期改成了时间戳格式
				var nowdate = new Date().getTime(); // 获取得到的当前时间
				var age = nowdate-time1; // 年龄时间戳
				var num = 24*60*60*1000*365*18; // 18岁时间戳
				if(age>num){
					this.red = false;
				}else{
					this.red = true;
				}
			},
			        
			getDate(type) {
				var date = new Date();
				var year = date.getFullYear();
				var month = date.getMonth() + 1;
				var day = date.getDate();
	
				if (type === 'start') {
					year = year - 60;
				} else if (type === 'end') {
					year = year + 2;
				}
				month = month > 9 ? month : '0' + month;;
				day = day > 9 ? day : '0' + day;
				return `${year}-${month}-${day}`;
			},
			
			// 选择喜欢类型的按钮事件
			w_chose:function(){
				if(this.woman == true){
					this.woman = false;
				}else{
					this.woman = true;
				}
			},
			m_chose:function(){
				if(this.man == false){
					this.man = true;
				}else{
					this.man = false;
				}
			},
			//同意调控按钮
			clause:function(){
				if(this.box == false){
					this.box = true;
				}else{
					this.box = false;
				}
			},
			// 完成按钮
			sub:function(){
				console.log(2222)
				if(this.man == true && this.woman == true){
					this.like = 2;
				} else if(this.man == false && this.woman == true){
					this.like = 0;
				}else if(this.man == true && this.woman == false){
					this.like = 1;
				}else{
					uni.showModal({
					    title: '提示',
					    content: '默认选择女性',
					    success: function (res) {
					        if (res.confirm) {
					            console.log('用户点击确定');
								this.like = 0;
								console.log(this.like)
					        } else if (res.cancel) {
					            console.log('用户点击取消');
					        }
					    }
					});
					
				}
				console.log(this.like)
			},
			
			
			
			
		}
	}
	
</script>

<style lang="scss">
page{
	width: 750rpx;
	height: 980rpx;
	background: #2C405A;
}
.main{
	position:fixed;
	bottom: 0rpx;
	width: 750rpx;
	height: 980rpx;
	background: #FFFFFF;
	border-radius:12rpx 12rpx 0rpx 0rpx;
	display: flex;
	flex-direction:column;
	align-items:center;
	.p{
		color: #595959;
		font-size: 32rpx;
		line-height:40rpx;
		text-align: center;
		padding: 43rpx 311rpx 60rpx 311rpx;
	}
	.input{
		width:360rpx;
		color: #ACACAC;
		font-size: 30rpx;
		line-height:40rpx;
		background-color: #F6F6F6;
		border-radius: 10rpx;
		padding: 21rpx 105rpx 21rpx 105rpx;
		background-image: url(../../static/pictures/geren_1.png);
		background-repeat: no-repeat; /*设置图片不重复*/
		background-position: left; /*图片显示的位置*/
		background-position:20rpx; // 设置图片位置
		padding-left: 68rpx; //设置搜索文字位置
		background-size: 30rpx 30rpx; // 搜索图标的大小
	}
	.p1{
		color: #ACACAC;
		font-size: 22rpx;
		line-height: 22rpx;
		margin-top: 15rpx;
	}
	.uni-list{
		width:535rpx;
		padding:49rpx 105rpx 0rpx 105rpx;
		.uni-list-cell{
			.uni-list-cell-db{
				border-radius: 10rpx;
				background-color:#F6F6F6;
				padding:20rpx;
				.uni-input{
					color:#ACACAC;
					font-size: 30rpx;
					display: inline-block;
				}
				.img{
					width: 30rpx;
					height: 30rpx;
					display: inline-block;
					margin-right: 20rpx;
				}
			}
			.p2{
				font-size: 22rpx;
				line-height: 22rpx;
				color:#FF0000;
				margin-top: 15rpx;
			}
		}
	}
	.like{
		color: #191919;
		font-size: 30rpx;
		line-height: 30rpx;
		margin-top: 63rpx;
	}
	.p3{
		color: #ACACAC;
		font-size: 26rpx;
		line-height: 26rpx;
		margin-top: 23rpx;
	}
	.sex{
		height: 120rpx;
		padding:42rpx 197rpx 15rpx 200rpx;
		.man{
			width: 120rpx;
			height: 120rpx;
			margin-left: 113rpx;
		}
		.woman{
			width: 120rpx;
			height: 120rpx;
		}
	}
	.sex_p{
		line-height: 24rpx;
		.woman{
			color: #ACACAC;
			font-size: 24rpx;
			line-height: 24rpx;
		}
		.man{
			color: #ACACAC;
			font-size: 24rpx;
			line-height: 24rpx;
			margin-left: 187rpx;
		}
	}
	.chose{
		display:flex;
		align-items: center;
		justify-content: center;
		margin-top: 55rpx;
		.chose_img{
			width: 24rpx;
			height: 24rpx;
		}
		.chose_p{
			margin-left: 12rpx;
			color: #ACACAC;
			font-size: 24rpx;
			line-height: 24rpx;
		}
	}
	.button{
		position: fixed;
		bottom: 0rpx;
		width: 750rpx;
		height: 70rpx;
		background: #ACACAC;
		font-size: 30rpx;
		line-height: 30rpx;
		color: #FFFFFF;
		display: flex;
		align-items: center;
		justify-content: center;
		
	}
	
}
</style>
