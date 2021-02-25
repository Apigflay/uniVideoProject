import Vue from 'vue'
import App from './App'
import Global_ from './lib/js/GlobalObj.js'   //全局对象
Vue.prototype.GLOBAL = Global_; //添加Global_到Vue的原型对象上
// 以下是注册组件的方法
import TagBar from './components/tabbar/tabbar.vue';
Vue.component("TagBar",TagBar); // 全局注册组件


Vue.config.productionTip = false

App.mpType = 'app'

const app = new Vue({
    ...App
})
app.$mount()
