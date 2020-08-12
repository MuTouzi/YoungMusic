using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class MyInfoController : ApiController
    {
        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public UserInfo UpUserInfo([FromBody] object json)
        {
            JObject j = JObject.Parse(json.ToString());
            UserInfo u = new UserInfo
            {
                U_Id = int.Parse(j["uid"].ToString()),
                //U_Img = j["img"].ToString(),
                U_Info = j["info"].ToString(),
                U_Name = j["name"].ToString(),
                U_Birthday = (DateTime)j["birthday"],
                U_Tell = j["tell"].ToString(),
                U_Gender = j["gender"].ToString(),
                U_Hobby = j["hobby"].ToString()
            };
            return UserInfoService.UpUserInfo(u);
        }


        /// <summary>
        /// 查询用户的粉丝
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public object SelUserFans(int id)
        {
            return UserInfoService.SelUserFans(id);
        }

        /// <summary>
        /// 查询用户的关注
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public object SelUserFollow(int id)
        {
            return UserInfoService.SelUserFollow(id);
        }

        /// <summary>
        /// 用户关注另一个用户
        /// </summary>
        /// <param name="id">当前用户id</param>
        /// <param name="uid">要关注的用户id</param>
        /// <returns></returns>
        [HttpGet]
        public string AddUserFollow(int id, int uid)
        {
            return UserInfoService.AddOrRmUserFollow(id, uid, true);
        }

        /// <summary>
        /// 用户取消关注另一个用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public string RmUserFollow(int id, int uid)
        {
            return UserInfoService.AddOrRmUserFollow(id, uid, false);
        }

        /// <summary>
        /// 查询该用户的基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Dictionary<string, object> SelUserInfoByID(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var u = UserInfoService.SelUserInfoByID(id);
            if (u != null)
            {
                string[] creatSongMenu = u.U_CreatSongMenu?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] collectSongMenu = u.U_CollectSongMenu?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var creatSong = SongMenuService.SelSongMenuByUser(creatSongMenu);
                var collectSong = SongMenuService.SelSongMenuByUser(collectSongMenu);
                dic.Add("userInfo", u);
                dic.Add("creatSongMenu", creatSong);
                dic.Add("collectSongMenu", collectSong);
            }
            return dic;
        }

        /// <summary>
        /// 根据用户id查询最近听的歌曲和听歌次数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [HttpGet]
        public Dictionary<string, object> SelRecentSong(int id, int length = 20)
        {
            return SongListService.SelRecentSong(id, length);
        }

        /// <summary>
        /// 添加最近播放的歌曲
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="sid">歌曲id</param>
        /// <returns></returns>
        [HttpGet]
        public string AddRecentSong(int id, int sid)
        {
            if (UserInfoService.AddRecentSong(id, sid)) return "成功";
            else return "失败";
        }
    }
}
