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
    
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            this.Dynamic = new HashSet<Dynamic>();
            this.DynamicComment = new HashSet<DynamicComment>();
            this.DynamicComment1 = new HashSet<DynamicComment>();
            this.MenuComment = new HashSet<MenuComment>();
            this.MenuComment1 = new HashSet<MenuComment>();
            this.SongComment = new HashSet<SongComment>();
            this.SongComment1 = new HashSet<SongComment>();
            this.SongMenu = new HashSet<SongMenu>();
        }
    
        public int U_Id { get; set; }
        public string U_Name { get; set; }
        public string U_Pwd { get; set; }
        public string U_Email { get; set; }
        public string U_Info { get; set; }
        public string U_Tell { get; set; }
        public string U_Img { get; set; }
        public string U_Hobby { get; set; }
        public string U_Fans { get; set; }
        public string U_Follow { get; set; }
        public string U_CollectSongMenu { get; set; }
        public string U_CreatSongMenu { get; set; }
        public string U_Like { get; set; }
        public string U_Gender { get; set; }
        public Nullable<System.DateTime> U_Birthday { get; set; }
        public Nullable<System.DateTime> U_RegistrationTime { get; set; }
        public int IsAdmin { get; set; }
        public int IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dynamic> Dynamic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DynamicComment> DynamicComment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DynamicComment> DynamicComment1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuComment> MenuComment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuComment> MenuComment1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SongComment> SongComment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SongComment> SongComment1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SongMenu> SongMenu { get; set; }
    }
}
