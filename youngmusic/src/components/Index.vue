<template>
  <div>
    <div id="content">
      <!-- 轮播动画 -->
      <div class="con_banner">
        <div class="img">
          <div class="left" @click="left()">&lt;</div>
          <img v-for="(v,i) in img" :key="i" :src="v" v-show="i==n" />
          <div>
            <ul>
              <li v-for="(v,i) in img" :key="i"></li>
            </ul>
          </div>
          <div class="right" @click="right()">&gt;</div>
        </div>
      </div>
    </div>
    <div class="middiv">
      <div class="remen">
        <p class="title">
          <img src="../assets/images/唱片.png" />
          <span class="t-span">
            热门推荐
            <!-- <span class="con_type">
								<a href="#">华语</a> |
								<a href="#">流行</a> |
								<a href="#">摇滚</a> |
								<a href="#">民谣</a>
            </span>-->
          </span>
          <span class="con_gengduo">
            <a href="javascript:;">更多>></a>
          </span>
        </p>
        <ul class="ul_list">
          <li v-for="(v,index) in itemssong" :key="index" @click="itemclick(v.S_Id)">
            <div>
              <a href="javascript:;">
                <img :src="v.S_Cover" />
              </a>
            </div>
            <a href="javascript:;">{{v.S_Name}}</a>
          </li>
        </ul>
      </div>
      <div class="gedan">
        <p class="title">
          <img src="../assets/images/唱片.png" />
          <span class="t-span">歌单</span>
          <span class="con_gengduo">
            <a href="javascript:;" @click="moremenuclick()">更多>></a>
          </span>
        </p>
        <ul class="ul_list">
          <li v-for="(values,index) in itemsmenu" :key="index" @click="itemmenuclick(values.M_Id)">
            <div>
              <a href="javascript:;">
                <img :src="imgurl+values.M_Img" />
              </a>
            </div>
            <a href="javascript:;">{{values.M_Name}}</a>
            <!-- M_UserId -->
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import $ from "jquery";
import { request } from "../network/request.js";
export default {
  name: "Index",
  data() {
    return {
      imgurl: "http://39.101.180.27:8089/api",
      // 用于轮播
      n: 0,
      imgpath: "",
      img: [
        require("../assets/images/轮播/t1.jpg"),
        require("../assets/images/轮播/t2.jpg"),
        require("../assets/images/轮播/t3.jpg"),
        require("../assets/images/轮播/t4.jpg"),
        require("../assets/images/轮播/t5.jpg"),
        require("../assets/images/轮播/t6.jpg"),
        require("../assets/images/轮播/t7.jpg"),
        require("../assets/images/轮播/t8.jpg")
      ],
      //推荐歌曲的数据
      itemssong: [],
      //推荐歌单的数据
      itemsmenu: []
    };
  },
  methods: {
    // 歌曲推荐
    SongRecommend() {
      //加载时显示推荐歌曲
      request({
        url: "/api/Home/Top8",
        params: {
          id: 1
        }
      })
        .then(res => {
          this.itemssong = res;
          // console.log(res)
        })
        .catch(err => {
          console.log(err);
        });
    },
    //歌单推荐
    MenuRecommend() {
      //加载时显示歌单
      request({
        url: "/api/Home/Top8",
        params: {
          id: 2
        }
      })
        .then(res => {
          // console.log(res);
          this.itemsmenu = res;
          // console.log(this.itemsmenu)
        })
        .catch(err => {
          console.log(err);
        });
    },
    // 轮播上一张
    left() {
      if (this.n >= this.img.length) {
        this.n = 0;
      }
      if (this.n >= 0) {
        this.n -= 1;
      }
    },
    // 轮播下一张
    right() {
      if (this.n == this.img.length) {
        this.n = 0;
      }
      if (this.n <= this.img.length - 1) {
        this.n += 1;
      }
    },
    play() {
      this.n++;
      if (this.n == this.img.length - 1) {
        this.n = 0;
      }
    },
    // 歌曲跳转
    itemclick(id) {
      this.$router.push({
        path: "./songinfo",
        query: {
          songid: id
        }
      });
    },
    // // 歌曲更多跳转
    // itemclick() {
    // 	this.$router.push('./songinfo')
    // },
    // 歌单跳转
    itemmenuclick(id) {
      this.$router.push({
        path: "./SongMenuInfo",
        query: {
          menuid: id
        }
      });
    },
    // 歌单更多跳转
    moremenuclick() {
      this.$router.push({
        path: "./songmenulist",
        query: {
          typeid: 0
        }
      });
    }
  },
  mounted() {
    sessionStorage.setItem("img", "http://39.101.180.27:8089/api");
    // 轮播定时
    setInterval(this.play, 8000);

    //加载时显示推荐歌曲
    this.SongRecommend();
    // 加载时显示推荐歌单
    this.MenuRecommend();
  }
};
</script>

<style scoped>
@media screen and (max-width: 400px) {
  .left {
    display: none;
  }
  .right {
    display: none;
  }
  .con_gengduo {
    display: none;
  }
}
/* banner轮播动画区 */
#banner {
  height: 400px;
  width: 100%;
  background: #242424;
}

.img {
  width: 70%;
  margin: 0 auto;
  text-align: center;
  color: #ffffff;
}

.left {
  opacity: 0.5;
  font-size: 6em;
  position: absolute;
  top: 250px;
  left: 180px;
  font-family: "agency fb";
  width: 50px;
  /* background: #000000; */
  cursor: pointer;
}

.left:hover {
  opacity: 1;
}

.img img {
  width: 100%;
  height: auto;
  height: 400px;
}

.right {
  opacity: 0.5;
  font-size: 6em;
  position: absolute;
  top: 250px;
  right: 180px;
  font-family: "agency fb";
  width: 50px;
  /* background: #000000; */
  cursor: pointer;
}

.right:hover {
  opacity: 1;
}

.img_li {
  display: flex;
  margin: 0 auto;
  text-align: center;
  margin-left: 50%;
  position: relative;
  top: -30px;
  left: -142px;
}

.li-d {
  width: 15px;
  height: 4px;
  background: #f5f5f5;
  border-radius: 2px;
  margin-left: 15px;
  opacity: 0.6;
}

.li-d:hover {
  background: #9b0909;
  opacity: 0.8;
  transition: 0.5s all;
}

.show {
  background: #9b0909;
  opacity: 0.8;
}

#content {
  background: #f5f5f5;
  /* background: black; */
}

.con_banner {
  height: 400px;
  background: #242424;
}

.title {
  width: 95%;
  margin: 0 auto;
  font-size: 1.2em;
  border-bottom: 3px #c20c0c solid;
  padding: 15px;
}

.title .t-span {
  position: relative;
  top: 15px;
}

.title > img {
  object-fit: initial;
  position: relative;
  top: 8px;
  padding: 0 10px;
  width: 30px;
  left: 20px;
  margin-right: 15px;
}

/* 热门推荐的几个歌曲类型 */

.title .con_type {
  font-size: 0.8em;
  margin-left: 30px;
}

.title .con_type a:hover {
  color: #9b0909;
  text-decoration: underline;
}

/* 更多按钮 */

.con_gengduo {
  font-size: 0.7em;
  position: absolute;
  right: 300px;
  margin-top: 22px;
}

/* 更多按钮的a标签悬浮添加下划线 */

.con_gengduo a:hover {
  color: #000;
  text-decoration: underline;
}

.middiv {
  background-color: #f5f5f5;
}
/* 热门推荐  个性  排行  歌单 */
.remen,
.gexing,
.paihang,
.gedan {
  width: 70%;
  margin: 0 auto;
  background: #fff;
}

/* 歌曲列表ul */

.ul_list {
  display: flex;
  flex-flow: wrap;
  margin: 0 auto;
  margin-left: 70px;
}

.ul_list li {
  margin: 15px 15px;
}

.ul_list li a {
  font-size: 0.9em;
  display: block;
  width: 150px;
  word-wrap: break-word;
}

.ul_list li:hover a {
  text-decoration: underline;
  color: #9b0909;
}

.ul_list img {
  width: 150px;
  height: 150px;
  min-width: 150px;
}
</style>
