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
            return dc.UserFavoriteProducts.Where(u => u.Id == this.Id).Select(u => u.Product).ToList();
        }
        public void AddFavoriteProduct(Product product)
        {
            BlueServiceDataContext dc = new Models.BlueServiceDataContext();
            dc.UserFavoriteProducts.Add(new UserFavoriteProduct() { User = this, Product = product });
            dc.SaveChanges();
        }


    }
}