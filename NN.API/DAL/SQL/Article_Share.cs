//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article_Share
    {
        public long ID { get; set; }
        public long ArticleID { get; set; }
        public string UserID { get; set; }
        public System.DateTime DateOfShare { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
