using Newtonsoft.Json;
using OAuthTwitterWrapper;
using OAuthTwitterWrapper.JsonTypes;
using Quartz;
using Suu.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Suu.TwitterFetcher
{
    public class MyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var twit = new OAuthTwitterWrapper.OAuthTwitterWrapper();
            var sesarch = twit.GetSearch();
			try
			{
				var SearchResponse = JsonConvert.DeserializeObject<Search>(sesarch);


            using (SuuEntities SuuContext = new SuuEntities())
            {
                for (int a = 0; a < SearchResponse.Results.Count; a++)
                {

                    var results = SearchResponse.Results;
                    if (results[a].Text.Trim().Substring(0, 2).ToString() != "RT")
                    {
                        var status = new FrontEnd.Models.Status()
                        {
                            Id = results[a].id,
                            created_at =  DateFormatter(results[a].CreatedAt) ,
                            text = results[a].Text,
                            truncated = results[a].truncated,
                            //entities
                            //metadata
                            source = results[a].source,
                            in_reply_to_status_id = results[a].in_reply_to_status_id,
                            in_reply_to_user_id = results[a].in_reply_to_user_id,
                            in_reply_to_screen_name = results[a].in_reply_to_screen_name,
                            //user
                            //geo = results[a].geo,
                           // coordinates = results[a].coordinates,
                         //   place = results[a].place ? results[a].place. : ,
                            contributors = results[a].contributors,
                            is_quote_status = results[a].is_quote_status,
                            retweet_count = results[a].retweet_count,
                            favorite_count = results[a].favorite_count,
                            favorited = results[a].favorited,
                            retweeted = results[a].retweeted,
                            lang = results[a].lang,
                            is_count = 0,
                            User = new FrontEnd.Models.User()
                            {
                                Id = results[a].User.Id,
                                name = results[a].User.Name,
                                screen_name = results[a].User.ScreenName,
                                location = results[a].User.Location,
                                description = results[a].User.Description,
                                url = results[a].User.Url,
                                //entities
                                ///->protected
                                followers_count = results[a].User.FollowersCount,
                                friends_count = results[a].User.FriendsCount,
                                listed_count = results[a].User.ListedCount,
                                created_at = DateFormatter(results[a].User.CreatedAt),
                                favourites_count = results[a].User.FavouritesCount,
                                utc_offset = results[a].User.UtcOffset,
                                time_zone = results[a].User.TimeZone,
                                geo_enabled = results[a].User.GeoEnabled,
                                verified = results[a].User.Verified,
                                statuses_count = results[a].User.StatusesCount,
                                lang = results[a].User.Lang,
                                contributors_enabled = results[a].User.ContributorsEnabled,
                                is_translator = results[a].User.IsTranslator,
                                is_translation_enabled = results[a].User.IsTranslationEnabled,
                                profile_background_color = results[a].User.ProfileBackgroundColor,
                                profile_background_image_url = results[a].User.ProfileBackgroundImageUrl,
                                profile_background_image_url_https = results[a].User.ProfileBackgroundImageUrlHttps,
                                // profile_banner_url = results[a].User.profile
                                profile_link_color = results[a].User.ProfileLinkColor,
                                profile_sidebar_border_color = results[a].User.ProfileSidebarBorderColor,
                                profile_sidebar_fill_color = results[a].User.ProfileSidebarFillColor,
                                profile_text_color = results[a].User.ProfileTextColor,
                                profile_use_background_image = results[a].User.ProfileUseBackgroundImage,
                                // has_extended_profile = results[a].User.has
                                default_profile = results[a].User.DefaultProfile,
                                default_profile_image = results[a].User.DefaultProfileImage,
                                following = results[a].User.Following,
                                follow_request_sent = results[a].User.FollowRequestSent,
                                notifications = results[a].User.Notifications,
                                //translator_type = results[a].User.tr
                                profile_image_url = results[a].User.ProfileImageUrl,
                                count = 1,
                                is_ready = 0
                            }
                        };






                        var UserIdList = SuuContext.Users.ToList().Select(s => s.Id);
                        var StatusList = SuuContext.Status.ToList().Select(s => s.Id);
                        var HashTagTextList = SuuContext.Hashtags.ToList().Select(s => s.text);
                        if (!StatusList.Contains(status.Id))
                        {
                            if (UserIdList.Contains(status.User.Id))
                            {
                                status.user_id = status.User.Id;
                                //status.User = null;
                                //if(string.IsNullOrEmpty(status.User.count)  )

                                var currentUser = SuuContext.Users.ToList().Where(s => s.Id == status.User.Id).FirstOrDefault();
                                var curentUserCurrentOccrence = currentUser.count;
                                var UserlatestOccurence = curentUserCurrentOccrence = curentUserCurrentOccrence + 1;
                                currentUser.count = UserlatestOccurence;
                                SuuContext.SaveChanges();

                                status.User = null;

                            }


                            foreach (var x in results[a].entities.Hashtags)
                            {
                                if (!HashTagTextList.Contains(x.Text))
                                {
                                    var EntityHashtag = new EntityHashtag()
                                    {
                                        status_id = status.Id,
                                        Hashtag = new FrontEnd.Models.Hashtag()
                                        {
                                            text = x.Text,
                                            count = 1
                                        },
                                        Status = null

                                    };

                                    SuuContext.EntityHashtags.Add(EntityHashtag);
                                }
                                else
                                {
                                    //var EntityHashtag = new EntityHashtag()
                                    //{
                                    //    status_id = status.Id,
                                    //    Hashtag = new FrontEnd.Models.Hashtag()
                                    //    {
                                    //        text = x.Text,
                                    //    },
                                    //    Status = null

                                    //};


                                    var currentHashTag = SuuContext.Hashtags.ToList().Where(s => s.text == x.Text).FirstOrDefault();
                                    var curentHashTagCurrentOccrence = currentHashTag.count;
                                    var HashTaglatestOccurence = curentHashTagCurrentOccrence = curentHashTagCurrentOccrence + 1;
                                    currentHashTag.count = HashTaglatestOccurence;
                                    //SuuContext.SaveChanges();

                                    var EntityHashtag = new EntityHashtag()
                                    {
                                        status_id = status.Id,
                                        hashtag_id = currentHashTag.Id
                                    };
                                    SuuContext.EntityHashtags.Add(EntityHashtag);


                                    // SuuContext.EntityHashtags.Add(EntityHashtag);
                                }
                            };

                            SuuContext.Status.Add(status);
                            SuuContext.SaveChanges();
                        }
                    }
                }
            }
			   Console.WriteLine("finish save to db cycle .........");
		}
			catch(Exception ex) {
				Console.WriteLine(ex.Message);
			}
}

        private long ConvertToLong(string x)
        {
            long temp;
            if (long.TryParse(x, out temp))
            {
                return temp;
            }

            return 0;
        }

        private string DateFormatter(string dateString)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                var date = DateTime.ParseExact(dateString,
                                       "ddd MMM dd HH:mm:ss '+0000' yyyy",
                                       CultureInfo.InvariantCulture);
                return date.ToString("dd/MM/yyyy");
			}
            else
            {
                return string.Empty;
            }
            
        }

    }


}
