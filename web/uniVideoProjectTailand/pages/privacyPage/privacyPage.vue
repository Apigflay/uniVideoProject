<template>
	<view class="main">
		<view v-show="show" class="top" style="width: 100%; height: 90rpx;"><image class="back" src="../../static/pictures/delete_1.png" @click="back()"></image></view>
		<view><image class="img" src="../../static/pictures/logo_1.png"></image></view>
		<view><image class="title" src="../../static/pictures/WOOPS.png"></image></view>
		<view class="content" v-show="tabbarLoginLanguage == 0">
			<b>隱私權政策</b></br>
			本隱私政策說明當您使用我們的網站、手機應用程式或其他線上產品和服務（以下統稱為「服務」），或者當您以其他方式與我們互動時，您的資訊將如何被收集並使用於「WOOPS」。我們可能會隨時更改此隱私權政策，因此我們鼓勵您在使用服務時，隨時查看並了解我們最新的隱私權政策，以便幫助保護您的資訊隱私。 </br>
			您所提供給我們的資訊</br>
			我們會收集您直接提供給我們的資訊。例如，我們會收集所有您創建帳戶時的資訊，以及使用服務來發送或接收的訊息（包含透過我們的服務「WOOPS」所拍攝的照片或影片，以及透過客服或與其他方式與我們溝通時所產生的訊息。）我們可能收集的資訊包含用戶名稱、帳戶密碼、電子郵件地址、電話號碼、年齡、性別以及任何您選擇提供的其他資訊。</br>
			我們誠心地建議您提供您擁有版權的內容（例如訊息、照片、影片、標題）。我們無法防範他人儲存您的內容（例如拍攝截圖），如果您不希望有心人士儲存您的某些內容，那麼您不應使用 WOOPS 發送該內容。</br>
			我們在您使用服務時所收集的資訊 </br>
			當您使用我們的服務時，我們會自動收集您的以下資訊：</br>
			使用紀錄：當您透過我們的服務發送或接收訊息，我們會收集這些訊息資訊，包括時間、日期、發件人和收件人等資訊。我們還會收集您發送與接收的訊息數量，以及您較常傳送訊息的朋友名單。我們也會收集您使用我們的服務時的資訊，包含您的瀏覽器類型、語言、使用時間、IP 位址等。</br>
			裝置資訊：我們會收集您使用我們的服務時使用的裝置資訊，包括裝置類型、操作系統和版本、設備標識碼、電話號碼和行動網絡資訊。此外，本服務在經過您的同意之下，可取用您裝置中的電話簿和影像存儲應用程式，以方便支援您在使用本服務時的功能。位置資訊：在經過您的同意之下，我們會收集有關您的裝置位置資訊，以方便支援您在使用本服務時的功能。</br>
			數據追蹤：我們會使用各種數據追蹤技術來收集資訊，來幫助我們來改進我們的服務和您的使用體驗。</br>
			<b>資訊使用</b></br>
			我們會將您的資訊運用於各種目的，包含：</br>
			•	提供我們的服務並且持續維護與改善，包含提供額外的功能與內容；</br>
			•	回應您的意見、提問與需求，並提供客戶服務；</br>
			•	寄送技術通告、版本更新、安全警示與服務，以及營運訊息；</br>
			•	提供您關於產品、服務、優惠、促銷、獎勵以及任何您可能感興趣的 WOOPS 相關活動資訊；</br>
			•	使用我們的服務時的活動監測與數據分析；</br>
			•	偵測、調查或防止詐欺與非法活動；</br>
			•	其他收集資訊的目的；</br>
		</view>
		
		<view class="content" v-show="tabbarLoginLanguage == 1">
			<b>隱私權政策</b></br>
			本隱私政策說明當您使用我們的網站、手機應用程式或其他線上產品和服務（以下統稱為「服務」），或者當您以其他方式與我們互動時，您的資訊將如何被收集並使用於「WOOPS」。我們可能會隨時更改此隱私權政策，因此我們鼓勵您在使用服務時，隨時查看並了解我們最新的隱私權政策，以便幫助保護您的資訊隱私。 </br>
			您所提供給我們的資訊</br>
			我們會收集您直接提供給我們的資訊。例如，我們會收集所有您創建帳戶時的資訊，以及使用服務來發送或接收的訊息（包含透過我們的服務「WOOPS」所拍攝的照片或影片，以及透過客服或與其他方式與我們溝通時所產生的訊息。）我們可能收集的資訊包含用戶名稱、帳戶密碼、電子郵件地址、電話號碼、年齡、性別以及任何您選擇提供的其他資訊。</br>
			我們誠心地建議您提供您擁有版權的內容（例如訊息、照片、影片、標題）。我們無法防範他人儲存您的內容（例如拍攝截圖），如果您不希望有心人士儲存您的某些內容，那麼您不應使用 WOOPS 發送該內容。</br>
			我們在您使用服務時所收集的資訊 </br>
			當您使用我們的服務時，我們會自動收集您的以下資訊：</br>
			使用紀錄：當您透過我們的服務發送或接收訊息，我們會收集這些訊息資訊，包括時間、日期、發件人和收件人等資訊。我們還會收集您發送與接收的訊息數量，以及您較常傳送訊息的朋友名單。我們也會收集您使用我們的服務時的資訊，包含您的瀏覽器類型、語言、使用時間、IP 位址等。</br>
			裝置資訊：我們會收集您使用我們的服務時使用的裝置資訊，包括裝置類型、操作系統和版本、設備標識碼、電話號碼和行動網絡資訊。此外，本服務在經過您的同意之下，可取用您裝置中的電話簿和影像存儲應用程式，以方便支援您在使用本服務時的功能。位置資訊：在經過您的同意之下，我們會收集有關您的裝置位置資訊，以方便支援您在使用本服務時的功能。</br>
			數據追蹤：我們會使用各種數據追蹤技術來收集資訊，來幫助我們來改進我們的服務和您的使用體驗。</br>
			<b>資訊使用</b></br>
			我們會將您的資訊運用於各種目的，包含：</br>
			•	提供我們的服務並且持續維護與改善，包含提供額外的功能與內容；</br>
			•	回應您的意見、提問與需求，並提供客戶服務；</br>
			•	寄送技術通告、版本更新、安全警示與服務，以及營運訊息；</br>
			•	提供您關於產品、服務、優惠、促銷、獎勵以及任何您可能感興趣的 WOOPS 相關活動資訊；</br>
			•	使用我們的服務時的活動監測與數據分析；</br>
			•	偵測、調查或防止詐欺與非法活動；</br>
			•	其他收集資訊的目的；</br>
		</view>
		
		<view class="content" v-show="tabbarLoginLanguage == 2">
			<b>Privacy policy</b></br>
			This Privacy Policy explains how information about you is collected, used by WOOPS when you use our websites, mobile applications and other online products and services (collectively, the "Services") or when you otherwise interact with us. We may change this Privacy Policy from time to time, therefore we encourage you to review the Privacy </br>
			Policy whenever you access the Services to stay informed about our information practices and the ways you can help protect your privacy.</br>
			
			We collect information you provide directly to us. For example, we collect information when you create an account, use the Services to send or receive messages, including photos or videos taken via our Services ("WOOPS”), request customer support or otherwise communicate with us. The types of information we may collect include your username, password, email address, phone number, age, gender and any other information you choose to provide.</br>
			
			We kindly suggest you to provide contents (such as messages, photos, videos, captions) which you own the copyright. We cannot prevent others from making copies of your contents (e.g., by taking a screenshot). You should not use WOOPS to send messages if you want to be certain that the recipient cannot keep a copy.</br>
			Information we receive from others</br>
			In addition to the information you provide us directly, we receive information about you from others, including:</br>
			Usage history: When you send or receive messages through our service, we collect information about these messages, including time, date, sender, and recipient information. We also collect the number of messages you send and receive, and a list of your friends who send messages more often. We also collect information about your use of our services, including your browser type, language, time of use, IP address, etc.</br>
			
			
			Device information: We collect device information that you use when using our services, including device type, operating system and version, device identifier, phone number, and mobile network information. In addition, with your consent, this service can access the phone book and image storage applications on your device to facilitate support for your functions when using this service.</br>
			
			Location information: With your consent, we will collect information about your device's location to help support your functionality when using the service.</br>
			Data tracking: We use various data tracking technologies to collect information to help us improve our services and your experience.</br>
			Use of Information</br>
			We use your information for a variety of purposes, including:</br>
			• Provide our services and ongoing maintenance and improvement, including providing additional features and content;</br>
			• Respond to your comments, questions and needs, and provide customer service;</br>
			• Send technical announcements, version updates, security alerts and services, and operational messages;</br>
			• Provide you with information about products, services, offers, promotions, rewards and any WOOPS-related events that may interest you;</br>
			• Activity monitoring and data analysis when using our services;</br>
			• detect, investigate or prevent fraud and illegal activities;</br>
			• other purposes for which the information was collected;</br>
		</view>
		
		<view class="content" v-show="tabbarLoginLanguage == 3">
			<b>นโยบายความเป็นส่วนตัว</b></br>
				นโยบายความเป็นส่วนตัวนี้หมายความว่าเมื่อคุณใช้เว็บไซต์  แอปพลิเคชั่นในมือถือ หรือผลิตภัณฑ์และบริการทางออนไลน์อย่างอื่น ( หลังจากนี้จะใช้คำว่า “บริการ” ) ของเรา  หรือเวลาที่คุณใช้ช่องทางอื่น ๆ ในการติดต่อกับเรา ข้อมูลของคุณจะถูกรวบรวมหรือใช้ใน Woops  อย่างไร เราอาจจะปรับเปลี่ยนนโยบายความเป็นส่วนตัวนี้ได้ตลอดเวลา  ดังนั้นเวลาคุณใช้บริการ เราแนะนำให้คุณตรวจสอบนโยบายความเป็นส่วนตัวล่าสุด เพื่อรักษาความปลอดภัยของข้อมูลส่วนตัวของคุณ </br>
				
				ข้อมูลที่คุณให้กับเรา</br>
				เราจะรวบรวมข้อมูลที่คุณให้กับเรา เช่น เราจะรวบรวมข้อมูลบัญชีที่คุณสร้าง รวมทั้งข้อมูลที่ใช้บริการรับส่งข้อความ ( รวมไปถึงรูปภาพหรือวิดีโอที่ใช้ Woops ถ่าย และข้อมูลที่เกิดจากการติดต่อเราผ่านช่องทางต่างๆ ) เราอาจจะเก็บข้อมูลที่รวมไปถึงชื่อบัญชี รหัสผ่าน ที่อยู่อีเมล เบอร์โทรศัพท์ อายุ เพศ และข้อมูลอื่น ๆ ที่คุณเลือกที่จะให้ </br>
				เราขอแนะนำให้คุณให้เนื้อหาที่คุณเป็นเจ้าของลิขสิทธิ์ เช่น ข้อความ ภาพถ่าย วิดีโอและชื่อเรื่อง เราไม่สามารถป้องกันไม่ให้ผู้อื่นลอกเลียนแบบเนื้อหาของคุณ เช่น การจับภาพหน้าจอ ถ้าคุณไม่ต้องการให้ผู้อื่นบันทึกเนื้อหาของคุณไว้ คุณไม่ควรใช้  Woops  เพื่อส่งเนื้อหา</br>
				
				ข้อมูลที่เรารวบรวมเมื่อคุณใช้บริการ</br>
				เมื่อคุณใช้บริการของเรา เราจะเก็บข้อมูลต่อไปนี้โดยอัตโนมัติ :</br>
				บันทึกการใช้ :เมื่อคุณส่งหรือรับข้อความผ่านทางบริการของเรา เรารวบรวมข้อมูลเกี่ยวกับเวลา วันที่ ผู้ส่งและผู้รับ นอกจากนี้เรายังรวบรวมปริมาณของข้อความที่คุณส่งและรับ และรายชื่อเพื่อนที่คุณส่งบ่อยครั้ง  เรายังเก็บรวบรวมข้อมูลเกี่ยวกับเวลาที่คุณใช้บริการของเรา รวมถึงชนิดของเบราว์เซอร์ ภาษา เวลาการใช้ ที่อยู่ IP ฯลฯ</br>
				ข้อมูลอุปกรณ์:เมื่อคุณใช้บริการของเรา เราจะเก็บรวบรวมข้อมูลเกี่ยวกับอุปกรณ์ที่คุณใช้ รวมถึงชนิดของอุปกรณ์ ระบบปฏิบัติการ รุ่นรหัสอุปกรณ์ เบอร์โทรศัพท์และข้อมูลเครือข่ายโทรศัพท์ นอกจากนี้ บริการนี้จะช่วยให้คุณสามารถใช้สมุดโทรศัพท์และโปรแกรมจัดเก็บภาพในอุปกรณ์ของคุณได้อย่างสะดวกสบาย</br>
				ข้อมูลตำแหน่ง : เราจะรวบรวมข้อมูลเกี่ยวกับสถานที่ตั้งของอุปกรณ์ของคุณ เพื่อสนับสนุนฟังก์ชั่นในการทำงานในการใช้บริการนี้ภายใต้การยินยอมของคุณ</br>
				ติดตามข้อมูล:เราจะใช้เทคนิคต่าง ๆ รวบรวมข้อมูล เพื่อช่วยปรับปรุงบริการของเราและประสบการณ์การใช้ของคุณ</br>
				
				การใช้ข้อมูล</br>
				เราจะใช้ข้อมูลของคุณเพื่อวัตถุประสงค์ต่างๆ ดังนี้ </br>
				•	ให้บริการของเรา และยังคงรักษา อีกทั้งปรับปรุงบริการ รวมถึงมีฟังก์ชั่นและเนื้อหาเพิ่มเติม</br>
				•	ตอบสนองต่อความคิดเห็น คำถามและความต้องการของคุณ และให้บริการลูกค้า</br>
				•	ส่งการแจ้งเตือนทางด้านเทคนิค การปรับปรุงรุ่น เตือนความปลอดภัยและบริการ และข้อมูลการดำเนินงาน</br>
				•	ให้ข้อมูลเกี่ยวกับผลิตภัณฑ์ บริการ ส่วนลด โปรโมชั่น รางวัลและกิจกรรมใด ๆ เกี่ยวกับ Woops ที่คุณอาจจะสนใจ</br>
				•	การตรวจสอบกิจกรรมและการวิเคราะห์ข้อมูลเมื่อใช้บริการของเรา</br>
				•	ตรวจสอบหรือป้องกันการหลอกลวงและกิจกรรมที่ผิดกฎหมาย</br>
				•	วัตถุประสงค์ของการรวบรวมข้อมูลอื่นๆ</br>
		</view>
	</view>
</template>

<script>
	import {encrypt,decrypt,system,systemId,base64ToArrayBuffer,sendData,sendD,work,sendD07,sendD11,navigateTo} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				tabbarLoginLanguage: null, // 用户语言
				pageId: null, // 用户返回上一页面需要的值
				show: true, // 退出页面按钮是否显示
			};
		},
		onLoad(obj){
			var option=JSON.parse(decrypt(decodeURIComponent(obj.action)))
			this.tabbarLoginLanguage = option.tabbarLoginLanguage;
			this.pageId = option.pageId;
			this.getLoginlanger();
			this.chose();
		},
		methods:{
				getLoginlanger:function(){ // 获取当前语言
					var that = this;
					uni.getStorage({
						key: 'storage_login_language',
						success: function (res) {
							that.tabbarLoginLanguage = JSON.parse(res.data);
							console.log(that.tabbarLoginLanguage);
							if(that.tabbarLoginLanguage == 0){
								that.langer = 0;
							}else if(that.tabbarLoginLanguage == 1){
								that.langer = 1;
							}else if(that.tabbarLoginLanguage == 2){
								that.langer = 2;
							}else if(that.tabbarLoginLanguage == 3){
								that.langer =3;
							}
						}
					});
				},
				
				chose:function(){ // 判断是否为安卓或IOS登录
					if(this.pageId == 25 || this.pageId == 26){
						this.show = false;
					}
				},
				
				back:function(){ // 退出
					// uni.navigateTo({
					// 	url: '/pages/dataset/dataset'
					// });
					// navigateTo('/pages/dataset/dataset',null);
					// uni.navigateBack({
					//     delta: 1
					// });
					if(this.pageId == 15){
						navigateTo('/pages/startup/startup',null);
					}else if(this.pageId == 16){
						navigateTo('/pages/dataset/dataset',null);
					}
				}
		}
	}
</script>

<style lang="scss">
page{
	width: 100%;
	height: 100%;
	background: #000000;
}
.main{
	width:100%;
	height: 100%;
	display: flex;
	flex-direction: column;
	align-items:center;
	.top{
		position:fixed;
		left: 0rpx;
		left: 0rpx;
		z-index: 999;
		width: 100%;
		height: 100rpx;
		background:rgba(0,0,0,0.5);
		// background:red;
		
		.back{
			position: absolute;
			top: 20rpx;
			right: 5rpx;
			width: 36rpx;
			height: 36rpx;
			padding:10rpx;
			margin-right: 25rpx;
		}
	}
	.img{
		width:317rpx;
		height: 354rpx;
		margin-top:100rpx;
	}
	.title{
		width:311rpx;
		height: 91rpx;
		margin-top: 43rpx;
		margin-bottom: 75rpx;
	}
	.content{
		padding: 0rpx 51rpx;
		color: #ACACAC;
		font-size:26rpx;
		line-height: 50rpx;
		padding-bottom: 405rpx;
	}
}
</style>
