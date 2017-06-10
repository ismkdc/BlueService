using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlueService.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string Department { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public string Token { get; set; }
        public User()
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("Arabic Standard Time");
            CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
            Products = new List<Models.Product>();
        }
        public List<Product> GetFavoriteProducts()
        {
            BlueServiceDataContext dc = new Models.BlueServiceDataContext();
            return dc.UserFavoriteProducts.Where(u => u.User.Id == this.Id).Select(u => u.Product).ToList();
        }
        public void AddFavoriteProduct(int _product, BlueServiceDataContext dc)
        {
            var product = dc.Products.SingleOrDefault(p => p.Id == _product);
            if (product == null) return;
            var data = dc.UserFavoriteProducts.SingleOrDefault(uf => uf.User.Id == this.Id && uf.Product.Id == _product);
            if(data == null)
            {
                dc.UserFavoriteProducts.Add(new UserFavoriteProduct() { User = this, Product = product });
            }
            else
            {
                dc.UserFavoriteProducts.Remove(data);
            }
           
            dc.SaveChanges();
        }
        public List<Conversation> GetConversations()
        {
            BlueServiceDataContext dc = new Models.BlueServiceDataContext();
            var data = dc.Conversations.Where(c => c.User1Id == this.Id || c.User2Id == this.Id).OrderByDescending(c => c.Date).ToList();
            foreach (var item in data)
            {
                if(item.User2Id == this.Id)
                {
                    item.User2 = dc.Users.SingleOrDefault(u => u.Id == item.User1Id);
                }
                else
                {
                    item.User2 = dc.Users.SingleOrDefault(u => u.Id == item.User2Id);
                }
               
            }
            return data;
        }
        



    }
}