<template>
	<div id="appdiv">
		<!-- 居中 -->
		<div id="app-cendiv">
			<!-- 左边 -->
			<div class="app-left">
				<div class="leftdiv">
					<ul class="yiul">
						<li>
							<b>推荐</b>
							<ul class="erul">
								<li @click="sexclick(0)" :class="[{sexli:ishow==0}]">{{sex[0]}}</li>
							</ul>
							<hr>
						</li>
						<li>
							<b>华语</b>
							<ul class="erul">
								<li @click="sexclick(1)" :class="[{sexli:ishow==1}]">{{sex[1]}}</li>
								<li @click="sexclick(2)" :class="[{sexli:ishow==2}]">{{sex[2]}}</li>
								<li @click="sexclick(3)" :class="[{sexli:ishow==3}]">{{sex[3]}}</li>
							</ul>
							<hr>
						</li>
						<li>
							<b>日本</b>
							<ul class="erul">
								<li @click="sexclick(4)" :class="[{sexli:ishow==4}]">{{sex[4]}}</li>
								<li @click="sexclick(5)" :class="[{sexli:ishow==5}]">{{sex[5]}}</li>
								<li @click="sexclick(6)" :class="[{sexli:ishow==6}]">{{sex[6]}}</li>
							</ul>
							<hr>
						</li>
						<li>
							<b>欧美</b>
							<ul class="erul">
								<li @click="sexclick(7)" :class="[{sexli:ishow==7}]" >{{sex[7]}}</li>
								<li @click="sexclick(8)" :class="[{sexli:ishow==8}]" >{{sex[8]}} </li>
								<li @click="sexclick(9)" :class="[{sexli:ishow==9}]" >{{sex[9]}}</li>
							</ul>
							<hr>
						</li>
						<li>
							<b>其它</b>
							<ul class="erul">
								<li @click="sexclick(10)" :class="[{sexli:ishow==10}]" >{{sex[10]}}</li>
								<li @click="sexclick(11)" :class="[{sexli:ishow==11}]" >{{sex[11]}}</li>
								<li @click="sexclick(12)" :class="[{sexli:ishow==12}]" >{{sex[12]}}</li>
							</ul>
							<hr>
						</li>
					</ul>
				</div>
			</div>
			<!-- 右边 -->
			<div class="app-right">
				<div class="righrdiv">
					<p class="title">
						<span class="t-span">{{sex[ishow]}}</span>
						<span class="con_gengduo"><a href="#">更多>></a></span>
					</p>
					<hr class="layui-bg-red">
					<div id="gedandiv" class="row">
						<div class="col-md-2" v-for="(items,index) in Songmenuitem">
							<a :href="items.url">
								<dl class="dlgedan">
									<dt><img :src="items.url" /></dt>
									<!-- <dt><img :src="{{items.imgsrc}}" </dt> -->
									<dd><a href="#">{{items.title}}{{index+1}}</a></dd>
								</dl>
							</a>
						</div>
					</div>

				</div>
			</div>
		</div>
	</div>

</template>

<script>
	export default {
		name: 'Singer',
		data() {
			return {
				ishow: 0,
				sex:[
					'推荐歌手',
					'华语男歌手',
					'华语女歌手',
					'华语组合/乐队',
					'日本男歌手   ',
					'日本女歌手   ',
					'日本组合/乐队',
					'欧美男歌手   ',
					'欧美女歌手   ',
					'欧美组合/乐队',
					'其它男歌手   ',
					'其它女歌手   ',
					'其它组合/乐队',
				],
				Songmenuitem: [{
						title:'歌手名',
						url: 'https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=2602350908,3703565268&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=1939370566,746500979&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=2602350908,3703565268&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=1390387221,494074822&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=3611851672,1704980968&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss2.bdstatic.com/70cFvnSh_Q1YnxGkpoWK1HF6hhy/it/u=2792227807,4022524439&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=717710321,2367013307&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=1410196640,3773142477&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss2.bdstatic.com/70cFvnSh_Q1YnxGkpoWK1HF6hhy/it/u=2943700442,4107976192&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=819414940,491277439&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=869353365,1786849498&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss0.bdstatic.com/70cFvHSh_Q1YnxGkpoWK1HF6hhy/it/u=1806666910,2107903574&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=2848077753,140733772&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=3283069406,3381458047&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://pic.rmb.bdstatic.com/85e205d0226467e8f97ca36f20f3cbd6.png@wm_2,t_55m+5a625Y+3L+WWteWwj+iDluWoseS5kOi1hOiurw==,fc_ffffff,ff_U2ltSGVp,sz_17,x_11,y_11',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=3374209525,3779134006&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://pic.rmb.bdstatic.com/69f0beb1c9fff93572e608b6e2171155.jpeg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=626486091,2741564521&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://pic.rmb.bdstatic.com/792bfbd904d75ac30c0a99825852dbd3.png@wm_2,t_55m+5a625Y+3L+ioq+WunuaXtg==,fc_ffffff,ff_U2ltSGVp,sz_18,x_12,y_12',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://pic.rmb.bdstatic.com/792bfbd904d75ac30c0a99825852dbd3.png@wm_2,t_55m+5a625Y+3L+ioq+WunuaXtg==,fc_ffffff,ff_U2ltSGVp,sz_18,x_12,y_12',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://pic.rmb.bdstatic.com/2d689237d45b5be31dee2e0fbec4a30f.png@wm_2,t_55m+5a625Y+3L+ahguagkeWbouWbog==,fc_ffffff,ff_U2ltSGVp,sz_18,x_12,y_12',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss2.bdstatic.com/70cFvnSh_Q1YnxGkpoWK1HF6hhy/it/u=1814798421,1855874309&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=1036248470,2340906815&fm=26&gp=0.jpg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://pic.rmb.bdstatic.com/0cd28f78edc72d29d97d58257f173632.jpeg',
						imgsrc: '图片地址'
					},
					{
						title:'歌手名',
						url: 'https://imgsa.baidu.com/forum/w%3D580/sign=063afe672e2dd42a5f0901a3333b5b2f/3f01213fb80e7bec85f7a8ef242eb9389b506b8b.jpg',
						imgsrc: '图片地址'
					}
				]
			}
		},
		methods: {
			sexclick(Index) {
				this.ishow=Index;
			}
		}
	}
</script>

<style scoped>
	@import url("./singer.css");
</style>
