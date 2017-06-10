using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlueService.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public Product()
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("Arabic Standard Time");
            Date = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
        }
    }
}