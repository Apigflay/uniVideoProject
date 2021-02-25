// 状态
const state = {
  isLogin:null,//是否登录
  userInfo: {},//登录信息
  
}
const getters ={
	get_isLogin(state){
		return state.isLogin;
	},
	get_userInfo(state){
		return state.userInfo;
	}
}
// 提交
const mutations = {
	set_isLogin:(state,data)=>{
	  state.isLogin = data 
	},
	set_userInfo:(state,data)=>{
		state.userInfo = data 
	}
}
// 方法
const actions = {
	SET_isLogin: ({ commit }, data) => {
	  commit('set_isLogin', data)
	},
	SET_userInfo: ({ commit }, data) => {
		commit('set_userInfo', data)
	},
  
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
