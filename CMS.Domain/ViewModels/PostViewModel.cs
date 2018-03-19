using CMS.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CMS.Domain.ViewModels
{
    public class PostViewModel
    {

        public int PostID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        public string FeaturedImageUrl { get; set; }
        public IFormFile Image { get; set; }

        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Author { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
        public string Commentedby { get; set; }
        public int CommentIsApproved { get; set; }

        public IEnumerable<TermViewModel> Terms { get; set; }
        public List<int> Termid  { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}
