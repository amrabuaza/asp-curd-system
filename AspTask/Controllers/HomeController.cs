using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspTask.Models;
using AspTask.Services.Interfaces;

namespace AspTask.Controllers
{
    public class HomeController : Controller
    {
        #region Constructors
        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            this.postService = postService;
        }
        #endregion

        #region Fields
        private readonly IPostService postService;
        private readonly ILogger<HomeController> _logger;
        #endregion

        #region Actions
        public IActionResult Index()
        {
            IEnumerable<Post> posts = postService.GetActivePosts();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }// end class
}// end namespace
