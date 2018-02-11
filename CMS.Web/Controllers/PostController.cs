﻿using System;
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
        private ICommentService commentService;



        public PostController(IPostService _postService, ICommentService _commentService)
        {
            postService = _postService;
            commentService = _commentService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostViewModel postVM)
        {
            postVM.Author = User.Identity.Name;
            postVM.CreatedDate = DateTime.Now;

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
            //postVM.ModifiedBy = User.Identity.Name;
            //postVM.ModifiedDate = DateTime.Now;

            //postService.Update(postVM);

            return View("Index");
        }
        //[HttpPost]
        public ActionResult Comment(CommentViewModel commentVM)
        {
            commentVM.CommentTime = DateTime.Now;
            commentService.Create(commentVM);
            return View();
        }
        public ActionResult GetCommentByPost(int id)
        {
            commentService.GetCommentByPost(id);
            return View("Comment");
        }
    }
}
