<template>
	<div id="boxapp">
		<div id="box-cendiv">
			<div id="mc" v-show="isshow">
				<div id="upd">
					<fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
						<legend>个人信息修改</legend>
					</fieldset>
					<form class="layui-form" action>
						<div class="layui-form-item">
							<label class="layui-form-label">用户名:</label>
							<div class="layui-input-inline">
								<input type="text" name="username" lay-verify="required" lay-reqtext="用户名是必填项，岂能为空？" placeholder="请输入"
								 autocomplete="off" class="layui-input" v-model="userupd.name" />
							</div>
						</div>
						<div class="layui-form-item">
							<div class="layui-inline">
								<label class="layui-form-label">手机号:</label>
								<div class="layui-input-inline">
									<input type="tel" name="phone" lay-verify="required|phone" placeholder="请输入手机号" autocomplete="off" class="layui-input"
									 v-model="userupd.tell" />
								</div>
							</div>
						</div>

						<div class="layui-form-item">
							<div class="layui-inline">
								<label class="layui-form-label">生日:</label>
								<div class="layui-input-inline">
									<input type="text" name="date" id="date" lay-verify="date" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input"
									 v-model="userupd.birthday.split('T')[0]" />
								</div>
							</div>
						</div>
						<div class="layui-form-item" pane>
							<label class="layui-form-label">歌曲偏爱:</label>
							<div class="layui-input-block">
								<input type="checkbox" name="loadtype" lay-skin="primary" value="1" title="流行" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="2" title="华语" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="3" title="欧美" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="4" title="日语" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="5" title="粤语" />
								<br />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="6" title="民族" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="7" title="摇滚" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="8" title="民谣" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="9" title="说唱" />
								<input type="checkbox" name="loadtype" lay-skin="primary" value="10" title="轻音乐" />
							</div>
						</div>
						<div class="layui-form-item">
							<label class="layui-form-label">性别:</label>
							<div class="layui-input-block">
								<input type="radio" name="sex" value="男" title="男" />
								<input type="radio" name="sex" value="女" title="女" />
							</div>
						</div>
						<div class="layui-form-item layui-form-text">
							<label class="layui-form-label">个性签名:</label>
							<div class="layui-input-block">
								<textarea placeholder="请输入内容" class="layui-textarea" style="width:94%;max-height: 240px;" v-model="userupd.info"></textarea>
							</div>
						</div>

						<div class="layui-form-item">
							<div class="layui-input-block">
								<button type="button" class="layui-btn layui-btn-primary layui-bg-blue" @click="sumbitclick()">立即提交</button>
								<button type="reset" class="layui-btn layui-btn-primary" @click="backclick">返回</button>
							</div>
						</div>
					</form>
				</div>
			</div>
			<!-- 个人信息 -->
			<div class="layui-container box-div">
				<div class="infodiv">
					<!-- 头像 判断当前页面是不是自己的主页-->
					<div class="imgdiv"  v-show="friendid!=uid"   >
						<img v-if="userinfo.U_Img" :src="imgurl+userinfo.U_Img"/>
					</div>
					<div class="imgdiv" id="upimg" v-show="friendid==uid" >
						<!-- id="upimg" -->
						<img v-if="userinfo.U_Img" :src="imgurl+userinfo.U_Img"   id="imgs" />
						<!-- class="img"  -->
						<span class="updimg" >点击更换头像</span>
					</div>
					<!-- 个人信息 -->
					<div class="app-info">
						<div class="infodiv-top layui-icon">
							<p class="pname">
								<b>{{userinfo.U_Name}}</b>
								<span v-show="userinfo.U_Gender!='男'" style="color:rgb(255, 35, 35)">&#xe662;</span>
								<span v-show="userinfo.U_Gender!='女'" style="color:#1e9fff">&#xe661;</span>
								<!-- <br /> -->
							</p>
							<div style="display:flex;margin: 10px 0px;">
								<button @click="gz()" type="button" v-show="friendid!=uid" class="layui-btn layui-bg-blue btn">{{gzorfans}}</button>
								<button  @click="backuser()" type="button" v-show="friendid!=uid&&uid!=null"  class="layui-btn layui-btn-primary btn"><a href="javascript:;">回到主页</a></button>
								<button type="button" class="layui-btn layui-btn-normal btn btnupd" v-show="friendid==uid" @click="showclick">编辑个人资料</button>
							</div>
							<hr class="hr-red" />
							<span class="span-bolck" lay-separator="|">
								<a href="javascript:;">
									<span>{{dtncount}}</span>动态
								</a>
								<a href="javascript:;" @click="followclick()">
									<span>{{follow}}</span>关注
								</a>
								<a href="javascript:;" @click="fansclick()">
									<span>{{ fanslength}}</span>粉丝
								</a>
							</span>
							<p class="p-address">
								出生日期:
								<span>{{Birthday}}</span>
							</p>
							<p class="p-info">
								个性签名:
								<span>{{userinfo.U_Info}}</span>
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- 关注等 -->
		<div id="div-fans" v-show="userfriend">
			<div class="ph-div-h">
				<p class="ph-p">
					{{followfans}}
					<span class="ph-span">（{{this.userfriend.length}}）</span>
				</p>
			</div>
			<hr class="hr-red" />
			<div class="fan-uldiv">
				<ul class="fanul">
					<li class="fansli" v-for="(items , index) in userfriend" :key="index" v-if="index<followfanscount">
						<div class="lidiv">
							<div class="fanimgdiv">
								<img class="fansuserimg" :src="imgurl+ items.U_Img" />
							</div>
							<div class="faninfodiv layui-icon">
								<p class="fannamep">
									<a href="javasrcipt:;" @click="openfriend(items.U_Id)">{{items.U_Name}}</a>
									<span v-show="userinfo.U_Gender!='男'">&#xe662;</span>
									<span v-show="userinfo.U_Gender!='女'">&#xe661;</span>
								</p>
								<p class="faninfop">动态0|关注3|粉丝5</p>
							</div>
						</div>
					</li>
				</ul>
			</div>
			<a href="javescript:;" @click="followfanscount=100" v-if="followfanscount!=100">查看更多>></a>
		</div>
		<!-- 歌曲排行 -->
		<div id="paihangdiv">
			<div class="ph-div-h">
				<p class="ph-p">
					听歌排行
					<span class="ph-span">累计听歌</span>
				</p>
			</div>

			<hr class="hr-red" />
			<!-- <hr class="layui-bg-red"> -->
			<div class="ph-uldiv">
				<table class="layui-table" lay-skin="line">
					<colgroup>
						<col width="150" />
						<col width="300" />
						<col width="300" />
						<col />
					</colgroup>
					<thead>
						<tr>
							<th></th>
							<th>歌名</th>
							<th>歌手名</th>
							<th>播放次数</th>
						</tr>
					</thead>
					<tbody>
						<tr v-for="(items,index) in listensongplay" :key="index" v-if="index<listeningcount&&listensongplay!=null">
							<td>{{index+1}}</td>
							<td>
								<i class="layui-icon layui-icon-play" title="点击播放" @click="play(index)"></i>
								<a href="javascript:;" @click="songclick(items.songid)">{{items.songname}}</a>
							</td>
							<td>
								<a href="javascript:;" @click="singerclick(items.singerid)">{{items.singername}}</a>
							</td>
							<td>{{items.count}}</td>
						</tr>
					</tbody>
				</table>
				<span class="more-span">
					<a href="javascript:;" @click="listeningcount=100" v-if="listeningcount!=100">查看更多>></a>
				</span>
			</div>
		</div>

		<!-- 歌单列表 -->
		<div id="menudiv">
			<div class="ph-div-h">
				<p class="ph-p">
					{{friendid==uid?'我': userinfo.U_Name}}创建的歌单
					<span class="ph-span">歌单数量({{itemcreatemenu.length}})</span>
				</p>
			</div>
			<hr class="hr-red" />
			<ul class="menu-div-ul">
				<li v-for="(items,index) in itemcreatemenu" :key="index" @click="menutarget(items.M_Id)">
					<a href="javascript:;">
						<div>
							<img :src="imgurl+items.M_Img" />
						</div>
						<span>{{items.M_Name}}</span>
					</a>
				</li>
			</ul>
			<!-- 收藏歌单 -->
			<div class="ph-div-h">
				<p class="ph-p">
					{{friendid==uid?'我': userinfo.U_Name}}收藏的歌单
					<span class="ph-span">歌单数量({{itemCollectionmenu.length}})</span>
				</p>
			</div>
			<hr class="hr-red" />
			<ul class="menu-div-ul">
				<li v-for="(items,index) in itemCollectionmenu" :key="index" @click="menutarget(items.M_Id)">
					<a href="#">
						<div>
							<img :src="imgurl+items.M_Img" />
						</div>
						<span>{{items.M_Name}}</span>
					</a>
				</li>
			</ul>
		</div>
	</div>
</template>

<script>
	import $ from "jquery";
	import {
		layuijs
	} from "../../assets/js/layuifrom.js";
	import {
		request
	} from "../../network/request.js";
	import {upimgjs	} from "../../assets/js/upimg.js";
	import axios from "axios";
	export default {
		name: "",
		data() {
			return {
				imgurl: sessionStorage.img,
				isshow: false,
				// 控制粉丝关注的显示人数
				followfanscount: 2,
				// 控制最近播放歌曲量
				listeningcount: 8,
				// 用户id
				uid: sessionStorage.U_Id,
				// 改页面传递的参数，也就是观看用户
				friendid: this.$route.query.friendid,
				// 存放用户数据
				userinfo: "",
				dtncount:0,
				// 存放用户修改的数据
				userupd: {
					uid: "",
					name: "",
					// img: '',
					tell: "",
					info: "",
					birthday: "",
					hobby: "",
					gender: ""
				},
				collectSongMenu: "",
				creatSongMenu: "",
				// 获取最近播放的歌曲id和次数
				listeningsong: "",
				// 获取最近播放的歌曲详细信息
				songinfoitems: "",
				// 存放清洗的数据
				listensongplay: [],
				followfans: "",
				// 用户创建的歌单list
				itemcreatemenu: "",
				// 用户收藏的list
				itemCollectionmenu: "",
				// 用户粉丝和关注数据
				userfriend: "",
				// 加载时判断是否关注改用户
				gzorfans: "关注",
				// 存放用于播放的数据
				getsonglist: [],
				getsong: {
					name: "",
					singer: "",
					cover: "",
					url: "",
					id: ""
				},
			};
		},
		methods: {
			
			// 打开编辑个个人资料页面
			showclick() {
				var that = this;
				this.userupd.uid = sessionStorage.U_Id;
				this.userupd.name = this.userinfo.U_Name;
				// this.userupd.img = this.userinfo.U_Img
				this.userupd.tell = this.userinfo.U_Tell;
				this.userupd.info = this.userinfo.U_Info;
				this.userupd.birthday = this.userinfo.U_Birthday;
				this.userupd.hobby = this.userinfo.U_Hobby;
				this.userupd.gender = this.userinfo.U_Gender;
				this.isshow = !this.isshow;
				layuijs();
				layui.use(["form"], function() {
					var form = layui.form,
						$ = layui.$;
					var unitType = [];
					if (that.userupd.hobby != "") {
						// console.log("1");
						unitType = that.userupd.hobby.split(",");
					}
					for (var j = 0; j < unitType.length; j++) {
						var unitTypeCheckbox = $("input[name='loadtype']");
						// console.log('爱好数据',unitType[j])
						for (var i = 0; i < unitTypeCheckbox.length; i++) {
							if (unitTypeCheckbox[i].value == unitType[j]) {
								// console.log('复选框的值',unitTypeCheckbox[i].title)
								unitTypeCheckbox[i].checked = true;
							}
						}
					}
					var sex = that.userupd.gender;
					var sexCheckbox = $("input[name='sex']");
					for (var i = 0; i < sexCheckbox.length; i++) {
						if (sexCheckbox[i].title == sex) {
							sexCheckbox[i].checked = true;
						}
					}
					form.render();
				});
			},
			// 修改的提交按钮
			sumbitclick() {
				var bir=$("#date").val()
				this.userupd.birthday=bir
				var unitTypeCheckbox = $("input[name='loadtype']");
				var hobby = "";
				// console.log('爱好数据',unitType[j])
				for (var i = 0; i < unitTypeCheckbox.length; i++) {
					if (unitTypeCheckbox[i].checked == true) {
						hobby += unitTypeCheckbox[i].value + ",";
					}
				}
				this.userupd.hobby = hobby;
				var sexCheckbox = $("input[name='sex']");
				for (var i = 0; i < sexCheckbox.length; i++) {
					if (sexCheckbox[i].checked == true) {
						this.userupd.gender = sexCheckbox[i].value;
					}
				}
				console.log(this.userupd)
				// http://39.101.180.27:8089/api/Search/SelTypeList
				axios
					.post(
						"http://39.101.180.27:8089/" + "api/MyInfo/UpUserInfo",
						this.userupd
					)
					.then(res => {
						// console.log(res.data);
						layer.msg("修改成功~");
						window.location.reload();
					})
					.catch(function(error) {
						console.log(error);
					});
			},
			backclick() {
				this.isshow = false;
			},
			// 加载时获取用户的动态量(次数)
			loadgetdtcount() {
				if (this.uid != null) {
					request({
						url: '/api/Dynamic/GetDynamicCount',
						params: {
							id: this.uid
						}
					}).then(res => {
						this.dtncount = res
					}).catch(err => {
						console.log(err);
					})
				}
			},
			// 返回主页
			backuser(){
				window.location.reload(); //刷新
			},
			// --------------------------------
			// 加载时判断该用户是否关注
			loadisgz() {
				if (this.userinfo.U_Fans != "") {
					var gzarr = this.userinfo.U_Fans.split(",");
					gzarr.forEach(el => {
						if (el == sessionStorage.U_Id) {
							this.gzorfans = "已关注";
						}
					});
				}
			},
			// 加载时获取用户详细数据
			loadgetuserinfo(id) {
				request({
						url: "/api/MyInfo/SelUserInfoByID",
						params: {
							id: id
						}
					})
					.then(res => {
						// console.log(res)
						this.userinfo = res.userInfo;
						this.loadisgz();
						this.collectSongMenu = res.collectSongMenu;
						this.creatSongMenu = res.creatSongMenu;
						// layer.msg('歌单收藏成功~');
					})
					.catch(err => {
						console.log("错了");
					});
			},
			// 加载时获取听歌数据
			loadgetlisteningsong(id) {
				request({
						url: "/api/MyInfo/SelRecentSong",
						params: {
							id: id,
							length: 20
						}
					})
					.then(res => {
						// console.log(res)
						if (res != null) {
							if (res.songCoutn != null && res.songList != null) {
								this.listeningsong = res.songCoutn;
								this.songinfoitems = res.songList;
								// 用户最近播放排行的信息显示
								this.clearsong();
							}
						}
					})
					.catch(err => {
						console.log(err);
					});
			},
			// 加载时获取用户的歌单数据
			loadmenu(id) {
				request({
						url: "/api/MyInfo/SelUserInfoByID",
						params: {
							id: id
						}
					})
					.then(res => {
						// console.log(res)
						if (res.creatSongMenu != null) {
							this.itemcreatemenu = res.creatSongMenu;
						}
						if (res.collectSongMenu != null) {
							this.itemCollectionmenu = res.collectSongMenu;
						}
						// console.log("歌单数据请求成功")
					})
					.catch(err => {
						console.log("错了");
					});
			},
			// 用于显示播放列表的数据
			clearsong() {
				var song = {
					songid: "",
					songname: "",
					singername: "",
					singerid: "",
					count: ""
				};

				this.listensongplay = [];
				this.listeningsong.forEach(listenitems => {
					// console.log(listenitems.id);
					this.songinfoitems.forEach(el => {
						// console.log(el.sID);
						// 查找相同的数据
						if (listenitems.id == el.sID) {
							// console.log(listenitems.id);
							// 将数据放入一个新的对象中
							song = {};
							song.songid = el.sID;
							song.songname = el.sName;
							song.singername = el.gSingName;
							song.singerid = el.sSingID;
							song.count = listenitems.count;
							this.getsong = {},
								this.getsong.name = el.sName;
							this.getsong.singer = el.gSingName;
							this.getsong.cover = el.sCover;
							this.getsong.url = el.sUrl;
							this.getsong.id = el.sID;
							this.listensongplay.push(song);
							this.getsonglist.push(this.getsong)
						}
					});
				});
			},
			// 歌曲播放
			play(index) {
				this.$store.commit("currplay", this.getsonglist[index]);
			},
			// 播放列表歌曲的跳转
			songclick(songids) {
				this.$router.push({
					path: "./songinfo",
					query: {
						songid: songids
					}
				});
			},
			// 播放列表歌手的跳转
			singerclick() {},
			// 歌单的跳转
			menutarget(mid) {
				this.$router.push({
					path: "./SongMenuInfo",
					query: {
						menuid: mid
					}
				});
			},
			// 用户关注用户
			gz() {
				if (sessionStorage.U_Id == null)  {
				  layer.msg("请登录~");
				  return
				}
				if (this.gzorfans == "关注") {
					request({
							url: "/api/MyInfo/AddUserFollow",
							params: {
								uid: this.friendid,
								id: this.uid
							}
						})
						.then(res => {
							console.log(res);
							layer.msg("关注用户成功~");
							this.gzorfans = "已关注";
						})
						.catch(err => {
							console.log("错了");
							layer.msg("关注失败~");
						});
				} else if (this.gzorfans == "已关注") {
					request({
							url: "/api/MyInfo/RmUserFollow",
							params: {
								uid: this.friendid,
								id: this.uid
							}
						})
						.then(res => {
							console.log(res);
							layer.msg("已取消关注~");
							this.gzorfans = "关注";
						})
						.catch(err => {
							console.log("错了");
							layer.msg("取消关注失败~");
						});
				}
			},
			// =调用查看关注的信息
			getfollowinfo() {
				request({
						url: "/api/MyInfo/SelUserFollow",
						params: {
							id: this.friendid
						}
					})
					.then(res => {
						// console.log(res)
						this.userfriend = res;
					})
					.catch(err => {
						console.log("错了");
					});
			},
			// 调用查看粉丝的信息
			getfansinfo() {
				request({
						url: "/api/MyInfo/SelUserFans",
						params: {
							id: this.friendid
						}
					})
					.then(res => {
						// console.log(res);
						this.userfriend = res;
					})
					.catch(err => {
						console.log("错了");
					});
			},
			// 查看关注信息
			followclick() {
				this.followfans = "关注用户";
				this.getfollowinfo();
			},
			// 查看粉丝信息
			fansclick() {
				this.followfans = "粉丝用户";
				this.getfansinfo();
			},
			//查看朋友信息
			openfriend(id) {
				this.friendid = id;
				this.loadgetuserinfo(id);
				// 获取用户的最近播放的歌名和次数
				this.loadgetlisteningsong(id);
				// 获取用户的歌单信息
				this.loadmenu(id);
				this.getfollowinfo();
				this.getfansinfo();
			}
		},
		created() {
		
			//获取用户的基本信息
			this.loadgetuserinfo(this.friendid);
			// 获取用户的最近播放的歌名和次数
			this.loadgetlisteningsong(this.friendid);
			// 获取用户的歌单信息
			this.loadmenu(this.friendid);

			// =========================
		},
		mounted() {
			// 更换头像
			upimgjs()
			this.loadgetdtcount()
		},
		computed: {
			follow() {
				if (this.userinfo.U_Follow == "") {
					return 0;
				}
				return this.userinfo.U_Follow.split(",").length - 1;
			},
			fanslength() {
				if (this.userinfo.U_Fans == "") {
					return 0;
				}
				return this.userinfo.U_Fans.split(",").length - 1;
			},
			Birthday() {
				if (this.userinfo.U_Birthday != "") {
					return this.userinfo.U_Birthday.split("T")[0];
				}
				return "无";
			}
		}
	};
</script>

<style scoped>
	@import url("./myinfo.css");
</style>

 