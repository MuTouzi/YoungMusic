<template>
  <div id="app-cendiv">
    <!-- 新建歌单 -->
    <div id="xj" v-if="isshow">
      <div class="xjBox">
        <div v-if="ishowxj">
          <div class="top">
            <span class="l-title">新建歌单</span>
            <i class="fa fa-times close" aria-hidden="true" @click="escclick()"></i>
          </div>
          <div class="mid">
            <input
              type="text"
              name="title"
              required
              lay-verify="required"
              placeholder="请输入新建的歌单名"
              autocomplete="off"
              class="layui-input textmenu"
              v-model="addmenuname"
            />
            <div class="divbtn">
              <button type="button" class="layui-btn layui-btn-normal" @click="addclick()">添加</button>
              <button type="button" class="layui-btn layui-btn-primary" @click="escclick()">取消</button>
            </div>
          </div>
        </div>
        <div v-if="ishowdel">
          <div class="top">
            <span class="l-title">提示</span>
            <i class="fa fa-times close" aria-hidden="true" @click="escclick()"></i>
          </div>
          <div class="mid">
            <div>
              <p style="font-size: 1.2em;
                color: #666;
                margin: 10px 0;
                text-align: center;">是否删除？</p>
            </div>
            <div class="divbtn">
              <button type="button" class="layui-btn layui-btn-normal" @click="delbtnclick()">确定</button>
              <button type="button" class="layui-btn layui-btn-primary" @click="escclick()">取消</button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 编辑歌单 -->
    <!-- 歌单修改 -->
		
    <div id="menuupddiv" v-show="updmenuisshow">
      <div class="updmenudiv">
        <div>
          <div class="l-top">
            <span class="l-title">编辑歌单</span>
            <i class="fa fa-times close" aria-hidden="true" @click="escclick()"></i>
          </div>
          <div class="mid">
            <!-- 歌单名: -->
            <input
              type="text"
              v-model="updmenuitems.name"
              name="title"
              required
              lay-verify="required"
              placeholder="请输入歌单名"
              autocomplete="off"
              class="layui-input"
            />
            <div class="menuimg" id="menuupimg">
              <img :src="imgurl+updimg" class="imgs" id="img" />
              <span class="msgss">编辑封面</span>
            </div>
            <!-- 歌单类型 -->
            <select class="seltype" name="types" @change="selchange($event)" v-model="seltype" lay-verify="">
              <option :value="item.id" v-for="(item,index) in typelist" :key="index">{{item.name}}</option>
            </select>   
            <br />
            <!-- 介绍 -->
            <textarea name="" v-model="updmenuitems.info" required lay-verify="required" 
            placeholder="请输入歌单介绍" class="layui-textarea info"></textarea>
            <div class="divbtn">
              <button type="button" class="layui-btn layui-btn-normal" @click="updmenuclick()">确定</button>
              <button type="button" class="layui-btn  layui-btn-primary" @click="escclick()">取消</button>
            </div>
          </div>
        </div>
      </div>
    </div>
   
		
		
		<!-- 收藏歌单 -->
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
            <li
              class="hover"
              v-for="(item, index ) in itemcreatemenu"
              :key="index"
              @click="addcollect(item.M_Id)"
            >
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

    <!-- 左边 -->
    <div class="app-left">
      <div class="layui-collapse input-div">
        <div class="layui-colla-item">
          <p class="layui-colla-title">
            创建的歌单(
            <span>{{itemcreatemenu.length}}</span>)
            <button
              type="button"
              class="layui-btn layui-btn-xs layui-btn-danger btn-xj"
              @click.stop="xjclick()"
            >新建</button>
          </p>
          <div class="layui-colla-content layui-show">
            <ul>
              <li
                :class="[{'xz':index==xz}]"
                v-for="(item,index ) in itemcreatemenu"
                :key="index"
                @click="CreatemenuClick(index,item.M_Id)"
              >
                <a href="javascript:;" :title="item.M_Name">
                  <img v-if="item.M_Img" :src="imgurl+item.M_Img" />
                  <div class="lidiv">
                    <p>{{item.M_Name.length < 11 ? item.M_Name : item.M_Name.substr(0, 10) + "..." }}</p>
                    <p class="lidiv-p">
                      <span class="lidiv-name">{{item.M_SongId.split(',').length-1}}首</span>
                      <span class="iconspan">
                        <a
                          title="修改"
                          class="fa fa-pencil-square-o"
                          @click.stop="updclick(item)"
                          v-show="item.M_Name!='我喜欢的音乐'"
                        ></a>
                        &nbsp;
                        <a
                          title="删除"
                          class="fa fa-trash-o"
                          @click.stop="delclick(item.M_Id,1)"
                          v-show="item.M_Name!='我喜欢的音乐'"
                        ></a>
                      </span>
                    </p>
                  </div>
                </a>
              </li>
            </ul>
          </div>
        </div>
        <div class="layui-colla-item" v-show="itemCollectionmenu.length>0">
          <p class="layui-colla-title">
            收藏的歌单(
            <span>{{itemCollectionmenu.length}}</span>)
          </p>
          <div class="layui-colla-content layui-show">
            <ul>
              <li
                :class="[{'xz':index==xz}]"
                v-for="(item, index ) in itemCollectionmenu"
                :key="index"
                @click="CollectionmenuClick(index,item.M_Id)"
              >
                <a href="javascript:;" :title="item.M_Name">
                  <img v-if="item.M_Img" :src="imgurl+item.M_Img" />
                  <div class="lidiv">
                    <p>{{item.M_Name.length < 11 ? item.M_Name : item.M_Name.substr(0, 10) + "..." }}</p>
                    <p class="lidiv-p">
                      <span class="lidiv-name">{{item.M_SongId.split(',').length-1}}首</span>
                      <span class="iconspan">
                        <!-- <a class="fa fa-pencil-square-o"></a> -->
                        <!-- &nbsp; -->
                        <a title="删除" class="fa fa-trash-o" @click.stop="delclick(item.M_Id,2)"></a>
                      </span>
                    </p>
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
          <img v-if="Menuimg" :src="imgurl+Menuimg" />
        </div>
        <!-- 歌单div -->
        <div class="menudiv">
          <p>
            歌单:
            <span>{{Menutitle}}</span>
          </p>
          <div class="divinfo">
            <img v-if="MenuUserimg" :src="imgurl+MenuUserimg" />
            <div class="divinfo-name">
              <a href="javascript:;">{{MenuName}}</a>
              <span>
                <u>{{ MenuTime==''?'':MenuTime.split('T')[0]}}</u> 创建
              </span>
            </div>
          </div>
          <div class="btndiv layui-icon">
            <button type="button" class="layui-btn layui-btn-normal" @click="play()">
              <i class="fa fa-play-circle-o" aria-hidden="true"></i>播放
            </button>
            <!-- <button type="button" class="layui-btn layui-btn-primary"><i class="fa fa-heart" aria-hidden="true"></i>收藏</button> -->
            <a href="#pl">
              <button type="button" class="layui-btn layui-btn-primary">
                <i class="fa fa-commenting-o" aria-hidden="true"></i>评论
              </button>
            </a>
          </div>
        </div>
      </div>
      <!-- 歌曲列表 -->
      <div class="right-paihang">
        <div class="ph-div-h">
          <p class="ph-h">
            歌曲列表
            <span class="ph-span">{{itemsmymusic.length}}首歌</span>
            <!-- <span class="ph-span-count">播放次数</span> -->
          </p>
        </div>
        <hr class="layui-bg-red" />
        <div class="phdiv">
          <table class="layui-table" lay-size="sm">
            <colgroup>
              <col width="20" />
              <col/>
              <col width="200" />
              <col width="100" />
              <col width="100" />
              <col width="200" />
            </colgroup>
            <thead>
              <tr>
                <th></th>
                <th>歌曲标题</th>
                <th>歌手</th>
                <th>专辑</th>
                <th>播放次数</th>
                <th>操作</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td colspan="6" style="text-align:center" v-show="itemsmymusic==''">当前歌单无歌曲</td>
              </tr>
              <!-- 收藏的歌曲只显示20条 -->
              <tr v-for="(v,i) in itemsmymusic" v-if="i<20">
                <td>{{i+1}}</td>
                <td @click="songinfoclick(v.sID)">
                  <a href="javascript:;">{{v.sName}}</a>
                </td>
                <td>
                  <a href="javascript:;">{{v.gName}}</a>
                </td>
                <td>
                  <a href="javascript:;">{{v.sAlbumID==0?'暂无':''}}</a>
                </td>
                <td class="hot-hover">
                  <div class="hidd">{{v.sPlayCount}}</div>
                </td>
                <td><div class="key">
                    <a title="添加到播放列表" @click="joinmenu(v)">
                      <i class="fa fa-plus-square" aria-hidden="true"></i>
                    </a>&nbsp;&nbsp;
                    <a title="收藏" @click="collectmenu(v.sID)">
                      <i class="fa fa-folder-open-o" aria-hidden="true"></i>
                    </a>&nbsp;&nbsp;
                    <a title="下载" :href="v.sUrl" download>
                      <i class="fa fa-download" aria-hidden="true"></i>
                    </a>&nbsp;&nbsp;
                    <a title="删除" @click="delsong(v.sID)" v-show="!istype">
                      <i class="fa fa-trash-o" aria-hidden="true"></i>
                    </a>
                  </div></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="app-comment" id="pl">
        <p>
          <span class="app-title">评论</span>
          <!-- <span class="comm-number">共 <b>{{count}}</b> 条</span> -->
        </p>
        <hr class="hr-red" />
        <div class="Dialog">
          <img :src="imgurl+userimg" class="avatar" />
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
</template>
<script>
import { tab } from "../../assets/js/tab.js";
import { request } from "../../network/request.js";
import { onDrag } from "../../assets/js/onDrag.js";
import axios from "axios";
export default {
  name: "MyMusic",
  data() {
    return {
      count: 0,
      imgurl: sessionStorage.img,
      userimg: sessionStorage.U_Img,
      isshow: false,
      ishowxj: true,
      ishowdel: false,
      collectionisshow: false,
      tdshow: true,
      xz: 0,
      userid: sessionStorage.U_Id,
      // 编辑歌单div的显示
      updmenuisshow: false,
      seltype: 0,
      //存放要编辑歌单的数据
      updmenuitems: {
        mid: "",
        name: "",
        // 'img': '',
        type: "",
        info: ""
      },
      // 回复框的显示
      huifuisshow: false,
      huifuisshow2: false,
      huifuisshow3: false,
      // 控制一个回复框显示
      i: 0,
      i2: 0,
      //控制是否显示歌单列表
      ismenushow: false,
      // yi创建歌单数据
      itemcreatemenu: "",
      // yi收藏歌单数据
      itemCollectionmenu: "",
      // 标记为创建歌单还是收藏歌单
      istype: false,
      // 存放歌单id
      Menuid: "",
      Menuimg: "MenuDefault.png",
      MenuTime: "",
      MenuName: "",
      Menutitle: "",
      MenuUserimg: sessionStorage.U_Img,
      //一个歌单的歌曲数据
      itemsmymusic: "",
      // 添加歌单的歌单名
      addmenuname: "",
      // 要删除的歌单id
      delmenuid: "",
      // 收藏的歌曲id
      collectsongid: "",
      // 标记是取消收藏还是删除创建歌单
      flag: "",
      // 评论数据
      commentmenu: "",
      // 评论框输入的内容
      content: "",
			//控制子回复框的显示与隐藏
			songhuifuclass:0,
      // 回复的内容
      huifucontent: "",
      // 监听发表评论的字数
      fontcount: 0,
      addmunuidcount: 0,
      updimg: "",
      typelist: [
        {
          id: "0",
          name: "请选择一个类型"
        },
        {
          id: "1",
          name: "流行"
        },
        {
          id: "2",
          name: "华语"
        },
        {
          id: "3",
          name: "欧美"
        },
        {
          id: "4",
          name: "日语"
        },
        {
          id: "5",
          name: "粤语"
        },
        {
          id: "6",
          name: "民族"
        },
        {
          id: "7",
          name: "摇滚"
        },
        {
          id: "8",
          name: "民谣"
        },
        {
          id: "9",
          name: "说唱"
        },
        {
          id: "10",
          name: "轻音乐"
        }
      ]
    };
  },

  created() {
    // 加载时请求数据
    if (sessionStorage.U_Id != null) {
      this.loadreq();
    } else {
		this.$router.push('/index')
    }
  },
  methods: {
    // 获取歌曲评论信息
    loadgetcomment(mid) {
      request({
        url: "/api/SongMenu/GetSongMenuComm",
        params: {
          id: mid
        }
      })
        .then(res => {
          // console.log(res)
          this.commentmenu = res;
        })
        .catch(err => {
          console.log("错了");
        });
    },
    // 新建菜单的按钮
    xjclick() {
      this.isshow = true;
      this.ishowxj = true;
      this.ishowdel = false;
    },
    //关闭
    closeclick() {
      (this.isshow = false), (this.ishowxj = false);
    },
    // 添加歌单
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
          // console.log(res.date);
          this.addmunuidcount = res.data;
          // 判断是否收藏歌曲到歌单
          if (this.collectsongid != "") {
            this.addcollect(this.addmunuidcount);
          }
          // 页面关闭
          (this.isshow = false),
            // 新建歌单页面添加完就关闭
            (this.ishowxj = false);
          layer.msg("歌单创建成功~");
          this.loadreq();
        })
        .catch(function(error) {
          console.log(error);
          layer.msg("歌单创建失败~");
          (this.isshow = true),
            // 新建歌单页面添加完就关闭
            (this.ishowxj = true);
        });
    },
    // 关闭弹窗
    closeclick() {
      this.collectionisshow = false;
    },
    // 加入播放歌单歌单
    joinmenu(v) {
      var song = {
        name: "",
        singer: "",
        cover: "",
        url: "",
        id: ""
      };
      song.name = v.sName;
      song.singer = v.gName;
      song.cover = v.sCover;
      song.url = v.sUrl;
      song.id = v.sID;
      this.$store.commit("joinMenu", song);
      layer.msg("已加入播放列表~");
    },
    // 打开收藏歌单功能页面样式
    collectmenu(songid) {
      this.collectsongid = songid;
      this.collectionisshow = true;
    },
    // 新建歌单样式
    addmenu() {
      this.collectionisshow = false;
      this.isshow = true;
      this.ishowxj = true;
      this.ishowdel = false;
    },
    // 添加到创建歌单
    addcollect(mid) {
      request({
        url: "/api/SongInfo/AddCollecting",
        params: {
          uid: sessionStorage.U_Id,
          muneID: mid,
          songID: this.collectsongid
        }
      })
        .then(res => {
          // console.log(res)
          // console.log("收藏成功");
          layer.msg("歌单收藏成功~");
          this.collectionisshow = false;
          this.loadreq();
          // 歌曲收藏成功后，将中间变量清空，以防创建歌单时使用
          this.collectsongid = "";
        })
        .catch(err => {
          console.log("错了");
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

    // 歌单删除歌曲
    delsong(songid) {
      request({
        url: "/api/SongInfo/RMCollecting",
        params: {
          uid: sessionStorage.U_Id,
          muneID: this.Menuid,
          songID: songid
        }
      })
        .then(res => {
          console.log("歌曲删除成功");
          layer.msg("删除成功~");
          this.loadreq();
        })
        .catch(err => {
          console.log("错了");
        });
    },
    // 取消按钮
    escclick() {
      (this.isshow = false),
        (this.ishowxj = false),
        (this.updmenuisshow = false);
    },
    // select改变事件
    selchange(event) {
      // 如果选择了第一个：让选择的提示，则改成流行，以防数据库报错
      if (event.target.value == 0) {
        event.target.value = 1;
      }
      this.updmenuitems.type = event.target.value;
      // console.log(this.updmenuitems.type);
    },
    //编辑 的图标
    updclick(menu) {
      this.updmenuitems.name = menu.M_Name;
      this.updmenuitems.mid = menu.M_Id;
      // this.updmenuitems.img = menu.M_Img
      this.updimg = menu.M_Img;
      // console.log(menu.M_Img);
      // console.log(this.updimg);
      this.updmenuitems.type = menu.M_Type;
      this.updmenuitems.info = menu.M_Info;

      this.seltype = this.updmenuitems.type;
      this.updmenuimg(menu.M_Id);
      this.updmenuisshow = true;
    },
    //修改歌单图片
    updmenuimg(mid) {
      layui.use(["upload"], function() {
        // 图片上传
        layui.use("upload", function() {
          var that = this;
          var $ = layui.jquery,
            upload = layui.upload;
          //普通图片上传
          var uploadInst = upload.render({
            elem: "#menuupimg",
            url: "http://39.101.180.27:8089/api/UpLoad/SongMenuImgUpLoad", //改成您自己的上传接口
            data: {
              id: function() {
                return mid;
              }
            },
            before: function(obj) {
              //预读本地文件示例，不支持ie8
              obj.preview(function(index, file, result) {
                $("#img").attr("src", result); //图片链接（base64）
              });
            },
            done: function(res) {
              //如果上传失败
              if (res.code > 0) {
                return layer.msg("上传失败");
              }
              // console.log(res);
              //上传成功
              layer.msg("图片上传成功");
              // window.location.reload()
            },
            error: function() {
              //演示失败状态，并实现重传
              var demoText = $("#demoText");
              demoText.html(
                '<span style="color: #FF5722;">上传失败</span> <a class="layui-btn layui-btn-xs demo-reload">重试</a>'
              );
              demoText.find(".demo-reload").on("click", function() {
                uploadInst.upload();
              });
            }
          });
        });
      });
    },
    // 确认修改
    updmenuclick() {
      // console.log(this.updmenuitems);
      axios
        .post(
          "http://39.101.180.27:8089/" + "api/SongMenu/UpSongMenuInfo",
          this.updmenuitems
        )
        .then(res => {
          layer.msg(res.data);
		// window.location.href='http://localhost:8080/index#/mymusic'
		window.location.reload()
          // this.updmenuisshow = false;
          // this.loadreq();
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    //删除的图标
    delclick(mid, flag) {
      this.delmenuid = mid;
      this.flag = flag;
      this.isshow = true;
      this.ishowxj = false;
      this.ishowdel = true;
    },
    //确认删除
    delbtnclick() {
      var url = "";
      if (this.flag == 1) {
        url = "/api/SongMenu/RmCreateSongMenu";
      } else if (this.flag == 2) {
        url = "api/SongMenu/RmCollectSongMenu";
      }
      request({
        url: url,
        params: {
          uid: sessionStorage.U_Id,
          mid: this.delmenuid
        }
      })
        .then(res => {
          // console.log("歌单删除成功")
          layer.msg("歌单删除成功~");
          // 页面关闭
          (this.isshow = false),
            // 新建歌单页面添加完就关闭
            (this.ishowdel = false);
          this.loadreq();
        })
        .catch(err => {
          // console.log("删除失败");
          layer.msg("歌单删除失败~");
          // 新建歌单页面添加完就关闭
          (this.isshow = true), (this.ishowdel = true);
        });
    },
    // 加载时请求数据
    loadreq() {
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
          //打开页面默认打开第一个歌单
          this.CreatemenuClick(0, this.itemcreatemenu[0].M_Id);
          // 获取歌曲评论信息
          this.loadgetcomment(this.itemcreatemenu[0].M_Id);
          this.itemCollectionmenu = res.collectSongMenu;
          // console.log("歌单数据请求成功")
        })
        .catch(err => {
          console.log("错了");
        });
    },
    // 根据id获取歌单信息
    getmenu(mid) {
      request({
        url: "/api/SongMenu/SelSongMenuByID/",
        params: {
          id: mid
        }
      })
        .then(res => {
          // console.log(res)
          this.itemsmymusic = res.songInfo;
          // console.log(this.itemsmymusic)
          this.MenuName = res.songMenuInfo.U_Name;
          this.MenuUserimg = res.songMenuInfo.U_Img;
          // console.log(res.songMenuInfo.U_Img)
          // console.log(this.itemsmymusic)
          // this.itemcreatemenu=res.creatSongMenu
          // this.itemCollectionmenu=res.collectSongMenu
          // console.log("点击歌单获取数据成功")
        })
        .catch(err => {
          console.log("错了");
        });
    },
    // 已创建的歌单的跳转
    CreatemenuClick(index, mid) {
      // 标记为创建歌单
      this.istype = false;
      // 歌单头部信息获取替换，因为接口不同只能这样
      this.Menutitle = this.itemcreatemenu[index].M_Name;
      this.MenuTime = this.itemcreatemenu[index].M_CreatTime;
      this.Menuimg = this.itemcreatemenu[index].M_Img;
      this.Menuid = mid;
      this.xz = index;
      // 根据歌单id获取信息
      this.getmenu(mid);
      // 根据歌单id获取歌单评论
      this.loadgetcomment(mid);
    },
    // 收藏歌单的单机显示数据
    CollectionmenuClick(index, mid) {
      // 标记为创建歌单
      this.istype = true;
      // 替换
      this.Menutitle = this.itemCollectionmenu[index].M_Name;
      this.MenuTime = this.itemCollectionmenu[index].M_CreatTime;
      this.Menuimg = this.itemCollectionmenu[index].M_Img;
      this.Menuid = mid;
      this.xz = index;
      // 根据歌单id获取信息
      this.getmenu(mid);
      // 根据歌单id获取歌单评论
      this.loadgetcomment(mid);
    },
    // 跳转到歌曲详情
    songinfoclick(sid) {
      this.$router.push({
        path: "./songinfo",
        query: {
          songid: sid
        }
      });
    },
    // 歌单播放次数加一
    playadd1() {
      request({
        url: "/api/SongMenu/AddPlayCount",
        params: {
          id: this.Menuid
        }
      })
        .then(res => {
          console.log("播放歌单次数+1");
          // layer.msg(res);
        })
        .catch(err => {
          console.log(err);
        });
    },
    // 播放
    play() {
      if (this.itemsmymusic == "") {
        layer.msg("该歌单无播放歌曲~");
        return;
      }
      this.$store.commit("clearsongmenu", songplay);
      for (let i = 0; i < this.itemsmymusic.length; i++) {
        // console.log(this.itemsmymusic[i])
        var songplay = {
          name: "",
          singer: "",
          cover: "",
          url: "",
          id: ""
        };
        songplay = {};
        songplay.name = this.itemsmymusic[i].sName;
        songplay.singer = this.itemsmymusic[i].gName;
        songplay.cover = this.itemsmymusic[i].sCover;
        songplay.url = this.itemsmymusic[i].sUrl;
        songplay.id = this.itemsmymusic[i].sID;
        // console.log(songplay)
        this.$store.commit("playmeunsong", songplay);
      }
      this.$store.state.play();
      // 歌单播放次数加一
      this.playadd1();
      layer.msg("开始播放~");
    },
    // 发表评论
    addcomment() {
      var data = {
        mid: this.Menuid,
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
          this.loadgetcomment(this.Menuid);
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
        mid: this.Menuid,
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
          this.loadgetcomment(this.Menuid);
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  },
  computed: {
    descNum() {
      if (this.content.length >= 140) {
        this.content = this.content.substr(0, 140);
        return 140;
      }
      return this.content.length == 140 ? 140 : 140 - this.content.length;
    },
    descNum2() {
      if (this.huifucontent.length >= 140) {
        this.huifucontent = this.huifucontent.substr(0, 140);
        return 140;
      }
      return this.huifucontent.length == 140
        ? 140
        : 140 - this.huifucontent.length;
    }
  },
  mounted(){
      //拖拽
      onDrag($(".updmenudiv").get(0));
  }
};
</script>

<style scoped>
  @import url("./mymusic.css");

  /* 编辑歌单 */
	#menuupddiv {
		position: fixed;
    z-index: 10;
    width: 100%;
    height: 100%;
    -webkit-user-select:none;
    -moz-user-select:none;
    -ms-user-select:none;
    user-select:none;
	}

	.updmenudiv {
		width: 600px;
		height: 370px;
		background-color: #fff;
		position: absolute;
		left: 50%;
		top: 50%;
		transform: translate(-75%, -75%);
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

	.menuimg {
		position: absolute;
		top: 136px;
    left: 380px;
    
    cursor: pointer;
	}

	.imgs {
		width: 150px;
		height: 150px;
		/* border: 1px solid black; */
	}

	.msgss {
		/* border: 1px solid black; */
		width: 150px;
		position: absolute;
    top: 130px;
    left: 0;
		text-align: center;
		opacity: 0.5;
		color: white;
    background-color: #000000;
	}

	.seltype {
		margin-top: 20px;
		width: 300px;
    height: 38px;
    line-height: 1.3;
    border-width: 1px;
    border-style: solid;
    background-color: #fff;
    border-radius: 2px;
    border-color: #e6e6e6;
    transition: all 0.5s;
  }
  .seltype:hover{
    transition: all 0.5s;
    border-color:#D2D2D2
  }

	.info {
    margin-top: 20px;
    width:300px;
    resize:none;
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
</style>
