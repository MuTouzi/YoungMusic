<template>
<div id="appdiv">
    <!-- 新建歌单 -->
    <div id="xj" v-if="ishowxj">
        <div class="logindiv">
            <div>
                <div class="l-top">
                    <span class="l-title">新建歌单</span>
                    <i class="fa fa-times close" aria-hidden="true" @click="closeclick()"></i>
                </div>
                <div class="mid">
                    <input type="text" name="title" lay-verify="required" placeholder="请输入新建的歌单名" autocomplete="off" class="layui-input textmenu" v-model="addmenuname" />
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
                <i class="fa fa-times close" aria-hidden="true" @click="closeclick()"></i>
            </div>
            <div class="mids">
                <ul>
                    <li class="add">
                        <div class="newadd">
                            <i class="layui-icon layui-icon-add-1" aria-hidden="true" @click="addmenu()">&nbsp;创建新的歌单</i>
                        </div>
                    </li>
                    <li class="hover" v-for="(item, index ) in itemcreatemenu" :key="index" @click="CollectionmenuClick(item.M_Id)">
                        <img :src="imgurl+item.M_Img" />
                        <div class="lidivadd">
                            <p class="pname">{{item.M_Name}}</p>
                            <p class="pcount">{{item.M_SongId.split(',').length-1}}首</p>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <div class="app-top">
        <div class="app-fl">
            <div class="cover">
                <img v-if="this.getsong.cover" :src="this.getsong.cover" />
            </div>
        </div>
        <div class="app-fr">
            <div class="info">
                <p class="app-title">{{this.getsong.name}}</p>
                <p class="app-singer">
                    <span class="comm">歌手：</span>
                    <a href="javascript:;">{{this.getsong.singer}}</a>
                </p>
                <p class="app-album">
                    <!-- <span class="comm">专辑：</span>
                    <a href="javascript:;">专辑</a> -->
                </p>
                <p class="app-btn-box">
                    <button type="button" class="layui-btn layui-btn-normal" @click="play()">
                        <i class="fa fa-play-circle-o" aria-hidden="true"></i>播放
                    </button>
                    <button type="button" class="layui-btn layui-btn-primary" @click="joinmenu()">
                        <i class="fa fa-check-circle-o" aria-hidden="true"></i>加入播放列表
                    </button>
                    <button type="button" class="layui-btn layui-btn-primary" @click="collectmenu()">
                        <i class="fa fa-heart" aria-hidden="true"></i>收藏
                    </button>
                    <br>
                    <br>
                    <button type="button" class="layui-btn layui-btn-primary" @click="zf()">
                        <i class="fa fa-share" aria-hidden="true"></i>转发
                    </button>
                    <a :href="getsong.url" download>
                        <button type="button" class="layui-btn layui-btn-primary">
                            <i class="fa fa-cloud-download" aria-hidden="true"></i>下载
                        </button>
                    </a>
                    <a href="#pl">
                        <button type="button" class="layui-btn layui-btn-primary">
                            <i class="fa fa-commenting-o" aria-hidden="true"></i>评论
                        </button>
                    </a>
                </p>
            </div>
        </div>
    </div>

    <div class="app-comment" id="pl">
        <p>
            <span class="app-title">评论</span>
            <!-- <span class="comm-number">共 <b>18</b> 条</span> -->
        </p>
        <hr class="hr-red" />
        <div class="Dialog">
            <img v-show="userid" :src="imgurl+uimg" class="avatar" />
            <img v-show="!userid" v-if="this.getsong.cover" :src="this.getsong.cover" class="avatar" />
            <div class="content">
                <p class="talk">
                    <textarea name required lay-verify="required" placeholder="请输入评论" class="textarea" v-model="content"></textarea>
                </p>
            </div>
        </div>
        <div class="commSub" style="float: right;">
            <span class="wordCount">{{descNum}}</span>
            <button type="button" class="layui-btn layui-btn-sm layui-btn-normal" @click="addcomment()">评论</button>
        </div>
        <div class="splendid">
            <p class="comment-title">精彩评论</p>
            <hr />
            <div class="pl-info-all">
                <ul>
                    <!-- ===============主评论板块 ul结构生成================ -->
                    <li v-for="(items,index) in commentsong" :key="index">
                        <img :src="imgurl+items.From_UserInfo.U_Img" class="pl-tx" alt />
                        <div class="pl-u-info">
                            <a href="javascript:;" @click="openfriend(items.SC_From_User)">{{items.From_UserInfo.U_Name}}</a> ：
                            <span class="contentsty">
                                {{items.SC_Content}}
                                <span class="msg" @click="huifu(index)">回复</span>
                            </span>
                            <!-- 回复框div -->
                            <div v-show="huifuisshow&&index==i" class="talk">
                                <p>
                                    <textarea v-model="huifucontent" required lay-verify="required" placeholder="请输入回复内容" class="layui-textarea huifu"></textarea>
                                </p>
                                <p>
                                    <span>{{descNum2}}</span>
                                    <button type="button" class="layui-btn layui-btn-sm layui-btn-normal" @click="btnhuifu(items.SC_Id,items.SC_From_User,userid)">回复</button>
                                </p>
                            </div>
                            <!-- ==============主评论的回复板块  由原先的ul 改为div============== -->
                            <div class="reply">
                                <div v-for="(son ,indexs) in items.ChildrenComm" :key="indexs" class="reply-inner">
                                    <!-- =====回复信息显示的位置===== -->
                                    <a href="javascript:;" @click="openfriend(son.From_UserInfo.U_Id)">
                                        <img :src="imgurl+son.From_UserInfo.U_Img" />
                                        &nbsp;{{son.From_UserInfo.U_Name}}
                                    </a> &nbsp; 回复&nbsp;
                                    <a href="javascript:;" @click="openfriend(son.To_UserInfo.U_Id)">
                                        <img :src="imgurl+son.To_UserInfo.U_Img" />
                                        &nbsp;{{son.To_UserInfo.U_Name}}
                                    </a>
                                    ：
                                    <span class="contentsty">
                                        {{son.SC_Content}}
                                        <span class="msg msg2" @click="huifu2(indexs,index)">回复</span>
                                    </span>
                                    <!-- =============回复框div========== -->
                                    <div v-show="huifuisshow2&&indexs==i2&&songhuifuclass==index" class="talk talk-inner">
                                        <p>
                                            <textarea v-model="huifucontent" required lay-verify="required" placeholder="请输入回复内容" class="layui-textarea huifu huifu2"></textarea>
                                        </p>
                                        <p>
                                            <span>{{descNum2}}</span>
                                            <button type="button" class="layui-btn layui-btn-sm layui-btn-normal" @click="btnhuifu(son.SC_Pid,son.From_UserInfo.U_Id,userid)">回复</button>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <span style="color: #c2c2c2;margin-left: 20px;position: relative;top: 10px;line-height: 30px;">{{items.SC_UpTime}}</span>
                        </div>
                    </li>
                </ul>
            </div>
            <hr class="layui-bg-gray" />
        </div>
    </div>

</div>
</template>

<script>
import {
    request
} from "../../network/request.js";
import axios from "axios";
export default {
    name: "SongInfo",
    data() {
        return {
            ishowxj: false,
            imgurl: sessionStorage.img,
            collectionisshow: false,
            userid: sessionStorage.U_Id,
            // 回复框的显示
            huifuisshow: false,
            huifuisshow2: false,
            huifuisshow3: false,
            // 控制一个回复框显示
            i: 0,
            i2: 0,
            // 用户的歌单数据
            itemcreatemenu: "",
            //歌曲数据
            songdata: {},
            // 评论数据
            commentsong: "",
            // 评论条数
            commentcount: 0,
            songid: this.$route.query.songid,
            // 清洗数据的
            getsong: {
                name: "",
                singer: "",
                cover: "",
                url: "",
                id: ""
            },
            addmenuname: "",
            // 用户头像
            uimg: sessionStorage.U_Img,
            // 评论框输入的内容
            content: "",
            // 回复的内容
            huifucontent: "",
            // 监听发表评论的字数
            fontcount: 0,
            addmunuidcount: 0,
            //控制子回复框的显示与隐藏
            songhuifuclass: 0,
        };
    },
    methods: {
        // 过去歌曲详细信息显示
        loadgetsonginfo() {
            request({
                    url: "api/SongInfo/SelSongInfoByID",
                    params: {
                        id: this.songid
                    }
                })
                .then(res => {
                    this.songdata = res;
                    this.getsong.name = this.songdata[0].sName;
                    this.getsong.singer = this.songdata[0].singerName;
                    this.getsong.cover = this.songdata[0].sCover;
                    this.getsong.url = this.songdata[0].sUrl;
                    this.getsong.id = this.songid;

                    // console.log(this.songdata)
                    // console.log(res)
                })
                .catch(err => {
                    console.log(err);
                });
        },
        // 获取歌曲评论信息
        loadgetcomment() {
            request({
                    url: "/api/SongInfo/GetSongComment",
                    params: {
                        id: this.songid
                    }
                })
                .then(res => {
                    // console.log(res)
                    this.commentsong = res;
                })
                .catch(err => {
                    console.log("错了");
                });
        },
        // 播放按钮
        play() {
            this.$store.commit("currplay", this.getsong);
            // this.$store.commit('zdplays')
        },
        // 转发
        zf() {
            var that = this
            layer.prompt({
                formType: 2,
                // value: '输入想说的话吧~',
                title: '转发',
            }, function (value, index, elem) {
                var data = {
                    userId: that.userid,
                    // userId:sessionStorage.U_Id,
                    title: '',
                    content: value,
                    songID: that.songid,
                    imgs: '',
                };
                axios.post(
                        "http://39.101.180.27:8089/" + "api/Dynamic/AddDynamic",
                        data
                    ).then(res => {
                        console.log(res.data);
                        if (res.data == "发布成功") {
                            layer.msg("发布成功")
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
                layer.close(index);
            });
        },
        // 添加歌单的确认按钮
        addclick() {
            if (this.addmenuname == "我喜欢的音乐") {
                layer.msg("歌单名不能为我喜欢的音乐~");
                return;
            }
            var data = {
                uid: sessionStorage.U_Id,
                name: this.addmenuname
            };
            axios
                .post(
                    "http://39.101.180.27:8089/" + "api/SongMenu/AddSongMenuToUser",
                    data
                )
                .then(res => {
                    // console.log(res.data);
                    this.addmunuidcount = res.data;
                    // 歌单创建完后重新获取一些用户歌单信息
                    console.log("歌单创建成功");
                    // this.getmenu()
                    this.CollectionmenuClick(this.addmunuidcount);
                    // 页面关闭
                    this.ishowxj = false;
                })
                .catch(function (error) {
                    console.log(error);
                    layer.msg("歌单创建失败~");
                    // 新建歌单页面添加完就关闭
                    this.ishowxj = true;
                });
        },
        // 新建歌单样式
        addmenu() {
            this.collectionisshow = false;
            this.ishowxj = true;
        },
        // 加入播放歌单歌单
        joinmenu() {
            this.$store.commit("joinMenu", this.getsong);
        },
        // 获取用户歌单数据
        getmenu() {
            request({
                    url: "/api/MyInfo/SelUserInfoByID",
                    params: {
                        id: sessionStorage.U_Id
                    }
                })
                .then(res => {
                    // console.log(res.creatSongMenu)
                    // console.log(res.)
                    this.itemcreatemenu = res.creatSongMenu;
                    console.log("歌单数据请求成功");
                })
                .catch(err => {
                    console.log("错了");
                });
        },
        // 收藏到歌单
        collectmenu() {
            if (sessionStorage.U_Id != null) {
                this.collectionisshow = true;
                this.getmenu();
            } else {
                // alert("请登录")
                layer.msg("请登录~");
            }
        },
        // 歌曲收藏成功后，歌曲收藏次数加1
        addclooectsong1() {
            request({
                    url: "/api/SongInfo/AddCollectCount",
                    params: {
                        id: this.songid
                    }
                })
                .then(res => {
                    console.log(res);
                    console.log("+1");
                })
                .catch(err => {
                    console.log("错了");
                });
        },
        // 将数据传入后添加到收藏歌单
        CollectionmenuClick(mid) {
            request({
                    url: "/api/SongInfo/AddCollecting",
                    params: {
                        uid: sessionStorage.U_Id,
                        muneID: mid,
                        songID: this.songid
                    }
                })
                .then(res => {
                    // console.log(res)
                    console.log("收藏成功");
                    layer.msg("歌单收藏成功~");
                    this.addclooectsong1();
                    this.collectionisshow = false;
                })
                .catch(err => {
                    console.log("错了");
                });
        },
        //关闭
        closeclick() {
            this.collectionisshow = false;
            this.ishowxj = false;
        },
        //查看朋友信息
        openfriend(id) {
            this.$router.push({
                path: "./info",
                query: {
                    friendid: id
                }
            });
        },
        // 发表评论
        addcomment() {
            if (this.userid == null) {
                layer.msg("请登录~");
                return;
            }
            var data = {
                sid: this.songid,
                pid: "",
                from: sessionStorage.U_Id,
                content: this.content,
                to: ""
            };
            axios
                .post(
                    "http://39.101.180.27:8089/" + "api/SongInfo/AddSongComment",
                    data
                )
                .then(res => {
                    // console.log(res.data);
                    layer.msg("发表成功~");
                    this.content = "";
                    // 获取评论信息
                    this.loadgetcomment();
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        // 回复样式
        huifu(index) {
            // 将另外一个回复框隐藏
            this.huifuisshow2 = false
            this.huifuisshow = !this.huifuisshow;
            this.i = index;
        },
        // 回复
        huifu2(index, index2) {
            this.songhuifuclass = index2
            // 将另外一个回复框隐藏
            this.huifuisshow = false
            console.log(index);
            console.log(index2);
            this.huifuisshow2 = !this.huifuisshow2;
            this.i2 = index;
        },
        // 回复评论按钮
        btnhuifu(pid, toid, fromid) {
            if (this.userid == null) {
                layer.msg("请登录~");
                return;
            }
            var data = {
                sid: this.songid,
                pid: pid,
                from: fromid,
                content: this.huifucontent,
                to: toid
            };
            axios
                .post(
                    "http://39.101.180.27:8089/" + "api/SongInfo/AddSongComment",
                    data
                )
                .then(res => {
                    // console.log(res.data);
                    layer.msg("评论成功~");
                    this.huifuisshow = false;
                    this.huifuisshow2 = false;
                    this.huifucontent = "";
                    // 获取评论信息
                    this.loadgetcomment();
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    },
    mounted() {
        if (this.songid != null) {
            // 根据传递的歌曲id显示数据
            this.loadgetsonginfo();
            // 获取评论信息
            this.loadgetcomment();
        }
    },
    computed: {
        descNum() {
            if (this.content.length >= 140) {
                this.content = this.content.substr(0, 140);
                return 0;
            }
            return this.content.length == 140 ? 140 : 140 - this.content.length;
        },
        descNum2() {
            if (this.huifucontent.length >= 140) {
                this.huifucontent = this.huifucontent.substr(0, 140);
                return 0;
            }
            return this.huifucontent.length == 140 ?
                140 :
                140 - this.huifucontent.length;
        }
    }
};
</script>

<style scoped>
@import url("./songinfo.css");
</style>
