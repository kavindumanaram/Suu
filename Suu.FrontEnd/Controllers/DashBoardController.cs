using System;
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
				}).ToList().Take(30).OrderByDescending(m => m.Name);

				var StatusMonthGroupByCount = SuuContext.Status.GroupBy(s => s.created_at).Select(n => new
				{
					Name = n.Key,
					Count = n.Count()
				}).ToList().Take(30).OrderByDescending(m => m.Name);

				ViewData["TweetDataCount"] = $"[{string.Join(",", StatusMonthGroupByCount.Select(s => s.Count).ToList())}]";
				ViewData["TweetDataDate"] = $"['{string.Join("','", StatusMonthGroupByName.Select(s => s.Name).ToList())}']".ToString();

				String LastSyncDateTime = SuuContext.OrganizationSettings.Where(s => s.SettingName == "Organization.LastSyncDateTime").FirstOrDefault().SettingValue;

				ViewData["LastSyncDateTime"] = CalculateRelativeTime(DateFormatter(LastSyncDateTime));

				var FirstSyncDateTime = SuuContext.OrganizationSettings.Where(s => s.SettingName == "Organization.FirstSyncDateTime").FirstOrDefault();
				var TotalMonth = 0;
				if (!string.IsNullOrEmpty(FirstSyncDateTime.SettingValue))
				{
					//FirstSyncDateTime.SettingValue = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					TotalMonth = GetMonthsBetween(DateFormatter(FirstSyncDateTime.SettingValue), DateTime.UtcNow);
				}
				ViewData["TotalMonth"] = TotalMonth;


					//var LastSyncDateTime = SuuContext.OrganizationSettings.Where(s => s.SettingName == "Organization.LastSyncDateTime").FirstOrDefault();


				//.FirstOrDefault().Select(x => x.created_at);
				//.Count();

				ViewBag.User = SuuContext.Users.OrderByDescending(x => x.count).Take(10).ToList();
				ViewBag.AllUser = SuuContext.Users.OrderByDescending(x => x.count).Take(50).ToList();
				ViewBag.HashTag = SuuContext.Hashtags.OrderByDescending(x => x.count).Take(10).ToList();
        ViewBag.MessageWord = SuuContext.messageCounts.OrderByDescending(x => x.count).Take(10).ToList();
				ViewBag.UserCount = SuuContext.Users.Count();
				ViewBag.MessageCount = SuuContext.Status.Count();
				ViewBag.HashTagCount = SuuContext.Hashtags.Count();
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


		[HttpGet]
		public JsonResult ReriveUserCoordinates(int userId)
		{
			List<UserLocationCount> UserLocationCounts = null;
			using (SuuEntities SuuContext = new SuuEntities())
			{
				UserLocationCounts = SuuContext.UserLocationCounts.Where(m => !string.IsNullOrEmpty(m.lat) && !string.IsNullOrEmpty(m.lon)).ToList();
					};
			var json = JsonConvert.SerializeObject(UserLocationCounts);
			return Json(new { data = json }, JsonRequestBehavior.AllowGet);
		}

		private string CalculateRelativeTime(DateTime dt)
		{
			var ts = new TimeSpan(DateTime.UtcNow.Ticks - dt.Ticks);
			double delta = Math.Abs(ts.TotalSeconds);

			if (delta < 60)
			{
				return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
			}
			if (delta < 120)
			{
				return "a minute ago";
			}
			if (delta < 2700) // 45 * 60
			{
				return ts.Minutes + " minutes ago";
			}
			if (delta < 5400) // 90 * 60
			{
				return "an hour ago";
			}
			if (delta < 86400) // 24 * 60 * 60
			{
				return ts.Hours + " hours ago";
			}
			if (delta < 172800) // 48 * 60 * 60
			{
				return "yesterday";
			}
			if (delta < 2592000) // 30 * 24 * 60 * 60
			{
				return ts.Days + " days ago";
			}
			if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
			{
				int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
				return months <= 1 ? "one month ago" : months + " months ago";
			}
			int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
			return years <= 1 ? "one year ago" : years + " years ago";
		}

		/// <summary>
		/// The date formatter.
		/// </summary>
		/// <param name="dateString">the string date.</param>
		/// <returns></returns>
		private DateTime DateFormatter(string dateString)
		{
			if (string.IsNullOrEmpty(dateString))
			{
				throw new ArgumentException("dateString is null or empty.");
			}
			return DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
		}

		private static int GetMonthsBetween(DateTime from, DateTime to)
		{
			if (from > to) return GetMonthsBetween(to, from);

			var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

			if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
			{
				return monthDiff - 1;
			}
			else
			{
				return monthDiff;
			}
		}

	}
}