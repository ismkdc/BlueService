using BlueService.Models;
using BlueService.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<ProductViewModel> GetProducts(string token)
        {
            var user = dc.Users.SingleOrDefault(u => u.Token == token);
            if (user == null)
            {
                return null;
            }
            var favoriteProducts = user.GetFavoriteProducts();
            var produtcs = dc.Products.ToList().OrderByDescending(p => p.Date).Select(p => new ProductViewModel() { UserId = p.User.Id, Id = p.Id, ImageUrl = p.ImageUrl, Price = p.Price, Title = p.Title, Username = p.User.Name, UserProfileImage = p.User.ProfileImage, Date = p.Date.ToString("MM/dd/yy H:mm:ss"), Department = p.User.Department, Style = "black" }).ToList();

            foreach (var item2 in favoriteProducts)
            {
                var item = produtcs.SingleOrDefault(p => p.Id == item2.Id);
                if (item != null)
                    item.Style = "red";
            }

            return produtcs;

        }
        [HttpPost]
        public Response SellProduct(ProductSellModel product)
        {
            try
            {
                var user = dc.Users.SingleOrDefault(u => u.Token == product.Token);
                Product p = new Models.Product();
                p.Title = product.Title;
                p.Price = Double.Parse(product.Price);
                p.User = user;
                string path = System.Web.HttpContext.Current.Server.MapPath("~/files");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string imageName = Guid.NewGuid().ToString().Substring(0, 35) + ".jpg";
                string imgPath = Path.Combine(path, imageName);

                while (File.Exists(imgPath))
                {
                    imageName = Guid.NewGuid().ToString().Substring(0, 35) + ".jpg";
                    imgPath = Path.Combine(path, imageName);
                }

                byte[] imageBytes = Convert.FromBase64String(product.Image);

                File.WriteAllBytes(imgPath, imageBytes);
                p.ImageUrl = "http://tukasservice.azurewebsites.net/files/" + imageName;
                dc.Products.Add(p);
                dc.SaveChanges();
            }
            catch
            {
                return new Response("Error");
            }

            return new Response("success");
        }
        [HttpGet]
        public Response AddFavorite(string token, int id)
        {
            var user = dc.Users.SingleOrDefault(u => u.Token == token);
            if (user == null)
            {
                return null;
            }
            else
            {
                user.AddFavoriteProduct(id, dc);
                return new Response("success");
            }
        }
        [HttpGet]
        public Response DelFavorite(string token, int id)
        {
            var user = dc.Users.SingleOrDefault(u => u.Token == token);
            if (user == null)
            {
                return null;
            }
            else
            {
                var fav = dc.UserFavoriteProducts.SingleOrDefault(uf => uf.User.Id == user.Id && uf.Product.Id == id);
                if (fav == null) return null;
                dc.UserFavoriteProducts.Remove(fav);
                dc.SaveChanges();
                return new Response("success");
            }
        }

        [HttpGet]
        public Response DelProduct(string token, int id)
        {
            var user = dc.Users.SingleOrDefault(u => u.Token == token);
            if (user == null)
            {
                return null;
            }
            else
            {
                var product = dc.Products.SingleOrDefault(p => p.User.Id == user.Id && p.Id == id);

                if (product == null) return null;
                foreach (var item in dc.UserFavoriteProducts.Where(uf => uf.Product.Id == product.Id))
                {
                    dc.UserFavoriteProducts.Remove(item);
                }
                dc.Products.Remove(product);
                dc.SaveChanges();
                return new Response("success");
            }
        }

    }
}
