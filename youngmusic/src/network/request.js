import axios from 'axios'

export function request(config){
	//1. 创建axios实例
	const instance=axios.create({
		// baseURL:'https://localhost:44319/',
		baseURL:'http://39.101.180.27:8089/',
		timeout:5000
	})
	// axios的拦截器
	instance.interceptors.request.use(config=>{
		return config
	},err=>{
		// console.log(err);
	})
	//响应拦截
	instance.interceptors.response.use(res=>{
		return res.data
	},err=>{
		console.log(err);
	})
	// 发送真正的网络请求
	return instance(config)
	
}

