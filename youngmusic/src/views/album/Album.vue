<template>
	<div id="appdiv">
		<div class="app-top">
			<div class="app-fl">
				<div class="cover">
					<img src="http://p4.music.126.net/oVZdqfFvxFZQikXZrtSCKg==/109951165090029106.jpg?param=100y100" alt="">
				</div>
			</div>
			<div class="app-fr">
				<div class="info">
					<p class="app-title">专辑标题</p>
					<p class="app-singer"><span class="comm">歌手：</span><a href="javascript:;">歌手名字</a></p>
					<p class="app-album"><span class="comm">发行时间：</span><a href="javascript:;">专辑名字</a></p>
					<p class="app-btn-box">
						<button type="button" class="layui-btn layui-btn-normal"><i class="fa fa-check-circle-o" aria-hidden="true"></i>加入播放列表</button>
						<a href="#pl"><button type="button" class="layui-btn layui-btn-primary"><i class="fa fa-commenting-o" aria-hidden="true"></i>评论</button></a>
					</p>
				</div>
			</div>
		</div>
		<div class="albumInfo">
			<p><b>专辑介绍：</b></p>
			<div>
				adsfasd
				asdsgskjd卡死的回复可接受的但asdasdassafs分色法撒打算阿文大苏打 是的身份和当时的还是肯定会按时大苏打看过
				zz这个是歌曲呸歌 专辑的简介西信息
			</div>
		</div>
		<div class="app-comment">
			<p><span class="app-title">包含歌曲列表</span><span class="comm-number">共 <b>10086</b> 首歌</span></p>
			<hr class="hr-red" />
			<table class="layui-table" lay-even="" lay-skin="nob" style="margin-top: 25px;">
				<colgroup>
					<col width="150">
					<col>
					<col width="150">
					<col width="150">
				</colgroup>
				<thead>
					<tr>
						<th>编号</th>
						<th>标题</th>
						<th>歌手</th>
						<th>播放次数</th>
					</tr>
				</thead>
				<tbody>
					<tr>
						<td>1</td>
						<td><i class="layui-icon layui-icon-play" style="color: brown;margin: 0 5px;"></i><a href="javascript:;">风</a></td>
						<td><a href="javascript:;">阿呆</a></td>
						<td>12</td>
					</tr>
					<tr>
						<td>2</td>
						<td><i class="layui-icon layui-icon-play" style="color: brown;margin: 0 5px;"></i><a href="javascript:;">雨</a>
						</td>
						<td><a href="javascript:;">阿呆</a></td>
						<td>2</td>
					</tr>
					<tr>
						<td>3</td>
						<td><i class="layui-icon layui-icon-play" style="color: brown;margin: 0 5px;"></i><a href="javascript:;">雷</a></td>
						<td><a href="javascript:;">阿呆</a></td>
						<td>4</td>
					</tr>
				</tbody>
			</table>
		</div>
	</div>
</template>

<script>
	export default {
		name: 'Album',
		data(){
			return{
				
			}
		},
		methods:{
			
		},
		created() {
			// 图片上传
				layui.use('upload', function() {
					var $ = layui.jquery,
						upload = layui.upload;
						
					//普通图片上传
					var uploadInst = upload.render({
						elem: '#upimg',
						// http://39.101.180.27:8089/api/Default/PhotoDefault.jpg
						url: 'http://39.101.180.27:8089/api/UpLoad/UserPhotoUpLoad' //改成您自己的上传接口
							,
							data: {
							  id: function(){
							    return 1;
							  }
							},
						before: function(obj) {
							//预读本地文件示例，不支持ie8
							obj.preview(function(index, file, result) {
								$('#img').attr('src', result); //图片链接（base64）
							});
						},
						done: function(res) {
							//如果上传失败
							if (res.code > 0) {
								return layer.msg('上传失败');
							}
							console.log(res);
							//上传成功
							sessionStorage.setItem("U_Img",res)
							layer.msg('头像上传成功')
							// window.location.reload()
						},
						error: function() {
							//演示失败状态，并实现重传
							var demoText = $('#demoText');
							demoText.html(
								'<span style="color: #FF5722;">上传失败</span> <a class="layui-btn layui-btn-xs demo-reload">重试</a>'
							);
							demoText.find('.demo-reload').on('click', function() {
								uploadInst.upload();
							});
						}
					});
			});
		}
	}
</script>

<style scoped="">
@import url("./album.css");
</style>
