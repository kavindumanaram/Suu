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
                    var blog = new FrontEnd.Models.Status()
                    {
                        text = results[a].Text,
                        Id = results[a].id,
                        User = new FrontEnd.Models.User()
                        {
                            name = results[a].User.Name,
                            Id = results[a].User.Id
                        }
                    };

                    var UserIdList = SuuContext.Users.ToList().Select(s => s.Id);
                    var StatusList = SuuContext.Status.ToList().Select(s => s.Id);
                    if (!StatusList.Contains(blog.Id))
                    {
                        if (UserIdList.Contains(blog.User.Id))
                        {
                            blog.user_id = blog.User.Id;
                            blog.User = null;

                        }
                        SuuContext.Status.Add(blog);
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
