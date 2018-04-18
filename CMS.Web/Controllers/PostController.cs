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
using System.IO;

namespace CMS.Web.Controllers
{
    //[Authorize]
    public class PostController : Controller
    {


        private IPostService postService;
        private ICommentService commentService;
        private ITermService termService;
        private INotificationService notificationService;
        
        public PostController(IPostService _postService, ICommentService _commentService, ITermService _termService, INotificationService _notificationService)
        {
            postService = _postService;
            commentService = _commentService;
            termService = _termService;
            notificationService = _notificationService;
        }
        
        public ActionResult Index(string searchcontent)
        {
                if (String.IsNullOrEmpty(searchcontent))
                {
                    var item = postService.GetAllPost();
                    return View(item);
                }
                else
                {
                    var item = postService.Search(searchcontent);
                    ViewBag.searchcontent = searchcontent;
                    return View(item);
                }

            
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
            
            var posttermVM = new PostTermViewModel
            {
                PostID = postVM.PostID,
                TermID = postVM.Termid
            };

            var poststatusVM = new PostStatusViewModel
            {
                PostID = postVM.PostID,
                ViewCount =0
                
            };

            var post=postService.Create(postVM, posttermVM, poststatusVM);
            var item = new PostViewModel
            {
                Terms = termService.GetAllTerm()
            };
            return RedirectToAction(nameof(Postview), new { id = post.PostID.ToString() });
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
                CommentedBy=postVM.Commentedby,
                ParentID = postVM.ParentID
            };

            commentService.Create(commentVM);
            if (!User.Identity.Name.Equals(postVM.Author))
            {
                notificationService.create(commentVM);
            }
            
            return RedirectToAction(nameof(Postview), new { id = postVM.PostID.ToString() });
        }
        public ActionResult Postview(int id)
        {
            var post = postService.GetPostByID(id);
            post.Comments = commentService.GetCommentByPost(id);
            post.CommentsChild = commentService.GetChildComment();
            post.Posts = postService.GetAllPost();
            post.Terms = postService.GetTermByPost(id);
            if (String.IsNullOrEmpty(User.Identity.Name) || !User.Identity.Name.Equals(post.Author))
            {
                postService.UpdatePostView(postService.GetPostView(id));
            }
            return View(post);
            
        }
        [Authorize]
        public ActionResult DashBoard()
        {
            PostViewModel post = new PostViewModel
            {
                Posts = postService.GetPostByAuthor(User.Identity.Name),
                Comments =commentService.GetCommentByAuthor(User.Identity.Name),
                CommentsApproval=commentService.CommentsToApprove(User.Identity.Name),
                PostView = postService.GetPostViewbByAuthor(User.Identity.Name)
            };
            
            

            return View(post);

        }
        public ActionResult ApproveComment()
        {
            var comment = commentService.CommentsToApprove(User.Identity.Name);
            return View(comment);
        }
        public ActionResult Search(string s)
        {
            var post = postService.Search(s); 
            return View(post);
        }
        public ActionResult PostByTerm(int id)
        {
            var post = postService.GetPostByTerm(id);
            return View(post);
        }
        public ActionResult LastPost()
        {
            var post = postService.GetLastPost(User.Identity.Name);
            post.Comments = commentService.GetCommentByPost(post.PostID);
            post.Posts = postService.GetAllPost();
            post.Terms = postService.GetTermByPost(post.PostID);
            if (String.IsNullOrEmpty(User.Identity.Name) || !User.Identity.Name.Equals(post.Author))
            {
                postService.UpdatePostView(postService.GetPostView(post.PostID));
            }
            return View(post);

        }
        public ActionResult Approve()
        {
            var comment = commentService.CommentsToApprove(User.Identity.Name);
            return View(comment);
        }

        public ActionResult Reject()
        {
            var comment = commentService.CommentsToApprove(User.Identity.Name);
            return View(comment);
        }

        
        public JsonResult GetAllNotification()
        {
            var notification = notificationService.GetNotification(User.Identity.Name);
            return Json(notification);
        }

        public JsonResult NotificationCount()
        {
            var notification = notificationService.NotificationCount(User.Identity.Name);
            return Json(notification);
        }
    }
}
