import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import './assets/css/global.css'
import axios from 'axios'
import vueConfig from '../vue.config'
// axios.defaults
// axios.defaults.baseURL="/shiro"
const host=process.env.NODE_ENV==="development"?"":"http://192.168.43.41:81/api"
console.log(host)
const instance =axios.create({
  baseURL:host
})
instance.defaults.baseURL="/shiro"
// Vue.prototype.axios=instance;

Vue.use(ElementUI)
Vue.prototype.$http=instance
Vue.config.productionTip = false
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
