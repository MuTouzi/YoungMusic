using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class SongMenuController : ApiController
    {
        /// <summary>
        /// 根据歌单id 查询歌单基本信息和歌单的歌曲内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Dictionary<string, object> SelSongMenuByID(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var m = SongMenuService.SelSongMenuByID(id);
            if (m != null)
            {
                string songID = m.GetType().GetProperty("M_SongId").GetValue(m).ToString();
                string[] songMenuNum = songID?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var song = SongListService.SelSongListByMenu(songMenuNum);
                dic.Add("songMenuInfo", m);
                dic.Add("songInfo", song);
            }
            return dic;
        }

        /// <summary>
        /// 根据类型id查找歌单信息 类型id为0 查询全部
        /// </summary>
        /// <param name="id"></param>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public Dictionary<string, object> SelSongMenuByTypeID(int id, int top = 30, int page = 0)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            int count;
            if (id == 0)
                dic.Add("SongMenus", SongMenuService.GetSongMenus(out count, top, page));
            else
                dic.Add("SongMenus", SongMenuService.SelSongMenuByTypeID(out count, id, top, page));
            int pageSize = count % top == 0 ? count / top : (count / top) + 1;
            dic.Add("count", count);
            dic.Add("pageSize", pageSize);
            return dic;
        }

        /// <summary>
        /// 传歌单的基本信息 添加歌单并绑定到用户表字段
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string AddSongMenuToUser([FromBody] object json)
        {
            JObject j = JObject.Parse(json.ToString());
            SongMenu s = new SongMenu()
            {
                M_Img = "/Default/AddDafaultMenu.jpg",
                M_Info = "暂无简介",
                M_Name = j["name"].ToString(),
                M_Type = 1,
                M_UserId = int.Parse(j["uid"].ToString()),
                M_SongId = "",
                M_CollectCount = 0,
                M_PlayCount = 0,
                M_CreatTime = DateTime.Now
            };
            try
            {
                int mid = SongMenuService.CreatInitSongMenu(s);
                // 将创建的歌单添加到用户表字段中
                if (SongMenuService.AddSongMenu(s.M_UserId, mid, 1) == "创建成功")
                    return mid + "";
                else
                    return "失败";
            }
            catch (Exception)
            {
                return "失败";
            }
        }

        /// <summary>
        /// 删除创建的歌单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        [HttpGet]
        public string RmCreateSongMenu(int uid, int mid)
        {
            return SongMenuService.AddSongMenu(uid, mid, 1, true);
        }

        /// <summary>
        /// 删除收藏的歌单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        [HttpGet]
        public string RmCollectSongMenu(int uid, int mid)
        {
            return SongMenuService.AddSongMenu(uid, mid, 2, true);
        }

        /// <summary>
        /// 添加收藏的歌单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        [HttpGet]
        public string AddCollectSongMenu(int uid, int mid)
        {
            return SongMenuService.AddSongMenu(uid, mid, 2);
        }

        /// <summary>
        /// 歌单播放次数 +1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string AddPlayCount(int id)
        {
            if (SongMenuService.AddPlayCount(id))
                return "成功";
            else
                return "失败";
        }

        /// <summary>
        /// 修改歌单信息
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpSongMenuInfo([FromBody] object json)
        {
            JObject j = JObject.Parse(json.ToString());
            SongMenu s = new SongMenu()
            {
                M_Id = int.Parse(j["mid"].ToString()),
                //M_Img = j["img"].ToString(),
                M_Info = j["info"].ToString(),
                M_Name = j["name"].ToString(),
                M_Type = int.Parse(j["type"].ToString())
            };
            return SongMenuService.UpSongMenuInfo(s);
        }

        /// <summary>
        /// 根据歌单id 获取歌单评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CommModel> GetSongMenuComm(int id)
        {
            //Dictionary<string,object>
            return SongMenuService.GetSongMenuComm(id);
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string AddSongMenuComment([FromBody] object json)
        {
            JObject j = JObject.Parse(json.ToString());
            //获取评论内容
            string mid = j["mid"].ToString();
            string pid = j["pid"].ToString();
            string content = j["content"].ToString();
            string from = j["from"].ToString();
            string to = j["to"].ToString();
            MenuComment c = new MenuComment();
            if (mid != "") c.SM_Menu = int.Parse(mid); else return "参数不完整";
            if (pid != "") c.SM_Pid = int.Parse(pid);
            if (content != "") c.SM_Content = content; else return "参数不完整";
            if (from != "") c.SM_From_User = int.Parse(from); else return "参数不完整";
            if (to != "") c.SM_To_User = int.Parse(to); else c.SM_To_User = int.Parse(from);
            c.SM_UpTime = DateTime.Now;
            return SongMenuService.AddSongMenuComment(c);
        }

        /// <summary>
        /// 获取当前歌单是否已经收藏
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        [HttpGet]
        public string IsCollect(int uid, int mid)
        {
            try
            {
                if (SongMenuService.IsCollect(uid, mid))
                    return "已收藏";
                else
                    return "未收藏";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
