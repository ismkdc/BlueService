using BlueService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueService.ViewModels
{
    public class UserViewModel
    {
        public string ProfileImgUrl { get; set; }
        public string Name { get; set; }
        public List<ProductViewModel> SellingProducts { get; set; }
        public List<ProductViewModel> FavoriteProducts { get; set; }
    }
}