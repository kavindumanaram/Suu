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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Status = new HashSet<Status>();
        }
    
        public long Id { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public Nullable<bool> @protected { get; set; }
        public Nullable<int> followers_count { get; set; }
        public Nullable<int> friends_count { get; set; }
        public Nullable<int> listed_count { get; set; }
        public string created_at { get; set; }
        public Nullable<int> favourites_count { get; set; }
        public string utc_offset { get; set; }
        public string time_zone { get; set; }
        public Nullable<bool> geo_enabled { get; set; }
        public Nullable<bool> verified { get; set; }
        public Nullable<int> statuses_count { get; set; }
        public string lang { get; set; }
        public Nullable<bool> contributors_enabled { get; set; }
        public Nullable<bool> is_translator { get; set; }
        public Nullable<bool> is_translation_enabled { get; set; }
        public string profile_background_color { get; set; }
        public string profile_background_image_url { get; set; }
        public string profile_background_image_url_https { get; set; }
        public Nullable<bool> profile_background_tile { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_banner_url { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public Nullable<bool> profile_use_background_image { get; set; }
        public Nullable<bool> has_extended_profile { get; set; }
        public Nullable<bool> default_profile { get; set; }
        public Nullable<bool> default_profile_image { get; set; }
        public Nullable<bool> following { get; set; }
        public Nullable<bool> follow_request_sent { get; set; }
        public Nullable<bool> notifications { get; set; }
        public string translator_type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Status> Status { get; set; }
    }
}
