using BlueService.Models;
using BlueService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlueService.Controllers
{
    public class ProductController : ApiController
    {
        BlueServiceDataContext dc;
        public ProductController()
        {
            dc = new BlueServiceDataContext();
        }
        public List<ProductViewModel> GetProducts()
        {
            return dc.Products.ToList().Select(p => new ProductViewModel() { Id = p.Id, Description = p.Description, ImageUrl = p.ImageUrl, Price = p.Price, Title = p.Title, Username = p.User.Name, UserProfileImage = p.User.ProfileImage, Date = p.Date.ToString("MM/dd/yy H:mm:ss"), Department = p.User.Department }).ToList();
        }
    }
}
