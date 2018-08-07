using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Suu.FrontEnd.Models;

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
                ViewBag.User = SuuContext.Users.ToList();
                ViewBag.HashTag = SuuContext.Hashtags.ToList();
                return View();
            }
                
        }
    }
}