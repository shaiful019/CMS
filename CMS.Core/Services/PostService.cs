using System;
using System.Collections.Generic;
using System.Text;
using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using CMS.Core.Interfaces;
using CMS.Domain.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CMS.Core.Services
{
    public class PostService : IPostService
    {
        private UnitOfWork _unitOfWork;

        public PostService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Post Create(PostViewModel postVM, PostTermViewModel posttermVM)
        {
            var post = new Post
            {
                PostID = postVM.PostID,
                Title = postVM.Title,
                Content = postVM.Content,
                FeaturedImageUrl = postVM.FeaturedImageUrl,
                Url = postVM.Url,
                CreatedDate = postVM.CreatedDate,
                Author = postVM.Author,
                ModifiedBy = postVM.ModifiedBy,
                ModifiedDate = postVM.ModifiedDate
            };

            //_unitOfWork.PostRepository.Insert(post);

            post.PostTerms = new List<PostTerm>();


            foreach (int termID in posttermVM.TermID)
            {
                post.PostTerms.Add(new PostTerm
                {
                    TermID = termID
                });
                //_unitOfWork.PostTermRepository.Insert(term);
            }

            _unitOfWork.PostRepository.Insert(post);
            _unitOfWork.Save();

            return post;
        }

        public Post Update(PostViewModel postVM)
        {
            var post = _unitOfWork.PostRepository.GetById(postVM.PostID);

            post.Title = postVM.Title;
            post.Content = postVM.Content;
            post.FeaturedImageUrl = postVM.FeaturedImageUrl;
            post.Url = postVM.Url;
            post.ModifiedBy = postVM.ModifiedBy;
            post.ModifiedDate = postVM.ModifiedDate;

            _unitOfWork.PostRepository.Update(post);
            _unitOfWork.Save();

            return post;
        }

        public IEnumerable<PostViewModel> GetAllPost()
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate
                        }).AsEnumerable();

            return data;
        }

        public IEnumerable<PostViewModel> GetPostByAuthor(string author)
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        where s.Author == author
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate
                        }).AsEnumerable();

            return data;
        }

        public PostViewModel GetPostByID(int id)
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        where s.PostID == id
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate
                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<PostViewModel> GetPostByTerm(int termID)
        {
            var data = (from p in _unitOfWork.PostRepository.Get()
                        join pt in _unitOfWork.PostTermRepository.Get() on p.PostID equals pt.PostID
                        where pt.TermID == termID
                        select new PostViewModel
                        {
                            PostID = p.PostID,
                            Title = p.Title,
                            Content = p.Content,
                            FeaturedImageUrl = p.FeaturedImageUrl,
                            Url = p.Url,
                            CreatedDate = p.CreatedDate,
                            Author = p.Author,
                            ModifiedBy = p.ModifiedBy,
                            ModifiedDate = p.ModifiedDate
                        }).AsEnumerable();

            return data;
        }
        public void Uploadimage(IFormFile file)
        {
            var path = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Imagefiles/",
                       file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            
        }


    }
}
