﻿using System.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using OAuthTwitterWrapper.JsonTypes;

namespace OAuthTwitterWrapper
{
	public class OAuthTwitterWrapper : IOAuthTwitterWrapper
    {
		public IAuthenticateSettings AuthenticateSettings { get; set; }
		public ITimeLineSettings TimeLineSettings { get; set; }
		public ISearchSettings SearchSettings { get; set; }

		/// <summary>
		/// The default constructor takes all the settings from the appsettings file
		/// </summary>
		public OAuthTwitterWrapper()
		{
			string oAuthConsumerKey = ConfigurationManager.AppSettings["oAuthConsumerKey"];
            string oAuthConsumerSecret = ConfigurationManager.AppSettings["oAuthConsumerSecret"];
            string oAuthUrl = ConfigurationManager.AppSettings["oAuthUrl"];
			AuthenticateSettings = new AuthenticateSettings { OAuthConsumerKey = oAuthConsumerKey, OAuthConsumerSecret = oAuthConsumerSecret, OAuthUrl = oAuthUrl };
			string screenname = ConfigurationManager.AppSettings["screenname"];
			string include_rts = ConfigurationManager.AppSettings["include_rts"];
			string exclude_replies = ConfigurationManager.AppSettings["exclude_replies"];
			int count = Convert.ToInt16(ConfigurationManager.AppSettings["count"]);
			string timelineFormat = ConfigurationManager.AppSettings["timelineFormat"];			
			TimeLineSettings = new TimeLineSettings
			{
				ScreenName = screenname,
				IncludeRts = include_rts,
				ExcludeReplies = exclude_replies,
				Count = count,
				TimelineFormat = timelineFormat
			};
			string searchFormat = ConfigurationManager.AppSettings["searchFormat"];
			string searchQuery = ConfigurationManager.AppSettings["searchQuery"];
            string resultType = "recent";
			string geoCode = "";//"5.954920,80.554956,1200mi";
            string lang = "si";
			string until = "";//DateTime.Now.ToString("yyyy-MM-dd");//"2018-08-28";

            SearchSettings = new SearchSettings
            {
                SearchFormat = searchFormat,
                SearchQuery = searchQuery,
                ResultType = resultType,
                GeoCode = geoCode,
                Lang = lang,
                Count = count,
                Until = until,
								            };
				
		}

		/// <summary>
		/// This allows the authentications settings to be passed in
		/// </summary>
		/// <param name="authenticateSettings"></param>
		public OAuthTwitterWrapper(IAuthenticateSettings authenticateSettings)
		{
			AuthenticateSettings = authenticateSettings;
		}

		/// <summary>
		/// This allows the authentications and timeline settings to be passed in
		/// </summary>
		/// <param name="authenticateSettings"></param>
		/// <param name="timeLineSettings"></param>
		public OAuthTwitterWrapper(IAuthenticateSettings authenticateSettings, ITimeLineSettings timeLineSettings)
		{
			AuthenticateSettings = authenticateSettings;
			TimeLineSettings = timeLineSettings;
		}

		/// <summary>
		/// This allows the authentications, timeline and search settings to be passed in
		/// </summary>
		/// <param name="authenticateSettings"></param>
		/// <param name="timeLineSettings"></param>
		/// <param name="searchSettings"></param>
		public OAuthTwitterWrapper(IAuthenticateSettings authenticateSettings, ITimeLineSettings timeLineSettings, ISearchSettings searchSettings)
		{
			AuthenticateSettings = authenticateSettings;
			TimeLineSettings = timeLineSettings;
			SearchSettings = searchSettings;
		}

        public string GetMyTimeline()
        {
			var timeLineJson = string.Empty;
			IAuthenticate authenticate = new Authenticate();
			AuthResponse twitAuthResponse = authenticate.AuthenticateMe(AuthenticateSettings);

            // Do the timeline
			var utility = new Utility();
			timeLineJson = utility.RequstJson(TimeLineSettings.TimelineUrl, twitAuthResponse.TokenType, twitAuthResponse.AccessToken);
            
            return timeLineJson;
        }

		public string GetSearch()
		{
			var searchJson = string.Empty;
			IAuthenticate authenticate = new Authenticate();
			AuthResponse twitAuthResponse = authenticate.AuthenticateMe(AuthenticateSettings);

			// Do the timeline
			var utility = new Utility();
			searchJson = utility.RequstJson(SearchSettings.SearchUrl, twitAuthResponse.TokenType, twitAuthResponse.AccessToken);

			return searchJson;
		}

		public string GetSearch(string sinceId)
		{
			var searchJson = string.Empty;
			IAuthenticate authenticate = new Authenticate();
			AuthResponse twitAuthResponse = authenticate.AuthenticateMe(AuthenticateSettings);

			// Do the search
			var utility = new Utility();
			searchJson = utility.RequstJson($"{SearchSettings.SearchUrl}&since_id={sinceId}", twitAuthResponse.TokenType, twitAuthResponse.AccessToken);

			return searchJson;
		}
	}
}
