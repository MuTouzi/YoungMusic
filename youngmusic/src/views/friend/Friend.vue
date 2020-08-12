<template>
<div>
    <div id="adddiv" v-show="adddivshow">
        <div class="adddivson">
            <div class="l-top">
                <span class="l-title">发布动态</span>
                <i class="fa fa-times close" aria-hidden="true" @click="escclick()"></i>
            </div>
            <div id="flow">
                <form class="layui-form" id="formlayui" action="" @keydown.enter="false">
                    <div id="tea">
                        <textarea id="demo" class="layui-textarea layui-hide" name="content" lay-verify="content" placeholder="请输入内容"></textarea>
                    </div>
                    <div id="imgmorediv">
                        <i class="fa fa-picture-o fa-1 fa-icon" aria-hidden="true" id="moreimg"></i>
                        &nbsp;
                        <i class="fa fa-music fa-2 fa-icon" aria-hidden="true" @click="musicdivisshowfun()"></i>
                        &nbsp;<span style="font-size: 0.8em;">{{songTitle}}</span>
                        <div class="layui-upload-list" id="demo2"></div>
                    </div>
                    <div class="musicdiv" v-show="musicdivisshow">
                        <input type="text" class="layui-input" placeholder="按回车键进行搜索" @keydown.enter.stop.prevent="searchsongbyname()" v-model.trim="searchValue">
                        <!-- @input="musicchange($event)" -->
                        <ul>
                            <li v-for="item in songlist" :key="item.sID" @click="getsong(item.sID)">{{item.sName}}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;——&nbsp;&nbsp;{{item.singerName}}</li>
                        </ul>
                    </div>
                    <br />
                    <div class="divbtn">
                        <!-- @click="adddtclick()" -->
                        <!-- @click="adddtclick()" -->
                        <button type="button" lay-submit="" lay-filter="sure" class="layui-btn layui-btn-normal">发布</button>
                        <button type="button" class="layui-btn  layui-btn-primary" @click="escclick()">取消</button>
                    </div>
                </form>
            </div>

        </div>
    </div>

    <div id="app-cendiv">
        <!-- left -->
        <div class="app-left">
            <div class="left-center">
                <div class="dtdiv layui-icon ">
                    <p>
                        动态
                        <button class="layui-btn layui-btn-sm layui-btn-radius layui-bg-red btn " @click="open()">&#xe642;
                            发动态</button>
                    </p>
                </div>
                <hr class="layui-bg-red hr-red">
                <!-- 动态 -->
                <div class="div-dl">
                    <div style="text-align: center;" v-show="frienditems.length<=0">
                        <h1>还没好友发布动态呢，快去关注一下吧</h1>
                    </div>
                    <div class="div-list" v-for="(fri,index) in frienditems" :key="index">
                        <div class="dl-img">
                            <img :src="imgurl+fri.Dynamic.U_info.U_Img">
                        </div>
                        <div class="dl-list-info ">
                            <p><a href="javascript:;" @click="openfriend(fri.Dynamic.U_info.U_Id)">{{fri.Dynamic.U_info.U_Name}}</a></p>
                            <p>{{fri.Dynamic.D_UpTime}}</p>
                            <p v-html="fri.Dynamic.D_Content">
                            </p>
                            <!-- 如果动态没歌曲就不显示 -->
                            <p v-show="fri.Song" v-for="(songitems ,indexs) in fri.Song " :key="indexs" class="songp">
                                <img :src="songitems.sCover" />
                                <a href="javascript:;" @click="targetsong(songitems.sID)">{{songitems.sName}}</a>
                                <a href="javascript:;">{{songitems.singerName}}</a>
                            </p>
                            <p class="dtimg" v-show="imgitems[index]">
                                <img :src="v" v-for=" (v,i) in imgitems[index]" :key="i">
                            </p>
                            <p class="diazan">
                                <span class="layui-icon" lay-separator="-">
                                    <a href="javascript:;" title="点赞" @click.once="good(fri.Dynamic.D_Id,index)">&#xe6c6;<span>({{fri.Dynamic.D_GoodCount}})</span></a>
                                    <!-- <a href="javascript:;">转发<span>(123)</span></a> -->
                                    <a href="javascript:;" class="index-a" @click="plclick(index,fri.Dynamic.D_Id)" title="点击展开评论">评论</a>
                                    <a href="javascript:;" class="index-a" @click="deldt(fri.Dynamic.D_Id)" v-show="fri.Dynamic.D_UserId==userid" title="点击删除该动态">删除</a>
                                    <!-- <span>(23)</span> -->
                                </span>
                            </p>
                            <!-- {dtplshow:i==isshow} -->
                            <div :class="['dt-pl','app-comment',{dtplshow:index==isshow}]" id="pl">
                                <div class="Dialog">
                                    <!-- <img v-show="userid" :src="imgurl+uimg" class="avatar" />
                                    <img v-show="!userid" v-if="this.getsong.cover" :src="this.getsong.cover" class="avatar" /> -->
                                    <div class="content">
                                        <p class="talk">
                                            <textarea name required lay-verify="required" placeholder="请输入评论" class="textarea" v-model="content"></textarea>
                                        </p>
                                    </div>
                                </div>
                                <div class="commSub" style="float: right;">
                                    <span class="wordCount">{{descNum}}</span>
                                    <button type="button" class="layui-btn layui-btn-sm layui-btn-normal" @click="addcomment(index,fri.Dynamic.D_Id)">评论</button>
                                </div>
                                <div class="splendid">
                                    <p class="comment-title">精彩评论</p>
                                    <hr />
                                    <div class="pl-info-all">
                                        <ul>
                                            <!-- ===============主评论板块 ul结构生成================ -->
                                            <li v-for="(items,index) in dtcomment" :key="index">
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
                                                            <button type="button" class="layui-btn layui-btn-sm layui-btn-normal" @click="btnhuifu(index,fri.Dynamic.D_Id,items.SC_Id,items.SC_From_User,userid)">回复</button>
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
                                                                    <button type="button" class="layui-btn layui-btn-sm layui-btn-normal" @click="btnhuifu(index,fri.Dynamic.D_Id,son.SC_Pid,son.From_UserInfo.U_Id,userid)">回复</button>
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <span style="color: #c2c2c2;margin-left: 20px;position: relative;top: 10px;line-height: 30px;">{{items.SC_UpTime}}</span>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <hr class="layui-bg-gray">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- right -->
        <div class="app-rightfriend">
            <div class="right-top">
                <div class="right-top-top">
                    <div class="imgdiv">
                        <img :src="imgurl+userinfoitem.U_Img">
                    </div>
                    <div class="right-top-info">
                        <p class="p-name">{{userinfoitem.U_Name}}</p>
                        <p class="p-info">
                            {{userinfoitem.U_Info}}
                        </p>
                    </div>
                </div>
                <div class="span-bolck">
                    <span class="user-info" lay-separator="|">
                        <a href="javascript:;" @click="showmyseif()">动态<span class="layui-badge layui-bg-blue">{{dtncount}}</span></a>
                        <a href="javascript:;">关注<span class="layui-badge layui-bg-orange">{{follow}}</span></a>
                        <a href="javascript:;">粉丝<span class="layui-badge ">{{ fanslength}}</span></a>
                    </span>
                </div>
            </div>
            <!-- 用户 -->
            <div class="userdiv">
                <div class="user-mx">
                    <h6>明星用户</h6>
                    <a>换一批</a>
                    <hr class="layui-bg-red">
                    <div class="userul">
                        <ul>
                            <li>
                                <img src="../../assets/images/tian.gif">
                                <div class="lidiv">
                                    <p>华晨宇</p>
                                    <p>知名歌手</p>
                                </div>
                                <button type="button" class="layui-btn layui-btn-xs  btn-xj layui-icon  layui-btn-normal">&#xe654;关注</button>
                            </li>
                            <li>
                                <img src="../../assets/images/tian.gif">
                                <div class="lidiv">
                                    <p>歌手12313213212313231231</p>
                                    <p>912313213139</p>
                                </div>
                                <button type="button" class="layui-btn layui-btn-xs  btn-xj  layui-icon  layui-btn-normal">&#xe654;关注</button>
                            </li>
                            <li>
                                <img src="../../assets/images/tian.gif">
                                <div class="lidiv">
                                    <p>我喜欢的音乐</p>
                                    <p>9912312313212312312312312313 </p>
                                </div>
                                <button type="button" class="layui-btn layui-btn-xs  btn-xj layui-icon  layui-btn-normal">&#xe654;关注</button>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="user-friend">
                    <h6>感兴趣的人</h6>
                    <a>换一批</a>
                    <hr class="layui-bg-red">
                    <div class="userul">
                        <ul>
                            <li>
                                <img src="../../assets/images/tian.gif">
                                <div class="lidiv">
                                    <p>我喜欢的音乐</p>
                                    <p>99 </p>
                                </div>
                                <button type="button" class="layui-btn layui-btn-xs  btn-xj layui-icon  layui-btn-normal">&#xe654;关注</button>
                            </li>
                            <li>
                                <img src="../../assets/images/tian.gif">
                                <div class="lidiv">
                                    <p>我喜欢的音乐</p>
                                    <p>99 </p>
                                </div>
                                <button type="button" class="layui-btn layui-btn-xs  btn-xj layui-icon  layui-btn-normal">&#xe654;关注</button>
                            </li>
                            <li>
                                <img src="../../assets/images/tian.gif">
                                <div class="lidiv ">
                                    <p>我喜欢的音乐</p>
                                    <p>99 </p>
                                </div>
                                <button type="button" class="layui-btn layui-btn-xs  btn-xj  layui-icon  layui-btn-normal">&#xe654;关注</button>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

    </div>

</div>
</template>

<script>
import $ from 'jquery'
import {
    request
} from "../../network/request.js";
import axios from 'axios'
import {
    moreimg
} from '../../assets/js/more.js'
import {
    onDrag
} from "../../assets/js/onDrag.js";

// 节流函数
const delay = (function () {
    let timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

export default {
    name: '',
    data() {
        return {
            isshow: -1,
            imgurl: sessionStorage.img,
            adddivshow: false,
            // 存储用户信息
            userinfoitem: '',
            // 存储用户动态量
            dtncount: 0,
            // 存放用户关注的动态
            frienditems: '',
            // 控制音乐div
            musicdivisshow: false,
            // 存放选择的音乐id
            songid: '',
            searchValue: '',
            songlist: '',
            // 关于评论区域的一些数据
            // 存放评论的数据
            dtcomment: '',
            userid: sessionStorage.U_Id,
            // 用户头像
            uimg: sessionStorage.U_Img,
            // 评论框输入的内容
            content: "",
            // 回复框的显示
            huifuisshow: false,
            huifuisshow2: false,
            huifuisshow3: false,
            // 控制一个回复框显示
            i: 0,
            i2: 0,
            // 回复的内容
            huifucontent: "",
            // 监听发表评论的字数
            fontcount: 0,
            addmunuidcount: 0,
            //控制子回复框的显示与隐藏
            songhuifuclass: 0,
            teaup: '',
            imgup: '',
            imgitems: [],
            songTitle: '',
        }
    },
    created() {
        // 加载时获取用户信息
        this.loadgetuserinfo()
        // 获取动态量
        this.loadgetdtcount()
        // 获取动态数据
        this.loadgetdtitem()
    },
    methods: {
        // 加载时获取用户信息
        loadgetuserinfo() {
            if (this.userid != null) {
                request({
                        url: "/api/MyInfo/SelUserInfoByID",
                        params: {
                            id: this.userid
                        }
                    })
                    .then(res => {
                        // console.log(res)
                        this.userinfoitem = res.userInfo
                    })
                    .catch(err => {
                        console.log(err);
                    });
            }
        },
        //加载时获取用户好友的动态
        loadgetdtitem() {
            if (this.userid != null) {
                request({
                    url: '/api/Dynamic/GetDynamicInfo',
                    params: {
                        uid: this.userid,
                        top: 1000,
                        page: 0
                    }
                }).then(res => {
                    // console.log(res);
                    this.frienditems = res
                    var img = []
                    this.frienditems.forEach(el => {
                        if (el.Dynamic.D_imgs != "") {
                            img = []
                            var arr = el.Dynamic.D_imgs.split(',')
                            img.push(arr)
                        }
                        this.imgitems.push(arr)
                    })
                    // console.log(this.imgitems);
                }).catch(err => {
                    console.log(err);
                })

            }
        },
        // 加载时获取用户的动态量(次数)
        loadgetdtcount() {
            if (this.userid != null) {
                request({
                    url: '/api/Dynamic/GetDynamicCount',
                    params: {
                        id: this.userid
                    }
                }).then(res => {
                    this.dtncount = res
                }).catch(err => {
                    console.log(err);
                })
            }
        },
        // 查看自己的动态
        showmyseif() {
            if (this.userid != null) {
                request({
                    url: '/api/Dynamic/GetMyDynamicInfo',
                    params: {
                        uid: this.userid,
                        top: 100,
                        page: 0
                    }
                }).then(res => {
                    console.log(res);
                    this.frienditems = res
                    var img = []
                    this.frienditems.forEach(el => {
                        if (el.Dynamic.D_imgs != "") {
                            img = []
                            var arr = el.Dynamic.D_imgs.split(',')
                            img.push(arr)
                        }
                        this.imgitems.push(arr)
                    })
                    // console.log(this.imgitems);
                }).catch(err => {
                    console.log(err);
                })
            }
        },
        // 发布动态样式
        open() {
            // moreimg()
            var that = this
            layui.use(['form', 'layedit'], function () {
                var form = layui.form,
                    layer = layui.layer,
                    layedit = layui.layedit
                //创建一个编辑器
                var editIndex = layedit.build('demo', {
                    tool: [
                        'strong' //加粗
                        , 'italic' //斜体
                        , 'underline' //下划线
                        , 'del' //删除线
                        , '|' //分割线
                        , 'left' //左对齐
                        , 'center' //居中对齐
                        , 'right' //右对齐
                        , 'face' //表情
                    ],
                    height: 200,
                });

                //自定义验证规则
                form.verify({
                    content: function (value) {
                        layedit.sync(editIndex);
                    },
                });
                //监听提交
                form.on('submit(sure)', function (data) {
                    that.teaup = data.field.content
                    // 执行发布动态的功能事件
                    that.adddtclick()
                    layui.layedit.build('demo');
                    return false;
                });
            });

            layui.use('upload', function () {
                var $ = layui.jquery,
                    upload = layui.upload;
                //多图片上传
                upload.render({
                    elem: '#moreimg',
                    url: "http://39.101.180.27:8089/api/UpLoad/DynamicImgsUpLoad" //改成您自己的上传接口
                        ,
                    multiple: true,
                    number: 6,
                    auto: true
                        // ,bindAction:'#sure'
                        ,
                    before: function (obj) {
                        //预读本地文件示例，不支持ie8
                        obj.preview(function (index, file, result) {
                            // console.log(result);
                            $('#demo2').append('<img src="' + result + '" alt="' + file.name + '" class="layui-upload-img"' + ' style="width: 80px; height: 80px; margin: 0 10px 10px 0;" ' + '>')
                        });
                    },
                    done: function (res) {
                        that.imgup += res
                        // console.log(res);
                    }
                });
            });
            this.adddivshow = !this.adddivshow
        },
        // 发布动态功能按钮
        adddtclick() {
            if (this.teaup == "") {
                return;
            }
            // console.log(this.teaup);
            var data = {
                userId: this.userid,
                title: '',
                content: this.teaup,
                songID: this.songid,
                imgs: this.imgup,
            };
            axios.post(
                    "http://39.101.180.27:8089/" + "api/Dynamic/AddDynamic",
                    data
                ).then(res => {
                    // console.log(res.data);
                    if (res.data == "发布成功") {
                        layer.msg("发布成功")
                        this.adddivshow = false
                        this.teaup = ""
                        // window.location.reload()
                        this.imgitems = [];
                        this.loadgetdtitem()
                        $("#formlayui")[0].reset();
                        $('#demo2').empty()
                        this.imgup = ''
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        // 关闭发布动态的div
        escclick() {
            this.adddivshow = !this.adddivshow
            this.teaup = "";
            this.songTitle = "";
            this.searchValue = "";
            this.songlist = [];
            this.musicdivisshow = false;
        },
        musicdivisshowfun() {
            if (this.musicdivisshow) this.musicdivisshow = false
            else this.musicdivisshow = true;
        },
        //模糊查询被评价人
        async fetchData(val) {
            //写上你的搜索方法体
            // console.log(this.searchValue);
            this.searchsongbyname()
        },
        // 根据输入框内容查询
        searchsongbyname() {
            request({
                url: '/api/Search/SelSongListByName',
                params: {
                    name: this.searchValue,
                    top: 5,
                    page: 0,
                }
            }).then(res => {
                // console.log(res);
                if (res != '') {
                    this.songlist = []
                    res.forEach(el => {
                        this.songlist.push(el)
                    })
                } else {
                    layer.msg('曲库暂时该信息，请重新搜索');
                    this.songlist = []
                }
            }).catch(err => {
                console.log('错了')
            })
        },
        // 获取歌曲id
        getsong(id) {
            this.songTitle = event.target.innerText
            // console.log(event.target.innerText)
            // console.log(id);
            this.songid = id
            this.musicdivisshow = false;
        },
        // 点赞加一
        good(dtid, index) {
            request({
                url: 'api/Dynamic/AddGoodCount',
                params: {
                    id: dtid
                }
            }).then(res => {
                layer.msg("已点赞");
                // 暂时让他加一
                this.frienditems[index].Dynamic.D_GoodCount += 1

            }).catch(err => {
                layer.msg("失败了哦，请稍后再试~");
            })
        },
        // 删除动态
        deldt(dtid) {
            request({
                url: 'api/Dynamic/RmDynamic',
                params: {
                    id: dtid
                }
            }).then(res => {
                layer.msg("删除成功");
                this.imgitems = []
                this.loadgetdtitem()
            }).catch(err => {
                layer.msg("失败了哦，请稍后再试~");
            })
        },
        // 展开评论区域
        plclick(index, dtid) {
            // 展开评论区域
            this.isshow = index
            // 获取评论数据
            request({
                url: '/api/Dynamic/GetCommByDynID/',
                params: {
                    id: dtid,
                }
            }).then(res => {
                // console.log(res);
                this.dtcomment = res
            }).catch(err => {
                console.log(err);
            })
        },
        // 发表评论主评论
        addcomment(index, dtid) {
            if (this.userid == null) {
                layer.msg("请登录~");
                return;
            }
            var data = {
                did: dtid,
                pid: '',
                content: this.content,
                from: this.userid,
                to: '',
            };
            axios
                .post(
                    "http://39.101.180.27:8089/" + "api/Dynamic/AddDynamicComment",
                    data
                )
                .then(res => {
                    console.log(res);
                    console.log(res.data);
                    layer.msg("发表评论成功~");
                    this.content = "";
                    // 获取评论信息
                    this.plclick(index, dtid)
                })
                .catch(function (error) {
                    console.log(error);
                });
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
        // 查看歌曲
        targetsong(id) {
            this.$router.push({
                path: "./songinfo",
                query: {
                    songid: id
                }
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
            this.huifuisshow2 = !this.huifuisshow2;
            this.i2 = index;
        },
        // 回复评论按钮
        btnhuifu(index, dtid, pid, toid, fromid) {
            if (this.userid == null) {
                layer.msg("请登录~");
                return;
            }
            var data = {
                did: dtid,
                pid: pid,
                from: fromid,
                content: this.huifucontent,
                to: toid
            };
            axios
                .post(
                    "http://39.101.180.27:8089/" + "api/Dynamic/AddDynamicComment",
                    data
                )
                .then(res => {
                    // console.log(res.data);
                    layer.msg("评论成功~");
                    this.huifuisshow = false;
                    this.huifuisshow2 = false;
                    this.huifucontent = "";
                    // 获取评论信息
                    this.plclick(index, dtid)
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    },
    computed: {
        follow() {
            if (this.userinfoitem.U_Follow == "" || this.userinfoitem.U_Follow == null) {
                return 0;
            } else return this.userinfoitem.U_Follow.split(",").length - 1;
        },
        fanslength() {
            if (this.userinfoitem.U_Fans == "" || this.userinfoitem.U_Fans == null) {
                return 0;
            } else return this.userinfoitem.U_Fans.split(",").length - 1;
        },
        descNum() {
            if (this.content.length >= 140) {
                this.content = this.content.substr(0, 140);
                return 0;
            } else return this.content.length == 140 ? 140 : 140 - this.content.length;
        },
        descNum2() {
            if (this.huifucontent.length >= 140) {
                this.huifucontent = this.huifucontent.substr(0, 140);
                return 0;
            } else return this.huifucontent.length == 140 ?
                140 :
                140 - this.huifucontent.length;
        },
    },
    watch: {
        //用来监听页面变量的改变
        //监听搜索，300ms执行一次fetchData方法（去搜索）
        // searchValue() {
        //     delay(() => {
        //         this.fetchData();
        //     }, 700);
        // },
    },
    mounted() {
        //拖拽
        onDrag($(".adddivson").get(0));
    }
}
</script>

<style scoped>
@import url("./friend.css");

/* 评论区域 */
.app-comment {
    margin-top: 40px;
    /* padding: 0 50px; */
}

.app-comment .app-title {
    font-size: 20px;
    margin-left: 12px;
}

.comm-number {
    font-size: 12px;
    margin-left: 10px;
    color: rgba(45, 52, 54, 0.8);
}

/* 分割线 */
.hr-red {
    background-color: rgb(194, 12, 12);
    height: 3px;
}

/* 对话框的样式 */
.Dialog {
    display: flex;
}

/* 圆形头像 */
.avatar {
    width: 54px;
    max-width: 54px;
    height: 54px;
    border-radius: 50%;
    object-fit: cover;
    flex: 1;
    margin-top: 10px;
}

.Dialog .content {
    /* margin-left: 10px; */
    flex: 2;
}

.Dialog .talk {
    width: 100%;
    height: 80px;
    display: inline-block;
    border: 1px solid #ddd;
    border-radius: 3px;
    font-size: 14px;
    background-color: #fff;
    position: relative;
}

.Dialog .textarea {
    width: 99%;
    height: 75px;
    border: 0px;
    padding: 5px 0 0 5px;
    resize: none;
}

.commSub .wordCount {
    color: rgba(45, 52, 54, 0.8);
    margin-right: 10px;
}

/* 清除浮动 */
.commSub:after {
    content: "";
    display: block;
    clear: both;
}

.splendid {
    padding-top: 40px;
}

.splendid .comment-title {
    font-size: 12px;
    font-weight: bold;
    margin-bottom: -6px;
    margin-left: 12px;
}

/* 评论详细列表 */
.pl-info-all {
    /* background-color: #F5F5F5; */
    padding: 0 8px;
    /* border: 1px solid #c8c8c8; */
    position: relative;
}

.pl-info-all ul li {
    display: flex;
    font-family: Arial, Helvetica, sans-serif;
    /* line-height: 40px; */
    font-size: 13px;
    /* margin-bottom: 5px; */
    padding: 15px 0;
    border-bottom: 1px dashed #c8c8c8;
}

.pl-u-info ul li {
    border: none;
    line-height: 30px;
}

.pl-info-all .pl-tx {
    /* border: 1px solid #d5d5d5; */
    /* padding: 3px; */
    display: block;
    width: 45px;
    height: 45px;
    /* border-radius: 20px; */
}

.pl-u-info {
    width: 100%;
    padding-right: 20px;
}

.pl-u-info img {
    display: inline-block;
    width: 30px;
    height: 30px;
    border-radius: 10px;
}

.pl-info-all ul li a {
    color: #01AAED;
    margin-left: 5px;
}

.pl-info-all .pl-u-info {
    margin: 5px 0 0 5px;
}

#app a:hover {
    text-decoration: underline;
}

/* 评论的样式 */
.talk>p:nth-child(2) {
    text-align: right;
    padding: 2px 4px 0 0;
}

.talk-inner {
    width: 97%;
}

.pl-u-info:hover .msg {
    /* display: inline; */
    opacity: 1;
    transition: all 0.5s;
}

.msg {
    z-index: 2;
    /* display: none; */
    text-align: center;
    transition: all 0.5s;
    cursor: pointer;
    color: #1E9FFF;
    position: absolute;
    opacity: 0;
    right: 50px;
    transition: all 0.5s;
}

.msg:hover {
    text-decoration: underline;
}

.msg2 {
    right: 20px;
    top: 15px;
}

.huifu {
    width: 99%;
    min-height: 50px;
    height: 50px;
    margin: 5px 0 0 5px;
    resize: none;
}

.reply {
    padding-left: 20px;
    background: #e6e6e68a;
    margin-top: 10px;
}

.reply-inner {
    padding: 10px;
    padding-right: 50px;
    position: relative;
}

/* 这里是发布动态 */
/* 这里是发布动态 */
/* 这里是发布动态 */
/* 这里是发布动态 */
/* 这里是发布动态 */
#adddiv {
    position: fixed;
    z-index: 10;
    width: 100%;
    height: 100%;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}

.adddivson {
    width: 700px;
    /* height: 370px; */
    min-height: 465px;
    /* height: 470px; */
    background-color: #fff;
    position: absolute;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -75%);
    box-shadow: 0 0 10px -1px #393D49;
    border-radius: 5px;
}

.l-top {
    height: 40px;
    background-color: #393D49;
    border-top-left-radius: 5px;
    border-top-right-radius: 5px;
    margin-bottom: 35px;
    cursor: move;
}

.l-title {
    font-size: 1em;
    color: #fff;
    position: relative;
    left: 20px;
    line-height: 40px;
}

.close {
    font-size: 1.5em;
    position: absolute;
    right: 10px;
    line-height: 40px;
    /* color: #fff; */
    -webkit-text-stroke: 3px #393D49;
    color: #fff;
    cursor: pointer;
}

#flow {
    width: 88%;
    margin: 0 auto;
    min-height: 350px;
    max-height: 350px;
    overflow: scroll;
}

#tea {
    /* margin:0 50px; */
    /* margin-top: 10px; */
    /* border: 1px solid black; */
}

#tea span {
    position: relative;
    top: -95px;
}

#tea textarea {
    width: 90%;
    height: 105px;
    margin: 5px 0 0 5px;
    resize: none;
}

#imgmorediv {
    /* margin-left: 60px; */
    margin-top: 20px;
}

.fa-icon {
    font-size: 1.3em;
    cursor: pointer;
}

.divbtn {
    text-align: center;
}

/*选择歌曲歌曲 */
/*选择歌曲歌曲 */

.musicdiv {
    /* border: 1px solid #bf2500; */
}

.musicdiv ul {
    /* margin-top: 20px; */
    /* border: 1px solid #bf2500; */
    width: 100%;
    background-color: #fff;
    color: #666;
}

.musicdiv ul li:hover {
    background-color: #F2F2F2;
    cursor: pointer;
}

.musicdiv ul li {
    padding: 0 10px;
    margin-top: 5px;
    /* border: 1px solid #00F7DE; */
    line-height: 25px;
    height: 25px;
}

/* 滚动条样式 */
#flow::-webkit-scrollbar {
    width: 5px;
    /* height: 10px; */
    border-radius: 10px;
    display: none;
}
</style>
