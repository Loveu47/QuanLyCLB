//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCLB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThanhVien
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string MSSV { get; set; }
        public string SDT { get; set; }
        public string NganhHoc { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<int> ToChucId { get; set; }
        public Nullable<int> BanId { get; set; }
        public string ChucVu { get; set; }
    
        public virtual Ban Ban { get; set; }
        public virtual ToChuc ToChuc { get; set; }
    }
}
