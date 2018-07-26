﻿using Newtonsoft.Json;
using OAuthTwitterWrapper;
using OAuthTwitterWrapper.JsonTypes;
using Quartz;
using Suu.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suu.TwitterFetcher
{
    public class MyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //Console.WriteLine($"[{DateTime.UtcNow}] Welcome from MyJob!!!!");
            var twit = new OAuthTwitterWrapper.OAuthTwitterWrapper();
            var timeline = twit.GetMyTimeline();
            var result = JsonConvert.DeserializeObject<List<TimeLine>>(timeline);

            using (SuuEntities SuuContext = new SuuEntities())
            {
                var x = new TwitterMessage();
                x.Text = DateTime.Now.ToString();
                SuuContext.TwitterMessages.Add(x);
                SuuContext.SaveChanges();
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
