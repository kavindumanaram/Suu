﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Suu.FrontEnd.Models;
using System.Web.Services;
using Newtonsoft.Json;
using System.Globalization;

namespace Suu.FrontEnd.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            using (SuuEntities SuuContext = new SuuEntities())
            {
                ViewBag.Status = SuuContext.Status.ToList();

				var StatusMonthGroupByName = SuuContext.Status.GroupBy(s => s.created_at).Select(n => new
				{
					Name = n.Key,
				//	Count = n.Count()
				}).ToList().Take(30);

				var StatusMonthGroupByCount = SuuContext.Status.GroupBy(s => s.created_at).Select(n => new
				{
					//Name = n.Key,
						Count = n.Count()
				}).ToList().Take(30);

				//List<double?> rainValues = new List<double?> { 49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 1054.4 };
				//List<double?> temperatureValues = new List<double?> { 7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6 };

				//List<ColumnSeriesData> rainData = new List<ColumnSeriesData>();
				//List<SplineSeriesData> temperatureData = new List<SplineSeriesData>();

				//rainValues.ForEach(p => rainData.Add(new ColumnSeriesData { Y = p }));
				//temperatureValues.ForEach(p => temperatureData.Add(new SplineSeriesData { Y = p }));

				ViewData["TweetDataCount"] = $"[{string.Join(",", StatusMonthGroupByCount.Select(s => s.Count).ToList())}]";
				ViewData["TweetDataDate"] = $"['{string.Join("','", StatusMonthGroupByName.Select(s => s.Name).ToList())}']".ToString();

				//.FirstOrDefault().Select(x => x.created_at);
				//.Count();


				ViewBag.User = SuuContext.Users.OrderByDescending(x => x.count).Take(10).ToList();
                ViewBag.HashTag = SuuContext.Hashtags.OrderByDescending(x => x.count).Take(10).ToList();
                ViewBag.MessageWord = SuuContext.messageCounts.OrderByDescending(x => x.count).Take(10).ToList();
                return View();
            }
                
        }

        //public IEnumerable<Status> filtetStatus(int? hashTagId1)
        //{
        //    var hashTagId = 1029055333451223041;
        //    using (SuuEntities SuuContext = new SuuEntities())
        //    {
        //        var EntityHashtagObject = SuuContext.EntityHashtags.Where(s => s.Id == hashTagId).FirstOrDefault();
        //        var StatausObject = SuuContext.Status.Where(w => w.Id == EntityHashtagObject.Id).FirstOrDefault();

        //        return SuuContext.Status.Where(s => s.Id == StatausObject.Id).ToList();
        //    }
        //}

        [HttpGet] // can be HttpGet
        public JsonResult Test(int hashTagId)
        {

          //  var hashTagId = 1029055333451223041;
            //using (SuuEntities SuuContext = new SuuEntities())
            //{
            //    var EntityHashtagObject = SuuContext.EntityHashtags.Where(s => s.Id == hashTagId).FirstOrDefault();
            //    var StatausObject = SuuContext.Status.Where(w => w.Id == EntityHashtagObject.Id).FirstOrDefault();

            //    // return SuuContext.Status.Where(s => s.Id == StatausObject.Id).ToList();
            //    return Json(new { data = SuuContext.Status.Where(s => s.Id == StatausObject.Id).ToList() }, JsonRequestBehavior.AllowGet);

            //}

            IEnumerable<Status> status = null;
            using (SuuEntities SuuContext = new SuuEntities())
            {
                SuuContext.Configuration.LazyLoadingEnabled = false;
                //   status = SuuContext.Status.ToList() ;
                //  status = SuuContext.EntityHashtags.Where(x => x.hashtag_id == hashTagId).ToList();
                var entity = SuuContext.EntityHashtags.Where(x => x.hashtag_id == hashTagId).Select(s => s.status_id).ToList();
                status = SuuContext.Status.Where(s => entity.Contains(s.Id)).ToList();
                foreach (var statusObject in status)
                {
                    statusObject.User = new Models.User()
                    {
                       profile_image_url = SuuContext.Users.Where(s => s.Id == statusObject.user_id).Select(s => s.profile_image_url).FirstOrDefault(),
                       is_ready = SuuContext.Users.Where(s => s.Id == statusObject.user_id).Select(s => s.is_ready).FirstOrDefault(),
                    };
                }
            }
            var json = JsonConvert.SerializeObject(status);
            return Json(new { data = json }, JsonRequestBehavior.AllowGet);
        }

		/// <summary>
		/// Retrive top ten tweets.
		/// </summary>
		/// <param name="userId">The user id</param>
		/// <returns></returns>
		[HttpGet] 
		public JsonResult ReriveTopTenTweetForUser(int userId)
		{
			IEnumerable<Status> status = null;
			using (SuuEntities SuuContext = new SuuEntities())
			{
				SuuContext.Configuration.LazyLoadingEnabled = false;
				//var entity = SuuContext.EntityHashtags.Where(x => x.hashtag_id == hashTagId).Select(s => s.status_id).ToList();
				//var TopTenTweets = SuuContext.Status.Where(s => s.user_id = userId)
				var currentUserId = SuuContext.Users.Where(a => a.user_id == userId).Select(x => x.Id).FirstOrDefault();
				status = SuuContext.Status.Where(s => s.user_id == currentUserId).ToList();
				foreach (var statusObject in status)
				{
					statusObject.User = new Models.User()
					{
						profile_image_url = SuuContext.Users.Where(s => s.Id == statusObject.user_id).Select(s => s.profile_image_url).FirstOrDefault(),
						is_ready = SuuContext.Users.Where(s => s.Id == statusObject.user_id).Select(s => s.is_ready).FirstOrDefault(),
					};
				}
			}
			var json = JsonConvert.SerializeObject(status);
			return Json(new { data = json }, JsonRequestBehavior.AllowGet);
		}
	}
}