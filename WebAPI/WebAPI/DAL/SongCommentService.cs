using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.DAL
{
    /// <summary>
    /// 歌曲评论操作
    /// </summary>
    public class SongCommentService
    {
        /// <summary>
        /// 获取主评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<CommModel> SelCommentBySongID(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var comm = (from c in db.SongComment
                            join u1 in db.UserInfo
                            on c.SC_From_User equals u1.U_Id
                            join u2 in db.UserInfo
                            on c.SC_To_User equals u2.U_Id
                            where c.SC_Song == id
                            select new
                            {
                                SC_Id = c.SC_Id,
                                SC_Pid = c.SC_Pid,
                                SC_Content = c.SC_Content,
                                SC_From_User = c.SC_From_User,
                                From_UserInfo = new
                                {
                                    U_Id = u1.U_Id,
                                    U_Img = u1.U_Img,
                                    U_Info = u1.U_Info,
                                    //U_Like = u1.U_Like,
                                    U_Name = u1.U_Name,
                                    U_Email = u1.U_Email
                                },
                                //SC_To_User = c.SC_To_User,
                                //To_UserInfo = new
                                //{
                                //    U_Id = u2.U_Id,
                                //    U_Img = u2.U_Img,
                                //    U_Info = u2.U_Info,
                                //    //U_Like = u2.U_Like,
                                //    U_Name = u2.U_Name,
                                //    U_Email = u2.U_Email
                                //},
                                SC_Song = c.SC_Song,
                                SC_UpTime = c.SC_UpTime
                            }).Where(m => m.SC_Pid == null).OrderByDescending(m => m.SC_UpTime).ToList();

                List<CommModel> list = new List<CommModel>();

                foreach (var item in comm)
                {
                    //创建一个评论对象 将linq查询的结果和该评论对应的子评论装入对象
                    list.Add(new CommModel()
                    {
                        SC_Id = item.SC_Id,
                        SC_Pid = item.SC_Pid,
                        SC_Content = item.SC_Content,
                        SC_From_User = item.SC_From_User,
                        //SC_To_User = item.SC_To_User,
                        SC_Song = item.SC_Song,
                        SC_UpTime = item.SC_UpTime,
                        From_UserInfo = item.From_UserInfo,
                        //To_UserInfo = item.To_UserInfo,
                        //根据评论id获取子评论
                        ChildrenComm = GetChildrenComm(id, item.SC_Id)
                    });
                }
                return list;
            }
        }


        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static string AddSongComment(SongComment c)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                db.SongComment.Add(c);
                if (db.SaveChanges() > 0) return "发布成功"; else return "发布失败";
            }
        }

        /// <summary>
        /// 获取子评论
        /// </summary>
        /// <param name="sid">歌曲id</param>
        /// <param name="pid">评论id</param>
        /// <returns></returns>
        internal static object GetChildrenComm(int sid, int pid)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var comm = (from c in db.SongComment
                            join u1 in db.UserInfo
                            on c.SC_From_User equals u1.U_Id
                            join u2 in db.UserInfo
                            on c.SC_To_User equals u2.U_Id
                            where c.SC_Song == sid
                            where c.SC_Pid == pid
                            select new
                            {
                                SC_Id = c.SC_Id,
                                SC_Pid = c.SC_Pid,
                                SC_Content = c.SC_Content,
                                SC_From_User = c.SC_From_User,
                                From_UserInfo = new
                                {
                                    U_Id = u1.U_Id,
                                    U_Img = u1.U_Img,
                                    U_Info = u1.U_Info,
                                    //U_Like = u1.U_Like,
                                    U_Name = u1.U_Name,
                                    U_Email = u1.U_Email
                                },
                                SC_To_User = c.SC_To_User,
                                To_UserInfo = new
                                {
                                    U_Id = u2.U_Id,
                                    U_Img = u2.U_Img,
                                    U_Info = u2.U_Info,
                                    //U_Like = u2.U_Like,
                                    U_Name = u2.U_Name,
                                    U_Email = u2.U_Email
                                },
                                SC_Song = c.SC_Song
                            }).ToList();
                return comm;
            }
        }

    }
}