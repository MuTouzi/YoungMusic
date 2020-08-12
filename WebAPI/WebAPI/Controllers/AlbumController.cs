using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.DAL;

namespace WebAPI.Controllers
{
    public class AlbumController : ApiController
    {
        /// <summary>
        /// 获取前N条专辑 或者区间性的查询专辑
        /// </summary>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetAlbums(int top, int page = 0)
        {
            return AlbumService.GetAlbums(top, page);
        }

        /// <summary>
        /// 根据专辑id查询专辑所有信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetAlbumByID(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var albun = AlbumService.GetAlbumByID(id);
            if (albun != null)
            {
                string songID = albun.GetType().GetProperty("A_Song").GetValue(albun).ToString();
                string[] songMenuNum = songID?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var song = SongListService.SelSongListByMenu(songMenuNum);
                dic.Add("AlbumInfo", albun);
                dic.Add("SongList", song);
            }
            return dic;
        }

    }
}
