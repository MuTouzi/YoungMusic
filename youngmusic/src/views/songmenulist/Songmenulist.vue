<template>
  <div id="appdiv">
    <div id="app-cendiv">
      <div class="dtdiv layui-icon">
        <p>
          <button
            :class="['layui-btn', 'layui-btn-sm','btn-type',{btnxz:typeindex==index}]"
            v-for="(items,index) in songtypeitems"
            :key="index"
            @click="btntypeclick(index)"
          >{{items}}</button>
          <!-- <button class="layui-btn layui-btn-sm  layui-bg-red btn-type">热门</button> -->
        </p>
      </div>
      <hr class="layui-bg-red ftrst-hr" />
      <h2 v-show="Songmenuitem.length<=0" class="msg">目前还没有该类型歌单哦 去新建一个吧！</h2>
      <div id="gedandiv" class="row">
        <div
          class="gedanRow"
          v-for="(items,index) in Songmenuitem"
          :key="index"
          @click="menuclick(items.M_Id)"
        >
          <a href="javascript:;" :title="items.M_Name">
            <dl class="dlgedan">
              <dt>
                <img :src="imgurl+items.M_Img" />
              </dt>
              <dd>
                
                <a href="javascript:;">{{items.M_Name.length < 10 ? items.M_Name : items.M_Name.substr(0, 9) + "..." }}</a>
              </dd>
              <dd>
                <a href="javascript:;" @click.stop="userclick(items.M_UserId)">
                  <span class="by">by:</span>
                  {{items.U_Name.length < 8 ? items.U_Name : items.U_Name.substr(0,7) + "..." }}
                </a>
              </dd>
            </dl>
          </a>
        </div>
      </div>
      <div id="pagediv">
        <ul class="pagination">
          <li>
            <a href="javascript:;" @click="first()">«</a>
          </li>
          <li>
            <a href="javascript:;" @click="shang()">上一页</a>
          </li>
          <li>
            <a
              href="javascript:;"
              @click="pageclick(index)"
              :class="[{active:pageclassindex==index}]"
              v-for="(page,index ) in pageindex"
              :key="index"
            >{{index+1}}</a>
          </li>
          <!-- v-if="index<10" -->
          <li>
            <a href="javascript:;" @click="xia()">下一页</a>
          </li>
          <li>
            <a href="javascript:;" @click="last()">»</a>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import { request } from "../../network/request.js";
export default {
  name: "Songmenulist",
  data() {
    return {
      imgurl: sessionStorage.img,
      // 类型按钮的索引
      typeindex: 0,
      // 分页的索引
      pageindex: 0,
      // 各分的样式
      pageclassindex: 0,
      songtypeitems: [
        "全部",
        "流行",
        "华语",
        "欧美",
        "日语",
        "粤语",
        "民族",
        "摇滚",
        "民谣",
        "说唱",
        "轻音乐"
      ],
      Songmenuitem: "",
      // 歌单详情页跳转过来的
      typeid: this.$route.query.typeid
    };
  },
  methods: {
    // 分页查询(类型按钮)
    btntypeclick(indexs) {
      // 将下面分页页面的下标为0
      this.pageclassindex = 0;
      // 将选择的类型给全局--控制类型按钮的样式
      this.typeindex = indexs;
      // 根据歌曲类型查询数据肯定是显示第一页数据
      this.getsongmenutype(indexs, 0);
    },
    // 根据歌曲类型查询数据
    getsongmenutype(typeid, page) {
      request({
        url: "api/SongMenu/SelSongMenuByTypeID",
        params: {
          // 类型
          id: typeid,
          // 一页多少条
          top: 25,
          // 第几页
          page: page
        }
      })
        .then(res => {
          // console.log(res)
          // 将查询的歌曲信息传入
          this.Songmenuitem = res.SongMenus;
          // 将有多少页返回
          this.pageindex = res.pageSize;
        })
        .catch(err => {
          console.log(err);
        });
    },
    // 分页按钮具体的1，2，3
    pageclick(index) {
      // 控制样式
      this.pageclassindex = index;
      this.getsongmenutype(this.typeindex, index);
    },
    //跳转到用户
    userclick(uid) {
      this.$router.push({
        path: "./info",
        query: {
          friendid: uid
        }
      });
    },
    // 跳转到歌单
    menuclick(mid) {
      this.$router.push({
        path: "./SongMenuInfo",
        query: {
          menuid: mid
        }
      });
    },
    // 第一页
    first() {
      this.pageclassindex = 0;
      this.getsongmenutype(this.typeindex, 0);
    },
    // 最后一页
    last() {
		// 更改样式
      this.pageclassindex = this.pageindex - 1;
      this.getsongmenutype(this.typeindex, this.pageindex - 1);
    },
    // 上一页
    shang() {
      if (this.pageclassindex > 0) {
        this.pageclassindex--;
				this.getsongmenutype(this.typeindex, this.pageclassindex);
      }
      
    },
    // 下一页
    xia() {
      if (this.pageclassindex < this.pageindex - 1) {
        this.pageclassindex++;
				this.getsongmenutype(this.typeindex, this.pageclassindex);
      }
      
    }
  },

  created() {
    if (this.typeid != "") {
      this.typeindex = this.typeid;
      this.getsongmenutype(this.typeid, 0);
    } else {
      this.getsongmenutype(0, 0);
    }
  }
};
</script>

<style scoped="" >
@import url("./songmenulist.css");
</style>
