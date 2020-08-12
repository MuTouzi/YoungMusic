using System;

namespace WebAPI.Models
{
    public class CommModel
    {
        public int SC_Id { get; set; }
        public Nullable<int> SC_Pid { get; set; }
        public int SC_Song { get; set; }
        public int SC_From_User { get; set; }
        public string SC_Content { get; set; }
        public int SC_To_User { get; set; }
        public Nullable<System.DateTime> SC_UpTime { get; set; }
        public object From_UserInfo { get; set; }
        public object To_UserInfo { get; set; }
        public object ChildrenComm { get; set; }
    }
}