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
    
    public partial class MenuComment
    {
        public int SM_Id { get; set; }
        public Nullable<int> SM_Pid { get; set; }
        public int SM_Menu { get; set; }
        public int SM_From_User { get; set; }
        public string SM_Content { get; set; }
        public int SM_To_User { get; set; }
        public Nullable<System.DateTime> SM_UpTime { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual SongMenu SongMenu { get; set; }
        public virtual UserInfo UserInfo1 { get; set; }
    }
}
