<template>
	<view class="liveroom">
		<toast></toast>
		liveroom
		{{this.$store.getters['login/get_userInfo']}}
		<!-- {{this.$store.getters['isLogin']}} -->
		<button @click="showT">点击</button>
		<button @click="noneT">none</button>
		<button @click="setSex">设置喜好</button>
	</view>
</template>

<script>
	import {sendDSocket} from "../../lib/js/GlobalFunction.js"
	export default {
		data() {
			return {
				
			};
		},
		created() {
			this.getInitMsg()
		},
		methods:{
			getInitMsg:function(){
				console.log(this.$store.getters['Allid'])
				
			},
			showT:function(){
				this.$loading();
				// this.$loading(false);
				console.log(this.$store)
				console.log(this.$store.getters['Allid'])
				
				this.$store.commit("set_id",4444)
				console.log(this.$store.getters['Allid'])
				console.log(this.$store.state)
			},
			noneT:function(){
				// this.$loading();
				this.$loading(false);
			},
			setSex:function(){
				var str =JSON.stringify({
					"useridx": 10000001,
					"toidx": 10000002,
					"status":true
				})
				uni.sendSocketMessage({
				  data: sendDSocket(str,11007)
				});
		
			}
		}
	}
</script>

<style lang="scss">
.liveroom{
	width: 100%;
	height: 100%;
	background: #fff;
	color: #000;
}
</style>
