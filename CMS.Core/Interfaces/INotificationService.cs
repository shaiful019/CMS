using System;
using System.Collections.Generic;
using System.Text;
using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CMS.Core.Interfaces
{
    public interface INotificationService
    {
        Notification create(CommentViewModel CommentVM);
        int NotificationCount(string User);
        IEnumerable<NotificationViewModel> GetNotification(string User);
    }
}
