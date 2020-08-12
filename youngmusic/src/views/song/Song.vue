<template>
	<div id="boxapp">
		<!-- 新建歌单 -->
		<div id="xj" v-if="ishowxj">
			<div class="logindiv">
				<div>
					<div class="l-top">
						<span class="l-title">新建歌单</span>
						<i class="fa fa-times close " aria-hidden="true" @click="closeclick()"></i>
					</div>
					<div class="mid">
						<input type="text" name="title" lay-verify="required" placeholder="请输入新建的歌单名" autocomplete="off" class="layui-input textmenu"
						 v-model="addmenuname">
						<div class="divbtn">
							<button type="button" class="layui-btn layui-btn-normal" @click="addclick()">添加</button>
							<button type="button" class="layui-btn" @click="closeclick()">取消</button>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div id="addmenudiv" v-if="collectionisshow">
			<div class="addmenu">
				<div class="l-top">
					<span class="l-title">添加到歌单</span>
					<i class="fa fa-times close " aria-hidden="true" @click="closeclick()"></i>
				</div>
				<div class="mids">
					<ul>
						<li class="add">
							<div class="newadd">
								<i class="layui-icon layui-icon-add-1" aria-hidden="true" @click="addmenu()">&nbsp;创建新的歌单</i>
							</div>
						</li>
						<li class="hover" v-for="(item, index ) in itemcreatemenu" :key="index" @click="CollectionmenuClick(item.M_Id)">
							<img :src="imgurl+item.M_Img">
							<div class="lidivadd">
								<p class="pname">{{item.M_Name}}</p>
								<p class="pcount">{{item.M_SongId.split(',').length-1}}首 </p>
							</div>
						</li>
					</ul>
				</div>
			</div>
		</div>

		<div id="box-cendiv">
			<!-- 搜索框div -->
			<div class="searchdiv">
				<div class="search_form">
					<input type="text" class="input_text" placeholder="请输入搜索内容" v-model="content" @keyup.enter="search()">
					<input type="button" value="搜索" class="input_sub" @click="search()">
				</div>
			</div>
			<div class="songlistdiv">
				<table class="layui-table " lay-size="sm">
					<colgroup>
						<col width="30">
						<col>
						<col width="200">
						<col width="90">
						<col width="90">
						<col width="200">
					</colgroup>
					<thead>
						<tr>
							<th></th>
							<th>歌曲名称</th>
							<th> 歌手</th>
							<th>歌曲类型</th>
							<th>播放次数</th>
							<th>操作</th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<td colspan="6" style="text-align:center" v-show="songlist==''">请在输入框填入要搜索的歌曲或歌手</td>
						</tr>
						<tr v-for="(items,index) in songlist" :key="index">
							<td>{{index+1}}</td>
							<td @click="targetsong(items.sID)"><a href="javascript:;">{{items.sName}}</a></td>
							<td><a href="javascript:;">{{items.singerName}}</a></td>
							<td>{{items.typeName}}</td>
							<td>{{items.sPlayCount}}</td>
							<td>
								<div>
									<a title="播放" @click="play(items)"><i class="fa fa-play-circle" aria-hidden="true"></i></a>&nbsp;&nbsp;
									<!-- <a title="添加到播放列表" @click="joinmenu()"><i class="fa fa-plus-square" aria-hidden="true"></i></a>&nbsp;&nbsp; -->
									<a title="收藏" @click="collectmenu(items.sID)"><i class="fa fa-folder-open-o" aria-hidden="true"></i></a>&nbsp;&nbsp;
									<a title="下载" :href="items.sUrl" download><i class="fa fa-download " aria-hidden="true"></i></a>&nbsp;&nbsp;
								</div>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
	</div>
</template>
<script>
	import {
		request
	} from '../../network/request.js'
	import axios from 'axios'
	export default {
		name: 'Song',
		data() {
			return {
				imgurl: sessionStorage.img,
				ishowxj: false,
				// 传进来的歌曲名
				// songname:this.$route.query.songname,
				// 收藏歌单的歌单界面
				collectionisshow: false,
				// 创建的歌单名
				addmenuname: '',
				// 用户的歌单数据
				itemcreatemenu: '',
				// 搜索框的内容
				content: this.$route.query.songname,
				// 存储搜索的结果
				songlist: [],
				// 存储歌曲id
				collectsongid: '',
				// 存新建歌单的歌单id
				addmunuidcount: 0,
				// 导入歌曲到输入库饿
				SongList: [],
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
			}
		},
		methods: {
			// 搜索按钮单击
			search() {
				if (this.content != '') {
					this.searchsongbyname(this.content)
				} else {
					layer.msg("请输入搜索内容")
				}
			},
			// 查询歌曲后保存到数据库
			SongInfo() {
				new Promise((resove, reject) => {
					// 1.查找
					axios.get('https://autumnfish.cn/search', {
						params: {
							keywords: this.content
						}
					}).then(res => {
						// 开始遍历清洗成数据库需要的对象
						res.data.result.songs.forEach(el => {
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
				axios.post("http://39.101.180.27:8089/" + "api/Home/AddSongList", this.SongList).then(res => {
					console.log(res.data);
				}).catch(function(error) {
					console.log(error);
				});
			},
			//关闭
			closeclick() {
				this.collectionisshow = false
				this.ishowxj = false
			},
			// 收藏到歌单
			collectmenu(sid) {
				if (sessionStorage.U_Id != null) {
					this.collectionisshow = true
					this.collectsongid = sid
					// this.getmenu()
				} else {
					layer.msg('请登录~');
				}
			},
			// 添加歌单的确认按钮
			addclick() {
				if (this.addmenuname == "我喜欢的音乐") {
					layer.msg('歌单名不能为\'我喜欢的音乐\'~');
					return
				}
				var data = {
					'uid': sessionStorage.U_Id,
					'name': this.addmenuname,
				}
				axios.post("http://39.101.180.27:8089/" + "api/SongMenu/AddSongMenuToUser", data).then(res => {
					// console.log(res.data);
					this.addmunuidcount = res.data
					// 歌单创建完后重新获取一些用户歌单信息
					console.log("歌单创建成功");
					// this.getmenu() 
					this.CollectionmenuClick(this.addmunuidcount)
					// 页面关闭
					this.ishowxj = false
				}).catch(function(error) {
					console.log(error);
					layer.msg('歌单创建失败~');
					// 新建歌单页面添加完就关闭
					this.ishowxj = true
				});
			},
			// 获取用户歌单数据
			getmenu() {
				request({
					url: '/api/MyInfo/SelUserInfoByID',
					params: {
						id: sessionStorage.U_Id
					}
				}).then(res => {
					// console.log(res.creatSongMenu)
					// console.log(res.)
					this.itemcreatemenu = res.creatSongMenu
					console.log("歌单数据请求成功")
				}).catch(err => {
					console.log('错了')
				})
			},
			// 添加歌曲到已创建歌单
			CollectionmenuClick(mid) {
				request({
					url: '/api/SongInfo/AddCollecting',
					params: {
						uid: sessionStorage.U_Id,
						muneID: mid,
						songID: this.collectsongid
					}
				}).then(res => {
					// console.log(res)
					// 歌曲收藏成功，次数加一
					this.addclooectsong1()
					console.log("收藏成功")
					layer.msg('歌曲收藏成功~');
					this.collectionisshow = false
				}).catch(err => {
					console.log('错了')
				})
			},
			// 歌曲收藏成功后，歌曲收藏次数加1
			addclooectsong1() {
				request({
					url: '/api/SongInfo/AddCollectCount',
					params: {
						id: this.collectsongid
					}
				}).then(res => {
					console.log(res)
					console.log("+1")
				}).catch(err => {
					console.log('错了')
				})
			},
			// 根据输入框内容查询
			searchsongbyname(songname) {
				request({
					url: '/api/Search/SelSongListByName',
					params: {
						name: songname,
						top: 1000,
						page: 0,
					}
				}).then(res => {
					if (res != '') {
						this.songlist = []
						res.forEach(el => {
							this.songlist.push(el)
						})
					} else {
						layer.msg('曲库暂时还没有这个歌曲或歌手啦,听会歌再来试试~');
						this.songlist = ''
						//调用添加歌曲的接口   songname
						this.SongInfo()
					}
				}).catch(err => {
					console.log('错了')
				})
			},
			// 新建歌单
			addmenu() {
				this.collectionisshow = false
				this.ishowxj = true
			},
			play(song) {
				// console.log(sid)
				var getsong = {
					'name': song.sName,
					'singer': song.singerName,
					'cover': song.sCover,
					'url': song.sUrl,
					'id': song.sID,
				}
				this.$store.commit('currplay', getsong)
			},
			// 跳转到歌曲详情页面
			targetsong(songid) {
				this.$router.push({
					path: "./songinfo",
					query: {
						songid: songid
					}
				});
			},
		},
		created() {
			if (this.content != '') {
				this.searchsongbyname(this.content)
			}
			if (sessionStorage.U_Id != null) {
				this.getmenu()
			}
		},
		watch: {
			'$route'(to, from) {
				if (this.$route.query.songname) {
					this.content = this.$route.query.songname
					if (this.content != '') {
						// console.log("123")
						// console.log(this.content);
						this.searchsongbyname(this.content)
					}
					if (sessionStorage.U_Id != null) {
						this.getmenu()
					}
				}
			},
		},
	}
</script>
<style scoped>
	@import url("song.css");
</style>
