<template>
  <div id="app">
    <headers></headers>
    <router-view class="contentView"></router-view>
    <player class="play"></player>
    <footers></footers>
  </div>
</template>

<script>
import headers from "./components/header.vue";
import footers from "./components/footer.vue";

// 播放
import player from "./components/musicplayer.vue";

import $ from "jquery";
export default {
  name: "App",
  data() {
    return {};
  },
  components: {
    headers,
    footers,
    player
  },
mounted() {
this.resize();
//监听页面缩放 让他始终保持最佳大小
  window.onresize = () => {
    return (() => {
      var w = $(window).width();
      $("body").css("zoom", w / 1500);
    })();
  };
},
methods: {
  resize: function() {
    var w = $(window).width();
    //   console.log(w);
    $("body").css("zoom", w / 1500);
  }
}
};
</script>

<style>
/* 字体 */
/* @import url("assets/font-awesome-4.7.0/css/font-awesome.css"); */
/* 模板 */
@import url("assets/css/Template_css.css");

body {
  background-color: rgba(241, 240, 240, 0.7);
}
a:hover {
  cursor: pointer;
}
.play {
  z-index: 1000;
}
.contentView {
  min-height: 50vh;
}
body {
    min-width: 1200px;
}
</style>
