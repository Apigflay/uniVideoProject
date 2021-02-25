<template>
	<view class="main">
		<view class="top">
			<image src="../../static/pictures/back_1.png" class="img" @click="goback()"></image>
			<text>{{this.Language.language[this.tabbarLoginLanguage].language189}}</text>
		</view>
		<view class="center">
			<view class="list" v-for="(item,index) in model1Data" :key="index" :id="index">
				<image :src="item.SmallPic" class="photo" ></image>
				<text class="p">{{item.MyName}}</text>
				<view :class="item.change == 1?'button':'button1'" @click="unclock(item,index)"><text class="p1">{{fy_unlock[tabbarLoginLanguage]}}</text></view>
			</view>
		</view>
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,sendD07,sendD09,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				tabbarLoginLanguage: null, // 用户语言
				page: 1, // 页面加在数据时传递的页面
				dislike: false, // 取消拉黑向接口传递的状态值
				UserIdx: null, // 用户IDX
				tabbarLoginData: null, // 获取页面初始化信息
				model1Data: null, // 获取页面信息的变量
				model2Data: null, // 页面下拉获取更新信息的变量
				
				fy_unlock:['解除封锁','解除封鎖','Unlock ban','ปลดล็อค'],
			};
		},
		onLoad:function() {//监听页面加载，其参数为上个页面传递的数据，参数类型为Object
				// var option=JSON.parse(decrypt(decodeURIComponent(obj.action)));
				this.getLoginlanger(); // 获取语言
				this.getLoginMsg(); // 获取用户登录信息
				this.getInitMsg(); // 初始化加载页面列表信息
		},
		methods:{
			getLoginlanger:function(){ // 获取当前语言
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.tabbarLoginLanguage = JSON.parse(res.data);
						// console.log(that.tabbarLoginLanguage);
					}
				});
			},
			//----------------------获取当前页面用户信息-----------------------------------
			getLoginMsg:function(){ // 登录信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.tabbarLoginData = JSON.parse(res.data);
						// console.log(that.tabbarLoginData.useridx);
						that.UserIdx = that.tabbarLoginData.useridx;
						if(that.tabbarLoginData.isAnchor==false){
							that.isAnchor = false;
						}
					}
				});
			},
			//----------------------获取当前页面用户信息-----------------------------------
			
			//----------------------获取页面数据信息---------------------------------------
			getInitMsg:function(){ // 获取搜索页面传来的参数
				console.log(Number(this.UserIdx));
				var array=base64ToArrayBuffer(encrypt(JSON.stringify({
					userIdx: Number(this.UserIdx),//	int
					Page: this.page
				})))
				var res = JSON.parse(decrypt(sendData('POST',this.GLOBAL.urlPoint+'/userinfo/GetBlackList',array)));
				console.log(res)
				if(res.code==100){
					// console.log(res.data);
					this.model1Data = res.data;
				}
			},
			//----------------------获取页面数据信息---------------------------------------
			
			//-----------------------页面拉黑功能-----------------------------------
			
			unclock: function(e,f){ // 解锁按钮
				// console.log(e.UserIdx)
				// console.log(f)
				var array =JSON.stringify({
					"useridx": Number(this.UserIdx),
					"toidx": Number(e.UserIdx),
					"status": this.dislike
				})
				// console.log({
				// 	"useridx": Number(this.UserIdx),
				// 	"toidx": Number(e.UserIdx),
				// 	"status": this.dislike
				// })
				// console.log(array) //{"useridx":10009829,"toidx":"11000000","status":false}
				var arr = sendD09(array);
				// console.log(arr) // send07 加密后的 array数据
				// alert(this.$Socket.isconnect)
				// console.log(this.$Socket)
				if(this.$Socket.isconnect==false){
					this.$Socket.onReload()
				}
				var that = this;
				this.$Socket.nsend(arr)  //放射一个数据到服务器
				this.$Socket.eventPatch.onMsg((msg,sk)=>{    //监听是否接受消息
					setTimeout(function() {
					// console.log(msg)
					// console.log(sk)
						 var fileReader = new FileReader();
						 fileReader.onload = function (progressEvent) {
							var arrayBuffer = this.result; // arrayBuffer即为blob对应的arrayBuffer
							var HeadRecv = new Uint32Array(arrayBuffer, 0, 3);
							var strArray = new Uint8Array(arrayBuffer, 12, HeadRecv[0] - 12 - 1);
							var str = new TextDecoder().decode(strArray);//{"code":-1,"error":"用户名或密码错误"}
							if(HeadRecv[1]==10010){
								
								// navigateTo('/pages/startup/startup',null);
							}else if(HeadRecv[1]==11010){
								if(that.tabbarLoginLanguage == 0 || that.tabbarLoginLanguage == 1){
									var str1 = '成功'
								}else if(that.tabbarLoginLanguage == 2){
									var str1 = 'success'
								}else if(that.tabbarLoginLanguage == 3){
									var str1 = 'ประสบความสำเร็จ'
								}
								uni.showToast({
									title: str1,
									duration: 1000,
									icon:"none",
								});
								that.model1Data.splice(f,1); // 删除当前解除封锁的主播数据
								// console.log(that.model1Data);
							}
							// console.log(HeadRecv[1])
							// console.log(JSON.parse(str))
						 };
						 fileReader.readAsArrayBuffer(msg.data);
					}, 0);
				});
				// 仅限于用户没有退出页面的情况下 动态调整 拉黑与解除
				if(this.dislike == true){
					this.dislike = false; 
				}else{
					this.dislike = true;
				}
			
			},
			
			//-----------------------页面拉黑功能-----------------------------------
			
			goback: function(){
				// uni.navigateTo({
				// 	url: "/pages/dataset/dataset"
				// })
				navigateTo('/pages/dataset/dataset',null);
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
			position: relative;
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
				position: absolute;
				left: 150rpx;
				color: #747474;
				font-size:27rpx;
				line-height: 50rpx;
				
				max-width: 300rpx;
				overflow: hidden;
				text-overflow:ellipsis;
				white-space: nowrap;
			}
			.button{
				width:150rpx;
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
				width:150rpx;
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
