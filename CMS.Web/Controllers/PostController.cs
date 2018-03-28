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

            var item = new PostViewModel
            {
                Terms = termService.GetAllTerm()
            };
            return View(item);
        }

        [HttpPost]
        public ActionResult Create(PostViewModel postVM)
        {
            if (postVM.Image != null && postVM.Image.Length != 0)
            {
                postVM.FeaturedImageUrl = "~/Imagefiles/" + postVM.Image.FileName;
                postService.Upload(postVM.Image);
            }
            
            postVM.Author = User.Identity.Name;
            postVM.CreatedDate = DateTime.Now;
            
            var data = new PostTermViewModel
            {
                PostID = postVM.PostID,
                TermID = postVM.Termid
            };
           
            postService.Create(postVM,data);
            var item = new PostViewModel
            {
                Terms = termService.GetAllTerm()
            };
            return View(item);
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
        [HttpPost]
        public ActionResult Comment(PostViewModel postVM)
        {
            CommentViewModel commentVM = new CommentViewModel
            {
                PostID = postVM.PostID,
                Content = postVM.Content,
                CommentTime = DateTime.Now,
                IsApproved=postVM.CommentIsApproved,
                CommentedBy=postVM.Commentedby
            };

            commentService.Create(commentVM);

            return View();
        }
        public ActionResult Postview(int id)
        {
            var post = postService.GetPostByID(id);
            post.Comments = commentService.GetCommentByPost(id);
            post.Posts = postService.GetAllPost();
            post.Terms = termService.GetAllTerm();

            return View(post);
            
        }
        //[Authorize]
        public ActionResult DashBoard()
        {
            var post = postService.GetPostByAuthor(User.Identity.Name);

            return View(post);

        }

    }
}
