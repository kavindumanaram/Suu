//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Suu.FrontEnd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Status()
        {
            this.EntityHashtags = new HashSet<EntityHashtag>();
        }
    
        public long Id { get; set; }
        public string created_at { get; set; }
        public string text { get; set; }
        public Nullable<bool> truncated { get; set; }
        public string source { get; set; }
        public Nullable<long> in_reply_to_status_id { get; set; }
        public Nullable<long> in_reply_to_user_id { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public string geo { get; set; }
        public string coordinates { get; set; }
        public string place { get; set; }
        public string contributors { get; set; }
        public string retweeted_status { get; set; }
        public Nullable<bool> is_quote_status { get; set; }
        public Nullable<int> retweet_count { get; set; }
        public Nullable<int> favorite_count { get; set; }
        public Nullable<bool> favorited { get; set; }
        public Nullable<bool> retweeted { get; set; }
        public string lang { get; set; }
        public Nullable<bool> possibly_sensitive { get; set; }
        public Nullable<long> user_id { get; set; }
        public Nullable<int> is_count { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityHashtag> EntityHashtags { get; set; }
        public virtual User User { get; set; }
    }
}
