using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Domain.Models
{
    public class Post : BaseEntity
    {

        public int PostID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        public string FeaturedImageUrl { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Author { get; set; }

        public ICollection<PostTerm> PostTerms { get; set; }

    }
}
