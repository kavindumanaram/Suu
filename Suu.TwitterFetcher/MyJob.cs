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

            foreach (var result in SearchResponse.Results)
            {


                Console.Write(result.Text + " —> " + result.place + " : " + result.id_str + "<br>");


            }



            //using (SuuEntities SuuContext = new SuuEntities())
            //{

            //    var UserIdList = SuuContext.Users.Select(u => u.Id);

            //    for (int a = 0; a < results.Count; a++)
            //    {
            //        //var blog = new FrontEnd.Models.Status()
            //        //{
            //        //    text = results[a].Text,
            //        //    Id = ConvertToLong(results[a].Id),
            //        //    User = new FrontEnd.Models.User()
            //        //    {
            //        //        name = results[a].User.Name,
            //        //        Id = results[a].User.Id
            //        //    }
            //        //};
            //        //SuuContext.Status.Add(blog);
            //        //SuuContext.SaveChanges();


            //        // ---------------------
            //       /* var status = new FrontEnd.Models.Status();
            //        status.text = results[a].Text;
            //        status.Id = ConvertToLong(results[a].Id);
            //        status.User.name = results[a].User.Name;
            //        status.User.Id = results[a].User.Id;
            //        SuuContext.Status.Add(status);
            //        SuuContext.SaveChanges();
            //        */


            //    }
            //}
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
