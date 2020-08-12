import Vue from 'vue'
import Router from 'vue-router'

import Index from '../components/Index.vue'
import Friend from '../views/friend/Friend.vue'
import Creation from '../views/creation/Creation.vue'
import MyInfo from '../views/myinfo/MyInfo.vue'
import MyMusic from '../views/mymusic/MyMusic.vue'
import RankingList from '../views/rankinglist/RankingList.vue'
import SongInfo from '../views/songinfo/SongInfo.vue'
import Songmenulist from '../views/songmenulist/Songmenulist.vue'
import Singer from '../views/singer/Singer.vue'
import SongMenuInfo from '../views/songmenuinfo/SongMenuInfo.vue'
import Song from '../views/song/Song.vue'
import Album from '../views/album/Album.vue'

Vue.use(Router)
const originalPush=Router.prototype.push
Router.prototype.push=function push(location){
	return originalPush.call(this,location).catch(err=>err)
}
const routes = [
	{
		path: '',
		redirect: "/index"
	},
	{
		path: '/index',
		component: Index
	},
	// 动态
	{
		path: '/friend',
		component: Friend
	},
	// 创作者
	{
		path: '/creation',
		component: Creation
	},
	// 我的信息
	{
		path: '/info',
		component: MyInfo
	},
	// 我的音乐
	{
		path: '/mymusic',
		component: MyMusic
	},
	// 转接
	{
		path: '/album',
		component: Album
	},
	// 排行榜
	{
		path: '/rankinglist',
		component: RankingList
	},
	// 歌曲详情
	{
		path: '/songinfo',
		name:'SongInfo',
		component: SongInfo
	},
	// 歌单
	{
		path: '/Songmenulist',
		component: Songmenulist
	},// 歌单详情
	{
		path: '/SongMenuInfo',
		component: SongMenuInfo
	},
	// 歌手
	{
		path: '/Singer',
		component: Singer
	},
	//歌曲搜索
	{
		path: '/song',
		component: Song
	},
]
export default new Router({
	routes:routes,
	// mode: 'history',
	base: process.env.BASE_URL,
})
