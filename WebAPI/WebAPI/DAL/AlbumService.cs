using System.Linq;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class AlbumService
    {
        /// <summary>
        /// 获取专辑信息 有分页
        /// </summary>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static object GetAlbums(int top, int page)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var list = (from a in db.Album
                            join s in db.Singer
                            on a.A_SingerId equals s.S_Id
                            select new
                            {
                                A_Id = a.A_Id,
                                A_Img = a.A_Img,
                                A_Info = a.A_Info,
                                A_Name = a.A_Name,
                                A_PlayCount = a.A_PlayCount,
                                A_SingerId = a.A_SingerId,
                                SingerInfo = new
                                {
                                    S_Id = s.S_Id,
                                    S_Img = s.S_Img,
                                    S_Name = s.S_Name,
                                    S_Info = s.S_Info
                                },
                                A_Song = a.A_Song,
                                A_Time = a.A_Time
                            }).OrderByDescending(x => x.A_PlayCount).Skip(top * page).Take(top).ToList();
                return list;
            }

        }

        /// <summary>
        /// 根据专辑id查询专辑基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static object GetAlbumByID(int id)
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                var a = (from m in db.Album
                         join s in db.Singer
                         on m.A_SingerId equals s.S_Id
                         where m.A_Id == id
                         select new
                         {
                             A_Id = m.A_Id,
                             A_Name = m.A_Name,
                             A_Img = m.A_Img,
                             A_Info = m.A_Info,
                             A_PlayCount = m.A_PlayCount,
                             A_SingerId = m.A_SingerId,
                             SingerInfo = new
                             {
                                 S_Id = s.S_Id,
                                 S_Name = s.S_Name,
                                 S_Img = s.S_Img,
                                 S_Info = s.S_Info
                             },
                             A_Time = m.A_Time,
                             A_Song = m.A_Song
                         }).ToList().FirstOrDefault();
                return a;
            }
        }
    }
}