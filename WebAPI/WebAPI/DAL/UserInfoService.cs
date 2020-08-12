using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.DAL
{
    /// <summary>
    /// UserInfo 用户信息管理
    /// </summary>
    public class UserInfoService
    {
        /// <summary>
        /// 添加一个用户信息
        /// </summary>
        /// <param name="u">UserInfo</param>
        internal static int AddUserInfo(UserInfo u)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                UserInfo user = db.UserInfo.FirstOrDefault(m => m.U_Email == u.U_Email);
                try
                {
                    if (user == null)
                    {
                        db.UserInfo.Add(u);
                        db.SaveChanges();
                        user = db.UserInfo.FirstOrDefault(m => m.U_Email == u.U_Email);
                        return user.U_Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 跟新用户头像
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="resultUrl"></param>
        internal static void UpUserPhoto(int uid, string resultUrl)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var user = db.UserInfo.FirstOrDefault(m => m.U_Id == uid);
                if (user != null)
                {
                    user.U_Img = resultUrl;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        internal static UserInfo UpUserInfo(UserInfo u)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var user = db.UserInfo.FirstOrDefault(m => m.U_Id == u.U_Id);
                if (user != null)
                {
                    user.U_Id = u.U_Id;
                    user.U_Info = u.U_Info;
                    user.U_Name = u.U_Name;
                    user.U_Birthday = u.U_Birthday;
                    user.U_Tell = u.U_Tell;
                    user.U_Gender = u.U_Gender;
                    user.U_Hobby = u.U_Hobby;
                }
                db.SaveChanges();
                return SelUserInfoByID(u.U_Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">当前用户id</param>
        /// <param name="uid">要关注/取消关注的用户id</param>
        /// <param name="v">关注还是取消关注  true:关注  false：取消关注</param>
        internal static string AddOrRmUserFollow(int id, int uid, bool v)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var u1 = db.UserInfo.FirstOrDefault(m => m.U_Id == id);
                var u2 = db.UserInfo.FirstOrDefault(m => m.U_Id == uid);
                if (u1 == null || u2 == null) return "用户不存在";

                string[] number1 = u1.U_Follow?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] number2 = u2.U_Fans?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //取消关注后的关注列表
                string nowNumber1 = "";
                //取消关注后的粉丝列表
                string nowNumber2 = "";
                //是否已经关注
                bool f1 = false;
                //是否有这个粉丝
                bool f2 = false;
                foreach (string item in number1)
                {
                    //如果是相等的 说明已经关注
                    if (item == u2.U_Id.ToString())
                        f1 = true;
                    else
                        //取消关注后的关注列表
                        nowNumber1 = nowNumber1 + item + ",";
                }
                foreach (string item in number2)
                {
                    //如果是相等的 说明已经有这个粉丝
                    if (item == u1.U_Id.ToString())
                        f2 = true;
                    else
                        //取消关注后的粉丝列表
                        nowNumber2 = nowNumber2 + item + ",";
                }

                if (v)
                {
                    //如果是添加关注
                    if (f1 && f2) return "已关注";
                    if (f1 && !f2)
                    {
                        //我已经关注了 但是你的粉丝表里面还没有我
                        u2.U_Fans += u1.U_Id + ",";
                    }
                    else if (!f1 && f2)
                    {
                        //我还没关注你 但是你的粉丝表有我  那就将我从你的粉丝表的数据减出来
                        u2.U_Fans = nowNumber2;
                    }
                    else
                    {
                        u1.U_Follow += u2.U_Id + ",";
                        u2.U_Fans += u1.U_Id + ",";
                    }
                    db.SaveChanges();
                    return "添加关注成功";
                }
                else
                {
                    //如果是取消关注
                    if (!f1 && !f2) return "当前未关注";
                    if (f1 && !f2)
                    {
                        //我已经关注了 但是你的粉丝表里面还没有我
                        u2.U_Fans += u1.U_Id + ",";
                    }
                    else if (!f1 && f2)
                    {
                        //我还没关注你 但是你的粉丝表有我  那就将我从你的粉丝表的数据减出来
                        u2.U_Fans = nowNumber2;
                    }
                    else
                    {
                        u1.U_Follow = nowNumber1;
                        u2.U_Fans = nowNumber2;
                    }
                    db.SaveChanges();
                    return "取消关注成功";
                }
            }
        }

        /// <summary>
        /// 用户的关注数据
        /// </summary>
        /// <param name="id"></param>
        internal static object SelUserFollow(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var userInfo = db.UserInfo.FirstOrDefault(m => m.U_Id == id);
                if (userInfo != null)
                {
                    if (userInfo.U_Follow == null) userInfo.U_Follow = "";
                    string[] number = userInfo.U_Follow?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> list = new List<int>();
                    foreach (string item in number)
                    {
                        list.Add(int.Parse(item));
                    }
                    var u = (from user in db.UserInfo
                             select new
                             {
                                 U_Id = user.U_Id,
                                 U_Img = user.U_Img,
                                 U_Info = user.U_Info,
                                 U_Like = user.U_Like,
                                 U_Name = user.U_Name,
                                 U_Email = user.U_Email,
                                 U_Birthday = user.U_Birthday,
                                 U_Tell = user.U_Tell,
                                 U_Fans = user.U_Fans,
                                 U_Follow = user.U_Follow,
                                 U_Gender = user.U_Gender,
                                 U_Hobby = user.U_Hobby,
                                 U_RegistrationTime = user.U_RegistrationTime,
                                 U_CollectSongMenu = user.U_CollectSongMenu,
                                 U_CreatSongMenu = user.U_CreatSongMenu
                             }).Where(m => list.Contains(m.U_Id)).ToList();
                    return u;
                }
            }
            return null;
        }



        /// <summary>
        /// 用户的粉丝数据
        /// </summary>
        /// <param name="id"></param>
        internal static object SelUserFans(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var userInfo = db.UserInfo.FirstOrDefault(m => m.U_Id == id);
                if (userInfo != null)
                {
                    if (userInfo.U_Fans == null) userInfo.U_Fans = "";
                    string[] number = userInfo.U_Fans?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> list = new List<int>();
                    foreach (string item in number)
                    {
                        list.Add(int.Parse(item));
                    }
                    var u = (from user in db.UserInfo
                             select new
                             {
                                 U_Id = user.U_Id,
                                 U_Img = user.U_Img,
                                 U_Info = user.U_Info,
                                 U_Like = user.U_Like,
                                 U_Name = user.U_Name,
                                 U_Email = user.U_Email,
                                 U_Birthday = user.U_Birthday,
                                 U_Tell = user.U_Tell,
                                 U_Fans = user.U_Fans,
                                 U_Follow = user.U_Follow,
                                 U_Gender = user.U_Gender,
                                 U_Hobby = user.U_Hobby,
                                 U_RegistrationTime = user.U_RegistrationTime,
                                 U_CollectSongMenu = user.U_CollectSongMenu,
                                 U_CreatSongMenu = user.U_CreatSongMenu
                             }).Where(m => list.Contains(m.U_Id)).ToList();
                    return u;
                }
            }
            return null;
        }

        /// <summary>
        /// 查询用户基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static UserInfo SelUserInfoByID(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var user = db.UserInfo.FirstOrDefault(m => m.U_Id == id);
                if (user != null)
                {
                    UserInfo u = new UserInfo()
                    {
                        U_Img = user.U_Img,
                        U_Info = user.U_Info,
                        U_Like = user.U_Like,
                        U_Name = user.U_Name,
                        U_Email = user.U_Email,
                        U_Birthday = user.U_Birthday,
                        U_Tell = user.U_Tell,
                        U_Fans = user.U_Fans,
                        U_Follow = user.U_Follow,
                        U_Gender = user.U_Gender,
                        U_Hobby = user.U_Hobby,
                        U_RegistrationTime = user.U_RegistrationTime,
                        U_CollectSongMenu = user.U_CollectSongMenu,
                        U_CreatSongMenu = user.U_CreatSongMenu
                    };
                    return u;
                }
                return null;
            }
        }

        /// <summary>
        /// 添加到用户最近播放
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        internal static bool AddRecentSong(int id, int sid)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var u = db.UserInfo.FirstOrDefault(m => m.U_Id == id);
                if (u != null)
                {
                    u.U_Like += sid.ToString() + ",";
                    SongListService.AddCount(sid, 1);
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }

        /// <summary>
        /// 查询当前用户是否存在 并且邮箱和密码正确
        /// </summary>
        /// <param name="u">如果邮箱密码正确 并存在这个用户 返回true  否则返回false</param>
        /// <returns></returns>
        internal static UserInfo SelUserInfo(UserInfo u)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                UserInfo user = db.UserInfo.FirstOrDefault(m => m.U_Email == u.U_Email && m.U_Pwd == u.U_Pwd);
                return user;
            }
        }

    }
}