import Vue from 'vue'
import App from './App'
import router from './router'
import axios from  'axios'
import store from './store'
import layer from "layui-layer";
import 'font-awesome/css/font-awesome.min.css';


Vue.config.productionTip = false


/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
	store,
  render: h => h(App)
})
