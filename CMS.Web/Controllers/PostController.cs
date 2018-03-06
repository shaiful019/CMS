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
using System.IO;

namespace CMS.Web.Controllers
{
    //[Authorize]
    public class PostController : Controller
    {


        private IPostService postService;
        private ICommentService commentService;
        private ITermService termService;
        
        public IEnumerable<TermViewModel> Categories = new List<TermViewModel>();
        public IEnumerable<TermViewModel> Tags = new List<TermViewModel>();
        

        public PostController(IPostService _postService, ICommentService _commentService, ITermService _termService)
        {
            postService = _postService;
            commentService = _commentService;
            termService = _termService;

        }


        public ActionResult Index()
        {

            var item = postService.GetAllPost();
            
            return View(item);
        }

        public ActionResult Create()
        {

            Categories = termService.GetCatagory();
            ViewBag.allcatagories = Categories;
            Tags = termService.GetTags();
            ViewBag.alltags = Tags;
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostViewModel postVM)
        {
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Imagefiles/",
                        postVM.Image.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                postVM.Image.CopyTo(stream);
            }

            postVM.FeaturedImageUrl = "~/Imagefiles/"+ postVM.Image.FileName;
            postVM.Author = User.Identity.Name;
            postVM.CreatedDate = DateTime.Now;

            postService.Create(postVM);
            Categories = termService.GetCatagory();
            ViewBag.allcatagories = Categories;
            Tags = termService.GetTags();
            ViewBag.alltags = Tags;

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

            return View("Index");
        }
        //[HttpPost]
        public ActionResult Comment(Comment postVM)
        {
            //postVM.CommentTime = DateTime.Now;
            //commentService.Create(postVM);
            return View();
        }
        public ActionResult Postview(int id)
        {
            var post = postService.GetPostByID(id);

            return View(post);
            
        }
        [Authorize]
        public ActionResult PostByAuthor()
        {
            var post = postService.GetPostByAuthor(User.Identity.Name);

            return View(post);

        }

    }
}
