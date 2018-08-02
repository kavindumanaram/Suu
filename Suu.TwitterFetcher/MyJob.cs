using Newtonsoft.Json;
using OAuthTwitterWrapper;
using OAuthTwitterWrapper.JsonTypes;
using Quartz;
using Suu.FrontEnd.Models;
using System;
using System.Collections.Generic;
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
            var SearchResponse = JsonConvert.DeserializeObject<Search>(sesarch);

            using (SuuEntities SuuContext = new SuuEntities())
            {
                for (int a = 0; a < SearchResponse.Results.Count; a++)
                {

                    var results = SearchResponse.Results;
                    var status = new FrontEnd.Models.Status()
                    {
                        Id = results[a].id,
                        created_at = results[a].CreatedAt,
                        text = results[a].Text,
                        truncated = results[a].truncated,
                        //entities
                        //metadata
                        source = results[a].source,
                        in_reply_to_status_id = results[a].in_reply_to_status_id,
                        in_reply_to_user_id = results[a].in_reply_to_user_id,
                        in_reply_to_screen_name = results[a].in_reply_to_screen_name,
                        //user
                        geo = results[a].geo,
                        coordinates = results[a].coordinates,
                        place = results[a].place,
                        contributors = results[a].contributors,
                        is_quote_status = results[a].is_quote_status,
                        retweet_count = results[a].retweet_count,
                        favorite_count = results[a].favorite_count,
                        favorited = results[a].favorited,
                        retweeted = results[a].retweeted,
                        lang = results[a].lang,
                        User = new FrontEnd.Models.User()
                        {
                            name = results[a].User.Name,
                            Id = results[a].User.Id
                        }
                    };

                    var UserIdList = SuuContext.Users.ToList().Select(s => s.Id);
                    var StatusList = SuuContext.Status.ToList().Select(s => s.Id);
                    if (!StatusList.Contains(status.Id))
                    {
                        if (UserIdList.Contains(status.User.Id))
                        {
                            status.user_id = status.User.Id;
                            status.User = null;

                        }
                        SuuContext.Status.Add(status);
                        SuuContext.SaveChanges();
                    }
                }
            }
            Console.ReadLine();
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

    }


}
