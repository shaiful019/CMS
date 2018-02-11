using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Domain.Models;
using CMS.Core.Interfaces;
using CMS.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Web.Controllers
{
    //[Authorize]
    public class PostController : Controller
    {
        private IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostViewModel postVM)
        {
            postVM.Author = User.Identity.Name;

            postService.Create(postVM);

            return View();
        }

        public ActionResult Edit(int id)
        {
            var post = postService.GetPostByID(id);

            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel postVM)
        {
            postVM.ModifiedBy = User.Identity.Name;
            postVM.ModifiedDate = DateTime.Now;

            postService.Update(postVM);

            return View();
        }
    }
}
