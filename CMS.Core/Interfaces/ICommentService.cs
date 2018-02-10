using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Interfaces
{
    public interface ICommentService
    {
        Comment Create(CommentViewModel commentVM);
        Comment Update(CommentViewModel commentVM);
        Comment Delete(CommentViewModel commentVM);

        IEnumerable<CommentViewModel> GetCommentByPost(int postID);

    }
}
