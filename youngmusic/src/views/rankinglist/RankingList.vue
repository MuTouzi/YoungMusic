<template>
	<div id="app-cendiv">
			<!-- 左边 -->
					<div class="app-left">
						<div class="layui-collapse input-div">
							<div class="layui-colla-item">
								<p class="layui-colla-title">云音乐特色榜
								</p>
								<div class="layui-colla-content layui-show">
									<ul>
										<li>
											<a href="#">
												<img src="../../assets/images/tian.gif">
												<div class="lidiv">
													<p>飙升榜</p>
													<p>每天更新</p>
												</div>
											</a>
										</li>
										<li>
											<a href="#">
												<img src="../../assets/images/tian.gif">
												<div class="lidiv">
													<p>新歌榜</p>
													<p>每天更新</p>
												</div>
											</a>
										</li>
										<li>
											<a href="#">
												<img src="../../assets/images/tian.gif">
												<div class="lidiv">
													<p>原创歌曲榜</p>
													<p>每天更新</p>
												</div>
											</a>
										</li>
										<li>
											<a href="#">
												<img src="../../assets/images/tian.gif">
												<div class="lidiv">
													<p>热歌榜</p>
													<p>每天更新</p>
												</div>
											</a>
										</li>
									</ul>
								</div>
							</div>
						</div>
					</div>
					
					<!-- 右边 -->
								<div class="app-right">
									<div class="right-top">
										<!-- 歌单头像 -->
										<div class="imgdiv">
											<img src="../../assets/images/tian.gif">
										</div>
										<!-- 榜div -->
										<div class="menudiv">
											<p>榜名</p>
											<div class="divinfo">
												<p>
													<i class="layui-icon layui-icon-time"></i>最近更新<u>2016-10-16</u>(每天更新)
												</p>
											</div>
											<div class="btndiv layui-icon">
												<button type="button" class="layui-btn layui-btn-normal"><i class="fa fa-play-circle-o"
														aria-hidden="true"></i>播放</button>
												<button type="button" class="layui-btn layui-btn-primary"><i class="fa fa-heart"
														aria-hidden="true"></i>收藏</button>
												<button type="button" class="layui-btn layui-btn-primary"><i class="fa fa-heart"
														aria-hidden="true"></i>转发</button>
												<button type="button" class="layui-btn layui-btn-primary"><i class="fa fa-commenting-o"
														aria-hidden="true"></i>评论</button>
											</div>
										</div>
									</div>
									<!-- 歌曲列表 -->
									<div class="right-paihang">
										<div class="ph-div-h">
											<p class="ph-h">歌曲列表<span class="ph-span">100</span>首歌
														<span class="ph-span-count">播放次数:<span>112312</span></span>
											</p>
										</div>
										<hr class="layui-bg-red ">
										<div class="phdiv">
											<table class="layui-table " lay-size="sm">
												<colgroup>
													<col width="10">
													<col width="200">
													<col>
												</colgroup>
												<thead>
													<tr>
														<th></th>
														<th>歌曲标题</th>
														<th>时长</th>
														<th> 歌手</th>
														<th>专辑</th>
													</tr>
												</thead>
												<tbody>
													<tr>
														<td>1</td>
														<td> <a href="#">分手快乐</a></td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td><a href="#">专辑名</a></td>
													</tr>
													<tr>
														<td>2</td>
														<td> <a href="#">分手快乐</a>2</td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td></td>
													</tr>
													<tr>
														<td>1</td>
														<td> <a href="#">分手快乐</a></td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td><a href="#">专辑名</a></td>
													</tr>
													<tr>
														<td>2</td>
														<td> <a href="#">分手快乐</a>2</td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td></td>
													</tr>
													<tr>
														<td>1</td>
														<td> <a href="#">分手快乐</a></td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td><a href="#">专辑名</a></td>
													</tr>
													<tr>
														<td>2</td>
														<td> <a href="#">分手快乐</a>2</td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td></td>
													</tr>
													<tr>
														<td>1</td>
														<td> <a href="#">分手快乐</a></td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td><a href="#">专辑名</a></td>
													</tr>
													<tr>
														<td>2</td>
														<td> <a href="#">分手快乐</a>2</td>
														<td>03:14</td>
														<td> <a href="#">寒风</a></td>
														<td></td>
													</tr>
												</tbody>
											</table>
										</div>
									</div>
									<!-- 评论 -->
									<div id="pl-div">
										<div class="ph-div-h">
											<p class="ph-h">评论<span class="ph-span">100</span>条评论 </p>
											<hr class="layui-bg-red ">
										</div>
										<div>
											<div class="pl-img">
												<img src="../../assets/images/tian.gif">
											</div>
											<div>
												先空着吧
											</div>
											<div>
												最新评论
											</div>
										</div>
									</div>
								</div>
					
					
	</div>
</template>

<script>
	export default {
	  name: 'RankingList'
	}
</script>

<style scoped>
	@import url("./rankinglist.css");
	</style>
