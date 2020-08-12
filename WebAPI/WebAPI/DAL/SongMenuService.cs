using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.DAL
{
    /// <summary>
    /// SongMenu  歌单管理
    /// </summary>
    public class SongMenuService
    {
        /// <summary>
        /// 用户注册 初始化默认歌单 我喜欢的音乐
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>歌单id 如果id=0  初始化失败</returns>
        internal static int CreatInitSongMenu(int uid)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                try
                {
                    SongMenu s = new SongMenu()
                    {
                        M_Img = "/Default/MenuDefault.png",
                        M_UserId = uid,
                        M_Name = "我喜欢的音乐",
                        M_Info = "我喜欢的音乐♥",
                        M_Type = 1,
                        M_PlayCount = 0,
                        M_SongId = "",
                        M_CollectCount = 0,
                        M_CreatTime = DateTime.Now
                    };
                    db.SongMenu.Add(s);
                    db.SaveChanges();
                    SongMenu songMenu = db.SongMenu.FirstOrDefault(m => m.M_UserId == uid && m.M_Name == "我喜欢的音乐");
                    if (songMenu != null)
                        return songMenu.M_Id;
                    else
                        return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 创建一个歌单 并返回添加的歌单id
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal static int CreatInitSongMenu(SongMenu s)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                try
                {
                    db.SongMenu.Add(s);
                    db.SaveChanges();
                    SongMenu songMenu = db.SongMenu.OrderByDescending(m => m.M_CreatTime).FirstOrDefault(m => m.M_Name == s.M_Name && m.M_UserId == s.M_UserId);
                    if (songMenu != null)
                        return songMenu.M_Id;
                    else
                        return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 上传歌单图片
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="resultUrl"></param>
        internal static void UpSongMenuImg(int mid, string resultUrl)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var m = db.SongMenu.FirstOrDefault(x => x.M_Id == mid);
                if (m != null)
                {
                    m.M_Img = resultUrl;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据用户存储的歌单id 查询歌单列表
        /// </summary>
        /// <param name="SongMenuID"></param>
        /// <returns></returns>
        internal static object SelSongMenuByUser(string[] SongMenuID)
        {
            if (SongMenuID == null) return null;
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                //将歌曲id转成list 使用Contains去查询包含该id的歌曲信息
                List<int> list = new List<int>();
                foreach (string item in SongMenuID)
                {
                    list.Add(int.Parse(item));
                }
                var song = (from s in db.SongMenu
                            join u in db.UserInfo
                            on s.M_UserId equals u.U_Id
                            select new
                            {
                                M_Id = s.M_Id,
                                M_Name = s.M_Name,
                                M_Img = s.M_Img,
                                M_Info = s.M_Info,
                                M_CollectCount = s.M_CollectCount,
                                M_PlayCount = s.M_PlayCount,
                                M_Type = s.M_Type,
                                M_UserId = s.M_UserId,
                                U_Name = u.U_Name,
                                M_CreatTime = s.M_CreatTime,
                                M_SongId = s.M_SongId
                            }).Where(m => list.Contains(m.M_Id)).ToList();
                return song;
            }
        }


        /// <summary>
        /// 根据类型id 查询歌单列表 有分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        internal static object SelSongMenuByTypeID(out int count, int id, int top, int page)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var songMenu = (from s in db.SongMenu
                                join u in db.UserInfo
                                on s.M_UserId equals u.U_Id
                                where s.M_Type == id
                                select new
                                {
                                    M_Id = s.M_Id,
                                    M_Name = s.M_Name,
                                    M_Img = s.M_Img,
                                    M_Info = s.M_Info,
                                    M_CollectCount = s.M_CollectCount,
                                    M_PlayCount = s.M_PlayCount,
                                    M_Type = s.M_Type,
                                    M_UserId = s.M_UserId,
                                    U_Name = u.U_Name,
                                    M_CreatTime = s.M_CreatTime,
                                    M_SongId = s.M_SongId
                                }).Where(x => x.M_Name != "我喜欢的音乐").ToList();
                count = songMenu.Count;
                var sm = songMenu.OrderBy(m => m.M_PlayCount).OrderByDescending(m => m.M_CollectCount)
                    .Skip(top * page).Take(top);
                return sm;
            }
        }

        /// <summary>
        /// 修改歌单信息
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal static string UpSongMenuInfo(SongMenu s)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                SongMenu m = db.SongMenu.FirstOrDefault(sm => sm.M_Id == s.M_Id);
                if (m != null)
                {
                    m.M_Info = s.M_Info;
                    m.M_Name = s.M_Name;
                    m.M_Type = s.M_Type;
                    //m.M_UserId = s.M_UserId;
                    db.SaveChanges();
                    return "修改成功";
                }
                return "要修改的歌单不存在";
            }
        }

        /// <summary>
        /// 判断歌单用户是否已经收藏
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        internal static bool IsCollect(int uid, int mid)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var u = db.UserInfo.FirstOrDefault(x => x.U_Id == uid);
                if (u == null) throw new Exception("用户不存在");
                string[] songNumber = u.U_CollectSongMenu.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in songNumber)
                {
                    if (item == mid.ToString())
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 获取所有歌单信息
        /// </summary>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        internal static object GetSongMenus(out int count, int top, int page)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var songMenu = (from s in db.SongMenu
                                join u in db.UserInfo
                                on s.M_UserId equals u.U_Id
                                select new
                                {
                                    M_Id = s.M_Id,
                                    M_Name = s.M_Name,
                                    M_Img = s.M_Img,
                                    M_Info = s.M_Info,
                                    M_CollectCount = s.M_CollectCount,
                                    M_PlayCount = s.M_PlayCount,
                                    M_Type = s.M_Type,
                                    M_UserId = s.M_UserId,
                                    U_Name = u.U_Name,
                                    M_CreatTime = s.M_CreatTime,
                                    M_SongId = s.M_SongId
                                }).Where(x => x.M_Name != "我喜欢的音乐").ToList();
                count = songMenu.Count;
                var sm = songMenu.OrderByDescending(m => m.M_PlayCount)
                    .OrderByDescending(m => m.M_CollectCount).Skip(top * page).Take(top);
                return sm;
            }
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static string AddSongMenuComment(MenuComment c)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                db.MenuComment.Add(c);
                if (db.SaveChanges() > 0) return "发布成功"; else return "发布失败";
            }
        }


        /// <summary>
        /// 歌单播放次数+1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static bool AddPlayCount(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var s = db.SongMenu.FirstOrDefault(m => m.M_Id == id);
                s.M_PlayCount += 1;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 添加收藏歌曲/取消收藏歌曲
        /// </summary>
        /// <param name="uID">用户id</param>
        /// <param name="songID">歌曲id</param>
        /// <param name="muneID">歌单id</param>
        /// <param name="flag">是否要删除  已收藏并且要删除才会删除  true要删除  false 不删除</param>
        /// <returns>操作成功 返回 true  歌单不存在会抛出异常</returns>
        internal static string AddSongToMenu(int uID, int songID, int muneID, bool flag)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                SongMenu m = db.SongMenu.FirstOrDefault(menu => menu.M_Id == muneID && menu.M_UserId == uID);
                if (m != null)
                {
                    string number = m.M_SongId;
                    string[] songNumber = number.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string nowNumber = "";
                    bool f = false;
                    foreach (string item in songNumber)
                    {
                        //如果是相等的 说明已收藏
                        if (item == songID.ToString())
                            f = true;
                        else
                            nowNumber = nowNumber + item + ",";
                    }
                    if (f)
                    {
                        //如果是已收藏 然后要删除
                        if (flag)
                        {
                            m.M_SongId = nowNumber;
                            db.SaveChanges();
                            return "删除成功";
                        }
                        else
                        {
                            //不要删除
                            m.M_SongId = number;
                            db.SaveChanges();
                            return "已收藏";
                        }
                    }
                    else
                    {
                        //如果是没有收藏
                        number = number + songID + ",";
                        m.M_SongId = number;
                        db.SaveChanges();
                        return "添加收藏成功";
                    }
                }
                return ("歌单不存在");
            }
        }


        /// <summary>
        /// 操作用户创建或收藏的歌单
        /// 要删除歌单并且是已收藏或者是已创建的歌单：删除成功===已收藏或已经创建要继续收藏创建：已收藏===当前歌单成功添加到用户创建列表：创建成功===当歌单成功添加到用户收藏列表：收藏成功
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="sid">歌单id</param>
        /// <param name="flag">1：操作用户创建的歌单-----2：操作用户收藏的歌单</param>
        /// <param name="isRM">是否是删除 true 要删除  false 不删除  默认false</param>
        /// <returns>要删除歌单并且是已收藏或者是已创建的歌单：删除成功===已收藏或已经创建要继续收藏创建：已收藏===当前歌单成功添加到用户创建列表：创建成功===当歌单成功添加到用户收藏列表：收藏成功</returns>
        internal static string AddSongMenu(int uid, int mid, int flag, bool isRM = false)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                UserInfo u = db.UserInfo.FirstOrDefault(m => m.U_Id == uid);
                if (u != null)
                {
                    try
                    {
                        //把传进来的歌曲id除外的字符串  如果已收藏 要删除收藏 用这个字符串
                        string nowNumber = "";
                        //用来判断是否已经收藏
                        bool f = false;

                        //添加到我创建的歌单列表
                        if (flag == 1)
                        {
                            string number = u.U_CreatSongMenu;
                            if (number == null)
                            {
                                number = "";
                            }
                            //获取当前创建的所有歌单
                            string[] creatSongMenuNum = number?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string item in creatSongMenuNum)
                            {
                                //如果是相等的 说明已收藏
                                if (item == mid.ToString())
                                    f = true;
                                else
                                    nowNumber = nowNumber + item + ",";
                            }
                            //如果是已收藏的
                            if (f)
                            {
                                if (isRM)
                                {
                                    //要删除掉
                                    u.U_CreatSongMenu = nowNumber;
                                    db.SaveChanges();
                                    return "删除成功";
                                }
                                else
                                {
                                    return "已收藏";
                                }
                            }
                            else
                            {
                                //如果是我创建的新歌单
                                //歌单收藏次数 +1
                                SongMenu songMenu = db.SongMenu.FirstOrDefault(m => m.M_Id == mid);
                                if (songMenu != null) songMenu.M_CollectCount += 1;
                                //新的歌单id
                                number += mid + ",";
                                u.U_CreatSongMenu = number;
                                db.SaveChanges();
                                return "创建成功";
                            }
                        }
                        //添加到我收藏的歌单列表
                        else if (flag == 2)
                        {
                            string number = u.U_CollectSongMenu;
                            if (number == null)
                                number = "";

                            //获取当前创建的所有歌单
                            string[] collectSongMenuNum = number?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string item in collectSongMenuNum)
                            {
                                //如果是相等的 说明已收藏
                                if (item == mid.ToString())
                                    f = true;
                                else
                                    nowNumber = nowNumber + item + ",";
                            }
                            //如果是已收藏的
                            if (f)
                            {
                                //要删除掉
                                if (isRM)
                                {
                                    u.U_CollectSongMenu = nowNumber;
                                    db.SaveChanges();
                                    return "删除成功";
                                }
                                else
                                {
                                    return "已收藏";
                                }
                            }
                            else
                            {
                                //歌单收藏次数 +1
                                SongMenu songMenu = db.SongMenu.FirstOrDefault(m => m.M_Id == mid);
                                if (songMenu != null) songMenu.M_CollectCount += 1;
                                //新的歌单id
                                number += mid + ",";
                                u.U_CollectSongMenu = number;
                                db.SaveChanges();
                                return "收藏成功";
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return "出现错误";
                    }
                }
                //如果当前用户不存在
                return "当前用户不存在";
            }
        }

        /// <summary>
        /// 根据歌单id，查询歌单基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static object SelSongMenuByID(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var s = (from m in db.SongMenu
                         join u in db.UserInfo
                         on m.M_UserId equals u.U_Id
                         join t in db.Type
                         on m.M_Type equals t.T_Id
                         where m.M_Id == id
                         select new
                         {
                             M_Id = m.M_Id,
                             M_Name = m.M_Name,
                             M_Img = m.M_Img,
                             M_Info = m.M_Info,
                             M_CollectCount = m.M_CollectCount,
                             M_PlayCount = m.M_PlayCount,
                             M_Type = m.M_Type,
                             T_TypeName = t.T_Name,
                             M_UserId = m.M_UserId,
                             U_Name = u.U_Name,
                             U_Img = u.U_Img,
                             M_CreatTime = m.M_CreatTime,
                             M_SongId = m.M_SongId
                         }).ToList().FirstOrDefault();
                return s;
            }
        }


        /// <summary>
        /// 获取主评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<CommModel> GetSongMenuComm(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var comm = (from c in db.MenuComment
                            join u1 in db.UserInfo
                            on c.SM_From_User equals u1.U_Id
                            join u2 in db.UserInfo
                            on c.SM_To_User equals u2.U_Id
                            where c.SM_Menu == id
                            select new
                            {
                                SM_Id = c.SM_Id,
                                SM_Pid = c.SM_Pid,
                                SM_Content = c.SM_Content,
                                SM_From_User = c.SM_From_User,
                                From_UserInfo = new
                                {
                                    U_Id = u1.U_Id,
                                    U_Img = u1.U_Img,
                                    U_Info = u1.U_Info,
                                    //U_Like = u1.U_Like,
                                    U_Name = u1.U_Name,
                                    U_Email = u1.U_Email
                                },
                                //SM_To_User = c.SM_To_User,
                                //To_UserInfo = new
                                //{
                                //    U_Id = u2.U_Id,
                                //    U_Img = u2.U_Img,
                                //    U_Info = u2.U_Info,
                                //    //U_Like = u2.U_Like,
                                //    U_Name = u2.U_Name,
                                //    U_Email = u2.U_Email
                                //},
                                SM_Menu = c.SM_Menu,
                                SM_UpTime = c.SM_UpTime
                            }).Where(m => m.SM_Pid == null).OrderByDescending(m => m.SM_UpTime).ToList();

                List<CommModel> list = new List<CommModel>();

                foreach (var item in comm)
                {
                    //创建一个评论对象 将linq查询的结果和该评论对应的子评论装入对象
                    list.Add(new CommModel()
                    {
                        SC_Id = item.SM_Id,
                        SC_Pid = item.SM_Pid,
                        SC_Content = item.SM_Content,
                        SC_From_User = item.SM_From_User,
                        //SC_To_User = item.SM_To_User,
                        SC_Song = item.SM_Menu,
                        SC_UpTime = item.SM_UpTime,
                        From_UserInfo = item.From_UserInfo,
                        //To_UserInfo = item.To_UserInfo,
                        //根据评论id获取子评论
                        ChildrenComm = GetChildrenComm(id, item.SM_Id)
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 获取子评论
        /// </summary>
        /// <param name="sid">歌曲id</param>
        /// <param name="pid">评论id</param>
        /// <returns></returns>
        internal static object GetChildrenComm(int mid, int pid)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var comm = (from c in db.MenuComment
                            join u1 in db.UserInfo
                            on c.SM_From_User equals u1.U_Id
                            join u2 in db.UserInfo
                            on c.SM_To_User equals u2.U_Id
                            where c.SM_Menu == mid
                            where c.SM_Pid == pid
                            select new
                            {
                                SM_Id = c.SM_Id,
                                SM_Pid = c.SM_Pid,
                                SM_Content = c.SM_Content,
                                SM_From_User = c.SM_From_User,
                                From_UserInfo = new
                                {
                                    U_Id = u1.U_Id,
                                    U_Img = u1.U_Img,
                                    U_Info = u1.U_Info,
                                    //U_Like = u1.U_Like,
                                    U_Name = u1.U_Name,
                                    U_Email = u1.U_Email
                                },
                                SM_To_User = c.SM_To_User,
                                To_UserInfo = new
                                {
                                    U_Id = u2.U_Id,
                                    U_Img = u2.U_Img,
                                    U_Info = u2.U_Info,
                                    //U_Like = u2.U_Like,
                                    U_Name = u2.U_Name,
                                    U_Email = u2.U_Email
                                },
                                SM_Menu = c.SM_Menu,
                                SM_UpTime = c.SM_UpTime
                            }).ToList();
                return comm;
            }
        }

    }
}