using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace OAuthTwitterWrapper.JsonTypes
{
    public class Metadata
    {
        public string result_type { get; set; }
        public string iso_language_code { get; set; }
    }

    public class RetweetedStatus
    {
        public Metadata metadata { get; set; }
        public string created_at { get; set; }
        public Int64 id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public bool truncated { get; set; }
        public long? in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long? in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public User user { get; set; }
        public string geo { get; set; }
        public object coordinates { get; set; }
        public object place { get; set; }
        public object contributors { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public Entities entities { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public string lang { get; set; }
    }

    public class Status
    {
        public Metadata metadata { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        public Int64 id { get; set; }
        public string id_str { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        public string source { get; set; }
        public bool truncated { get; set; }
        public long? in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long? in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        public Geo geo { get; set; }
        public Coordinates coordinates { get; set; }
        public Place place { get; set; }
        public string contributors { get; set; }
        public bool is_quote_status { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public Entities entities { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public string lang { get; set; }
        public RetweetedStatus retweeted_status { get; set; }
        public bool? possibly_sensitive { get; set; }
    }

    public class SearchMetadata
    {
        public double completed_in { get; set; }
        public long max_id { get; set; }
        public string max_id_str { get; set; }
        public string next_results { get; set; }
        public string query { get; set; }
        public string refresh_url { get; set; }
        public int count { get; set; }
        public int since_id { get; set; }
        public string since_id_str { get; set; }
    }

    public class Place
    {
        public string id { get; set; }
        public string url { get; set; }
        public string place_type { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string country_code { get; set; }
        public string country { get; set; }
        public IList<object> contained_within { get; set; }
        public BoundingBox bounding_box { get; set; }
        public Attributes attributes { get; set; }
    }

    public class BoundingBox
    {
        public string type { get; set; }
        public IList<IList<IList<double>>> coordinates { get; set; }
    }

	public class Coordinates
	{
		public string type { get; set; }
		public IList<double> coordinates { get; set; }
	}

	public class Geo
	{
		public string type { get; set; }
		public IList<double> coordinates { get; set; }
	}

	//When using PowerTrack, 30-Day and Full-Archive Search APIs, and Volume Streams this hash is null. Example:
	public class Attributes
    {
    }

    public class Search
    {
        [JsonProperty("statuses")]
        public List<Status> Results { get; set; }
        public SearchMetadata search_metadata { get; set; }
    }

}


