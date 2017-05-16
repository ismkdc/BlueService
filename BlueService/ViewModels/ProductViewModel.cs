using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueService.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserProfileImage { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public string Department { get; set; }
    }
}