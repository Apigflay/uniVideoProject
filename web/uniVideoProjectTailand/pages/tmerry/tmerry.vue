<template>
	<view class="content">
		<image class="more111" src="../../static/imgs/close1.png" mode="" @click="goBackPage"></image>
		<input class="text" id="input1" type="text" ref="text"  :value="item" />
		<view class="maski" @click="makePerUrl"></view>
		<image class="bgImg" src="../../static/imgs/newyear_e.png" mode="" v-if="loginLanguage==0||loginLanguage==1||loginLanguage==2"></image>
		<image class="bgImg" src="../../static/imgs/newyear_t.png" mode="" v-if="loginLanguage==3"></image>
		<!-- <view class="textArea" v-if="loginLanguage==0||loginLanguage==1||loginLanguage==2">
			<view class="b"><b>Celebrate New Year，give coins for free</b></view>
			<view class="nor">Invite friends into the SM circle , play some exciting things together!</view>
			<view class="normal">In WOOPS, coins can be used:</view>
			<view class="normal">Unlock exciting private videos</view>
			<view class="normal normal3">Send gifts to tune your girlfriend and let her play with props in front of you</view>
			<view class="copyArea">
				<view class="top">Give <image class="img" src="../../static/imgs/goldM1.png" mode=""></image>100，Get<image class="img" src="../../static/imgs/goldM1.png" mode=""></image>200</view>
				<view class="norSize" >One-click copy the exclusive invitation link, go to share with your friends!</view>
				<view class="norSize">Invite more, get more coins ,up to 1,000 coins every day!</view>
				<view class="norSize">SM loved by high-class players, let's explore the new world together!</view>
				<view class="btn" @click="makePerUrl">copy</view>
			</view>
			<view class="normalleft">Activity rules</view>
			<view class="normalleft">1. Old users can get 100 coins by inviting friends to register WOOPS, new users who register successfully will get 200 coins</view>
			<view class="normalleft">2. Activity time: Jan. 1st, 2020-Jan.7th, 2020</view>
			<view class="normalrules">The final interpretation right of the event belongs to this platform</view>
		</view> -->
		<!-- <view class="textArea" v-if="loginLanguage==3">
			<view class="b">ให้เหรียญฉลองปีใหม่ฟรี</view>
			<view class="nor">ชวนเพื่อมาเข้า SM เล่นอะไรหวาดเสียวด้วยกันสิ！</view>
			<view class="normal">ใน WOOPS เหรียญสามารถใช้ ：</view>
			<view class="normal">ปลดล็อควิดีโอลับเฉพาะ</view>
			<view class="normal normal3">ส่งของขวัญให้แฟนสาว ให้เธอเล่นของเล่นต่อหน้าคุณ</view>
			<view class="copyArea">
				<view class="top">Give <image class="img" src="../../static/imgs/goldM1.png" mode=""></image>100，Get<image class="img" src="../../static/imgs/goldM1.png" mode=""></image>200</view>
				<view class="norSize">กดลิ้งค์คลิ๊กเดียวเพื่อชวนเพื่อนโดยเฉพาะ รีบแชร์ให้เพื่อสิ!</view>
				<view class="norSize">ยิ่งชวนมากยิ่งได้มาก แต่ละวันได้มากสุด 1,000 เหรียญ!</view>
				<view class="norSize">ผู้เล่นที่ไฮโซ ไปค้นหาโลกใบใหม่ด้วยกัน!</view>
				<view class="btn" @click="makePerUrl">คัดลอก</view>
			</view>
			<view class="normalleft">รายละเอียดกิจกรรม</view>
			<view class="normalleft">1. ผู้ใช้เก่าทุกครั้งที่ชวนเพื่อนมาลงทะเบียน WOOPS จะได้ 100 เหรียญ ผู้ใช้ใหม่ที่ถูกเชิญมาลงทะเบียนได้รับทันที 200 เหรียญ</view>
			<view class="normalleft">2. เวลากิจกรรม ：วันที่ 1 -7 มกราคม 2563</view>
			<view class="normalrules">กิจกรรมนี้สงวนลิขสิทธิ์โดยแพลตฟอร์มนี้</view>
		</view> -->
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,regMail,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				loginLanguage:null,//语言 用户语言
				loginData:null,//登录信息
				item:'',//
			};
		},
		onLoad() {
			this.getLoginLanguage(); // 获取语言
			this.getLoginData()//
		},
		methods:{
			getLoginData:function(){//获取socket成功信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_str',
					success: function (res) {
						that.loginData = JSON.parse(res.data);
						that.item = 'https://www.woopsss.com/#/?InvitationCode='+that.loginData.useridx;
					}
				});
			},
			getLoginLanguage:function(){//获取socket成功信息
				var that = this;
				uni.getStorage({
					key: 'storage_login_language',
					success: function (res) {
						that.loginLanguage = JSON.parse(res.data);
					}
				});
			},
			goBackPage:function(){
				navigateTo('/pages/home/home',null);
			},
			makePerUrl:function(){//制作自己的专属链接
				var controls = document.getElementsByTagName('input');
				controls[0].select(); //选择对象 
				document.execCommand("Copy"); //执行浏览器复制命令
				if(this.loginLanguage==0||this.loginLanguage==1){
				   var str = "已复制好，可贴粘";
				   uni.showToast({
						title:str,
						icon:"none"
				   })
				}else if(this.loginLanguage==2){
				   var str = "Copied and pasted";
				   uni.showToast({
						title:str,
						icon:"none"
				   })
				}else if(this.loginLanguage==3){
				   var str = "มันถูกคัดลอกและติด";
				   uni.showToast({
						title:str,
						icon:"none"
				   })
				}
				
			}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	// background:rgba(35,35,35,1);#191919
	// background:#21317E;
}
.content{
	width: 100%;
	height: 100%;
	position: relative;
	.more111{
		position: absolute;
		z-index: 100009;
		width: 36rpx;
		height: 36rpx;
		padding: 10rpx;
		top: 28rpx;
		right: 28rpx;
		background: #fff;
		
	}
	.bgImg{
		width: 750rpx;
		height: 3260rpx;
	}
	.text{
		z-index: 10;
		width: 532rpx;
		height: 50rpx;
		position: absolute;
		left:70rpx;
		top: 2260rpx;
		color: #fff;
		font-size: 24rpx;
		background: #9a0a1e;
		outline: none;
		border: 0;
	}
	.maski{
		z-index: 11;
		width: 74rpx;
		height: 76rpx;
		position: absolute;
		left:614rpx;
		top: 2250rpx;
	}
	.textArea{
		width: 650rpx;
		padding:100rpx 50rpx;
		// height: 100%;
		text-align: center;
		.b{
			font-size: 40rpx;
		}
		.nor{
			margin-top: 40rpx;
			margin-bottom: 40rpx;
			font-size: 32rpx;
		}
		.normal{
			margin-top: 10rpx;
			font-size: 32rpx;
		}
		.normal3{
			margin-top: 20rpx;
			font-size: 32rpx;
		}
		.copyArea{
			width: 600rpx;
			padding: 25rpx;
			margin-top: 100rpx;
			margin-bottom: 30rpx;
			color: #fff;
			background-image: linear-gradient(to top right, #4342A0, #6469E1);
			font-size: 32rpx;
			text-align: left;
			.top{
				display: flex;
				align-items: center;
				font-size: 40rpx;
				margin-bottom: 20rpx;
				.img{
					margin:0 10rpx;
					width: 40rpx;
					height: 40rpx;
				}
			}
			.norSize{
				font-size: 32rpx;
				margin-bottom: 10rpx;
			}
			.btn{
				margin-top: 40rpx;
				margin-bottom: 20rpx;
				background: #45449F;
				text-align: center;
				padding: 20rpx 0;
				font-size: 40rpx;
			}
		}
		.normalleft{
			margin-top: 20rpx;
			font-size: 32rpx;
			text-align: left;
		}
		.normalrules{
			font-size: 32rpx;
			margin-top: 200rpx;
			// font-weight: 600;
		}
	}
	

}
</style>
