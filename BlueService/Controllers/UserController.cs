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
    public class UserController : ApiController
    {
        BlueServiceDataContext dc;
        public UserController()
        {
            dc = new BlueServiceDataContext();
        }
        [HttpPost]
        public Response Login(User user)
        {
            user.Password = Extras.GetMd5Hash(user.Password);
            if ((user = dc.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password)) != null)
            {
                return new Response(user.Token);
            }
            else
            {
                return new Response("Login Error");
            }
        }
        [HttpPost]
        public Response Register(User user)
        {
            user.Password = Extras.GetMd5Hash(user.Password);
            if (dc.Users.SingleOrDefault(u => u.Email == user.Email) != null)
            {
                return new Response("Error: User already registered");
            }
            else
            {
                user.Token = Extras.GetMd5Hash(user.Email + user.Password);
                dc.Users.Add(user);
                dc.SaveChanges();
                return new Response("Successfuly Register");
            }
        }

        public UserViewModel GetInfo(string token)
        {
            if (token.Length > 0)
            {
                var user = dc.Users.SingleOrDefault(u => u.Token == token);
                if (user != null)
                {
                    return new UserViewModel() { Name = user.Name, ProfileImgUrl = user.ProfileImage, SellingProducts = user.Products.Select(p => new ProductViewModel() { Id = p.Id, Title = p.Title, ImageUrl = p.ImageUrl }).ToList(), FavoriteProducts = user.GetFavoriteProducts().Select(p => new ProductViewModel() { Id = p.Id, Title = p.Title, ImageUrl = p.ImageUrl }).ToList() };
                }


            }
            return null;
        }
        [HttpPost]
        public Response UpdateProfilePhoto(UserProfilePhotoUpdateView up)
        {
            if (up.Token.Length > 0)
            {
                var user = dc.Users.SingleOrDefault(u => u.Token == up.Token);
                if (user != null)
                {
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

                    byte[] imageBytes = Convert.FromBase64String(up.Image);

                    File.WriteAllBytes(imgPath, imageBytes);

                    user.ProfileImage = "http://tukasservice.azurewebsites.net/files/" + imageName;
                    dc.SaveChanges();
                    return new Response("success");
                }


            }
            return null;
        }
    }
}
