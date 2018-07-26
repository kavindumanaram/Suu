﻿using OAuthTwitterWrapper;
using Quartz;
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
            Console.WriteLine(timeline);
            Console.ReadLine();
        }
    }
}
