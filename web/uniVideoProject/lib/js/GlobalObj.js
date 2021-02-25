let tagArry =[];
// let urlPoint ='http://60.191.222.11:8022';
let urlPoint ='https://live.mycat1314.com';
let urlSocketPoint ='ws://192.168.1.100:17400';
export default
{
  tagArry,//tag数组 test
  urlPoint,//url前缀
  urlSocketPoint,//socket前缀
}
// // // console.log(this.$store.state.login.userInfo)
// console.log(this.$store.getters['login/get_userInfo'])
// this.$store.commit('login/set_userInfo',4444)
// this.$store.dispatch('login/SET_userInfo',333)
// console.log(this.$store.state.login.userInfo)
// getApp().globalData.url1 
// 1244813168059535361

  // import {InitaddList} from "../../lib/js/http.js"
// InitaddList(params,113,url).then(//地址列表aid 113
// 	  	(res) => {
// 			console.log(res.data)
// 	  		if(res.data.code==200){
// 	  			this.address=res.data.data.memberAddrVoList;
// 				console.log(this.address.length)
// 	  		}else{
// 	  			uni.showToast({
// 	  				icon:'none',
// 	  				title: res.data.message,
// 	  				duration: 1000,
// 	  			});
// 	  		}
// 	  	},
// 	  	(err) => {
// 	  	console.log('get err', err)
// 	  	}
// 	  )