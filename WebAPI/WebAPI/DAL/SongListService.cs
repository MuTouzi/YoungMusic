using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.DAL
{
    /// <summary>
    /// SongList 歌曲列表管理
    /// </summary>
    public class SongListService
    {
        /// <summary>
        /// 添加一个歌手信息 => 名字
        /// </summary>
        /// <param name="name">歌手名</param>
        /// <returns>歌手id</returns>
        public static int AddSinger(string name)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                Singer s = db.Singer.FirstOrDefault(m => m.S_Name == name);
                if (s == null)
                {
                    db.Singer.Add(new Singer()
                    {
                        S_Name = name
                    });
                    db.SaveChanges();
                }
                s = db.Singer.FirstOrDefault(m => m.S_Name == name);
                return s.S_Id;
            }
        }

        /// <summary>
        /// 将用户信息添加到歌手
        /// </summary>
        /// <param name="u">用户对象</param>
        /// <returns></returns>
        public static int AddSinger(UserInfo u)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                Singer s = db.Singer.FirstOrDefault(m => m.S_Name == u.U_Name);
                if (s == null)
                {
                    db.Singer.Add(new Singer()
                    {
                        S_Name = u.U_Name,
                        S_Img = u.U_Img,
                        S_Info = u.U_Info
                    });
                    db.SaveChanges();
                }
                s = db.Singer.FirstOrDefault(m => m.S_Name == u.U_Name);
                return s.S_Id;
            }
        }

        /// <summary>
        /// 根据传进来的歌单id数组 查询对应的歌曲信息
        /// </summary>
        /// <param name="songMenuNum"></param>
        /// <returns></returns>
        internal static object SelSongListByMenu(string[] songMenuNum)
        {
            if (songMenuNum == null) return null;
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                //将歌曲id转成list 使用Contains去查询包含该id的歌曲信息
                List<int> list = new List<int>();
                foreach (string item in songMenuNum)
                {
                    list.Add(int.Parse(item));
                }
                var song = (from s in db.SongList
                            join g in db.Singer
                            on s.S_Singer equals g.S_Id
                            select new
                            {
                                sID = s.S_Id,//歌曲ID
                                sName = s.S_Name,//歌曲名称
                                sUrl = s.S_Url,//歌曲播放地址
                                sCover = s.S_Cover,//歌曲封面
                                sPlayCount = s.S_PlayCount,//播放次数
                                sCollectCount = s.S_CollectCount,//收藏次数
                                sUpTime = s.S_UpTime,//上传时间
                                sLyric = s.S_Lyric,//歌词
                                sAlbumID = s.S_Album,//专辑id
                                wyid = s.WYID,//网易ID
                                gID = s.S_Singer,
                                gName = g.S_Name,
                            }).Where(m => list.Contains(m.sID)).ToList();
                return song;
            }

        }

        /// <summary>
        /// 根据用户id查询最近听的歌曲和听歌次数
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="maxLength">最近听歌前N项</param>
        /// <returns></returns>
        internal static Dictionary<string, object> SelRecentSong(int uid, int maxLength)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {

                Dictionary<string, object> dic = new Dictionary<string, object>();
                var u = db.UserInfo.FirstOrDefault(m => m.U_Id == uid);
                if (u != null)
                {
                    string[] number = u.U_Like?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //将歌曲id转成list 使用Contains去查询包含该id的歌曲信息
                    List<int> list = new List<int>();
                    Dictionary<string, int> dicCount = new Dictionary<string, int>();
                    if (number != null)
                    {
                        number = number.Reverse().ToArray();
                        int i = 0;
                        int j = 0;
                        while (j < maxLength && i < number.Length)
                        {
                            //查询是否有这个键 如果有取出来，没有返回默认值0
                            int c = dicCount.ContainsKey(number[i]) ? dicCount[number[i]] : 0;
                            if (c == 0)
                            {
                                //如果当前字典不存在这个键 去添加这个键 并在列表中添加
                                dicCount.Add(number[i], 1);
                                list.Add(int.Parse(number[i]));
                                j++;
                            }
                            else
                            {
                                //否则取出值进行+1
                                dicCount[number[i]] += 1;
                            }
                            i++;
                        }
                        //dic.Add("playCount", dicCount);
                        //dic.Add("songOrder", list);
                        dic.Add("songCoutn", toList(list, dicCount));
                        var song = (from s in db.SongList
                                    join g in db.Singer
                                    on s.S_Singer equals g.S_Id
                                    select new
                                    {
                                        sID = s.S_Id,//歌曲ID
                                        sName = s.S_Name,//歌曲名称
                                        sUrl = s.S_Url,//歌曲播放地址
                                        sCover = s.S_Cover,//歌曲封面
                                        sPlayCount = s.S_PlayCount,//播放次数
                                        sCollectCount = s.S_CollectCount,//收藏次数
                                        sSingID = s.S_Singer,
                                        gSingName = g.S_Name,
                                        sUpTime = s.S_UpTime,//上传时间
                                        sLyric = s.S_Lyric,//歌词
                                        sAlbumID = s.S_Album,//专辑id
                                        wyid = s.WYID,//网易ID
                                    }).Where(m => list.Contains(m.sID)).ToList();
                        dic.Add("songList", song);
                    }
                }
                return dic;
            }
        }

        private static object toList(List<int> list, Dictionary<string, int> dicCount)
        {
            var o = (from i in list
                     join d in dicCount
                     on i.ToString() equals d.Key
                     select new
                     {
                         id = i,
                         count = d.Value
                     }).ToList();
            return o;
        }

        /// <summary>
        /// 根据类型id查询歌曲列表  有分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        internal static object SelSongListByTypeID(int id, int top, int page)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var song = (from s in db.SongList
                            join g in db.Singer
                            on s.S_Singer equals g.S_Id
                            join t in db.Type
                            on s.S_Type equals t.T_Id
                            join a in db.Album
                            on s.S_Album equals a.A_Id into info
                            from i in info.DefaultIfEmpty()
                            where s.S_Type == id
                            select new
                            {
                                sID = s.S_Id,//歌曲ID
                                sName = s.S_Name,//歌曲名称
                                typeID = t.T_Id,//类型ID
                                typeName = t.T_Name,//类型名称
                                singerID = g.S_Id,//歌手ID
                                singerName = g.S_Name,//歌手名称
                                singerImg = g.S_Img,//歌手图片
                                sUrl = s.S_Url,//歌曲播放地址
                                sCover = s.S_Cover,//歌曲封面
                                sPlayCount = s.S_PlayCount,//播放次数
                                sCollectCount = s.S_CollectCount,//收藏次数
                                sUpTime = s.S_UpTime,//上传时间
                                sLyric = s.S_Lyric,//歌词
                                sAlbumID = s.S_Album,//专辑id
                                sAlbumName = i.A_Name,//专辑id
                                wyid = s.WYID,//网易ID
                            }).OrderBy(m => m.sID).Skip(top * page).Take(top).ToList();
                return song;
            }
        }

        /// <summary>
        /// 查询所有歌曲 有分页
        /// </summary>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        internal static object SelSongListALL(int top, int page)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var song = (from s in db.SongList
                            join g in db.Singer
                            on s.S_Singer equals g.S_Id
                            join t in db.Type
                            on s.S_Type equals t.T_Id
                            join a in db.Album
                            on s.S_Album equals a.A_Id into info
                            from i in info.DefaultIfEmpty()
                            select new
                            {
                                sID = s.S_Id,//歌曲ID
                                sName = s.S_Name,//歌曲名称
                                typeID = t.T_Id,//类型ID
                                typeName = t.T_Name,//类型名称
                                singerID = g.S_Id,//歌手ID
                                singerName = g.S_Name,//歌手名称
                                singerImg = g.S_Img,//歌手图片
                                sUrl = s.S_Url,//歌曲播放地址
                                sCover = s.S_Cover,//歌曲封面
                                sPlayCount = s.S_PlayCount,//播放次数
                                sCollectCount = s.S_CollectCount,//收藏次数
                                sUpTime = s.S_UpTime,//上传时间
                                sLyric = s.S_Lyric,//歌词
                                sAlbumID = s.S_Album,//专辑id
                                sAlbumName = i.A_Name,//专辑id
                                wyid = s.WYID,//网易ID
                            }).OrderBy(m => m.sID).Skip(top * page).Take(top).ToList();
                return song;
            }
        }

        /// <summary>
        /// 模糊查询 查询歌名是传入值或者歌手是传入值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static object SelSongListByName(string name, int top = 30, int page = 0)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var song = (from s in db.SongList
                            join g in db.Singer
                            on s.S_Singer equals g.S_Id
                            join t in db.Type
                            on s.S_Type equals t.T_Id
                            join a in db.Album
                            on s.S_Album equals a.A_Id into info
                            from i in info.DefaultIfEmpty()
                            select new
                            {
                                sID = s.S_Id,//歌曲ID
                                sName = s.S_Name,//歌曲名称
                                typeID = t.T_Id,//类型ID
                                typeName = t.T_Name,//类型名称
                                singerID = g.S_Id,//歌手ID
                                singerName = g.S_Name,//歌手名称
                                singerImg = g.S_Img,//歌手图片
                                sUrl = s.S_Url,//歌曲播放地址
                                sCover = s.S_Cover,//歌曲封面
                                sPlayCount = s.S_PlayCount,//播放次数
                                sCollectCount = s.S_CollectCount,//收藏次数
                                sUpTime = s.S_UpTime,//上传时间
                                sLyric = s.S_Lyric,//歌词
                                sAlbumID = s.S_Album,//专辑id
                                sAlbumName = i.A_Name,//专辑id
                                wyid = s.WYID,//网易ID
                            }).Where(m => m.singerName.Contains(name) || m.sName.Contains(name))
                            .OrderByDescending(m => m.sPlayCount).OrderByDescending(m => m.sCollectCount).Skip(top * page).Take(top).ToList();
                return song;
            }
        }

        /// <summary>
        /// 添加一个歌曲信息 如果当前歌曲不存在 就添加 如果存在 就不添加
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool AddSongList(SongList s)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                try
                {
                    SongList song = db.SongList.FirstOrDefault(m => m.S_Name == s.S_Name && m.S_Singer == s.S_Singer && m.S_Url == s.S_Url && m.S_Cover == s.S_Cover);
                    if (song == null)
                    {
                        db.SongList.Add(s);
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据歌曲ID 查歌曲详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object SelSongbyID(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var song = (from s in db.SongList
                            join g in db.Singer
                            on s.S_Singer equals g.S_Id
                            join t in db.Type
                            on s.S_Type equals t.T_Id
                            join a in db.Album
                            on s.S_Album equals a.A_Id into info
                            from i in info.DefaultIfEmpty()
                            where s.S_Id == id
                            select new
                            {
                                sID = s.S_Id,//歌曲ID
                                sName = s.S_Name,//歌曲名称
                                typeID = t.T_Id,//类型ID
                                typeName = t.T_Name,//类型名称
                                singerID = g.S_Id,//歌手ID
                                singerName = g.S_Name,//歌手名称
                                singerImg = g.S_Img,//歌手图片
                                sUrl = s.S_Url,//歌曲播放地址
                                sCover = s.S_Cover,//歌曲封面
                                sPlayCount = s.S_PlayCount,//播放次数
                                sCollectCount = s.S_CollectCount,//收藏次数
                                sUpTime = s.S_UpTime,//上传时间
                                sLyric = s.S_Lyric,//歌词
                                sAlbumID = s.S_Album,//专辑id
                                sAlbumName = i.A_Name,//专辑id
                                wyid = s.WYID,//网易ID
                            }).ToList();
                return song;
            }
        }

        /// <summary>
        /// 热门歌曲推荐
        /// </summary>
        /// <returns></returns>
        internal static object SelHotTop(int top = 10)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var list = (from s in db.SongList
                            orderby s.S_CollectCount descending
                            orderby s.S_PlayCount descending
                            select new
                            {
                                S_Id = s.S_Id,
                                S_Name = s.S_Name,
                                S_Album = s.S_Album,
                                S_CollectCount = s.S_CollectCount,
                                S_Url = s.S_Url,
                                S_Audit_State = s.S_Audit_State,
                                S_Cover = s.S_Cover,
                                S_Lyric = s.S_Lyric,
                                S_PlayCount = s.S_PlayCount,
                                S_Singer = s.S_Singer,
                                S_Type = s.S_Type,
                                S_UpTime = s.S_UpTime
                            }).Take(top).ToList();
                return list;
            }
        }

        /// <summary>
        /// 热门歌单推荐 不包括 ‘我喜欢的音乐’
        /// </summary>
        /// <returns></returns>
        internal static object SelSongListTop(int top = 10)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var list = (from s in db.SongMenu
                            orderby s.M_CollectCount descending
                            orderby s.M_PlayCount descending
                            where s.M_Name != "我喜欢的音乐"
                            select new
                            {
                                M_Id = s.M_Id,
                                M_Img = s.M_Img,
                                M_CollectCount = s.M_CollectCount,
                                M_CreatTime = s.M_CreatTime,
                                M_Info = s.M_Info,
                                M_Name = s.M_Name,
                                M_PlayCount = s.M_PlayCount,
                                M_SongId = s.M_SongId,
                                M_Type = s.M_Type,
                                M_UserId = s.M_UserId
                            }).Take(top).ToList();
                return list;
            }
        }

        /// <summary>
        /// 更改歌曲的播放次数和收藏次数
        /// </summary>
        /// <param name="id">歌曲id</param>
        /// <param name="flag">标识：1：播放次数+1 2：收藏次数+1</param>
        /// <returns></returns>
        internal static bool AddCount(int id, int flag = 1)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var s = db.SongList.FirstOrDefault(m => m.S_Id == id);
                if (s != null)
                {
                    if (flag == 1)
                    {
                        //播放次数+1
                        s.S_PlayCount += 1;
                        return db.SaveChanges() > 0;
                    }
                    else
                    {
                        //收藏次数+1
                        s.S_CollectCount += 1;
                        return db.SaveChanges() > 0;
                    }
                }
                return false;
            }
        }
    }
}