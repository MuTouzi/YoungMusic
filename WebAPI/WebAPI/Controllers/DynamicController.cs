using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class DynamicController : ApiController
    {

        /// <summary>
        /// 点赞数加一
        /// </summary>
        /// <param name="id">动态id</param>
        /// <returns></returns>
        [HttpGet]
        public string AddGoodCount(int id)
        {
            return DynamicService.AddGoodCount(id);
        }

        /// <summary>
        /// 获取用户关注的用户发布的动态个数
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetDynamicCount(int id)
        {
            return DynamicService.GetDynamicCount(id).ToString();
        }

        /// <summary>
        /// 获取动态信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Dictionary<string, object>> GetDynamicInfo(int uid, int top = 30, int page = 0)
        {
            return DynamicService.GetDynamicInfo(uid, top, page);
        }

        /// <summary>
        /// 获取自己发布的动态信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="top"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Dictionary<string, object>> GetMyDynamicInfo(int uid, int top = 30, int page = 0)
        {
            return DynamicService.GetMyDynamicInfo(uid, top, page);
        }

        /// <summary>
        /// 根据动态id 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CommModel> GetCommByDynID(int id)
        {
            return DynamicService.GetCommByDynID(id);
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string AddDynamicComment([FromBody] object json)
        {
            JObject j = JObject.Parse(json.ToString());
            //获取评论内容
            string did = j["did"].ToString();
            string pid = j["pid"].ToString();
            string content = j["content"].ToString();
            string from = j["from"].ToString();
            string to = j["to"].ToString();
            DynamicComment c = new DynamicComment();
            if (did != "") c.DC_Dynamic = int.Parse(did); else return "参数不完整";
            if (pid != "") c.DC_Pid = int.Parse(pid);
            if (content != "") c.DC_Content = content; else return "参数不完整";
            if (from != "") c.DC_From_User = int.Parse(from); else return "参数不完整";
            if (to != "") c.DC_To_User = int.Parse(to); else c.DC_To_User = int.Parse(from);
            c.DC_UpTime = DateTime.Now;
            return DynamicService.AddDynamicComment(c);
        }

        /// <summary>
        /// ！！！！！！！发布动态！！！！！！！
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddDynamic([FromBody] object json)
        {
            try
            {
                JObject j = JObject.Parse(json.ToString());
                //获取发布动态的内容
                string uid = j["userId"].ToString();
                string title = j["title"].ToString();
                string content = j["content"].ToString();
                string songID = j["songID"].ToString();
                string imgs = j["imgs"].ToString();
                Dynamic dynamic = new Dynamic();
                if (uid != "") dynamic.D_UserId = int.Parse(uid); else return "参数不完整";
                if (title != "") dynamic.D_Title = title; else dynamic.D_Title = "";
                if (content != "") dynamic.D_Content = content; else return "参数不完整";
                if (songID != "") dynamic.D_SongID = int.Parse(songID); else dynamic.D_SongID = null;
                if (imgs != "") dynamic.D_imgs = imgs; else dynamic.D_imgs = "";
                dynamic.D_GoodCount = 0;
                dynamic.D_UpTime = DateTime.Now;
                //将动态添加到数据库
                if (DynamicService.AddDynamic(dynamic))
                    //返回添加结果
                    return "发布成功";
                else
                    return "失败";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 删除动态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string RmDynamic(int id) {
           return DynamicService.RmDynamic(id);
        }
    }
}
