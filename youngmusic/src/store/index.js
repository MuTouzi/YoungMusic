import Vue from 'vue'
import Vuex from 'vuex'
// import createPersistedState from "vuex-persistedstate"
// 安装插件
Vue.use(Vuex)

const store = new Vuex.Store({
	// 用来放对象数据
	state: {
		isRandom: false,
		index: 0,
		songList: [],
		// 存放播放方法的
		play:'',
		// 存放登录后的个人全部信息
		userinfo:'',
		// song: {
		// 	name: '',
		// 	singer: '',
		// 	cover: require("../assets/images/tian.gif"),
		// 	url: '../../static/music/夏天的风 - (原唱：温岚).mp3',
		// 	id: 0,
		// },
		songmenu: [			
			{
				name: '要不要买菜',
				singer: '多情种（Cover：胡杨林）',
				cover: require("../assets/images/tian.gif"),
				url: '../../static/music/要不要买菜%20-%20多情种（Cover：胡杨林）.mp3',
				id: 2,
			},
		],
	},
	//用来放方法
	mutations: {
		// 加1
		up(state) {
			if (state.index > 0) {
				state.index--
			} else {
				state.index = state.songmenu.length
			}

		},
		// 减1
		down(state) {
			if (state.isRandom) {
				state.index = parseInt(Math.random() * (state.songmenu.length), 10);
			} else {
				if (state.index < state.songmenu.length - 1) {
					state.index++
				} else {
					state.index = 0
				}
			}

		},
		xd(state, count) {
			state.index = count
		},
		clear0(state) {
			state.index = 0
		},
		Randomtrue(state) {
			state.isRandom = true
		},
		Randomfalse(state) {
			state.isRandom = false
		},
		// 播放歌曲
		currplay(state, Song) {
			//将当前歌曲数据放入歌单的第一个
			state.songmenu.unshift(Song)
			state.index = 0
			state.play()
		},
		// 加入歌单
		joinMenu(state, Song) {
			state.songmenu.push(Song)
		},
		// 清空方法
		clearsongmenu(state) {
			state.songmenu=[]
		},
		// 歌单数据清空后重新获取播放
		playmeunsong(state, Songmenu) {
			state.songmenu.push(Songmenu)
		},
		joinuser(state,user){
			state.userinfo=user
		}
		
	},
	actions: {

	},
	getters: {

	},
	modules: {},
	// plugins:[createPersistedState({
	// 	 storage: window.sessionStorage,
	// 	      reducer(val) {
	// 	          return {
	// 	          // 只储存state中的user
	// 	          user: val.userinfo
	// 	        }
	// 	     }
	// })]
})

export default store
