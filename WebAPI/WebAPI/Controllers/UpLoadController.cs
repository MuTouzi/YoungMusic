using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UpLoadController : ApiController
    {
        private string uri = "http://" + HttpContext.Current.Request.Url.Authority+ "/api";

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UserPhotoUpLoad()
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            int uid = int.Parse(request.Form["id"].ToString());
            string resultUrl = Upload(request.Files[0], "Photo");
            if (resultUrl == "")
                resultUrl = "文件过大，将文件大小控制在25M内";
            else
                //更新用户头像
                UserInfoService.UpUserPhoto(uid, resultUrl);
            string ResultJson = JsonConvert.SerializeObject(resultUrl);
            return ResultJson;
        }

        /// <summary>
        /// 更新歌单图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string SongMenuImgUpLoad()
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            int mid = int.Parse(request.Form["id"].ToString());
            string resultUrl = Upload(request.Files[0], "SongMenu");
            if (resultUrl == "")
                resultUrl = "文件过大，将文件大小控制在25M内";
            else
                //更新用户头像
                SongMenuService.UpSongMenuImg(mid, resultUrl);
            string ResultJson = JsonConvert.SerializeObject(resultUrl);
            return ResultJson;
        }

        /// <summary>
        /// 动态图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string DynamicImgsUpLoad()
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            string resultUrl = "";
            for (int i = 0; i < request.Files.Count; i++)
            {
                string url = Upload(request.Files[0], "DynamicImgs");
                if (url == "")
                {
                    resultUrl = "文件过大，将文件大小控制在25M内";
                    break;
                }
                else resultUrl += uri + url + ",";
            }

            string ResultJson = JsonConvert.SerializeObject(resultUrl);
            return ResultJson;
        }

        /// <summary>
        /// 音乐上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string MusicUpLoad()
        {
            try
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpRequestBase request = context.Request;//定义传统request对象
                                                          //string resultUrl = Upload(request.Files[0], "Music");
                SongList s = new SongList()
                {
                    WYID = "",
                    S_Name = request.Form["sname"].ToString(),
                    S_Type = 1,
                    //先上传文件 在添加到歌手表
                    S_Url = uri + Upload(request.Files[0], "Music"),
                    S_Cover = uri + Upload(request.Files[1], "MusicCover"),

                    //添加到数据库  这里直接接收返回的歌手id
                    S_Singer = SongListService.AddSinger(UserInfoService.SelUserInfoByID(int.Parse(request.Form["uid"].ToString()))),
                    S_PlayCount = 0,
                    S_CollectCount = 0,
                    S_UpTime = DateTime.Now,
                    S_Lyric = "",
                    S_Album = 0
                };
                if (SongListService.AddSongList(s))
                    return "添加成功";
                return "添加失败";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postedFile">上载的文件对象</param>
        /// <param name="file">文件要保存的文件夹路径  格式：Photo\\img\\</param>
        /// <returns></returns>
        private string Upload(HttpPostedFileBase postedFile, string file = "")
        {
            //string filepath = "images\\Goods\\";
            string path = GetMapPath(file);//UpLoad/images/goods/
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filelength = postedFile.ContentLength;
            string fileExt = GetPostfixStr(postedFile.FileName);
            var fileMaxSize = 31457280; //30M
            var fileName = Guid.NewGuid() + "." + fileExt; //返回的上传后的文件名
            string resultUrl = $"/UpLoad/{file}/" + fileName;//存入数据库的链接
            if (filelength <= fileMaxSize)
            {
                byte[] buffer = new byte[filelength];
                postedFile.InputStream.Read(buffer, 0, filelength);
                postedFile.SaveAs(path + "/" + fileName);
                return resultUrl;
            }
            throw new Exception("文件大小超出限定");
        }

        /// <summary>
        /// 获取文件格式名  文件后缀
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string GetPostfixStr(string filename)
        {
            int start = filename.LastIndexOf(".");
            //文件名后缀
            return filename.Substring(start + 1);
        }

        /// <summary>
        /// 获取服务器根目录
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                //服务器物理路径
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
    }
}
