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