﻿using Microsoft.AspNetCore.Mvc;

namespace BlogAz.AdminPresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
