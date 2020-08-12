<template>
	<div id="head">
		<img src="../assets/images/PlayMusic/up.png">
		<div id="wrap">
			<!-- <audio id="music" :src="url[index]"></audio> -->
			<audio id="music" :src="$store.state.songmenu[$store.state.index].url "></audio>

			<span class="play_left">
				<img src="../assets/images/PlayMusic/last_1.png" class="t_20" @click="up">
				<img src="../assets/images/PlayMusic/play_1.png" class="t_30" id="p1">
				<img src="../assets/images/PlayMusic/stop_1.png" class="t_30" id="p2">
				<img src="../assets/images/PlayMusic/next_1.png" class="t_20" @click="down">
			</span>
			<div class="SongInfo">
				<a href="#"><img :src="$store.state.songmenu[$store.state.index].cover" class="songImg"></a>
				<!-- <a href="javascript:;"><img :src="$store.state.songmenu[$store.state.index].cover | ' '" class="songImg"></a> -->
				<span class="song"><a href="javascript:;">{{$store.state.songmenu[$store.state.index].name}}</a></span>
				<!-- <span class="song"><a href="javascript:;">{{$store.state.songmenu[$store.state.index].name | ''}}</a></span> -->
				<span class="singer"><a href="javascript:;">{{$store.state.songmenu[$store.state.index].singer}}</a></span>
				<!-- <span class="singer"><a href="javascript:;">{{$store.state.songmenu[$store.state.index].singer | ''}}</a></span> -->
				<div class="jinDuTiao">
					<div class="YuanDian"></div>
					<div class="JinDu">
						<div class="progress"></div>
					</div>
				</div>
				<span class="songTime">
					<span class="currentTime">00:00</span> / <span class="totalTime">00:00</span>
				</span>
			</div>

			<span class="play_right">
				<!-- <img src="../assets/images/PlayMusic/collect_1.png" class="t_10"> -->
				<img src="../assets/images/PlayMusic/音量_1.png" class="t_10 vol">
				<div id="volume">
					<div id="vol-back"></div>
				</div>
				<img src="../assets/images/PlayMusic/单曲播放.png" class="t_10 playOrder p1" alt="单曲播放">
				<img src="../assets/images/PlayMusic/随机播放_1.png" class="t_10 playOrder p2" alt="随机播放">
				<img src="../assets/images/PlayMusic/单曲循环.png" class="t_10 playOrder p3" alt="单曲循环">

				<div id="SongList">
					<ul>
						<li v-for="(item,index) in $store.state.songmenu" :key="index" @click="play(index)">{{item.name}}--{{item.singer}}
						</li>
					</ul>
				</div>
				<img src="../assets/images/PlayMusic/list_1.png" class="t_10 list">
			</span>
		</div>
	</div>
</template>

<script>
	import $ from 'jquery'
	import{request} from '../network/request.js'
	export default {
		name: 'musicplayer',
		data() {
			return {
				songplay: '',
				// 是否是随机播放
				isRandom: false,
				// 将vue改成js
				url: [
					"../../static/music/夏天的风 - (原唱：温岚).mp3",
					"../../static/music/要不要买菜%20-%20多情种（Cover：胡杨林）.mp3",
					"../../static/music/贝加尔湖畔%C2%A0钢琴.mp3",
				],
				index: 0,
				songList: [],
			}
		},
		methods: {
			// 上一首
			up() {
				this.$store.commit('up')
				// 更换播放路径后播放
				this.songplay()
			},
			// 下一首
			down() {
				this.$store.commit('down')
				// 更换播放路径后播放
				this.songplay()
			},
			// 歌单的跳转播放
			play(indexs) {
				this.$store.commit('xd', indexs)
				this.songplay()
			},
			// 用户歌曲播放次数加一
			playsongadd1(songid){				
				request({
					url: '/api/MyInfo/AddRecentSong',
					params: {
						id: sessionStorage.U_Id,
						sid:songid
					}
				}).then(res => {
					// console.log(res)
					// console.log("播放次数加1成功")
					this.playsongadd2(songid)
				}).catch(err => {
					// console.log('错了')
					this.playsongadd2(songid)
				})
			},
			// 歌曲播放次数加一
			playsongadd2(songid){				
				request({
					url: '/api/SongInfo/AddPlayCount',
					params: {
						id:songid
					}
				}).then(res => {
					// console.log(res)
					// console.log("播放次数加1成功")
				}).catch(err => {
					console.log('错了')
				})
			},
		},
		mounted() {
			//==============================
			var that = this
			$(function() {
				//获取播放控件
				var _audio = $('#music').get(0);
				//默认的时候让所有的音频加载，
				//否则在火狐ie等浏览器下由于jquery插件的存在导致onloadedmetadata事件不响应
				_audio.load();

				//进度条的计时器管理
				var progressBar = null;

				//当前播放时间的计时器
				var TimeManager = null;
				
				// layui弹出层模块
				layui.use('layer', function() {
					var layer = layui.layer;
				});
					
				//播放按钮被单击
				$("#p1").click(function() {
					// 播放变暂停
					$("#p2").show();
					$(this).hide();
					//播放
					_audio.play(); 
					duration();
				})

				//暂停按钮被单击
				$("#p2").click(function() {
					// 暂停变播放
					$("#p1").show();
					$(this).hide();
					//暂停
					_audio.pause(); 
					clearInterval(TimeManager);
				})

				// 播放顺序  p1:列表循环  p2:随机播放  p3:单曲循环
				// 初始化
				var playOrder = $(".playOrder")
				playOrder.addClass("notShow")
				playOrder.eq(0).removeClass("notShow")
				// 列表循环单击 变随机播放
				$("#wrap .p1").click(function() {
					$(this).addClass("notShow")
					$("#wrap .p2").removeClass("notShow")
					layer.msg('随机播放');
					// that.isRandom = true
					that.$store.commit('Randomtrue')
				})
				// 随机播放单击 变单曲循环
				$("#wrap .p2").click(function() {
					$(this).addClass("notShow")
					$("#wrap .p3").removeClass("notShow")
					// that.isRandom = false
					that.$store.commit('Randomtrue')
					_audio.loop = true
					layer.msg('单曲循环');
				})
				// 单曲循环单击 变列表循环
				$("#wrap .p3").click(function() {
					$(this).addClass("notShow")
					$("#wrap .p1").removeClass("notShow")
					layer.msg('列表循环');
					// that.isRandom = false
					that.$store.commit('Randomfalse')
					_audio.loop = false
				})

				//单击列表按钮 自动切换显示状态
				$("#wrap .list").click(function() {
					$("#SongList").fadeToggle(200);
					$("#volume").hide(200);
					return false;
				})

				//音乐列表阻止事件冒泡
				$("#SongList").click(function() {
					return false;
				})

				//单击音量按钮 自动切换显示状态
				$("#wrap .vol").click(function() {
					$("#volume").slideToggle(200);
					$("#SongList").hide(200);
					return false;
				})

				//音量调整阻止事件冒泡
				$("#volume").click(function() {
					return false;
				})

				//阻止单击音乐播放区的事件冒泡
				$("#wrap").click(function() {
					return false;
				})

				// 事件冒泡 单击页面其他区域 隐藏播放区域 隐藏歌单列表
				// 如果要其他div被显示之后单击其他位置隐藏 增加这个判断
				$(document).click(function() {
					if ($("#SongList").is(':visible')) {
						$("#SongList").fadeOut(200);
						$("#head").stop().animate({
							bottom: "0px"
						})
						$("#wrap").stop().animate({
							bottom: "-55px"
						})
					}
					if ($("#volume").is(':visible')) {
						$("#volume").slideToggle(200);
						$("#head").stop().animate({
							bottom: "0px"
						})
						$("#wrap").stop().animate({
							bottom: "-55px"
						})
					}
				})


				//播放区域的显示隐藏    如果要其他div被显示之后播放器不被隐藏 增加这个判断的 &&
				$("#head").hover(function() {
					if (!$("#SongList").is(':visible') && !$("#volume").is(':visible')) {
						$(this).stop().animate({
							bottom: "55px"
						}, "300ms")
						$("#wrap").stop().animate({
							bottom: "0px"
						}, "300ms")
					}
				}, function() {
					if (!$("#SongList").is(':visible') && !$("#volume").is(':visible')) {
						$(this).stop().animate({
							bottom: "0px"
						})
						$("#wrap").stop().animate({
							bottom: "-55px"
						})
					}
				})
				
				
				//切换播放音乐 显示当前时间和总时间
				that.songplay = function songPlay() {
					_audio.autoplay = true; //自动播放
					// 开始播放，显示暂停
					$("#p2").show();
					$("#p1").hide();
					_audio.addEventListener("loadeddata", 
					//歌曲一经完整的加载完毕( 也可以写成上面提到的那些事件类型) 适用于下一曲时获取时间
						function() {
							duration();
						}, false);
						// 歌曲id
						console.log(that.$store.state.songmenu[that.$store.state.index].id)
						// 播放歌曲就将该歌曲播放次数加一
						that.playsongadd1(that.$store.state.songmenu[that.$store.state.index].id)
						// 判断是否登录，登录了就执行最近播放加一，不登陆就不执行最近播放加一
						// if(sessionStorage.U_Id!=null){
						// 	// 登录了，那么用户的最近播放列表的歌曲播放次数加1，同时歌曲的播放次数加一
						// 	that.playsongadd1(that.$store.state.songmenu[that.$store.state.index].id)
						// }else{
						// 	// 未登录，就歌曲的播放次数加一
						// 	that.playsongadd2(that.$store.state.songmenu[that.$store.state.index].id)
						// }
						
				}
						
					// 将自动播放放入公共变量
					that.$store.state.play=that.songplay
					
				//这个适用于页面首次加载时播放音乐获取时间
				function songPlay2() {
					_audio.autoplay = true; //自动播放
					$("#p2").show();
					$("#p1").hide();
					duration();
				}

				//音频加载完成后的一系列操作 获取总时长
				//_audio.addEventListener('ended', wrap.down(), false);

				function duration() {
					// 清除
					clearInterval(TimeManager);

					//获取总时长
					var time = _audio.duration;
					//分钟
					var minute = time / 60;
					// console.log(time);
					var minutes = parseInt(minute);
					// console.log(minutes);

					if (minutes < 10) {
						minutes = "0" + minutes;
					}
					//秒
					var second = time % 60;
					var seconds = Math.round(second);
					if (seconds < 10) {
						seconds = "0" + seconds;
					}

					//总共时长的秒数
					$(".totalTime").text(minutes + ":" + seconds);

					TimeManager = setInterval(function() {
						timeDisposal(_audio.currentTime);
					}, 1000);
				}
				//当前时长的时间处理
				function timeDisposal(time) {
					//分钟
					var minute = time / 60;
					var minutes = parseInt(minute);
					// console.log(minutes);

					if (minutes < 10) {
						minutes = "0" + minutes;
					}
					//秒
					var second = time % 60;
					var seconds = Math.round(second);
					if (seconds < 10) {
						seconds = "0" + seconds;
					}
					//当前时长
					$(".currentTime").text(minutes + ":" + seconds);
				}
				//进度条原点的位置
				progressBar = setInterval(function() {
					var bfb = eval(_audio.currentTime / _audio.duration);
					if (bfb >= 1) {
						// 大于1就播放下一首
						that.down();
					}
					// 进度条随着播放向前进 增加宽度百分比  这个 +1 是为了隐藏右边圆角
					if (bfb > 0.6) {
						$(".progress").css("width", bfb * 100 + "%")
					} else {
						$(".progress").css("width", bfb * 100 + 1 + "%")
					}

					// console.log(bfb);
					$(".YuanDian").css("left", eval(585 * bfb) + "px");
				}, 1000);

				//进度条的原点的拖拽
				var dian = $(".YuanDian").get(0);
				dian.onmousedown = function() {
					//alert("a");
					var X = event.clientX - dian.offsetLeft;
					var Y = event.clientY - dian.offsetTop;

					document.onmousemove = function() {
						dian.style.left = event.clientX - X + "px";
						dian.style.top = event.clientY - Y + "px";

						if (dian.offsetLeft < 0) {
							dian.style.left = 0 + "px";
						}
						if (dian.offsetLeft > 600 - dian.offsetWidth) {
							dian.style.left = 585 + "px";
						}
						if (dian.offsetTop < -2) {
							dian.style.top = -2 + "px";
						}
						if (dian.offsetTop >= 10 - dian.offsetHeight) {
							dian.style.top = -2 + "px";
						}
						var bfb = dian.offsetLeft / 600
						if (bfb > 0.6) {
							$(".progress").css("width", bfb * 100 + 0.8 + "%")
						} else {
							$(".progress").css("width", bfb * 100 + 1 + "%")
						}
					}
					document.onmouseup = function() {
						document.onmousemove = null;
						document.onmouseup = null;
						// 松开的时候 继续播放
						_audio.currentTime = eval((dian.offsetLeft / 600 + 0.01) * _audio.duration);
						songPlay2();
					}
				}

				// 进度条的点击
				$(".jinDuTiao").click(function() {
					var len = event.clientX - $(this).offset().left
					dian.style.left = len + "px";
					var bfb = len / 600
					if (bfb > 0.6) {
						$(".progress").css("width", bfb * 100 + 0.8 + "%")
					} else {
						$(".progress").css("width", bfb * 100 + 1 + "%")
					}
					// 松开的时候 继续播放
					_audio.currentTime = eval((bfb + 0.01) * _audio.duration);
					songPlay2();
				})

				// 音量大小
				layui.use('slider', function() {
					var $ = layui.$,
						slider = layui.slider;
					//垂直滑块
					slider.render({
						elem: '#vol-back',
						type: 'vertical', //垂直滑块
						height: '100',
						value: '100',
						theme: '#9B0909', //主题色
						change: function(value) {
							console.log(value) //动态获取滑块数值
							_audio.volume = value / 100
						}
					});
				})
			})
		}
	}
</script>

<style>

</style>
