using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Interfaces
{
    public interface IPostService
    {
        Post Create(PostViewModel postVM, PostTermViewModel posttermVM);
        Post Update(PostViewModel postVM);
        PostViewModel GetPostByID(int id);
        IEnumerable<PostViewModel> GetPostByAuthor(string author);
        IEnumerable<PostViewModel> GetPostByTerm(int termID);
        IEnumerable<PostViewModel> GetAllPost();
        IEnumerable<TermViewModel> GetTermByPost(int postID);
        void Upload(IFormFile file);
        
    }
}
