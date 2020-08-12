<template>
  <div id="app-cendiv">
    <!-- left -->
    <div class="app-left">
      <div class="left-center">
        <div class="right-top">
          <!-- 歌单头像 -->
          <div class="imgdiv">
            <img v-if="menuinfo.M_Img" :src="imgurl+menuinfo.M_Img" />
          </div>
          <!-- 歌单div -->
          <div class="menudiv">
            <p>
              歌单:
              <span>{{menuinfo.M_Name}}</span>
            </p>
            <div class="divinfo">
              <img v-if="menuinfo.U_Img" :src="imgurl+menuinfo.U_Img" />
              <div class="divinfo-name">
                <a href="javascript:;">{{menuinfo.U_Name}}</a>
                <span>
                  <u>{{this.menuinfo.M_CreatTime==null?'':this.menuinfo.M_CreatTime.split('T')[0] }}</u> 创建
                </span>
              </div>
            </div>
            <div class="btndiv layui-icon">
              <button type="button" class="layui-btn layui-btn-normal" @click="play()">
                <i class="fa fa-play-circle-o" aria-hidden="true"></i>播放
              </button>
              <button type="button" class="layui-btn layui-btn-primary" @click="collectmenu()">
                <i class="fa fa-heart" aria-hidden="true"></i>
                {{iscollect}}
              </button>
              <a href="#pl">
                <button type="button" class="layui-btn layui-btn-primary">
                  <i class="fa fa-commenting-o" aria-hidden="true"></i>评论
                </button>
              </a>
            </div>
            <br />
            <div>
              标签：
              <button
                type="button"
                class="layui-btn layui-btn-normal layui-btn-radius layui-btn-sm"
                @click="targetmenuoinfolist(menuinfo.M_Type)"
              >{{this.menuinfo.T_TypeName}}</button>
            </div>
            <br />
            <div class="introduceInfo">
              <p><b>歌单介绍:</b></p>
              <p>{{menuinfo.M_Info}}</p>
            </div>
          </div>
        </div>
        <!-- 歌曲列表 -->
        <div class="right-paihang">
          <div class="ph-div-h">
            <p class="ph-h">
              歌曲列表
              <span class="ph-span">{{menusongitems.length}}首歌</span>
              <!-- <span class="ph-span-count">播放次数</span> -->
            </p>
          </div>
          <hr class="layui-bg-red" />
          <div class="phdiv">
            <table class="layui-table" lay-size="sm">
              <colgroup>
                <col width="10" />
                <col width="200" />
                <col />
              </colgroup>
              <thead>
                <tr>
                  <th></th>
                  <th>歌曲标题</th>
                  <th>播放次数</th>
                  <th>歌手</th>
                  <th>专辑</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td colspan="6" style="text-align:center" v-show="menusongitems==''">暂无歌曲</td>
                </tr>
                <!-- 收藏的歌曲只显示20条 -->
                <tr v-for="(v,i) in menusongitems" v-if="i<20">
                  <td>{{i+1}}</td>
                  <td @click="songinfoclick(v.sID)">
                    <a href="#">{{v.sName}}</a>
                  </td>
                  <td>{{v.sPlayCount}}</td>
                  <td>
                    <a href="#">{{v.gName}}</a>
                  </td>
                  <td>
                    <a href="#">{{v.sAlbumID==0?'暂无':''}}</a>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>

        <!-- 评论 -->
        <div class="app-comment" id="pl">
          <p>
            <span class="app-title">评论</span>
            <!-- <span class="comm-number">共 <b>100</b> 条</span> -->
          </p>
          <hr class="hr-red" />
          <div class="Dialog">
            <img v-show="userid" :src="imgurl+uimg" class="avatar" />
            <img v-show="!userid" v-if="menuinfo.M_Img" :src="imgurl+menuinfo.M_Img" class="avatar" />
            <div class="content">
              <p class="talk">
                <textarea
                  name
                  required
                  lay-verify="required"
                  placeholder="请输入评论"
                  class="textarea"
                  v-model="content"
                ></textarea>
              </p>
            </div>
          </div>
          <div class="commSub" style="float: right;">
            <span class="wordCount">{{descNum}}</span>
            <button
              type="button"
              class="layui-btn layui-btn-sm layui-btn-normal"
              @click="addcomment()"
            >评论</button>
          </div>
          <div class="splendid">
            <p class="comment-title">精彩评论</p>
            <hr />
            <!-- =============评论模块============ -->
            <div class="pl-info-all">
              <ul>
                <!-- ===============主评论板块 ul结构生成================ -->
                <li v-for="(items,index) in commentmenu" :key="index">
                  <img :src="imgurl+items.From_UserInfo.U_Img" class="pl-tx" alt />
                  <div class="pl-u-info">
                    <a
                      href="javascript:;"
                      @click="openfriend(items.SC_From_User)"
                    >{{items.From_UserInfo.U_Name}}</a> ：
                    <span class="contentsty">
                      {{items.SC_Content}}
                      <span class="msg" @click="huifu(index)">回复</span>
                    </span>
                    <!-- 回复框div -->
                    <div v-show="huifuisshow&&index==i" class="talk">
                      <p>
                        <textarea
                          v-model="huifucontent"
                          required
                          lay-verify="required"
                          placeholder="请输入回复内容"
                          class="layui-textarea huifu"
                        ></textarea>
                      </p>
                      <p>
                        <span>{{descNum2}}</span>
                        <button
                          type="button"
                          class="layui-btn layui-btn-sm layui-btn-normal"
                          @click="btnhuifu(items.SC_Id,items.SC_From_User,userid)"
                        >回复</button>
                      </p>
                    </div>
                    <!-- ==============主评论的回复板块  由原先的ul 改为div============== -->
                    <div class="reply">
                      <div
                        v-for="(son ,indexs) in items.ChildrenComm"
                        :key="indexs"
                        class="reply-inner"
                      >
                        <!-- =====回复信息显示的位置===== -->
                        <a href="javascript:;" @click="openfriend(son.From_UserInfo.U_Id)">
                          <img :src="imgurl+son.From_UserInfo.U_Img" />
                          &nbsp;{{son.From_UserInfo.U_Name}}
                        </a> &nbsp; 回复&nbsp;
                        <a
                          href="javascript:;"
                          @click="openfriend(son.To_UserInfo.U_Id)"
                        >
                          <img :src="imgurl+son.To_UserInfo.U_Img" />
                          &nbsp;{{son.To_UserInfo.U_Name}}
                        </a>
                        ：
                        <span class="contentsty">
                          {{son.SM_Content}}
                          <span class="msg msg2" @click="huifu2(indexs,index)">回复</span>
                        </span>
                        <!-- =============回复框div========== -->
                        <div v-show="huifuisshow2&&indexs==i2&&songhuifuclass==index" class="talk talk-inner">
                          <p>
                            <textarea
                              v-model="huifucontent"
                              required
                              lay-verify="required"
                              placeholder="请输入回复内容"
                              class="layui-textarea huifu huifu2"
                            ></textarea>
                          </p>
                          <p>
                            <span>{{descNum2}}</span>
                            <button
                              type="button"
                              class="layui-btn layui-btn-sm layui-btn-normal"
                              @click="btnhuifu(son.SM_Pid,son.From_UserInfo.U_Id,userid)"
                            >回复</button>
                          </p>
                        </div>
                      </div>
                    </div>
                    <span
                      style="color: #c2c2c2;margin-left: 20px;position: relative;top: 10px;line-height: 30px;"
                    >{{items.SC_UpTime}}</span>
                  </div>
                </li>
              </ul>
            </div>
            <hr class="layui-bg-gray" />
          </div>
        </div>
      </div>
    </div>
    <!-- right -->
    <div class="app-right">
      <!-- 用户 -->
      <div class="user-friend">
        <h6 class="xgtj">相关推荐</h6>
        <hr class="hr-red" />
        <div class="userul">
          <ul>
            <li
              v-for="(items,index) in showmenulist"
              :key="index"
              class="showli"
              @click="menutarget(items.M_Id)"
            >
              <div class="lidiv2">
                <img :src="imgurl+items.M_Img" />
                <div class="lidiv">
                  <p>{{items.M_Name}}</p>
                  <p>当前热度：{{items.M_PlayCount}}</p>
                </div>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { request } from "../../network/request.js";
import axios from "axios";
export default {
  name: "SongMenuInfo.vue",
  data() {
    return {
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
      menuid: this.$route.query.menuid,
      menuinfo: "",
      menusongitems: "",
      // 推荐歌单
      showmenulist: "",
      // 用户头像
      uimg: sessionStorage.U_Img,
      // 评论数据
      commentmenu: "",
      // 评论框输入的内容
      content: "",
      // 回复的内容
      huifucontent: "",
      // 监听发表评论的字数
      fontcount: 0,
      // 显示收藏和取消收藏
      iscollect: "收藏",
			//控制子回复框的显示与隐藏
			songhuifuclass:0,
    };
  },

  methods: {
    // 根据歌单id获取数据
    loadgetmenuinfo(mid) {
      request({
        url: "/api/SongMenu/SelSongMenuByID/",
        params: {
          id: mid
        }
      })
        .then(res => {
          // console.log(res)
          this.menuinfo = res.songMenuInfo;
          this.menusongitems = res.songInfo;
          // console.log(this.menuinfo);
          // console.log(this.menusongitems);
          // console.log("根据歌单id查数据成功")
        })
        .catch(err => {
          console.log("错了");
        });
    },
    //加载时获取歌单评论信息
    loadgetcomment(mid) {
      request({
        url: "/api/SongMenu/GetSongMenuComm",
        params: {
          id: mid
        }
      })
        .then(res => {
          // console.log(res);
          this.commentmenu = res;
        })
        .catch(err => {
          console.log(err);
        });
    },
    //加载时显示右侧推荐的歌单
    loadgetshowmenu() {
      request({
        url: "/api/Home/Top8",
        params: {
          id: 2
        }
      })
        .then(res => {
          this.showmenulist = res;
          // console.log(this.showmenulist)
          // console.log("推荐歌单数据完成");
        })
        .catch(err => {
          console.log(err);
        });
    },
    //加载时判断用户是否收藏该歌单
    loadiscollectmenu() {
      request({
        url: "/api/SongMenu/IsCollect",
        params: {
          uid: sessionStorage.U_Id,
          mid: this.menuid
        }
      })
        .then(res => {
          console.log(res);
          if (res == "未收藏") {
            this.iscollect = "收藏";
          } else if (res == "未收藏") {
            this.iscollect = "取消收藏";
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    // 播放歌单数据
    play() {
      // 1判断歌单是否就数据
      if (this.menusongitems == "") {
        layer.msg("该歌单当前无播放歌曲~");
        return;
      }
      // 2先将歌单数据清空
      this.$store.commit("clearsongmenu");
      // 3，将当前歌单数据清洗后导入到歌单
      for (let i = 0; i < this.menusongitems.length; i++) {
        // console.log(this.itemsmymusic[i])
        var songplay = {
          name: "",
          singer: "",
          cover: "",
          url: "",
          id: ""
        };
        songplay = {};
        songplay.name = this.menusongitems[i].sName;
        songplay.singer = this.menusongitems[i].gName;
        songplay.cover = this.menusongitems[i].sCover;
        songplay.url = this.menusongitems[i].sUrl;
        songplay.id = this.menusongitems[i].sID;
        // 循环将歌曲导入歌单
        this.$store.commit("playmeunsong", songplay);
      }
      //4.播放列表数据替换后 开始播放
      this.$store.state.play();
      // 歌单歌曲播放次数加一
      this.playadd1();
    },
    // 歌单播放次数加一
    playadd1() {
      request({
        url: "/api/SongMenu/AddPlayCount",
        params: {
          id: this.menuid
        }
      })
        .then(res => {
          // console.log("歌单歌曲次数+1")
          // layer.msg(res);
        })
        .catch(err => {
          console.log(err);
        });
    },
    // 添加收藏
    collectmenu() {
      if (sessionStorage.U_Id == null) {
        layer.msg("请登录");
        return;
      }
      if (this.iscollect == "收藏") {
        // alert("1")
        this.getcollectmenu();
      } else if (this.iscollect == "取消收藏") {
        // alert("2")
        this.getesccollectmenu();
      }
    },
    // 添加收藏的get方法
    getcollectmenu() {
      request({
        url: "/api/SongMenu/AddCollectSongMenu",
        params: {
          uid: sessionStorage.U_Id,
          mid: this.menuid
        }
      })
        .then(res => {
          // console.log("歌单收藏成功");
          layer.msg("歌单收藏成功");
          this.iscollect = "取消收藏";
        })
        .catch(err => {
          layer.msg("歌单收藏失败~");
        });
    },
    // 取消收藏
    getesccollectmenu() {
      request({
        url: "/api/SongMenu/RmCollectSongMenu",
        params: {
          uid: sessionStorage.U_Id,
          mid: this.menuid
        }
      })
        .then(res => {
          // console.log(res)
          layer.msg("已取消收藏");
          this.iscollect = "收藏";
        })
        .catch(err => {
          console.log(err);
          layer.msg("取消收藏失败~");
        });
    },
    // 歌曲详情页的跳转
    songinfoclick(sid) {
      this.$router.push({
        path: "./songinfo",
        query: {
          songid: sid
        }
      });
    },
    // 标签跳转到歌单列表
    targetmenuoinfolist(typeid) {
      this.$router.push({
        path: "./songmenulist",
        query: {
          typeid: typeid
        }
      });
    },
    // 推荐歌单的跳转
    menutarget(mid) {
      // 更新歌单基本信息
      this.loadgetmenuinfo(mid);
      // 更新歌单评论信息
      this.loadgetcomment(mid);
    },
    // 发表评论
    addcomment() {
      if (this.userid == null) {
        layer.msg("请登录~");
        return;
      }
      var data = {
        mid: this.menuid,
        pid: "",
        from: sessionStorage.U_Id,
        content: this.content,
        to: ""
      };
      axios
        .post(
          "http://39.101.180.27:8089/" + "api/SongMenu/AddSongMenuComment",
          data
        )
        .then(res => {
          // console.log(res.data);
          layer.msg("发表成功~");
          this.content = "";
          // 更新歌单评论信息
          this.loadgetcomment(this.menuid);
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    // 回复
    huifu(index) {
      // 将另外一个回复框隐藏
      this.huifuisshow2 = false;
      this.huifuisshow = !this.huifuisshow;
      this.i = index;
    },
    // 回复
    huifu2(index,index2) {
				this.songhuifuclass=index2
      // 将另外一个回复框隐藏
      this.huifuisshow = false;
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
        mid: this.menuid,
        pid: pid,
        from: fromid,
        content: this.huifucontent,
        to: toid
      };
      axios
        .post(
          "http://39.101.180.27:8089/" + "api/SongMenu/AddSongMenuComment",
          data
        )
        .then(res => {
          // console.log(res.data);
          layer.msg("评论成功~");
          this.huifuisshow = false;
          this.huifuisshow2 = false;
          this.huifucontent = "";
          // 更新歌单评论信息
          this.loadgetcomment(this.menuid);
        })
        .catch(function(error) {
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
    }
  },
  created() {
    // 根据歌单id获取数据,主要功能
    this.loadgetmenuinfo(this.menuid);
    // 判断歌单是否已收藏
    if (this.userid != null) {
      this.loadiscollectmenu();
    }
    // 推荐歌单信息
    this.loadgetshowmenu();
    // 歌单评论信息
    this.loadgetcomment(this.menuid);
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
      return this.huifucontent.length == 140
        ? 140
        : 140 - this.huifucontent.length;
    }
  }
};
</script>

<style scoped="">
@import url("./songmenuinfo.css");
</style>
