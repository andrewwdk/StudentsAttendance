﻿using Microsoft.AspNet.Identity.EntityFramework;
using StudentsAttendanceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentsAttendanceWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "MyProfile");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}