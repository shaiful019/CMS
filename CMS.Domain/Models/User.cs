using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserIdentity { get; set; }
        public int FavoritePost { get; set; }
        public int FavoriteCatagory { get; set; }
        
    }
}
