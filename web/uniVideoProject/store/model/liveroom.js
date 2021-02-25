// 状态
const state = {
	liveInfo:null,//
	isLogin:null,//是否登录
	userInfo: {},//登录信息
  
}
const getters ={
	get_liveInfo(state){
		return state.liveInfo;
	},
	get_isLogin(state){
		return state.isLogin;
	},
	get_userInfo(state){
		return state.userInfo;
	}
}
// 提交
const mutations = {
	set_liveInfo:(state,data)=>{
	  state.liveInfo = data 
	},
	set_isLogin:(state,data)=>{
	  state.isLogin = data 
	},
	set_userInfo:(state,data)=>{
		state.userInfo = data 
	}
}
// 方法
const actions = {
	SET_liveInfo: ({ commit }, data) => {
	  commit('set_liveInfo', data)
	},
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
