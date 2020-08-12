using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class DynamicService
    {
        /// <summary>
        /// 动态点赞
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static string AddGoodCount(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var d = db.Dynamic.FirstOrDefault(x => x.D_Id == id);
                d.D_GoodCount += 1;
                if (db.SaveChanges() > 0) return "点赞成功";
                else return "失败";
            }
        }

        /// <summary>
        /// 获取动态个数
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        internal static int GetDynamicCount(int uid)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var user = db.UserInfo.FirstOrDefault(x => x.U_Id == uid);
                if (user == null) return 0;

                var dyn = (from d in db.Dynamic
                           join u in db.UserInfo
                           on d.D_UserId equals u.U_Id
                           select new
                           {
                               D_Id = d.D_Id,
                               D_Title = d.D_Title,
                               D_UserId = d.D_UserId,
                               D_Content = d.D_Content,
                               D_GoodCount = d.D_GoodCount,
                               D_imgs = d.D_imgs,
                               D_SongID = d.D_SongID,
                               D_UpTime = d.D_UpTime,
                               U_info = new
                               {
                                   U_Id = u.U_Id,
                                   U_Img = u.U_Img,
                                   U_Info = u.U_Info,
                                   U_Like = u.U_Like,
                                   U_Name = u.U_Name,
                                   //U_Email = u.U_Email,
                                   //U_Birthday = u.U_Birthday,
                                   //U_Tell = u.U_Tell,
                                   U_Fans = u.U_Fans,
                                   U_Follow = u.U_Follow,
                                   //U_Gender = u.U_Gender,
                                   //U_Hobby = u.U_Hobby,
                                   //U_RegistrationTime = u.U_RegistrationTime,
                                   //U_CollectSongMenu = u.U_CollectSongMenu,
                                   //U_CreatSongMenu = u.U_CreatSongMenu
                               }
                           }).Where(m => m.D_UserId==uid).ToList();
                return dyn.Count;
            }
        }

        /// <summary>
        /// 获取自己的动态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        internal static List<Dictionary<string, object>> GetMyDynamicInfo(int uid, int top, int page)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var user = db.UserInfo.FirstOrDefault(x => x.U_Id == uid);
                if (user == null) return null;

                var dyn = (from d in db.Dynamic
                           join u in db.UserInfo
                           on d.D_UserId equals u.U_Id
                           select new
                           {
                               D_Id = d.D_Id,
                               D_Title = d.D_Title,
                               D_UserId = d.D_UserId,
                               D_Content = d.D_Content,
                               D_GoodCount = d.D_GoodCount,
                               D_imgs = d.D_imgs,
                               D_SongID = d.D_SongID,
                               D_UpTime = d.D_UpTime,
                               U_info = new
                               {
                                   U_Id = u.U_Id,
                                   U_Img = u.U_Img,
                                   U_Info = u.U_Info,
                                   //U_Like = u.U_Like,
                                   U_Name = u.U_Name,
                                   //U_Email = u.U_Email,
                                   //U_Birthday = u.U_Birthday,
                                   //U_Tell = u.U_Tell,
                                   U_Fans = u.U_Fans,
                                   U_Follow = u.U_Follow,
                                   //U_Gender = u.U_Gender,
                                   //U_Hobby = u.U_Hobby,
                                   //U_RegistrationTime = u.U_RegistrationTime,
                                   //U_CollectSongMenu = u.U_CollectSongMenu,
                                   //U_CreatSongMenu = u.U_CreatSongMenu
                               }
                           }).Where(m => m.D_UserId == uid).ToList().OrderByDescending(m => m.D_UpTime).Skip(top * page).Take(top);
                foreach (var item in dyn)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Dynamic", item);
                    if (item.D_SongID != null)
                    {
                        var song = SongListService.SelSongbyID((int)item.D_SongID);
                        dic.Add("Song", song);
                    }
                    else
                        dic.Add("Song", null);
                    result.Add(dic);
                }
                return result;
            }
        }

        /// <summary>
        /// 获取主评论
        /// </summary>
        /// <param name="id">动态id</param>
        /// <returns></returns>
        internal static List<CommModel> GetCommByDynID(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var comm = (from c in db.DynamicComment
                            join u1 in db.UserInfo
                            on c.DC_From_User equals u1.U_Id
                            join u2 in db.UserInfo
                            on c.DC_To_User equals u2.U_Id
                            where c.DC_Dynamic == id
                            select new
                            {
                                DC_Id = c.DC_Id,
                                DC_Pid = c.DC_Pid,
                                DC_Content = c.DC_Content,
                                DC_From_User = c.DC_From_User,
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
                                DC_Dynamic = c.DC_Dynamic,
                                DC_UpTime = c.DC_UpTime
                            }).Where(m => m.DC_Pid == null).OrderByDescending(m => m.DC_UpTime).ToList();

                List<CommModel> list = new List<CommModel>();

                foreach (var item in comm)
                {
                    //创建一个评论对象 将linq查询的结果和该评论对应的子评论装入对象
                    list.Add(new CommModel()
                    {
                        SC_Id = item.DC_Id,
                        SC_Pid = item.DC_Pid,
                        SC_Content = item.DC_Content,
                        SC_From_User = item.DC_From_User,
                        //SC_To_User = item.SM_To_User,
                        SC_Song = item.DC_Dynamic,
                        SC_UpTime = item.DC_UpTime,
                        From_UserInfo = item.From_UserInfo,
                        //To_UserInfo = item.To_UserInfo,
                        //根据评论id获取子评论
                        ChildrenComm = GetChildrenComm(id, item.DC_Id)
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 删除动态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static string RmDynamic(int id)
        {
            using (YoungMusicEntities db=new YoungMusicEntities())
            {
                var d=db.Dynamic.FirstOrDefault(x => x.D_Id == id);
                if (d != null)
                {
                    db.Dynamic.Remove(d);
                    if (db.SaveChanges() > 0) return "删除成功";
                    else return "删除失败";
                }
                else return "当前动态不存在";
            }
        }

        /// <summary>
        /// 获取子评论
        /// </summary>
        /// <param name="sid">动态id</param>
        /// <param name="pid">评论id</param>
        /// <returns></returns>
        internal static object GetChildrenComm(int did, int pid)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var comm = (from c in db.DynamicComment
                            join u1 in db.UserInfo
                             on c.DC_From_User equals u1.U_Id
                            join u2 in db.UserInfo
                            on c.DC_To_User equals u2.U_Id
                            where c.DC_Dynamic == did
                            where c.DC_Pid == pid
                            select new
                            {
                                SM_Id = c.DC_Id,
                                SM_Pid = c.DC_Pid,
                                SM_Content = c.DC_Content,
                                SM_From_User = c.DC_From_User,
                                From_UserInfo = new
                                {
                                    U_Id = u1.U_Id,
                                    U_Img = u1.U_Img,
                                    U_Info = u1.U_Info,
                                    //U_Like = u1.U_Like,
                                    U_Name = u1.U_Name,
                                    U_Email = u1.U_Email
                                },
                                SM_To_User = c.DC_To_User,
                                To_UserInfo = new
                                {
                                    U_Id = u2.U_Id,
                                    U_Img = u2.U_Img,
                                    U_Info = u2.U_Info,
                                    //U_Like = u2.U_Like,
                                    U_Name = u2.U_Name,
                                    U_Email = u2.U_Email
                                },
                                SM_Menu = c.DC_Dynamic,
                                SM_UpTime = c.DC_UpTime
                            }).ToList();
                return comm;
            }
        }

        /// <summary>
        /// 发布动态
        /// </summary>
        /// <param name="dynamic"></param>
        /// <returns></returns>
        internal static bool AddDynamic(Dynamic dynamic)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                db.Dynamic.Add(dynamic);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取所有动态的信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        internal static List<Dictionary<string, object>> GetDynamicInfo(int uid, int top, int page)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var user = db.UserInfo.FirstOrDefault(x => x.U_Id == uid);
                if (user == null || user.U_Follow == "" || user.U_Follow == null) return null;
                string[] followNum = user.U_Follow?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> list = new List<int>();
                foreach (string item in followNum)
                {
                    list.Add(int.Parse(item));
                }
                list.Add(uid);
                var dyn = (from d in db.Dynamic
                           join u in db.UserInfo
                           on d.D_UserId equals u.U_Id
                           select new
                           {
                               D_Id = d.D_Id,
                               D_Title = d.D_Title,
                               D_UserId = d.D_UserId,
                               D_Content = d.D_Content,
                               D_GoodCount = d.D_GoodCount,
                               D_imgs = d.D_imgs,
                               D_SongID = d.D_SongID,
                               D_UpTime = d.D_UpTime,
                               U_info = new
                               {
                                   U_Id = u.U_Id,
                                   U_Img = u.U_Img,
                                   U_Info = u.U_Info,
                                   //U_Like = u.U_Like,
                                   U_Name = u.U_Name,
                                   //U_Email = u.U_Email,
                                   //U_Birthday = u.U_Birthday,
                                   //U_Tell = u.U_Tell,
                                   U_Fans = u.U_Fans,
                                   U_Follow = u.U_Follow,
                                   //U_Gender = u.U_Gender,
                                   //U_Hobby = u.U_Hobby,
                                   //U_RegistrationTime = u.U_RegistrationTime,
                                   //U_CollectSongMenu = u.U_CollectSongMenu,
                                   //U_CreatSongMenu = u.U_CreatSongMenu
                               }
                           }).Where(m => list.Contains(m.D_UserId)).ToList().OrderByDescending(m => m.D_UpTime).Skip(top * page).Take(top);
                foreach (var item in dyn)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Dynamic", item);
                    if (item.D_SongID != null)
                    {
                        var song = SongListService.SelSongbyID((int)item.D_SongID);
                        dic.Add("Song", song);
                    }
                    else
                        dic.Add("Song", null);
                    result.Add(dic);
                }
                return result;
            }
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static string AddDynamicComment(DynamicComment c)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                db.DynamicComment.Add(c);
                if (db.SaveChanges() > 0) return "发布成功"; else return "发布失败";
            }
        }

    }
}