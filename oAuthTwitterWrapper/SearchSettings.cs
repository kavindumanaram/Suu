using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAuthTwitterWrapper
{
	public class SearchSettings : ISearchSettings
	{
		public string SearchFormat { get; set; }
		public string SearchQuery { get; set; }
        public string ResultType { get; set; }
        public string GeoCode { get; set; }
        public string Lang { get; set; }
        public int Count { get; set; }
        public string Until { get; set; }

        public string SearchUrl {
			get { return string.Format(SearchFormat, SearchQuery, ResultType, GeoCode, Lang, Count, Until); }  
		}
	}
}
