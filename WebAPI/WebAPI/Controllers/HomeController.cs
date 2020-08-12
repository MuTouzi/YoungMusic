using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using System.Web.Http;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// 通过搜索框批量添加歌曲
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddSongList([FromBody] object json)
        {
            bool f = true;
            //解析json信息
            JArray jar = (JArray)JsonConvert.DeserializeObject(json.ToString());
            for (int i = 0; i < jar.Count; i++)
            {
                JObject j = JObject.Parse(jar[i].ToString());
                SongList s = new SongList()
                {
                    WYID = j["S_Id"].ToString(),
                    S_Name = j["S_Name"].ToString(),
                    S_Type = 1,
                    //添加到数据库  这里直接接收返回的歌手id
                    S_Singer = SongListService.AddSinger(j["S_Singer"].ToString()),
                    S_Url = j["S_Url"].ToString(),
                    S_Cover = j["S_Cover"].ToString(),
                    S_PlayCount = 0,
                    S_CollectCount = 0,
                    S_UpTime = DateTime.Now,
                    S_Lyric = "",
                    S_Album = 0,
                };
                if (!SongListService.AddSongList(s))
                {
                    f = false;
                }
            }
            if (f)
                return "添加成功";
            else
                return "添加失败";

        }


        /// <summary>
        /// 通过邮箱 密码 注册账号
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public string Register()
        {
            HttpContextBase contextBase = (HttpContextBase)Request.Properties["MS_HttpContext"];
            HttpRequestBase requestBase = contextBase.Request;

            UserInfo u = new UserInfo()
            {
                U_Name = requestBase.Form["email"].ToString(),
                U_Pwd = requestBase.Form["pwd"].ToString(),
                U_Email = requestBase.Form["email"].ToString(),
                U_Img = "/Default/PhotoDefault.jpg",
                U_Fans = "",//粉丝
                U_CreatSongMenu = "",//创建的歌单
                U_CollectSongMenu = "",//收藏的歌单
                U_Hobby = "",//爱好
                U_Tell = "",//电话
                U_Info = "这个人很懒 还没有填写哦~",//个人介绍
                U_Like = "",
                U_Follow = "",//关注
                U_Gender = "男",//性别
                U_Birthday = DateTime.Now,
                U_RegistrationTime = DateTime.Now
            };
            int uid = UserInfoService.AddUserInfo(u);
            if (uid != 0)
            {
                //初始化用户信息  --创建默认歌单 我的喜欢
                int sid = SongMenuService.CreatInitSongMenu(uid);
                //将创建的歌曲id 添加到用户创建列表
                if (sid != 0)
                {
                    try
                    {
                        if (SongMenuService.AddSongMenu(uid, sid, 1) == "创建成功")
                            return "注册成功";
                    }
                    catch (Exception e)
                    {
                        //用户不存在
                        return e.Message;
                    }
                }
                return "注册成功,用户信息初始化失败";
            }
            else
                return "注册失败，当前账号已存在或服务器错误";
        }

        /// <summary>
        /// 通过邮箱 发送验证码
        /// </summary>
        /// <returns>返回验证码</returns>
        [HttpPost]
        public object RegisterVerify([FromBody] string email)
        {
            string code = VerificationCode.code(6);
            try
            {
                if (Email.SendEmail(new string[] { email }, code))
                    return code;
                else return "";
            }
            catch (Exception)
            {
                return "邮件发生出现问题";
            }
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserInfo Login()
        {
            HttpContextBase contextBase = (HttpContextBase)Request.Properties["MS_HttpContext"];
            HttpRequestBase requestBase = contextBase.Request;
            UserInfo u = new UserInfo()
            {
                U_Email = requestBase.Form["email"].ToString(),
                U_Pwd = requestBase.Form["pwd"].ToString()
            };
            UserInfo user = UserInfoService.SelUserInfo(u);
            if (user != null)
            {
                u = new UserInfo()
                {
                    U_Id = user.U_Id,
                    U_Name = user.U_Name,
                    U_Img = user.U_Img,
                    U_Email = user.U_Email
                };
                return u;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 首页推荐  热门歌曲 热门歌单获取排前8条
        /// </summary>
        /// <param name="id">1：热门歌曲  2：热门歌单</param>
        /// <returns></returns>
        [HttpGet]
        public object Top8(int id)
        {
            int top = 10;
            if (id == 1)
            {
                //热门推荐
                var list = SongListService.SelHotTop(top);
                return list;
            }
            else if (id == 2)
            {
                //歌单
                var list = SongListService.SelSongListTop(top);
                return list;
            }
            return null;
        }

    }
}