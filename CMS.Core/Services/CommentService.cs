using CMS.Core.Interfaces;
using CMS.Domain.Models;
using CMS.Domain.Repositories;
using CMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CMS.Core.Services
{
    public class CommentService : ICommentService
    {
        private UnitOfWork _unitOfWork;
        public CommentService(UnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public Comment Create(CommentViewModel commentVM)
        {
            var comment = new Comment
            {
                CommentID = commentVM.CommentID,
                Content = commentVM.Content,
                PostID = commentVM.PostID
            };
            _unitOfWork.CommentRepository.Insert(comment);
            _unitOfWork.Save();
            return comment;
        }

        public IEnumerable<CommentViewModel> GetCommentByPost(int postID)
        {
            var data = (from s in _unitOfWork.CommentRepository.Get()
                        select new CommentViewModel
                        {
                            CommentID = s.CommentID,
                            Content = s.Content,
                            PostID = s.PostID
                        }).AsEnumerable();

            return data;
        }
    }
}
