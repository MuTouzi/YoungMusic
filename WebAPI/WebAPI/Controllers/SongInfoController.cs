using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class SongInfoController : ApiController
    {
        /// <summary>
        /// 根据歌曲id 查详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 歌曲所有详情信息  但是不包括评论
        /// </returns>
        [HttpGet]
        public object SelSongInfoByID(int id)
        {
            return SongListService.SelSongbyID(id);
        }

        /// <summary>
        /// 添加歌曲收藏到歌单
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="songID"></param>
        /// <param name="muneID"></param>
        /// <returns></returns>
        [HttpGet]
        public string AddCollecting(int uID, int songID, int muneID)
        {
            //添加收藏
            return SongMenuService.AddSongToMenu(uID, songID, muneID, false);
        }

        /// <summary>
        /// 歌单里面删除歌曲
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="songID"></param>
        /// <param name="muneID"></param>
        /// <returns></returns>
        [HttpGet]
        public string RMCollecting(int uID, int songID, int muneID)
        {
            //取消收藏
            return SongMenuService.AddSongToMenu(uID, songID, muneID, true);
        }

        /// <summary>
        /// 歌曲播放次数+1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string AddPlayCount(int id)
        {
            if (SongListService.AddCount(id)) return "成功";
            else return "失败";
        }

        /// <summary>
        /// 歌曲收藏次数+1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string AddCollectCount(int id)
        {
            if (SongListService.AddCount(id, 2)) return "成功";
            else return "失败";
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string AddSongComment([FromBody] object json)
        {
            JObject j = JObject.Parse(json.ToString());
            //获取评论内容
            string sid = j["sid"].ToString();
            string pid = j["pid"].ToString();
            string content = j["content"].ToString();
            string from = j["from"].ToString();
            string to = j["to"].ToString();
            SongComment c = new SongComment();
            if (sid != "") c.SC_Song = int.Parse(sid); else return "参数不完整";
            if (pid != "") c.SC_Pid = int.Parse(pid);
            if (content != "") c.SC_Content = content; else return "参数不完整";
            if (from != "") c.SC_From_User = int.Parse(from); else return "参数不完整";
            if (to != "") c.SC_To_User = int.Parse(to); else c.SC_To_User = int.Parse(from);
            c.SC_UpTime = DateTime.Now;
            return SongCommentService.AddSongComment(c);
        }

        /// <summary>
        /// 根据歌曲id 获取所有评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CommModel> GetSongComment(int id)
        {
            var comm = SongCommentService.SelCommentBySongID(id);
            return comm;
        }
    }
}
