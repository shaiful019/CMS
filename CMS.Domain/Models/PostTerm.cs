﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{
    public class PostTerm
    {
        public int PostTermID { get; set; }
        public int PostID { get; set; }
        public int TermID { get; set; }

        public virtual Post Post { get; set; }
        public virtual Term Term { get; set; }
    }
}
