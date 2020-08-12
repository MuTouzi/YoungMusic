using System.Web.Http;
using WebAPI.DAL;

namespace WebAPI.Controllers
{
    public class SearchController : ApiController
    {
        /// <summary>
        /// 搜索框进行搜索
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public object SelSongListByName(string name, int top = 30, int page = 0)
        {
            return SongListService.SelSongListByName(name, top, page);
        }

        /// <summary>
        /// 根据歌曲类型查询歌曲
        /// </summary>
        /// <param name="id"></param>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public object SelSongListByType(int id, int top = 30, int page = 0)
        {
            return SongListService.SelSongListByTypeID(id, top, page);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public object SelSongListALL(int top = 30, int page = 0)
        {
            return SongListService.SelSongListALL(top, page);
        }


        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object SelTypeList()
        {
            return TypeService.SelTypeListAll();
        }
    }
}
