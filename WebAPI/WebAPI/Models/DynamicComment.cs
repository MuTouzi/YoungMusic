//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DynamicComment
    {
        public int DC_Id { get; set; }
        public Nullable<int> DC_Pid { get; set; }
        public int DC_Dynamic { get; set; }
        public int DC_From_User { get; set; }
        public string DC_Content { get; set; }
        public int DC_To_User { get; set; }
        public Nullable<System.DateTime> DC_UpTime { get; set; }
    
        public virtual Dynamic Dynamic { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual UserInfo UserInfo1 { get; set; }
    }
}
