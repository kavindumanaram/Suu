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
    
    public partial class TwitterMessage1
    {
        public int TwitterMessage { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<int> Id { get; set; }
        public string Text { get; set; }
        public Nullable<bool> Truncated { get; set; }
        public Nullable<int> EntitiesId { get; set; }
        public Nullable<int> MetadataId { get; set; }
        public string Source { get; set; }
        public Nullable<int> InReplyToStatusId { get; set; }
        public Nullable<int> inReplyToUserId { get; set; }
        public string InReplyToScreenName { get; set; }
        public string UserId { get; set; }
        public string Geo { get; set; }
        public string Coordinates { get; set; }
        public string Place { get; set; }
        public string Contributors { get; set; }
        public Nullable<bool> IsQuoteStatus { get; set; }
        public Nullable<int> RetweetCount { get; set; }
    }
}