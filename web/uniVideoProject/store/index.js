import Vue from 'vue';
import Vuex from 'vuex';
import state from './state.js'
import getters from './getter.js'
import actions from './actions.js'
import mutations from './mutations.js'
import login from './model/login.js'
import liveroom from './model/liveroom.js'
Vue.use(Vuex);
export default new Vuex.Store({
	modules:{
		login,
		liveroom
	},
	state,
	getters,
	mutations,//同步
	actions//异步
})