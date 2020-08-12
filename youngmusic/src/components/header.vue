<template>
	<div id="top">
		<!-- 蒙层遮罩 -->
		<div id="mc" :class="[{notShow:mcisshow}]">
			<div :class="['login',{notShow:loginisshow}]">
				<div class="l-top">
					<span class="l-title">登录</span>
					<i class="fa fa-times l-close" aria-hidden="true" @click="closeclick()"></i>
				</div>
				<div class="l-centre">
					<input type="text" name="title" required lay-verify="required" placeholder="邮箱" autocomplete="off" class="layui-input"
					 id="login-email" v-model="inputloginemail">
					<br />
					<input type="password" name="title" required lay-verify="required" placeholder="密码" autocomplete="off" class="layui-input"
					 id="login-pwd" v-model="inputloginpwd" @keyup.enter="loginbtnClick()">
					<button type="button" class="layui-btn layui-btn-normal" id="login-btn" @click="loginbtnClick()">登录</button>
					<br />
					<button type="button" class="layui-btn layui-btn-primary goRegister" @click="goRegisterclick()">注册</button>
				</div>
			</div>
			<div :class="['register',{notShow:registerishow}]">
				<div class="r-top">
					<span class="r-title">注册</span>
					<i class="fa fa-times l-close" aria-hidden="true" @click="closeclick()"></i>
				</div>
				<div class="r-centre">
					<input type="text" required lay-verify="required" placeholder="邮箱" autocomplete="off" class="layui-input" id="email"
					 v-model="email">
					<br />
					<div class="r-row">
						<input type="text" required lay-verify="required" placeholder="验证码" autocomplete="off" class="layui-input yzm" id="code"
						 v-model="inputcode">
						<a href="javascript:;" :class="['layui-btn','layui-btn-normal',{'layui-btn-disabled':isjinyong} ]" id="sendEmail"
						 @click="sendEmailclick()">{{yzm}}</a></div>
					<br />
					<input type="password" name="title" required lay-verify="required" placeholder="密码" autocomplete="off" class="layui-input"
					 id="pwd" v-model="pwd">
					<button type="button" class="layui-btn layui-btn-normal" id="register" @click="registerclick()">注册</button>
					<br />
					<button type="button" class="layui-btn layui-btn-primary backLogin" @click="backLoginclick()">返回登录</button>
				</div>
			</div>
		</div>
		<div class="m_top">
			<span class="logo">YoungMusic</span>
			<ul class="menu1">
				<li :class="[{'topCheck':liisshow==0}]"><a href="javascript:;" @click="indexclick(0)">发现音乐</a></li>
				<li :class="[{'topCheck':liisshow==1}]"><a href="javascript:;" @click="mymusicclick(1)">我的音乐</a></li>
				<li :class="[{'topCheck':liisshow==2}]"><a href="javascript:;" @click="friendclick(2)">朋友</a></li>
			</ul>
			<div class="m_input">
				<img src="../assets/images/find.png">
				<input id="find" type="text" placeholder="音乐"  @keydown="findserach($event)" v-model="find">
			</div>
			<a href="#" class="m_content" @click="creationclick">创作者中心</a>			
			<a href="javascript:;" :class="['m_login',{notShow:m_loginisshow}]" @click="aloginclick()">登录</a>
			
			<ul class="layui-nav userPhoto" style="background-color: #242424; font-size: 10px;">
				<li class="layui-nav-item m_img" v-show="selectishow" lay-unselect="">
				<a href="javascript:;"><img v-if="loginimg" :src="imgurl+loginimg" 
				:class="['layui-nav-img',{notShow :userisshow},'user']"/></a>
					<dl class="layui-nav-child ">
						<dd><a href="javascript:;" @click="infoclick()">个人中心</a></dd>
						<dd><a href="javascript:;" @click="Logout()">退出登录</a></dd>
					</dl>
				</li>
			</ul>
		</div>
		<div class="m_back"></div>
		<ul class="menu2">
			<li><a href="javascript:;" :class="[{'topItemCheck':erliisshow==0}]" @click="indexclick(0)">推荐</a></li>
			<li><a href="javascript:;" :class="[{'topItemCheck':erliisshow==1}]" @click="ranklistclick">排行榜</a></li>
			<li><a href="javascript:;" :class="[{'topItemCheck':erliisshow==2}]" @click="songmenuclick">歌单</a></li>
			<li><a href="javascript:;" :class="[{'topItemCheck':erliisshow==3}]" @click="singerclick">歌手</a></li>
		</ul>
	</div>
</template>

<script>
	import $ from 'jquery'
	import axios from 'axios'
	import {
		top
	} from '../assets/js/TOP.js'
	import {
		logincss
	} from '../assets/js/logincss.js'
	import {
		request
	} from '../network/request.js'
	import {upimgjs	} from "../assets/js/upimg.js";
	export default {
		name: 'headers',
		data() {
			return {
				// 图片
				imgurl:'http://39.101.180.27:8089/api',
				// 验证码按钮的v-model
				yzm: '获取验证码',
				// 输入的验证码
				inputcode: '',
				// 注册时输入的密码
				pwd: '',
				// 注册时输入的邮箱
				email: '',
				// 一级导航的样式变量
				liisshow: 0,
				// 二级导航的样式变量
				erliisshow: 0,
				// 登录时输入的邮箱
				inputloginemail: '',
				// 登录时输入的密码
				inputloginpwd: '',
				// 关于控制样式的数据变量
				m_loginisshow: false,
				userisshow: true,
				mcisshow: true,
				loginimg: '',
				registerishow: true,
				loginisshow: true,
				isjinyong: false,
				selectishow:false,
				// 关于session的数据变量
				U_Id: " ",
				U_Name: '',
				U_Img: '',
				U_Email: '',
				// 接口数据
				serviceURL: 'http://39.101.180.27:8089/',
				data: '',
				code: '',
				EmailUrl: '',
				countdown: 60,
				SongList: [],
				find: '',
				dataresultsongs: {},
				Song: {
					'S_Id': '',
					'S_Name': '',
					'S_Type': '',
					'S_Url': '',
					'S_Singer': '',
					'S_Cover': '',
					'S_PlayCount': 0,
					'S_CollectCount': 0,
					'S_UpTime': '',
					'S_Lyric': '',
					'S_Album': '',
				},
				// song1: [],
				// funget: '',
				singerinfo: [],
			}
		},
		
		mounted() {
			if(sessionStorage.getItem('store')){
				this.$store.replaceState(Object.assign({},this.$store.state,JSON.parse(sessionStorage.getItem('store'))))
			}
			window.addEventListener('beforeunload',()=>{
				sessionStorage.setItem("store",JSON.stringify(this.$store.state))
			})
			
			this.showload()
			// 判断用户是否登录，
			if(sessionStorage.U_Id!=null){
				this.selectishow=true
				}
			// 回到顶部
			top()
			// 拖拉
			logincss()
			
			layui.use('element', function(){
			  var element = layui.element; //导航的hover效果、二级菜单等功能，需要依赖element模块
			  
			  //监听导航点击
			  element.on('nav(demo)', function(elem){
			    //console.log(elem)
			    layer.msg(elem.text());
			  });
			});
		},
		methods: {
			// 跳转到首页
			indexclick(index) {
				this.liisshow = index
				this.erliisshow = index
				this.$router.push('/index')
			},
			// 跳转到动态
			friendclick(index) {
				if(sessionStorage.U_Id==null){
					layer.msg('请登录');
					return
				}
				this.liisshow = index
				this.$router.push('/friend')
			},
			// 跳转到创作者中心
			creationclick() {
				this.$router.push('/creation')
			},
			// 个人详情页
			infoclick() {
				this.$router.push({
					path:'./info',
					query:{
						friendid:sessionStorage.U_Id
					}
					})
			},
			// 退出
			Logout(){				
				layer.msg('退出成功~');
				this.$router.push('/index')
					window.location.reload()
				sessionStorage.clear()
				this.selectishow=false
			},
			// 我的音乐跳转
			mymusicclick(index) {
				if(sessionStorage.U_Id==null){
					layer.msg('请登录');
					return
				}
				this.liisshow = index
				this.$router.push('./mymusic')
			},
			//排行榜
			ranklistclick() {
				this.erliisshow = 1
				// this.$router.push('./album')
			},
			// 歌单
			songmenuclick() {
				this.erliisshow = 2
				this.$router.push({
					path: './songmenulist',
					query: {
						typeid: 0
					}
				})
			},
			// 歌手
			singerclick() {
				this.erliisshow = 3
				this.$router.push('./Singer')
			},
			//进入个人主页
			loginclick() {
				this.$router.push('./info')				
						},
			// 加载时执行的方法
			showload() {
				// http://39.101.180.27:8089/api/Search/SelTypeList
				// sessionStorage.setItem("img", 'http://39.101.180.27:8089/api')
				
				this.U_Id = sessionStorage.U_Id
				this.U_Img = sessionStorage.U_Img
				this.U_Name = sessionStorage.U_Name
				this.U_Email = sessionStorage.U_Email
				if (this.U_Id != undefined && this.U_Name != undefined && this.U_Img != undefined && this.U_Email != undefined) {
					this.showUser()
				}
			},
			// 登录弹窗1111
			aloginclick() {
				this.mcisshow = false
				this.registerishow = true
				this.loginisshow = false

			},
			// 点击登录按钮
			goRegisterclick() {
				this.loginisshow = true
				this.registerishow = false
			},
			// 返回登录按钮1111
			backLoginclick() {
				this.loginisshow = false
				this.registerishow = true
			},
			// 弹窗关闭1111
			closeclick() {
				this.mcisshow = true
			},
			// 显示头像 取消显示登录
			showUser() {
				this.m_loginisshow = true
				this.userisshow = false
				this.loginimg = sessionStorage.U_Img
				this.mcisshow = true;
			},
			// 注册账号的按钮
			registerclick() {
				//这个code是api传出的验证码
				if (this.code == "") {
					layer.msg('邮箱验证码出错，请重新获取');
				} else if (this.code != this.inputcode) {
					layer.msg('邮箱验证码错误！');
				} else if (this.email != "" && this.code == this.inputcode && this.pwd != "") {
					//如果邮箱不为空  邮箱在获取验证码时获取到   两个code相同   密码不为空
					$.post(this.serviceURL + "api/Home/Register",
						this.data = {
							"email": this.email,
							"pwd": this.pwd
						},
						function(d) {
							layer.msg(d);
						})
				}
			},
			// 获取验证码按钮
			sendEmailclick() {
				var that = this
				if (this.yzm == "获取验证码") {
					//邮箱正则 验证邮箱格式
					var reg = /^([a-zA-Z]|[0-9])(\w|\-)+@[a-zA-Z0-9]+\.([a-zA-Z]{2,4})$/;
					// 输入的邮箱值
					if (reg.test(this.email)) {
						//按钮改属性
						this.settime()
						this.EmailUrl = this.email
						// 发送验证码请求
						$.post(this.serviceURL + "api/Home/RegisterVerify", this.data = {
								"": this.EmailUrl
							},
							function(data) {
								that.code = data;
								// alert(data)
							})
					} else {
						layer.msg('邮箱格式不正确');
					}
				}
			},
			//单击获取验证码 更改按钮样式 以及倒计时1111
			settime() {
				this.isjinyong = true
				if (this.countdown == 0) {
					this.isjinyong = false
					this.yzm = "获取验证码"
					this.countdown = 60; //还原倒计时时间
					this.isjinyong = false
					return;
				} else {
					this.isjinyong = true
					this.yzm = "重新发送(" + this.countdown + ")"
					this.countdown--;
				}
				setTimeout(this.settime, 1000);
			},
			// 登录按钮
			loginbtnClick() {
				var that = this
				if (this.inputloginemail == "" || this.inputloginpwd == "") {
					layer.msg('账号或密码请填写完整哦~');
				} else {
					$.post(this.serviceURL + "api/Home/Login",
						this.data = {
							"email": this.inputloginemail,
							"pwd": this.inputloginpwd
						},
						function(d) {
							if(d==null){
								layer.msg('账号或密码错误~');
								return
							}							
							// 添加用户信息到 sessionStorage
							sessionStorage.setItem("U_Id", d.U_Id)
							sessionStorage.setItem("U_Name", d.U_Name)
							sessionStorage.setItem("U_Img", d.U_Img)
							sessionStorage.setItem("U_Email", d.U_Email)
							location.reload() 
								
							that.showload()
							that.showUser();
							that.selectishow=true
						})
				}
			},
			// 搜索框
			findserach(ev) {
				if (ev.keyCode == 13) {
					// 触发事件
					// this.SongInfo();
					this.$router.push({
						path:'./song',
						query:{
							songname:this.find
						},
						// window.location.reload()
						})
				}
			},
			// 查询歌曲后保存到数据库
			SongInfo() {
				new Promise((resove, reject) => {
					// 1.查找
					axios.get('https://autumnfish.cn/search', {
						params: {
							keywords: this.find
						}
					}).then(res => {
						//将找到的数据导入dataresultsongs
						this.dataresultsongs = res.data.result.songs
						// 开始遍历清洗成数据库需要的对象
						this.dataresultsongs.forEach(el => {
							this.Song = {}
							this.Song.S_Id = el.id;
							this.Song.S_Name = el.name;
							this.Song.S_Type = "流行"
							this.Song.S_Url = "https://music.163.com/song/media/outer/url?id=" + el.id + ".mp3&br=320000";
							this.Song.S_Singer = "";
							this.Song.S_Cover = "";
							this.Song.S_PlayCount = 0; //播放次数
							this.Song.S_CollectCount = 0; //收藏次数
							this.Song.S_UpTime = ""; //上传时间
							this.Song.S_Lyric = ""; //歌词
							this.Song.S_Album = ""; //专辑表关联 专辑id
							// 3.清洗后的数据保存到SongList
							this.SongList.push(this.Song)
							// console.log(this.SongList)
						})
						// console.log(this.SongList[1]["S_Id"])
						return Promise.resolve(this.SongList)
					}).then(res => {
						this.get3(this.SongList)
					})
				}).catch(err => {
					reject(err)
				})

			},
		
			ge2(res, i) {
				return new Promise((resolve, reject) => {
					// console.log(this.SongList[i].S_Id)
					// 根据歌曲id查询歌手信息等
					axios.get('https://autumnfish.cn/song/detail', {
						params: {
							ids: this.SongList[i].S_Id
						}
					}).then(res => {
						resolve(res)
					})

				})
			},
			async get3(res) {
				//循环遍历id  去找到对应的歌曲url 和 歌手 歌手图片		
				for (let i = 0; i < this.SongList.length; i++) {
					var singerinfo = await this.ge2(this.SongList, i)
					// console.log(singerinfo)						 
					this.SongList[i].S_Singer = singerinfo.data.songs[0].ar[0].name;
					this.SongList[i].S_Cover = singerinfo.data.songs[0].al.picUrl;
				}
				// console.log(this.SongList)
				// 最后将清洗完的数据json传送到数据库
				axios.post(this.serviceURL + "api/Home/AddSongList", this.SongList).then(res => {
					console.log(res.data);
				}).catch(function(error) {
					console.log(error);
				});
			},
		},
	computed:{
		userimg(){
			if(this.loginimg!=''){
				return this.imgurl+this.loginimg
			}
		}
	}
	}
</script>

<style scoped="">
	@import url("../assets/css/login.css");
</style>
