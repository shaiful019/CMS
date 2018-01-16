﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string Content { get; set; }

        public virtual Post Post { get; set; }
    }
}
