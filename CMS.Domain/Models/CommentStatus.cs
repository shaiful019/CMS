using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{
    public class CommentStatus
    {
        public int CommentStatusID { get; set; }
        public int CommentID { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
    }
}
